using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edOverlay\\OverlayImageDialog", Win32PCOnly = true)]
public class OverlayImageDialog : DialogBoxBase
{
	private IContainer components;

	private Button CancelBtn;

	private Button OKBtn;

	private GroupBox OverlayImageFileGroup;

	private DialogLabel ImageOffsetXLabel;

	private Label LoadOverlayImageInfoLabel;

	private DialogPathLoader ImageOverlayFilePathInput;

	private DialogLabel ImageOffsetYLabel;

	private DialogNumericUpDownFloat ImageOffsetYInput;

	private DialogNumericUpDownFloat ImageOffsetXInput;

	private DialogNumericUpDownFloat ImageScaleXInput;

	private DialogLabel ImageScaleXLabel;

	private DialogLabel ImageScaleYLabel;

	private DialogNumericUpDownFloat ImageScaleYInput;

	private DialogLabel InfoLabel;

	private DialogLabel MaxImageSizeLabel;

	public string ImageOverlayFilePath
	{
		get
		{
			return ImageOverlayFilePathInput.Path;
		}
		set
		{
			ImageOverlayFilePathInput.Path = value;
		}
	}

	public string ImageOverlayFilePathFilter
	{
		set
		{
			ImageOverlayFilePathInput.Filter = value;
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
		this.CancelBtn = new System.Windows.Forms.Button();
		this.OKBtn = new System.Windows.Forms.Button();
		this.OverlayImageFileGroup = new System.Windows.Forms.GroupBox();
		this.MaxImageSizeLabel = new palDotNet.palDialog.DialogLabel();
		this.InfoLabel = new palDotNet.palDialog.DialogLabel();
		this.ImageScaleYLabel = new palDotNet.palDialog.DialogLabel();
		this.ImageScaleYInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.ImageScaleXLabel = new palDotNet.palDialog.DialogLabel();
		this.ImageScaleXInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.ImageOffsetYInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.ImageOffsetXInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.ImageOffsetYLabel = new palDotNet.palDialog.DialogLabel();
		this.ImageOffsetXLabel = new palDotNet.palDialog.DialogLabel();
		this.LoadOverlayImageInfoLabel = new System.Windows.Forms.Label();
		this.ImageOverlayFilePathInput = new palDotNet.palDialog.DialogPathLoader();
		this.OverlayImageFileGroup.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ImageScaleYInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ImageScaleXInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ImageOffsetYInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ImageOffsetXInput).BeginInit();
		base.SuspendLayout();
		this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CancelBtn.Location = new System.Drawing.Point(407, 239);
		this.CancelBtn.Name = "CancelBtn";
		this.CancelBtn.Size = new System.Drawing.Size(75, 23);
		this.CancelBtn.TabIndex = 2;
		this.CancelBtn.Text = "Cancel";
		this.CancelBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKBtn.Location = new System.Drawing.Point(326, 239);
		this.OKBtn.Name = "OKBtn";
		this.OKBtn.Size = new System.Drawing.Size(75, 23);
		this.OKBtn.TabIndex = 1;
		this.OKBtn.Text = "OK";
		this.OKBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Click += new System.EventHandler(OKBtn_Click);
		this.OverlayImageFileGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.OverlayImageFileGroup.Controls.Add(this.MaxImageSizeLabel);
		this.OverlayImageFileGroup.Controls.Add(this.InfoLabel);
		this.OverlayImageFileGroup.Controls.Add(this.ImageScaleYLabel);
		this.OverlayImageFileGroup.Controls.Add(this.ImageScaleYInput);
		this.OverlayImageFileGroup.Controls.Add(this.ImageScaleXLabel);
		this.OverlayImageFileGroup.Controls.Add(this.ImageScaleXInput);
		this.OverlayImageFileGroup.Controls.Add(this.ImageOffsetYInput);
		this.OverlayImageFileGroup.Controls.Add(this.ImageOffsetXInput);
		this.OverlayImageFileGroup.Controls.Add(this.ImageOffsetYLabel);
		this.OverlayImageFileGroup.Controls.Add(this.ImageOffsetXLabel);
		this.OverlayImageFileGroup.Controls.Add(this.LoadOverlayImageInfoLabel);
		this.OverlayImageFileGroup.Controls.Add(this.ImageOverlayFilePathInput);
		this.OverlayImageFileGroup.Location = new System.Drawing.Point(13, 13);
		this.OverlayImageFileGroup.Name = "OverlayImageFileGroup";
		this.OverlayImageFileGroup.Size = new System.Drawing.Size(469, 200);
		this.OverlayImageFileGroup.TabIndex = 0;
		this.OverlayImageFileGroup.TabStop = false;
		this.OverlayImageFileGroup.Text = "Overlay image";
		this.MaxImageSizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.MaxImageSizeLabel.Location = new System.Drawing.Point(320, 29);
		this.MaxImageSizeLabel.Name = "MaxImageSizeLabel";
		this.MaxImageSizeLabel.Size = new System.Drawing.Size(122, 13);
		this.MaxImageSizeLabel.TabIndex = 4;
		this.MaxImageSizeLabel.Text = "Maximum: --K x --K";
		this.MaxImageSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.InfoLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.InfoLabel.Location = new System.Drawing.Point(23, 168);
		this.InfoLabel.Name = "InfoLabel";
		this.InfoLabel.Size = new System.Drawing.Size(419, 20);
		this.InfoLabel.TabIndex = 10;
		this.InfoLabel.Text = "NOTE: (meters/pixel) refers to meters in the terrain and pixels of the source image.";
		this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ImageScaleYLabel.AutoSize = true;
		this.ImageScaleYLabel.Location = new System.Drawing.Point(234, 136);
		this.ImageScaleYLabel.Name = "ImageScaleYLabel";
		this.ImageScaleYLabel.Size = new System.Drawing.Size(113, 13);
		this.ImageScaleYLabel.TabIndex = 8;
		this.ImageScaleYLabel.Text = "Scale Y (meters/pixel):";
		this.ImageScaleYInput.DecimalPlaces = 6;
		this.ImageScaleYInput.Location = new System.Drawing.Point(362, 134);
		this.ImageScaleYInput.Name = "ImageScaleYInput";
		this.ImageScaleYInput.Size = new System.Drawing.Size(80, 20);
		this.ImageScaleYInput.TabIndex = 9;
		this.ImageScaleXLabel.AutoSize = true;
		this.ImageScaleXLabel.Location = new System.Drawing.Point(234, 106);
		this.ImageScaleXLabel.Name = "ImageScaleXLabel";
		this.ImageScaleXLabel.Size = new System.Drawing.Size(113, 13);
		this.ImageScaleXLabel.TabIndex = 6;
		this.ImageScaleXLabel.Text = "Scale X (meters/pixel):";
		this.ImageScaleXInput.DecimalPlaces = 6;
		this.ImageScaleXInput.Location = new System.Drawing.Point(362, 104);
		this.ImageScaleXInput.Name = "ImageScaleXInput";
		this.ImageScaleXInput.Size = new System.Drawing.Size(80, 20);
		this.ImageScaleXInput.TabIndex = 7;
		this.ImageOffsetYInput.DecimalPlaces = 2;
		this.ImageOffsetYInput.Location = new System.Drawing.Point(129, 134);
		this.ImageOffsetYInput.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.ImageOffsetYInput.Minimum = new decimal(new int[4] { 100000, 0, 0, -2147483648 });
		this.ImageOffsetYInput.Name = "ImageOffsetYInput";
		this.ImageOffsetYInput.Size = new System.Drawing.Size(80, 20);
		this.ImageOffsetYInput.TabIndex = 5;
		this.ImageOffsetXInput.DecimalPlaces = 2;
		this.ImageOffsetXInput.Location = new System.Drawing.Point(129, 104);
		this.ImageOffsetXInput.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.ImageOffsetXInput.Minimum = new decimal(new int[4] { 100000, 0, 0, -2147483648 });
		this.ImageOffsetXInput.Name = "ImageOffsetXInput";
		this.ImageOffsetXInput.Size = new System.Drawing.Size(80, 20);
		this.ImageOffsetXInput.TabIndex = 3;
		this.ImageOffsetYLabel.AutoSize = true;
		this.ImageOffsetYLabel.Location = new System.Drawing.Point(23, 136);
		this.ImageOffsetYLabel.Name = "ImageOffsetYLabel";
		this.ImageOffsetYLabel.Size = new System.Drawing.Size(88, 13);
		this.ImageOffsetYLabel.TabIndex = 4;
		this.ImageOffsetYLabel.Text = "Offset Y (meters):";
		this.ImageOffsetXLabel.AutoSize = true;
		this.ImageOffsetXLabel.Location = new System.Drawing.Point(23, 106);
		this.ImageOffsetXLabel.Name = "ImageOffsetXLabel";
		this.ImageOffsetXLabel.Size = new System.Drawing.Size(88, 13);
		this.ImageOffsetXLabel.TabIndex = 2;
		this.ImageOffsetXLabel.Text = "Offset X (meters):";
		this.LoadOverlayImageInfoLabel.AutoSize = true;
		this.LoadOverlayImageInfoLabel.Location = new System.Drawing.Point(23, 29);
		this.LoadOverlayImageInfoLabel.Name = "LoadOverlayImageInfoLabel";
		this.LoadOverlayImageInfoLabel.Size = new System.Drawing.Size(129, 13);
		this.LoadOverlayImageInfoLabel.TabIndex = 0;
		this.LoadOverlayImageInfoLabel.Text = "Specify the image to load:";
		this.ImageOverlayFilePathInput.BackColor = System.Drawing.Color.Transparent;
		this.ImageOverlayFilePathInput.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.ImageOverlayFilePathInput.CheckFileExists = true;
		this.ImageOverlayFilePathInput.Description = null;
		this.ImageOverlayFilePathInput.Filter = null;
		this.ImageOverlayFilePathInput.Location = new System.Drawing.Point(26, 54);
		this.ImageOverlayFilePathInput.Name = "ImageOverlayFilePathInput";
		this.ImageOverlayFilePathInput.Path = "";
		this.ImageOverlayFilePathInput.Size = new System.Drawing.Size(416, 24);
		this.ImageOverlayFilePathInput.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CancelBtn;
		base.ClientSize = new System.Drawing.Size(494, 274);
		base.Controls.Add(this.OverlayImageFileGroup);
		base.Controls.Add(this.OKBtn);
		base.Controls.Add(this.CancelBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "OverlayImageDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Overlay Image";
		this.OverlayImageFileGroup.ResumeLayout(false);
		this.OverlayImageFileGroup.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ImageScaleYInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ImageScaleXInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ImageOffsetYInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ImageOffsetXInput).EndInit();
		base.ResumeLayout(false);
	}

	public OverlayImageDialog()
	{
		InitializeComponent();
		ImageOverlayFilePath = "";
		SyncOkButton();
	}

	public void SetMaxImageSize(int maxWidth, int maxHeight)
	{
		int num = (int)Math.Floor((float)maxWidth / 1024f);
		int num2 = (int)Math.Floor((float)maxHeight / 1024f);
		MaxImageSizeLabel.Text = "Maximum: " + num + "K × " + num2 + "K";
	}

	public DialogPathLoader GetImageOverlayFilePath()
	{
		return ImageOverlayFilePathInput;
	}

	private void SyncOkButton()
	{
		AllowSubmitDialog = true;
		OKBtn.Enabled = AllowSubmitDialog;
	}

	private void OKBtn_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
