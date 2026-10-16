using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

internal class IconLoader(uint newId) : TextBox(newId)
{
	protected enum MessagesIconLoader
	{
		eHeightMultiplier = 162,
		eLastIconLoaderMsg = eHeightMultiplier
	}

	public EventHandler EventUpdateView;

	private uint TheHeightMultiplier = 2u;

	public uint HeightMultiplier => TheHeightMultiplier;

	public override void Serialize(ISerializer serializer, uint message)
	{
		if (message == 162)
		{
			serializer.Serialize(ref TheHeightMultiplier);
			if (EventUpdateView != null)
			{
				EventUpdateView(this, EventArgs.Empty);
			}
		}
		else
		{
			base.Serialize(serializer, message);
		}
	}
}
