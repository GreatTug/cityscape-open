using System;
using System.Collections.Generic;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

public class Output
{
	public enum EnumModes
	{
		ePrint,
		eWarning,
		eError,
		eAbort,
		eClearScreen
	}

	private static SerializerReader TheSerializer;

	private static List<string> Lines = new List<string>();

	private static List<EnumModes> Modes = new List<EnumModes>();

	public static SerializerReader Serializer
	{
		get
		{
			return TheSerializer;
		}
		set
		{
			TheSerializer = value;
		}
	}

	public static void Read(out List<string> lines, out List<EnumModes> modes)
	{
		lines = null;
		modes = null;
		if (TheSerializer == null)
		{
			return;
		}
		if (TheSerializer.Begin())
		{
			lines = new List<string>();
			modes = new List<EnumModes>();
			while (!TheSerializer.AtEnd)
			{
				byte inst = 0;
				string inst2 = "";
				TheSerializer.Serialize(ref inst);
				TheSerializer.Serialize(ref inst2);
				lines.Add(inst2);
				modes.Add((EnumModes)inst);
			}
		}
		TheSerializer.End(clearIndex: true);
		if (Lines.Count <= 0)
		{
			return;
		}
		if (lines == null)
		{
			lines = new List<string>();
			modes = new List<EnumModes>();
		}
		lock (Lines)
		{
			lines.AddRange(Lines);
			modes.AddRange(Modes);
			Lines.Clear();
			Modes.Clear();
		}
	}

	public static void WriteLine(string line, EnumModes mode)
	{
		lock (Lines)
		{
			Lines.Add(line + Environment.NewLine);
			Modes.Add(mode);
		}
	}

	public static void WriteLine(string line)
	{
		WriteLine(line, EnumModes.ePrint);
	}
}
