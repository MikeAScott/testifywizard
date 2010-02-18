@echo off
setlocal
set port=%1
start "Selenium Server" /min java -jar selenium-server.jar -port %1 %2 %3 %4 %5 %6
echo **************************************
echo * Selenium server is running on %port% *
echo **************************************
