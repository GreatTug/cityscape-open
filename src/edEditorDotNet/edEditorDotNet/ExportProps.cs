using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportProps : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox propOptions;

	private DialogPathLoader ReferenceOutputPath;

	private DialogLabel ReferenceFolderLabel;

	private DialogRadioButton ReferenceFilesSame;

	private DialogCheckBox ReferenceFilesDifferent;

	private DialogRadioButton ReferenceFilesCopySame;

	private DialogCheckBox EmbedProps;

	private CheckBox overwriteProps;

	private DialogCheckBox ReferenceOutputPathUseExportPath;

	private GroupBox groupBox1;

	private bool embedEnable;

	private bool diffEnable;

	private bool sameEnable;

	private bool sameCopyEnable;

	public DialogCheckBox EmbedPropsCheckBox => EmbedProps;

	public DialogCheckBox ReferenceFilesDifferentCheckBox => ReferenceFilesDifferent;

	public DialogRadioButton ReferenceFilesSameRadioButton => ReferenceFilesSame;

	public DialogRadioButton ReferenceFilesCopySameRadioButton => ReferenceFilesCopySame;

	public CheckBox ReferenceOutputPathUseExportPathCheckBox => ReferenceOutputPathUseExportPath;

	public CheckBox OverwritePropsCheckBox => overwriteProps;

	public bool ForceEmbeddedProps
	{
		set
		{
			SuspendLayout();
			if (value)
			{
				EmbedProps.Enabled = false;
				ReferenceFilesDifferent.Enabled = false;
				ReferenceFilesSame.Enabled = false;
				ReferenceFilesCopySame.Enabled = false;
				EmbedProps.Checked = true;
				DialogCheckBox referenceFilesDifferent = ReferenceFilesDifferent;
				bool flag = (ReferenceFilesSame.Checked = true);
				referenceFilesDifferent.Checked = flag;
				ReferenceFilesCopySame.Checked = false;
			}
			SetReferencePathEnable();
			ResumeLayout(performLayout: true);
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
		this.propOptions = new System.Windows.Forms.GroupBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ReferenceFilesSame = new palDotNet.palDialog.DialogRadioButton();
		this.ReferenceFilesCopySame = new palDotNet.palDialog.DialogRadioButton();
		this.ReferenceOutputPathUseExportPath = new palDotNet.palDialog.DialogCheckBox();
		this.overwriteProps = new System.Windows.Forms.CheckBox();
		this.EmbedProps = new palDotNet.palDialog.DialogCheckBox();
		this.ReferenceFilesDifferent = new palDotNet.palDialog.DialogCheckBox();
		this.ReferenceFolderLabel = new palDotNet.palDialog.DialogLabel();
		this.ReferenceOutputPath = new palDotNet.palDialog.DialogPathLoader();
		this.propOptions.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.propOptions.Controls.Add(this.groupBox1);
		this.propOptions.Controls.Add(this.ReferenceOutputPathUseExportPath);
		this.propOptions.Controls.Add(this.overwriteProps);
		this.propOptions.Controls.Add(this.EmbedProps);
		this.propOptions.Controls.Add(this.ReferenceFilesDifferent);
		this.propOptions.Controls.Add(this.ReferenceFolderLabel);
		this.propOptions.Controls.Add(this.ReferenceOutputPath);
		this.propOptions.Dock = System.Windows.Forms.DockStyle.Fill;
		this.propOptions.Location = new System.Drawing.Point(0, 0);
		this.propOptions.Name = "propOptions";
		this.propOptions.Size = new System.Drawing.Size(360, 200);
		this.propOptions.TabIndex = 22;
		this.propOptions.TabStop = false;
		this.propOptions.Text = "Prop options";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.ReferenceFilesSame);
		this.groupBox1.Controls.Add(this.ReferenceFilesCopySame);
		this.groupBox1.Location = new System.Drawing.Point(25, 65);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(329, 39);
		this.groupBox1.TabIndex = 12;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "For props with same type as export type:";
		this.ReferenceFilesSame.AutoSize = true;
		this.ReferenceFilesSame.Location = new System.Drawing.Point(6, 16);
		this.ReferenceFilesSame.Name = "ReferenceFilesSame";
		this.ReferenceFilesSame.SaveToConfig = true;
		this.ReferenceFilesSame.Size = new System.Drawing.Size(55, 17);
		this.ReferenceFilesSame.TabIndex = 2;
		this.ReferenceFilesSame.TabStop = true;
		this.ReferenceFilesSame.Text = "Export";
		this.ReferenceFilesSame.UseVisualStyleBackColor = true;
		this.ReferenceFilesSame.CheckedChanged += new System.EventHandler(ReferenceFilesSame_CheckedChanged);
		this.ReferenceFilesCopySame.AutoSize = true;
		this.ReferenceFilesCopySame.Location = new System.Drawing.Point(68, 16);
		this.ReferenceFilesCopySame.Name = "ReferenceFilesCopySame";
		this.ReferenceFilesCopySame.SaveToConfig = true;
		this.ReferenceFilesCopySame.Size = new System.Drawing.Size(85, 17);
		this.ReferenceFilesCopySame.TabIndex = 3;
		this.ReferenceFilesCopySame.TabStop = true;
		this.ReferenceFilesCopySame.Text = "Copy original";
		this.ReferenceFilesCopySame.UseVisualStyleBackColor = true;
		this.ReferenceFilesCopySame.CheckedChanged += new System.EventHandler(ReferenceFilesCopySame_CheckedChanged);
		this.ReferenceOutputPathUseExportPath.AutoSize = true;
		this.ReferenceOutputPathUseExportPath.Checked = true;
		this.ReferenceOutputPathUseExportPath.CheckState = System.Windows.Forms.CheckState.Checked;
		this.ReferenceOutputPathUseExportPath.Location = new System.Drawing.Point(25, 123);
		this.ReferenceOutputPathUseExportPath.Name = "ReferenceOutputPathUseExportPath";
		this.ReferenceOutputPathUseExportPath.SaveToConfig = true;
		this.ReferenceOutputPathUseExportPath.Size = new System.Drawing.Size(101, 17);
		this.ReferenceOutputPathUseExportPath.TabIndex = 4;
		this.ReferenceOutputPathUseExportPath.Text = "Use export path";
		this.ReferenceOutputPathUseExportPath.UseVisualStyleBackColor = true;
		this.ReferenceOutputPathUseExportPath.CheckedChanged += new System.EventHandler(TexturePathUseExportPath_CheckedChanged);
		this.overwriteProps.AutoSize = true;
		this.overwriteProps.Location = new System.Drawing.Point(6, 177);
		this.overwriteProps.Name = "overwriteProps";
		this.overwriteProps.Size = new System.Drawing.Size(130, 17);
		this.overwriteProps.TabIndex = 10;
		this.overwriteProps.Text = "Overwrite existing files";
		this.overwriteProps.UseVisualStyleBackColor = true;
		this.overwriteProps.Visible = false;
		this.EmbedProps.AutoSize = true;
		this.EmbedProps.Location = new System.Drawing.Point(6, 19);
		this.EmbedProps.Name = "EmbedProps";
		this.EmbedProps.SaveToConfig = true;
		this.EmbedProps.Size = new System.Drawing.Size(116, 17);
		this.EmbedProps.TabIndex = 0;
		this.EmbedProps.Text = "Embed props to file";
		this.EmbedProps.UseVisualStyleBackColor = true;
		this.EmbedProps.CheckedChanged += new System.EventHandler(EmbedProps_CheckedChanged);
		this.ReferenceFilesDifferent.AutoSize = true;
		this.ReferenceFilesDifferent.Checked = true;
		this.ReferenceFilesDifferent.CheckState = System.Windows.Forms.CheckState.Checked;
		this.ReferenceFilesDifferent.Location = new System.Drawing.Point(25, 42);
		this.ReferenceFilesDifferent.Name = "ReferenceFilesDifferent";
		this.ReferenceFilesDifferent.SaveToConfig = true;
		this.ReferenceFilesDifferent.Size = new System.Drawing.Size(177, 17);
		this.ReferenceFilesDifferent.TabIndex = 1;
		this.ReferenceFilesDifferent.Text = "Export props of different file type";
		this.ReferenceFilesDifferent.UseVisualStyleBackColor = true;
		this.ReferenceFilesDifferent.CheckedChanged += new System.EventHandler(ReferenceFilesDifferent_CheckedChanged);
		this.ReferenceFolderLabel.AutoSize = true;
		this.ReferenceFolderLabel.Location = new System.Drawing.Point(7, 107);
		this.ReferenceFolderLabel.Name = "ReferenceFolderLabel";
		this.ReferenceFolderLabel.SaveToConfig = false;
		this.ReferenceFolderLabel.Size = new System.Drawing.Size(71, 13);
		this.ReferenceFolderLabel.TabIndex = 5;
		this.ReferenceFolderLabel.Text = "Output folder:";
		this.ReferenceOutputPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ReferenceOutputPath.BackColor = System.Drawing.Color.Transparent;
		this.ReferenceOutputPath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eDirectories;
		this.ReferenceOutputPath.CheckFileExists = false;
		this.ReferenceOutputPath.Description = null;
		this.ReferenceOutputPath.Filter = null;
		this.ReferenceOutputPath.Location = new System.Drawing.Point(21, 141);
		this.ReferenceOutputPath.Name = "ReferenceOutputPath";
		this.ReferenceOutputPath.Path = "";
		this.ReferenceOutputPath.SaveToConfig = true;
		this.ReferenceOutputPath.Size = new System.Drawing.Size(334, 24);
		this.ReferenceOutputPath.TabIndex = 5;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.propOptions);
		base.Name = "ExportProps";
		base.Size = new System.Drawing.Size(360, 200);
		this.propOptions.ResumeLayout(false);
		this.propOptions.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	public ExportProps()
	{
		InitializeComponent();
	}

	public void UpdateUI()
	{
		embedEnable = EmbedProps.Enabled;
		diffEnable = ReferenceFilesDifferent.Enabled;
		sameEnable = ReferenceFilesSame.Enabled;
		sameCopyEnable = ReferenceFilesCopySame.Enabled;
		SetReferencePathEnable();
		EmbedProps_CheckedChanged(null, EventArgs.Empty);
	}

	private void SetReferencePathEnable()
	{
		bool flag = !EmbedProps.Checked && (ReferenceFilesDifferent.Checked || ReferenceFilesSame.Checked || ReferenceFilesCopySame.Checked);
		ReferenceOutputPathUseExportPath.Enabled = flag;
		ReferenceOutputPath.Enabled = flag && !ReferenceOutputPathUseExportPath.Checked;
	}

	private void ReferenceFilesDifferent_CheckedChanged(object sender, EventArgs e)
	{
		SetReferencePathEnable();
	}

	private void ReferenceFilesSame_CheckedChanged(object sender, EventArgs e)
	{
		if (ReferenceFilesSame.Checked)
		{
			ReferenceFilesCopySame.Checked = false;
		}
		SetReferencePathEnable();
	}

	private void ReferenceFilesCopySame_CheckedChanged(object sender, EventArgs e)
	{
		if (ReferenceFilesCopySame.Checked)
		{
			ReferenceFilesSame.Checked = false;
		}
		SetReferencePathEnable();
	}

	private void EmbedProps_CheckedChanged(object sender, EventArgs e)
	{
		SuspendLayout();
		if (EmbedProps.Checked)
		{
			DialogCheckBox referenceFilesDifferent = ReferenceFilesDifferent;
			bool flag = (ReferenceFilesSame.Checked = true);
			referenceFilesDifferent.Checked = flag;
		}
		groupBox1.Enabled = !EmbedProps.Checked;
		ReferenceFilesDifferent.Enabled = !EmbedProps.Checked && diffEnable;
		ReferenceFilesSame.Enabled = !EmbedProps.Checked && sameEnable;
		ReferenceFilesCopySame.Enabled = !EmbedProps.Checked && sameCopyEnable;
		SetReferencePathEnable();
		ResumeLayout();
	}

	private void TexturePathUseExportPath_CheckedChanged(object sender, EventArgs e)
	{
		SetReferencePathEnable();
	}
}
