<#
.SYNOPSIS
    CityScape Open-Source Build Script
.DESCRIPTION
    Restores and builds all managed assemblies for CityScape using MSBuild.
#>

[CmdletBinding()]
param(
    [switch]$Clean,
    [switch]$Rebuild
)

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
    "src\CityScape\CityScape.csproj",
    "tools\test_subsystems\test_subsystems.csproj"
)

if ($Clean) {
    Write-Host "`n---> Cleaning all projects..." -ForegroundColor Yellow
    foreach ($proj in $projects) {
        & $msbuild $proj -t:Clean -p:Configuration=Release
    }
    if (Test-Path "bin\Release\net48") {
        Remove-Item "bin\Release\net48\*" -Recurse -Force -ErrorAction SilentlyContinue
    }
    Write-Host "[OK] Clean completed successfully!" -ForegroundColor Green
    if (-not $Rebuild) { exit 0 }
}

$buildTarget = if ($Rebuild) { "Rebuild" } else { "Build" }

foreach ($proj in $projects) {
    Write-Host "`n---> Building ($buildTarget) $proj..." -ForegroundColor Yellow
    & $msbuild -t:Restore $proj -p:Configuration=Release
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Restore failed for $proj"
        exit $LASTEXITCODE
    }

    & $msbuild $proj -t:$buildTarget -p:Configuration=Release
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build failed for $proj"
        exit $LASTEXITCODE
    }
    Write-Host "[OK] $proj compiled successfully!" -ForegroundColor Green

    if ($proj -eq "src\palDotNet\palDotNet.csproj") {
        if (Test-Path "src\palDotNet\bin\Release\net48\palDotNet.dll") {
            Copy-Item "src\palDotNet\bin\Release\net48\palDotNet.dll" -Destination "lib\palDotNet.dll" -Force
            Copy-Item "src\palDotNet\bin\Release\net48\palDotNet.pdb" -Destination "lib\palDotNet.pdb" -Force -ErrorAction SilentlyContinue
        }
    }
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

# Copy main application configuration and response file
if (Test-Path "CityScape.exe.config") {
    Copy-Item "CityScape.exe.config" -Destination "$binDist\CityScape.exe.config" -Force
    Copy-Item "CityScape.exe.config" -Destination "$binDist\CityScape_Managed.exe.config" -Force
    Copy-Item "CityScape.exe.config" -Destination "CityScape_Managed.exe.config" -Force
}
if (Test-Path "CityScape.exe.response.txt") {
    Copy-Item "CityScape.exe.response.txt" -Destination "$binDist\CityScape.exe.response.txt" -Force
    Copy-Item "CityScape.exe.response.txt" -Destination "$binDist\CityScape_Managed.exe.response.txt" -Force
    Copy-Item "CityScape.exe.response.txt" -Destination "CityScape_Managed.exe.response.txt" -Force
}

# Copy sample worlds and data directories to make binDist completely self-contained
$dataItems = @("assets", "icons", "Worlds", "1-grids", "2-grids", "docs", "cache", "1.cst", "2.cst", "pro.csp")
foreach ($item in $dataItems) {
    if (Test-Path $item) {
        Copy-Item -Path $item -Destination $binDist -Recurse -Force
    }
}

# Compile standalone lifecycle test tool
$csc = "${env:windir}\Microsoft.NET\Framework\v4.0.30319\csc.exe"
if (Test-Path $csc) {
    Write-Host "`n---> Compiling tools\test_lifecycle.cs..." -ForegroundColor Yellow
    & $csc /nologo /target:exe /out:"$binDist\test_lifecycle.exe" /reference:"$binDist\palDotNet.dll","$binDist\SmartPropertyGrid.dll","$binDist\WeifenLuo.WinFormsUI.Docking.dll" tools\test_lifecycle.cs
    if ($LASTEXITCODE -eq 0) {
        Write-Host "[OK] tools\test_lifecycle.cs compiled successfully!" -ForegroundColor Green
    }
}

# Mirror executables and DLLs to workspace root matching original CityScape 1.8 layout
Write-Host "`n---> Mirroring executables and libraries to root (matching original layout)..." -ForegroundColor Yellow
Get-ChildItem -Path $binDist -Filter "*.dll" | Copy-Item -Destination "." -Force
Get-ChildItem -Path $binDist -Filter "*.exe" | Copy-Item -Destination "." -Force
Get-ChildItem -Path $binDist -Filter "*.pdb" | Copy-Item -Destination "." -Force

Write-Host "[OK] Staged all distribution binaries to $binDist and workspace root" -ForegroundColor Green

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "  All CityScape assemblies built successfully!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
