using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edGISBorkDialogs\\GisImportOpenStreetMapDialog", Win32PCOnly = true)]
public class GisImportOpenStreetMapDialog : DialogBoxBase
{
	private IContainer components;

	private Button CancelBtn;

	private Button OKBtn;

	private GroupBox OpenStreetMapGroupBox;

	private DialogPathLoader OsmFilePathInput;

	private DialogCheckBox ImportFencesCheckBox;

	private DialogCheckBox ImportPowerLinesCheckBox;

	private DialogCheckBox ImportRoadsCheckBox;

	private DialogCheckBox ImportWaterCheckBox;

	private DialogLabel OsmFilePathInfoLabel;

	private DialogLabel OsmFeaturesInfoLabel;

	private Panel OsmFeaturesToImportPanel;

	private DialogCheckBox ImportBuildingsCheckBox;

	private TabControl OsmTabControl;

	private TabPage OpenStreetMapFileTab;

	private DialogMessagePanel mMessagePanel;

	private TabPage RoadOptionsTab;

	private RoadGenerationOptionsPanel RoadGenerationOptionsPanel;

	private TabPage BuildingOptionsTab;

	private GroupBox BuildingHeightsGroupBox;

	private GroupBox groupBox1;

	private DialogLabel RandomHeightsInfoLabel;

	private DialogNumericUpDownFloat RandomHeightMaxInput;

	private DialogLabel RandomHeightMaxLabel;

	private DialogLabel RandomHeightMinLabel;

	private DialogNumericUpDownFloat RandomHeightMinInput;

	private DialogLabel HeightsInfoLabel;

	private Panel RoadOptionsTabPanel;

	private Panel BuildingOptionsTabPanel;

	private bool ValidOsmFilePath;

	private bool ValidOsmFeatureSet;

