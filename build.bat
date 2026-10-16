@echo off
echo ========================================
echo   CityScape Open Source Build Engine
echo ========================================

powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0build.ps1" %*
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Build failed with code %ERRORLEVEL%
    exit /b %ERRORLEVEL%
)

echo [SUCCESS] Build finished.
exit /b 0
