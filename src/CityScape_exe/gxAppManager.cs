using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 12)]
[NativeCppClass]
[DebugInfoInPDB]
[MiscellaneousBits(64)]
internal struct gxAppManager
{
	[StructLayout(LayoutKind.Sequential, Size = 52)]
	[DebugInfoInPDB]
	[MiscellaneousBits(65)]
	[CLSCompliant(false)]
	[NativeCppClass]
	public struct StartupInfo
	{
		[SpecialName]
		public unsafe static void _003CMarshalCopy_003E(StartupInfo* P_0, StartupInfo* P_1)
		{
			global::_003CModule_003E.gxAppManager_002EStartupInfo_002E_007Bctor_007D(P_0, P_1);
		}

		[SpecialName]
		public unsafe static void _003CMarshalDestroy_003E(StartupInfo* P_0)
		{
			global::_003CModule_003E.gxAppManager_002EStartupInfo_002E_007Bdtor_007D(P_0);
		}
	}
}
