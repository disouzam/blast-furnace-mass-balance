@echo off
@echo =================================================================
@echo BlastFurnace.MassBalance.App - Remove bin and obj folders
@echo =================================================================
@echo off
@echo.
@echo.

cd ..\
md log
cd ..\
cd csharp\src\BlastFurnace.MassBalance.App
dir /b /s > ..\..\..\.vscode\log\BlastFurnace.MassBalance.App-Files-0-before.txt
@echo on
rd /S /Q bin
rd /S /Q obj
@echo off
dir /b /s > ..\..\..\.vscode\log\BlastFurnace.MassBalance.App-Files-1-after.txt
cd ..\
@REM pause