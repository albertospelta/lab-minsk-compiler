@echo off
REM dotnet --info
dotnet build Minsk.Compiler.slnf
dotnet test Minsk.Compiler.slnf --no-build