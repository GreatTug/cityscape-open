using System.Runtime.InteropServices;

namespace Sano.Utility.NativeMethods;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct WindowsMessages
{
	public const int WM_NCHITTEST = 132;

	public const int WM_KEYDOWN = 511;

	public const int WM_KEYUP = 767;
}
