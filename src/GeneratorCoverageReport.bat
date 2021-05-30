cd /d %~dp0
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/
start ./TestResults/CoverageReport/index.html
