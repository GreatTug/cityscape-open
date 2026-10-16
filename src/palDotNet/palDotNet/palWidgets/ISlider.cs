using System;
using System.Windows.Forms;

namespace palDotNet.palWidgets;

internal interface ISlider
{
	int NumPixelsTrigger { get; }

	bool AlwaysFlushEditing { get; set; }

	Type ValueType { get; }

	SliderEnums.PointModes AddIncrement(float incr);

	void InitNumericUpDown(NumericUpDown upDown);

	void GetMinMax(out IComparable min, out IComparable max);

	string ProcessUpDown(bool up, string value, string element);
}
