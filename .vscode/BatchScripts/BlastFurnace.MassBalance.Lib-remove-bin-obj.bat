@echo off
@echo =================================================================
@echo BlastFurnace.MassBalance.Lib - Remove bin and obj folders
@echo =================================================================
@echo off
@echo.
@echo.

cd ..\
md log
cd ..\
cd src\csharp\BlastFurnace.MassBalance.Lib
dir /b /s > ..\..\.vscode\log\BlastFurnace.MassBalance.Lib-Files-0-before.txt
@echo on
rd /S /Q bin
rd /S /Q obj
@echo off
dir /b /s > ..\..\.vscode\log\BlastFurnace.MassBalance.Lib-Files-1-after.txt
cd ..\
@REM pause