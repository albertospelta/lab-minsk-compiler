﻿using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Lowering;
using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;

namespace Minsk.CodeAnalysis
{
    public class Compilation
    {
        private BoundGlobalScope _globalScope;

        public Compilation(SyntaxTree syntaxTree)
            : this(null, syntaxTree)
        {
        }

        private Compilation(Compilation previous, SyntaxTree syntaxTree)
        {
            Previous = previous;
            SyntaxTree = syntaxTree;
        }

        public Compilation Previous { get; }
        public SyntaxTree SyntaxTree { get; }

        internal BoundGlobalScope GlobalScope
        {
            get
            {
                if (_globalScope == null)
                {
                    var globalScope = Binder.BindGlobalScope(Previous?.GlobalScope, SyntaxTree.Root);
                    Interlocked.CompareExchange(ref _globalScope, globalScope, null); // for thread safety
                }

                return _globalScope;
            }
        }

        public Compilation ContinueWith(SyntaxTree syntaxTree)
        {
            return new Compilation(this, syntaxTree);
        }

        public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
        {
            var diagnostics = SyntaxTree.Diagnostics.Concat(GlobalScope.Diagnostics).ToImmutableArray();
            if (diagnostics.Any())
                return new EvaluationResult(diagnostics, null);

            var program = Binder.BindProgram(GlobalScope);
            if (program.Diagnostics.Any())
                return new EvaluationResult(program.Diagnostics.ToImmutableArray(), null);

            var statement = GetStatement();
            var evaluator = new Evaluator(program.FunctionBodies, statement, variables);
            var value = evaluator.Evaluate();
            return new EvaluationResult(ImmutableArray<Diagnostic>.Empty, value);
        }

        public void EmitTree(TextWriter writer)
        {
            var statement = GetStatement();
            statement.WriteTo(writer);
        }

        private BoundBlockStatement GetStatement()
        {
            var statement = GlobalScope.Statement;
            return Lowerer.Lower(statement);
        }
    }
}
