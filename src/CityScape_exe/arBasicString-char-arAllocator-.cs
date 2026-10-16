using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 8)]
[NativeCppClass]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
internal struct arBasicString_003Cchar_002CarAllocator_003E
{
	[StructLayout(LayoutKind.Sequential, Size = 20)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[MiscellaneousBits(65)]
	internal struct StringData
	{
	}

	[SpecialName]
	public unsafe static void _003CMarshalCopy_003E(arBasicString_003Cchar_002CarAllocator_003E* P_0, arBasicString_003Cchar_002CarAllocator_003E* P_1)
	{
		int num = (*(int*)P_0 = *(int*)P_1);
		if (num != 0)
		{
			global::_003CModule_003E.arAtomic_002EInterlockIncrementWrapper((int*)(num + 16));
		}
	}

	[SpecialName]
	public unsafe static void _003CMarshalDestroy_003E(arBasicString_003Cchar_002CarAllocator_003E* P_0)
	{
		StringData* ptr = (StringData*)(int)(*(uint*)P_0);
		if (ptr != null && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)ptr + 4) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr));
			*(int*)ptr = 0;
			((int*)ptr)[2] = 0;
			((int*)ptr)[1] = 0;
			global::_003CModule_003E.delete(ptr);
		}
		*(int*)P_0 = 0;
	}
}
