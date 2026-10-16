using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 8)]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
[NativeCppClass]
internal static struct trTerrainExporter
{
	[StructLayout(LayoutKind.Sequential, Size = 104)]
	[MiscellaneousBits(65)]
	[NativeCppClass]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	public struct ExportOptions
	{
		[CLSCompliant(false)]
		[NativeCppClass]
		[DebugInfoInPDB]
		[MiscellaneousBits(64)]
		public enum EnumLodExport
		{

		}

		[NativeCppClass]
		[MiscellaneousBits(64)]
		[DebugInfoInPDB]
		[CLSCompliant(false)]
		public enum EnumAxis
		{

		}

		[DebugInfoInPDB]
		[CLSCompliant(false)]
		[MiscellaneousBits(64)]
		[NativeCppClass]
		public enum EnumPropExport
		{

		}
	}

	[StructLayout(LayoutKind.Sequential, Size = 4)]
	[MiscellaneousBits(64)]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	[NativeCppClass]
	public struct ExtraExportOptions
	{
	}

	[MiscellaneousBits(64)]
	[CLSCompliant(false)]
	[DebugInfoInPDB]
	[NativeCppClass]
	public enum EnumExporterAttributes
	{

	}

	[DebugInfoInPDB]
	[MiscellaneousBits(64)]
	[NativeCppClass]
	[CLSCompliant(false)]
	public enum EnumSupportedOptions
	{

	}
}
