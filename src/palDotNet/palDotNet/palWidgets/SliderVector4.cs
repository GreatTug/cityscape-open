using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class SliderVector4 : Slider<Vector4, float>
{
	public float x
	{
		get
		{
			return WidgetValue.x;
		}
		set
		{
			WidgetValue = new Vector4(value, WidgetValue.y, WidgetValue.z, WidgetValue.w);
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
			WidgetValue = new Vector4(WidgetValue.x, value, WidgetValue.z, WidgetValue.w);
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
			WidgetValue = new Vector4(WidgetValue.x, WidgetValue.y, value, WidgetValue.w);
		}
	}

	public float w
	{
		get
		{
			return WidgetValue.w;
		}
		set
		{
			WidgetValue = new Vector4(WidgetValue.x, WidgetValue.y, WidgetValue.z, value);
		}
	}

	public SliderVector4(uint newId)
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
		Vector4 inst = WidgetValue;
		serializer.Serialize(ref inst);
		if (serializer.Reading)
		{
			float theX = (float)Math.Round(inst.x, SliderEnums.NumRoundingDecimals);
			float theY = (float)Math.Round(inst.y, SliderEnums.NumRoundingDecimals);
			float theZ = (float)Math.Round(inst.z, SliderEnums.NumRoundingDecimals);
			float theW = (float)Math.Round(inst.w, SliderEnums.NumRoundingDecimals);
			inst = new Vector4(theX, theY, theZ, theW);
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

	public SliderEnums.PointModes AddIncrementW(float incr)
	{
		float num = w + incr * Delta;
		num = (float)Math.Round(num, SliderEnums.NumRoundingDecimals);
		num = Math.Max(TheMin, num);
		num = Math.Min(TheMax, num);
		w = num;
		return SliderEnums.PointModes.eFloat;
	}

	public override string ProcessUpDown(bool up, string value, string element)
	{
		float val = float.Parse(value) + (up ? Delta : (0f - Delta));
		val = Math.Max(Min, val);
		return Math.Min(Max, val).ToString();
	}
}
