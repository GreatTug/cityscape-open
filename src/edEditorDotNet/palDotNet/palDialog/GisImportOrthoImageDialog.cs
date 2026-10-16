using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edGISBorkDialogs\\GisImportOrthoImageDialog", Win32PCOnly = true)]
[DialogBoxCreator]
public class GisImportOrthoImageDialog : DialogBoxBase
{
	private IContainer components;

	private Button CancelBtn;

	private Button OKBtn;

	private GroupBox groupBox1;

	private DialogPathLoader OrthoImagePathInput;

	private DialogLabel OrthoImagePathInfoLabel;

	private DialogLabel MaxImageSizeLabel;

	public GisImportOrthoImageDialog()
	{
		InitializeComponent();
		OrthoImagePathInput.TextBox.TextChanged += OrthoImageFilePathTextBox_TextChanged;
		SyncOkButton();
	}

	public void SetMaxImageSize(int maxWidth, int maxHeight)
	{
		int num = (int)Math.Floor((float)maxWidth / 1024f);
		int num2 = (int)Math.Floor((float)maxHeight / 1024f);
		MaxImageSizeLabel.Text = "Maximum: " + num + "K × " + num2 + "K";
	}

	private void OrthoImageFilePathTextBox_TextChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private bool IsValidInput()
	{
		if (string.IsNullOrEmpty(OrthoImagePathInput.TextBox.Text))
		{
			return false;
		}
		return true;
	}

	private void SyncOkButton()
	{
		AllowSubmitDialog = IsValidInput();
		OKBtn.Enabled = AllowSubmitDialog;
	}

	private void OKBtn_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
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
		this.CancelBtn = new System.Windows.Forms.Button();
		this.OKBtn = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.MaxImageSizeLabel = new palDotNet.palDialog.DialogLabel();
		this.OrthoImagePathInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.OrthoImagePathInput = new palDotNet.palDialog.DialogPathLoader();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CancelBtn.Location = new System.Drawing.Point(397, 149);
		this.CancelBtn.Name = "CancelBtn";
		this.CancelBtn.Size = new System.Drawing.Size(75, 23);
		this.CancelBtn.TabIndex = 2;
		this.CancelBtn.Text = "Cancel";
		this.CancelBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKBtn.Location = new System.Drawing.Point(316, 149);
		this.OKBtn.Name = "OKBtn";
		this.OKBtn.Size = new System.Drawing.Size(75, 23);
		this.OKBtn.TabIndex = 1;
		this.OKBtn.Text = "OK";
		this.OKBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Click += new System.EventHandler(OKBtn_Click);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.MaxImageSizeLabel);
		this.groupBox1.Controls.Add(this.OrthoImagePathInfoLabel);
		this.groupBox1.Controls.Add(this.OrthoImagePathInput);
		this.groupBox1.Location = new System.Drawing.Point(13, 13);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(459, 112);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "OrthoImage file";
		this.MaxImageSizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.MaxImageSizeLabel.Location = new System.Drawing.Point(315, 33);
		this.MaxImageSizeLabel.Name = "MaxImageSizeLabel";
		this.MaxImageSizeLabel.Size = new System.Drawing.Size(122, 13);
		this.MaxImageSizeLabel.TabIndex = 5;
		this.MaxImageSizeLabel.Text = "Maximum: --K x --K";
		this.MaxImageSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.OrthoImagePathInfoLabel.AutoSize = true;
		this.OrthoImagePathInfoLabel.Location = new System.Drawing.Point(22, 33);
		this.OrthoImagePathInfoLabel.Name = "OrthoImagePathInfoLabel";
		this.OrthoImagePathInfoLabel.Size = new System.Drawing.Size(172, 13);
		this.OrthoImagePathInfoLabel.TabIndex = 0;
		this.OrthoImagePathInfoLabel.Text = "Specify the OrthoImage file to load:";
		this.OrthoImagePathInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.OrthoImagePathInput.BackColor = System.Drawing.Color.Transparent;
		this.OrthoImagePathInput.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.OrthoImagePathInput.CheckFileExists = true;
		this.OrthoImagePathInput.Description = null;
		this.OrthoImagePathInput.Filter = "GeoTiff file (*.tif)|*.tif";
		this.OrthoImagePathInput.Location = new System.Drawing.Point(25, 66);
		this.OrthoImagePathInput.Name = "OrthoImagePathInput";
		this.OrthoImagePathInput.Path = "";
		this.OrthoImagePathInput.Size = new System.Drawing.Size(412, 24);
		this.OrthoImagePathInput.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CancelBtn;
		base.ClientSize = new System.Drawing.Size(484, 184);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.OKBtn);
		base.Controls.Add(this.CancelBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GisImportOrthoImageDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "GIS Mode - Import OrthoImage";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
