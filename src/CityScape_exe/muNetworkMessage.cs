using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 44)]
[NativeCppClass]
[DebugInfoInPDB]
[MiscellaneousBits(64)]
internal struct muNetworkMessage
{
	[DebugInfoInPDB]
	[MiscellaneousBits(64)]
	
	[CLSCompliant(false)]
	public enum EnumMessageType
	{

	}

	[StructLayout(LayoutKind.Sequential, Size = 32)]
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	[MiscellaneousBits(65)]
	[NativeCppClass]
	public struct ServiceInfo
	{
	}
}
