using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palVisual;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public class ViewPropertyGrid : ViewTool, IPersistComponentSettings
{
	private IContainer components;

	private PropertyGrid propertyGrid1;

	public PropertyGrid TheGrid => propertyGrid1;

	private ViewPropertyGrid()
	{
		InitializeComponent();
	}

	public ViewPropertyGrid(Guid guid)
	{
		InitializeComponent();
		TheGuid = guid;
		PalDockContent.DockContents.Add(guid, this);
	}

	public override void RemoveFromDockContents(WidgetPanel panel)
	{
		base.RemoveFromDockContents(panel);
		propertyGrid1.DeregisterEvents();
	}

	public override void SaveComponentSettings()
	{
		propertyGrid1.SaveComponentSettings(SettingsKey);
	}

	public override void ResetComponentSettings()
	{
		propertyGrid1.ResetComponentSettings(SettingsKey);
	}

	public override void LoadComponentSettings()
	{
		propertyGrid1.LoadComponentSettings(SettingsKey);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			SaveComponentSettings();
			if (components != null)
			{
				components.Dispose();
			}
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.propertyGrid1 = new palDotNet.palVisualPropertyGrid.PropertyGrid();
		base.SuspendLayout();
		this.propertyGrid1.AllowScrollValueOnDoubleClick = false;
		this.propertyGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.propertyGrid1.CommentsHeight = 70;
		this.propertyGrid1.CommentsVisibility = false;
		this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.propertyGrid1.LabelColumnWidthRatio = 0.6;
		this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
		this.propertyGrid1.Name = "propertyGrid1";
		this.propertyGrid1.Size = new System.Drawing.Size(292, 273);
		this.propertyGrid1.TabIndex = 0;
		this.propertyGrid1.Text = "propertyGrid1";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 273);
		base.Controls.Add(this.propertyGrid1);
		base.Name = "ViewPropertyGrid";
		base.ResumeLayout(false);
	}
}
