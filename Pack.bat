@echo off
echo BUILD Cadmus Tivac Packages
del .\Cadmus.Tivac.Parts\bin\Debug\*.nupkg
del .\Cadmus.Tivac.Parts\bin\Debug\*.snupkg
del .\Cadmus.Seed.Tivac.Parts\bin\Debug\*.nupkg
del .\Cadmus.Seed.Tivac.Parts\bin\Debug\*.snupkg
del .\Cadmus.Tivac.Services\bin\Debug\*.nupkg
del .\Cadmus.Tivac.Services\bin\Debug\*.snupkg

cd .\Cadmus.Tivac.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Tivac.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Tivac.Services
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
