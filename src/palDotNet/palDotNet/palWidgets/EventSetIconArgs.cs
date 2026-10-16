using System;

namespace palDotNet.palWidgets;

public class EventSetIconArgs : EventArgs
{
	public readonly string Path;

	public EventSetIconArgs(string iconPath)
	{
		Path = iconPath;
	}
}
