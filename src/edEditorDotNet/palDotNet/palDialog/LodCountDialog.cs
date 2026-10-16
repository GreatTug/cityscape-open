using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using edEditorDotNet.Properties;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edLod\\LodCountDialog")]
[DialogBoxCreator]
public class LodCountDialog : DialogBoxBase
{
	private IContainer components;

	private Button mOkButton;

	private Button mCancelButton;

	private Label LodCountLabel;

	private Label LodCountWarningLabel;

	private PictureBox mWarningIcon;

	private Panel WarningPanel;

	private Label LodCountRangeLabel;

	private DialogNumericUpDownInt LodCountInput;

	private int LodCount;

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
		this.mOkButton = new System.Windows.Forms.Button();
		this.mCancelButton = new System.Windows.Forms.Button();
		this.LodCountLabel = new System.Windows.Forms.Label();
		this.LodCountWarningLabel = new System.Windows.Forms.Label();
		this.mWarningIcon = new System.Windows.Forms.PictureBox();
		this.WarningPanel = new System.Windows.Forms.Panel();
		this.LodCountRangeLabel = new System.Windows.Forms.Label();
		this.LodCountInput = new palDotNet.palDialog.DialogNumericUpDownInt();
		((System.ComponentModel.ISupportInitialize)this.mWarningIcon).BeginInit();
		this.WarningPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.LodCountInput).BeginInit();
		base.SuspendLayout();
		this.mOkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mOkButton.Location = new System.Drawing.Point(226, 150);
		this.mOkButton.Name = "mOkButton";
		this.mOkButton.Size = new System.Drawing.Size(75, 23);
		this.mOkButton.TabIndex = 4;
		this.mOkButton.Text = "OK";
		this.mOkButton.UseVisualStyleBackColor = true;
		this.mOkButton.Click += new System.EventHandler(mOkButton_Click);
		this.mCancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.mCancelButton.Location = new System.Drawing.Point(307, 150);
		this.mCancelButton.Name = "mCancelButton";
		this.mCancelButton.Size = new System.Drawing.Size(75, 23);
		this.mCancelButton.TabIndex = 5;
		this.mCancelButton.Text = "Cancel";
		this.mCancelButton.UseVisualStyleBackColor = true;
		this.LodCountLabel.AutoSize = true;
		this.LodCountLabel.Location = new System.Drawing.Point(25, 85);
		this.LodCountLabel.Name = "LodCountLabel";
		this.LodCountLabel.Size = new System.Drawing.Size(62, 13);
		this.LodCountLabel.TabIndex = 1;
		this.LodCountLabel.Text = "LOD count:";
		this.LodCountWarningLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LodCountWarningLabel.Location = new System.Drawing.Point(56, 0);
		this.LodCountWarningLabel.Name = "LodCountWarningLabel";
		this.LodCountWarningLabel.Size = new System.Drawing.Size(282, 49);
		this.LodCountWarningLabel.TabIndex = 0;
		this.LodCountWarningLabel.Text = "Decreasing the LOD count will discard the properties of the lower LODs.";
		this.LodCountWarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.mWarningIcon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.mWarningIcon.Image = edEditorDotNet.Properties.Resources.Warning32;
		this.mWarningIcon.Location = new System.Drawing.Point(9, 9);
		this.mWarningIcon.Name = "mWarningIcon";
		this.mWarningIcon.Size = new System.Drawing.Size(32, 30);
		this.mWarningIcon.TabIndex = 5;
		this.mWarningIcon.TabStop = false;
		this.WarningPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.WarningPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.WarningPanel.Controls.Add(this.LodCountWarningLabel);
		this.WarningPanel.Controls.Add(this.mWarningIcon);
		this.WarningPanel.Location = new System.Drawing.Point(18, 12);
		this.WarningPanel.Name = "WarningPanel";
		this.WarningPanel.Size = new System.Drawing.Size(355, 50);
		this.WarningPanel.TabIndex = 0;
		this.LodCountRangeLabel.AutoSize = true;
		this.LodCountRangeLabel.Location = new System.Drawing.Point(223, 85);
		this.LodCountRangeLabel.Name = "LodCountRangeLabel";
		this.LodCountRangeLabel.Size = new System.Drawing.Size(106, 13);
		this.LodCountRangeLabel.TabIndex = 3;
		this.LodCountRangeLabel.Text = "Must have at least 1.";
		this.LodCountInput.Location = new System.Drawing.Point(120, 83);
		this.LodCountInput.Maximum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.LodCountInput.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.LodCountInput.Name = "LodCountInput";
		this.LodCountInput.Size = new System.Drawing.Size(80, 20);
		this.LodCountInput.TabIndex = 2;
		this.LodCountInput.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.LodCountInput.ValueChanged += new System.EventHandler(LodCountInput_ValueChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.mCancelButton;
		base.ClientSize = new System.Drawing.Size(394, 185);
		base.Controls.Add(this.LodCountInput);
		base.Controls.Add(this.LodCountRangeLabel);
		base.Controls.Add(this.WarningPanel);
		base.Controls.Add(this.LodCountLabel);
		base.Controls.Add(this.mCancelButton);
		base.Controls.Add(this.mOkButton);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "LodCountDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Set LOD Count";
		((System.ComponentModel.ISupportInitialize)this.mWarningIcon).EndInit();
		this.WarningPanel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.LodCountInput).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public LodCountDialog()
	{
		InitializeComponent();
		SyncOkButton();
	}

	public void Init(int lodCount)
	{
		LodCount = lodCount;
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		LodCountInput.Value = LodCount;
	}

	public int GetLodCount()
	{
		return LodCount;
	}

	private void LodCountInput_ValueChanged(object sender, EventArgs e)
	{
		LodCount = (int)LodCountInput.Value;
	}

	private void SyncOkButton()
	{
		AllowSubmitDialog = true;
		mOkButton.Enabled = AllowSubmitDialog;
	}

	private void mOkButton_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
