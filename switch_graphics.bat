@echo off
setlocal
echo ========================================================
echo   CityScape 1.8 3D Graphics Backend Switcher
echo ========================================================

if "%~1"=="" goto usage
if /I "%~1"=="vulkan" goto do_vulkan
if /I "%~1"=="directx" goto do_dx
if /I "%~1"=="dx12" goto do_dx
if /I "%~1"=="native" goto do_native
if /I "%~1"=="dx9" goto do_native
goto usage

:do_vulkan
echo [SWITCH] Activating Khronos Vulkan 1.3 backend [DXVK]...
if exist "%~dp0lib\d3d9_dxvk.dll" (
    copy /Y "%~dp0lib\d3d9_dxvk.dll" "%~dp0d3d9.dll" >nul
    if exist "%~dp0bin\Release\net48" copy /Y "%~dp0lib\d3d9_dxvk.dll" "%~dp0bin\Release\net48\d3d9.dll" >nul
    echo [OK] Active backend is now VULKAN 1.3 [DXVK].
    exit /b 0
) else (
    echo [ERROR] lib\d3d9_dxvk.dll not found!
    exit /b 1
)

:do_dx
echo [SWITCH] Activating DirectX 11 / 12 backend [dgVoodoo]...
if exist "%~dp0lib\d3d9_dgvoodoo.dll" (
    copy /Y "%~dp0lib\d3d9_dgvoodoo.dll" "%~dp0d3d9.dll" >nul
    if exist "%~dp0bin\Release\net48" copy /Y "%~dp0lib\d3d9_dgvoodoo.dll" "%~dp0bin\Release\net48\d3d9.dll" >nul
    echo [OK] Active backend is now DIRECTX 11 / 12 [dgVoodoo].
    exit /b 0
) else (
    echo [ERROR] lib\d3d9_dgvoodoo.dll not found!
    exit /b 1
)

:do_native
echo [SWITCH] Activating Native Direct3D 9 backend...
if exist "%~dp0d3d9.dll" del /F "%~dp0d3d9.dll"
if exist "%~dp0bin\Release\net48\d3d9.dll" del /F "%~dp0bin\Release\net48\d3d9.dll"
echo [OK] Active backend is now NATIVE DIRECT3D 9.
exit /b 0

:usage
echo Usage:
echo   switch_graphics.bat vulkan     - Use Khronos Vulkan 1.3 via DXVK
echo   switch_graphics.bat directx    - Use DirectX 11 / 12 via dgVoodoo
echo   switch_graphics.bat native     - Use Native Windows Direct3D 9
echo.
exit /b 0
