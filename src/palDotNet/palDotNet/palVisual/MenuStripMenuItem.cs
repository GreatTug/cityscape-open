using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class MenuStripMenuItem : IMenuItem
{
	private ToolStripItem ToolStripItem;

	public object MenuItem => ToolStripItem;

	public MenuStripMenuItem(ToolStripItem item)
	{
		ToolStripItem = item;
	}

	public void Show()
	{
		ToolStripMenuItem item = ToolStripItem as ToolStripMenuItem;
		if (item == null)
		{
			return;
		}
		int count = item.DropDownItems.Count;
		if (count > 0)
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			ToolStripItem[] array = new ToolStripItem[count];
			item.DropDownItems.CopyTo(array, 0);
			contextMenuStrip.Items.AddRange(array);
			contextMenuStrip.Closed += delegate
			{
				item.DropDownItems.Clear();
				item.DropDownItems.AddRange(array);
			};
			contextMenuStrip.Show(FormAppBase.Instance, FormAppBase.Instance.PointToClient(Control.MousePosition));
		}
	}
}
