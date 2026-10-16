using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 1)]
[NativeCppClass]
[DebugInfoInPDB]
[MiscellaneousBits(64)]
internal struct mtSimd
{
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	[MiscellaneousBits(65)]
	[NativeCppClass]
	public struct VecFloatUnion
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 16)]
	[CLSCompliant(false)]
	[MiscellaneousBits(65)]
	[DebugInfoInPDB]
	[NativeCppClass]
	public struct VecIntUnion
	{
	}
}
