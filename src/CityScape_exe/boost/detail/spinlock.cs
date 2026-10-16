using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

namespace boost.detail;

[StructLayout(LayoutKind.Sequential, Size = 4)]
[DebugInfoInPDB]
[NativeCppClass]
[MiscellaneousBits(64)]
internal struct spinlock
{
	[StructLayout(LayoutKind.Sequential, Size = 4)]
	[MiscellaneousBits(64)]
	[CLSCompliant(false)]
	[NativeCppClass]
	[DebugInfoInPDB]
	public struct scoped_lock
	{
	}
}
