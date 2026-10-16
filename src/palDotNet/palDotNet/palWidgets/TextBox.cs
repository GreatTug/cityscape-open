using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class TextBox(uint newId) : Widget<string>(newId)
{
	protected enum MessagesTextBox
	{
		eLastTextBoxMsg = 161
	}

	private string PrevValue = string.Empty;

	public override bool ManualEnable
	{
		get
		{
			return base.ManualEnable;
		}
		set
		{
			if (base.ManualEnable == value)
			{
				return;
			}
			if (base.ValueVisible)
			{
				if (value)
				{
					WidgetValue = PrevValue;
					PrevValue = string.Empty;
				}
				else
				{
					PrevValue = WidgetValue;
					WidgetValue = string.Empty;
				}
			}
			else
			{
				PrevValue = string.Empty;
			}
			base.ManualEnable = value;
		}
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			serializer.SerializeArString(ref TheValue);
			if (serializer.Reading)
			{
				OnWidgetCreate();
			}
			break;
		case 147u:
		{
			string inst = TheValue;
			serializer.SerializeArString(ref inst);
			WidgetValueNoSerialize = inst;
			break;
		}
		default:
			base.Serialize(serializer, message);
			break;
		case 136u:
			break;
		}
	}
}
