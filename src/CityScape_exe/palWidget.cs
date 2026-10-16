using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 60)]
[NativeCppClass]
[DebugInfoInPDB]
[MiscellaneousBits(64)]
internal struct palWidget
{
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	[MiscellaneousBits(64)]
	[NativeCppClass]
	public enum EnumMsg
	{

	}

	[StructLayout(LayoutKind.Sequential, Size = 20)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[CLSCompliant(false)]
	[MiscellaneousBits(65)]
	public struct DragDropData
	{
	}

	[MiscellaneousBits(64)]
	[NativeCppClass]
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	public enum EnumEditingState
	{

	}

	[MiscellaneousBits(64)]
	[NativeCppClass]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	public enum EnumFocus
	{

	}
}
