using System;

namespace palDotNet.palWidgets;

public class EventEnabledArgs : EventArgs
{
	public bool Enabled;

	public EventEnabledArgs(bool activate)
	{
		Enabled = activate;
	}
}
