# Minsk

[![Build Status](https://dev.azure.com/albertospelta/Minsk/_apis/build/status/albertospelta.minsk?branchName=master)](https://dev.azure.com/albertospelta/Minsk/_build/latest?definitionId=8&branchName=master)

> Building a compiler

### 1. Expression evaluator

* Basic REPL (read-eval-print loop) for an expression evaluator
* Added lexer, a parser, and an evaluator
* Handle `+`, `-`, `*`, `/`, and parenthesized expressions
* Print syntax trees

### 2. More operators and literals

* Generalized parsing using precedences
* Support unary operators, such as `+2` and `-3`
* Support for Boolean literals (`false`, `true`)
* Support for conditions such as `1 == 3 && 2 != 3 || true`
* Internal representation for type checking (`Binder`, and `BoundNode`)

### 3. Assignments and variables 
  
* Extracted compiler into a separate library
* Exposed span on diagnostics that indicate where the error occurred
* Support for assignments and variables

### 4. Add tests

* Added tests for lexing all tokens and their combinations
* Added tests for parsing unary and binary operators
* Added tests for evaluating
* Added test execution to our CI

### 5. Line numbers and clean-up

* Code clean-up
* Added `SourceText`, which allows to compute line number information

### 6. Line numbers and clean-up

* Added colorization to REPL
* Added compilation unit
* Added chaining to compilations
* Added statements
* Added variable declaration statements

### 7. Declarative tests, if-, while-, and for-statements

* Made evaluation tests more declarative, especially for diagnostics
* Added support for `<,` `<=`, `>=`, and `>`
* Added support for if-statements
* Added support for while-statements
* Added support for for-statements
* Ensure parser doesn't loop infinitely on malformed block
* Ensure binder doesn't crash when binding fabricated identifiers

### 8. Lowering

* Add support for bitwise operators
* Add ability to output the bound tree
* Add ability to lower bound tree
* Lower `for`-statements into `while`-statements
* Print syntax and bound tree before evaluation
* Lower `if`, `while`, and `for` into gotos

### [ ... ]