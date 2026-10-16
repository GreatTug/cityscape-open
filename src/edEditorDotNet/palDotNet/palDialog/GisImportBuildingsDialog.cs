using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edGISBorkDialogs\\GisImportBuildingsDialog", Win32PCOnly = true)]
public class GisImportBuildingsDialog : DialogBoxBase
{
	private bool ValidBuildingsFilePath;

	private string[] HeightTab_HeightUnitConversionFactorItems;

	private IContainer components;

	private Button CancelBtn;

	private Button OKBtn;

	private GroupBox BuildingsFileGroup;

	private DialogPathLoader BuildingsShapefilePathInput;

	private DialogLabel BuildingsFileInfoLabel;

	private DialogLabel BuildingHeightUnitsLabel;

	private DialogComboBox HeightUnitsComboBox;

	private DialogLabel BuildingHeightsInfoLabel;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage HeightsTabPage;

	private DialogLabel HeightFileFieldNameLabel;

	private DialogComboBox HeightFieldNameComboBox;

	private DialogCheckGroupBox HeightsCheckGroupBox;

	private DialogLabel UnitsToMetersLabel;

	private DialogTextBox HeightUnitConversionToMetersTextBox;

	private DialogLabel HeightsInfoLabel;

	private TabPage AggregateDataTabPage;

	private DialogCheckGroupBox AggregateDataCheckGroupBox;

	private DialogLabel GroupIdFieldNameLabel;

	private DialogComboBox GroupIdFieldNameComboBox;

	private TabPage TypeStyleTabPage;

	private DialogStyleGridChooser TypeStyleGridChooser;

	private DialogCheckGroupBox BuildingTypeMappingCheckGroupBox;

	private DialogComboBox BuildingTypeFieldNameComboBox;

	private DialogLabel BuildingTypeFiledNameLabel;

	private GroupBox groupBox1;

	private DialogLabel RandomHeightMinLabel;

	private DialogNumericUpDownFloat RandomHeightMinInput;

	private DialogNumericUpDownFloat RandomHeightMaxInput;

	private DialogLabel RandomHeightMaxLabel;

	private DialogLabel RandomHeightsInfoLabel;

	private Panel HeightsPanel;

	private Panel AggregateDataPanel;

	private Panel TypeStylesPanel;

	private DialogMessagePanel mMessagePanel;

	public DialogPathLoader BuildingsShapefilePath => BuildingsShapefilePathInput;

	public DialogStyleGridChooser TypeStyleChooser => TypeStyleGridChooser;

	public DialogMessagePanel MessagePanel => mMessagePanel;

	public event BuildingsDialogOnShownHandler BuildingsDialogOnShown;

	public GisImportBuildingsDialog()
	{
		InitializeComponent();
		InvalidateBuildingsFilePath();
		HeightTab_ResetInput();
		AggregateDataTab_ResetInput();
		BuildingTypeMappingCheckGroupBox.DisableChildrenIfUnchecked = false;
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		ResetAttributeTabs();
		if (BuildingsDialogOnShown != null)
		{
			BuildingsDialogOnShown(this, e);
		}
		SyncAttributeTabs();
		SyncOkButton();
	}

	public void SyncAttributeTabs()
	{
		bool validBuildingsFilePath = ValidBuildingsFilePath;
		HeightsPanel.Enabled = validBuildingsFilePath;
		AggregateDataPanel.Enabled = validBuildingsFilePath;
		TypeStylesPanel.Enabled = validBuildingsFilePath;
	}

	public void ResetAttributeTabs()
	{
		HeightTab_Reset();
		AggregateDataTab_Reset();
		TypeStylesTab_Reset();
	}

	public void InvalidateBuildingsFilePath()
	{
		ValidBuildingsFilePath = false;
		SyncAttributeTabs();
		SyncOkButton();
	}

	public void ValidateBuildingsFilePath()
	{
		ValidBuildingsFilePath = true;
		SyncAttributeTabs();
		SyncOkButton();
	}

	private void HeightTab_Reset()
	{
		HeightsCheckGroupBox.Checked = false;
	}

	private void HeightTab_ResetInput()
	{
		HeightFieldNameComboBox.Items.Clear();
		HeightUnitsComboBox.Items.Clear();
	}

