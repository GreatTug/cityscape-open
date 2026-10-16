using System;
using System.Windows.Forms;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

internal class Slider<_ValueType, _LimitType> : Widget<_ValueType>, ISlider where _ValueType : struct, IEquatable<_ValueType> where _LimitType : struct, IEquatable<_LimitType>, IComparable
{
	protected enum MessagesSlider
	{
		eLastSliderMsg = 160
	}

	protected _LimitType TheMin;

	protected _LimitType TheMax;

	protected _LimitType TheDelta;

	private bool TheSendEditingMessages;

	private static bool AlwaysFlushEditingFlag = true;

	public virtual _LimitType Min => TheMin;

	public virtual _LimitType Max => TheMax;

	public virtual _LimitType Delta => TheDelta;

	public virtual int NumPixelsTrigger => 3;

	public override bool AlwaysFlushEditing
	{
		get
		{
			return AlwaysFlushEditingFlag;
		}
		set
		{
			AlwaysFlushEditingFlag = value;
		}
	}

	public override bool SendEditingMessages => TheSendEditingMessages;

	public Type ValueType => typeof(_ValueType);

	public Slider(uint newId)
		: base(newId)
	{
	}

	protected virtual void SerializeLimits(ISerializer serializer)
	{
		throw new NotImplementedException();
	}

	protected virtual void SerializeValue(ISerializer serializer)
	{
		throw new NotImplementedException();
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			serializer.Serialize(ref TheSendEditingMessages);
			SerializeValue(serializer);
			SerializeLimits(serializer);
			if (serializer.Reading)
			{
				OnWidgetCreate();
			}
			break;
		case 147u:
			SerializeValue(serializer);
			break;
		default:
			base.Serialize(serializer, message);
			break;
		case 136u:
			break;
		}
	}

	public void GetMinMax(out IComparable min, out IComparable max)
	{
		min = Min;
		max = Max;
	}

	public virtual SliderEnums.PointModes AddIncrement(float incr)
	{
		throw new NotImplementedException();
	}

	public virtual void InitNumericUpDown(NumericUpDown upDown)
	{
		throw new NotImplementedException();
	}

	public virtual string ProcessUpDown(bool up, string value, string element)
	{
		throw new NotImplementedException("Needs to be implemented in derived classes\n");
	}
}
