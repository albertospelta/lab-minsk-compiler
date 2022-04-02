using Minsk.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;

namespace Minsk.CodeAnalysis.Syntax
{
    public class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object? value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object? Value { get; }
        public bool IsMissing => Text == null;

        public override TextSpan Span => new TextSpan(Position, Text?.Length ?? 0);
    }
}
