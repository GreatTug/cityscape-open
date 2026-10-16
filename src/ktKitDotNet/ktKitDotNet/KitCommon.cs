using System.Drawing;

namespace ktKitDotNet;

public class KitCommon
{
	public enum EnumDisplayType
	{
		eComboBox,
		eRadioButtons
	}

	public enum StringDisplayType
	{
		eTextBox,
		eFileLoader
	}

	public enum Units
	{
		eNone,
		eAngle,
		eMetric,
		ePercent
	}

	public enum DataType
	{
		eInteger,
		eFloat,
		eBoolean,
		eEnumeration,
		eColor,
		eString,
		eVector2,
		eVector3,
		eVector4
	}

	public delegate void BoolChangedHandler(bool value);

	public delegate void ColorChangedHandler(Color value);

	public delegate void EnumChangedHandler(int index);

	public delegate void EnumDisplayTypeChangedHandler(EnumDisplayType displayType);

	public delegate void EnumListChangedHandler(string[] enumList);

	public delegate void FloatChangedHandler(float x);

	public delegate void Vector2ChangedHandler(float x, float y);

	public delegate void Vector3ChangedHandler(float x, float y, float z);

	public delegate void Vector4ChangedHandler(float x, float y, float z, float w);

	public delegate void UnitsChangedHandler(Units unit);

	public delegate void IntChangedHandler(int i);

	public delegate void StringChangedHandler(string value);

	public delegate void StringDisplayChangedHandler(StringDisplayType display);
}
