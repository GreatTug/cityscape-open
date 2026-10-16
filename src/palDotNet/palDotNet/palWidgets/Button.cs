using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class Button : Widget<bool>
{
	public enum EnumContentAlignment
	{
		eTopLeft = 1,
		eTopCenter = 2,
		eTopRight = 4,
		eMiddleLeft = 0x10,
		eMiddleCenter = 0x20,
		eMiddleRight = 0x40,
		eBottomLeft = 0x100,
		eBottomCenter = 0x200,
		eBottomRight = 0x400
	}

	private string TheIconPath;

	private bool TheIconOnly;

	public override bool WidgetValue
	{
		get
		{
			return true;
		}
		set
		{
			OnWidgetValueChanged(EventArgs.Empty);
		}
	}

	public string IconPath => TheIconPath;

	public bool IconOnly => TheIconOnly;

	public EnumContentAlignment IconAlignment { get; private set; }

	public Button(uint newId)
		: base(newId)
	{
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
		{
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			serializer.Serialize(ref TheIconPath);
			serializer.Serialize(ref TheIconOnly);
			uint inst = (uint)IconAlignment;
			serializer.Serialize(ref inst);
			if (serializer.Reading)
			{
				IconAlignment = (EnumContentAlignment)inst;
				OnWidgetCreate();
			}
			break;
		}
		default:
			base.Serialize(serializer, message);
			base.ValueVisible = true;
			break;
		case 136u:
		case 147u:
			break;
		}
	}
}
