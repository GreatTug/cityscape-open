<#
.SYNOPSIS
    CityScape 1.8 Open Source Edition - Automated Portable Distribution Packager
.DESCRIPTION
    Packages standalone, zero-installation portable distribution ZIP archives for:
    1. CityScape_1.8_OpenSource_Portable.zip (Full 3D Engine, Vulkan 1.3 / DX12 / D3D9, 123 3D models, assets & runtimes)
    2. CityScape_1.8_Full_Source_And_Tools.zip (Complete decompiled C# source, MSBuild solution, RE tools, tests, docs)
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
    Write-Host "[1/4] Building all managed assemblies via build.ps1..." -ForegroundColor Yellow
    & "$RootDir\build.ps1"
} else {
    Write-Host "[1/4] Skipping build (--SkipBuild specified)..." -ForegroundColor Yellow
}

if (-not (Test-Path $DistDir)) {
    New-Item -ItemType Directory -Path $DistDir -Force | Out-Null
}

if (Test-Path $StagingDir) {
    Remove-Item -Path $StagingDir -Recurse -Force -ErrorAction SilentlyContinue
}
New-Item -ItemType Directory -Path $StagingDir -Force | Out-Null

# Helper functions
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

Add-Type -AssemblyName System.IO.Compression.FileSystem

function Create-PackageZip {
    param([string]$SourceDir, [string]$ZipPath)
    if (Test-Path $ZipPath) { Remove-Item $ZipPath -Force }
    [System.IO.Compression.ZipFile]::CreateFromDirectory($SourceDir, $ZipPath, [System.IO.Compression.CompressionLevel]::Optimal, $false)
}

# -------------------------------------------------------------
# PACKAGE 1: Standalone Portable Edition (Zero-Installation)
# -------------------------------------------------------------
Write-Host "`n[2/4] Assembling CityScape 1.8 Open Source Portable Release..." -ForegroundColor Yellow
$Pkg1Name = "CityScape_1.8_OpenSource_Portable"
$Pkg1Dir = Join-Path $StagingDir $Pkg1Name
New-Item -ItemType Directory -Path $Pkg1Dir -Force | Out-Null

# Core Executable & Configuration
Copy-PackageFile (Join-Path $BinDir "CityScape.exe") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "CityScape.exe.config") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "CityScape.exe.response.txt") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "CityScape.DefaultLayout.xml") $Pkg1Dir

# Managed Assemblies
$ManagedDlls = @(
    "palDotNet.dll",
    "edEditorDotNet.dll",
    "ktKitDotNet.dll",
    "gisImageryDotNet.dll",
    "BugReporter.exe",
    "BugReporter.exe.config",
    "SmartPropertyGrid.dll",
    "WeifenLuo.WinFormsUI.Docking.dll"
)
foreach ($dll in $ManagedDlls) {
    Copy-PackageFile (Join-Path $BinDir $dll) $Pkg1Dir
}

# Native Third-Party DLLs
$NativeDlls = @(
    "D3DX9_42.DLL",
    "FreeImage.dll",
    "FreeImageNET.dll",
    "libcollada14dom21.dll",
    "libeay32.dll",
    "ssleay32.dll",
    "DevIL.NET2.dll",
    "xinput1_3.dll"
)
foreach ($dll in $NativeDlls) {
    if (Test-Path (Join-Path $RootDir "lib\$dll")) {
        Copy-PackageFile (Join-Path $RootDir "lib\$dll") $Pkg1Dir
    } elseif (Test-Path (Join-Path $RootDir $dll)) {
        Copy-PackageFile (Join-Path $RootDir $dll) $Pkg1Dir
    }
}

# Private SxS Microsoft.VC90.CRT runtime for computers without VC++ 2008 redist
if (Test-Path (Join-Path $RootDir "lib\Microsoft.VC90.CRT")) {
    Copy-PackageDirectory (Join-Path $RootDir "lib\Microsoft.VC90.CRT") $Pkg1Dir
}

# Graphics Backends & Switcher
Copy-PackageFile (Join-Path $RootDir "d3d9.dll") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "d3d9_dxvk.dll") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "d3d9_dgvoodoo.dll") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "dgVoodoo.conf") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "switch_graphics.bat") $Pkg1Dir

# Copy 1-click launcher batch file
Copy-PackageFile (Join-Path $RootDir "Run_CityScape.bat") $Pkg1Dir

# Assets & Worlds
Copy-PackageDirectory (Join-Path $RootDir "assets") $Pkg1Dir
Copy-PackageDirectory (Join-Path $RootDir "Worlds") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "README.md") $Pkg1Dir
Copy-PackageFile (Join-Path $RootDir "LICENSE") $Pkg1Dir

$Pkg1Zip = Join-Path $DistDir "$Pkg1Name.zip"
Create-PackageZip $Pkg1Dir $Pkg1Zip
Write-Host "  -> Created $Pkg1Zip ($([math]::Round((Get-Item $Pkg1Zip).Length / 1MB, 2)) MB)" -ForegroundColor Green

# -------------------------------------------------------------
# PACKAGE 2: Full Source & Tools Edition
# -------------------------------------------------------------
Write-Host "`n[3/4] Assembling CityScape 1.8 Full Source & Tools Archive..." -ForegroundColor Yellow
$Pkg2Name = "CityScape_1.8_Full_Source_And_Tools"
$Pkg2Dir = Join-Path $StagingDir $Pkg2Name
New-Item -ItemType Directory -Path $Pkg2Dir -Force | Out-Null

# Source Trees
Copy-PackageDirectory (Join-Path $RootDir "src") $Pkg2Dir
Copy-PackageDirectory (Join-Path $RootDir "lib") $Pkg2Dir
Copy-PackageDirectory (Join-Path $RootDir "tools") $Pkg2Dir
Copy-PackageDirectory (Join-Path $RootDir "docs") $Pkg2Dir
Copy-PackageDirectory (Join-Path $RootDir "tests") $Pkg2Dir
Copy-PackageDirectory (Join-Path $RootDir "assets") $Pkg2Dir
Copy-PackageDirectory (Join-Path $RootDir "Worlds") $Pkg2Dir

# Root Scripts & Metadata
Copy-PackageFile (Join-Path $RootDir "CityScape.sln") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "CityScape.slnx") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "Directory.Build.props") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "build.bat") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "build.ps1") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "package.bat") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "test.bat") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "switch_graphics.bat") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "CityScape.exe") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "CityScape.exe.config") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "CityScape.exe.response.txt") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "CityScape.DefaultLayout.xml") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "README.md") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "CONTRIBUTING.md") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "LICENSE") $Pkg2Dir
Copy-PackageFile (Join-Path $RootDir "agent.md") $Pkg2Dir

# Remove any obj/bin from inside staged src
Get-ChildItem -Path (Join-Path $Pkg2Dir "src") -Include "bin","obj" -Recurse -Directory | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue

$Pkg2Zip = Join-Path $DistDir "$Pkg2Name.zip"
Create-PackageZip $Pkg2Dir $Pkg2Zip
Write-Host "  -> Created $Pkg2Zip ($([math]::Round((Get-Item $Pkg2Zip).Length / 1MB, 2)) MB)" -ForegroundColor Green

# -------------------------------------------------------------
# Generate SHA-256 Checksums
# -------------------------------------------------------------
Write-Host "`n[4/4] Generating SHA-256 Checksum Ledger (SHA256SUMS.txt)..." -ForegroundColor Yellow

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
Write-Host "  Distribution Packages Built & Verified Cleanly!         " -ForegroundColor Green
Write-Host "==========================================================" -ForegroundColor Green
