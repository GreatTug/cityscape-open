using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class SliderFloat(uint newId) : Slider<float, float>(newId)
{
	private enum MessagesSliderFloat
	{
		eUnitsSet = 161,
		eUnitsClear = 162,
		eLastSliderFloatMsg = eUnitsClear
	}

	public delegate void SetUnitsDel(SliderFloat slider, bool clear);

	public SetUnitsDel SetUnitsDelegate;

	private float PreviousConversionFactor = 1f;

	private float FullPrecisionValue;

	public override float Min => TheMin * ConversionFactor;

	public override float Max => TheMax * ConversionFactor;

	public override float Delta => TheDelta * ConversionFactor;

	public float ConversionFactor
	{
		get
		{
			if (Units == null)
			{
				return 1f;
			}
			return Units.ConversionFactor;
		}
	}

	public string UnitType
	{
		get
		{
			if (!base.ValueVisible || Units == null)
			{
				return "";
			}
			return Units.UnitType;
		}
	}

	public Units Units { get; private set; }

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 161u:
		case 162u:
		{
			bool flag = message == 162;
			float num = 1f;
			if (flag)
			{
				Units = null;
			}
			else
			{
				Units = Units.Serialize(serializer);
				num = Units.ConversionFactor;
			}
			if (SetUnitsDelegate != null)
			{
				SetUnitsDelegate(this, flag);
			}
			bool valueVisible = base.ValueVisible;
			SerializeUpdate = false;
			FullPrecisionValue = FullPrecisionValue / PreviousConversionFactor * num;
			WidgetValue = (float)Math.Round(FullPrecisionValue, SliderEnums.NumRoundingDecimals);
			SerializeUpdate = true;
			PreviousConversionFactor = num;
			base.ValueVisible = valueVisible;
			break;
		}
		default:
			base.Serialize(serializer, message);
			break;
		}
	}

	protected override void SerializeLimits(ISerializer serializer)
	{
		serializer.Serialize(ref TheMin);
		serializer.Serialize(ref TheMax);
		serializer.Serialize(ref TheDelta);
	}

	protected override void SerializeValue(ISerializer serializer)
	{
		if (serializer.Writing)
		{
			FullPrecisionValue = WidgetValue;
			float inst = FullPrecisionValue / ConversionFactor;
			serializer.Serialize(ref inst);
		}
		else if (serializer.Reading)
		{
			serializer.Serialize(ref FullPrecisionValue);
			FullPrecisionValue *= ConversionFactor;
			float widgetValueNoSerialize = (float)Math.Round(FullPrecisionValue, SliderEnums.NumRoundingDecimals);
			WidgetValueNoSerialize = widgetValueNoSerialize;
		}
	}

	public override string ProcessUpDown(bool up, string value, string element)
	{
		float num = float.Parse(value);
		num = Math.Max(val2: (!up) ? (num - Delta) : (num + Delta), val1: Min);
		num = (FullPrecisionValue = Math.Min(Max, num));
		return num.ToString();
	}

	public override void UpdateUnits(Units units)
	{
		if (Units != null && units.UnitName == Units.UnitName && base.ValueVisible)
		{
			FullPrecisionValue /= PreviousConversionFactor;
			FullPrecisionValue *= units.ConversionFactor;
			WidgetValueNoSerialize = (float)Math.Round(FullPrecisionValue, SliderEnums.NumRoundingDecimals);
			PreviousConversionFactor = units.ConversionFactor;
		}
	}

	public override SliderEnums.PointModes AddIncrement(float incr)
	{
		float num = WidgetValue + incr * Delta;
		num = (float)Math.Round(num, SliderEnums.NumRoundingDecimals);
		num = Math.Max(Min, num);
		num = (WidgetValue = Math.Min(Max, num));
		FullPrecisionValue = num;
		return SliderEnums.PointModes.eFloat;
	}
}
