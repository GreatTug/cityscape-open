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
    "src\BugReporter\BugReporter.csproj",
    "src\CityScape\CityScape.csproj"
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

# Stage distribution to bin/Release/net48
$binDist = "bin\Release\net48"
New-Item -ItemType Directory -Force -Path $binDist | Out-Null
Write-Host "`n---> Staging distribution to $binDist..." -ForegroundColor Yellow

# Copy third-party runtime dependencies from lib/
if (Test-Path "lib") {
    Copy-Item "lib\*" -Destination $binDist -Force
}

# Copy newly built assemblies and debug symbols from each project
foreach ($proj in $projects) {
    $projDir = Split-Path $proj
    $projBin = Join-Path $projDir "bin\Release\net48"
    if (Test-Path $projBin) {
        Copy-Item "$projBin\*" -Destination $binDist -Force
    }
}

# Copy main application configuration
if (Test-Path "CityScape.exe.config") {
    Copy-Item "CityScape.exe.config" -Destination "$binDist\CityScape.exe.config" -Force
}

Write-Host "[OK] Staged all distribution binaries to $binDist" -ForegroundColor Green

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "  All CityScape assemblies built successfully!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
