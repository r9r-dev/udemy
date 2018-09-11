@echo off
cd datingapp-spa

echo BUILDING SPA IN PRODUCTION MODE
ng build --prod --build-optimizer=false
cd ..
cd DatingApp.api

echo STARTING WEBSITE
dotnet run
pause