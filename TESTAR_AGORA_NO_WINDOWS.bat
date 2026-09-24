@echo off
title Auto Pecas Concorrente ERP - Build de Teste
cd /d "%~dp0"
where dotnet >nul 2>nul
if errorlevel 1 (
 echo.
 echo ERRO: .NET 8 SDK nao encontrado.
 echo Instale o .NET 8 SDK e execute este arquivo novamente.
 pause
 exit /b 1
)
powershell -NoProfile -ExecutionPolicy Bypass -File ".\build\preflight.ps1"
if errorlevel 1 goto erro
powershell -NoProfile -ExecutionPolicy Bypass -File ".\build\publish-win-x64.ps1"
if errorlevel 1 goto erro
echo.
echo BUILD CONCLUIDO.
echo Procure o executavel em: publish\win-x64\AutoPecasConcorrenteERP.exe
if exist ".\publish\win-x64\AutoPecasConcorrenteERP.exe" start "" ".\publish\win-x64\AutoPecasConcorrenteERP.exe"
pause
exit /b 0
:erro
echo.
echo O compilador encontrou erro. Copie a tela/saida e envie para correcao.
pause
exit /b 1
