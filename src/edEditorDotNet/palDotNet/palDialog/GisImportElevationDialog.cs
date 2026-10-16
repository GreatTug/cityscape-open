using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edGISBorkDialogs\\GisImportElevationDialog", Win32PCOnly = true)]
[DialogBoxCreator]
public class GisImportElevationDialog : DialogBoxBase
{
	private IContainer components;

	private Button CancelBtn;

	private Button OKBtn;

	private GroupBox ElevationFileGroupBox;

	private DialogPathLoader ElevationFilePathInput;

	private DialogLabel ImportFilePathLabel;

	private Panel AuxiliaryInfoPanel;

	private DialogLabel AuxiliaryFilesLabel;

	private DialogLabel AuxExampleLabel;

	private DialogLabel AuxInfoLabel;

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
		this.ElevationFileGroupBox = new System.Windows.Forms.GroupBox();
		this.AuxiliaryInfoPanel = new System.Windows.Forms.Panel();
		this.AuxiliaryFilesLabel = new palDotNet.palDialog.DialogLabel();
		this.AuxExampleLabel = new palDotNet.palDialog.DialogLabel();
		this.AuxInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.ImportFilePathLabel = new palDotNet.palDialog.DialogLabel();
		this.ElevationFilePathInput = new palDotNet.palDialog.DialogPathLoader();
		this.ElevationFileGroupBox.SuspendLayout();
		this.AuxiliaryInfoPanel.SuspendLayout();
		base.SuspendLayout();
		this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CancelBtn.Location = new System.Drawing.Point(407, 209);
		this.CancelBtn.Name = "CancelBtn";
		this.CancelBtn.Size = new System.Drawing.Size(75, 23);
		this.CancelBtn.TabIndex = 2;
		this.CancelBtn.Text = "Cancel";
		this.CancelBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKBtn.Location = new System.Drawing.Point(326, 209);
		this.OKBtn.Name = "OKBtn";
		this.OKBtn.Size = new System.Drawing.Size(75, 23);
		this.OKBtn.TabIndex = 1;
		this.OKBtn.Text = "OK";
		this.OKBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Click += new System.EventHandler(OKBtn_Click);
		this.ElevationFileGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ElevationFileGroupBox.Controls.Add(this.AuxiliaryInfoPanel);
		this.ElevationFileGroupBox.Controls.Add(this.ImportFilePathLabel);
		this.ElevationFileGroupBox.Controls.Add(this.ElevationFilePathInput);
		this.ElevationFileGroupBox.Location = new System.Drawing.Point(13, 13);
		this.ElevationFileGroupBox.Name = "ElevationFileGroupBox";
		this.ElevationFileGroupBox.Size = new System.Drawing.Size(469, 175);
		this.ElevationFileGroupBox.TabIndex = 0;
		this.ElevationFileGroupBox.TabStop = false;
		this.ElevationFileGroupBox.Text = "Elevation file";
		this.AuxiliaryInfoPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.AuxiliaryInfoPanel.Controls.Add(this.AuxiliaryFilesLabel);
		this.AuxiliaryInfoPanel.Controls.Add(this.AuxExampleLabel);
		this.AuxiliaryInfoPanel.Controls.Add(this.AuxInfoLabel);
		this.AuxiliaryInfoPanel.Location = new System.Drawing.Point(6, 85);
		this.AuxiliaryInfoPanel.Name = "AuxiliaryInfoPanel";
		this.AuxiliaryInfoPanel.Size = new System.Drawing.Size(457, 84);
		this.AuxiliaryInfoPanel.TabIndex = 2;
		this.AuxiliaryFilesLabel.AutoSize = true;
		this.AuxiliaryFilesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
		this.AuxiliaryFilesLabel.Location = new System.Drawing.Point(16, 16);
		this.AuxiliaryFilesLabel.Margin = new System.Windows.Forms.Padding(3);
		this.AuxiliaryFilesLabel.Name = "AuxiliaryFilesLabel";
		this.AuxiliaryFilesLabel.Size = new System.Drawing.Size(72, 13);
		this.AuxiliaryFilesLabel.TabIndex = 0;
		this.AuxiliaryFilesLabel.Text = "Auxiliary Files:";
		this.AuxExampleLabel.AutoSize = true;
		this.AuxExampleLabel.Location = new System.Drawing.Point(16, 54);
		this.AuxExampleLabel.Margin = new System.Windows.Forms.Padding(3);
		this.AuxExampleLabel.Name = "AuxExampleLabel";
		this.AuxExampleLabel.Size = new System.Drawing.Size(374, 13);
		this.AuxExampleLabel.TabIndex = 2;
		this.AuxExampleLabel.Text = "Ex: to import example.tif you must also have example.tfw in the same directory.";
		this.AuxInfoLabel.AutoSize = true;
		this.AuxInfoLabel.Location = new System.Drawing.Point(16, 35);
		this.AuxInfoLabel.Margin = new System.Windows.Forms.Padding(3);
		this.AuxInfoLabel.Name = "AuxInfoLabel";
		this.AuxInfoLabel.Size = new System.Drawing.Size(212, 13);
		this.AuxInfoLabel.TabIndex = 1;
		this.AuxInfoLabel.Text = "A corresponding world file (*.tfw) is required.";
		this.ImportFilePathLabel.AutoSize = true;
		this.ImportFilePathLabel.Location = new System.Drawing.Point(22, 32);
		this.ImportFilePathLabel.Name = "ImportFilePathLabel";
		this.ImportFilePathLabel.Size = new System.Drawing.Size(113, 13);
		this.ImportFilePathLabel.TabIndex = 0;
		this.ImportFilePathLabel.Text = "Specify a file to import:";
		this.ElevationFilePathInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ElevationFilePathInput.BackColor = System.Drawing.Color.Transparent;
		this.ElevationFilePathInput.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.ElevationFilePathInput.CheckFileExists = true;
		this.ElevationFilePathInput.Description = null;
		this.ElevationFilePathInput.Filter = null;
		this.ElevationFilePathInput.Location = new System.Drawing.Point(25, 55);
		this.ElevationFilePathInput.Name = "ElevationFilePathInput";
		this.ElevationFilePathInput.Path = "";
		this.ElevationFilePathInput.Size = new System.Drawing.Size(418, 24);
		this.ElevationFilePathInput.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CancelBtn;
		base.ClientSize = new System.Drawing.Size(494, 244);
		base.Controls.Add(this.ElevationFileGroupBox);
		base.Controls.Add(this.OKBtn);
		base.Controls.Add(this.CancelBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GisImportElevationDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "GIS Mode - Import Elevation";
		this.ElevationFileGroupBox.ResumeLayout(false);
		this.ElevationFileGroupBox.PerformLayout();
		this.AuxiliaryInfoPanel.ResumeLayout(false);
		this.AuxiliaryInfoPanel.PerformLayout();
		base.ResumeLayout(false);
	}

	public GisImportElevationDialog()
	{
		InitializeComponent();
		SyncOkButton();
	}

	public DialogLabel GetImportFilePathLabel()
	{
		return ImportFilePathLabel;
	}

	public DialogPathLoader GetElevationFilePathInput()
	{
		return ElevationFilePathInput;
	}

	public Panel GetAuxiliaryInfoPanel()
	{
		return AuxiliaryInfoPanel;
	}

	public DialogLabel GetAuxiliaryInfoLabel()
	{
		return AuxInfoLabel;
	}

	public DialogLabel GetAuxiliaryExampleLabel()
	{
		return AuxExampleLabel;
	}

	private bool IsValidInput()
	{
		if (string.IsNullOrEmpty(ElevationFilePathInput.Path))
		{
			return false;
		}
		return true;
	}

	public void SyncOkButton()
	{
		AllowSubmitDialog = IsValidInput();
		OKBtn.Enabled = AllowSubmitDialog;
	}

	private void OKBtn_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
