@echo off
setlocal
cd /d "%~dp0"

echo ==========================================================
echo   CityScape 1.8 Distribution Packager
echo ==========================================================

powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0tools\package.ps1" %*
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Packaging failed with exit code %ERRORLEVEL%.
    exit /b %ERRORLEVEL%
)

echo [SUCCESS] Distribution packaging completed.
exit /b 0
