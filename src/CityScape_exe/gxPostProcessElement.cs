using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 76)]
[NativeCppClass]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
internal struct gxPostProcessElement
{
	[StructLayout(LayoutKind.Sequential, Size = 12)]
	[MiscellaneousBits(64)]
	[NativeCppClass]
	[DebugInfoInPDB]
	internal static struct ElementInput
	{
		[StructLayout(LayoutKind.Explicit, Size = 4)]
		[MiscellaneousBits(66)]
		[NativeCppClass]
		[DebugInfoInPDB]
		internal struct _003Cunnamed_002Dtype_002DVarHandle_003E
		{
		}
	}

	[StructLayout(LayoutKind.Sequential, Size = 16)]
	[NativeCppClass]
	[DebugInfoInPDB]
	[MiscellaneousBits(64)]
	internal struct ElementInputTex
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 4)]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	[MiscellaneousBits(64)]
	[NativeCppClass]
	public struct TextureRef
	{
	}
}
