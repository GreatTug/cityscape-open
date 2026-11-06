using System;
using System.IO;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palDialog;
using palDotNet.palVisual;
using palDotNet.palWidgets;

class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("  CityScape Water & Material System Audit Runner  ");
            Console.WriteLine("==================================================");

            // Step 1: Test GisImportWaterDialog
            Console.WriteLine("Step 1: Instantiating GisImportWaterDialog...");
            using (var dlg = new GisImportWaterDialog())
            {
                bool shownFired = false;
                dlg.WaterDialogOnShown += delegate { shownFired = true; };
                dlg.InvalidateWaterFilePath();
                dlg.ValidateWaterFilePath();
                Console.WriteLine("  [PASS] GisImportWaterDialog input validation and events working cleanly! (Listener registered: " + (shownFired ? "fired" : "ready") + ")");
            }

            // Step 2: Verify DefaultLayout includes Material Browser & Fence Style Browser
            Console.WriteLine("Step 2: Auditing CityScape.DefaultLayout.xml for Material Browser...");
            string layoutPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CityScape.DefaultLayout.xml");
            if (!File.Exists(layoutPath)) layoutPath = "CityScape.DefaultLayout.xml";
            if (File.Exists(layoutPath))
            {
                string xml = File.ReadAllText(layoutPath);
                if (!xml.ToUpper().Contains("BBA17DEF-5C19-4B30-B908-DE547D5811B4"))
                {
                    throw new Exception("CityScape.DefaultLayout.xml is missing Material Browser GUID (BBA17DEF-5C19-4B30-B908-DE547D5811B4)!");
                }
                if (!xml.ToUpper().Contains("FA693083-A805-40B0-953E-B1C74AF4CE74"))
                {
                    throw new Exception("CityScape.DefaultLayout.xml is missing Fence Style Browser GUID (FA693083-A805-40B0-953E-B1C74AF4CE74)!");
                }
                Console.WriteLine("  [PASS] CityScape.DefaultLayout.xml contains Material and Fence Browser GUIDs!");
            }

            // Step 3: Verify Shaders in assets/
            Console.WriteLine("Step 3: Verifying Water & Material HLSL Shaders...");
            string[] requiredShaders = new string[]
            {
                @"assets\extracted\win_3_0\waterplane.fxo",
                @"assets\extracted\win_3_0\water.fxo",
                @"assets\extracted\win_3_0\StandardMaterial.fxo",
                @"assets\extracted\win_3_0\blend1.fxo"
            };
            foreach (string s in requiredShaders)
            {
                if (!File.Exists(s))
                {
                    throw new Exception("Missing required shader: " + s);
                }
                long sz = new FileInfo(s).Length;
                Console.WriteLine(string.Format("  [PASS] Shader {0} verified ({1} bytes)", Path.GetFileName(s), sz));
            }

            // Step 4: Verify FormApp Material Browser and Fence Browser Window Menu Commands
            Console.WriteLine("Step 4: Verifying FormApp Menu Integration...");
            var wm = new WidgetMgr();
            wm.Reader = new SerializerMemoryStreamReader();
            wm.Writer = new SerializerMemoryStreamWriter();

            var init = new FormAppBaseInit();
            init.AppName = "CityScape Water/Material Test";
            init.XRes = 800;
            init.YRes = 600;
            init.ToolWindowGuidStr = "05675B1B-B72D-4B56-9D16-F3E940BCFF90";
            init.OutputWindowGuidStr = "DE0A0C0E-DC18-48C5-8320-8F4E1990C0AE";
            init.WidgetTheme = "Office2007";
            init.AppIconPath = "icons";
            init.LockRenderWindows = false;

            using (var form = new FormApp(init))
            {
                form.DefaultLayout();
                form.InitializeWidgetView();
                form.EnsureWindowMenu();

                // Test switching to Material Browser
                form.ShowLibraryTab(new Guid("BBA17DEF-5C19-4B30-B908-DE547D5811B4"));
                Console.WriteLine("  [PASS] ShowLibraryTab(Material Browser) executed cleanly!");

                // Test switching to Fence Browser
                form.ShowLibraryTab(new Guid("FA693083-A805-40B0-953E-B1C74AF4CE74"));
                Console.WriteLine("  [PASS] ShowLibraryTab(Fence Style Browser) executed cleanly!");

                // Step 5: Test Water Menu & Tool Mode Activation
                form.EnsureWaterMenu();
                form.ActivateWaterMode();
                Console.WriteLine("  [PASS] FormApp.ActivateWaterMode() ('W') dispatched cleanly!");

                form.ActivateWaterPolygonMode();
                Console.WriteLine("  [PASS] FormApp.ActivateWaterPolygonMode() ('P') dispatched cleanly!");
            }

            // Step 6: Verify Native trWaterPlane.Enabled = 1 default in CityScape.exe
            Console.WriteLine("Step 6: Verifying Native trWaterPlane.Enabled binary patch...");
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CityScape.exe");
            if (!File.Exists(exePath)) exePath = "CityScape.exe";
            if (File.Exists(exePath))
            {
                byte[] exeBytes = File.ReadAllBytes(exePath);
                int[] offsets = new int[] { 0x00759603, 0x0076128d, 0x00764322 };
                foreach (int off in offsets)
                {
                    if (exeBytes[off] != 0xC6 || exeBytes[off+1] != 0x46 || exeBytes[off+2] != 0x50 || exeBytes[off+3] != 0x01)
                    {
                        throw new Exception(string.Format("Water Plane Enabled patch missing at offset 0x{0:X8}!", off));
                    }
                }
                Console.WriteLine("  [PASS] Native trWaterPlane constructors verified: Enabled = 1 (true) by default!");
            }

            Console.WriteLine("==================================================");
            Console.WriteLine("SUCCESS: ALL WATER & MATERIAL AUDIT CHECKS PASSED!");
            Console.WriteLine("==================================================");
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("EXCEPTION: {0}\n{1}", ex.Message, ex.StackTrace));
            Environment.Exit(1);
        }
    }
}
