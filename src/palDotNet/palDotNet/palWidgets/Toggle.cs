using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class Toggle : Widget<bool>
{
	protected enum MessagesToggle
	{
		eDisplayedValues = 161,
		eLastToggleMsg = eDisplayedValues
	}

	public delegate void SetDisplayedValuesDel(Toggle toggle);

	public SetDisplayedValuesDel SetDisplayedValuesDelegate;

	private string TheIconPath;

	private string TheTrueString = "Enabled";

	private string TheFalseString = "";

	private bool TheIconOnly;

	public string IconPath => TheIconPath;

	public bool IconOnly => TheIconOnly;

	public string[] DisplayedValues => new string[2] { TheTrueString, TheFalseString };

	public Toggle(uint newId)
		: base(newId)
	{
		TheValue = false;
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			serializer.Serialize(ref TheValue);
			serializer.Serialize(ref TheIconPath);
			serializer.Serialize(ref TheIconOnly);
			if (serializer.Reading)
			{
				OnWidgetCreate();
			}
			break;
		case 161u:
			serializer.Serialize(ref TheTrueString);
			serializer.Serialize(ref TheFalseString);
			if (SetDisplayedValuesDelegate != null)
			{
				SetDisplayedValuesDelegate(this);
			}
			break;
		case 147u:
		{
			bool inst = TheValue;
			serializer.Serialize(ref inst);
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
