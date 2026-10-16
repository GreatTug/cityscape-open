using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

internal class GridVector3
{
	private SliderVector3 TheWidget;

	public Vector3 WidgetValue
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
			TheWidget.WidgetValue = new Vector3(value, TheWidget.WidgetValue.y, TheWidget.WidgetValue.z);
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
			TheWidget.WidgetValue = new Vector3(TheWidget.WidgetValue.x, value, TheWidget.WidgetValue.z);
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
			TheWidget.WidgetValue = new Vector3(TheWidget.WidgetValue.x, TheWidget.WidgetValue.y, value);
		}
	}

	public GridVector3(SliderVector3 theWidget)
	{
		TheWidget = theWidget;
	}
}
