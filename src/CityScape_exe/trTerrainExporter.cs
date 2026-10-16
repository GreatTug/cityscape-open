using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

[StructLayout(LayoutKind.Sequential, Size = 8)]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
[NativeCppClass]
internal struct trTerrainExporter
{
	[StructLayout(LayoutKind.Sequential, Size = 104)]
	[MiscellaneousBits(65)]
	[NativeCppClass]
	[DebugInfoInPDB]
	[CLSCompliant(false)]
	public struct ExportOptions
	{
		[CLSCompliant(false)]
		
		[DebugInfoInPDB]
		[MiscellaneousBits(64)]
		public enum EnumLodExport
		{

		}

		
		[MiscellaneousBits(64)]
		[DebugInfoInPDB]
		[CLSCompliant(false)]
		public enum EnumAxis
		{

		}

		[DebugInfoInPDB]
		[CLSCompliant(false)]
		[MiscellaneousBits(64)]
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
	public enum EnumExporterAttributes
	{

	}

	[DebugInfoInPDB]
	[MiscellaneousBits(64)]
	
	[CLSCompliant(false)]
	public enum EnumSupportedOptions
	{

	}
}
