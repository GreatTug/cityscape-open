using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class GenericLoader : IconLoader
{
	private enum MessagesGenericLoader
	{
		eSetIcon = 163,
		eButtonPressed = 164,
		eLastGenericLoaderMsg = eButtonPressed
	}

	private byte[] TheBitmapBytes;

	public byte[] BitmapBytes => TheBitmapBytes;

	public GenericLoader(uint newId)
		: base(newId)
	{
	}

	public void SendButtonPressed()
	{
		WriteMessage(164u);
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 163u:
		{
			int inst = 0;
			serializer.Serialize(ref inst);
			TheBitmapBytes = null;
			if (inst > 0)
			{
				serializer.Serialize(ref TheBitmapBytes, inst);
			}
			if (EventUpdateView != null)
			{
				EventUpdateView(this, EventArgs.Empty);
			}
			break;
		}
		default:
			base.Serialize(serializer, message);
			break;
		case 164u:
			break;
		}
	}
}
