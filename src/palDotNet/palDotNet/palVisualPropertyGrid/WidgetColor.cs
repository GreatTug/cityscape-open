using System;
using System.Drawing;
using VisualHint.SmartPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

internal class WidgetColor : WidgetBase<Color>
{
	private bool LastVisible;

	public override string BindName => "ColorValue";

	public WidgetColor(WidgetBase widget, PropertyGrid grid, PropertyEnumerator gridEnum, Attribute[] attrs)
		: base(widget, grid, gridEnum, attrs)
	{
		widget.WidgetValueChanged += widget_WidgetValueChanged;
	}

	public override PropertyEnumerator AppendProperty(PropertyGrid grid, PropertyEnumerator gridEnum, int id, string name, string memberName, string comment, Attribute[] attrs)
	{
		PropertyEnumerator propertyEnumerator = base.AppendProperty(grid, gridEnum, id, name, memberName, comment, attrs);
		propertyEnumerator.Property.Feel = grid.GetRegisteredFeel("list");
		propertyEnumerator.Property.Value.Look = new PropertyColorLook();
		LastVisible = TheWidget.ValueVisible;
		return propertyEnumerator;
	}

	public override void SetValue(WidgetBase widget)
	{
		IColorSlider colorSlider = widget as IColorSlider;
		TheValue = colorSlider.ColorValue;
		TheWidget = widget;
	}

	public override bool ValuesDifferent(Color newValue)
	{
		return !TheValue.Equals(newValue);
	}

	private void widget_WidgetValueChanged(object sender, EventArgs e)
	{
		if (LastVisible != TheWidget.ValueVisible)
		{
			base.PropEnum.Property.InvalidateProperty(children: true);
			LastVisible = TheWidget.ValueVisible;
		}
	}
}
