using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
public class TreeView(uint newId) : ItemView(newId)
{
	public enum MessagesTreeView
	{
		ePathSeparatorChar = 182,
		eExpandAll = 183,
		eCollapseAll = 184,
		eLastTreeViewMsg = eCollapseAll
	}

	public delegate void CreateTreeViewDel(TreeView treeView);

	public delegate void ExpandOrCollapseDel(bool expand);

	public static CreateTreeViewDel CreateTreeViewDelegate;

	public ExpandOrCollapseDel ExpandOrCollapseDelegate;

	private char ThePathSeparator = '/';

	public char PathSeparator => ThePathSeparator;

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 182u:
			serializer.Serialize(ref ThePathSeparator);
			break;
		case 183u:
			if (ExpandOrCollapseDelegate != null)
			{
				ExpandOrCollapseDelegate(expand: true);
			}
			break;
		case 184u:
			if (ExpandOrCollapseDelegate != null)
			{
				ExpandOrCollapseDelegate(expand: false);
			}
			break;
		default:
			base.Serialize(serializer, message);
			break;
		}
	}

	protected override void CreateInstance()
	{
		if (CreateTreeViewDelegate != null)
		{
			CreateTreeViewDelegate(this);
		}
	}
}
