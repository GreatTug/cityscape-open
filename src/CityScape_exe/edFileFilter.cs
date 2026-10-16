using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 20)]
[NativeCppClass]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
internal struct edFileFilter
{
	[StructLayout(LayoutKind.Sequential, Size = 24)]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	[NativeCppClass]
	[MiscellaneousBits(65)]
	public struct Entry
	{
	}
}
