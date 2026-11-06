@echo off
setlocal enabledelayedexpansion
title CityScape 1.8 Graphics Backend Switcher

set "PARAM=%~1"

:: Resolve source DLL paths (works in both dev root and portable zip packages)
set "DXVK_SRC="
if exist "%~dp0lib\d3d9_dxvk.dll" set "DXVK_SRC=%~dp0lib\d3d9_dxvk.dll"
if not defined DXVK_SRC if exist "%~dp0d3d9_dxvk.dll" set "DXVK_SRC=%~dp0d3d9_dxvk.dll"

set "DGVOODOO_SRC="
if exist "%~dp0lib\d3d9_dgvoodoo.dll" set "DGVOODOO_SRC=%~dp0lib\d3d9_dgvoodoo.dll"
if not defined DGVOODOO_SRC if exist "%~dp0d3d9_dgvoodoo.dll" set "DGVOODOO_SRC=%~dp0d3d9_dgvoodoo.dll"

if not "%PARAM%"=="" goto handle_param

:menu
cls
echo ========================================================
echo   CityScape 1.8 - 3D Graphics Backend Switcher
echo ========================================================
echo.
echo   [1] Khronos Vulkan 1.3 [DXVK on Modern GPU] - VARSAYILAN / EN YUKSEK FPS
echo   [2] DirectX 11 / 12 [dgVoodoo2 Safe Fallback] - VM / Eski Kartlar
echo   [3] Orijinal Windows Direct3D 9 [Native Legacy]
echo.
set /p "CHOICE=Seciminiz [1/2/3, Varsayilan: 1]: "
if "%CHOICE%"=="" set "CHOICE=1"
if "%CHOICE%"=="1" goto do_vulkan
if "%CHOICE%"=="2" goto do_dx
if "%CHOICE%"=="3" goto do_native
goto menu

:handle_param
if /I "%PARAM%"=="vulkan" goto do_vulkan
if /I "%PARAM%"=="directx" goto do_dx
if /I "%PARAM%"=="dx12" goto do_dx
if /I "%PARAM%"=="native" goto do_native
if /I "%PARAM%"=="dx9" goto do_native
goto menu

:do_vulkan
echo.
echo [SWITCH] Khronos Vulkan 1.3 [DXVK] aktif ediliyor...
if defined DXVK_SRC (
    copy /Y "!DXVK_SRC!" "%~dp0d3d9.dll" >nul
    if exist "%~dp0bin\Release\net48" copy /Y "!DXVK_SRC!" "%~dp0bin\Release\net48\d3d9.dll" >nul
    echo [BASARILI] Aktif 3B Motor: VULKAN 1.3 [DXVK]
) else (
    echo [HATA] d3d9_dxvk.dll bulunamadi!
)
goto end

:do_dx
echo.
echo [SWITCH] DirectX 11 / 12 [dgVoodoo2] aktif ediliyor...
if defined DGVOODOO_SRC (
    copy /Y "!DGVOODOO_SRC!" "%~dp0d3d9.dll" >nul
    if exist "%~dp0bin\Release\net48" copy /Y "!DGVOODOO_SRC!" "%~dp0bin\Release\net48\d3d9.dll" >nul
    echo [BASARILI] Aktif 3B Motor: DIRECTX 11 / 12 [dgVoodoo2]
) else (
    echo [HATA] d3d9_dgvoodoo.dll bulunamadi!
)
goto end

:do_native
echo.
echo [SWITCH] Orijinal Windows Direct3D 9 aktif ediliyor...
if exist "%~dp0d3d9.dll" del /F "%~dp0d3d9.dll"
if exist "%~dp0bin\Release\net48\d3d9.dll" del /F "%~dp0bin\Release\net48\d3d9.dll"
echo [BASARILI] Aktif 3B Motor: YEREL DIRECT3D 9
goto end

:end
echo.
echo CityScape.exe secilen grafik motoruyla baslatilmaya hazir.
echo.
if "%PARAM%"=="" pause
exit /b 0
