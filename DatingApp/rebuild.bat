@echo off
cd datingapp-spa

echo RESTORING NPM PACKAGES
call npm install

echo.
echo.
echo BUILDING SPA
call ng build
cd ..
cd DatingApp.api

echo.
echo.
echo BUILDING API
call dotnet build
pause