using System.Collections.Generic;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

public class Units
{
	public delegate void UpdateUnitsDel(Units unitToUpdate);

	public string UnitName;

	public uint PreviousCurrent;

	public uint CurrentConversion;

	public string[] UnitTypes;

	public float[] ConversionFactors;

	public static Dictionary<string, Units> UnitsHash = new Dictionary<string, Units>();

	public static UpdateUnitsDel UpdateUnitsDelegate;

	public float PreviousConversionFactor => ConversionFactors[PreviousCurrent];

	public float ConversionFactor => ConversionFactors[CurrentConversion];

	public string UnitType => UnitTypes[CurrentConversion];

	public static void SerializeSetCurrent(ISerializer reader)
	{
		string inst = "";
		string inst2 = "";
		reader.Serialize(ref inst);
		reader.Serialize(ref inst2);
		if (!UnitsHash.TryGetValue(inst, out var value))
		{
			return;
		}
		for (int i = 0; i < value.UnitTypes.Length; i++)
		{
			if (value.UnitTypes[i] == inst2)
			{
				value.PreviousCurrent = value.CurrentConversion;
				value.CurrentConversion = (uint)i;
				if (UpdateUnitsDelegate != null)
				{
					UpdateUnitsDelegate(value);
				}
				break;
			}
		}
	}

	public static Units Serialize(ISerializer serializer)
	{
		Units units = new Units();
		serializer.Serialize(ref units.UnitName);
		serializer.Serialize(ref units.CurrentConversion);
		units.PreviousCurrent = units.CurrentConversion;
		uint inst = 0u;
		serializer.Serialize(ref inst);
		units.UnitTypes = new string[inst];
		units.ConversionFactors = new float[inst];
		for (int i = 0; i < units.UnitTypes.Length; i++)
		{
			string inst2 = string.Empty;
			float inst3 = 0f;
			serializer.Serialize(ref inst2);
			serializer.Serialize(ref inst3);
			units.UnitTypes[i] = inst2;
			units.ConversionFactors[i] = inst3;
		}
		if (UnitsHash.ContainsKey(units.UnitName))
		{
			return UnitsHash[units.UnitName];
		}
		UnitsHash[units.UnitName] = units;
		return units;
	}
}
