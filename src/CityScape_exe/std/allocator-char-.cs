using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

namespace std;

[StructLayout(LayoutKind.Sequential, Size = 1)]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
[NativeCppClass]
internal struct allocator_003Cchar_003E
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	[MiscellaneousBits(65)]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	[NativeCppClass]
	public struct rebind_003Cchar_003E
	{
	}

	[SpecialName]
	public unsafe static void _003CMarshalCopy_003E(allocator_003Cchar_003E* P_0, allocator_003Cchar_003E* P_1)
	{
		global::_003CModule_003E.std_002Eallocator_003Cchar_003E_002E_007Bctor_007D(P_0, P_1);
	}
}
