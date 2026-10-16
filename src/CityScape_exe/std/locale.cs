using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

namespace std;

[StructLayout(LayoutKind.Sequential, Size = 4)]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
[NativeCppClass]
internal struct locale
{
	[StructLayout(LayoutKind.Sequential, Size = 4)]
	[MiscellaneousBits(64)]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	[NativeCppClass]
	public struct id
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 52)]
	[CLSCompliant(false)]
	[NativeCppClass]
	[DebugInfoInPDB]
	[UnsafeValueType]
	[MiscellaneousBits(64)]
	public struct _Locimp
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 8)]
	[NativeCppClass]
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	[MiscellaneousBits(64)]
	public struct facet
	{
	}
}
