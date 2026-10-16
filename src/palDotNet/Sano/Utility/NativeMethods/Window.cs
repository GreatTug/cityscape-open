using System;
using System.Runtime.InteropServices;

namespace Sano.Utility.NativeMethods;

public class Window
{
	[DllImport("User32.dll", SetLastError = true)]
	public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	private Window()
	{
	}
}
