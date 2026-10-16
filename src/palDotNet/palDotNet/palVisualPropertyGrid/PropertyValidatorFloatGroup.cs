using System;
using VisualHint.SmartPropertyGrid;

namespace palDotNet.palVisualPropertyGrid;

internal class PropertyValidatorFloatGroup : PropertyValidatorBase
{
	internal PropertyValidatorFloatGroup(PropertyEnumerator propEnum)
		: base(propEnum)
	{
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
		if (!(value is string))
		{
			return false;
		}
		char groupedValueSeparator = base.Enumerator.Property.Value.GroupedValueSeparator;
		string[] array = (value as string).Split(groupedValueSeparator);
		int count = base.Enumerator.Children.Count;
		if (array.Length != count)
		{
			return false;
		}
		PropertyEnumerator children = base.Enumerator.Children;
		int num = 0;
		while (children != children.RightBound)
		{
			string text = array[num++].Trim();
			if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(children.Property.Value.DisplayString))
			{
				return false;
			}
			if (!string.IsNullOrEmpty(text) && !float.TryParse(text, out var _))
			{
				return false;
			}
			children.MoveNext();
		}
		return true;
	}
}
