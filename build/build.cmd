@echo off

dotnet build .\minsk.sln /nologo
dotnet test .\Minsk.Tests\Minsk.Tests.csproj