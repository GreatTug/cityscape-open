using System;
using System.Collections.Generic;

namespace palDotNet.palWidgets;

[WidgetCreator]
public class PropertyGridView(uint newId) : WidgetPanel(newId), ISupportsMenus
{
	private Dictionary<ISupportsMenus, IMenuItem> WidgetToVisual = new Dictionary<ISupportsMenus, IMenuItem>();

	public Bank WidgetBank { get; private set; }

	public IPropertyGridControl PropertyGridControl { private get; set; }

	public override void AddWidget(WidgetBase widget)
	{
		if (widget is Bank)
		{
			if (WidgetBank != null)
			{
				throw new ApplicationException("'" + base.Name + "' already has a widget bank !");
			}
			WidgetBank = widget as Bank;
			if (PropertyGridControl != null)
			{
				PropertyGridControl.OnWidgetBankAdded(this, null);
			}
		}
	}

	public override bool RemoveWidget(WidgetBase widget)
	{
		if (widget is Bank)
		{
			WidgetBank = null;
		}
		else
		{
			base.RemoveWidget(widget);
		}
		widget.Parent = null;
		return true;
	}

	public override object MapMenuToVisual(ISupportsMenus menu)
	{
		IMenuItem value = null;
		WidgetToVisual.TryGetValue(menu, out value);
		return value?.MenuItem;
	}

	public override void AddMapMenuToVisual(ISupportsMenus menu, IMenuItem visual)
	{
		WidgetToVisual.Add(menu, visual);
	}

	public override bool RemoveMapMenuToVisual(ISupportsMenus menu)
	{
		return WidgetToVisual.Remove(menu);
	}
}