	public void HeightTab_SetHeightFieldNameItems(string[] itemNames)
	{
		HeightFieldNameComboBox.Items.Clear();
		HeightFieldNameComboBox.Items.AddRange(itemNames);
		HeightFieldNameComboBox.SelectedIndex = -1;
	}

	public void HeightTab_SetHeightUnitItems(string[] itemNames, string[] itemConversionFactors)
	{
		HeightUnitsComboBox.Items.Clear();
		HeightUnitsComboBox.Items.AddRange(itemNames);
		HeightUnitsComboBox.SelectedIndex = 0;
		HeightTab_HeightUnitConversionFactorItems = itemConversionFactors;
		SyncHeightUnitInput();
	}

	private void SyncHeightUnitInput()
	{
		if (HeightsCheckGroupBox.Checked)
		{
			int selectedIndex = HeightUnitsComboBox.SelectedIndex;
			if (HeightUnitsComboBox.Text == "custom")
			{
				HeightUnitConversionToMetersTextBox.Enabled = true;
				HeightUnitConversionToMetersTextBox.Text = "0.0";
			}
			else
			{
				HeightUnitConversionToMetersTextBox.Enabled = false;
				HeightUnitConversionToMetersTextBox.Text = HeightTab_HeightUnitConversionFactorItems[selectedIndex];
			}
		}
	}

	private bool IsValidHeightFieldName()
	{
		if (HeightFieldNameComboBox.SelectedIndex < 0 || HeightFieldNameComboBox.SelectedIndex >= HeightFieldNameComboBox.Items.Count)
		{
			return false;
		}
		return true;
	}

	private bool IsValidHeightUnit()
	{
		if (HeightUnitsComboBox.SelectedIndex < 0 || HeightUnitsComboBox.SelectedIndex >= HeightUnitsComboBox.Items.Count)
		{
			return false;
		}
		return true;
	}

