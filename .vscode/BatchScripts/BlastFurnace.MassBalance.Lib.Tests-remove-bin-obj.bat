@echo off
@echo =================================================================
@echo BlastFurnace.MassBalance.Lib.Tests - Remove bin and obj folders
@echo =================================================================
@echo off
@echo.
@echo.

cd ..\
md log
cd ..\
cd src\csharp\BlastFurnace.MassBalance.Lib.Tests
dir /b /s > ..\..\..\.vscode\log\BlastFurnace.MassBalance.Lib.Tests-Files-0-before.txt
@echo on
rd /S /Q bin
rd /S /Q obj
@echo off
dir /b /s > ..\..\..\.vscode\log\BlastFurnace.MassBalance.Lib.Tests-Files-1-after.txt
cd ..\
@REM pause