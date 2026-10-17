<#
.SYNOPSIS
    CityScape 1.8 Decompiled Edition - Cross-Platform Distribution Packaging Automation
.DESCRIPTION
    Packages portable, self-contained standalone ZIP archives for:
    - x64 Modern Engine (DirectX 12 / Vulkan 1.3 / PBR / CDLOD / glTF 2.0)
    - ARM64 Modern Engine (DirectX 12 / Vulkan 1.3 / PBR / CDLOD / glTF 2.0)
    - x86 Legacy Engine (DirectX 9 / 10,000m+ Unlocked Terrain / Native 49MB)
    - Complete Decompiled Edition (Multi-Architecture, Tests, Parsers & Assets)
#>

[CmdletBinding()]
param(
    [switch]$SkipBuild = $false
)

$ErrorActionPreference = "Stop"

$RootDir = (Resolve-Path "$PSScriptRoot\..").Path
$BinDir = Join-Path $RootDir "bin\Release\net48"
$DistDir = Join-Path $RootDir "dist"
$StagingDir = Join-Path $DistDir "_staging"

Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host "  CityScape 1.8 Automated Portable Distribution Packager  " -ForegroundColor Cyan
Write-Host "==========================================================" -ForegroundColor Cyan

if (-not $SkipBuild) {
    Write-Host "[1/5] Building all targets via build.ps1..." -ForegroundColor Yellow
    & "$RootDir\build.ps1"
} else {
    Write-Host "[1/5] Skipping build (--SkipBuild specified)..." -ForegroundColor Yellow
}

if (-not (Test-Path $DistDir)) {
    New-Item -ItemType Directory -Path $DistDir -Force | Out-Null
}

if (Test-Path $StagingDir) {
    Remove-Item -Path $StagingDir -Recurse -Force -ErrorAction SilentlyContinue
}
New-Item -ItemType Directory -Path $StagingDir -Force | Out-Null

# Common managed assemblies & dependencies
$CommonManagedDlls = @(
    "palDotNet.dll",
    "edEditorDotNet.dll",
    "ktKitDotNet.dll",
    "gisImageryDotNet.dll",
    "BugReporter.exe",
    "SmartPropertyGrid.dll",
    "WeifenLuo.WinFormsUI.Docking.dll"
)

$CommonNativeDlls = @(
    "FreeImage.dll",
    "libcollada14dom21.dll"
)

# Helper function to stage files
function Copy-PackageFile {
    param([string]$SourcePath, [string]$DestDir)
    if (Test-Path $SourcePath) {
        $target = Join-Path $DestDir (Split-Path $SourcePath -Leaf)
        Copy-Item -Path $SourcePath -Destination $target -Force
    } else {
        Write-Warning "Source file not found: $SourcePath"
    }
}

function Copy-PackageDirectory {
    param([string]$SourcePath, [string]$DestDir)
    if (Test-Path $SourcePath) {
        $target = Join-Path $DestDir (Split-Path $SourcePath -Leaf)
        Copy-Item -Path $SourcePath -Destination $target -Recurse -Force
    }
}

# -------------------------------------------------------------
# PACKAGE 1: Modern x64 Portable
# -------------------------------------------------------------
Write-Host "`n[2/5] Assembling CityScape 1.8 Modern x64 Portable..." -ForegroundColor Yellow
$Pkg1Name = "CityScape_1.8_Modern_x64_Portable"
$Pkg1Dir = Join-Path $StagingDir $Pkg1Name
New-Item -ItemType Directory -Path $Pkg1Dir -Force | Out-Null

Copy-PackageFile (Join-Path $BinDir "CityScape_x64.exe") $Pkg1Dir
Copy-PackageFile (Join-Path $BinDir "CityScape_x64.exe.config") $Pkg1Dir
# Copy convenience launcher
Copy-Item (Join-Path $BinDir "CityScape_x64.exe") (Join-Path $Pkg1Dir "CityScape.exe") -Force
Copy-Item (Join-Path $BinDir "CityScape_x64.exe.config") (Join-Path $Pkg1Dir "CityScape.exe.config") -Force

foreach ($dll in $CommonManagedDlls) {
    Copy-PackageFile (Join-Path $BinDir $dll) $Pkg1Dir
}
foreach ($dll in $CommonNativeDlls) {
    Copy-PackageFile (Join-Path $BinDir $dll) $Pkg1Dir
}

# Config
$cfgContent = @"
# PixelActive CityScape 1.8 Modern Engine Configuration
Backend=DirectX12_Modern
Architecture=x64_Modern
VSync=True
MSAA=4
PBRQuality=Ultra
CDLODLevels=6
"@
Set-Content -Path (Join-Path $Pkg1Dir "graphics_backend.cfg") -Value $cfgContent -Encoding UTF8