	private void HeightUnitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SyncHeightUnitInput();
		SyncOkButton();
	}

	private void HeightUnitsComboBox_TextUpdate(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private void HeightFieldNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private void HeightFieldNameComboBox_TextChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private void HeightsCheckGroupBox_CheckStateChanged(object sender, EventArgs e)
	{
		SyncHeightUnitInput();
	}

	public void HeightTab_SetRandomHeightMin(float randomHeightMin)
	{
		RandomHeightMinInput.Value = (decimal)randomHeightMin;
	}

	public void HeightTab_SetRandomHeightMax(float randomHeightMax)
	{
		RandomHeightMaxInput.Value = (decimal)randomHeightMax;
	}

	private void AggregateDataTab_Reset()
	{
		AggregateDataCheckGroupBox.Checked = false;
	}

	private void AggregateDataTab_ResetInput()
	{
		GroupIdFieldNameComboBox.Items.Clear();
	}

	public void AggregateDataTab_SetGroupIdFieldNameItems(string[] itemNames)
	{
		GroupIdFieldNameComboBox.Items.Clear();
		GroupIdFieldNameComboBox.Items.AddRange(itemNames);
		GroupIdFieldNameComboBox.SelectedIndex = -1;
	}

	private void TypeStylesTab_Reset()
	{
		BuildingTypeMappingCheckGroupBox.Checked = false;
	}

	public void SetBuildingTypeFieldNameItems(string[] itemNames)
	{
		BuildingTypeFieldNameComboBox.Items.Clear();
		BuildingTypeFieldNameComboBox.Items.AddRange(itemNames);
		BuildingTypeFieldNameComboBox.SelectedIndex = -1;
		SyncBuildingStyleChooser();
	}

	public void ClearBuildingTypeFieldNameItems()
	{
		BuildingTypeFieldNameComboBox.Items.Clear();
		SyncBuildingStyleChooser();
	}

	public void ClearBuildingTypeStyles()
	{
		TypeStyleGridChooser.ClearStyles();
	}

	public string GetBuildingTypeFieldName()
	{
		return BuildingTypeFieldNameComboBox.Text;
	}

	private void BuildingTypeFieldNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SyncBuildingStyleChooser();
	}

	private void BuildingTypeMappingCheckGroupBox_CheckedStateChanged(object sender, EventArgs e)
	{
		if (BuildingTypeMappingCheckGroupBox.Checked)
		{
			BuildingTypeFieldNameComboBox.Enabled = true;
			SyncBuildingStyleChooser();
		}
		else
		{
			BuildingTypeFieldNameComboBox.Enabled = false;
			TypeStyleGridChooser.Enabled = false;
		}
	}

	private void SyncBuildingStyleChooser()
	{
		if (BuildingTypeMappingCheckGroupBox.Checked && BuildingTypeFieldNameComboBox.SelectedIndex != -1)
		{
			TypeStyleGridChooser.Enabled = true;
		}
		else
		{
			TypeStyleGridChooser.Enabled = false;
		}
	}

	private bool IsValidInput()
	{
		if (!ValidBuildingsFilePath)
		{
			return false;
		}
		if (HeightsCheckGroupBox.Checked)
		{
			if (!IsValidHeightFieldName())
			{
				return false;
			}
			if (!IsValidHeightUnit())
			{
				return false;
			}
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
		this.BuildingsFileGroup = new System.Windows.Forms.GroupBox();
		this.mMessagePanel = new palDotNet.palDialog.DialogMessagePanel();
		this.BuildingsFileInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.BuildingsShapefilePathInput = new palDotNet.palDialog.DialogPathLoader();
		this.BuildingHeightUnitsLabel = new palDotNet.palDialog.DialogLabel();
		this.HeightUnitsComboBox = new palDotNet.palDialog.DialogComboBox();
		this.BuildingHeightsInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.HeightsTabPage = new System.Windows.Forms.TabPage();
		this.HeightsPanel = new System.Windows.Forms.Panel();
		this.HeightsInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.RandomHeightsInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.RandomHeightMaxInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.RandomHeightMaxLabel = new palDotNet.palDialog.DialogLabel();
		this.RandomHeightMinLabel = new palDotNet.palDialog.DialogLabel();
		this.RandomHeightMinInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.HeightsCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.HeightFileFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.UnitsToMetersLabel = new palDotNet.palDialog.DialogLabel();
		this.HeightUnitConversionToMetersTextBox = new palDotNet.palDialog.DialogTextBox();
		this.HeightFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.AggregateDataTabPage = new System.Windows.Forms.TabPage();
		this.AggregateDataPanel = new System.Windows.Forms.Panel();
		this.AggregateDataCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.GroupIdFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.GroupIdFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.TypeStyleTabPage = new System.Windows.Forms.TabPage();
		this.TypeStylesPanel = new System.Windows.Forms.Panel();
		this.BuildingTypeMappingCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.BuildingTypeFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.BuildingTypeFiledNameLabel = new palDotNet.palDialog.DialogLabel();
		this.TypeStyleGridChooser = new palDotNet.palDialog.DialogStyleGridChooser();
		this.BuildingsFileGroup.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.HeightsTabPage.SuspendLayout();
		this.HeightsPanel.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.RandomHeightMaxInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RandomHeightMinInput).BeginInit();
		this.HeightsCheckGroupBox.SuspendLayout();
		this.AggregateDataTabPage.SuspendLayout();
		this.AggregateDataPanel.SuspendLayout();
		this.AggregateDataCheckGroupBox.SuspendLayout();
		this.TypeStyleTabPage.SuspendLayout();
		this.TypeStylesPanel.SuspendLayout();
		this.BuildingTypeMappingCheckGroupBox.SuspendLayout();
		base.SuspendLayout();
		this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CancelBtn.Location = new System.Drawing.Point(407, 389);
		this.CancelBtn.Name = "CancelBtn";
		this.CancelBtn.Size = new System.Drawing.Size(75, 23);
		this.CancelBtn.TabIndex = 2;
		this.CancelBtn.Text = "Cancel";
		this.CancelBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKBtn.Location = new System.Drawing.Point(326, 389);
		this.OKBtn.Name = "OKBtn";
		this.OKBtn.Size = new System.Drawing.Size(75, 23);
		this.OKBtn.TabIndex = 1;
		this.OKBtn.Text = "OK";
		this.OKBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Click += new System.EventHandler(OKBtn_Click);
		this.BuildingsFileGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.BuildingsFileGroup.Controls.Add(this.mMessagePanel);
		this.BuildingsFileGroup.Controls.Add(this.BuildingsFileInfoLabel);
		this.BuildingsFileGroup.Controls.Add(this.BuildingsShapefilePathInput);
		this.BuildingsFileGroup.Location = new System.Drawing.Point(7, 7);
		this.BuildingsFileGroup.Name = "BuildingsFileGroup";
		this.BuildingsFileGroup.Size = new System.Drawing.Size(448, 140);
		this.BuildingsFileGroup.TabIndex = 0;
		this.BuildingsFileGroup.TabStop = false;
		this.BuildingsFileGroup.Text = "Buildings file";
		this.mMessagePanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.mMessagePanel.Location = new System.Drawing.Point(22, 100);
		this.mMessagePanel.Name = "mMessagePanel";
		this.mMessagePanel.Size = new System.Drawing.Size(400, 20);
		this.mMessagePanel.TabIndex = 1;
		this.mMessagePanel.TabStop = false;
		this.BuildingsFileInfoLabel.AutoSize = true;
		this.BuildingsFileInfoLabel.Location = new System.Drawing.Point(19, 30);
		this.BuildingsFileInfoLabel.Name = "BuildingsFileInfoLabel";
		this.BuildingsFileInfoLabel.SaveToConfig = false;
		this.BuildingsFileInfoLabel.Size = new System.Drawing.Size(275, 13);
		this.BuildingsFileInfoLabel.TabIndex = 0;
		this.BuildingsFileInfoLabel.Text = "Specify a feature shapefile (*.shp) with building geometry:";
		this.BuildingsShapefilePathInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.BuildingsShapefilePathInput.BackColor = System.Drawing.Color.Transparent;
		this.BuildingsShapefilePathInput.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.BuildingsShapefilePathInput.CheckFileExists = true;
		this.BuildingsShapefilePathInput.Description = null;
		this.BuildingsShapefilePathInput.Filter = "Shapefile (*.shp)|*.shp";
		this.BuildingsShapefilePathInput.Location = new System.Drawing.Point(22, 60);
		this.BuildingsShapefilePathInput.Name = "BuildingsShapefilePathInput";
		this.BuildingsShapefilePathInput.Path = "";
		this.BuildingsShapefilePathInput.SaveToConfig = true;
		this.BuildingsShapefilePathInput.Size = new System.Drawing.Size(400, 24);
		this.BuildingsShapefilePathInput.TabIndex = 1;
		this.BuildingHeightUnitsLabel.AutoSize = true;
		this.BuildingHeightUnitsLabel.Location = new System.Drawing.Point(27, 105);
		this.BuildingHeightUnitsLabel.Name = "BuildingHeightUnitsLabel";
		this.BuildingHeightUnitsLabel.SaveToConfig = false;
		this.BuildingHeightUnitsLabel.Size = new System.Drawing.Size(34, 13);
		this.BuildingHeightUnitsLabel.TabIndex = 3;
		this.BuildingHeightUnitsLabel.Text = "Units:";
		this.HeightUnitsComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.HeightUnitsComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.HeightUnitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.HeightUnitsComboBox.FormattingEnabled = true;
		this.HeightUnitsComboBox.Location = new System.Drawing.Point(118, 102);
		this.HeightUnitsComboBox.Name = "HeightUnitsComboBox";
		this.HeightUnitsComboBox.SaveToConfig = true;
		this.HeightUnitsComboBox.Size = new System.Drawing.Size(121, 21);
		this.HeightUnitsComboBox.TabIndex = 4;
		this.HeightUnitsComboBox.SelectedIndexChanged += new System.EventHandler(HeightUnitsComboBox_SelectedIndexChanged);
		this.HeightUnitsComboBox.TextUpdate += new System.EventHandler(HeightUnitsComboBox_TextUpdate);
		this.BuildingHeightsInfoLabel.AutoSize = true;
		this.BuildingHeightsInfoLabel.Location = new System.Drawing.Point(27, 29);
		this.BuildingHeightsInfoLabel.Name = "BuildingHeightsInfoLabel";
		this.BuildingHeightsInfoLabel.SaveToConfig = false;
		this.BuildingHeightsInfoLabel.Size = new System.Drawing.Size(373, 13);
		this.BuildingHeightsInfoLabel.TabIndex = 0;
		this.BuildingHeightsInfoLabel.Text = "Building heights will be loaded from a corresponding shape attribute (*.dbf) file.";
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.HeightsTabPage);
		this.tabControl1.Controls.Add(this.AggregateDataTabPage);
		this.tabControl1.Controls.Add(this.TypeStyleTabPage);
		this.tabControl1.Location = new System.Drawing.Point(13, 13);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(469, 350);
		this.tabControl1.TabIndex = 0;
		this.tabPage1.Controls.Add(this.BuildingsFileGroup);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(461, 324);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Shapefile";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.HeightsTabPage.Controls.Add(this.HeightsPanel);
		this.HeightsTabPage.Location = new System.Drawing.Point(4, 22);
		this.HeightsTabPage.Name = "HeightsTabPage";
		this.HeightsTabPage.Padding = new System.Windows.Forms.Padding(3);
		this.HeightsTabPage.Size = new System.Drawing.Size(461, 324);
		this.HeightsTabPage.TabIndex = 1;
		this.HeightsTabPage.Text = "Heights";
		this.HeightsTabPage.UseVisualStyleBackColor = true;
		this.HeightsPanel.Controls.Add(this.HeightsInfoLabel);
		this.HeightsPanel.Controls.Add(this.groupBox1);
		this.HeightsPanel.Controls.Add(this.HeightsCheckGroupBox);
		this.HeightsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.HeightsPanel.Location = new System.Drawing.Point(3, 3);
		this.HeightsPanel.Name = "HeightsPanel";
		this.HeightsPanel.Size = new System.Drawing.Size(455, 318);
		this.HeightsPanel.TabIndex = 3;
		this.HeightsInfoLabel.AutoSize = true;
		this.HeightsInfoLabel.Location = new System.Drawing.Point(10, 18);
		this.HeightsInfoLabel.Name = "HeightsInfoLabel";
		this.HeightsInfoLabel.SaveToConfig = false;
		this.HeightsInfoLabel.Size = new System.Drawing.Size(330, 13);
		this.HeightsInfoLabel.TabIndex = 0;
		this.HeightsInfoLabel.Text = "Building heights will be randomized if they are not specified or invalid.";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.RandomHeightsInfoLabel);
		this.groupBox1.Controls.Add(this.RandomHeightMaxInput);
		this.groupBox1.Controls.Add(this.RandomHeightMaxLabel);
		this.groupBox1.Controls.Add(this.RandomHeightMinLabel);
		this.groupBox1.Controls.Add(this.RandomHeightMinInput);
		this.groupBox1.Location = new System.Drawing.Point(3, 47);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(449, 97);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Random Heights Range";
		this.RandomHeightsInfoLabel.AutoSize = true;
		this.RandomHeightsInfoLabel.Location = new System.Drawing.Point(30, 27);
		this.RandomHeightsInfoLabel.Name = "RandomHeightsInfoLabel";
		this.RandomHeightsInfoLabel.SaveToConfig = false;
		this.RandomHeightsInfoLabel.Size = new System.Drawing.Size(324, 13);
		this.RandomHeightsInfoLabel.TabIndex = 0;
		this.RandomHeightsInfoLabel.Text = "Specify the bounds for the random building height range (in meters):";
		this.RandomHeightMaxInput.DecimalPlaces = 3;
		this.RandomHeightMaxInput.Location = new System.Drawing.Point(310, 58);
		this.RandomHeightMaxInput.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.RandomHeightMaxInput.Name = "RandomHeightMaxInput";
		this.RandomHeightMaxInput.SaveToConfig = true;
		this.RandomHeightMaxInput.Size = new System.Drawing.Size(90, 20);
		this.RandomHeightMaxInput.TabIndex = 4;
		this.RandomHeightMaxLabel.AutoSize = true;
		this.RandomHeightMaxLabel.Location = new System.Drawing.Point(240, 60);
		this.RandomHeightMaxLabel.Name = "RandomHeightMaxLabel";
		this.RandomHeightMaxLabel.SaveToConfig = false;
		this.RandomHeightMaxLabel.Size = new System.Drawing.Size(62, 13);
		this.RandomHeightMaxLabel.TabIndex = 3;
		this.RandomHeightMaxLabel.Text = "Max height:";
		this.RandomHeightMinLabel.AutoSize = true;
		this.RandomHeightMinLabel.Location = new System.Drawing.Point(30, 60);
		this.RandomHeightMinLabel.Name = "RandomHeightMinLabel";
		this.RandomHeightMinLabel.SaveToConfig = false;
		this.RandomHeightMinLabel.Size = new System.Drawing.Size(59, 13);
		this.RandomHeightMinLabel.TabIndex = 1;
		this.RandomHeightMinLabel.Text = "Min height:";
		this.RandomHeightMinInput.DecimalPlaces = 3;
		this.RandomHeightMinInput.Location = new System.Drawing.Point(97, 58);
		this.RandomHeightMinInput.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.RandomHeightMinInput.Name = "RandomHeightMinInput";
		this.RandomHeightMinInput.SaveToConfig = true;
		this.RandomHeightMinInput.Size = new System.Drawing.Size(90, 20);
		this.RandomHeightMinInput.TabIndex = 2;
		this.HeightsCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.HeightsCheckGroupBox.Controls.Add(this.HeightFileFieldNameLabel);
		this.HeightsCheckGroupBox.Controls.Add(this.UnitsToMetersLabel);
		this.HeightsCheckGroupBox.Controls.Add(this.HeightUnitConversionToMetersTextBox);
		this.HeightsCheckGroupBox.Controls.Add(this.BuildingHeightsInfoLabel);
		this.HeightsCheckGroupBox.Controls.Add(this.BuildingHeightUnitsLabel);
		this.HeightsCheckGroupBox.Controls.Add(this.HeightFieldNameComboBox);
		this.HeightsCheckGroupBox.Controls.Add(this.HeightUnitsComboBox);
		this.HeightsCheckGroupBox.Location = new System.Drawing.Point(3, 150);
		this.HeightsCheckGroupBox.Name = "HeightsCheckGroupBox";
		this.HeightsCheckGroupBox.SaveToConfig = true;
		this.HeightsCheckGroupBox.Size = new System.Drawing.Size(449, 145);
		this.HeightsCheckGroupBox.TabIndex = 2;
		this.HeightsCheckGroupBox.TabStop = false;
		this.HeightsCheckGroupBox.Text = "Import heights";
		this.HeightsCheckGroupBox.CheckStateChanged += new System.EventHandler(HeightsCheckGroupBox_CheckStateChanged);
		this.HeightFileFieldNameLabel.AutoSize = true;
		this.HeightFileFieldNameLabel.Location = new System.Drawing.Point(27, 67);
		this.HeightFileFieldNameLabel.Name = "HeightFileFieldNameLabel";
		this.HeightFileFieldNameLabel.SaveToConfig = false;
		this.HeightFileFieldNameLabel.Size = new System.Drawing.Size(63, 13);
		this.HeightFileFieldNameLabel.TabIndex = 1;
		this.HeightFileFieldNameLabel.Text = "Height field:";
		this.UnitsToMetersLabel.AutoSize = true;
		this.UnitsToMetersLabel.Location = new System.Drawing.Point(256, 105);
		this.UnitsToMetersLabel.Name = "UnitsToMetersLabel";
		this.UnitsToMetersLabel.SaveToConfig = false;
		this.UnitsToMetersLabel.Size = new System.Drawing.Size(50, 13);
		this.UnitsToMetersLabel.TabIndex = 5;
		this.UnitsToMetersLabel.Text = "to meters";
		this.HeightUnitConversionToMetersTextBox.Location = new System.Drawing.Point(312, 102);
		this.HeightUnitConversionToMetersTextBox.Name = "HeightUnitConversionToMetersTextBox";
		this.HeightUnitConversionToMetersTextBox.SaveToConfig = true;
		this.HeightUnitConversionToMetersTextBox.Size = new System.Drawing.Size(104, 20);
		this.HeightUnitConversionToMetersTextBox.TabIndex = 6;
		this.HeightFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.HeightFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.HeightFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.HeightFieldNameComboBox.FormattingEnabled = true;
		this.HeightFieldNameComboBox.Location = new System.Drawing.Point(118, 64);
		this.HeightFieldNameComboBox.Name = "HeightFieldNameComboBox";
		this.HeightFieldNameComboBox.SaveToConfig = true;
		this.HeightFieldNameComboBox.Size = new System.Drawing.Size(121, 21);
		this.HeightFieldNameComboBox.Sorted = true;
		this.HeightFieldNameComboBox.TabIndex = 2;
		this.HeightFieldNameComboBox.SelectedIndexChanged += new System.EventHandler(HeightFieldNameComboBox_SelectedIndexChanged);
		this.HeightFieldNameComboBox.TextChanged += new System.EventHandler(HeightFieldNameComboBox_TextChanged);
		this.AggregateDataTabPage.Controls.Add(this.AggregateDataPanel);
		this.AggregateDataTabPage.Location = new System.Drawing.Point(4, 22);
		this.AggregateDataTabPage.Name = "AggregateDataTabPage";
		this.AggregateDataTabPage.Padding = new System.Windows.Forms.Padding(3);
		this.AggregateDataTabPage.Size = new System.Drawing.Size(461, 324);
		this.AggregateDataTabPage.TabIndex = 2;
		this.AggregateDataTabPage.Text = "Aggregate Data";
		this.AggregateDataTabPage.UseVisualStyleBackColor = true;
		this.AggregateDataPanel.Controls.Add(this.AggregateDataCheckGroupBox);
		this.AggregateDataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.AggregateDataPanel.Location = new System.Drawing.Point(3, 3);
		this.AggregateDataPanel.Name = "AggregateDataPanel";
		this.AggregateDataPanel.Size = new System.Drawing.Size(455, 318);
		this.AggregateDataPanel.TabIndex = 1;
		this.AggregateDataCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.AggregateDataCheckGroupBox.Controls.Add(this.GroupIdFieldNameLabel);
		this.AggregateDataCheckGroupBox.Controls.Add(this.GroupIdFieldNameComboBox);
		this.AggregateDataCheckGroupBox.Location = new System.Drawing.Point(3, 3);
		this.AggregateDataCheckGroupBox.Name = "AggregateDataCheckGroupBox";
		this.AggregateDataCheckGroupBox.SaveToConfig = true;
		this.AggregateDataCheckGroupBox.Size = new System.Drawing.Size(449, 81);
		this.AggregateDataCheckGroupBox.TabIndex = 0;
		this.AggregateDataCheckGroupBox.TabStop = false;
		this.AggregateDataCheckGroupBox.Text = "Aggregate data";
		this.GroupIdFieldNameLabel.AutoSize = true;
		this.GroupIdFieldNameLabel.Location = new System.Drawing.Point(32, 33);
		this.GroupIdFieldNameLabel.Name = "GroupIdFieldNameLabel";
		this.GroupIdFieldNameLabel.SaveToConfig = false;
		this.GroupIdFieldNameLabel.Size = new System.Drawing.Size(104, 13);
		this.GroupIdFieldNameLabel.TabIndex = 0;
		this.GroupIdFieldNameLabel.Text = "Group ID field name:";
		this.GroupIdFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.GroupIdFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.GroupIdFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.GroupIdFieldNameComboBox.FormattingEnabled = true;
		this.GroupIdFieldNameComboBox.Location = new System.Drawing.Point(189, 30);
		this.GroupIdFieldNameComboBox.Name = "GroupIdFieldNameComboBox";
		this.GroupIdFieldNameComboBox.SaveToConfig = true;
		this.GroupIdFieldNameComboBox.Size = new System.Drawing.Size(160, 21);
		this.GroupIdFieldNameComboBox.Sorted = true;
		this.GroupIdFieldNameComboBox.TabIndex = 1;
		this.TypeStyleTabPage.Controls.Add(this.TypeStylesPanel);
		this.TypeStyleTabPage.Location = new System.Drawing.Point(4, 22);
		this.TypeStyleTabPage.Name = "TypeStyleTabPage";
		this.TypeStyleTabPage.Padding = new System.Windows.Forms.Padding(3);
		this.TypeStyleTabPage.Size = new System.Drawing.Size(461, 324);
		this.TypeStyleTabPage.TabIndex = 3;
		this.TypeStyleTabPage.Text = "Type Styles";
		this.TypeStyleTabPage.UseVisualStyleBackColor = true;
		this.TypeStylesPanel.Controls.Add(this.BuildingTypeMappingCheckGroupBox);
		this.TypeStylesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TypeStylesPanel.Location = new System.Drawing.Point(3, 3);
		this.TypeStylesPanel.Name = "TypeStylesPanel";
		this.TypeStylesPanel.Size = new System.Drawing.Size(455, 318);
		this.TypeStylesPanel.TabIndex = 1;
		this.BuildingTypeMappingCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.BuildingTypeMappingCheckGroupBox.Controls.Add(this.BuildingTypeFieldNameComboBox);
		this.BuildingTypeMappingCheckGroupBox.Controls.Add(this.BuildingTypeFiledNameLabel);
		this.BuildingTypeMappingCheckGroupBox.Controls.Add(this.TypeStyleGridChooser);
		this.BuildingTypeMappingCheckGroupBox.Location = new System.Drawing.Point(3, 3);
		this.BuildingTypeMappingCheckGroupBox.Name = "BuildingTypeMappingCheckGroupBox";
		this.BuildingTypeMappingCheckGroupBox.SaveToConfig = true;
		this.BuildingTypeMappingCheckGroupBox.Size = new System.Drawing.Size(449, 312);
		this.BuildingTypeMappingCheckGroupBox.TabIndex = 0;
		this.BuildingTypeMappingCheckGroupBox.TabStop = false;
		this.BuildingTypeMappingCheckGroupBox.Text = "Building types";
		this.BuildingTypeMappingCheckGroupBox.CheckStateChanged += new System.EventHandler(BuildingTypeMappingCheckGroupBox_CheckedStateChanged);
		this.BuildingTypeFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.BuildingTypeFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.BuildingTypeFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.BuildingTypeFieldNameComboBox.FormattingEnabled = true;
		this.BuildingTypeFieldNameComboBox.Location = new System.Drawing.Point(202, 27);
		this.BuildingTypeFieldNameComboBox.Name = "BuildingTypeFieldNameComboBox";
		this.BuildingTypeFieldNameComboBox.SaveToConfig = true;
		this.BuildingTypeFieldNameComboBox.Size = new System.Drawing.Size(160, 21);
		this.BuildingTypeFieldNameComboBox.Sorted = true;
		this.BuildingTypeFieldNameComboBox.TabIndex = 1;
		this.BuildingTypeFieldNameComboBox.SelectedIndexChanged += new System.EventHandler(BuildingTypeFieldNameComboBox_SelectedIndexChanged);
		this.BuildingTypeFiledNameLabel.AutoSize = true;
		this.BuildingTypeFiledNameLabel.Location = new System.Drawing.Point(72, 30);
		this.BuildingTypeFiledNameLabel.Name = "BuildingTypeFiledNameLabel";
		this.BuildingTypeFiledNameLabel.SaveToConfig = false;
		this.BuildingTypeFiledNameLabel.Size = new System.Drawing.Size(85, 13);
		this.BuildingTypeFiledNameLabel.TabIndex = 0;
		this.BuildingTypeFiledNameLabel.Text = "Type field name:";
		this.TypeStyleGridChooser.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TypeStyleGridChooser.Location = new System.Drawing.Point(13, 71);
		this.TypeStyleGridChooser.Margin = new System.Windows.Forms.Padding(10);
		this.TypeStyleGridChooser.MinimumSize = new System.Drawing.Size(0, 140);
		this.TypeStyleGridChooser.Name = "TypeStyleGridChooser";
		this.TypeStyleGridChooser.Size = new System.Drawing.Size(423, 228);
		this.TypeStyleGridChooser.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CancelBtn;
		base.ClientSize = new System.Drawing.Size(494, 424);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.OKBtn);
		base.Controls.Add(this.CancelBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GisImportBuildingsDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "GIS Mode - Import Buildings";
		this.BuildingsFileGroup.ResumeLayout(false);
		this.BuildingsFileGroup.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.HeightsTabPage.ResumeLayout(false);
		this.HeightsPanel.ResumeLayout(false);
		this.HeightsPanel.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.RandomHeightMaxInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RandomHeightMinInput).EndInit();
		this.HeightsCheckGroupBox.ResumeLayout(false);
		this.HeightsCheckGroupBox.PerformLayout();
		this.AggregateDataTabPage.ResumeLayout(false);
		this.AggregateDataPanel.ResumeLayout(false);
		this.AggregateDataCheckGroupBox.ResumeLayout(false);
		this.AggregateDataCheckGroupBox.PerformLayout();
		this.TypeStyleTabPage.ResumeLayout(false);
		this.TypeStylesPanel.ResumeLayout(false);
		this.BuildingTypeMappingCheckGroupBox.ResumeLayout(false);
		this.BuildingTypeMappingCheckGroupBox.PerformLayout();
		base.ResumeLayout(false);
	}
}
