using System;
using System.Collections.Generic;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class Mutex : Widget<int>
{
	private enum Messages
	{
		eAddValue = 161
	}

	public delegate void AddValuesDel(List<string> names);

	private List<string> Names = new List<string>();

	private List<int> MutexValues = new List<int>();

	public AddValuesDel AddValuesDelegate;

	public override string PropertyValueName => "WidgetStringValue";

	public string WidgetStringValue
	{
		get
		{
			if (FindNameIndex(TheValue) < 0)
			{
				return " ";
			}
			return FindName(TheValue);
		}
		set
		{
			Predicate<string> match = (string checkString) => checkString == value;
			int num = Names.FindIndex(match);
			if (num >= 0)
			{
				WidgetValue = MutexValues[num];
			}
		}
	}

	public Mutex(uint newId)
		: base(newId)
	{
		TheValue = 0;
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			serializer.Serialize(ref TheValue);
			if (serializer.Reading)
			{
				OnWidgetCreate();
			}
			break;
		case 147u:
		{
			int inst3 = TheValue;
			serializer.Serialize(ref inst3);
			if (inst3 != TheValue)
			{
				WidgetValue = inst3;
			}
			break;
		}
		case 161u:
		{
			string inst = null;
			int inst2 = 0;
			serializer.Serialize(ref inst);
			serializer.Serialize(ref inst2);
			Names.Add(inst);
			MutexValues.Add(inst2);
			if (AddValuesDelegate != null)
			{
				AddValuesDelegate(Names);
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

	public int FindNameIndex(int theValue)
	{
		Predicate<int> match = (int checkInt) => checkInt == theValue;
		return MutexValues.FindIndex(match);
	}

	public string FindName(int theValue)
	{
		int index = FindNameIndex(theValue);
		return Names[index];
	}
}
