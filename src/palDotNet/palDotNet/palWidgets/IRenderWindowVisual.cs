namespace palDotNet.palWidgets;

public interface IRenderWindowVisual
{
	Cursors.EnumCursor CurrentCursor { set; }

	void SetVisible(bool isHorizontal, bool visible);

	void SetValue(bool isHorizontal, int barValue);

	void SetLimits(bool isHorizontal, int min, int max);

	void SetLargeChange(bool isHoriztonal, int value);

	void SetSmallChange(bool isHoriztonal, int value);

	int GetValue(bool isHorizontal);
}
