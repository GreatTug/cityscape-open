using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet.ExportDialogItems;

public class ExportPathControl : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox groupBox1;

	private DialogPathLoader exportPath;

	public string Filter
	{
		get
		{
			return exportPath.Filter;
		}
		set
		{
			exportPath.Filter = value;
		}
	}

	public string Path
	{
		get
		{
			return exportPath.Path;
		}
		set
		{
			exportPath.Path = value;
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.exportPath = new palDotNet.palDialog.DialogPathLoader();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.exportPath);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(564, 134);
		this.groupBox1.TabIndex = 17;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Export path";
		this.exportPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportPath.BackColor = System.Drawing.Color.Transparent;
		this.exportPath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eSave;
		this.exportPath.CheckFileExists = false;
		this.exportPath.Description = "Choose File Path to Save Exported Files";
		this.exportPath.Filter = "Generic files (*.dae)|*.dae|All files (*.*)|*.*";
		this.exportPath.Location = new System.Drawing.Point(13, 21);
		this.exportPath.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
		this.exportPath.Name = "exportPath";
		this.exportPath.Path = "";
		this.exportPath.SaveToConfig = true;
		this.exportPath.Size = new System.Drawing.Size(538, 24);
		this.exportPath.TabIndex = 14;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "ExportPathControl";
		base.Size = new System.Drawing.Size(564, 134);
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public ExportPathControl()
	{
		InitializeComponent();
	}

	public void okBtn_Click(object sender, EventArgs e)
	{
		while (string.IsNullOrEmpty((string)exportPath.DialogValue))
		{
			DialogResult dialogResult = exportPath.ShowDialog();
			if (dialogResult == DialogResult.Cancel)
			{
				break;
			}
		}
	}
}
