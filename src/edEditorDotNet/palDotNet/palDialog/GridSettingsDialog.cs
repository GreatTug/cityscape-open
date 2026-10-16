using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edDialogs\\GridSettingsDialog")]
public class GridSettingsDialog : DialogBoxBase
{
	private IContainer components;

	private Button cancelBtn;

	private Button okBtn;

	private DialogNumericUpDownFloat GridSpacingInput;

	private DialogNumericUpDownFloat ContourSpacingInput;

	private DialogLabel GridSpacingLabel;

	private DialogLabel ContourSpacingLabel;

	private DialogCheckBox ContourLineFadeCheckBox;

	private DialogCheckGroupBox EnableGridCheckGroupBox;

	private DialogCheckGroupBox EnableContoursCheckGroupBox;

	public GridSettingsDialog()
	{
		InitializeComponent();
		SyncOkButton();
	}

	private void SyncOkButton()
	{
		AllowSubmitDialog = true;
		okBtn.Enabled = AllowSubmitDialog;
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
		this.cancelBtn = new System.Windows.Forms.Button();
		this.okBtn = new System.Windows.Forms.Button();
		this.GridSpacingInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.ContourSpacingInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.GridSpacingLabel = new palDotNet.palDialog.DialogLabel();
		this.ContourSpacingLabel = new palDotNet.palDialog.DialogLabel();
		this.ContourLineFadeCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.EnableGridCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.EnableContoursCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		((System.ComponentModel.ISupportInitialize)this.GridSpacingInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ContourSpacingInput).BeginInit();
		this.EnableGridCheckGroupBox.SuspendLayout();
		this.EnableContoursCheckGroupBox.SuspendLayout();
		base.SuspendLayout();
		this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelBtn.Location = new System.Drawing.Point(239, 219);
		this.cancelBtn.Name = "cancelBtn";
		this.cancelBtn.Size = new System.Drawing.Size(75, 23);
		this.cancelBtn.TabIndex = 3;
		this.cancelBtn.Text = "Cancel";
		this.cancelBtn.UseVisualStyleBackColor = true;
		this.okBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okBtn.Location = new System.Drawing.Point(158, 219);
		this.okBtn.Name = "okBtn";
		this.okBtn.Size = new System.Drawing.Size(75, 23);
		this.okBtn.TabIndex = 2;
		this.okBtn.Text = "OK";
		this.okBtn.UseVisualStyleBackColor = true;
		this.okBtn.Click += new System.EventHandler(OKBtn_Click);
		this.GridSpacingInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.GridSpacingInput.DecimalPlaces = 1;
		this.GridSpacingInput.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.GridSpacingInput.Location = new System.Drawing.Point(188, 33);
		this.GridSpacingInput.Minimum = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.GridSpacingInput.Name = "GridSpacingInput";
		this.GridSpacingInput.Size = new System.Drawing.Size(80, 20);
		this.GridSpacingInput.TabIndex = 1;
		this.GridSpacingInput.Value = new decimal(new int[4] { 10, 0, 0, 0 });
		this.ContourSpacingInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ContourSpacingInput.DecimalPlaces = 1;
		this.ContourSpacingInput.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.ContourSpacingInput.Location = new System.Drawing.Point(188, 33);
		this.ContourSpacingInput.Minimum = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.ContourSpacingInput.Name = "ContourSpacingInput";
		this.ContourSpacingInput.Size = new System.Drawing.Size(80, 20);
		this.ContourSpacingInput.TabIndex = 1;
		this.ContourSpacingInput.Value = new decimal(new int[4] { 10, 0, 0, 0 });
		this.GridSpacingLabel.AutoSize = true;
		this.GridSpacingLabel.Location = new System.Drawing.Point(29, 35);
		this.GridSpacingLabel.Name = "GridSpacingLabel";
		this.GridSpacingLabel.Size = new System.Drawing.Size(69, 13);
		this.GridSpacingLabel.TabIndex = 0;
		this.GridSpacingLabel.Text = "Grid spacing:";
		this.ContourSpacingLabel.AutoSize = true;
		this.ContourSpacingLabel.Location = new System.Drawing.Point(29, 35);
		this.ContourSpacingLabel.Name = "ContourSpacingLabel";
		this.ContourSpacingLabel.Size = new System.Drawing.Size(87, 13);
		this.ContourSpacingLabel.TabIndex = 0;
		this.ContourSpacingLabel.Text = "Contour spacing:";
		this.ContourLineFadeCheckBox.AutoSize = true;
		this.ContourLineFadeCheckBox.Location = new System.Drawing.Point(32, 68);
		this.ContourLineFadeCheckBox.Name = "ContourLineFadeCheckBox";
		this.ContourLineFadeCheckBox.Size = new System.Drawing.Size(161, 17);
		this.ContourLineFadeCheckBox.TabIndex = 2;
		this.ContourLineFadeCheckBox.Text = "Fade contour lines by normal";
		this.ContourLineFadeCheckBox.UseVisualStyleBackColor = true;
		this.EnableGridCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.EnableGridCheckGroupBox.Controls.Add(this.GridSpacingLabel);
		this.EnableGridCheckGroupBox.Controls.Add(this.GridSpacingInput);
		this.EnableGridCheckGroupBox.Location = new System.Drawing.Point(12, 12);
		this.EnableGridCheckGroupBox.Name = "EnableGridCheckGroupBox";
		this.EnableGridCheckGroupBox.Size = new System.Drawing.Size(301, 73);
		this.EnableGridCheckGroupBox.TabIndex = 0;
		this.EnableGridCheckGroupBox.TabStop = false;
		this.EnableGridCheckGroupBox.Text = "Enable grid";
		this.EnableContoursCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.EnableContoursCheckGroupBox.Controls.Add(this.ContourSpacingLabel);
		this.EnableContoursCheckGroupBox.Controls.Add(this.ContourSpacingInput);
		this.EnableContoursCheckGroupBox.Controls.Add(this.ContourLineFadeCheckBox);
		this.EnableContoursCheckGroupBox.Location = new System.Drawing.Point(12, 91);
		this.EnableContoursCheckGroupBox.Name = "EnableContoursCheckGroupBox";
		this.EnableContoursCheckGroupBox.Size = new System.Drawing.Size(301, 104);
		this.EnableContoursCheckGroupBox.TabIndex = 1;
		this.EnableContoursCheckGroupBox.TabStop = false;
		this.EnableContoursCheckGroupBox.Text = "Enable contours";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.ClientSize = new System.Drawing.Size(326, 254);
		base.Controls.Add(this.EnableContoursCheckGroupBox);
		base.Controls.Add(this.EnableGridCheckGroupBox);
		base.Controls.Add(this.cancelBtn);
		base.Controls.Add(this.okBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GridSettingsDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Grid Settings";
		((System.ComponentModel.ISupportInitialize)this.GridSpacingInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ContourSpacingInput).EndInit();
		this.EnableGridCheckGroupBox.ResumeLayout(false);
		this.EnableGridCheckGroupBox.PerformLayout();
		this.EnableContoursCheckGroupBox.ResumeLayout(false);
		this.EnableContoursCheckGroupBox.PerformLayout();
		base.ResumeLayout(false);
	}
}
