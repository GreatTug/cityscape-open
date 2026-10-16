using System;

namespace palDotNet.palWidgets;

public abstract class Widget<_ValueType> : WidgetBase where _ValueType : IEquatable<_ValueType>
{
	protected _ValueType TheValue;

	public override object WidgetValueObject => WidgetValue;

	public virtual _ValueType WidgetValue
	{
		get
		{
			return TheValue;
		}
		set
		{
			bool flag = TheValue.Equals(value);
			if (!flag)
			{
				TheValue = value;
				OnWidgetValueChanged(EventArgs.Empty);
			}
			CheckValueVisible(flag);
		}
	}

	public virtual _ValueType WidgetValueNoSerialize
	{
		set
		{
			if (!TheValue.Equals(value))
			{
				TheValue = value;
				SerializeUpdate = false;
				OnWidgetValueChanged(EventArgs.Empty);
				SerializeUpdate = true;
			}
		}
	}

	public Widget(uint newId)
		: base(newId)
	{
	}
}
