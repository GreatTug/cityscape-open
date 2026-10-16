using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class SliderVector3 : Slider<Vector3, float>
{
	public float x
	{
		get
		{
			return WidgetValue.x;
		}
		set
		{
			WidgetValue = new Vector3(value, WidgetValue.y, WidgetValue.z);
		}
	}

	public float y
	{
		get
		{
			return WidgetValue.y;
		}
		set
		{
			WidgetValue = new Vector3(WidgetValue.x, value, WidgetValue.z);
		}
	}

	public float z
	{
		get
		{
			return WidgetValue.z;
		}
		set
		{
			WidgetValue = new Vector3(WidgetValue.x, WidgetValue.y, value);
		}
	}

	public SliderVector3(uint newId)
		: base(newId)
	{
	}

	protected override void SerializeLimits(ISerializer serializer)
	{
		serializer.Serialize(ref TheMin);
		serializer.Serialize(ref TheMax);
		serializer.Serialize(ref TheDelta);
	}

	protected override void SerializeValue(ISerializer serializer)
	{
		Vector3 inst = WidgetValue;
		serializer.Serialize(ref inst);
		if (serializer.Reading)
		{
			float theX = (float)Math.Round(inst.x, SliderEnums.NumRoundingDecimals);
			float theY = (float)Math.Round(inst.y, SliderEnums.NumRoundingDecimals);
			float theZ = (float)Math.Round(inst.z, SliderEnums.NumRoundingDecimals);
			inst = new Vector3(theX, theY, theZ);
		}
		WidgetValueNoSerialize = inst;
	}

	public SliderEnums.PointModes AddIncrementX(float incr)
	{
		float num = x + incr * Delta;
		num = (float)Math.Round(num, SliderEnums.NumRoundingDecimals);
		num = Math.Max(TheMin, num);
		num = Math.Min(TheMax, num);
		x = num;
		return SliderEnums.PointModes.eFloat;
	}

	public SliderEnums.PointModes AddIncrementY(float incr)
	{
		float num = y + incr * Delta;
		num = (float)Math.Round(num, SliderEnums.NumRoundingDecimals);
		num = Math.Max(TheMin, num);
		num = Math.Min(TheMax, num);
		y = num;
		return SliderEnums.PointModes.eFloat;
	}

	public SliderEnums.PointModes AddIncrementZ(float incr)
	{
		float num = z + incr * Delta;
		num = (float)Math.Round(num, SliderEnums.NumRoundingDecimals);
		num = Math.Max(TheMin, num);
		num = Math.Min(TheMax, num);
		z = num;
		return SliderEnums.PointModes.eFloat;
	}

	public override string ProcessUpDown(bool up, string value, string element)
	{
		float val = float.Parse(value) + (up ? Delta : (0f - Delta));
		val = Math.Max(Min, val);
		return Math.Min(Max, val).ToString();
	}
}
