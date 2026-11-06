@echo off
setlocal
cd /d "%~dp0"
title CityScape 1.8 Open Source Edition

echo =======================================================================
echo   PixelActive CityScape 1.8 - Procedural 3D Urban Simulation Engine
echo =======================================================================
echo.

:: Detect executable location (staged build or standalone root)
set "EXE_PATH="
if exist "%~dp0CityScape.exe" set "EXE_PATH=%~dp0CityScape.exe"
if not defined EXE_PATH if exist "%~dp0bin\Release\net48\CityScape.exe" set "EXE_PATH=%~dp0bin\Release\net48\CityScape.exe"

if not defined EXE_PATH (
    echo [HATA / ERROR] CityScape.exe bulunamadi!
    echo Lutfen once 'build.bat' veya 'powershell -File build.ps1' calistirarak derleyin.
    echo Please compile first by running 'build.bat' or use the pre-built portable zip.
    echo.
    pause
    exit /b 1
)

:: Graphics driver guidance
echo [INFO] Varsayilan Grafik API: Khronos Vulkan 1.3 / DirectX 9 (DXVK)
echo [NOTE] Eger ekran siyah kalir veya acilmazsa, 'switch_graphics.bat' calistirip
echo        [2] DirectX 11/12 (dgVoodoo2) veya [3] Native modunu seciniz.
echo.
echo CityScape baslatiliyor...
echo.

start "" "%EXE_PATH%" %*
exit /b 0
