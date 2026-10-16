<#
.SYNOPSIS
    CityScape Open-Source Build Script
.DESCRIPTION
    Restores and builds all managed assemblies for CityScape using MSBuild.
#>

$ErrorActionPreference = "Stop"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  CityScape Open Source Build Engine" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Find MSBuild
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
if (Test-Path $vswhere) {
    $msbuild = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | Select-Object -First 1
}

if (-not $msbuild -or -not (Test-Path $msbuild)) {
    Write-Error "MSBuild.exe could not be located via vswhere. Please ensure Visual Studio or Build Tools is installed."
    exit 1
}

Write-Host "Using MSBuild: $msbuild" -ForegroundColor Green

$projects = @(
    "src\palDotNet\palDotNet.csproj",
    "src\gisImageryDotNet\gisImageryDotNet.csproj",
    "src\ktKitDotNet\ktKitDotNet.csproj",
    "src\edEditorDotNet\edEditorDotNet.csproj",
    "src\BugReporter\BugReporter.csproj"
)

foreach ($proj in $projects) {
    Write-Host "`n---> Building $proj..." -ForegroundColor Yellow
    & $msbuild -t:Restore $proj -p:Configuration=Release
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Restore failed for $proj"
        exit $LASTEXITCODE
    }

    & $msbuild $proj -p:Configuration=Release
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build failed for $proj"
        exit $LASTEXITCODE
    }
    Write-Host "[OK] $proj compiled successfully!" -ForegroundColor Green
}

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "  All CityScape assemblies built successfully!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
