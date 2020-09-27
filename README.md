# Minsk [http://minsk-compiler.net]

[![Build Status](https://dev.azure.com/albertospelta/Minsk/_apis/build/status/albertospelta.minsk?branchName=master)](https://dev.azure.com/albertospelta/Minsk/_build/latest?definitionId=8&branchName=master)

> Building a compiler

This repo contains **Minsk**, a handwritten compiler in C#. It illustrates basic concepts of compiler construction and how one can tool the language inside of an IDE by exposing APIs for parsing and type checking. This compiler uses many of the concepts that you can find in the Microsoft C# and Visual Basic compilers, code named [Roslyn].

[ds9-minsk]: https://www.youtube.com/watch?v=138gX3wolOo
[Roslyn]: https://github.com/dotnet/roslyn

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

### 9. A better REPL

* Add REPL ability to edit multiple lines, have history and syntax highlighting

### 10. String literals and type symbols

* Added support for string literals and type symbols

### 11. Function calls

* Added support for calling built-in functions and convert between types

### 12. Declaring functions

* Added support for explicit typing of variables and function declarations

### 13. Declaring functions

* Added pretty printing for bound nodes as well as `break` and `continue` statements

### 14. Return statements and control flow analysis

* Added support for return statements and control flow analysis.

### [ ... ]