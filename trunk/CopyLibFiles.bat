@echo off
setlocal
set SolutionDir=%1
set TargetDir=%2

echo ***************************************************************
echo copying library files:
echo     to: %SolutionDir%\lib
echo ***************************************************************
echo.

xcopy /y "%SolutionDir%\..\tools\nunit\nunit.framework.dll" "%SolutionDir%\..\lib\nUnit"
xcopy /y "%SolutionDir%\..\FitNesse\dotnet2\fit.dll" "%SolutionDir%\..\lib\FitNesseDotNet"
xcopy /y "%SolutionDir%\..\FitNesse\dotnet2\fitlibrary.dll" "%SolutionDir%\..\lib\FitNesseDotNet"

echo.