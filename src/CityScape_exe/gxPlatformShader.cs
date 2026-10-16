using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 8)]
[DebugInfoInPDB]
[MiscellaneousBits(64)]
[NativeCppClass]
internal struct gxPlatformShader
{
	[StructLayout(LayoutKind.Sequential, Size = 28)]
	[CLSCompliant(false)]
	[MiscellaneousBits(64)]
	[NativeCppClass]
	[DebugInfoInPDB]
	public struct ShaderData
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 20)]
	[MiscellaneousBits(65)]
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	[NativeCppClass]
	public struct ShaderHandle
	{
		[CLSCompliant(false)]
		[NativeCppClass]
		[DebugInfoInPDB]
		[MiscellaneousBits(64)]
		public enum EnumShaderType
		{

		}
	}
}
