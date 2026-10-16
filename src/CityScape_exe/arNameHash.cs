using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 48)]
[MiscellaneousBits(64)]
[NativeCppClass]
[DebugInfoInPDB]
internal struct arNameHash
{
	[StructLayout(LayoutKind.Sequential, Size = 8)]
	[DebugInfoInPDB]
	[MiscellaneousBits(65)]
	[CLSCompliant(false)]
	[NativeCppClass]
	public struct TypePointer
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 12)]
	[MiscellaneousBits(64)]
	[CLSCompliant(false)]
	[NativeCppClass]
	[DebugInfoInPDB]
	public struct Iterator
	{
	}
}
