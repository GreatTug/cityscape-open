using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

internal class GridVector4
{
	private SliderVector4 TheWidget;

	public Vector4 WidgetValue
	{
		get
		{
			return TheWidget.WidgetValue;
		}
		set
		{
			TheWidget.WidgetValue = value;
		}
	}

	public float x
	{
		get
		{
			return TheWidget.WidgetValue.x;
		}
		set
		{
			TheWidget.WidgetValue = new Vector4(value, TheWidget.WidgetValue.y, TheWidget.WidgetValue.z, TheWidget.WidgetValue.w);
		}
	}

	public float y
	{
		get
		{
			return TheWidget.WidgetValue.y;
		}
		set
		{
			TheWidget.WidgetValue = new Vector4(TheWidget.WidgetValue.x, value, TheWidget.WidgetValue.z, TheWidget.WidgetValue.w);
		}
	}

	public float z
	{
		get
		{
			return TheWidget.WidgetValue.z;
		}
		set
		{
			TheWidget.WidgetValue = new Vector4(TheWidget.WidgetValue.x, TheWidget.WidgetValue.y, value, TheWidget.WidgetValue.w);
		}
	}

	public float w
	{
		get
		{
			return TheWidget.WidgetValue.w;
		}
		set
		{
			TheWidget.WidgetValue = new Vector4(TheWidget.WidgetValue.x, TheWidget.WidgetValue.y, TheWidget.WidgetValue.z, value);
		}
	}

	public GridVector4(SliderVector4 theWidget)
	{
		TheWidget = theWidget;
	}
}
