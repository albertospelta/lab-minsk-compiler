@echo off

dotnet --info
dotnet restore .\src\Minsk.sln
dotnet build .\src\Minsk.sln --no-restore
dotnet test .\src\Minsk.Tests\Minsk.Tests.csproj --no-build --verbosity normal