using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class Label : WidgetBase
{
	public Label(uint newId)
		: base(newId)
	{
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			if (serializer.Reading)
			{
				OnWidgetCreate();
			}
			break;
		default:
			base.Serialize(serializer, message);
			base.ValueVisible = true;
			break;
		case 136u:
		case 147u:
			break;
		}
	}
}
