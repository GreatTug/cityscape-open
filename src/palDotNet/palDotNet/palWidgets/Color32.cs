using System;
using System.Drawing;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class Color32 : Slider<uint, uint>, IColorSlider
{
	private Color TheColor;

	public override string PropertyValueName => "ColorValue";

	public override int NumPixelsTrigger => 10;

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
				TheValue = (uint)((TheColor.R << 24) | (TheColor.G << 16) | (TheColor.B << 8) | TheColor.A);
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

	public Color32(uint newId)
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
		uint inst = TheValue;
		serializer.Serialize(ref inst);
		if (serializer.Reading && inst != TheValue)
		{
			uint red = (inst & 0xFF000000u) >> 24;
			uint green = (inst & 0xFF0000) >> 16;
			uint blue = (inst & 0xFF00) >> 8;
			uint alpha = inst & 0xFF;
			SerializeUpdate = false;
			ColorValue = Color.FromArgb((int)alpha, (int)red, (int)green, (int)blue);
			SerializeUpdate = true;
		}
	}
}
