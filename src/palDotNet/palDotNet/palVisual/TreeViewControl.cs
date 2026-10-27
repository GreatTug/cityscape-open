using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class TreeViewControl : ItemViewControl, IWidgetPanelControl
{
	private IContainer components;

	private TreeViewDerived treeView1;

	private ImageList imageList1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem renameToolStripMenuItem;

	private ToolStripMenuItem expandAllToolStripMenuItem;

	private ToolStripMenuItem collapseAllToolStripMenuItem;

	private palDotNet.palWidgets.TreeView TheTreeView;

	private bool SelectingAnItem;

	private static Dictionary<string, Bitmap> IconCache = new Dictionary<string, Bitmap>();

	[Browsable(false)]
	[ReadOnly(true)]
	public System.Windows.Forms.TreeView UnderlyingTreeView { get { return treeView1; } }

	[Browsable(false)]
	[ReadOnly(true)]
	public WidgetPanel WidgetPanel
	{
		get
		{
			return TheTreeView;
		}
		set
		{
			if (TheTreeView != null)
			{
				throw new ApplicationException("Tree View has already been set!");
			}
			TheTreeView = value as palDotNet.palWidgets.TreeView;
			TheTreeView.EventAddItem += AddItem;
			TheTreeView.EventRemoveItem += RemoveItem;
			TheTreeView.EventShowContextMenu += ShowContextMenu;
			TheTreeView.EventSelectItems += SelectItems;
			TheTreeView.EventGetSelectedItems += GetSelectedItems;
			TheTreeView.EventClearAllItems += ClearAllItems;
			TheTreeView.EventBeginRenameItem += renameToolStripMenuItem_Click;
			TheTreeView.EventIconSize += TheTreeView_EventIconsSize;
			TheTreeView.ExpandOrCollapseDelegate = delegate(bool expand)
			{
				if (expand)
				{
					treeView1.ExpandAll();
				}
				else
				{
					treeView1.CollapseAll();
				}
			};
		}
	}

	protected override ItemView ItemView => TheTreeView;

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
		this.treeView1 = new palDotNet.palVisual.TreeViewDerived();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.treeView1.AllowDrop = true;
		this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.treeView1.HideSelection = false;
		this.treeView1.ImageIndex = 0;
		this.treeView1.ImageList = this.imageList1;
		this.treeView1.LabelEdit = true;
		this.treeView1.Location = new System.Drawing.Point(0, 0);
		this.treeView1.Name = "treeView1";
		this.treeView1.SelectedImageIndex = 0;
		this.treeView1.Size = new System.Drawing.Size(471, 533);
		this.treeView1.TabIndex = 0;
		this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(treeView1_AfterLabelEdit);
		this.treeView1.DoubleClick += new System.EventHandler(treeView1_DoubleClick);
		this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(treeView1_MouseUp);
		this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView1_SelectedIndexChanged);
		this.treeView1.KeyUp += new System.Windows.Forms.KeyEventHandler(treeView1_KeyUp);
		this.treeView1.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(treeView1_BeforeLabelEdit);
		this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(treeView1_KeyDown);
		this.treeView1.Click += new System.EventHandler(treeView1_Click);
		this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
		this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.renameToolStripMenuItem, this.expandAllToolStripMenuItem, this.collapseAllToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(137, 70);
		this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
		this.renameToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.renameToolStripMenuItem.Text = "Rena&me";
		this.renameToolStripMenuItem.Click += new System.EventHandler(renameToolStripMenuItem_Click);
		this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
		this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.expandAllToolStripMenuItem.Text = "&Expand All";
		this.expandAllToolStripMenuItem.Click += new System.EventHandler(expandAllToolStripMenuItem_Click);
		this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
		this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.collapseAllToolStripMenuItem.Text = "&Collapse All";
		this.collapseAllToolStripMenuItem.Click += new System.EventHandler(collapseAllToolStripMenuItem_Click);
		this.AllowDrop = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.treeView1);
		base.Name = "TreeViewControl";
		base.Size = new System.Drawing.Size(471, 533);
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public TreeViewControl()
	{
		InitializeComponent();
		AddEvents(treeView1);
		treeView1.DragDrop += base.DragDropControl_DragDrop;
		treeView1.DragEnter += Event_DragEnter;
		treeView1.DragLeave += Event_DragLeave;
		treeView1.DragOver += Event_DragOver;
		treeView1.BeforeSelect += treeView1_BeforeSelect;
	}

	private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
	{
		if (e.Action == TreeViewAction.Unknown && !SelectingAnItem)
		{
			e.Cancel = true;
		}
		SelectingAnItem = false;
	}

	protected override IItemViewItem GetItemAt(Point point)
	{
		Point point2 = treeView1.PointToClient(point);
		return treeView1.GetNodeAt(point2.X, point2.Y) as IItemViewItem;
	}

	protected override IItemViewItem[] GetSelectedItems()
	{
		Point mousePosition = Control.MousePosition;
		mousePosition = treeView1.PointToClient(mousePosition);
		PointToClient(Control.MousePosition);
		TreeViewHitTestInfo treeViewHitTestInfo = treeView1.HitTest(mousePosition);
		if (treeViewHitTestInfo.Node == null)
		{
			return null;
		}
		if (treeViewHitTestInfo.Location != TreeViewHitTestLocations.Label && treeViewHitTestInfo.Location != TreeViewHitTestLocations.Image)
		{
			return null;
		}
		if (treeViewHitTestInfo.Node is IItemViewItem itemViewItem)
		{
			return new IItemViewItem[1] { itemViewItem };
		}
		return null;
	}

	public void AddItem(object sender, ItemView.ItemArgs args)
	{
		treeView1.PathSeparator = string.Concat(TheTreeView.PathSeparator);
		string[] array = args.Name.Split(treeView1.PathSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
		TreeNodeCollection nodes = treeView1.Nodes;
		for (int i = 0; i < array.Length - 1; i++)
		{
			TreeNode[] array2 = nodes.Find(array[i], searchAllChildren: false);
			if (array2.Length == 1)
			{
				nodes = array2[0].Nodes;
				continue;
			}
			return;
		}
		string text = array[array.Length - 1];
		TreeViewControlNode treeViewControlNode = null;
		TreeNode[] array3 = nodes.Find(text, searchAllChildren: false);
		int num = -1;
		if (array3 != null && array3.Length == 1)
		{
			treeViewControlNode = array3[0] as TreeViewControlNode;
			num = treeViewControlNode.SelectedImageIndex;
		}
		else
		{
			treeViewControlNode = new TreeViewControlNode(text, args.AllowLabelEdit);
		}
		treeViewControlNode.Text = text;
		treeViewControlNode.Name = text;
		Bitmap value = null;
		if (args.IconPath != string.Empty)
		{
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
						value = IconLoaderHelper.Load(args.IconPath, imageList1.ImageSize);
						IconCache.Add(args.IconPath, value);
					}
				}
				catch (Exception)
				{
				}
			}
			if (value != null)
			{
				imageList1.Images.Add(value);
				TreeViewControlNode treeViewControlNode2 = treeViewControlNode;
				int imageIndex = (treeViewControlNode.SelectedImageIndex = imageList1.Images.Count - 1);
				treeViewControlNode2.ImageIndex = imageIndex;
			}
		}
		if (num >= 0)
		{
			imageList1.Images[num] = value;
		}
		else
		{
			nodes.Add(treeViewControlNode);
		}
	}

	public void ClearAllItems(object sender, EventArgs args)
	{
		treeView1.Nodes.Clear();
	}

	public void RemoveItem(object sender, ItemView.ItemArgs args)
	{
		TreeNode treeNode = FindChild(args.Name);
		if (treeNode != null)
		{
			treeView1.Nodes.Remove(treeNode);
		}
	}

	public void ShowContextMenu(object sender, ContextMenuArgs args)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = TheTreeView.MapMenuToVisual(args.Menu) as ToolStripMenuItem;
		int count = toolStripMenuItem.DropDownItems.Count;
		if (count > 0)
		{
			ToolStripItem[] array = new ToolStripItem[count];
			toolStripMenuItem.DropDownItems.CopyTo(array, 0);
			contextMenuStrip.Items.AddRange(array);
			contextMenuStrip.Items.Add(new ToolStripSeparator());
		}
		if (treeView1.SelectedNode != null)
		{
			TreeViewControlNode treeViewControlNode = treeView1.SelectedNode as TreeViewControlNode;
			if (treeViewControlNode.AllowLabelEdit)
			{
				contextMenuStrip.Items.Add(renameToolStripMenuItem);
				contextMenuStrip.Items.Add(new ToolStripSeparator());
			}
		}
		contextMenuStrip.Items.Add(expandAllToolStripMenuItem);
		contextMenuStrip.Items.Add(collapseAllToolStripMenuItem);
		contextMenuStrip.Show(this, PointToClient(Control.MousePosition));
	}

	public void SelectItems(object sender, ItemView.SelectArgs args)
	{
		if (args.Items.Length == 1)
		{
			SelectANode(FindChild(args.Items[0]));
		}
	}

	public void GetSelectedItems(object sender, ItemView.SelectArgs args)
	{
		TreeNode selectedNode = treeView1.SelectedNode;
		args.Items = new string[(selectedNode != null) ? 1u : 0u];
		if (selectedNode != null)
		{
			args.Items[0] = treeView1.SelectedNode.FullPath;
		}
	}

	private void treeView1_MouseUp(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			TreeNode nodeAt = treeView1.GetNodeAt(e.Location);
			if (nodeAt != null)
			{
				SelectANode(nodeAt);
			}
			TheTreeView.ShowMenu();
		}
		else if (e.Button == MouseButtons.Left)
		{
			TreeNode nodeAt2 = treeView1.GetNodeAt(e.Location);
			if ((nodeAt2 == null || !nodeAt2.Bounds.Contains(e.Location)) && treeView1.SelectedNode != null)
			{
				SelectANode(null);
				TheTreeView.SelectItems(new string[0]);
			}
		}
	}

	private void treeView1_SelectedIndexChanged(object sender, TreeViewEventArgs e)
	{
		ItemView.SelectArgs selectArgs = new ItemView.SelectArgs(null);
		GetSelectedItems(this, selectArgs);
		TheTreeView.SelectItems(selectArgs.Items);
	}

	private void treeView1_KeyDown(object sender, KeyEventArgs e)
	{
		TheTreeView.SendKeyEvent(e.KeyData, ItemView.Messages.eKeyPressed);
	}

	private void treeView1_KeyUp(object sender, KeyEventArgs e)
	{
		TheTreeView.SendKeyEvent(e.KeyData, ItemView.Messages.eKeyReleased);
	}

	private void renameToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (treeView1.SelectedNode != null)
		{
			TreeViewControlNode treeViewControlNode = treeView1.SelectedNode as TreeViewControlNode;
			if (treeViewControlNode.AllowLabelEdit)
			{
				treeView1.Focus();
				treeViewControlNode.BeginEdit();
			}
		}
	}

	private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
	{
		if (e.Label == null)
		{
			e.CancelEdit = true;
			return;
		}
		if (e.Label.IndexOf(TheTreeView.PathSeparator) >= 0)
		{
			WidgetMgr.Instance.ShowMessageBox("Item name with '" + TheTreeView.PathSeparator + "' character is illegal!", "Illegal Name", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			e.CancelEdit = true;
			return;
		}
		e.CancelEdit = !TheTreeView.AfterLabelEdit(e.Node.Text, e.Label);
		if (!e.CancelEdit)
		{
			e.Node.Name = e.Label;
		}
	}

	private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
	{
		TreeViewControlNode treeViewControlNode = e.Node as TreeViewControlNode;
		e.CancelEdit = !treeViewControlNode.AllowLabelEdit;
	}

	private void treeView1_Click(object sender, EventArgs e)
	{
		Point pt = treeView1.PointToClient(Control.MousePosition);
		TreeNode nodeAt = treeView1.GetNodeAt(pt);
		if (nodeAt != null && nodeAt.Bounds.Contains(pt))
		{
			uint message = 180u;
			TheTreeView.ItemClicked(nodeAt.FullPath, message);
		}
	}

	private void treeView1_DoubleClick(object sender, EventArgs e)
	{
		TreeNode selectedNode = treeView1.SelectedNode;
		if (selectedNode != null)
		{
			uint message = 181u;
			TheTreeView.ItemClicked(selectedNode.FullPath, message);
		}
	}

	private void TheTreeView_EventIconsSize(object sender, ItemView.IconSizeArgs e)
	{
		Image[] array = new Image[treeView1.ImageList.Images.Count];
		int num = 0;
		foreach (Image image in treeView1.ImageList.Images)
		{
			array[num++] = image;
		}
		treeView1.ImageList.Images.Clear();
		treeView1.ImageList.ImageSize = new Size((int)e.Width, (int)e.Height);
		treeView1.ImageList.Images.AddRange(array);
	}

	private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		treeView1.ExpandAll();
	}

	private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		treeView1.CollapseAll();
	}

	public bool CopySettingsTo(string settingsKey, string settingsKeyTo, IWidgetPanelControl panel)
	{
		return false;
	}

	protected override void StartDragging()
	{
		base.StartDragging();
		Point mousePosition = Control.MousePosition;
		mousePosition = treeView1.PointToClient(mousePosition);
		TreeNode nodeAt = treeView1.GetNodeAt(mousePosition);
		if (nodeAt != null)
		{
			SelectANode(nodeAt);
		}
	}

	protected override void DoDragHover()
	{
		Point mousePosition = Control.MousePosition;
		mousePosition = treeView1.PointToClient(mousePosition);
		treeView1.GetNodeAt(mousePosition)?.Expand();
	}

	private void SelectANode(TreeNode node)
	{
		if (treeView1.SelectedNode != node)
		{
			if (node != null)
			{
				SelectingAnItem = true;
			}
			treeView1.SelectedNode = node;
		}
	}

	internal TreeNode FindChild(string path)
	{
		string[] array = path.Split(treeView1.PathSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
		if (array.Length <= 0)
		{
			return null;
		}
		TreeNodeCollection nodes = treeView1.Nodes;
		while (nodes != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				TreeNode[] array2 = nodes.Find(array[i], searchAllChildren: false);
				if (array2 != null && array2.Length > 0)
				{
					if (i == array.Length - 1)
					{
						return array2[0];
					}
					nodes = array2[0].Nodes;
					continue;
				}
				return null;
			}
		}
		return null;
	}

	public void SaveComponentSettings(string settingsKey)
	{
	}

	public void ResetComponentSettings(string settingsKey)
	{
	}

	public void LoadComponentSettings(string settingsKey)
	{
	}
}
