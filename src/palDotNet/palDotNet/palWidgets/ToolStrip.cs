using System;
using System.Collections.Generic;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
public class ToolStrip(uint newId) : BankBase(newId), ISupportsMenus
{
	public enum EnumPosition
	{
		eTop,
		eBottom,
		eLeft,
		eRight
	}

	public enum EnumLayoutStyle
	{
		eFlow,
		eHorizontalStackWithOverflow,
		eStackWithOverflow,
		eTable,
		eVerticalStackWithOverflow
	}

	public delegate void AddMenuItemDel(WidgetBase data, object visual);

	public AddMenuItemDel AddMenuItemDelegate;

	private Dictionary<ISupportsMenus, IMenuItem> WidgetToVisual = new Dictionary<ISupportsMenus, IMenuItem>();

	public EnumPosition Position { get; private set; }

	public uint IconSize { get; private set; }

	public EnumLayoutStyle LayoutStyle { get; private set; }

	public static event EventHandler ToolStripCreate;

	public override object MapMenuToVisual(ISupportsMenus menu)
	{
		IMenuItem value = null;
		WidgetToVisual.TryGetValue(menu, out value);
		return value?.MenuItem;
	}

	public override void AddMapMenuToVisual(ISupportsMenus menu, IMenuItem visual)
	{
		WidgetToVisual.Add(menu, visual);
		if (menu is WidgetBase data && AddMenuItemDelegate != null)
		{
			AddMenuItemDelegate(data, visual.MenuItem);
		}
	}

	public override bool RemoveMapMenuToVisual(ISupportsMenus menu)
	{
		return WidgetToVisual.Remove(menu);
	}

	public override void Serialize(ISerializer serializer, uint msgType)
	{
		switch (msgType)
		{
		case 137u:
		{
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			uint inst = 0u;
			serializer.Serialize(ref inst);
			Position = (EnumPosition)inst;
			uint inst2 = 0u;
			serializer.Serialize(ref inst2);
			IconSize = inst2;
			uint inst3 = 0u;
			serializer.Serialize(ref inst3);
			LayoutStyle = (EnumLayoutStyle)inst3;
			if (serializer.Reading && ToolStripCreate != null)
			{
				ToolStripCreate(this, EventArgs.Empty);
			}
			break;
		}
		default:
			base.Serialize(serializer, msgType);
			break;
		case 136u:
			break;
		}
	}
}
