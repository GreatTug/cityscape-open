using System;
using System.ComponentModel;
using VisualHint.SmartPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

internal class PropertyValidatorSlider : PropertyValidatorBase
{
	private ISlider _slider;

	internal PropertyValidatorSlider(PropertyEnumerator propEnum, ISlider slider)
		: base(propEnum)
	{
		_slider = slider;
	}

	internal PropertyValidatorSlider(ISlider slider)
	{
		_slider = slider;
	}

	public override bool Check(object value, bool modify)
	{
		if (value == null)
		{
			return true;
		}
		if (base.Enumerator == null)
		{
			throw new NullReferenceException("The constructor of the validator should have been passed an enumerator on the corresponding property.");
		}
		TypeConverter converter = TypeDescriptor.GetConverter(_slider.ValueType);
		IComparable comparable;
		if (converter.CanConvertFrom(value.GetType()))
		{
			PropertyTypeDescriptorContext typeDescriptorContext = base.Enumerator.Property.Value.GetTypeDescriptorContext(base.Enumerator);
			comparable = (IComparable)converter.ConvertFrom(typeDescriptorContext, base.Enumerator.Property.Value.CultureInfo, value);
		}
		else
		{
			if (_slider.ValueType != value.GetType())
			{
				return false;
			}
			comparable = (IComparable)value;
		}
		bool result = true;
		_slider.GetMinMax(out var min, out var max);
		if (comparable.CompareTo(min) == -1)
		{
			base.Enumerator.Property.Value.SetValue(min);
			base.Enumerator.Property.Value.PreviousValue = base.Enumerator.Property.Value.GetValue();
			result = false;
		}
		else if (comparable.CompareTo(max) == 1)
		{
			base.Enumerator.Property.Value.SetValue(max);
			base.Enumerator.Property.Value.PreviousValue = base.Enumerator.Property.Value.GetValue();
			result = false;
		}
		return result;
	}
}
