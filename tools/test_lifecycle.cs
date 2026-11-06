using System;
using System.Windows.Forms;
using palDotNet.palWidgets;
using palDotNet.palCore;
using palDotNet.palVisual;

class Program {
    [STAThread]
    static void Main() {
        try {
            Console.WriteLine("Step 1: Initializing WidgetMgr...");
            var wm = new WidgetMgr();
            wm.Reader = new SerializerMemoryStreamReader();
            wm.Writer = new SerializerMemoryStreamWriter();

            Console.WriteLine("Step 2: Initializing FormAppBaseInit...");
            var init = new FormAppBaseInit();
            init.AppName = "CityScape Load Test";
            init.XRes = 800;
            init.YRes = 600;
            init.ToolWindowGuidStr = "05675B1B-B72D-4B56-9D16-F3E940BCFF90";
            init.OutputWindowGuidStr = "DE0A0C0E-DC18-48C5-8320-8F4E1990C0AE";
            init.WidgetTheme = "Office2007";
            init.AppIconPath = "icons";
            init.LockRenderWindows = false;

            Console.WriteLine("Step 3: Constructing FormApp...");
            var form = new FormApp(init);
            Console.WriteLine(string.Format("FormApp constructed: {0}", form.Text));

            Console.WriteLine("Step 4: FormApp.DefaultLayout()...");
            form.DefaultLayout();
            Console.WriteLine("DefaultLayout succeeded!");

            Console.WriteLine("Step 5: FormApp.InitializeWidgetView()...");
            form.InitializeWidgetView();
            Console.WriteLine("InitializeWidgetView succeeded!");

            Console.WriteLine("Step 6: FormApp.LoadLayout()...");
            form.LoadLayout();
            Console.WriteLine("LoadLayout succeeded!");

            Console.WriteLine("Step 7: Verifying Window Menu on MenuStrip...");
            form.EnsureWindowMenu();
            bool hasWindowMenu = false;
            foreach (ToolStripItem item in form.MainMenuStrip.Items) {
                if (item.Text.Replace("&", "").Trim().Equals("Window", StringComparison.OrdinalIgnoreCase)) {
                    hasWindowMenu = true;
                    var dropDown = item as ToolStripDropDownItem;
                    Console.WriteLine(string.Format("  [PASS] Found Window Menu with {0} items:", dropDown.DropDownItems.Count));
                    foreach (ToolStripItem sub in dropDown.DropDownItems) {
                        Console.WriteLine(string.Format("    - {0}", sub.Text));
                    }
                    break;
                }
            }
            if (!hasWindowMenu) {
                throw new Exception("Window menu was not found on MainMenuStrip!");
            }

            Console.WriteLine("Step 8: Testing ResetWindowLayout()...");
            form.ResetWindowLayout();
            Console.WriteLine("  [PASS] ResetWindowLayout() completed cleanly!");

            Console.WriteLine("Step 9: Testing SaveCurrentLayout()...");
            form.SaveCurrentLayout();
            Console.WriteLine("  [PASS] SaveCurrentLayout() completed cleanly!");

            Console.WriteLine("Step 10: Testing ShowLibraryTab (Road Style)...");
            form.ShowLibraryTab(new Guid("05675B1B-B72D-4B56-9D16-F3E940BCFF90"));
            Console.WriteLine("  [PASS] ShowLibraryTab (Road Style) executed cleanly!");

            form.Dispose();
            Console.WriteLine("SUCCESS: ALL LIFECYCLE & WINDOW RESTORE STEPS COMPLETED CLEANLY!");
        } catch (Exception ex) {
            Console.WriteLine(string.Format("EXCEPTION: {0}\n{1}", ex.Message, ex.StackTrace));
            Environment.Exit(1);
        }
    }
}
