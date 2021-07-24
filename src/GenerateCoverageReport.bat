cd /d %~dp0
dotnet build
dotnet test WaterTrans.Boilerplate.UnitTests /p:CollectCoverage=true  /p:CoverletOutput=../TestResults/ --no-build
dotnet test WaterTrans.Boilerplate.IntegrationTests --filter "TestCategory=IntegrationTests|TestCategory=IntegrationTests.Web.Api" /p:CollectCoverage=true  /p:CoverletOutput=../TestResults/ /p:MergeWith="../TestResults/coverage.json" --no-build
dotnet test WaterTrans.Boilerplate.IntegrationTests --filter "TestCategory=IntegrationTests.Web.Server" /p:CollectCoverage=true  /p:CoverletOutput=../TestResults/ /p:MergeWith="../TestResults/coverage.json" /p:CoverletOutputFormat=cobertura --no-build
dotnet tool install dotnet-reportgenerator-globaltool
dotnet reportgenerator -reports:TestResults/coverage.cobertura.xml -targetdir:TestResults/CoverageReport -reporttypes:HtmlInline
start ./TestResults/CoverageReport/index.html
