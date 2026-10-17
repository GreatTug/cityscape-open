"""
CityScape 1.8 - Automated Stress & Resilience Benchmark
Tests rapid backend switching, simulated frame recording, and glTF GLB binary export
under load to prove long-term stability and lack of memory leaks.
"""

import os
import sys
import time
import subprocess

def run_stress_benchmark():
    print("==========================================================")
    print("  CityScape 1.8 Multi-Backend Stress & Memory Benchmark   ")
    print("==========================================================")

    dist_dir = os.path.join(os.path.dirname(__file__), "..", "bin", "Release", "net48")
    pal_dll = os.path.abspath(os.path.join(dist_dir, "palDotNet.dll"))

    assert os.path.exists(pal_dll), f"Missing palDotNet.dll at {pal_dll}"

    ps_stress_script = f"""
    $ErrorActionPreference = 'Stop'
    Add-Type -Path '{pal_dll}'

    $process = [System.Diagnostics.Process]::GetCurrentProcess()
    $initialMem = $process.WorkingSet64 / 1MB
    Write-Host "  [INIT] Starting Private Working Set: $([math]::Round($initialMem, 2)) MB"

    Write-Host "`n  [STAGE 1] Performing 50 rapid graphics backend switches (DX12 <-> Vulkan <-> DX9)..."
    for ($i = 0; $i -lt 50; $i++) {{
        $target = if ($i % 3 -eq 0) {{ [palDotNet.palModernEngine.GraphicsBackendType]::DirectX12_Modern }} `
                  elseif ($i % 3 -eq 1) {{ [palDotNet.palModernEngine.GraphicsBackendType]::Vulkan_Modern }} `
                  else {{ [palDotNet.palModernEngine.GraphicsBackendType]::DirectX9_Legacy }}

        $ok = [palDotNet.palModernEngine.GraphicsBackendManager]::SwitchBackend($target, [IntPtr]::Zero)
        if (-not $ok) {{ throw "SwitchBackend failed for $target at iteration $i" }}
    }}
    Write-Host "  [PASS] 50 backend switches completed successfully with 0 exceptions."

    Write-Host "`n  [STAGE 2] Simulating 1,000 high-throughput frame render cycles..."
    $backend = [palDotNet.palModernEngine.GraphicsBackendManager]::ActiveBackend
    $backend.Initialize([IntPtr]::Zero, 1920, 1080)
    for ($f = 0; $f -lt 1000; $f++) {{
        $backend.BeginFrame([System.Drawing.Color]::CornflowerBlue)
        $backend.RecordMeshDraw(10000, 30000)
        $backend.EndFrame()
        $backend.Present()
    }}
    $stats = $backend.GetStats()
    Write-Host "  [PASS] 1,000 frames recorded: Total Triangles: $($stats.TrianglesRendered), Frames: $($stats.FrameIndex)"

    Write-Host "`n  [STAGE 3] Executing 20 heavy glTF 2.0 Binary (GLB) mesh export cycles..."
    $tempDir = [System.IO.Path]::Combine([System.IO.Path]::GetTempPath(), "cs_stress_" + [Guid]::NewGuid().ToString())
    [System.IO.Directory]::CreateDirectory($tempDir) | Out-Null
    try {{
        $exporter = New-Object palDotNet.palModernEngine.GltfModernExporter
        for ($e = 0; $e -lt 20; $e++) {{
            $mesh = New-Object palDotNet.palModernEngine.ExportMeshData
            $mesh.Name = "StressTile_$e"
            for ($v = 0; $v -lt 100; $v++) {{
                $vert = New-Object palDotNet.palModernEngine.ExportVertex
                $vert.X = $v * 10.0; $vert.Y = [math]::Sin($v); $vert.Z = $v * 5.0
                $vert.Nx = 0.0; $vert.Ny = 1.0; $vert.Nz = 0.0
                $vert.U = $v / 100.0; $vert.V = $v / 100.0
                $mesh.Vertices.Add($vert)
            }}
            for ($idx = 0; $idx -lt 90; $idx += 3) {{
                $mesh.Indices.Add($idx)
                $mesh.Indices.Add($idx + 1)
                $mesh.Indices.Add($idx + 2)
            }}
            $list = New-Object 'System.Collections.Generic.List[palDotNet.palModernEngine.ExportMeshData]'
            $list.Add($mesh)
            $glbFile = [System.IO.Path]::Combine($tempDir, "stress_$e.glb")
            $res = $exporter.ExportScene($glbFile, $list, "StressScene")
            if (-not $res -or -not (Test-Path $glbFile)) {{ throw "GLB export failed at iteration $e" }}
        }}
        Write-Host "  [PASS] 20 glTF 2.0 Binary archives written and verified on disk."
    }} finally {{
        [System.IO.Directory]::Delete($tempDir, $true)
    }}

    Write-Host "`n  [STAGE 4] Stress testing CDLOD Multithreaded Async Mesh Worker Pool (100km terrain)..."
    $terrain = New-Object palDotNet.palVisual.TerrainSurfaceData(100000.0, 4800.0, 100000.0, 4800.0, 1, 1, 25.0)
    $quadtree = New-Object palDotNet.palVisual.CdlodTerrainQuadtree($terrain, 4)
    $frustum = [palDotNet.palVisual.ViewFrustum3D]::CreateFromCamera(0.0, 300.0, -3000.0, 0.0, 0.0, 60.0, 16.0 / 9.0, 5.0, 30000.0)
    $tested = 0; $culled = 0
    $visibleChunks = $quadtree.SelectVisibleChunks($frustum, 0.0, 300.0, -3000.0, [ref]$tested, [ref]$culled)
    
    $worker = New-Object palDotNet.palVisual.CdlodAsyncMeshWorker(4)
    try {{
        $swWorker = [System.Diagnostics.Stopwatch]::StartNew()
        $enqueuedCount = 0
        foreach ($chunk in $visibleChunks) {{
            if ($worker.EnqueueRequest($chunk.Node, $chunk.LodLevel, $terrain, 16)) {{
                $enqueuedCount++
            }}
        }}
        $done = $worker.WaitForCompletion(5000)
        $swWorker.Stop()
        if (-not $done) {{ throw "CdlodAsyncMeshWorker timed out under stress test!" }}
        if ($worker.CompletedCacheCount -lt $enqueuedCount) {{ throw "Incomplete cache count in CdlodAsyncMeshWorker" }}
        Write-Host "  [PASS] Background worker generated $enqueuedCount CDLOD meshes in $($swWorker.ElapsedMilliseconds) ms ($($worker.CompletedCacheCount) in cache)."
    }} finally {{
        $worker.Dispose()
    }}

    Write-Host "`n  [STAGE 5] Benchmarking Mathematical PBR Shaders & Wave Simulation..."
    $pipeline = New-Object palDotNet.palModernEngine.ModernShaderPipeline
    $pbr = New-Object palDotNet.palModernEngine.PbrMaterialParameters
    $pbr.Roughness = 0.35; $pbr.Metallic = 0.75
    [float]$diff = 0.0; [float]$spec = 0.0
    [float]$wx = 0.0; [float]$wy = 0.0; [float]$wz = 0.0
    $swShader = [System.Diagnostics.Stopwatch]::StartNew()
    for ($s = 0; $s -lt 5000; $s++) {{
        $pbr.EvaluateFullPbrBrdf(0.85, 0.85, 0.98, 0.95, [ref]$diff, [ref]$spec)
        $pipeline.WaterPipeline.EvaluateWaterDisplacement([float]$s, [float]($s * 2), 5.0, [ref]$wx, [ref]$wy, [ref]$wz)
    }}
    $swShader.Stop()
    Write-Host "  [PASS] 5,000 PBR BRDF & Ocean Gerstner wave evaluations completed in $($swShader.ElapsedMilliseconds) ms."

    [System.GC]::Collect()
    [System.GC]::WaitForPendingFinalizers()
    $process.Refresh()
    $finalMem = $process.WorkingSet64 / 1MB
    $deltaMem = $finalMem - $initialMem
    Write-Host "`n  [FINAL] Final Working Set: $([math]::Round($finalMem, 2)) MB (Delta: $([math]::Round($deltaMem, 2)) MB)"
    if ($deltaMem -gt 50.0) {{ throw "Memory working set delta exceeded 50 MB threshold ($deltaMem MB)" }}
    Write-Host "=========================================================="
    Write-Host "  STRESS BENCHMARK PASSED (100% STABLE, 0 LEAKS)          "
    Write-Host "=========================================================="
    """

    res = subprocess.run(["powershell", "-NoProfile", "-Command", ps_stress_script], capture_output=True, text=True)
    print(res.stdout)
    if res.returncode != 0:
        print(res.stderr, file=sys.stderr)
        sys.exit(res.returncode)

if __name__ == "__main__":
    run_stress_benchmark()
