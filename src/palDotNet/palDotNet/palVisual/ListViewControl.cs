using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class ListViewControl : ItemViewControl, IWidgetPanelControl
{
	private class ItemComparer : IComparer
	{
		private int col;

		public ItemComparer()
		{
			col = 0;
		}

		public ItemComparer(int column)
		{
			col = column;
		}

		public int Compare(object x, object y)
		{
			ListViewControlItem listViewControlItem = x as ListViewControlItem;
			ListViewControlItem listViewControlItem2 = y as ListViewControlItem;
			if (listViewControlItem.SortOrder == listViewControlItem2.SortOrder)
			{
				return string.Compare(listViewControlItem.SubItems[col].Text, listViewControlItem2.SubItems[col].Text);
			}
			return listViewControlItem.SortOrder - listViewControlItem2.SortOrder;
		}
	}

	private const int ScrollZone = 12;

	private const float ScrollDelay = 100f;

	private const int WM_VSCROLL = 277;

	private const int SB_LINEUP = 0;

	private const int SB_LINEDOWN = 1;

	private const int LVM_FIRST = 4096;

	private const int WM_PAINT = 15;

	private const int RDW_INVALIDATE = 1;

	private IContainer components;

	private ListViewDerived listView1;

	private ColumnHeader columnHeader1;

	private ImageList imageListLarge;

	private ImageList imageListSmall;

	private ContextMenuStrip rightClickContextMenuStrip;

	private ToolStripMenuItem largeIconsToolStripMenuItem;

	private ToolStripMenuItem smallIconsToolStripMenuItem;

	private ToolStripMenuItem detailsToolStripMenuItem;

	private ToolStripMenuItem renameToolStripMenuItem;

	private bool Dirty;

	private palDotNet.palWidgets.ListView TheListView;

	private bool NeedToSendSelectedItems;

	private ListViewControlSettings TheConfigSettings;

	private static Dictionary<string, Bitmap> IconCache = new Dictionary<string, Bitmap>();

	private DateTime LastScrollTime;

	private static int ListViewDiff = 32;

	[Browsable(false)]
	[ReadOnly(true)]
	public WidgetPanel WidgetPanel
	{
		get
		{
			return TheListView;
		}
		set
		{
			if (TheListView != null)
			{
				throw new ApplicationException("List View has already been set!");
			}
			TheListView = value as palDotNet.palWidgets.ListView;
			TheListView.EventAddItem += AddItem;
			TheListView.EventRemoveItem += RemoveItem;
			TheListView.EventShowContextMenu += ShowContextMenu;
			TheListView.EventSelectItems += SelectItems;
			TheListView.EventGetSelectedItems += GetSelectedItems;
			TheListView.EventViewMode += ViewMode;
			TheListView.EventClearAllItems += ClearAllItems;
			TheListView.EventBeginRenameItem += renameToolStripMenuItem_Click;
			TheListView.EventPauseUpdate += PauseUpdate;
			TheListView.EventIconSize += TheListView_EventIconSize;
			TheListView.EventSmallIconSize += TheListView_EventSmallIconSize;
			TheListView.EventMultiSelect += TheListView_EventMultiSelect;
		}
	}

	protected override ItemView ItemView => TheListView;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
		this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
		this.rightClickContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.largeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.smallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.listView1 = new palDotNet.palVisual.ListViewDerived();
		this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
		this.rightClickContextMenuStrip.SuspendLayout();
		base.SuspendLayout();
		this.imageListLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
		this.imageListLarge.ImageSize = new System.Drawing.Size(32, 32);
		this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
		this.imageListSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
		this.imageListSmall.ImageSize = new System.Drawing.Size(16, 16);
		this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
		this.rightClickContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.renameToolStripMenuItem, this.largeIconsToolStripMenuItem, this.smallIconsToolStripMenuItem, this.detailsToolStripMenuItem });
		this.rightClickContextMenuStrip.Name = "contextMenuStrip1";
		this.rightClickContextMenuStrip.Size = new System.Drawing.Size(135, 92);
		this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
		this.renameToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
		this.renameToolStripMenuItem.Text = "Rena&me";
		this.renameToolStripMenuItem.Click += new System.EventHandler(renameToolStripMenuItem_Click);
		this.largeIconsToolStripMenuItem.CheckOnClick = true;
		this.largeIconsToolStripMenuItem.Name = "largeIconsToolStripMenuItem";
		this.largeIconsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
		this.largeIconsToolStripMenuItem.Text = "&Large Icons";
		this.largeIconsToolStripMenuItem.Click += new System.EventHandler(largeIconsToolStripMenuItem_Click);
		this.smallIconsToolStripMenuItem.CheckOnClick = true;
		this.smallIconsToolStripMenuItem.Name = "smallIconsToolStripMenuItem";
		this.smallIconsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
		this.smallIconsToolStripMenuItem.Text = "&Small Icons";
		this.smallIconsToolStripMenuItem.Click += new System.EventHandler(smallIconsToolStripMenuItem_Click);
		this.detailsToolStripMenuItem.Checked = true;
		this.detailsToolStripMenuItem.CheckOnClick = true;
		this.detailsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
		this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
		this.detailsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
		this.detailsToolStripMenuItem.Text = "&Details";
		this.detailsToolStripMenuItem.Click += new System.EventHandler(detailsToolStripMenuItem_Click);
		this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
		this.listView1.AllowDrop = true;
		this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.columnHeader1 });
		this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.listView1.HideSelection = false;
		this.listView1.LabelEdit = true;
		this.listView1.LargeImageList = this.imageListLarge;
		this.listView1.Location = new System.Drawing.Point(0, 0);
		this.listView1.Name = "listView1";
		this.listView1.ShowGroups = false;
		this.listView1.Size = new System.Drawing.Size(418, 315);
		this.listView1.SmallImageList = this.imageListSmall;
		this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
		this.listView1.TabIndex = 0;
		this.listView1.UseCompatibleStateImageBehavior = false;
		this.listView1.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(listView1_AfterLabelEdit);
		this.listView1.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
		this.listView1.DoubleClick += new System.EventHandler(listView1_DoubleClick);
		this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(listView1_MouseUp);
		this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(listView1_KeyUp);
		this.listView1.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(listView1_BeforeLabelEdit);
		this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(listView1_KeyDown);
		this.listView1.Click += new System.EventHandler(listView1_Click);
		this.columnHeader1.Text = "Name";
		this.AllowDrop = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.listView1);
		base.Name = "ListViewControl";
		base.Size = new System.Drawing.Size(418, 315);
		this.rightClickContextMenuStrip.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public ListViewControl()
	{
		InitializeComponent();
		AddEvents(listView1);
		listView1.VisibleChanged += listView1_VisibleChanged;
		listView1.DragDrop += base.DragDropControl_DragDrop;
		listView1.DragEnter += Event_DragEnter;
		listView1.DragLeave += Event_DragLeave;
		listView1.DragOver += Event_DragOver;
	}

	public ListViewControlSettings GetConfigSettings(string settingsKey)
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new ListViewControlSettings(settingsKey);
		}
		return TheConfigSettings;
	}

	public void AddItem(object sender, ItemView.ItemArgs args)
	{
		ClearSorting();
		ListViewItem[] array = listView1.Items.Find(args.Name, searchAllSubItems: false);
		int num = -1;
		ListViewControlItem listViewControlItem = null;
		if (array != null && array.Length > 0)
		{
			listViewControlItem = array[0] as ListViewControlItem;
			ListViewItem.ListViewSubItem listViewSubItem = listViewControlItem.SubItems[0];
			string name = (listViewControlItem.SubItems[0].Text = args.Name);
			listViewSubItem.Name = name;
			listViewControlItem.Name = args.Name;
			listViewControlItem.SortOrder = args.SortOrder;
			listViewControlItem.AllowLabelEdit = args.AllowLabelEdit;
			num = listViewControlItem.ImageIndex;
		}
		else
		{
			listViewControlItem = new ListViewControlItem(args.Name, args.AllowLabelEdit);
			listViewControlItem.SubItems.Add(args.Name);
			listViewControlItem.SortOrder = args.SortOrder;
			listViewControlItem.Name = args.Name;
		}
		if (args.IconPath != string.Empty)
		{
			Bitmap value = null;
			if (args.BitmapBytes != null)
			{
				using MemoryStream stream = new MemoryStream(args.BitmapBytes);
				value = new Bitmap(stream);
				IconCache.Remove(args.IconPath);
				IconCache.Add(args.IconPath, value);
			}
			else
			{
				try
				{
					if (!IconCache.TryGetValue(args.IconPath, out value))
					{
						value = IconLoaderHelper.Load(args.IconPath, imageListLarge.ImageSize);
						IconCache.Add(args.IconPath, value);
					}
				}
				catch (Exception)
				{
				}
			}
			if (value != null)
			{
				Bitmap value2 = new Bitmap(value, imageListSmall.ImageSize);
				if (num >= 0)
				{
					imageListLarge.Images[num] = value;
					imageListSmall.Images[num] = value2;
				}
				else
				{
					imageListLarge.Images.Add(value);
					imageListSmall.Images.Add(value2);
					listViewControlItem.ImageIndex = imageListLarge.Images.Count - 1;
				}
			}
			else if (num >= 0)
			{
				imageListLarge.Images[num] = null;
				imageListSmall.Images[num] = null;
			}
		}
		if (num < 0)
		{
			listView1.BeginUpdate();
			listView1.Items.Add(listViewControlItem);
			listView1.EndUpdate();
		}
		else
		{
			listView1.Invalidate();
		}
	}

	public void RemoveItem(object sender, ItemView.ItemArgs args)
	{
		listView1.Items.RemoveByKey(args.Name);
	}

	public void PauseUpdate(object sender, ItemView.PauseUpdateArgs args)
	{
		if (TheListView.PauseCount == 1 && args.Pause)
		{
			listView1.BeginUpdate();
		}
		else if (TheListView.PauseCount == 0 && !args.Pause)
		{
			if (NeedToSendSelectedItems)
			{
				listView1_SelectedIndexChanged(this, null);
			}
			listView1.EndUpdate();
			SortTheList();
		}
	}

	public void ShowContextMenu(object sender, ContextMenuArgs args)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = TheListView.MapMenuToVisual(args.Menu) as ToolStripMenuItem;
		int count = toolStripMenuItem.DropDownItems.Count;
		if (count > 0)
		{
			ToolStripItem[] array = new ToolStripItem[count];
			toolStripMenuItem.DropDownItems.CopyTo(array, 0);
			contextMenuStrip.Items.AddRange(array);
			contextMenuStrip.Items.Add(new ToolStripSeparator());
		}
		if (listView1.SelectedItems.Count == 1)
		{
			ListViewControlItem listViewControlItem = listView1.SelectedItems[0] as ListViewControlItem;
			if (listViewControlItem.AllowLabelEdit)
			{
				contextMenuStrip.Items.Add(renameToolStripMenuItem);
				contextMenuStrip.Items.Add(new ToolStripSeparator());
			}
		}
		contextMenuStrip.Items.Add(largeIconsToolStripMenuItem);
		contextMenuStrip.Items.Add(smallIconsToolStripMenuItem);
		contextMenuStrip.Items.Add(detailsToolStripMenuItem);
		contextMenuStrip.Show(this, PointToClient(Control.MousePosition));
	}

	public void SelectItems(object sender, ItemView.SelectArgs args)
	{
		if (TheListView.PauseCount == 0)
		{
			listView1.BeginUpdate();
		}
		listView1.SelectedItems.Clear();
		string[] items = args.Items;
		foreach (string text in items)
		{
			for (int j = 0; j < listView1.Items.Count; j++)
			{
				if (listView1.Items[j].Text == text)
				{
					listView1.Items[j].Selected = true;
					break;
				}
			}
		}
		if (TheListView.PauseCount == 0)
		{
			listView1.EndUpdate();
		}
	}

	public void GetSelectedItems(object sender, ItemView.SelectArgs args)
	{
		args.Items = new string[listView1.SelectedItems.Count];
		int num = 0;
		foreach (ListViewItem selectedItem in listView1.SelectedItems)
		{
			args.Items[num++] = selectedItem.Text;
		}
	}

	public void ViewMode(object sender, palDotNet.palWidgets.ListView.ViewArgs args)
	{
		switch (args.ViewMode)
		{
		case palDotNet.palWidgets.ListView.ViewMode.eViewDetails:
			detailsToolStripMenuItem_Click(this, null);
			break;
		case palDotNet.palWidgets.ListView.ViewMode.eViewSmallIcon:
			smallIconsToolStripMenuItem_Click(this, null);
			break;
		case palDotNet.palWidgets.ListView.ViewMode.eViewLargeIcon:
			largeIconsToolStripMenuItem_Click(this, null);
			break;
		}
	}

	public void ClearAllItems(object sender, EventArgs args)
	{
		listView1.Items.Clear();
		imageListLarge.Images.Clear();
		imageListSmall.Images.Clear();
	}

	private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (listView1.ListViewItemSorter == null)
		{
			listView1.ListViewItemSorter = new ItemComparer();
			Dirty = false;
		}
		listView1.View = View.SmallIcon;
		smallIconsToolStripMenuItem.Checked = true;
		detailsToolStripMenuItem.Checked = false;
		largeIconsToolStripMenuItem.Checked = false;
		if (Dirty)
		{
			listView1.Sort();
			Dirty = false;
		}
		ListViewSetSpacing();
	}

	private void largeIconsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (listView1.ListViewItemSorter == null)
		{
			listView1.ListViewItemSorter = new ItemComparer();
			Dirty = false;
		}
		listView1.View = View.LargeIcon;
		largeIconsToolStripMenuItem.Checked = true;
		detailsToolStripMenuItem.Checked = false;
		smallIconsToolStripMenuItem.Checked = false;
		if (Dirty)
		{
			listView1.Sort();
			Dirty = false;
		}
		ListViewSetSpacing();
	}

	private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (listView1.ListViewItemSorter == null)
		{
			listView1.ListViewItemSorter = new ItemComparer();
			Dirty = false;
		}
		listView1.View = View.Details;
		detailsToolStripMenuItem.Checked = true;
		largeIconsToolStripMenuItem.Checked = false;
		smallIconsToolStripMenuItem.Checked = false;
		if (Dirty)
		{
			listView1.Sort();
			Dirty = false;
		}
		ListViewSetSpacing();
	}

	private void listView1_Click(object sender, EventArgs e)
	{
		SendClick(180u);
	}

	private void listView1_DoubleClick(object sender, EventArgs e)
	{
		SendClick(181u);
	}

	private void SendClick(uint msg)
	{
		if (listView1.SelectedItems.Count > 0)
		{
			TheListView.ItemClicked(listView1.SelectedItems[0].Text, msg);
		}
	}

	private void listView1_MouseUp(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			TheListView.ShowMenu();
		}
	}

	private void listView1_SelectedIndexChanged(object sender, EventArgs e)
	{
		NeedToSendSelectedItems = TheListView.PauseCount > 0;
		if (NeedToSendSelectedItems)
		{
			return;
		}
		string[] array = new string[listView1.SelectedItems.Count];
		int num = 0;
		foreach (ListViewItem selectedItem in listView1.SelectedItems)
		{
			array[num++] = selectedItem.Text;
		}
		TheListView.SelectItems(array);
	}

	private void listView1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			listView1.Focus();
			if (TheListView.PauseCount == 0)
			{
				listView1.BeginUpdate();
			}
			for (int i = 0; i < listView1.Items.Count; i++)
			{
				listView1.Items[i].Selected = true;
			}
			if (TheListView.PauseCount == 0)
			{
				listView1.EndUpdate();
			}
		}
		else if (e.KeyCode == Keys.F5)
		{
			listView1.Sorting = SortOrder.None;
			listView1.Sorting = SortOrder.Ascending;
			ListViewSetSpacing();
		}
		else
		{
			TheListView.SendKeyEvent(e.KeyData, ItemView.Messages.eKeyPressed);
		}
	}

	private void listView1_KeyUp(object sender, KeyEventArgs e)
	{
		TheListView.SendKeyEvent(e.KeyData, ItemView.Messages.eKeyReleased);
	}

	private void renameToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (listView1.SelectedItems.Count == 1)
		{
			ListViewControlItem listViewControlItem = listView1.SelectedItems[0] as ListViewControlItem;
			if (listViewControlItem.AllowLabelEdit)
			{
				listView1.Focus();
				listViewControlItem.BeginEdit();
			}
		}
	}

	public void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
	{
		if (e.Label == null)
		{
			e.CancelEdit = true;
			return;
		}
		ListViewItem listViewItem = listView1.Items[e.Item];
		e.CancelEdit = !TheListView.AfterLabelEdit(listViewItem.Text, e.Label);
		if (!e.CancelEdit)
		{
			listViewItem.Name = e.Label;
			Dirty = true;
		}
	}

	private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
	{
		ListViewControlItem listViewControlItem = listView1.SelectedItems[0] as ListViewControlItem;
		e.CancelEdit = !listViewControlItem.AllowLabelEdit;
	}

	private void TheListView_EventIconSize(object sender, ItemView.IconSizeArgs e)
	{
		Image[] array = new Image[listView1.LargeImageList.Images.Count];
		int num = 0;
		foreach (Image image in listView1.LargeImageList.Images)
		{
			array[num++] = image;
		}
		listView1.LargeImageList.ImageSize = new Size((int)e.Width, (int)e.Height);
		listView1.LargeImageList.Images.AddRange(array);
		ListViewSetSpacing();
	}

	private void TheListView_EventSmallIconSize(object sender, ItemView.IconSizeArgs e)
	{
		Image[] array = new Image[listView1.SmallImageList.Images.Count];
		int num = 0;
		foreach (Image image in listView1.SmallImageList.Images)
		{
			array[num++] = image;
		}
		listView1.SmallImageList.ImageSize = new Size((int)e.Width, (int)e.Height);
		listView1.SmallImageList.Images.AddRange(array);
		ListViewSetSpacing();
	}

	private void TheListView_EventMultiSelect(object sender, palDotNet.palWidgets.ListView.MultiSelectArgs e)
	{
		listView1.MultiSelect = e.Flag;
	}

	private void listView1_VisibleChanged(object sender, EventArgs e)
	{
		ListViewSetSpacing();
	}

	protected override void Event_DragEnter(object sender, DragEventArgs drgevent)
	{
		LastScrollTime = DateTime.Now;
		base.Event_DragEnter(sender, drgevent);
	}

	protected override void Event_DragOver(object sender, DragEventArgs e)
	{
		base.Event_DragOver(sender, e);
		DateTime now = DateTime.Now;
		if ((float)now.Subtract(LastScrollTime).Milliseconds > 100f)
		{
			Point point = listView1.PointToClient(new Point(e.X, e.Y));
			if (listView1.Bottom - point.Y < 12)
			{
				SendMessage(listView1.Handle, 277, 1, 0);
				LastScrollTime = now;
			}
			else if (point.Y - listView1.Top < 12)
			{
				SendMessage(listView1.Handle, 277, 0, 0);
				LastScrollTime = now;
			}
		}
	}

	protected override void Event_DragLeave(object sender, EventArgs e)
	{
		base.Event_DragLeave(sender, e);
	}

	protected override IItemViewItem GetItemAt(Point point)
	{
		Point point2 = listView1.PointToClient(point);
		return listView1.GetItemAt(point2.X, point2.Y) as IItemViewItem;
	}

	protected override IItemViewItem[] GetSelectedItems()
	{
		IItemViewItem itemAt = GetItemAt(Control.MousePosition);
		if (itemAt != null)
		{
			ListViewItem listViewItem = itemAt as ListViewItem;
			listViewItem.Selected = true;
		}
		IItemViewItem[] array = new IItemViewItem[listView1.SelectedItems.Count];
		int num = 0;
		foreach (ListViewItem selectedItem in listView1.SelectedItems)
		{
			array[num++] = selectedItem as IItemViewItem;
		}
		return array;
	}

	public bool CopySettingsTo(string settingsKey, string settingsKeyTo, IWidgetPanelControl panel)
	{
		if (panel is ListViewControl listViewControl)
		{
			SaveComponentSettings(settingsKey);
			ListViewControlSettings configSettings = listViewControl.GetConfigSettings(settingsKeyTo);
			foreach (SettingsPropertyValue propertyValue in TheConfigSettings.PropertyValues)
			{
				configSettings[propertyValue.Name] = propertyValue.PropertyValue;
			}
			return true;
		}
		return false;
	}

	public void SaveComponentSettings(string settingsKey)
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new ListViewControlSettings(settingsKey);
		}
		TheConfigSettings.View = listView1.View;
		TheConfigSettings.Save();
	}

	public void ResetComponentSettings(string settingsKey)
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new ListViewControlSettings(settingsKey);
		}
		TheConfigSettings.Reload();
	}

	public void LoadComponentSettings(string settingsKey)
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new ListViewControlSettings(settingsKey);
		}
		View? view = TheConfigSettings.View;
		if (view.HasValue)
		{
			listView1.View = view.Value;
			switch (listView1.View)
			{
			case View.Details:
				detailsToolStripMenuItem.Checked = true;
				largeIconsToolStripMenuItem.Checked = false;
				smallIconsToolStripMenuItem.Checked = false;
				break;
			case View.SmallIcon:
				detailsToolStripMenuItem.Checked = false;
				largeIconsToolStripMenuItem.Checked = false;
				smallIconsToolStripMenuItem.Checked = true;
				break;
			case View.LargeIcon:
				detailsToolStripMenuItem.Checked = false;
				largeIconsToolStripMenuItem.Checked = true;
				smallIconsToolStripMenuItem.Checked = false;
				break;
			}
		}
	}

	private void ClearSorting()
	{
		listView1.Sorting = SortOrder.None;
		listView1.ListViewItemSorter = null;
	}

	private void SortTheList()
	{
		listView1.Sorting = SortOrder.None;
		listView1.ListViewItemSorter = new ItemComparer();
		listView1.ListViewItemSorter = null;
	}

	[DllImport("User32.dll")]
	private static extern int SendMessage(IntPtr Handle, int wMsg, int wParam, int lParam);

	[DllImport("user32.dll")]
	private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

	private void ListViewSetSpacing()
	{
		if (listView1.LargeImageList == null || listView1.SmallImageList == null)
		{
			return;
		}
		int num = 0;
		if (listView1.View == View.LargeIcon)
		{
			Size size = new Size(0, 0);
			switch (listView1.View)
			{
			case View.LargeIcon:
				size = listView1.LargeImageList.ImageSize;
				break;
			case View.SmallIcon:
				size = listView1.SmallImageList.ImageSize;
				break;
			}
			using (Graphics graphics = Graphics.FromHwnd(listView1.Handle))
			{
				SizeF sizeF = graphics.MeasureString("TestString", listView1.Font);
				size.Width += ListViewDiff;
				size.Height += 6 + (int)Math.Ceiling(sizeF.Height * 2f);
			}
			num = size.Height * 65536 + (size.Width & 0xFFFF);
		}
		else
		{
			num = -1;
		}
		SendMessage(listView1.Handle, 4149, 0, num);
		RedrawWindow(listView1.Handle, IntPtr.Zero, IntPtr.Zero, 1u);
	}
}
