using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palVisualPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class PropertyGridControl : UserControl, ISupportsToolStrip, IWidgetPanelControl, IPropertyGridControl
{
	private ContainerControl TheTopToolStripContainer;

	private ContainerControl TheBottomToolStripContainer;

	private ContainerControl TheLeftToolStripContainer;

	private ContainerControl TheRightToolStripContainer;

	private LibraryControlSettings TheConfigSettings;

	private IContainer components;

	private palDotNet.palVisualPropertyGrid.PropertyGrid propertyGrid1;

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

	[Browsable(false)]
	[ReadOnly(true)]
	public WidgetPanel WidgetPanel
	{
		get
		{
			return PropertyGridView;
		}
		set
		{
			if (PropertyGridView != null)
			{
				throw new ApplicationException("Property Grid View has already been set!");
			}
			PropertyGridView = value as PropertyGridView;
			PropertyGridView.PropertyGridControl = this;
		}
	}

	public PropertyGridView PropertyGridView { private get; set; }

	public PropertyGridControl()
	{
		InitializeComponent();
	}

	public void OnWidgetBankAdded(object sender, EventArgs args)
	{
		Bank widgetBank = PropertyGridView.WidgetBank;
		propertyGrid1.WidgetBank = widgetBank;
	}

	public bool CopySettingsTo(string settingsKey, string settingsKeyTo, IWidgetPanelControl panel)
	{
		return false;
	}

	public void SaveComponentSettings(string settingsKey)
	{
		GetConfigSettings(settingsKey);
		propertyGrid1.SaveComponentSettings(settingsKey);
	}

	public void ResetComponentSettings(string settingsKey)
	{
		GetConfigSettings(settingsKey);
		propertyGrid1.ResetComponentSettings(settingsKey);
	}

	public void LoadComponentSettings(string settingsKey)
	{
		GetConfigSettings(settingsKey);
		SuspendLayout();
		propertyGrid1.LoadComponentSettings(settingsKey);
		ResumeLayout();
	}

	public LibraryControlSettings GetConfigSettings(string settingsKey)
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new LibraryControlSettings(settingsKey);
		}
		return TheConfigSettings;
	}

	public void ShowContextMenu(object sender, ContextMenuArgs args)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = PropertyGridView.MapMenuToVisual(args.Menu) as ToolStripMenuItem;
		int count = toolStripMenuItem.DropDownItems.Count;
		if (count > 0)
		{
			ToolStripItem[] array = new ToolStripItem[count];
			toolStripMenuItem.DropDownItems.CopyTo(array, 0);
			contextMenuStrip.Items.AddRange(array);
		}
		contextMenuStrip.Show(this, PointToClient(Control.MousePosition));
	}

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
		this.propertyGrid1 = new palDotNet.palVisualPropertyGrid.PropertyGrid();
		base.SuspendLayout();
		this.propertyGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
		this.propertyGrid1.Name = "propertyGrid1";
		this.propertyGrid1.Size = new System.Drawing.Size(251, 325);
		this.propertyGrid1.TabIndex = 0;
		this.propertyGrid1.Text = "propertyGrid1";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.propertyGrid1);
		base.Name = "PropertyGridControl";
		base.Size = new System.Drawing.Size(251, 325);
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