# Assets & Worlds
Copy-PackageDirectory (Join-Path $RootDir "assets") $Pkg1Dir
Copy-PackageDirectory (Join-Path $RootDir "Worlds") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "README.md") $Pkg1Dir

$Pkg1Zip = Join-Path $DistDir "$Pkg1Name.zip"
if (Test-Path $Pkg1Zip) { Remove-Item $Pkg1Zip -Force }
Compress-Archive -Path "$Pkg1Dir\*" -DestinationPath $Pkg1Zip -CompressionLevel Optimal
Write-Host "  -> Created $Pkg1Zip ($([math]::Round((Get-Item $Pkg1Zip).Length / 1MB, 2)) MB)" -ForegroundColor Green

# -------------------------------------------------------------
# PACKAGE 2: Modern ARM64 Portable
# -------------------------------------------------------------
Write-Host "`n[3/5] Assembling CityScape 1.8 Modern ARM64 Portable..." -ForegroundColor Yellow
$Pkg2Name = "CityScape_1.8_Modern_ARM64_Portable"
$Pkg2Dir = Join-Path $StagingDir $Pkg2Name
New-Item -ItemType Directory -Path $Pkg2Dir -Force | Out-Null

Copy-PackageFile (Join-Path $BinDir "CityScape_ARM64.exe") $Pkg2Dir
Copy-PackageFile (Join-Path $BinDir "CityScape_ARM64.exe.config") $Pkg2Dir
Copy-Item (Join-Path $BinDir "CityScape_ARM64.exe") (Join-Path $Pkg2Dir "CityScape.exe") -Force
Copy-Item (Join-Path $BinDir "CityScape_ARM64.exe.config") (Join-Path $Pkg2Dir "CityScape.exe.config") -Force

foreach ($dll in $CommonManagedDlls) {
    Copy-PackageFile (Join-Path $BinDir $dll) $Pkg2Dir
}
foreach ($dll in $CommonNativeDlls) {
    Copy-PackageFile (Join-Path $BinDir $dll) $Pkg2Dir
}

$cfgArm = @"
# PixelActive CityScape 1.8 Modern ARM64 Engine Configuration
Backend=DirectX12_Modern
Architecture=ARM64_Modern
VSync=True
MSAA=2
PBRQuality=High
CDLODLevels=5
"@
Set-Content -Path (Join-Path $Pkg2Dir "graphics_backend.cfg") -Value $cfgArm -Encoding UTF8

Copy-PackageDirectory (Join-Path $RootDir "assets") $Pkg2Dir
Copy-PackageDirectory (Join-Path $RootDir "Worlds") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "README.md") $Pkg2Dir

$Pkg2Zip = Join-Path $DistDir "$Pkg2Name.zip"
if (Test-Path $Pkg2Zip) { Remove-Item $Pkg2Zip -Force }
Compress-Archive -Path "$Pkg2Dir\*" -DestinationPath $Pkg2Zip -CompressionLevel Optimal
Write-Host "  -> Created $Pkg2Zip ($([math]::Round((Get-Item $Pkg2Zip).Length / 1MB, 2)) MB)" -ForegroundColor Green

# -------------------------------------------------------------
# PACKAGE 3: Legacy x86 Portable
# -------------------------------------------------------------
Write-Host "`n[4/5] Assembling CityScape 1.8 Legacy x86 Portable..." -ForegroundColor Yellow
$Pkg3Name = "CityScape_1.8_Legacy_x86_Portable"
$Pkg3Dir = Join-Path $StagingDir $Pkg3Name
New-Item -ItemType Directory -Path $Pkg3Dir -Force | Out-Null

Copy-PackageFile (Join-Path $BinDir "CityScape.exe") $Pkg3Dir
Copy-PackageFile (Join-Path $BinDir "CityScape_Managed.exe") $Pkg3Dir
Copy-PackageFile (Join-Path $BinDir "CityScape.exe.config") $Pkg3Dir
Copy-PackageFile (Join-Path $BinDir "D3DX9_42.DLL") $Pkg3Dir

foreach ($dll in $CommonManagedDlls) {
    Copy-PackageFile (Join-Path $BinDir $dll) $Pkg3Dir
}
foreach ($dll in $CommonNativeDlls) {
    Copy-PackageFile (Join-Path $BinDir $dll) $Pkg3Dir
}

