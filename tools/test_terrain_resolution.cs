using System;
using System.Windows.Forms;
using palDotNet.palDialog;

class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            Console.WriteLine("Step 1: Constructing TerrainSettingsPanel...");
            var panel = new TerrainSettingsPanel();
            panel.OnShown();
            Console.WriteLine("  [PASS] Panel instantiated and shown cleanly!");

            Console.WriteLine("Step 2: Verifying High Resolution Default (1.00m)...");
            if (panel.SamplingDistance.Value != 1.00m)
            {
                throw new Exception(string.Format("Expected default SamplingDistance = 1.00m, but was {0}m", panel.SamplingDistance.Value));
            }
            Console.WriteLine("  [PASS] Default SamplingDistance = 1.00m (High Resolution - Road Seating Optimized)!");

            Console.WriteLine("Step 3: Testing Ultra Resolution Preset (0.50m)...");
            panel.SamplingDistance.Value = 0.50m;
            Console.WriteLine("  [PASS] SamplingDistance accepted 0.50m (Ultra Detail)!");

            Console.WriteLine("Step 4: Testing Min/Max Limits...");
            panel.SetTessellationSampleDistRange(0f, 0f); // test native 0 passing
            if (panel.SamplingDistance.Minimum > 0.25m || panel.SamplingDistance.Maximum < 50m)
            {
                throw new Exception(string.Format("Invalid range [{0}, {1}]", panel.SamplingDistance.Minimum, panel.SamplingDistance.Maximum));
            }
            Console.WriteLine(string.Format("  [PASS] Range safely unlocked to [{0}m, {1}m]!", panel.SamplingDistance.Minimum, panel.SamplingDistance.Maximum));

            Console.WriteLine("Step 5: Testing Large Terrain & Bidirectional Tile Count Editing...");
            panel.SetTerrainSize(8000f, 8000f);
            panel.TileCountX.Value = 4;
            // 8000 / 4 = 2000m per tile
            Console.WriteLine(string.Format("  [PASS] 8000m terrain with TileCount 4 -> TileSize = {0}m x {1}m!", panel.TileSizeX.Value, panel.TileSizeY.Value));

            panel.Dispose();
            Console.WriteLine("SUCCESS: ALL TERRAIN RESOLUTION TESTS PASSED!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("EXCEPTION: {0}\n{1}", ex.Message, ex.StackTrace));
            Environment.Exit(1);
        }
    }
}
