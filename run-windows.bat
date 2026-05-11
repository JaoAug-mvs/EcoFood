@echo off
setlocal EnableExtensions

REM Pastas de saída: alinhar com EcoFood.csproj (net10.0-windows10.0.19041.0).
REM Importante: usar %~dp0 (pasta deste .bat), NAO %cd% — no Cursor/depurador o diretório
REM corrente pode nao ser a raiz do projecto e o .exe "não aparece".

set "TFM=net10.0-windows10.0.19041.0"
set "BASE=%~dp0"

REM WinUI/MAUI: abrir o .exe com START evita crash com pipes do terminal integrado.
echo [EcoFood] Compilando alvo Windows (%TFM%)...
dotnet build "%BASE%EcoFood.csproj" -f %TFM% -v minimal
if errorlevel 1 exit /b 1

call :TryLaunch "%BASE%bin\Debug\%TFM%"
if not errorlevel 1 exit /b 0
call :TryLaunch "%BASE%bin\Release\%TFM%"
if not errorlevel 1 exit /b 0

echo [EcoFood] ERRO: EcoFood.exe nao encontrado em:
echo   "%BASE%bin\Debug\%TFM%" nem Release. Compile primeiro ^(Debug ou Release^).
exit /b 1

:TryLaunch
set "ROOT=%~1"
if "%ROOT:~-1%"=="\" set "ROOT=%ROOT:~0,-1%"

if exist "%ROOT%\win-x64\EcoFood.exe" (
  echo [EcoFood] A abrir janela: "%ROOT%\win-x64\EcoFood.exe"
  start "" /D "%ROOT%\win-x64" "%ROOT%\win-x64\EcoFood.exe"
  goto :hint_ok
)
if exist "%ROOT%\win-arm64\EcoFood.exe" (
  echo [EcoFood] A abrir janela: "%ROOT%\win-arm64\EcoFood.exe"
  start "" /D "%ROOT%\win-arm64" "%ROOT%\win-arm64\EcoFood.exe"
  goto :hint_ok
)
for /d %%d in ("%ROOT%\win-*") do (
  if exist "%%d\EcoFood.exe" (
    echo [EcoFood] A abrir janela: "%%d\EcoFood.exe"
    start "" /D "%%d" "%%d\EcoFood.exe"
    goto :hint_ok
  )
)
exit /b 1

:hint_ok
echo.
echo *** Se no Gestor de tarefas so aparece ".NET Host" sem janela: NAO uses so "dotnet run". ***
echo *** Usa este BAT ou EcoFood.exe em win-x64. Na 1.^ª vez pode demorar ^(Defender^). ***
exit /b 0
