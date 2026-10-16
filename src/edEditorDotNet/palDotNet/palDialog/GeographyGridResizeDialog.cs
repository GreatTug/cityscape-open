using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edGeography\\GeographyGridResizeDialog")]
public class GeographyGridResizeDialog : DialogBoxBase
{
	private IContainer components;

	private Button cancelBtn;

	private Button okBtn;

	private Label ResolutionXLabel;

	private Label ResolutionYLabel;

	private DialogCheckBox maintainAspect;

	private DialogComboBox resampling;

	private Label ResamplingLabel;

	private DialogLabel currentResolution;

	private Label CurrentResolutionLabel;

	private DialogNumericUpDownInt gridResolutionX;

	private DialogNumericUpDownInt gridResolutionY;

	private DialogNumericUpDownFloat resAspectRatio;

	private GroupBox resolutionBox;

	private DialogNumericUpDownFloat sizeAspectRatio;

	private DialogRadioButton growCanvas;

	private DialogRadioButton stretchCanvas;

	private Panel OperationPanel;

	private DialogLabel OperationLabel;

	private DialogAnchorPanel AnchorPanel;

	private Panel ResamplingPanel;

	private int mCurrentResX;

	private int mCurrentResY;

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
		this.ResolutionXLabel = new System.Windows.Forms.Label();
		this.ResolutionYLabel = new System.Windows.Forms.Label();
		this.maintainAspect = new palDotNet.palDialog.DialogCheckBox();
		this.resampling = new palDotNet.palDialog.DialogComboBox();
		this.ResamplingLabel = new System.Windows.Forms.Label();
		this.currentResolution = new palDotNet.palDialog.DialogLabel();
		this.CurrentResolutionLabel = new System.Windows.Forms.Label();
		this.gridResolutionX = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.gridResolutionY = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.resAspectRatio = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.resolutionBox = new System.Windows.Forms.GroupBox();
		this.OperationLabel = new palDotNet.palDialog.DialogLabel();
		this.OperationPanel = new System.Windows.Forms.Panel();
		this.ResamplingPanel = new System.Windows.Forms.Panel();
		this.AnchorPanel = new palDotNet.palDialog.DialogAnchorPanel();
		this.growCanvas = new palDotNet.palDialog.DialogRadioButton();
		this.stretchCanvas = new palDotNet.palDialog.DialogRadioButton();
		this.sizeAspectRatio = new palDotNet.palDialog.DialogNumericUpDownFloat();
		((System.ComponentModel.ISupportInitialize)this.gridResolutionX).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridResolutionY).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.resAspectRatio).BeginInit();
		this.resolutionBox.SuspendLayout();
		this.OperationPanel.SuspendLayout();
		this.ResamplingPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.sizeAspectRatio).BeginInit();
		base.SuspendLayout();
		this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelBtn.Location = new System.Drawing.Point(257, 444);
		this.cancelBtn.Name = "cancelBtn";
		this.cancelBtn.Size = new System.Drawing.Size(75, 23);
		this.cancelBtn.TabIndex = 4;
		this.cancelBtn.Text = "Cancel";
		this.cancelBtn.UseVisualStyleBackColor = true;
		this.okBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okBtn.Location = new System.Drawing.Point(176, 444);
		this.okBtn.Name = "okBtn";
		this.okBtn.Size = new System.Drawing.Size(75, 23);
		this.okBtn.TabIndex = 3;
		this.okBtn.Text = "OK";
		this.okBtn.UseVisualStyleBackColor = true;
		this.okBtn.Click += new System.EventHandler(OKBtn_Click);
		this.ResolutionXLabel.AutoSize = true;
		this.ResolutionXLabel.Location = new System.Drawing.Point(22, 56);
		this.ResolutionXLabel.Name = "ResolutionXLabel";
		this.ResolutionXLabel.Size = new System.Drawing.Size(65, 13);
		this.ResolutionXLabel.TabIndex = 2;
		this.ResolutionXLabel.Text = "X resolution:";
		this.ResolutionYLabel.AutoSize = true;
		this.ResolutionYLabel.Location = new System.Drawing.Point(22, 84);
		this.ResolutionYLabel.Name = "ResolutionYLabel";
		this.ResolutionYLabel.Size = new System.Drawing.Size(65, 13);
		this.ResolutionYLabel.TabIndex = 4;
		this.ResolutionYLabel.Text = "Y resolution:";
		this.maintainAspect.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.maintainAspect.Checked = true;
		this.maintainAspect.CheckState = System.Windows.Forms.CheckState.Checked;
		this.maintainAspect.Location = new System.Drawing.Point(164, 114);
		this.maintainAspect.Name = "maintainAspect";
		this.maintainAspect.Size = new System.Drawing.Size(139, 17);
		this.maintainAspect.TabIndex = 6;
		this.maintainAspect.Text = "Constrain proportions";
		this.maintainAspect.UseVisualStyleBackColor = true;
		this.maintainAspect.CheckedChanged += new System.EventHandler(maintainAspect_CheckedChanged);
		this.resampling.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.resampling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.resampling.FormattingEnabled = true;
		this.resampling.Items.AddRange(new object[2] { "Bicubic", "Bilinear" });
		this.resampling.Location = new System.Drawing.Point(114, 6);
		this.resampling.Name = "resampling";
		this.resampling.Size = new System.Drawing.Size(120, 21);
		this.resampling.TabIndex = 1;
		this.ResamplingLabel.AutoSize = true;
		this.ResamplingLabel.Location = new System.Drawing.Point(1, 9);
		this.ResamplingLabel.Name = "ResamplingLabel";
		this.ResamplingLabel.Size = new System.Drawing.Size(65, 13);
		this.ResamplingLabel.TabIndex = 0;
		this.ResamplingLabel.Text = "Resampling:";
		this.currentResolution.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.currentResolution.AutoSize = true;
		this.currentResolution.Location = new System.Drawing.Point(161, 26);
		this.currentResolution.Name = "currentResolution";
		this.currentResolution.Size = new System.Drawing.Size(70, 13);
		this.currentResolution.TabIndex = 1;
		this.currentResolution.Text = "ResX x ResY";
		this.CurrentResolutionLabel.AutoSize = true;
		this.CurrentResolutionLabel.Location = new System.Drawing.Point(22, 26);
		this.CurrentResolutionLabel.Name = "CurrentResolutionLabel";
		this.CurrentResolutionLabel.Size = new System.Drawing.Size(92, 13);
		this.CurrentResolutionLabel.TabIndex = 0;
		this.CurrentResolutionLabel.Text = "Current resolution:";
		this.gridResolutionX.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gridResolutionX.Increment = new decimal(new int[4] { 2, 0, 0, 0 });
		this.gridResolutionX.Location = new System.Drawing.Point(164, 54);
		this.gridResolutionX.Maximum = new decimal(new int[4] { 6000, 0, 0, 0 });
		this.gridResolutionX.Minimum = new decimal(new int[4] { 32, 0, 0, 0 });
		this.gridResolutionX.Name = "gridResolutionX";
		this.gridResolutionX.Size = new System.Drawing.Size(80, 20);
		this.gridResolutionX.TabIndex = 3;
		this.gridResolutionX.Value = new decimal(new int[4] { 32, 0, 0, 0 });
		this.gridResolutionX.ValueChanged += new System.EventHandler(gridResolutionX_ValueChanged);
		this.gridResolutionY.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gridResolutionY.Increment = new decimal(new int[4] { 2, 0, 0, 0 });
		this.gridResolutionY.Location = new System.Drawing.Point(164, 82);
		this.gridResolutionY.Maximum = new decimal(new int[4] { 6000, 0, 0, 0 });
		this.gridResolutionY.Minimum = new decimal(new int[4] { 32, 0, 0, 0 });
		this.gridResolutionY.Name = "gridResolutionY";
		this.gridResolutionY.Size = new System.Drawing.Size(80, 20);
		this.gridResolutionY.TabIndex = 5;
		this.gridResolutionY.Value = new decimal(new int[4] { 32, 0, 0, 0 });
		this.gridResolutionY.ValueChanged += new System.EventHandler(gridResolutionY_ValueChanged);
		this.resAspectRatio.DecimalPlaces = 5;
		this.resAspectRatio.Increment = new decimal(new int[4] { 1, 0, 0, 327680 });
		this.resAspectRatio.Location = new System.Drawing.Point(12, 444);
		this.resAspectRatio.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.resAspectRatio.Name = "resAspectRatio";
		this.resAspectRatio.Size = new System.Drawing.Size(50, 20);
		this.resAspectRatio.TabIndex = 1;
		this.resAspectRatio.TabStop = false;
		this.resAspectRatio.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.resAspectRatio.Visible = false;
		this.resolutionBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.resolutionBox.Controls.Add(this.OperationLabel);
		this.resolutionBox.Controls.Add(this.OperationPanel);
		this.resolutionBox.Controls.Add(this.ResolutionXLabel);
		this.resolutionBox.Controls.Add(this.ResolutionYLabel);
		this.resolutionBox.Controls.Add(this.gridResolutionX);
		this.resolutionBox.Controls.Add(this.gridResolutionY);
		this.resolutionBox.Controls.Add(this.maintainAspect);
		this.resolutionBox.Controls.Add(this.currentResolution);
		this.resolutionBox.Controls.Add(this.CurrentResolutionLabel);
		this.resolutionBox.Location = new System.Drawing.Point(12, 12);
		this.resolutionBox.Name = "resolutionBox";
		this.resolutionBox.Size = new System.Drawing.Size(319, 401);
		this.resolutionBox.TabIndex = 0;
		this.resolutionBox.TabStop = false;
		this.resolutionBox.Text = "Resolution settings";
		this.OperationLabel.AutoSize = true;
		this.OperationLabel.BackColor = System.Drawing.Color.Transparent;
		this.OperationLabel.Location = new System.Drawing.Point(14, 142);
		this.OperationLabel.Name = "OperationLabel";
		this.OperationLabel.Size = new System.Drawing.Size(56, 13);
		this.OperationLabel.TabIndex = 7;
		this.OperationLabel.Text = "Operation:";
		this.OperationPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.OperationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.OperationPanel.Controls.Add(this.ResamplingPanel);
		this.OperationPanel.Controls.Add(this.AnchorPanel);
		this.OperationPanel.Controls.Add(this.growCanvas);
		this.OperationPanel.Controls.Add(this.stretchCanvas);
		this.OperationPanel.Location = new System.Drawing.Point(17, 159);
		this.OperationPanel.Name = "OperationPanel";
		this.OperationPanel.Size = new System.Drawing.Size(286, 225);
		this.OperationPanel.TabIndex = 8;
		this.ResamplingPanel.Controls.Add(this.ResamplingLabel);
		this.ResamplingPanel.Controls.Add(this.resampling);
		this.ResamplingPanel.Location = new System.Drawing.Point(32, 41);
		this.ResamplingPanel.Name = "ResamplingPanel";
		this.ResamplingPanel.Size = new System.Drawing.Size(237, 33);
		this.ResamplingPanel.TabIndex = 1;
		this.AnchorPanel.AnchorModeX = palDotNet.palDialog.DialogAnchorPanel.AnchorModes.eGrow;
		this.AnchorPanel.AnchorModeY = palDotNet.palDialog.DialogAnchorPanel.AnchorModes.eGrow;
		this.AnchorPanel.BackColor = System.Drawing.SystemColors.Control;
		this.AnchorPanel.Location = new System.Drawing.Point(32, 109);
		this.AnchorPanel.Name = "AnchorPanel";
		this.AnchorPanel.Size = new System.Drawing.Size(237, 100);
		this.AnchorPanel.TabIndex = 3;
		this.growCanvas.AutoSize = true;
		this.growCanvas.Location = new System.Drawing.Point(16, 85);
		this.growCanvas.Name = "growCanvas";
		this.growCanvas.Size = new System.Drawing.Size(121, 17);
		this.growCanvas.TabIndex = 2;
		this.growCanvas.Text = "Grow/shrink canvas";
		this.growCanvas.UseVisualStyleBackColor = true;
		this.growCanvas.CheckedChanged += new System.EventHandler(growCanvas_CheckedChanged);
		this.stretchCanvas.AutoSize = true;
		this.stretchCanvas.Checked = true;
		this.stretchCanvas.Location = new System.Drawing.Point(16, 18);
		this.stretchCanvas.Name = "stretchCanvas";
		this.stretchCanvas.Size = new System.Drawing.Size(97, 17);
		this.stretchCanvas.TabIndex = 0;
		this.stretchCanvas.TabStop = true;
		this.stretchCanvas.Text = "Stretch canvas";
		this.stretchCanvas.UseVisualStyleBackColor = true;
		this.stretchCanvas.CheckedChanged += new System.EventHandler(stretchCanvas_CheckedChanged);
		this.sizeAspectRatio.DecimalPlaces = 5;
		this.sizeAspectRatio.Increment = new decimal(new int[4] { 1, 0, 0, 327680 });
		this.sizeAspectRatio.Location = new System.Drawing.Point(71, 444);
		this.sizeAspectRatio.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.sizeAspectRatio.Name = "sizeAspectRatio";
		this.sizeAspectRatio.Size = new System.Drawing.Size(50, 20);
		this.sizeAspectRatio.TabIndex = 2;
		this.sizeAspectRatio.TabStop = false;
		this.sizeAspectRatio.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.sizeAspectRatio.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.CancelButton = this.cancelBtn;
		base.ClientSize = new System.Drawing.Size(344, 479);
		base.Controls.Add(this.sizeAspectRatio);
		base.Controls.Add(this.resAspectRatio);
		base.Controls.Add(this.resolutionBox);
		base.Controls.Add(this.cancelBtn);
		base.Controls.Add(this.okBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GeographyGridResizeDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Resize Grid";
		((System.ComponentModel.ISupportInitialize)this.gridResolutionX).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridResolutionY).EndInit();
		((System.ComponentModel.ISupportInitialize)this.resAspectRatio).EndInit();
		this.resolutionBox.ResumeLayout(false);
		this.resolutionBox.PerformLayout();
		this.OperationPanel.ResumeLayout(false);
		this.OperationPanel.PerformLayout();
		this.ResamplingPanel.ResumeLayout(false);
		this.ResamplingPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.sizeAspectRatio).EndInit();
		base.ResumeLayout(false);
	}

	public GeographyGridResizeDialog()
	{
		InitializeComponent();
		mCurrentResX = 0;
		mCurrentResY = 0;
		resampling.SelectedIndex = 0;
		AnchorPanel.SetDefaultConfig();
		SyncOkButton();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		mCurrentResX = (int)gridResolutionX.Value;
		mCurrentResY = (int)gridResolutionY.Value;
		currentResolution.Text = mCurrentResX + " × " + mCurrentResY;
		AnchorPanel.SetDefaultConfig();
		SyncAnchorPanelMode();
		SyncOperationGroup();
	}

	private void gridResolutionX_ValueChanged(object sender, EventArgs e)
	{
		if (maintainAspect.Checked)
		{
			gridResolutionY.Value = gridResolutionX.Value / resAspectRatio.Value;
		}
		SyncAnchorPanelMode();
	}

	private void gridResolutionY_ValueChanged(object sender, EventArgs e)
	{
		if (maintainAspect.Checked)
		{
			gridResolutionX.Value = gridResolutionY.Value * resAspectRatio.Value;
		}
		SyncAnchorPanelMode();
	}

	private void maintainAspect_CheckedChanged(object sender, EventArgs e)
	{
		if (maintainAspect.Checked)
		{
			gridResolutionY.Value = gridResolutionX.Value / resAspectRatio.Value;
		}
		SyncAnchorPanelMode();
	}

	private void SyncAnchorPanelMode()
	{
		if (gridResolutionX.Value < (decimal)mCurrentResX)
		{
			AnchorPanel.AnchorModeX = DialogAnchorPanel.AnchorModes.eShrink;
		}
		else
		{
			AnchorPanel.AnchorModeX = DialogAnchorPanel.AnchorModes.eGrow;
		}
		if (gridResolutionY.Value < (decimal)mCurrentResY)
		{
			AnchorPanel.AnchorModeY = DialogAnchorPanel.AnchorModes.eShrink;
		}
		else
		{
			AnchorPanel.AnchorModeY = DialogAnchorPanel.AnchorModes.eGrow;
		}
	}

	private void stretchCanvas_CheckedChanged(object sender, EventArgs e)
	{
		SyncOperationGroup();
	}

	private void growCanvas_CheckedChanged(object sender, EventArgs e)
	{
		SyncOperationGroup();
	}

	private void SyncOperationGroup()
	{
		if (stretchCanvas.Checked)
		{
			ResamplingPanel.Enabled = true;
			AnchorPanel.Enabled = false;
		}
		if (growCanvas.Checked)
		{
			ResamplingPanel.Enabled = false;
			AnchorPanel.Enabled = true;
		}
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
}
