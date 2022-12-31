@REM In order to execute sonar coverage, you'll need to have sonarqube community downloaded and running local on your machine
@REM and sonarscanner installed as well and placed its /bin folder as a PATH ENVIRONMENT VARIABLE.
@REM Additionaly, you need to provide a Project TOKEN and set its value on token parameter (line 7).

set project-key="blast-furnace-mass-balance"

set project-name="BlastFurnace.MassBalance"

set token="sqp_c7df1b0fd8bb4bd70456674f5520dd31f8b5661e"

set host=http://localhost:9000
 
@REM dotnet new tool-manifest --force

@REM dotnet tool update --local dotnet-sonarscanner --version 5.9.2

set CollectCoverage=true
echo %CollectCoverage%

set CoverletOutputFormat=opencover
echo %CoverletOutputFormat%

set "CoverletOutput=coverage.opencover.xml"
echo %CoverletOutput%

dotnet test -v:Minimal -c:Debug 

dotnet build-server shutdown

dotnet sonarscanner begin  /k:%project-key% /n:%project-name% /v:"v0" /d:sonar.host.url=%host% /d:sonar.login=%token% /d:sonar.cs.opencover.reportsPaths="**\coverage.opencover.xml"

dotnet build

dotnet sonarscanner end /d:sonar.login=%token%