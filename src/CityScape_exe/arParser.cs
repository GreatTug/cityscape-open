using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 48)]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
[NativeCppClass]
internal struct arParser
{
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[CLSCompliant(false)]
	[MiscellaneousBits(65)]
	public struct LoadInfo
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 20)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[MiscellaneousBits(65)]
	[CLSCompliant(false)]
	public struct SaveInfo
	{
	}
}
