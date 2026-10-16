using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
public class LibraryView : WidgetPanel
{
	public enum MessagesLibraryView
	{
		eVisibleTreeView = 168,
		eVisibleListView = 169,
		eVisiblePropertyGridView = 170,
		eVisibleRenderWindow = 171,
		eLastLibraryViewMsg = eVisibleRenderWindow
	}

	public ListView ListView { get; private set; }

	public RenderWindow RenderWindow { get; private set; }

	public TreeView TreeView { get; private set; }

	public PropertyGridView PropertyGridView { get; private set; }

	public ILibraryControl LibraryControl { private get; set; }

	public LibraryView(uint newId)
		: base(newId)
	{
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 168u:
		{
			bool inst4 = true;
			serializer.Serialize(ref inst4);
			if (LibraryControl != null)
			{
				LibraryControl.VisibleTreeView = inst4;
			}
			break;
		}
		case 169u:
		{
			bool inst2 = true;
			serializer.Serialize(ref inst2);
			if (LibraryControl != null)
			{
				LibraryControl.VisibleListView = inst2;
			}
			break;
		}
		case 170u:
		{
			bool inst3 = true;
			serializer.Serialize(ref inst3);
			if (LibraryControl != null)
			{
				LibraryControl.VisiblePropertyGridView = inst3;
			}
			break;
		}
		case 171u:
		{
			bool inst = true;
			serializer.Serialize(ref inst);
			if (LibraryControl != null)
			{
				LibraryControl.VisibleRenderWindow = inst;
			}
			break;
		}
		default:
			base.Serialize(serializer, message);
			break;
		}
	}

	public override void AddWidget(WidgetBase widget)
	{
		if (widget is ListView)
		{
			if (ListView != null)
			{
				throw new ApplicationException("'" + base.Name + "' already has a listview !");
			}
			ListView = widget as ListView;
			if (LibraryControl != null)
			{
				LibraryControl.OnListViewAdded(this, null);
			}
		}
		else if (widget is RenderWindow)
		{
			if (RenderWindow != null)
			{
				throw new ApplicationException("'" + base.Name + "' already has a renderwindow !");
			}
			RenderWindow = widget as RenderWindow;
			if (LibraryControl != null)
			{
				LibraryControl.OnRenderWindowAdded(this, null);
			}
		}
		else if (widget is TreeView)
		{
			if (TreeView != null)
			{
				throw new ApplicationException("'" + base.Name + "' already has a treeview !");
			}
			TreeView = widget as TreeView;
			if (LibraryControl != null)
			{
				LibraryControl.OnTreeViewAdded(this, null);
			}
		}
		else if (widget is PropertyGridView)
		{
			if (PropertyGridView != null)
			{
				throw new ApplicationException("'" + base.Name + "' already has a widget bank !");
			}
			PropertyGridView = widget as PropertyGridView;
			if (LibraryControl != null)
			{
				LibraryControl.OnPropertyGridViewAdded(this, null);
			}
		}
		else
		{
			base.AddWidget(widget);
		}
	}

	public override bool RemoveWidget(WidgetBase widget)
	{
		if (widget is ListView)
		{
			ListView = null;
		}
		else if (widget is RenderWindow)
		{
			RenderWindow = null;
		}
		else if (widget is TreeView)
		{
			TreeView = null;
		}
		else if (widget is PropertyGridView)
		{
			PropertyGridView = null;
		}
		else
		{
			base.RemoveWidget(widget);
		}
		widget.Parent = null;
		return true;
	}
}
