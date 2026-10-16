using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 32)]
[DebugInfoInPDB]
[MiscellaneousBits(64)]
[NativeCppClass]
internal struct arTypeHierarchy
{
	[StructLayout(LayoutKind.Sequential, Size = 20)]
	[NativeCppClass]
	[DebugInfoInPDB]
	[MiscellaneousBits(65)]
	internal struct ClassInfo
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	[MiscellaneousBits(65)]
	[DebugInfoInPDB]
	[NativeCppClass]
	internal struct ClassInfoCompare
	{
	}
}
