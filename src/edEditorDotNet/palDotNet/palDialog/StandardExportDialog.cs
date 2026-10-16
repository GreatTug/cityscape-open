using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using edEditorDotNet;
using edEditorDotNet.ExportDialogItems;
using edEditorDotNet.Properties;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edConvert\\StandardExportDialog")]
[DialogBoxCreator]
public class StandardExportDialog : DialogBoxBase
{
	private IContainer components;

	private Button cancelBtn;

	private Button okBtn;

	private ExportRange exportRange1;

	private ExportTileGrouping exportTileGrouping1;

	private ExportOptions exportOptions1;

	private ExportMeshMerging exportMeshMerging1;

	private ExportPathControl exportPath1;

	private ExportTextureSaving exportTextureSaving1;

	private ExportTextureTypes exportTextureTypes1;

	private ExportRelativePathControl exportRelativePathControl1;

	private ExportProps exportProps1;

	private ExportType exportType1;

	private ExportCoordinate exportCoordinate1;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private TabPage customTab;

	private ExportFilmbox Filmbox;

	private ExportOpenFlight OpenFlight;

	private ExportNavteq Navteq;

	private ExportCustomBase ExportCustomOptions;

	private PictureBox pictureBox1;

	private ExportBuildings exportBuildings1;

	private Hashtable CustomOptions;

	private ExportCustomBase CurrentCustomOption;

	private bool CustomTabRemoved;

