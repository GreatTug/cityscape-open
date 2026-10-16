using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
public class ListView : ItemView
{
	public enum ViewMode
	{
		eViewSmallIcon,
		eViewLargeIcon,
		eViewDetails
	}

	public enum MessagesListView
	{
		eSmallIconSize = 182,
		eViewMode = 183,
		eMultiSelect = 184,
		eLastListViewMsg = eMultiSelect
	}

	public delegate void CreateListViewDel(ListView listView);

	public class ViewArgs : EventArgs
	{
		public ViewMode ViewMode;

		public ViewArgs(ViewMode mode)
		{
			ViewMode = mode;
		}
	}

	public class MultiSelectArgs : EventArgs
	{
		public bool Flag;

		public MultiSelectArgs(bool flag)
		{
			Flag = flag;
		}
	}

	public static CreateListViewDel CreateListViewDelegate;

	public event EventHandler<ViewArgs> EventViewMode;

	public event EventHandler<IconSizeArgs> EventSmallIconSize;

	public event EventHandler<MultiSelectArgs> EventMultiSelect;

	public ListView(uint newId)
		: base(newId)
	{
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 183u:
		{
			uint inst2 = 2u;
			serializer.Serialize(ref inst2);
			if (EventViewMode != null)
			{
				EventViewMode(this, new ViewArgs((ViewMode)inst2));
			}
			break;
		}
		case 182u:
		{
			uint inst3 = 0u;
			uint inst4 = 0u;
			serializer.Serialize(ref inst3);
			serializer.Serialize(ref inst4);
			if (EventSmallIconSize != null)
			{
				EventSmallIconSize(this, new IconSizeArgs(inst3, inst4));
			}
			break;
		}
		case 184u:
		{
			bool inst = true;
			serializer.Serialize(ref inst);
			if (EventMultiSelect != null)
			{
				EventMultiSelect(this, new MultiSelectArgs(inst));
			}
			break;
		}
		default:
			base.Serialize(serializer, message);
			break;
		}
	}

	protected override void CreateInstance()
	{
		if (CreateListViewDelegate != null)
		{
			CreateListViewDelegate(this);
		}
	}
}
