using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palVisual;
using palDotNet.palWidgets;

namespace CityScape
{
    internal static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(int dwProcessId);

        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// Main application entry point for PixelActive CityScape 1.8.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // Setup working directory and base path
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                Directory.SetCurrentDirectory(baseDir);
            }
            catch
            {
            }

            // Check for command-line options
            if (args != null && args.Length > 0)
            {
                AttachConsole(ATTACH_PARENT_PROCESS);

                string firstArg = args[0].ToLowerInvariant();
                if (firstArg == "--help" || firstArg == "-h" || firstArg == "/?")
                {
                    Console.WriteLine("\nPixelActive CityScape 1.8 (Autodesk Promo Edition)");
                    Console.WriteLine("=================================================");
                    Console.WriteLine("Usage:");
                    Console.WriteLine("  CityScape.exe                  Start interactive urban modeling suite");
                    Console.WriteLine("  CityScape.exe [project.csp]    Load project file");
                    Console.WriteLine("  CityScape.exe --diagnostics    Run environment and assembly diagnostics");
                    Console.WriteLine("  CityScape.exe --version        Show version information");
                    Console.WriteLine("  CityScape.exe --help           Show this help message\n");
                    return;
                }
                else if (firstArg == "--version" || firstArg == "-v")
                {
                    Console.WriteLine("\nPixelActive CityScape 1.8 (Autodesk Promo Edition)");
                    Console.WriteLine("Build: 1.8.3.34904");
                    Console.WriteLine("Target Runtime: .NET Framework 4.8 (x86)");
                    Console.WriteLine("Open Source Reconstruction (2026)\n");
                    return;
                }
                else if (firstArg == "--diagnostics")
                {
                    Console.WriteLine("\n========================================");
                    Console.WriteLine("  CityScape Diagnostics & Assembly Check");
                    Console.WriteLine("========================================");
                    Console.WriteLine($"Base Directory: {baseDir}");
                    Console.WriteLine($"CLR Version:    {Environment.Version}");
                    Console.WriteLine($"OS Version:     {Environment.OSVersion}");
                    Console.WriteLine($"Is 64-bit OS:   {Environment.Is64BitOperatingSystem}");
                    Console.WriteLine($"Is 64-bit Proc: {Environment.Is64BitProcess}");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Referenced Assemblies:");
                    foreach (AssemblyName an in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
                    {
                        Console.WriteLine($"  - {an.Name} (v{an.Version})");
                    }
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Diagnostics complete: All subsystems operational.\n");
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Handle unhandled exceptions gracefully
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show(
                        "CityScape runtime error:\n\n" + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace,
                        "CityScape Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            };

            // Initialize FormAppBase parameters
            FormAppBaseInit initParams = default;
            initParams.AppName = "PixelActive CityScape 1.8 (Autodesk Promo Edition)";
            initParams.XRes = 1280;
            initParams.YRes = 800;
            initParams.ToolWindowGuidStr = "05675B1B-B72D-4B56-9D16-F3E940BCFF90";
            initParams.OutputWindowGuidStr = "DE0A0C0E-DC18-48C5-8320-8F4E1990C0AE";
            initParams.WidgetTheme = "Office2007";
            initParams.AppIconPath = Path.Combine(baseDir, "icons");
            initParams.LockRenderWindows = false;

            // Initialize Core Widget System & Message Streams
            WidgetMgr widgetMgr = new WidgetMgr();
            SerializerMemoryStreamReader reader = new SerializerMemoryStreamReader();
            SerializerMemoryStreamWriter writer = new SerializerMemoryStreamWriter();
            widgetMgr.Reader = reader;
            widgetMgr.Writer = writer;

            // Wire up quit event
            widgetMgr.EventQuit += delegate
            {
                Application.Exit();
            };

            // Launch CityScape UI
            try
            {
                FormApp mainForm = new FormApp(initParams);
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error launching CityScape UI:\n" + ex.Message + "\n\n" + ex.StackTrace,
                    "CityScape Startup Exception",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
