using System.Collections.Generic;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

public class BankBase(uint newId) : Widget<bool>(newId)
{
	protected enum Messages
	{
		eAddVisible = 161,
		eRemoveVisible = 162,
		eSuspendLayout = 163,
		eResumeLayout = 164,
		eLastMsg = eResumeLayout
	}

	private List<WidgetBase> TheChildren = new List<WidgetBase>();

	private bool LayoutSuspended;

	public override List<WidgetBase> Children => TheChildren;

	public bool IsLayoutSuspended => LayoutSuspended;

	public override void Serialize(ISerializer serializer, uint msgType)
	{
		switch (msgType)
		{
		case 163u:
			LayoutSuspended = true;
			OnEventSuspendLayout();
			break;
		case 164u:
			LayoutSuspended = false;
			OnEventResumeLayout();
			break;
		default:
			base.Serialize(serializer, msgType);
			break;
		}
	}

	public override void Destroy()
	{
		while (TheChildren.Count > 0)
		{
			TheChildren[0].Destroy();
		}
		TheChildren.Clear();
		base.Destroy();
	}

	public override void AddWidget(WidgetBase widget)
	{
		TheChildren.Add(widget);
	}

	public override bool RemoveWidget(WidgetBase widget)
	{
		bool result = TheChildren.Remove(widget);
		widget.Parent = null;
		return result;
	}
}
