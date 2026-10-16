using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 8)]
[NativeCppClass]
[DebugInfoInPDB]
[MiscellaneousBits(64)]
internal struct arOutput
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	[NativeCppClass]
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	[MiscellaneousBits(64)]
	public struct AbortException
	{
	}

	[MiscellaneousBits(64)]
	[CLSCompliant(false)]
	[NativeCppClass]
	[DebugInfoInPDB]
	public enum EnumPrintMode
	{

	}

	[StructLayout(LayoutKind.Sequential, Size = 12)]
	[NativeCppClass]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	[MiscellaneousBits(64)]
	public struct ErrorReporter
	{
	}
}
