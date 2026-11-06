using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edGISBorkDialogs\\GisImportWaterDialog", Win32PCOnly = true)]
[DialogBoxCreator]
public class GisImportWaterDialog : DialogBoxBase
{
	private IContainer components;

	private GroupBox WaterFileGroup;

	private Button CancelBtn;

	private Button OKBtn;

	private DialogPathLoader WaterShapefilePathInput;

	private DialogLabel WaterFileInfoLabel;

	private DialogMessagePanel mMessagePanel;

	private bool ValidWaterFilePath;

	public DialogPathLoader WaterShapefilePath => WaterShapefilePathInput;

	public DialogMessagePanel MessagePanel => mMessagePanel;

	public event WaterDialogOnShownHandler WaterDialogOnShown;

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
		this.WaterFileGroup = new System.Windows.Forms.GroupBox();
		this.WaterFileInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.WaterShapefilePathInput = new palDotNet.palDialog.DialogPathLoader();
		this.CancelBtn = new System.Windows.Forms.Button();
		this.OKBtn = new System.Windows.Forms.Button();
		this.mMessagePanel = new palDotNet.palDialog.DialogMessagePanel();
		this.WaterFileGroup.SuspendLayout();
		base.SuspendLayout();
		this.WaterFileGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.WaterFileGroup.Controls.Add(this.mMessagePanel);
		this.WaterFileGroup.Controls.Add(this.WaterFileInfoLabel);
		this.WaterFileGroup.Controls.Add(this.WaterShapefilePathInput);
		this.WaterFileGroup.Location = new System.Drawing.Point(13, 13);
		this.WaterFileGroup.Name = "WaterFileGroup";
		this.WaterFileGroup.Size = new System.Drawing.Size(469, 140);
		this.WaterFileGroup.TabIndex = 0;
		this.WaterFileGroup.TabStop = false;
		this.WaterFileGroup.Text = "Water file";
		this.WaterFileInfoLabel.AutoSize = true;
		this.WaterFileInfoLabel.Location = new System.Drawing.Point(19, 30);
		this.WaterFileInfoLabel.Name = "WaterFileInfoLabel";
		this.WaterFileInfoLabel.SaveToConfig = false;
		this.WaterFileInfoLabel.Size = new System.Drawing.Size(265, 13);
		this.WaterFileInfoLabel.TabIndex = 0;
		this.WaterFileInfoLabel.Text = "Specify a feature shapefile (*.shp) with water geometry:";
		this.WaterShapefilePathInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.WaterShapefilePathInput.BackColor = System.Drawing.Color.Transparent;
		this.WaterShapefilePathInput.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.WaterShapefilePathInput.CheckFileExists = true;
		this.WaterShapefilePathInput.Description = null;
		this.WaterShapefilePathInput.Filter = "Shapefile (*.shp)|*.shp";
		this.WaterShapefilePathInput.Location = new System.Drawing.Point(22, 60);
		this.WaterShapefilePathInput.Name = "WaterShapefilePathInput";
		this.WaterShapefilePathInput.Path = "";
		this.WaterShapefilePathInput.SaveToConfig = true;
		this.WaterShapefilePathInput.Size = new System.Drawing.Size(421, 24);
		this.WaterShapefilePathInput.TabIndex = 1;
		this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CancelBtn.Location = new System.Drawing.Point(407, 187);
		this.CancelBtn.Name = "CancelBtn";
		this.CancelBtn.Size = new System.Drawing.Size(75, 23);
		this.CancelBtn.TabIndex = 2;
		this.CancelBtn.Text = "Cancel";
		this.CancelBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKBtn.Location = new System.Drawing.Point(326, 187);
		this.OKBtn.Name = "OKBtn";
		this.OKBtn.Size = new System.Drawing.Size(75, 23);
		this.OKBtn.TabIndex = 1;
		this.OKBtn.Text = "OK";
		this.OKBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Click += new System.EventHandler(OKBtn_Click);
		this.mMessagePanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.mMessagePanel.Location = new System.Drawing.Point(22, 100);
		this.mMessagePanel.Name = "mMessagePanel";
		this.mMessagePanel.Size = new System.Drawing.Size(421, 20);
		this.mMessagePanel.TabIndex = 2;
		this.mMessagePanel.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CancelBtn;
		base.ClientSize = new System.Drawing.Size(494, 222);
		base.Controls.Add(this.OKBtn);
		base.Controls.Add(this.CancelBtn);
		base.Controls.Add(this.WaterFileGroup);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GisImportWaterDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "GIS Mode - Import Water";
		this.WaterFileGroup.ResumeLayout(false);
		this.WaterFileGroup.PerformLayout();
		base.ResumeLayout(false);
	}

	public GisImportWaterDialog()
	{
		InitializeComponent();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		WaterDialogOnShown?.Invoke(this, e);
		SyncOkButton();
	}

	public void InvalidateWaterFilePath()
	{
		ValidWaterFilePath = false;
		SyncOkButton();
	}

	public void ValidateWaterFilePath()
	{
		ValidWaterFilePath = true;
		SyncOkButton();
	}

	private bool IsValidInput()
	{
		if (!ValidWaterFilePath)
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
}
