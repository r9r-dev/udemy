@echo off
cd datingapp-spa
ng build --prod --build-optimizer=false
cd ..
cd DatingApp.api
dotnet run