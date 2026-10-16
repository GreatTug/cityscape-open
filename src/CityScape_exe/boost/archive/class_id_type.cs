using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

namespace boost.archive;

[StructLayout(LayoutKind.Sequential, Size = 4)]
[DebugInfoInPDB]
[NativeCppClass]
[MiscellaneousBits(65)]
internal struct class_id_type
{
	[SpecialName]
	public unsafe static void _003CMarshalCopy_003E(class_id_type* P_0, class_id_type* P_1)
	{
		*(int*)P_0 = *(int*)P_1;
	}
}
