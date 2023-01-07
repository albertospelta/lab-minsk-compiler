namespace ReplConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Minsk.CodeAnalysis;
    using Minsk.CodeAnalysis.Symbols;
    using Minsk.CodeAnalysis.Syntax;
    using Minsk.CodeAnalysis.Text;

    internal sealed class MinskRepl : Repl
    {
        private Compilation _previous;
        private bool _showTree;
        private bool _showProgram;
        private readonly Dictionary<VariableSymbol, object> _variables = new Dictionary<VariableSymbol, object>();

        protected override void RenderLine(string line)
        {
            var tokens = SyntaxTree.ParseTokens(line);
            foreach (var token in tokens)
            {
                var isKeyword = token.Kind.ToString().EndsWith("Keyword");
                var isIdentifier = token.Kind == SyntaxKind.IdentifierToken;
                var isNumber = token.Kind == SyntaxKind.NumberToken;
                var isString = token.Kind == SyntaxKind.StringToken;

                if (isKeyword)
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                else if (isIdentifier)
                    System.Console.ForegroundColor = ConsoleColor.DarkYellow;
                else if (isNumber)
                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                else if (isString)
                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                else
                    System.Console.ForegroundColor = ConsoleColor.DarkGray;

                System.Console.Write(token.Text);

                System.Console.ResetColor();
            }
        }

        protected override void EvaluateMetaCommand(string input)
        {
            switch (input)
            {
                case "#showTree":
                case "#st":
                    _showTree = !_showTree;
                    System.Console.WriteLine(_showTree ? "Showing parse trees." : "Not showing parse trees.");
                    break;
                case "#showProgram":
                case "#sp":
                    _showProgram = !_showProgram;
                    System.Console.WriteLine(_showProgram ? "Showing bound tree." : "Not showing bound tree.");
                    break;
                case "#cls":
                    System.Console.Clear();
                    break;
                case "#reset":
                    _previous = null;
                    _variables.Clear();
                    break;
                default:
                    base.EvaluateMetaCommand(input);
                    break;
            }
        }

        protected override bool IsCompleteSubmission(string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;

            var lastToLinesAreBlank = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                                           .Reverse()
                                           .TakeWhile((s) => string.IsNullOrEmpty(s))
                                           .Take(2)
                                           .Count() == 2;
            if (lastToLinesAreBlank)
                return true;

            var syntaxTree = SyntaxTree.Parse(text);

            // Use Members because we need to exclude the EndOfFileToken.
            if (syntaxTree.Root.Members.Last().GetLastToken().IsMissing)
                return false;

            return true;
        }

        protected override void EvaluateSubmission(string text)
        {
            var syntaxTree = SyntaxTree.Parse(text);

            var compilation = _previous == null
                                ? new Compilation(syntaxTree)
                                : _previous.ContinueWith(syntaxTree);

            if (_showTree)
                syntaxTree.Root.WriteTo(System.Console.Out);

            if (_showProgram)
                compilation.EmitTree(System.Console.Out);

            var result = compilation.Evaluate(_variables);

            if (!result.Diagnostics.Any())
            {
                if (result.Value != null)
                {
                    System.Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine(result.Value);
                    System.Console.ResetColor();
                }
                _previous = compilation;
            }
            else
            {
                foreach (var diagnostic in result.Diagnostics.OrderBy((d) => d.Span, new TextSpanComparer()))
                {
                    var lineIndex = syntaxTree.Text.GetLineIndex(diagnostic.Span.Start);
                    var line = syntaxTree.Text.Lines[lineIndex];
                    var lineNumber = lineIndex + 1;
                    var character = diagnostic.Span.Start - line.Start + 1;

                    System.Console.WriteLine();
                    System.Console.ForegroundColor = ConsoleColor.DarkRed;
                    System.Console.Write($"({ lineNumber }, { character }): ");
                    System.Console.WriteLine(diagnostic);
                    System.Console.ResetColor();

                    var prefixSpan = TextSpan.FromBounds(line.Start, diagnostic.Span.Start);
                    var suffixSpan = TextSpan.FromBounds(diagnostic.Span.End, line.End);

                    var prefix = syntaxTree.Text.ToString(prefixSpan);
                    var error = syntaxTree.Text.ToString(diagnostic.Span);
                    var suffix = syntaxTree.Text.ToString(suffixSpan);

                    System.Console.Write("    ");
                    System.Console.Write(prefix);

                    System.Console.ForegroundColor = ConsoleColor.DarkRed;
                    System.Console.Write(error);
                    System.Console.ResetColor();

                    System.Console.Write(suffix);
                    System.Console.WriteLine();
                }

                System.Console.WriteLine();
            }
        }
    }
}