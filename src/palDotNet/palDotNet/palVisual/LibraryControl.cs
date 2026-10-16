using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class LibraryControl : UserControl, ISupportsToolStrip, IWidgetPanelControl, ILibraryControl
{
	private IContainer components;

	private SplitContainer splitContainerAll;

	private SplitContainer splitContainerRight;

	private SplitContainer splitContainerLeft;

	private TreeViewControl treeViewControl1;

	private ListViewControl listViewControl1;

	private PropertyGridControl propertyGridControl1;

	private ControlRenderComposite controlRender1;

	private ContainerControl TheTopToolStripContainer;

	private ContainerControl TheBottomToolStripContainer;

	private ContainerControl TheLeftToolStripContainer;

	private ContainerControl TheRightToolStripContainer;

	private LibraryControlSettings TheConfigSettings;

	private string DelaySettingsKey = string.Empty;

	private Size LastValidSize;

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

	public bool VisibleTreeView
	{
		set
		{
			if (!value && splitContainerLeft.Panel2Collapsed)
			{
				splitContainerAll.Panel1Collapsed = true;
			}
			else if (value && splitContainerAll.Panel1Collapsed)
			{
				splitContainerAll.Panel1Collapsed = false;
				splitContainerLeft.Panel1Collapsed = false;
			}
			else
			{
				splitContainerLeft.Panel1Collapsed = !value;
			}
		}
	}

	public bool VisibleListView
	{
		set
		{
			if (!value && splitContainerLeft.Panel1Collapsed)
			{
				splitContainerAll.Panel1Collapsed = true;
			}
			else if (value && splitContainerAll.Panel1Collapsed)
			{
				splitContainerAll.Panel1Collapsed = false;
				splitContainerLeft.Panel2Collapsed = false;
			}
			else
			{
				splitContainerLeft.Panel2Collapsed = !value;
			}
		}
	}

	public bool VisiblePropertyGridView
	{
		set
		{
			if (!value && splitContainerRight.Panel2Collapsed)
			{
				splitContainerAll.Panel2Collapsed = true;
			}
			else if (value && splitContainerAll.Panel2Collapsed)
			{
				splitContainerAll.Panel2Collapsed = false;
			}
			splitContainerRight.Panel1Collapsed = !value;
		}
	}

	public bool VisibleRenderWindow
	{
		set
		{
			if (!value && splitContainerRight.Panel1Collapsed)
			{
				splitContainerAll.Panel2Collapsed = true;
			}
			else if (value && splitContainerAll.Panel2Collapsed)
			{
				splitContainerAll.Panel2Collapsed = false;
			}
			splitContainerRight.Panel2Collapsed = !value;
		}
	}

	[ReadOnly(true)]
	[Browsable(false)]
	public WidgetPanel WidgetPanel
	{
		get
		{
			return LibraryView;
		}
		set
		{
			if (LibraryView != null)
			{
				throw new ApplicationException("Tree View has already been set!");
			}
			LibraryView = value as LibraryView;
			LibraryView.LibraryControl = this;
		}
	}

	public LibraryView LibraryView { private get; set; }

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
		this.splitContainerAll = new System.Windows.Forms.SplitContainer();
		this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
		this.splitContainerRight = new System.Windows.Forms.SplitContainer();
		this.treeViewControl1 = new palDotNet.palVisual.TreeViewControl();
		this.listViewControl1 = new palDotNet.palVisual.ListViewControl();
		this.propertyGridControl1 = new palDotNet.palVisual.PropertyGridControl();
		this.controlRender1 = new palDotNet.palVisual.ControlRenderComposite();
		this.splitContainerAll.Panel1.SuspendLayout();
		this.splitContainerAll.Panel2.SuspendLayout();
		this.splitContainerAll.SuspendLayout();
		this.splitContainerLeft.Panel1.SuspendLayout();
		this.splitContainerLeft.Panel2.SuspendLayout();
		this.splitContainerLeft.SuspendLayout();
		this.splitContainerRight.Panel1.SuspendLayout();
		this.splitContainerRight.Panel2.SuspendLayout();
		this.splitContainerRight.SuspendLayout();
		base.SuspendLayout();
		this.splitContainerAll.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainerAll.Location = new System.Drawing.Point(0, 0);
		this.splitContainerAll.Name = "splitContainerAll";
		this.splitContainerAll.Panel1.Controls.Add(this.splitContainerLeft);
		this.splitContainerAll.Panel2.Controls.Add(this.splitContainerRight);
		this.splitContainerAll.Size = new System.Drawing.Size(1677, 549);
		this.splitContainerAll.SplitterDistance = 962;
		this.splitContainerAll.TabIndex = 0;
		this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainerLeft.Location = new System.Drawing.Point(0, 0);
		this.splitContainerLeft.Name = "splitContainerLeft";
		this.splitContainerLeft.Panel1.Controls.Add(this.treeViewControl1);
		this.splitContainerLeft.Panel2.Controls.Add(this.listViewControl1);
		this.splitContainerLeft.Size = new System.Drawing.Size(962, 549);
		this.splitContainerLeft.SplitterDistance = 354;
		this.splitContainerLeft.TabIndex = 0;
		this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
		this.splitContainerRight.Name = "splitContainerRight";
		this.splitContainerRight.Panel1.Controls.Add(this.propertyGridControl1);
		this.splitContainerRight.Panel1MinSize = 25;
		this.splitContainerRight.Panel2.Controls.Add(this.controlRender1);
		this.splitContainerRight.Panel2MinSize = 25;
		this.splitContainerRight.Size = new System.Drawing.Size(711, 549);
		this.splitContainerRight.SplitterDistance = 335;
		this.splitContainerRight.TabIndex = 0;
		this.treeViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.treeViewControl1.Location = new System.Drawing.Point(0, 0);
		this.treeViewControl1.Name = "treeViewControl1";
		this.treeViewControl1.Size = new System.Drawing.Size(354, 549);
		this.treeViewControl1.TabIndex = 0;
		this.listViewControl1.AllowDrop = true;
		this.listViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listViewControl1.Location = new System.Drawing.Point(0, 0);
		this.listViewControl1.Name = "listViewControl1";
		this.listViewControl1.Size = new System.Drawing.Size(604, 549);
		this.listViewControl1.TabIndex = 0;
		this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.propertyGridControl1.Location = new System.Drawing.Point(0, 0);
		this.propertyGridControl1.Name = "propertyGridControl1";
		this.propertyGridControl1.Size = new System.Drawing.Size(604, 549);
		this.propertyGridControl1.TabIndex = 0;
		this.controlRender1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.controlRender1.Location = new System.Drawing.Point(0, 0);
		this.controlRender1.Name = "controlRender1";
		this.controlRender1.Size = new System.Drawing.Size(372, 549);
		this.controlRender1.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.splitContainerAll);
		base.Name = "LibraryControl";
		base.Size = new System.Drawing.Size(1677, 549);
		this.splitContainerAll.Panel1.ResumeLayout(false);
		this.splitContainerAll.Panel2.ResumeLayout(false);
		this.splitContainerAll.ResumeLayout(false);
		this.splitContainerLeft.Panel1.ResumeLayout(false);
		this.splitContainerLeft.Panel2.ResumeLayout(false);
		this.splitContainerLeft.ResumeLayout(false);
		this.splitContainerRight.Panel1.ResumeLayout(false);
		this.splitContainerRight.Panel2.ResumeLayout(false);
		this.splitContainerRight.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public LibraryControl()
	{
		InitializeComponent();
	}

	public void OnListViewAdded(object sender, EventArgs args)
	{
		palDotNet.palWidgets.ListView listView = LibraryView.ListView;
		listViewControl1.WidgetPanel = listView;
		ToolStrip.RegisterToolStripSupporter(listView, listViewControl1);
	}

	public void OnRenderWindowAdded(object sender, EventArgs args)
	{
		RenderWindow renderWindow = LibraryView.RenderWindow;
		controlRender1.RenderWindow = renderWindow;
		ToolStrip.RegisterToolStripSupporter(renderWindow, controlRender1);
	}

	public void OnTreeViewAdded(object sender, EventArgs args)
	{
		palDotNet.palWidgets.TreeView treeView = LibraryView.TreeView;
		treeViewControl1.WidgetPanel = treeView;
		ToolStrip.RegisterToolStripSupporter(treeView, treeViewControl1);
	}

	public void OnPropertyGridViewAdded(object sender, EventArgs args)
	{
		PropertyGridView propertyGridView = LibraryView.PropertyGridView;
		propertyGridControl1.WidgetPanel = propertyGridView;
		ToolStrip.RegisterToolStripSupporter(propertyGridView, propertyGridControl1);
	}

	public LibraryControlSettings GetConfigSettings(string settingsKey)
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new LibraryControlSettings(settingsKey);
		}
		return TheConfigSettings;
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
		if (DelaySettingsKey != string.Empty)
		{
			LoadComponentSettings(DelaySettingsKey);
		}
		if (base.Size.Width > 0 && base.Size.Height > 0)
		{
			LastValidSize = base.Size;
		}
	}

	protected override void OnEnabledChanged(EventArgs e)
	{
		listViewControl1.Visible = base.Enabled;
		treeViewControl1.Visible = base.Enabled;
		propertyGridControl1.Visible = base.Enabled;
		splitContainerRight.Panel1.Visible = base.Enabled;
		controlRender1.Visible = base.Enabled;
		if (base.Enabled)
		{
			splitContainerAll.BorderStyle = BorderStyle.None;
		}
		else
		{
			splitContainerAll.BorderStyle = BorderStyle.FixedSingle;
		}
		base.OnEnabledChanged(e);
	}

	public bool CopySettingsTo(string settingsKey, string settingsKeyTo, IWidgetPanelControl panel)
	{
		if (panel is LibraryControl libraryControl)
		{
			SaveComponentSettings(settingsKey);
			LibraryControlSettings configSettings = libraryControl.GetConfigSettings(settingsKeyTo);
			foreach (SettingsPropertyValue propertyValue in TheConfigSettings.PropertyValues)
			{
				configSettings[propertyValue.Name] = propertyValue.PropertyValue;
			}
			listViewControl1.CopySettingsTo(settingsKey, settingsKeyTo, libraryControl.listViewControl1);
			propertyGridControl1.CopySettingsTo(settingsKey, settingsKeyTo, libraryControl.propertyGridControl1);
			return true;
		}
		return false;
	}

	public void SaveComponentSettings(string settingsKey)
	{
		GetConfigSettings(settingsKey);
		if (DelaySettingsKey == string.Empty)
		{
			TheConfigSettings.SplitContainer1SplitterDistance = splitContainerAll.SplitterDistance;
			TheConfigSettings.SplitContainer2SplitterDistance = splitContainerRight.SplitterDistance;
			TheConfigSettings.SplitContainer4SplitterDistance = splitContainerLeft.SplitterDistance;
			TheConfigSettings.LibraryControlSplitterSize = ((base.Size.Width == 0 || base.Size.Height == 0) ? LastValidSize : base.Size);
		}
		TheConfigSettings.Save();
		listViewControl1.SaveComponentSettings(settingsKey);
		propertyGridControl1.SaveComponentSettings(settingsKey);
	}

	public void ResetComponentSettings(string settingsKey)
	{
		GetConfigSettings(settingsKey);
		TheConfigSettings.Reload();
		listViewControl1.ResetComponentSettings(settingsKey);
		propertyGridControl1.ResetComponentSettings(settingsKey);
	}

	public void LoadComponentSettings(string settingsKey)
	{
		GetConfigSettings(settingsKey);
		try
		{
			SuspendLayout();
			listViewControl1.LoadComponentSettings(settingsKey);
			propertyGridControl1.LoadComponentSettings(settingsKey);
			LoadSplitterDistances(suspend: false);
			DelaySettingsKey = string.Empty;
			ResumeLayout();
		}
		catch (Exception)
		{
		}
	}

	private void LoadSplitterDistances(bool suspend)
	{
		if (TheConfigSettings == null)
		{
			return;
		}
		if (!TheConfigSettings.LibraryControlSplitterSize.HasValue)
		{
			DelaySettingsKey = string.Empty;
		}
		else if (base.Size.Width > 0 && base.Size.Height > 0)
		{
			float ratio = 1f;
			if (TheConfigSettings.LibraryControlSplitterSize.HasValue)
			{
				ratio = (float)base.Size.Width / (float)TheConfigSettings.LibraryControlSplitterSize.Value.Width;
			}
			if (suspend)
			{
				SuspendLayout();
			}
			SetSplitterDistance(splitContainerAll, TheConfigSettings.SplitContainer1SplitterDistance, ratio);
			SetSplitterDistance(splitContainerRight, TheConfigSettings.SplitContainer2SplitterDistance, ratio);
			SetSplitterDistance(splitContainerLeft, TheConfigSettings.SplitContainer4SplitterDistance, ratio);
			DelaySettingsKey = string.Empty;
			if (suspend)
			{
				ResumeLayout();
			}
		}
	}

	private void SetSplitterDistance(SplitContainer splitContainer, int? distance, float ratio)
	{
		if (distance.HasValue)
		{
			splitContainer.SplitterDistance = (int)((float)distance.Value * ratio);
		}
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
