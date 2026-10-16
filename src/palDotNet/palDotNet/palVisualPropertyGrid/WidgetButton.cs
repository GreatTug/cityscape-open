using System;
using VisualHint.SmartPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public class WidgetButton : Widget<bool>
{
	public WidgetButton(WidgetBase widget, PropertyGrid grid, PropertyEnumerator gridEnum, Attribute[] attrs)
		: base(widget, grid, gridEnum, attrs)
	{
	}

	public override PropertyEnumerator AppendProperty(PropertyGrid grid, PropertyEnumerator gridEnum, int id, string name, string memberName, string comment, Attribute[] attrs)
	{
		return grid.AppendButtonProperty(gridEnum, id, name, comment);
	}

	public override void SupportMultipleValues(PropertyGrid grid, PropertyEnumerator propEnum)
	{
	}
}
