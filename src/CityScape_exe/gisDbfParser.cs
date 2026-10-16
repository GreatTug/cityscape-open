using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 84)]
[NativeCppClass]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
internal struct gisDbfParser
{
	[StructLayout(LayoutKind.Sequential, Size = 20)]
	[NativeCppClass]
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	[MiscellaneousBits(65)]
	public struct FieldDescriptor
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 8)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[MiscellaneousBits(65)]
	internal struct FieldOfInterest
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 20)]
	[MiscellaneousBits(65)]
	[NativeCppClass]
	[DebugInfoInPDB]
	internal struct HeaderInfo
	{
	}
}
