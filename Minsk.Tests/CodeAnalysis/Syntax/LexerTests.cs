using Minsk.CodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Minsk.Test.CodeAnalysis.Syntax
{
    public class LexerTests
    {
        [Fact]
        public void Lexer_Tests_AllTokens()
        {
            var tokenKinds = Enum.GetValues(typeof(SyntaxKind))
                .Cast<SyntaxKind>()
                .Where((k) => 
                    k.ToString().EndsWith("Keyword") || 
                    k.ToString().EndsWith("Token")
                    );

            var testedTokenKinds = GetTokens().Concat(GetSeparators()).Select((t) => t.kind);
            var untestedTokenKinds = new SortedSet<SyntaxKind>(tokenKinds);
            untestedTokenKinds.Remove(SyntaxKind.EndOfFileToken);
            untestedTokenKinds.Remove(SyntaxKind.BadToken);
            untestedTokenKinds.ExceptWith(testedTokenKinds);

            Assert.Empty(untestedTokenKinds);
        }

        [Theory]
        [MemberData(nameof(GetTokensData))]
        public void Lexer_Lexes_Token(SyntaxKind kind, string text)
        {
            var tokens = SyntaxTree.ParseTokens(text);

            var token = Assert.Single(tokens);
            Assert.Equal(kind, token.Kind);
            Assert.Equal(text, token.Text);
        }

        [Theory]
        [MemberData(nameof(GetTokenPairsData))]
        public void Lexer_Lexes_TokenPairs(SyntaxKind type1Kind, string type1Text, SyntaxKind type2Kind, string type2Text)
        {
            var text = type1Text + type2Text;
            var tokens = SyntaxTree.ParseTokens(text).ToArray();

            Assert.Equal(2, tokens.Length);
            Assert.Equal(tokens[0].Kind, type1Kind);
            Assert.Equal(tokens[0].Text, type1Text);
            Assert.Equal(tokens[1].Kind, type2Kind);
            Assert.Equal(tokens[1].Text, type2Text);
        }

        [Theory]
        [MemberData(nameof(GetTokenPairsWithSeparatorData))]
        public void Lexer_Lexes_TokenPairs_WithSeparator(SyntaxKind type1Kind, string type1Text, SyntaxKind separetorKind, string separatorText, SyntaxKind type2Kind, string type2Text)
        {
            var text = type1Text + separatorText + type2Text;
            var tokens = SyntaxTree.ParseTokens(text).ToArray();

            Assert.Equal(3, tokens.Length);
            Assert.Equal(tokens[0].Kind, type1Kind);
            Assert.Equal(tokens[0].Text, type1Text);
            Assert.Equal(tokens[1].Kind, separetorKind);
            Assert.Equal(tokens[1].Text, separatorText);
            Assert.Equal(tokens[2].Kind, type2Kind);
            Assert.Equal(tokens[2].Text, type2Text);
        }

        public static IEnumerable<object[]> GetTokensData()
        {
            foreach (var (kind, text) in GetTokens().Concat(GetSeparators()))
                yield return new object[] { kind, text };
        }

        public static IEnumerable<object[]> GetTokenPairsData()
        {
            foreach (var (type1Kind, type1Text, type2Kind, type2Text) in GetTokenPairs())
                yield return new object[] { type1Kind, type1Text, type2Kind, type2Text };
        }

        public static IEnumerable<object[]> GetTokenPairsWithSeparatorData()
        {
            foreach (var token in GetTokenPairsWithSeparator())
            {
                yield return new object[]
                {
                    token.type1Kind,
                    token.type1Text,
                    token.separetorKind,
                    token.separatorText,
                    token.type2Kind,
                    token.type2Text
                };
            }
        }

        public static IEnumerable<(SyntaxKind kind, string text)> GetTokens()
        {
            var fixedTokens = Enum.GetValues(typeof(SyntaxKind))
                .Cast<SyntaxKind>()
                .Select((k) => (kind: k, text: SyntaxFacts.GetText(k)))
                .Where((t) => t.text != null);

            var dynamicTokens = new[]
            {
                (SyntaxKind.NumberToken, "1"),
                (SyntaxKind.NumberToken, "123"),
                (SyntaxKind.IdentifierToken, "a"),
                (SyntaxKind.IdentifierToken, "abc"),
            };

            return fixedTokens.Concat(dynamicTokens);
        }

        public static IEnumerable<(SyntaxKind kind, string text)> GetSeparators()
        {
            return new[]
            {
                (SyntaxKind.WhiteSpaceToken, " "),
                (SyntaxKind.WhiteSpaceToken, "  "),
                (SyntaxKind.WhiteSpaceToken, "\r"),
                (SyntaxKind.WhiteSpaceToken, "\n"),
                (SyntaxKind.WhiteSpaceToken, "\r\n")
            };
        }

        private static bool RequiresSeparator(SyntaxKind type1Kind, SyntaxKind type2Kind)
        {
            var type1IsKeyword = type1Kind.ToString().EndsWith("Keyword");
            var type2IsKeyword = type2Kind.ToString().EndsWith("Keyword");

            if (type1Kind == SyntaxKind.IdentifierToken && type2Kind == SyntaxKind.IdentifierToken)
                return true;

            if (type1IsKeyword && type2IsKeyword)
                return true;

            if (type1IsKeyword && type2Kind == SyntaxKind.IdentifierToken)
                return true;

            if (type1Kind == SyntaxKind.IdentifierToken && type2IsKeyword)
                return true;

            if (type1Kind == SyntaxKind.NumberToken && type2Kind == SyntaxKind.NumberToken)
                return true;

            if (type1Kind == SyntaxKind.BangToken && type2Kind == SyntaxKind.EqualsToken)
                return true;

            if (type1Kind == SyntaxKind.BangToken && type2Kind == SyntaxKind.EqualsEqualsToken)
                return true;

            if (type1Kind == SyntaxKind.EqualsToken && type2Kind == SyntaxKind.EqualsToken)
                return true;

            if (type1Kind == SyntaxKind.EqualsToken && type2Kind == SyntaxKind.EqualsEqualsToken)
                return true;

            return false;
        }

        public static IEnumerable<(SyntaxKind type1Kind, string type1Text, SyntaxKind type2Kind, string type2Text)> GetTokenPairs()
        {
            foreach (var type1 in GetTokens())
            {
                foreach (var type2 in GetTokens())
                {
                    if (!RequiresSeparator(type1.kind, type2.kind))
                        yield return (type1.kind, type1.text, type2.kind, type2.text);
                }
            }
        }
        public static IEnumerable<(SyntaxKind type1Kind, string type1Text, SyntaxKind separetorKind, string separatorText, SyntaxKind type2Kind, string type2Text)> GetTokenPairsWithSeparator()
        {
            foreach (var type1 in GetTokens())
            {
                foreach (var type2 in GetTokens())
                {
                    if (RequiresSeparator(type1.kind, type2.kind))
                    {
                        foreach (var separator in GetSeparators())
                        {
                            yield return (type1.kind, type1.text, separator.kind, separator.text, type2.kind, type2.text);
                        }
                    }
                }
            }
        }
    }
}
