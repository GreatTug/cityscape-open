using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace palDotNet.palVisual;

public class ViewTool : PalDockContent
{
	private IContainer components;

	protected Guid TheGuid;

	public override Guid Guid => TheGuid;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(palDotNet.palVisual.ViewTool));
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 266);
		base.CloseButton = false;
		base.CloseButtonVisible = false;
		base.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "ViewTool";
		this.Text = "Widgets";
		base.ResumeLayout(false);
	}

	internal ViewTool()
	{
		InitializeComponent();
	}

	public ViewTool(Guid guid)
	{
		InitializeComponent();
		TheGuid = guid;
		PalDockContent.DockContents.Add(guid, this);
	}
}
