using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minsk
{
    internal static class Program
    {
        private static void Main()
        {
            bool showTree = false;

            while (true)
            {
                Console.Write(">");
                
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                if (line == "#showTree" || line == "#st")
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
                var binder = new Binder();
                var boundExpression = binder.BindExpression(syntaxTree.Root);
                var diagnosticts = syntaxTree.Diagnostics.Concat(binder.Diagnostics);

                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Print(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (diagnosticts.Any())
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var diagnostic in syntaxTree.Diagnostics)
                        Console.WriteLine(diagnostic);
                    Console.ResetColor();
                }
                else
                {
                    var evaluator = new Evaluator(boundExpression);
                    var result = evaluator.Evaluate();
                    Console.WriteLine(result);
                }
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

            indent += isLast ? "   " : "│   ";

            var children = node.GetChildren();
            foreach (var child in children)
                Print(child, indent, isLast: child == children.LastOrDefault());
        }
    }
}