	private UiEnableManager uiManager;

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
		this.exportRange1 = new edEditorDotNet.ExportRange();
		this.exportTileGrouping1 = new edEditorDotNet.ExportTileGrouping();
		this.exportOptions1 = new edEditorDotNet.ExportOptions();
		this.exportMeshMerging1 = new edEditorDotNet.ExportMeshMerging();
		this.exportPath1 = new edEditorDotNet.ExportDialogItems.ExportPathControl();
		this.exportTextureSaving1 = new edEditorDotNet.ExportTextureSaving();
		this.exportTextureTypes1 = new edEditorDotNet.ExportTextureTypes();
		this.exportRelativePathControl1 = new edEditorDotNet.ExportRelativePathControl();
		this.exportProps1 = new edEditorDotNet.ExportProps();
		this.exportType1 = new edEditorDotNet.ExportType();
		this.exportCoordinate1 = new edEditorDotNet.ExportCoordinate();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.exportBuildings1 = new edEditorDotNet.ExportBuildings();
		this.customTab = new System.Windows.Forms.TabPage();
		this.ExportCustomOptions = new edEditorDotNet.ExportCustomBase();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.customTab.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelBtn.Location = new System.Drawing.Point(454, 593);
		this.cancelBtn.Name = "cancelBtn";
		this.cancelBtn.Size = new System.Drawing.Size(75, 23);
		this.cancelBtn.TabIndex = 1;
		this.cancelBtn.Text = "Cancel";
		this.cancelBtn.UseVisualStyleBackColor = true;
		this.okBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okBtn.Location = new System.Drawing.Point(373, 593);
		this.okBtn.Name = "okBtn";
		this.okBtn.Size = new System.Drawing.Size(75, 23);
		this.okBtn.TabIndex = 0;
		this.okBtn.Text = "OK";
		this.okBtn.UseVisualStyleBackColor = true;
		this.exportRange1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportRange1.Location = new System.Drawing.Point(6, 6);
		this.exportRange1.Name = "exportRange1";
		this.exportRange1.Size = new System.Drawing.Size(499, 137);
		this.exportRange1.TabIndex = 2;
		this.exportTileGrouping1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportTileGrouping1.Extension = "dae";
		this.exportTileGrouping1.Location = new System.Drawing.Point(6, 302);
		this.exportTileGrouping1.Name = "exportTileGrouping1";
		this.exportTileGrouping1.Size = new System.Drawing.Size(363, 110);
		this.exportTileGrouping1.TabIndex = 4;
		this.exportOptions1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportOptions1.Location = new System.Drawing.Point(6, 149);
		this.exportOptions1.Name = "exportOptions1";
		this.exportOptions1.Size = new System.Drawing.Size(499, 147);
		this.exportOptions1.TabIndex = 3;
		this.exportMeshMerging1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportMeshMerging1.Location = new System.Drawing.Point(375, 302);
		this.exportMeshMerging1.Name = "exportMeshMerging1";
		this.exportMeshMerging1.Size = new System.Drawing.Size(130, 110);
		this.exportMeshMerging1.TabIndex = 5;
		this.exportPath1.Filter = "";
		this.exportPath1.Location = new System.Drawing.Point(12, 518);
		this.exportPath1.Name = "exportPath1";
		this.exportPath1.Path = "";
		this.exportPath1.Size = new System.Drawing.Size(519, 57);
		this.exportPath1.TabIndex = 6;
		this.exportTextureSaving1.Location = new System.Drawing.Point(6, 6);
		this.exportTextureSaving1.Name = "exportTextureSaving1";
		this.exportTextureSaving1.Size = new System.Drawing.Size(268, 130);
		this.exportTextureSaving1.TabIndex = 2;
		this.exportTextureTypes1.Location = new System.Drawing.Point(280, 6);
		this.exportTextureTypes1.Name = "exportTextureTypes1";
		this.exportTextureTypes1.Size = new System.Drawing.Size(225, 130);
		this.exportTextureTypes1.TabIndex = 3;
		this.exportRelativePathControl1.Location = new System.Drawing.Point(6, 322);
		this.exportRelativePathControl1.Name = "exportRelativePathControl1";
		this.exportRelativePathControl1.Size = new System.Drawing.Size(499, 101);
		this.exportRelativePathControl1.TabIndex = 7;
		this.exportProps1.Location = new System.Drawing.Point(6, 142);
		this.exportProps1.Name = "exportProps1";
		this.exportProps1.Size = new System.Drawing.Size(268, 174);
		this.exportProps1.TabIndex = 4;
		this.exportType1.Location = new System.Drawing.Point(161, 12);
		this.exportType1.Name = "exportType1";
		this.exportType1.Size = new System.Drawing.Size(196, 30);
		this.exportType1.TabIndex = 34;
		this.exportCoordinate1.Location = new System.Drawing.Point(280, 221);
		this.exportCoordinate1.Name = "exportCoordinate1";
		this.exportCoordinate1.Size = new System.Drawing.Size(225, 95);
		this.exportCoordinate1.TabIndex = 6;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.customTab);
		this.tabControl1.Location = new System.Drawing.Point(12, 48);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(519, 464);
		this.tabControl1.TabIndex = 36;
		this.tabPage1.Controls.Add(this.exportRange1);
		this.tabPage1.Controls.Add(this.exportOptions1);
		this.tabPage1.Controls.Add(this.exportTileGrouping1);
		this.tabPage1.Controls.Add(this.exportMeshMerging1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(511, 438);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Scene Options";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tabPage2.Controls.Add(this.exportBuildings1);
		this.tabPage2.Controls.Add(this.exportTextureSaving1);
		this.tabPage2.Controls.Add(this.exportCoordinate1);
		this.tabPage2.Controls.Add(this.exportRelativePathControl1);
		this.tabPage2.Controls.Add(this.exportTextureTypes1);
		this.tabPage2.Controls.Add(this.exportProps1);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(511, 438);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Advanced Options";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.exportBuildings1.Location = new System.Drawing.Point(281, 142);
		this.exportBuildings1.Name = "exportBuildings1";
		this.exportBuildings1.Size = new System.Drawing.Size(224, 73);
		this.exportBuildings1.TabIndex = 5;
		this.customTab.Controls.Add(this.ExportCustomOptions);
		this.customTab.Location = new System.Drawing.Point(4, 22);
		this.customTab.Name = "customTab";
		this.customTab.Padding = new System.Windows.Forms.Padding(3);
		this.customTab.Size = new System.Drawing.Size(511, 438);
		this.customTab.TabIndex = 2;
		this.customTab.Text = "Custom Options";
		this.customTab.UseVisualStyleBackColor = true;
		this.ExportCustomOptions.Location = new System.Drawing.Point(6, 3);
		this.ExportCustomOptions.MaximumSize = new System.Drawing.Size(499, 385);
		this.ExportCustomOptions.MinimumSize = new System.Drawing.Size(499, 385);
		this.ExportCustomOptions.Name = "ExportCustomOptions";
		this.ExportCustomOptions.Size = new System.Drawing.Size(499, 385);
		this.ExportCustomOptions.TabIndex = 0;
		this.pictureBox1.Image = edEditorDotNet.Properties.Resources.Export;
		this.pictureBox1.Location = new System.Drawing.Point(417, 12);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(48, 48);
		this.pictureBox1.TabIndex = 37;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.ClientSize = new System.Drawing.Size(540, 630);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.exportType1);
		base.Controls.Add(this.exportPath1);
		base.Controls.Add(this.cancelBtn);
		base.Controls.Add(this.okBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "StandardExportDialog";
		this.Text = "Export";
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.customTab.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}

	public StandardExportDialog()
	{
		InitializeComponent();
		CurrentCustomOption = ExportCustomOptions;
		CustomTabRemoved = false;
		CustomOptions = new Hashtable();
		Filmbox = new ExportFilmbox();
		OpenFlight = new ExportOpenFlight();
		Navteq = new ExportNavteq();
		AddCustomOption(Filmbox);
		AddCustomOption(OpenFlight);
		AddCustomOption(Navteq);
		okBtn.Click += exportPath1.okBtn_Click;
		exportType1.GetExporterTypeComboBox().SelectedIndexChanged += StandardExportDialog_SelectedIndexChanged;
		exportOptions1.ExportPropsCheckBox.CheckedChanged += StandardExportDialog_CheckedChanged;
		uiManager = new UiEnableManager();
		uiManager.AddCheckBox(exportOptions1.ExportGraphicsCheckBox);
		uiManager.AddCheckBox(exportOptions1.ExportBuildingPlansCheckBox);
		uiManager.AddCheckBox(exportOptions1.ExportDataCheckBox);
		uiManager.AddCheckBox(exportOptions1.ExportLightingCheckBox);
		uiManager.AddCheckBox(exportOptions1.ExportNavMeshCheckBox);
		uiManager.AddCheckBox(exportOptions1.ExportPhysicsCheckBox);
		uiManager.AddCheckBox(exportOptions1.ExportPropsCheckBox);
		uiManager.AddCheckBox(exportOptions1.ExportTrafficCheckBox);
		uiManager.AddCheckBox(exportTextureTypes1.DiffuseCheckBox);
		uiManager.AddCheckBox(exportTextureTypes1.DiffuseDetailCheckBox);
		uiManager.AddCheckBox(exportTextureTypes1.NormalCheckBox);
		uiManager.AddCheckBox(exportTextureTypes1.SpecularCheckBox);
		uiManager.AddCheckBox(exportTextureTypes1.SpecularDetailCheckBox);
		uiManager.AddCheckBox(exportTextureTypes1.TerrainBlendCheckBox);
		uiManager.AddCheckBox(exportProps1.EmbedPropsCheckBox);
		uiManager.AddCheckBox(exportProps1.ReferenceFilesDifferentCheckBox);
		uiManager.AddRadioButton(exportProps1.ReferenceFilesSameRadioButton);
		uiManager.AddRadioButton(exportProps1.ReferenceFilesCopySameRadioButton);
		uiManager.AddRadioButton(exportCoordinate1.YAxisUpRadioButton);
		uiManager.AddRadioButton(exportCoordinate1.ZAxisUpRadioButton);
		uiManager.AddCheckBox(exportCoordinate1.ZeroOutMinHeightCheckBox);
		uiManager.AddCheckBoxPostfix(exportOptions1.ExportTrafficCheckBox, " (XML)");
		uiManager.AddCheckBoxPostfix(exportOptions1.ExportPropsCheckBox, " (XML)");
		uiManager.AddCheckBoxPostfix(exportOptions1.ExportDataCheckBox, " (XML)");
		ExportOptions.MergingOption[] mergingOptions = exportOptions1.MergingOptions;
		foreach (ExportOptions.MergingOption option in mergingOptions)
		{
			uiManager.AddMerge(option);
		}
	}

	private void UpdateUI()
	{
		UpdateProps();
	}

	private void AddCustomOption(ExportCustomBase customOption)
	{
		customOption.Location = new Point(6, 6);
		customOption.MaximumSize = new Size(499, 385);
		customOption.MinimumSize = new Size(499, 385);
		customOption.Name = "ExportCustomOptions";
		customOption.Size = new Size(499, 385);
		customOption.TabIndex = 0;
		customOption.Visible = false;
		customTab.Controls.Add(customOption);
		CustomOptions.Add(customOption.ExporterTypeName, customOption);
		SetToolTipsOnControls(customOption.Controls);
	}

	private void StandardExportDialog_CheckedChanged(object sender, EventArgs e)
	{
		UpdateProps();
	}

	private void UpdateProps()
	{
		uiManager.UpdateValues();
		exportProps1.Enabled = exportOptions1.ExportPropsCheckBox.Checked;
	}

	private void StandardExportDialog_SelectedIndexChanged(object sender, EventArgs e)
	{
		SuspendLayout();
		SetFileFilters(exportType1.GetFilter(), exportType1.GetPrimaryExtension());
		SetEnabledOptions(exportType1.GetSupportedOptions());
		SetCustomOptions(exportType1.GetExporterType());
		ResumeLayout(performLayout: true);
	}

	private void SetFileFilters(string filter, string baseExt)
	{
		if (filter == null)
		{
			return;
		}
		exportPath1.Filter = filter;
		string text = exportPath1.Path;
		if (text != null && text != "" && baseExt != null && baseExt != null && baseExt != "")
		{
			int num = text.LastIndexOf('.');
			if (num > -1)
			{
				text = text.Remove(num);
			}
			exportPath1.Path = text + "." + baseExt;
		}
	}

	private void SetEnabledOptions(string supportedOptions)
	{
		string[] array = supportedOptions.Split(';');
		string[] array2 = array;
		foreach (string name in array2)
		{
			uiManager.EnableElement(name);
		}
		uiManager.DisableUnset();
		exportCoordinate1.UpdateUI();
		exportOptions1.UpdateUI();
		exportProps1.UpdateUI();
		UpdateUI();
	}

	private void SetCustomOptions(string exporterType)
	{
		ExportCustomBase exportCustomBase = CustomOptions[exporterType] as ExportCustomBase;
		if (exportCustomBase == null)
		{
			if (!CustomTabRemoved)
			{
				CustomTabRemoved = true;
				if (tabControl1.SelectedIndex == 2)
				{
					tabControl1.SelectedIndex = 0;
				}
				tabControl1.Controls.Remove(customTab);
			}
			exportCustomBase = ExportCustomOptions;
		}
		else
		{
			if (CustomTabRemoved)
			{
				CustomTabRemoved = false;
				tabControl1.Controls.Add(customTab);
			}
			customTab.Text = exporterType + " Options";
		}
		CurrentCustomOption.Visible = false;
		CurrentCustomOption = exportCustomBase;
		CurrentCustomOption.Visible = true;
		exportProps1.ForceEmbeddedProps = exportCustomBase.ShouldForceEmbeddedProps;
	}
}
