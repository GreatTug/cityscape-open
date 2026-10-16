using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

namespace trLegacyProp.Road;

[StructLayout(LayoutKind.Sequential, Size = 20)]
[MiscellaneousBits(64)]
[NativeCppClass]
[DebugInfoInPDB]
internal struct ICurveHelper
{
	[StructLayout(LayoutKind.Sequential, Size = 72)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[MiscellaneousBits(65)]
	[CLSCompliant(false)]
	public struct FindResult
	{
	}
}
