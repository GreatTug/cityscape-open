using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
public class Bank : BankBase
{
	protected enum BankMessages
	{
		eBeginGroup = 165,
		eEndGroup = 166,
		eLastMsg = eEndGroup
	}

	private bool WidgetPanel;

	private bool Indent = true;

	private BankGroup TheCurrentGroup;

	public bool IsWidgetPanel => WidgetPanel;

	internal BankGroup CurrentGroup => TheCurrentGroup;

	public Bank(uint newId)
		: base(newId)
	{
		TheValue = false;
	}

	public override void Serialize(ISerializer serializer, uint msgType)
	{
		switch (msgType)
		{
		case 137u:
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			serializer.Serialize(ref WidgetPanel);
			serializer.Serialize(ref Indent);
			serializer.Serialize(ref TheValue);
			if (serializer.Reading)
			{
				OnWidgetCreate();
			}
			break;
		case 165u:
		{
			string inst2 = "";
			serializer.Serialize(ref inst2);
			TheCurrentGroup = new BankGroup(inst2);
			break;
		}
		case 166u:
			TheCurrentGroup = null;
			break;
		case 147u:
		{
			bool inst = TheValue;
			serializer.Serialize(ref inst);
			WidgetValueNoSerialize = inst;
			break;
		}
		default:
			base.Serialize(serializer, msgType);
			break;
		case 136u:
			break;
		}
	}
}
