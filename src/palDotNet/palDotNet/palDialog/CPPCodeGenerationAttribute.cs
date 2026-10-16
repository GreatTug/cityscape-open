using System;

namespace palDotNet.palDialog;

public class CPPCodeGenerationAttribute : Attribute
{
	private string TheOutputFile;

	private bool TheWin32PCOnly;

	public string OutputFile
	{
		get
		{
			return TheOutputFile;
		}
		set
		{
			TheOutputFile = value;
		}
	}

	public bool Win32PCOnly
	{
		get
		{
			return TheWin32PCOnly;
		}
		set
		{
			TheWin32PCOnly = value;
		}
	}
}
