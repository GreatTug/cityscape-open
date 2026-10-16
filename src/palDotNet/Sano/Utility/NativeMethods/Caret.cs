using System;
using System.Runtime.InteropServices;

namespace Sano.Utility.NativeMethods;

public class Caret
{
	[DllImport("user32.dll")]
	public static extern int HideCaret(IntPtr hwnd);

	[DllImport("user32.dll")]
	public static extern int ShowCaret(IntPtr hwnd);

	private Caret()
	{
	}
}
