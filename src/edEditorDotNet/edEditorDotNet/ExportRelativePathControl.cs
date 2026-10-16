using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportRelativePathControl : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox groupBox1;

	private DialogPathLoader TextureRelativePath;

	private DialogCheckBox TextureRelativeToggle;

	private DialogPathLoader ReferenceRelativePath;

	private DialogCheckBox PropRelativeToggle;

	private DialogCheckBox AllRelativeToExportPath;

	public CheckBox AllRelativeToExportPathCheckBox => AllRelativeToExportPath;

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
		this.AllRelativeToExportPath = new palDotNet.palDialog.DialogCheckBox();
		this.ReferenceRelativePath = new palDotNet.palDialog.DialogPathLoader();
		this.PropRelativeToggle = new palDotNet.palDialog.DialogCheckBox();
		this.TextureRelativeToggle = new palDotNet.palDialog.DialogCheckBox();
		this.TextureRelativePath = new palDotNet.palDialog.DialogPathLoader();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.AllRelativeToExportPath);
		this.groupBox1.Controls.Add(this.ReferenceRelativePath);
		this.groupBox1.Controls.Add(this.PropRelativeToggle);
		this.groupBox1.Controls.Add(this.TextureRelativeToggle);
		this.groupBox1.Controls.Add(this.TextureRelativePath);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(564, 134);
		this.groupBox1.TabIndex = 17;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Relative paths";
		this.AllRelativeToExportPath.AutoSize = true;
		this.AllRelativeToExportPath.Checked = true;
		this.AllRelativeToExportPath.CheckState = System.Windows.Forms.CheckState.Checked;
		this.AllRelativeToExportPath.Location = new System.Drawing.Point(6, 19);
		this.AllRelativeToExportPath.Name = "AllRelativeToExportPath";
		this.AllRelativeToExportPath.SaveToConfig = true;
		this.AllRelativeToExportPath.Size = new System.Drawing.Size(218, 17);
		this.AllRelativeToExportPath.TabIndex = 0;
		this.AllRelativeToExportPath.Text = "Make all paths relative to the export path";
		this.AllRelativeToExportPath.UseVisualStyleBackColor = true;
		this.AllRelativeToExportPath.CheckedChanged += new System.EventHandler(AllRelativeToExportPath_CheckedChanged);
		this.ReferenceRelativePath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ReferenceRelativePath.BackColor = System.Drawing.Color.Transparent;
		this.ReferenceRelativePath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eDirectories;
		this.ReferenceRelativePath.CheckFileExists = false;
		this.ReferenceRelativePath.Description = null;
		this.ReferenceRelativePath.Enabled = false;
		this.ReferenceRelativePath.Filter = null;
		this.ReferenceRelativePath.Location = new System.Drawing.Point(216, 72);
		this.ReferenceRelativePath.Name = "ReferenceRelativePath";
		this.ReferenceRelativePath.Path = "";
		this.ReferenceRelativePath.SaveToConfig = true;
		this.ReferenceRelativePath.Size = new System.Drawing.Size(342, 24);
		this.ReferenceRelativePath.TabIndex = 4;
		this.PropRelativeToggle.AutoSize = true;
		this.PropRelativeToggle.Enabled = false;
		this.PropRelativeToggle.Location = new System.Drawing.Point(16, 79);
		this.PropRelativeToggle.Name = "PropRelativeToggle";
		this.PropRelativeToggle.SaveToConfig = true;
		this.PropRelativeToggle.Size = new System.Drawing.Size(182, 17);
		this.PropRelativeToggle.TabIndex = 3;
		this.PropRelativeToggle.Text = "Make prop references relative to:";
		this.PropRelativeToggle.UseVisualStyleBackColor = true;
		this.PropRelativeToggle.CheckedChanged += new System.EventHandler(PropRelativeToggle_CheckedChanged);
		this.TextureRelativeToggle.AutoSize = true;
		this.TextureRelativeToggle.Enabled = false;
		this.TextureRelativeToggle.Location = new System.Drawing.Point(16, 49);
		this.TextureRelativeToggle.Name = "TextureRelativeToggle";
		this.TextureRelativeToggle.SaveToConfig = true;
		this.TextureRelativeToggle.Size = new System.Drawing.Size(145, 17);
		this.TextureRelativeToggle.TabIndex = 1;
		this.TextureRelativeToggle.Text = "Make textures relative to:";
		this.TextureRelativeToggle.UseVisualStyleBackColor = true;
		this.TextureRelativeToggle.CheckedChanged += new System.EventHandler(TextureRelativeToggle_CheckedChanged);
		this.TextureRelativePath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TextureRelativePath.BackColor = System.Drawing.Color.Transparent;
		this.TextureRelativePath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eDirectories;
		this.TextureRelativePath.CheckFileExists = false;
		this.TextureRelativePath.Description = "Choose File Path to Save Exported Files";
		this.TextureRelativePath.Enabled = false;
		this.TextureRelativePath.Filter = "Generic files (*.dae)|*.dae|All files (*.*)|*.*";
		this.TextureRelativePath.Location = new System.Drawing.Point(216, 42);
		this.TextureRelativePath.Name = "TextureRelativePath";
		this.TextureRelativePath.Path = "";
		this.TextureRelativePath.SaveToConfig = true;
		this.TextureRelativePath.Size = new System.Drawing.Size(342, 24);
		this.TextureRelativePath.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "ExportRelativePathControl";
		base.Size = new System.Drawing.Size(564, 134);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	public ExportRelativePathControl()
	{
		InitializeComponent();
	}

	private void TextureRelativeToggle_CheckedChanged(object sender, EventArgs e)
	{
		TextureRelativePath.Enabled = TextureRelativeToggle.Checked;
	}

	private void PropRelativeToggle_CheckedChanged(object sender, EventArgs e)
	{
		ReferenceRelativePath.Enabled = PropRelativeToggle.Checked;
	}

	private void AllRelativeToExportPath_CheckedChanged(object sender, EventArgs e)
	{
		if (!AllRelativeToExportPath.Checked)
		{
			TextureRelativeToggle.Enabled = true;
			PropRelativeToggle.Enabled = true;
			TextureRelativePath.Enabled = TextureRelativeToggle.Checked;
			ReferenceRelativePath.Enabled = PropRelativeToggle.Checked;
		}
		else
		{
			TextureRelativeToggle.Enabled = false;
			PropRelativeToggle.Enabled = false;
			TextureRelativePath.Enabled = false;
			ReferenceRelativePath.Enabled = false;
		}
	}
}
