using Minsk.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minsk
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showTree = false;

            while (true)
            {
                Console.Write(">");
                
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                if (line == "#tree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees" : "Not showing parse trees");
                    continue;
                }
                else if (line == "#cls")
                {
                    Console.Clear();
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);

                if (showTree)
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Print(syntaxTree.Root);
                    Console.ForegroundColor = color;
                }

                if (syntaxTree.Diagnostics.Any())
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var diagnostic in syntaxTree.Diagnostics)
                        Console.WriteLine(diagnostic);
                    Console.ForegroundColor = color;
                }
                else
                {
                    var evaluator = new Evaluator(syntaxTree.Root);
                    var result = evaluator.Evaluate();
                    Console.WriteLine(result);
                }

                //var lexer = new Lexer(line);
                //while (true)
                //{
                //    var token = lexer.NextToken();
                //    if (token.Kind == SyntaxKind.EndOfFileToken)
                //        break;

                //    Console.Write($"{token.Kind}: '{token.Text}'");
                //    if (token.Value != null)
                //        Console.Write($" {token.Value}");

                //    Console.WriteLine();
                //}

                //if (line == "1 + 2 * 3")
                //    Console.WriteLine("7");
                //else
                //    Console.WriteLine("ERROR: Invalid expression");
            }
        }

        static void Print(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            var children = node.GetChildren();
            foreach (var child in children)
                Print(child, indent, isLast: child == children.LastOrDefault());
        }
    }
}
