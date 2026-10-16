using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportCustomBase : UserControl, IDialogContainer
{
	private IContainer components;

	public virtual bool ShouldForceEmbeddedProps => false;

	public virtual string ExporterTypeName
	{
		get
		{
			throw new NotImplementedException();
		}
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
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.MaximumSize = new System.Drawing.Size(499, 385);
		this.MinimumSize = new System.Drawing.Size(499, 385);
		base.Name = "ExportCustomBase";
		base.Size = new System.Drawing.Size(499, 385);
		base.ResumeLayout(false);
	}

	public ExportCustomBase()
	{
		InitializeComponent();
	}
}
