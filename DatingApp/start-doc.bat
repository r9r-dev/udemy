@echo off
cd datingapp-spa
start "" "http://localhost:8080"
compodoc -p src/tsconfig.app.json -s
