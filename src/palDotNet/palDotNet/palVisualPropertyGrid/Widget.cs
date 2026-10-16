using System;
using VisualHint.SmartPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public class Widget<_ValueType> : WidgetBase<_ValueType> where _ValueType : IEquatable<_ValueType>
{
	public Widget(WidgetBase widget, PropertyGrid grid, PropertyEnumerator gridEnum, Attribute[] attrs)
		: base(widget, grid, gridEnum, attrs)
	{
	}

	public override void SetValue(WidgetBase widget)
	{
		palDotNet.palWidgets.Widget<_ValueType> widget2 = widget as palDotNet.palWidgets.Widget<_ValueType>;
		TheValue = widget2.WidgetValue;
		TheWidget = widget2;
	}

	public override bool ValuesDifferent(_ValueType newValue)
	{
		return !TheValue.Equals(newValue);
	}
}