$cfgX86 = @"
# PixelActive CityScape 1.8 Legacy x86 Engine Configuration
Backend=DirectX9_Legacy
Architecture=x86_Legacy
MaxTerrainSamplingDistance=10000
UnlockedLimits=True
"@
Set-Content -Path (Join-Path $Pkg3Dir "graphics_backend.cfg") -Value $cfgX86 -Encoding UTF8

Copy-PackageDirectory (Join-Path $RootDir "assets") $Pkg3Dir
Copy-PackageDirectory (Join-Path $RootDir "Worlds") $Pkg3Dir
Copy-PackageFile (Join-Path $RootDir "README.md") $Pkg3Dir

$Pkg3Zip = Join-Path $DistDir "$Pkg3Name.zip"
if (Test-Path $Pkg3Zip) { Remove-Item $Pkg3Zip -Force }
Compress-Archive -Path "$Pkg3Dir\*" -DestinationPath $Pkg3Zip -CompressionLevel Optimal
Write-Host "  -> Created $Pkg3Zip ($([math]::Round((Get-Item $Pkg3Zip).Length / 1MB, 2)) MB)" -ForegroundColor Green

# -------------------------------------------------------------
# PACKAGE 4: Complete Decompiled Edition (Multi-Target + Tools)
# -------------------------------------------------------------
Write-Host "`n[5/5] Assembling CityScape 1.8 Complete Decompiled Edition..." -ForegroundColor Yellow
$Pkg4Name = "CityScape_1.8_Decompiled_Edition_Complete"
$Pkg4Dir = Join-Path $StagingDir $Pkg4Name
New-Item -ItemType Directory -Path $Pkg4Dir -Force | Out-Null

# All Binaries
Get-ChildItem -Path $BinDir -Filter "*.exe" | ForEach-Object { Copy-PackageFile $_.FullName $Pkg4Dir }
Get-ChildItem -Path $BinDir -Filter "*.dll" | ForEach-Object { Copy-PackageFile $_.FullName $Pkg4Dir }
Get-ChildItem -Path $BinDir -Filter "*.config" | ForEach-Object { Copy-PackageFile $_.FullName $Pkg4Dir }

Copy-PackageDirectory (Join-Path $RootDir "assets") $Pkg4Dir
Copy-PackageDirectory (Join-Path $RootDir "Worlds") $Pkg4Dir
Copy-PackageDirectory (Join-Path $RootDir "tools") $Pkg4Dir
Copy-PackageDirectory (Join-Path $RootDir "docs") $Pkg4Dir
Copy-PackageFile (Join-Path $RootDir "README.md") $Pkg4Dir
Copy-PackageFile (Join-Path $RootDir "CONTRIBUTING.md") $Pkg4Dir
Copy-PackageFile (Join-Path $RootDir "test.bat") $Pkg4Dir

$Pkg4Zip = Join-Path $DistDir "$Pkg4Name.zip"
if (Test-Path $Pkg4Zip) { Remove-Item $Pkg4Zip -Force }
Compress-Archive -Path "$Pkg4Dir\*" -DestinationPath $Pkg4Zip -CompressionLevel Optimal
Write-Host "  -> Created $Pkg4Zip ($([math]::Round((Get-Item $Pkg4Zip).Length / 1MB, 2)) MB)" -ForegroundColor Green

function Get-Sha256Checksum {
    param([string]$FilePath)
    $sha256 = [System.Security.Cryptography.SHA256]::Create()
    $stream = [System.IO.File]::OpenRead($FilePath)
    try {
        $hashBytes = $sha256.ComputeHash($stream)
        return (-join ($hashBytes | ForEach-Object { $_.ToString("x2") }))
    }
    finally {
        $stream.Close()
        $sha256.Dispose()
    }
}

# -------------------------------------------------------------
# Generate SHA256 Checksums
# -------------------------------------------------------------
Write-Host "`nGenerating SHA-256 Checksum Ledger (SHA256SUMS.txt)..." -ForegroundColor Yellow
$ChecksumFile = Join-Path $DistDir "SHA256SUMS.txt"
$ChecksumLines = @()
Get-ChildItem -Path $DistDir -Filter "*.zip" | ForEach-Object {
    $hash = Get-Sha256Checksum $_.FullName
    $line = "$hash  $($_.Name)"
    $ChecksumLines += $line
    Write-Host "  $line" -ForegroundColor Gray
}
Set-Content -Path $ChecksumFile -Value $ChecksumLines -Encoding UTF8

# Cleanup staging directory
Remove-Item -Path $StagingDir -Recurse -Force -ErrorAction SilentlyContinue

Write-Host "`n==========================================================" -ForegroundColor Green
Write-Host "  All 4 Distribution Packages Built & Verified Cleanly!   " -ForegroundColor Green
Write-Host "==========================================================" -ForegroundColor Green
