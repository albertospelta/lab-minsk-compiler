using System;

namespace Minsk.CodeAnalysis
{
    public class Evaluator
    {
        private readonly ExpressionSyntax _root;

        public Evaluator(ExpressionSyntax root) => _root = root;

        public int Evaluate() => EvaluateExpression(_root);

        private int EvaluateExpression(ExpressionSyntax node)
        {
            if (node is LiteralExpressionSyntax n)
                return (int)n.LiteralToken.Value;

            if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                return b.OperatorToken.Kind switch
                {
                    SyntaxKind.PlusToken => left + right,
                    SyntaxKind.MinusToken => left - right,
                    SyntaxKind.StarToken => left * right,
                    SyntaxKind.SlashToken => left / right,
                    _ => throw new Exception($"Unexpected binary operator '{ b.OperatorToken.Kind }'")
                };
            }

            if (node is ParenthesizedExpressionSyntax p)
                return EvaluateExpression(p.Expression);

            throw new Exception($"Unexpected node '{ node.Kind }'");
        }
    }
}
