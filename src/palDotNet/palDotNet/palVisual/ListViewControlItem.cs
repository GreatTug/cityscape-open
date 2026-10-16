using System.Windows.Forms;

namespace palDotNet.palVisual;

public class ListViewControlItem : ListViewItem, IItemViewItem
{
	private bool TheAllowLabelEdit;

	private int TheSortOrder;

	public bool AllowLabelEdit
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

	public string DragString => base.Text;

	public int SortOrder
	{
		get
		{
			return TheSortOrder;
		}
		set
		{
			TheSortOrder = value;
		}
	}

	public ListViewControlItem(string text, bool allowLabelEdit)
		: base(text)
	{
		AllowLabelEdit = allowLabelEdit;
	}
}
