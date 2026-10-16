using System;
using System.Windows.Forms;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator(Aliases = new string[] { "SliderInt", "SliderChar" })]
internal class SliderInt : Slider<int, int>
{
	public override int NumPixelsTrigger => 10;

	public SliderInt(uint newId)
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
		int inst = WidgetValue;
		serializer.Serialize(ref inst);
		WidgetValueNoSerialize = inst;
	}

	public override string ProcessUpDown(bool up, string value, string element)
	{
		int num = int.Parse(value);
		num = Math.Max(val2: (!up) ? (num - Delta) : (num + Delta), val1: Min);
		return Math.Min(Max, num).ToString();
	}

	public override SliderEnums.PointModes AddIncrement(float incr)
	{
		int num = (int)(incr * (float)TheDelta);
		if (num != 0)
		{
			int val = WidgetValue + num;
			val = Math.Max(TheMin, val);
			val = Math.Min(TheMax, val);
			WidgetValue = val;
			return SliderEnums.PointModes.eInt;
		}
		return SliderEnums.PointModes.eNoIncrement;
	}

	public override void InitNumericUpDown(NumericUpDown upDown)
	{
		upDown.Minimum = TheMin;
		upDown.Maximum = TheMax;
		upDown.Increment = TheDelta;
		upDown.Value = WidgetValue;
	}
}
