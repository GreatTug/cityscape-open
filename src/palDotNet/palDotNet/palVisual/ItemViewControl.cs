using System;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class ItemViewControl : DragDropControl, ISupportsToolStrip
{
	private ContainerControl TheTopToolStripContainer;

	private ContainerControl TheBottomToolStripContainer;

	private ContainerControl TheLeftToolStripContainer;

	private ContainerControl TheRightToolStripContainer;

	private IItemViewItem LastDragItem;

	private IItemViewItem LastItem;

	private Rectangle DragBoxFromMouseDown;

	public ContainerControl TopToolStripContainer
	{
		get
		{
			return TheTopToolStripContainer;
		}
		set
		{
			TheTopToolStripContainer = value;
		}
	}

	public ContainerControl BottomToolStripContainer
	{
		get
		{
			return TheBottomToolStripContainer;
		}
		set
		{
			TheBottomToolStripContainer = value;
		}
	}

	public ContainerControl LeftToolStripContainer
	{
		get
		{
			return TheLeftToolStripContainer;
		}
		set
		{
			TheLeftToolStripContainer = value;
		}
	}

	public ContainerControl RightToolStripContainer
	{
		get
		{
			return TheRightToolStripContainer;
		}
		set
		{
			TheRightToolStripContainer = value;
		}
	}

	protected virtual ItemView ItemView
	{
		get
		{
			throw new NotImplementedException("Derived classes must override this function");
		}
	}

	protected void Event_MouseDown(object sender, MouseEventArgs e)
	{
		DragBoxFromMouseDown = Rectangle.Empty;
		DragDropData = null;
		if (e.Button == MouseButtons.Left && ItemView.AllowDragAndDrop)
		{
			IItemViewItem itemAt = GetItemAt(Control.MousePosition);
			if (itemAt != null)
			{
				Size dragSize = SystemInformation.DragSize;
				DragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
			}
		}
	}

	protected void Event_MouseMove(object sender, MouseEventArgs e)
	{
		if ((e.Button & MouseButtons.Left) == MouseButtons.Left && DragBoxFromMouseDown != Rectangle.Empty && !DragBoxFromMouseDown.Contains(e.X, e.Y) && ItemView.AllowDragAndDrop)
		{
			IItemViewItem[] selectedItems = GetSelectedItems();
			if (selectedItems != null && selectedItems.Length > 0)
			{
				DragDropData = new DragDropData();
				DragDropData.From = ItemView;
				DragDropData.FromTexts = new string[selectedItems.Length];
				int num = 0;
				IItemViewItem[] array = selectedItems;
				foreach (IItemViewItem itemViewItem in array)
				{
					DragDropData.FromTexts[num++] = itemViewItem.DragString;
				}
				DragDropData.FromType = selectedItems[0].GetType().ToString();
				StartDragging();
			}
			DragBoxFromMouseDown = Rectangle.Empty;
		}
		DragDropData = null;
	}

	protected void Event_MouseUp(object sender, MouseEventArgs e)
	{
		DragBoxFromMouseDown = Rectangle.Empty;
	}

	protected virtual void Event_DragEnter(object sender, DragEventArgs drgevent)
	{
		DragDropControl_DragEnter(sender, drgevent);
		if (LastItem != null)
		{
			LastItem.BackColor = SystemColors.Window;
		}
		LastItem = null;
		LastDragItem = GetItemAt(Control.MousePosition);
	}

	protected virtual void Event_DragLeave(object sender, EventArgs e)
	{
		if (LastItem != null)
		{
			LastItem.BackColor = SystemColors.Window;
		}
		LastItem = null;
	}

	protected virtual void Event_DragOver(object sender, DragEventArgs drgevent)
	{
		IItemViewItem itemAt = GetItemAt(Control.MousePosition);
		HandleDragOver(LastDragItem != itemAt, drgevent);
		LastDragItem = itemAt;
	}

	protected void AddEvents(Control control)
	{
		control.MouseDown += Event_MouseDown;
		control.MouseMove += Event_MouseMove;
		control.MouseUp += Event_MouseUp;
	}

	protected virtual IItemViewItem GetItemAt(Point point)
	{
		throw new NotImplementedException("Derived classes must override this function");
	}

	protected virtual IItemViewItem[] GetSelectedItems()
	{
		throw new NotImplementedException("Derived classes must override this function");
	}

	protected override void DragDropHelper(bool dragFinished, DragEventArgs e)
	{
		string toItemText = string.Empty;
		IItemViewItem itemAt = GetItemAt(new Point(e.X, e.Y));
		if (itemAt != null)
		{
			toItemText = itemAt.DragString;
		}
		if (LastItem != itemAt && LastItem != null)
		{
			LastItem.BackColor = SystemColors.Window;
		}
		if (DragDropSendMessage(ItemView, e, dragFinished, LastItem != itemAt, toItemText))
		{
			if (e.Effect == DragDropEffects.Move && itemAt != null)
			{
				itemAt.BackColor = SystemColors.ControlLight;
			}
			LastItem = itemAt;
		}
		if (LastItem != null && dragFinished)
		{
			LastItem.BackColor = SystemColors.Window;
		}
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		this.AllowDrop = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "ItemViewControl";
		base.ResumeLayout(false);
	}

	void ISupportsToolStrip.SuspendLayout()
	{
		SuspendLayout();
	}

	void ISupportsToolStrip.ResumeLayout()
	{
		ResumeLayout();
	}
}
