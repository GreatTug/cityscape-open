using System;
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

            form.Dispose();
            Console.WriteLine("SUCCESS: ALL LIFECYCLE STEPS COMPLETED CLEANLY!");
        } catch (Exception ex) {
            Console.WriteLine(string.Format("EXCEPTION: {0}\n{1}", ex.Message, ex.StackTrace));
            Environment.Exit(1);
        }
    }
}
