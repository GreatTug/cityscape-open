using System.Runtime.InteropServices;

namespace Sano.Utility.NativeMethods;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct RasterOperationCodes
{
	public const int SRCCOPY = 13369376;
}
