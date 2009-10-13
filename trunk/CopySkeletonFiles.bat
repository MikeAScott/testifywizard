@echo off
setlocal
set SolutionDir=%1
set TargetDir=%2

echo ***************************************************************
echo copying skeleton files:
echo     from: %SolutionDir%
echo     to:   %TargetDir%
echo ***************************************************************
echo.

mkdir "%TargetDir%\Resources"

xcopy /s /e /y "%SolutionDir%\Resources\*.*" "%TargetDir%\Resources"

echo.