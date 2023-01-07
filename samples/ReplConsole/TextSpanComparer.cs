﻿namespace ReplConsole
{
    using System.Collections.Generic;
    using Minsk.CodeAnalysis.Text;

    internal class TextSpanComparer : IComparer<TextSpan>
    {
        public int Compare(TextSpan x, TextSpan y)
        {
            var cmp = x.Start - y.Start;
            if (cmp == 0)
                cmp = x.Length - y.Length;
            return cmp;
        }
    }
}