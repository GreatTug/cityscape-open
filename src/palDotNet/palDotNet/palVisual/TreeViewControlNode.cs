using System.Windows.Forms;

namespace palDotNet.palVisual;

internal class TreeViewControlNode : TreeNode, IItemViewItem
{
	private bool TheAllowLabelEdit;

	internal bool AllowLabelEdit
	{
		get
		{
			return TheAllowLabelEdit;
		}
		set
		{
			TheAllowLabelEdit = value;
		}
	}

	public string DragString => base.FullPath;

	internal TreeViewControlNode(string text, bool allowLabelEdit)
		: base(text)
	{
		AllowLabelEdit = allowLabelEdit;
	}
}
