using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class CommandBar : Widget<string>
{
	private enum Messages
	{
		eShowHideCommandBar = 161
	}

	public delegate void ShowHideDel(bool show);

	public ShowHideDel ShowHideDelegate;

	public static event EventHandler EventCreateCommandBar;

	public CommandBar(uint newId)
		: base(newId)
	{
		TheValue = string.Empty;
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
			if (serializer.Reading && EventCreateCommandBar != null)
			{
				EventCreateCommandBar(this, null);
			}
			break;
		case 161u:
		{
			bool inst = false;
			serializer.Serialize(ref inst);
			if (ShowHideDelegate != null)
			{
				ShowHideDelegate(inst);
			}
			break;
		}
		default:
			base.Serialize(serializer, message);
			break;
		case 136u:
			break;
		}
	}

	public void SendCommand(string command)
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		uint inst = 147u;
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		writer.Serialize(ref command);
		writer.End(clearIndex: false);
	}
}
