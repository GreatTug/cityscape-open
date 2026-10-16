using System;
using VisualHint.SmartPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public class WidgetComboBoxInt : Widget<string>
{
	public override string BindName => "StringValue";

	public WidgetComboBoxInt(WidgetBase widget, PropertyGrid grid, PropertyEnumerator gridEnum, Attribute[] attrs)
		: base(widget, grid, gridEnum, attrs)
	{
	}

	public override void SetValue(WidgetBase widget)
	{
		ComboBoxInt theWidget = widget as ComboBoxInt;
		TheWidget = theWidget;
		TheValue = string.Empty;
	}
}
