using System;

namespace palDotNet.palWidgets;

public class ContextMenuArgs : EventArgs
{
	public Menu Menu;

	public ContextMenuArgs(Menu menu)
	{
		Menu = menu;
	}
}
