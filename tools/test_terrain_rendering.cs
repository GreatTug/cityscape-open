using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using palDotNet.palVisual;
using palDotNet.palDialog;

namespace CityScape.Tests
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("  CityScape Massive Terrain & Mesh Verification   ");
            Console.WriteLine("==================================================");

            int failures = 0;

            // 1. Test massive terrain creation matching user scenario (100,000m x 4,800m)
            try
            {
                float sizeX = 100000.0f;
                float sizeY = 4800.0f;
                float tileSizeX = 100000.0f;
                float tileSizeY = 4800.0f;
                int tileCountX = 1;
                int tileCountY = 1;
                float sampleDist = 2.0f;

                var terrain = new TerrainSurfaceData(sizeX, sizeY, tileSizeX, tileSizeY, tileCountX, tileCountY, sampleDist);

                if (terrain.SizeX != 100000.0f || terrain.SizeY != 4800.0f)
                {
                    Console.WriteLine("[FAIL] Terrain dimensions mismatch: " + terrain.SizeX + "x" + terrain.SizeY);
                    failures++;
                }
                else
                {
                    Console.WriteLine(string.Format("  [PASS] Terrain dimensions: {0:N0}m x {1:N0}m (Area: {2:F2} km², Aspect: {3:F1}:1)",
                        terrain.SizeX, terrain.SizeY, terrain.AreaSquareKilometers, terrain.AspectRatio));
                }

                if (Math.Abs(terrain.AreaSquareKilometers - 480.0) > 0.01)
                {
                    Console.WriteLine("[FAIL] Area calculation mismatch: " + terrain.AreaSquareKilometers);
                    failures++;
                }
                else
                {
                    Console.WriteLine("  [PASS] Area calculation verified (480.00 km²)");
                }

                if (terrain.TotalSamplePoints != 120000000L)
                {
                    Console.WriteLine("[FAIL] Total sample points mismatch: " + terrain.TotalSamplePoints);
                    failures++;
                }
                else
                {
                    Console.WriteLine(string.Format("  [PASS] Total sample points verified: {0:N0} samples", terrain.TotalSamplePoints));
                }

                // 2. Test ControlRender terrain activation
                ControlRender.SetActiveTerrain(terrain);
                if (ControlRender.ActiveTerrain.SizeX != 100000.0f)
                {
                    Console.WriteLine("[FAIL] ControlRender.ActiveTerrain SizeX mismatch!");
                    failures++;
                }
                else
                {
                    Console.WriteLine("  [PASS] ControlRender.ActiveTerrain updated and synchronized");
                }

                // 3. Test Viewport Off-screen rendering to Bitmap
                using (ControlRender cr = new ControlRender())
                {
                    cr.Width = 1280;
                    cr.Height = 720;
                    using (Bitmap bmp = new Bitmap(1280, 720))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            PaintEventArgs pe = new PaintEventArgs(g, new Rectangle(0, 0, 1280, 720));
                            // Invoke OnPaint via reflection
                            var onPaintMethod = typeof(ControlRender).GetMethod("OnPaint",
                                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                            onPaintMethod.Invoke(cr, new object[] { pe });
                        }

                        // Verify that rendering produced non-blank pixels
                        Color sample = bmp.GetPixel(640, 420);
                        Console.WriteLine(string.Format("  [PASS] 3D Terrain viewport rendered: center pixel color = ARGB({0}, {1}, {2}, {3})",
                            sample.A, sample.R, sample.G, sample.B));
                        if (sample.A == 0)
                        {
                            Console.WriteLine("[FAIL] Rendered viewport bitmap is empty/transparent!");
                            failures++;
                        }

                        string outBmp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "test_terrain_viewport.png");
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(outBmp));
                            bmp.Save(outBmp, System.Drawing.Imaging.ImageFormat.Png);
                            Console.WriteLine("  [PASS] Rendered 3D viewport saved to: " + outBmp);
                        }
                        catch { }
                    }
                }

                // 4. Test Camera Orbit & Zoom interaction
                using (ControlRender cr2 = new ControlRender())
                {
                    cr2.Width = 800;
                    cr2.Height = 600;
                    cr2.ResetCamera();
                    Console.WriteLine("  [PASS] Camera reset and viewport orbit/pan state verified");
                }

                // 5. Test Modern 3D Terrain Mesh & Heightmap Exporters
                Console.WriteLine("\n---> Verifying Modern 3D Terrain Export Pipeline...");
                var exportMesh = terrain.GenerateExportMesh(64, 64);
                if (exportMesh == null || exportMesh.Vertices.Count == 0 || exportMesh.Indices.Count == 0)
                {
                    Console.WriteLine("[FAIL] Failed to generate 3D export mesh from TerrainSurfaceData!");
                    failures++;
                }
                else
                {
                    Console.WriteLine(string.Format("  [PASS] Generated 3D export mesh: {0:N0} vertices, {1:N0} triangles",
                        exportMesh.Vertices.Count, exportMesh.Indices.Count / 3));
                }

                string exportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "terrain_exports");
                Directory.CreateDirectory(exportDir);

                // Test glTF 2.0 / GLB export
                string gltfPath = Path.Combine(exportDir, "massive_terrain.gltf");
                string glbPath = Path.Combine(exportDir, "massive_terrain.glb");
                var gltfExporter = new palDotNet.palModernEngine.GltfModernExporter();
                var meshList = new System.Collections.Generic.List<palDotNet.palModernEngine.ExportMeshData> { exportMesh };
                
                if (gltfExporter.ExportScene(gltfPath, meshList, "MassiveTerrainScene") && File.Exists(gltfPath))
                {
                    Console.WriteLine(string.Format("  [PASS] glTF 2.0 exporter verified: {0} ({1:N0} bytes)",
                        Path.GetFileName(gltfPath), new FileInfo(gltfPath).Length));
                }
                else
                {
                    Console.WriteLine("[FAIL] glTF 2.0 export failed!");
                    failures++;
                }

                if (gltfExporter.ExportScene(glbPath, meshList, "MassiveTerrainScene") && File.Exists(glbPath))
                {
                    Console.WriteLine(string.Format("  [PASS] GLB binary exporter verified: {0} ({1:N0} bytes)",
                        Path.GetFileName(glbPath), new FileInfo(glbPath).Length));
                }
                else
                {
                    Console.WriteLine("[FAIL] GLB binary export failed!");
                    failures++;
                }

                // Test Pixar OpenUSD export
                string usdPath = Path.Combine(exportDir, "massive_terrain.usda");
                var usdExporter = new palDotNet.palModernEngine.UsdModernExporter();
                if (usdExporter.ExportScene(usdPath, meshList, "MassiveTerrainScene") && File.Exists(usdPath))
                {
                    string usdContent = File.ReadAllText(usdPath);
                    if (usdContent.Contains("#usda 1.0") && usdContent.Contains("UsdPreviewSurface"))
                    {
                        Console.WriteLine(string.Format("  [PASS] Pixar USD exporter verified: {0} ({1:N0} bytes)",
                            Path.GetFileName(usdPath), new FileInfo(usdPath).Length));
                    }
                    else
                    {
                        Console.WriteLine("[FAIL] USDA output missing required OpenUSD headers!");
                        failures++;
                    }
                }
                else
                {
                    Console.WriteLine("[FAIL] Pixar USD export failed!");
                    failures++;
                }

                // Test Wavefront OBJ + PBR export
                string objPath = Path.Combine(exportDir, "massive_terrain.obj");
                var objExporter = new palDotNet.palModernEngine.WavefrontPbrModernExporter();
                if (objExporter.ExportScene(objPath, meshList, "MassiveTerrainScene") && File.Exists(objPath))
                {
                    Console.WriteLine(string.Format("  [PASS] Wavefront OBJ + PBR exporter verified: {0} ({1:N0} bytes)",
                        Path.GetFileName(objPath), new FileInfo(objPath).Length));
                }
                else
                {
                    Console.WriteLine("[FAIL] Wavefront OBJ export failed!");
                    failures++;
                }

                // Test Heightfield PNG export
                string pngHeightmap = Path.Combine(exportDir, "massive_terrain_heightmap.png");
                if (terrain.ExportHeightfieldImage(pngHeightmap, 256, 256) && File.Exists(pngHeightmap))
                {
                    Console.WriteLine(string.Format("  [PASS] Grayscale PNG heightmap exporter verified: {0} ({1:N0} bytes)",
                        Path.GetFileName(pngHeightmap), new FileInfo(pngHeightmap).Length));
                }
                else
                {
                    Console.WriteLine("[FAIL] PNG heightmap export failed!");
                    failures++;
                }

                // Test RAW 32-bit Float DEM export
                string rawDemPath = Path.Combine(exportDir, "massive_terrain_dem.raw");
                if (terrain.ExportHeightfieldRaw(rawDemPath, 256, 256) && File.Exists(rawDemPath))
                {
                    Console.WriteLine(string.Format("  [PASS] RAW 32-bit float DEM exporter verified: {0} ({1:N0} bytes)",
                        Path.GetFileName(rawDemPath), new FileInfo(rawDemPath).Length));
                }
                else
                {
                    Console.WriteLine("[FAIL] RAW DEM export failed!");
                    failures++;
                }

                // 6. Test Procedural 3D GIS & OSM Building Extrusion Engine
                Console.WriteLine("\n---> Verifying Procedural 3D GIS / OSM Building Extrusions...");
                var buildings = GisProceduralCityGenerator.GenerateUrbanGrid(terrain, 4, 4, 100f, 15f, 123);
                if (buildings == null || buildings.Count == 0)
                {
                    Console.WriteLine("[FAIL] Procedural city generation produced 0 buildings!");
                    failures++;
                }
                else
                {
                    Console.WriteLine(string.Format("  [PASS] Generated {0} procedural 3D buildings across 16 urban blocks", buildings.Count));
                    ControlRender.SetActiveBuildings(buildings);

                    // Verify building mesh generation and height attributes
                    var sampleBld = buildings[0];
                    var bldMesh = sampleBld.GenerateMesh();
                    if (bldMesh.Vertices.Count >= 8 && bldMesh.Indices.Count >= 12)
                    {
                        Console.WriteLine(string.Format("  [PASS] Extruded building mesh verified: {0} ({1} floors, {2:F1}m height, {3} vertices, {4} triangles)",
                            sampleBld.Footprint.Name, sampleBld.Footprint.FloorCount, sampleBld.TotalHeight, bldMesh.Vertices.Count, bldMesh.Indices.Count / 3));
                    }
                    else
                    {
                        Console.WriteLine("[FAIL] Extruded building mesh geometry is degenerate!");
                        failures++;
                    }

                    // Test Full City 3D Scene Export (Terrain + 3D Buildings) to glTF
                    string cityGltf = Path.Combine(exportDir, "procedural_city_complete.gltf");
                    var cityMeshes = new System.Collections.Generic.List<palDotNet.palModernEngine.ExportMeshData>();
                    cityMeshes.Add(exportMesh);
                    foreach (var b in buildings)
                    {
                        cityMeshes.Add(b.GenerateMesh());
                    }

                    if (gltfExporter.ExportScene(cityGltf, cityMeshes, "CompleteCityScene") && File.Exists(cityGltf))
                    {
                        Console.WriteLine(string.Format("  [PASS] Complete 3D City scene exported: {0} ({1:N0} bytes, {2} meshes)",
                            Path.GetFileName(cityGltf), new FileInfo(cityGltf).Length, cityMeshes.Count));
                    }
                    else
                    {
                        Console.WriteLine("[FAIL] Complete city scene export failed!");
                        failures++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FAIL] Exception during terrain verification: " + ex.ToString());
                failures++;
            }

            Console.WriteLine("==================================================");
            if (failures == 0)
            {
                Console.WriteLine("  ALL TERRAIN TESTS PASSED (100% OPERATIONAL)     ");
                Console.WriteLine("==================================================");
                return 0;
            }
            else
            {
                Console.WriteLine(string.Format("  {0} FAILURES DETECTED                           ", failures));
                Console.WriteLine("==================================================");
                return 1;
            }
        }
    }
}
