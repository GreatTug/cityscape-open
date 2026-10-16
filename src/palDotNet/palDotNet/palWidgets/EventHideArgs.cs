using System;

namespace palDotNet.palWidgets;

public class EventHideArgs : EventArgs
{
	public enum EnumHideType
	{
		eAutoHide,
		eHidden
	}

	public enum EnumDockingType
	{
		eLeft,
		eRight,
		eTop,
		eBottom,
		ePrevState
	}

	public readonly bool Hide;

	public EnumHideType Type;

	public EnumDockingType DockingType;

	public EventHideArgs(bool hide)
	{
		Hide = hide;
	}
}
