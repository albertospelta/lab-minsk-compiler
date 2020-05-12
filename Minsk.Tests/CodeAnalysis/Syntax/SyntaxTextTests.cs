using Minsk.CodeAnalysis.Text;
using Xunit;

namespace Minsk.Test.CodeAnalysis.Syntax
{
    public class SyntaxTextTests
    {
        [Theory]
        [InlineData(".", 1)]
        [InlineData(".\r\n", 2)]
        [InlineData(".\r\n\r\n", 3)]
        public void  SourceText_IncludesLastLine(string text, int exprectedLineCount)
        {
            var sourceText = SourceText.From(text);
            Assert.Equal(exprectedLineCount, sourceText.Lines.Length);
        }
    }
}
