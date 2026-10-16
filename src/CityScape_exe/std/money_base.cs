using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

namespace std;

[StructLayout(LayoutKind.Sequential, Size = 8)]
[NativeCppClass]
[MiscellaneousBits(65)]
[DebugInfoInPDB]
internal struct money_base
{
	[StructLayout(LayoutKind.Sequential, Size = 4)]
	[MiscellaneousBits(65)]
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[UnsafeValueType]
	public struct pattern
	{
	}
}