	public DialogPathLoader OsmFilePath => OsmFilePathInput;

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
		this.OpenStreetMapGroupBox = new System.Windows.Forms.GroupBox();
		this.OsmFeaturesToImportPanel = new System.Windows.Forms.Panel();
		this.ImportBuildingsCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.OsmFeaturesInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.ImportRoadsCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.ImportPowerLinesCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.ImportWaterCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.ImportFencesCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.OsmFilePathInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.OsmFilePathInput = new palDotNet.palDialog.DialogPathLoader();
		this.OsmTabControl = new System.Windows.Forms.TabControl();
		this.OpenStreetMapFileTab = new System.Windows.Forms.TabPage();
		this.RoadOptionsTab = new System.Windows.Forms.TabPage();
		this.RoadOptionsTabPanel = new System.Windows.Forms.Panel();
		this.RoadGenerationOptionsPanel = new palDotNet.palDialog.RoadGenerationOptionsPanel();
		this.BuildingOptionsTab = new System.Windows.Forms.TabPage();
		this.BuildingOptionsTabPanel = new System.Windows.Forms.Panel();
		this.BuildingHeightsGroupBox = new System.Windows.Forms.GroupBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.RandomHeightsInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.RandomHeightMaxInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.RandomHeightMaxLabel = new palDotNet.palDialog.DialogLabel();
		this.RandomHeightMinLabel = new palDotNet.palDialog.DialogLabel();
		this.RandomHeightMinInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.HeightsInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.mMessagePanel = new palDotNet.palDialog.DialogMessagePanel();
		this.OpenStreetMapGroupBox.SuspendLayout();
		this.OsmFeaturesToImportPanel.SuspendLayout();
		this.OsmTabControl.SuspendLayout();
		this.OpenStreetMapFileTab.SuspendLayout();
		this.RoadOptionsTab.SuspendLayout();
		this.RoadOptionsTabPanel.SuspendLayout();
		this.BuildingOptionsTab.SuspendLayout();
		this.BuildingOptionsTabPanel.SuspendLayout();
		this.BuildingHeightsGroupBox.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.RandomHeightMaxInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RandomHeightMinInput).BeginInit();
		base.SuspendLayout();
		this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CancelBtn.Location = new System.Drawing.Point(407, 389);
		this.CancelBtn.Name = "CancelBtn";
		this.CancelBtn.Size = new System.Drawing.Size(75, 23);
		this.CancelBtn.TabIndex = 3;
		this.CancelBtn.Text = "Cancel";
		this.CancelBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKBtn.Location = new System.Drawing.Point(326, 389);
		this.OKBtn.Name = "OKBtn";
		this.OKBtn.Size = new System.Drawing.Size(75, 23);
		this.OKBtn.TabIndex = 2;
		this.OKBtn.Text = "OK";
		this.OKBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Click += new System.EventHandler(OKBtn_Click);
		this.OpenStreetMapGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.OpenStreetMapGroupBox.Controls.Add(this.OsmFeaturesToImportPanel);
		this.OpenStreetMapGroupBox.Controls.Add(this.OsmFilePathInfoLabel);
		this.OpenStreetMapGroupBox.Controls.Add(this.OsmFilePathInput);
		this.OpenStreetMapGroupBox.Location = new System.Drawing.Point(6, 6);
		this.OpenStreetMapGroupBox.Name = "OpenStreetMapGroupBox";
		this.OpenStreetMapGroupBox.Size = new System.Drawing.Size(449, 312);
		this.OpenStreetMapGroupBox.TabIndex = 0;
		this.OpenStreetMapGroupBox.TabStop = false;
		this.OpenStreetMapGroupBox.Text = "OpenStreetMap file";
		this.OsmFeaturesToImportPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.OsmFeaturesToImportPanel.Controls.Add(this.ImportBuildingsCheckBox);
		this.OsmFeaturesToImportPanel.Controls.Add(this.OsmFeaturesInfoLabel);
		this.OsmFeaturesToImportPanel.Controls.Add(this.ImportRoadsCheckBox);
		this.OsmFeaturesToImportPanel.Controls.Add(this.ImportPowerLinesCheckBox);
		this.OsmFeaturesToImportPanel.Controls.Add(this.ImportWaterCheckBox);
		this.OsmFeaturesToImportPanel.Controls.Add(this.ImportFencesCheckBox);
		this.OsmFeaturesToImportPanel.Location = new System.Drawing.Point(35, 84);
		this.OsmFeaturesToImportPanel.Name = "OsmFeaturesToImportPanel";
		this.OsmFeaturesToImportPanel.Size = new System.Drawing.Size(375, 210);
		this.OsmFeaturesToImportPanel.TabIndex = 2;
		this.ImportBuildingsCheckBox.AutoSize = true;
		this.ImportBuildingsCheckBox.Location = new System.Drawing.Point(38, 59);
		this.ImportBuildingsCheckBox.Name = "ImportBuildingsCheckBox";
		this.ImportBuildingsCheckBox.Size = new System.Drawing.Size(68, 17);
		this.ImportBuildingsCheckBox.TabIndex = 3;
		this.ImportBuildingsCheckBox.Text = "Buildings";
		this.ImportBuildingsCheckBox.UseVisualStyleBackColor = true;
		this.ImportBuildingsCheckBox.CheckedChanged += new System.EventHandler(ImportFeaturesCheckBox_CheckedChanged);
		this.OsmFeaturesInfoLabel.AutoSize = true;
		this.OsmFeaturesInfoLabel.Location = new System.Drawing.Point(9, 10);
		this.OsmFeaturesInfoLabel.Name = "OsmFeaturesInfoLabel";
		this.OsmFeaturesInfoLabel.Size = new System.Drawing.Size(94, 13);
		this.OsmFeaturesInfoLabel.TabIndex = 0;
		this.OsmFeaturesInfoLabel.Text = "Features to import:";
		this.ImportRoadsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ImportRoadsCheckBox.AutoSize = true;
		this.ImportRoadsCheckBox.Location = new System.Drawing.Point(38, 35);
		this.ImportRoadsCheckBox.Name = "ImportRoadsCheckBox";
		this.ImportRoadsCheckBox.Size = new System.Drawing.Size(57, 17);
		this.ImportRoadsCheckBox.TabIndex = 1;
		this.ImportRoadsCheckBox.Text = "Roads";
		this.ImportRoadsCheckBox.UseVisualStyleBackColor = true;
		this.ImportRoadsCheckBox.CheckedChanged += new System.EventHandler(ImportFeaturesCheckBox_CheckedChanged);
		this.ImportPowerLinesCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ImportPowerLinesCheckBox.AutoSize = true;
		this.ImportPowerLinesCheckBox.Location = new System.Drawing.Point(150, 59);
		this.ImportPowerLinesCheckBox.Name = "ImportPowerLinesCheckBox";
		this.ImportPowerLinesCheckBox.Size = new System.Drawing.Size(80, 17);
		this.ImportPowerLinesCheckBox.TabIndex = 4;
		this.ImportPowerLinesCheckBox.Text = "Power lines";
		this.ImportPowerLinesCheckBox.UseVisualStyleBackColor = true;
		this.ImportPowerLinesCheckBox.CheckedChanged += new System.EventHandler(ImportFeaturesCheckBox_CheckedChanged);
		this.ImportWaterCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ImportWaterCheckBox.AutoSize = true;
		this.ImportWaterCheckBox.Location = new System.Drawing.Point(38, 83);
		this.ImportWaterCheckBox.Name = "ImportWaterCheckBox";
		this.ImportWaterCheckBox.Size = new System.Drawing.Size(55, 17);
		this.ImportWaterCheckBox.TabIndex = 5;
		this.ImportWaterCheckBox.Text = "Water";
		this.ImportWaterCheckBox.UseVisualStyleBackColor = true;
		this.ImportWaterCheckBox.CheckedChanged += new System.EventHandler(ImportFeaturesCheckBox_CheckedChanged);
		this.ImportFencesCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ImportFencesCheckBox.AutoSize = true;
		this.ImportFencesCheckBox.Location = new System.Drawing.Point(150, 35);
		this.ImportFencesCheckBox.Name = "ImportFencesCheckBox";
		this.ImportFencesCheckBox.Size = new System.Drawing.Size(61, 17);
		this.ImportFencesCheckBox.TabIndex = 2;
		this.ImportFencesCheckBox.Text = "Fences";
		this.ImportFencesCheckBox.UseVisualStyleBackColor = true;
		this.ImportFencesCheckBox.CheckedChanged += new System.EventHandler(ImportFeaturesCheckBox_CheckedChanged);
		this.OsmFilePathInfoLabel.AutoSize = true;
		this.OsmFilePathInfoLabel.Location = new System.Drawing.Point(32, 30);
		this.OsmFilePathInfoLabel.Name = "OsmFilePathInfoLabel";
		this.OsmFilePathInfoLabel.Size = new System.Drawing.Size(232, 13);
		this.OsmFilePathInfoLabel.TabIndex = 0;
		this.OsmFilePathInfoLabel.Text = "Specify an OpenStreetMap (*.osm) file to import:";
		this.OsmFilePathInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.OsmFilePathInput.BackColor = System.Drawing.Color.Transparent;
		this.OsmFilePathInput.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.OsmFilePathInput.CheckFileExists = true;
		this.OsmFilePathInput.Description = null;
		this.OsmFilePathInput.Filter = "OpenStreetMap file (*.osm)|*.osm";
		this.OsmFilePathInput.Location = new System.Drawing.Point(35, 54);
		this.OsmFilePathInput.Name = "OsmFilePathInput";
		this.OsmFilePathInput.Path = "";
		this.OsmFilePathInput.Size = new System.Drawing.Size(375, 24);
		this.OsmFilePathInput.TabIndex = 1;
		this.OsmTabControl.Controls.Add(this.OpenStreetMapFileTab);
		this.OsmTabControl.Controls.Add(this.RoadOptionsTab);
		this.OsmTabControl.Controls.Add(this.BuildingOptionsTab);
		this.OsmTabControl.Location = new System.Drawing.Point(13, 13);
		this.OsmTabControl.Name = "OsmTabControl";
		this.OsmTabControl.SelectedIndex = 0;
		this.OsmTabControl.Size = new System.Drawing.Size(469, 350);
		this.OsmTabControl.TabIndex = 0;
		this.OpenStreetMapFileTab.Controls.Add(this.OpenStreetMapGroupBox);
		this.OpenStreetMapFileTab.Location = new System.Drawing.Point(4, 22);
		this.OpenStreetMapFileTab.Name = "OpenStreetMapFileTab";
		this.OpenStreetMapFileTab.Padding = new System.Windows.Forms.Padding(3);
		this.OpenStreetMapFileTab.Size = new System.Drawing.Size(461, 324);
		this.OpenStreetMapFileTab.TabIndex = 0;
		this.OpenStreetMapFileTab.Text = "OpenStreetMap File";
		this.OpenStreetMapFileTab.UseVisualStyleBackColor = true;
		this.RoadOptionsTab.Controls.Add(this.RoadOptionsTabPanel);
		this.RoadOptionsTab.Location = new System.Drawing.Point(4, 22);
		this.RoadOptionsTab.Name = "RoadOptionsTab";
		this.RoadOptionsTab.Size = new System.Drawing.Size(461, 324);
		this.RoadOptionsTab.TabIndex = 1;
		this.RoadOptionsTab.Text = "Road Options";
		this.RoadOptionsTab.UseVisualStyleBackColor = true;
		this.RoadOptionsTabPanel.Controls.Add(this.RoadGenerationOptionsPanel);
		this.RoadOptionsTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.RoadOptionsTabPanel.Location = new System.Drawing.Point(0, 0);
		this.RoadOptionsTabPanel.Name = "RoadOptionsTabPanel";
		this.RoadOptionsTabPanel.Size = new System.Drawing.Size(461, 324);
		this.RoadOptionsTabPanel.TabIndex = 0;
		this.RoadGenerationOptionsPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RoadGenerationOptionsPanel.Location = new System.Drawing.Point(6, 6);
		this.RoadGenerationOptionsPanel.Margin = new System.Windows.Forms.Padding(6);
		this.RoadGenerationOptionsPanel.Name = "RoadGenerationOptionsPanel";
		this.RoadGenerationOptionsPanel.Size = new System.Drawing.Size(449, 312);
		this.RoadGenerationOptionsPanel.TabIndex = 0;
		this.BuildingOptionsTab.Controls.Add(this.BuildingOptionsTabPanel);
		this.BuildingOptionsTab.Location = new System.Drawing.Point(4, 22);
		this.BuildingOptionsTab.Name = "BuildingOptionsTab";
		this.BuildingOptionsTab.Size = new System.Drawing.Size(461, 324);
		this.BuildingOptionsTab.TabIndex = 2;
		this.BuildingOptionsTab.Text = "Building Options";
		this.BuildingOptionsTab.UseVisualStyleBackColor = true;
		this.BuildingOptionsTabPanel.Controls.Add(this.BuildingHeightsGroupBox);
		this.BuildingOptionsTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.BuildingOptionsTabPanel.Location = new System.Drawing.Point(0, 0);
		this.BuildingOptionsTabPanel.Name = "BuildingOptionsTabPanel";
		this.BuildingOptionsTabPanel.Size = new System.Drawing.Size(461, 324);
		this.BuildingOptionsTabPanel.TabIndex = 0;
		this.BuildingHeightsGroupBox.Controls.Add(this.groupBox1);
		this.BuildingHeightsGroupBox.Controls.Add(this.HeightsInfoLabel);
		this.BuildingHeightsGroupBox.Location = new System.Drawing.Point(6, 6);
		this.BuildingHeightsGroupBox.Margin = new System.Windows.Forms.Padding(6);
		this.BuildingHeightsGroupBox.Name = "BuildingHeightsGroupBox";
		this.BuildingHeightsGroupBox.Size = new System.Drawing.Size(450, 162);
		this.BuildingHeightsGroupBox.TabIndex = 0;
		this.BuildingHeightsGroupBox.TabStop = false;
		this.BuildingHeightsGroupBox.Text = "Building heights";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.RandomHeightsInfoLabel);
		this.groupBox1.Controls.Add(this.RandomHeightMaxInput);
		this.groupBox1.Controls.Add(this.RandomHeightMaxLabel);
		this.groupBox1.Controls.Add(this.RandomHeightMinLabel);
		this.groupBox1.Controls.Add(this.RandomHeightMinInput);
		this.groupBox1.Location = new System.Drawing.Point(6, 59);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(438, 97);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Random heights range";
		this.RandomHeightsInfoLabel.AutoSize = true;
		this.RandomHeightsInfoLabel.Location = new System.Drawing.Point(30, 27);
		this.RandomHeightsInfoLabel.Name = "RandomHeightsInfoLabel";
		this.RandomHeightsInfoLabel.Size = new System.Drawing.Size(324, 13);
		this.RandomHeightsInfoLabel.TabIndex = 0;
		this.RandomHeightsInfoLabel.Text = "Specify the bounds for the random building height range (in meters):";
		this.RandomHeightMaxInput.DecimalPlaces = 3;
		this.RandomHeightMaxInput.Location = new System.Drawing.Point(310, 58);
		this.RandomHeightMaxInput.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.RandomHeightMaxInput.Name = "RandomHeightMaxInput";
		this.RandomHeightMaxInput.Size = new System.Drawing.Size(90, 20);
		this.RandomHeightMaxInput.TabIndex = 4;
		this.RandomHeightMaxLabel.AutoSize = true;
		this.RandomHeightMaxLabel.Location = new System.Drawing.Point(240, 60);
		this.RandomHeightMaxLabel.Name = "RandomHeightMaxLabel";
		this.RandomHeightMaxLabel.Size = new System.Drawing.Size(62, 13);
		this.RandomHeightMaxLabel.TabIndex = 3;
		this.RandomHeightMaxLabel.Text = "Max height:";
		this.RandomHeightMinLabel.AutoSize = true;
		this.RandomHeightMinLabel.Location = new System.Drawing.Point(30, 60);
		this.RandomHeightMinLabel.Name = "RandomHeightMinLabel";
		this.RandomHeightMinLabel.Size = new System.Drawing.Size(59, 13);
		this.RandomHeightMinLabel.TabIndex = 1;
		this.RandomHeightMinLabel.Text = "Min height:";
		this.RandomHeightMinInput.DecimalPlaces = 3;
		this.RandomHeightMinInput.Location = new System.Drawing.Point(97, 58);
		this.RandomHeightMinInput.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.RandomHeightMinInput.Name = "RandomHeightMinInput";
		this.RandomHeightMinInput.Size = new System.Drawing.Size(90, 20);
		this.RandomHeightMinInput.TabIndex = 2;
		this.HeightsInfoLabel.AutoSize = true;
		this.HeightsInfoLabel.Location = new System.Drawing.Point(14, 29);
		this.HeightsInfoLabel.Name = "HeightsInfoLabel";
		this.HeightsInfoLabel.Size = new System.Drawing.Size(330, 13);
		this.HeightsInfoLabel.TabIndex = 0;
		this.HeightsInfoLabel.Text = "Building heights will be randomized if they are not specified or invalid.";
		this.mMessagePanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.mMessagePanel.Location = new System.Drawing.Point(12, 389);
		this.mMessagePanel.Name = "mMessagePanel";
		this.mMessagePanel.Size = new System.Drawing.Size(279, 20);
		this.mMessagePanel.TabIndex = 1;
		this.mMessagePanel.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CancelBtn;
		base.ClientSize = new System.Drawing.Size(494, 424);
		base.Controls.Add(this.mMessagePanel);
		base.Controls.Add(this.OsmTabControl);
		base.Controls.Add(this.OKBtn);
		base.Controls.Add(this.CancelBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GisImportOpenStreetMapDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "GIS Mode - Import OpenStreetMap";
		this.OpenStreetMapGroupBox.ResumeLayout(false);
		this.OpenStreetMapGroupBox.PerformLayout();
		this.OsmFeaturesToImportPanel.ResumeLayout(false);
		this.OsmFeaturesToImportPanel.PerformLayout();
		this.OsmTabControl.ResumeLayout(false);
		this.OpenStreetMapFileTab.ResumeLayout(false);
		this.RoadOptionsTab.ResumeLayout(false);
		this.RoadOptionsTabPanel.ResumeLayout(false);
		this.BuildingOptionsTab.ResumeLayout(false);
		this.BuildingOptionsTabPanel.ResumeLayout(false);
		this.BuildingHeightsGroupBox.ResumeLayout(false);
		this.BuildingHeightsGroupBox.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.RandomHeightMaxInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RandomHeightMinInput).EndInit();
		base.ResumeLayout(false);
	}

	public GisImportOpenStreetMapDialog()
	{
		InitializeComponent();
		InvalidateOsmFilePath();
		ImportPowerLinesCheckBox.Enabled = false;
		ImportFencesCheckBox.Enabled = false;
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		SyncTabs();
		SyncOkButton();
	}

	public void InvalidateOsmFilePath()
	{
		ValidOsmFilePath = false;
		SyncOsmFeaturesToImportPanel();
		SyncOkButton();
	}

	public void ValidateOsmFilePath()
	{
		ValidOsmFilePath = true;
		SyncOsmFeaturesToImportPanel();
		SyncOkButton();
	}

	public void SyncOsmFeaturesToImportPanel()
	{
		bool validOsmFilePath = ValidOsmFilePath;
		OsmFeaturesToImportPanel.Enabled = validOsmFilePath;
		CheckOsmFeaturesToImport();
	}

	public void CheckOsmFeaturesToImport()
	{
		if (OsmFeaturesToImportPanel.Enabled)
		{
			if (!ImportRoadsCheckBox.Checked && !ImportBuildingsCheckBox.Checked && !ImportWaterCheckBox.Checked && !ImportFencesCheckBox.Checked && !ImportPowerLinesCheckBox.Checked)
			{
				ValidOsmFeatureSet = false;
			}
			else
			{
				ValidOsmFeatureSet = true;
			}
		}
		else
		{
			ValidOsmFeatureSet = true;
		}
	}

	private void ImportFeaturesCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		SyncTabs();
		CheckOsmFeaturesToImport();
		SyncOkButton();
	}

	private void SyncTabs()
	{
		RoadOptionsTabPanel.Enabled = ImportRoadsCheckBox.Checked;
		BuildingOptionsTabPanel.Enabled = ImportBuildingsCheckBox.Checked;
	}

	private void SyncOkButton()
	{
		mMessagePanel.ClearMessage();
		if (!ValidOsmFilePath)
		{
			AllowSubmitDialog = false;
			OKBtn.Enabled = false;
		}
		else if (!ValidOsmFeatureSet)
		{
			mMessagePanel.ErrorMessage("No features have been selected for import.");
			AllowSubmitDialog = false;
			OKBtn.Enabled = false;
		}
		else
		{
			AllowSubmitDialog = true;
			OKBtn.Enabled = true;
		}
	}

	private void OKBtn_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
