using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 76)]
[NativeCppClass]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
internal struct gxPostProcess
{
	[StructLayout(LayoutKind.Sequential, Size = 12)]
	[NativeCppClass]
	[MiscellaneousBits(64)]
	[DebugInfoInPDB]
	internal struct OffScreenKey
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[MiscellaneousBits(64)]
	internal struct OffScreenHash
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 12)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[MiscellaneousBits(64)]
	internal struct OffScreenTracker
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 4)]
	[NativeCppClass]
	[MiscellaneousBits(64)]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	public struct OffScreenReference
	{
	}
}
