using System.Runtime.InteropServices;

namespace Sano.Utility.NativeMethods;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct ShowWindowMessages
{
	public const int SW_SHOWMINIMIZED = 2;

	public const int SW_SHOWMAXIMIZED = 3;

	public const int SW_SHOWNOACTIVATE = 4;

	public const int SW_SHOWMINNOACTIVE = 7;

	public const int SW_SHOWNA = 8;
}
