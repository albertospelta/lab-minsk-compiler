# Minsk

[![Build Status](https://dev.azure.com/albertospelta/Minsk/_apis/build/status/albertospelta.minsk?branchName=master)](https://dev.azure.com/albertospelta/Minsk/_build/latest?definitionId=8&branchName=master)

> Building a compiler

  1. expression evaluator
    * Basic REPL (read-eval-print loop) for an expression evaluator
    * Added lexer, a parser, and an evaluator
    * Handle `+`, `-`, `*`, `/`, and parenthesized expressions
    * Print syntax trees

  2. more operators and literals
    * Generalized parsing using precedences
    * Support unary operators, such as `+2` and `-3`
    * Support for Boolean literals (`false`, `true`)
    * Support for conditions such as `1 == 3 && 2 != 3 || true`
    * Internal representation for type checking (`Binder`, and `BoundNode`)

  3. assignments and variables 
    * Extracted compiler into a separate library
    * Exposed span on diagnostics that indicate where the error occurred
    * Support for assignments and variables

  4. add tests
  [ ... ]