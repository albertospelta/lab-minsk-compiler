using Minsk.CodeAnalysis.Binding;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Minsk.CodeAnalysis.Lowering
{
    internal class Lowerer : BoundTreeRewriter
    {
        private Lowerer()
        {
        }

        public static BoundStatement Lower(BoundStatement statement)
        {
            var lowerer = new Lowerer();
            return lowerer.RewriteStatement(statement);
        }
    }
}
