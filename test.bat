@echo off
setlocal
echo ========================================
echo   CityScape Automated Test Suite Runner
echo ========================================

set PYTHON_CMD=
if exist "%~dp0.venv\Scripts\python.exe" (
    set "PYTHON_CMD=%~dp0.venv\Scripts\python.exe"
) else (
    where py >nul 2>nul
    if %ERRORLEVEL% EQU 0 (
        set "PYTHON_CMD=py"
    ) else (
        where python >nul 2>nul
        if %ERRORLEVEL% EQU 0 (
            set "PYTHON_CMD=python"
        )
    )
)

if "%PYTHON_CMD%"=="" (
    echo [ERROR] No Python interpreter found in .venv, py, or system PATH!
    exit /b 1
)

echo Using Python: %PYTHON_CMD%
"%PYTHON_CMD%" "%~dp0tests\test_suite.py" %*
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Test suite failed with code %ERRORLEVEL%
    exit /b %ERRORLEVEL%
)

echo [SUCCESS] All test suites completed successfully.
exit /b 0
