using System;
using System.Collections.Generic;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

internal class ComboBox : Widget<int>
{
	private List<string> TheItems = new List<string>();

	public string[] Items => TheItems.ToArray();

	public override string PropertyValueName => "StringValue";

	public string StringValue
	{
		get
		{
			if (ValueValid)
			{
				return TheItems[TheValue];
			}
			return string.Empty;
		}
		set
		{
			int num = TheItems.FindIndex((string str) => str == value);
			if (num >= 0)
			{
				bool flag = TheValue.Equals(num);
				if (!flag)
				{
					TheValue = num;
					OnWidgetValueChanged(EventArgs.Empty);
				}
				CheckValueVisible(flag);
			}
		}
	}

	private bool ValueValid
	{
		get
		{
			if (TheValue >= 0)
			{
				return TheValue < TheItems.Count;
			}
			return false;
		}
	}

	public ComboBox(uint newId)
		: base(newId)
	{
		TheValue = 0;
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
		{
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			serializer.Serialize(ref TheValue);
			uint inst2 = 0u;
			serializer.Serialize(ref inst2);
			for (uint num = 0u; num < inst2; num++)
			{
				string inst3 = "";
				serializer.Serialize(ref inst3);
				TheItems.Add(inst3);
			}
			if (!ValueValid)
			{
				base.ValueVisible = false;
			}
			if (serializer.Reading)
			{
				OnWidgetCreate();
			}
			break;
		}
		case 147u:
		{
			int inst = TheValue;
			serializer.Serialize(ref inst);
			if (!ValueValid)
			{
				base.ValueVisible = false;
			}
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
