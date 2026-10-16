using System;
using VisualHint.SmartPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public class WidgetMutex : Widget<string>
{
	public override string BindName => "WidgetStringValue";

	public WidgetMutex(WidgetBase widget, PropertyGrid grid, PropertyEnumerator gridEnum, Attribute[] attrs)
		: base(widget, grid, gridEnum, attrs)
	{
	}

	public override void SetValue(WidgetBase widget)
	{
		Mutex theWidget = widget as Mutex;
		TheWidget = theWidget;
		TheValue = " ";
	}
}
