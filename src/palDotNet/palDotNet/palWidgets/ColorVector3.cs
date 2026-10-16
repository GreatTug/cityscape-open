using System;
using System.Drawing;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class ColorVector3 : SliderVector3, IColorSlider
{
	private Color TheColor;

	public override string PropertyValueName => "ColorValue";

	public override object WidgetValueObject => ColorValue;

	public Color ColorValue
	{
		get
		{
			return TheColor;
		}
		set
		{
			bool flag = TheColor == value;
			if (!flag)
			{
				TheColor = value;
				TheValue.x = (float)(int)TheColor.R / 255f;
				TheValue.y = (float)(int)TheColor.G / 255f;
				TheValue.z = (float)(int)TheColor.B / 255f;
				OnWidgetValueChanged(EventArgs.Empty);
			}
			CheckValueVisible(flag);
		}
	}

	public override bool AlwaysFlushEditing
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public ColorVector3(uint newId)
		: base(newId)
	{
	}

	protected override void SerializeValue(ISerializer serializer)
	{
		Vector3 inst = WidgetValue;
		serializer.Serialize(ref inst);
		if (!serializer.Reading)
		{
			return;
		}
		bool flag = inst.Equals(TheValue);
		if (!flag)
		{
			if (inst.x >= 0f && inst.y >= 0f && inst.z >= 0f && inst.x <= 1f && inst.y <= 1f && inst.z <= 1f)
			{
				SerializeUpdate = false;
				ColorValue = Color.FromArgb(255, (int)(inst.x * 255f), (int)(inst.y * 255f), (int)(inst.z * 255f));
				SerializeUpdate = true;
			}
			else
			{
				base.ValueVisible = false;
				SerializeUpdate = false;
				OnWidgetValueChanged(EventArgs.Empty);
			}
		}
		CheckValueVisible(flag);
	}
}
