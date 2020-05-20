using System;
using System.Collections.Generic;

namespace Minsk.CodeAnalysis.Syntax
{
    public static class SyntaxFacts
    {
        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.BangToken:
                    return 6;
                default:
                    return 0;
            }
        }

        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 5;
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 4;
                case SyntaxKind.EqualsEqualsToken:
                case SyntaxKind.BangEqualsToken:
                case SyntaxKind.LessToken:
                case SyntaxKind.LessOrEqualsToken:
                case SyntaxKind.GreatToken:
                case SyntaxKind.GreaterOrEqualsToken:
                    return 3;
                case SyntaxKind.AmpersandAmpersandToken:
                    return 2;
                case SyntaxKind.PipePipeToken:
                    return 1;
                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            return text switch
            {
                "true" => SyntaxKind.TrueKeyword,
                "if" => SyntaxKind.IfKeyword,
                "else" => SyntaxKind.ElseKeyword,
                "false" => SyntaxKind.FalseKeyword,
                "let" => SyntaxKind.LetKeyword,
                "var" => SyntaxKind.VarKeyword,
                _ => SyntaxKind.IdentifierToken,
            };
        }

        public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds()
        {
            var kinds = (SyntaxKind[])Enum.GetValues(typeof(SyntaxKind));
            foreach (var kind in kinds)
            {
                if (GetUnaryOperatorPrecedence(kind) > 0)
                    yield return kind;
            }
        }

        public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds()
        {
            var kinds = (SyntaxKind[])Enum.GetValues(typeof(SyntaxKind));
            foreach (var kind in kinds)
            {
                if (GetBinaryOperatorPrecedence(kind) > 0)
                    yield return kind;
            }
        }

        public static string GetText(SyntaxKind kind)
        {
            return kind switch
            {
                SyntaxKind.PlusToken => "+",
                SyntaxKind.MinusToken => "-",
                SyntaxKind.StarToken => "*",
                SyntaxKind.SlashToken => "/",
                SyntaxKind.BangToken => "!",
                SyntaxKind.EqualsToken => "=",
                SyntaxKind.LessToken => "<",
                SyntaxKind.LessOrEqualsToken => "<=",
                SyntaxKind.GreatToken => ">",
                SyntaxKind.GreaterOrEqualsToken => ">=",
                SyntaxKind.AmpersandAmpersandToken => "&&",
                SyntaxKind.PipePipeToken => "||",
                SyntaxKind.EqualsEqualsToken => "==",
                SyntaxKind.BangEqualsToken => "!=",
                SyntaxKind.OpenParenthesisToken => "(",
                SyntaxKind.CloseParenthesisToken => ")",
                SyntaxKind.OpenBraceToken => "{",
                SyntaxKind.CloseBraceToken => "}",
                SyntaxKind.TrueKeyword => "true",
                SyntaxKind.FalseKeyword => "false",
                SyntaxKind.IfKeyword => "if",
                SyntaxKind.ElseKeyword => "else",
                SyntaxKind.LetKeyword => "let",
                SyntaxKind.VarKeyword => "var",
                _ => null,
            };
        }
    }
}
