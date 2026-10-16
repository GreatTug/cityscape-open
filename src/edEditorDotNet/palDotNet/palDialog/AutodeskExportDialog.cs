using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using edEditorDotNet;
using edEditorDotNet.ExportDialogItems;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edConvert\\AutodeskExportDialog")]
public class AutodeskExportDialog : DialogBoxBase
{
	private ExportFilmbox FilmboxPanel;

	private IContainer components;

	private ExportPathControl exportPath1;

	private Button cancelBtn;

	private Button okBtn;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private ExportRange exportRange1;

	private ExportOptions exportOptions1;

	private ExportTileGrouping exportTileGrouping1;

	private ExportMeshMerging exportMeshMerging1;

	private TabPage tabPage2;

	private ExportBuildings exportBuildings1;

	private ExportTextureSaving exportTextureSaving1;

	private ExportCoordinate exportCoordinate1;

	private ExportRelativePathControl exportRelativePathControl1;

	private ExportTextureTypes exportTextureTypes1;

	private ExportProps exportProps1;

	private TabPage customTab;

	private ExportCustomBase ExportCustomOptions;

	private GroupBox ExportOptionsGroup;

	private ExportType exportType1;

	private Label ExportOptionsInfoLabel;

	private CheckBox EmbedTexturesCheckBox;

	private Label TextureFormatLabel;

	private ComboBox TextureFormatComboBox;

	private PictureBox ExportIconPictureBox;

	public AutodeskExportDialog()
	{
		InitializeComponent();
		okBtn.Click += exportPath1.okBtn_Click;
		FilmboxPanel = new ExportFilmbox();
		FilmboxPanel.Visible = false;
		base.Controls.Add(FilmboxPanel);
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		SetFileFilters(exportType1.GetFilter(), exportType1.GetPrimaryExtension());
		int count = exportTextureSaving1.TextureFormatComboBox.Items.Count;
		object[] array = new object[count];
		exportTextureSaving1.TextureFormatComboBox.Items.CopyTo(array, 0);
		TextureFormatComboBox.Items.Clear();
		TextureFormatComboBox.Items.AddRange(array);
		SetExportOptionDefaults();
		SyncTextureFormatControls();
	}

	private void SetExportOptionDefaults()
	{
		exportRange1.ExportAllTilesRadioButton.Checked = true;
		exportRange1.ExportTileToSingleFileCheckBox.Checked = true;
		exportOptions1.ExportGraphicsCheckBox.Checked = true;
		exportOptions1.ExportPropsCheckBox.Checked = true;
		exportOptions1.ExportTrafficCheckBox.Checked = false;
		exportOptions1.ExportLightingCheckBox.Checked = false;
		exportOptions1.AlwaysAllowMergeToSingleFile = true;
		exportOptions1.MergeToSingleFileCheckBox.Checked = true;
		exportOptions1.ExportBuildingPlansCheckBox.Checked = true;
		exportOptions1.ExportPhysicsCheckBox.Checked = false;
		exportOptions1.ExportNavMeshCheckBox.Checked = false;
		exportOptions1.ExportDataCheckBox.Checked = false;
		exportOptions1.ExportTrafficEdgeCurvesCheckBox.Checked = false;
		exportTileGrouping1.ExportLodsIndividuallyRadioButton.Checked = true;
		exportTileGrouping1.ExportLodsIndividuallyApplyToPropsCheckBox.Checked = true;
		exportMeshMerging1.MergeTerrainCheckBox.Checked = true;
		exportMeshMerging1.MergeRoadsCheckBox.Checked = true;
		exportMeshMerging1.MergeAll.Checked = true;
		exportTextureSaving1.SaveEmbeddedTexturesCheckBox.Checked = true;
		exportTextureSaving1.SaveSourceTexturesCheckBox.Checked = true;
		int count = exportTextureSaving1.TextureFormatComboBox.Items.Count;
		for (int i = 0; i < count; i++)
		{
			string itemText = TextureFormatComboBox.GetItemText(exportTextureSaving1.TextureFormatComboBox.Items[i]);
			if (itemText.ToLower() == "png")
			{
				TextureFormatComboBox.SelectedIndex = i;
				break;
			}
		}
		exportTextureSaving1.TexturePathUseExportPathCheckBox.Checked = true;
		exportTextureSaving1.OverwriteTexturesCheckBox.Checked = true;
		exportTextureTypes1.DiffuseCheckBox.Checked = true;
		exportTextureTypes1.DiffuseDetailCheckBox.Checked = false;
		exportTextureTypes1.NormalCheckBox.Checked = true;
		exportTextureTypes1.SpecularCheckBox.Checked = true;
		exportTextureTypes1.SpecularDetailCheckBox.Checked = false;
		exportTextureTypes1.TerrainBlendCheckBox.Checked = false;
		exportProps1.EmbedPropsCheckBox.Checked = true;
		exportProps1.ReferenceFilesDifferentCheckBox.Checked = true;
		exportProps1.ReferenceOutputPathUseExportPathCheckBox.Checked = true;
		exportProps1.ReferenceFilesSameRadioButton.Checked = true;
		exportProps1.OverwritePropsCheckBox.Checked = true;
		exportBuildings1.SplitBuildingsOnTileBoundsCheckBox.Checked = false;
		exportCoordinate1.ZAxisUpRadioButton.Checked = true;
		exportCoordinate1.ZeroOutMinHeightCheckBox.Checked = false;
		exportRelativePathControl1.AllRelativeToExportPathCheckBox.Checked = true;
		CheckBox embedTexturesCheckBox = FilmboxPanel.EmbedTexturesCheckBox;
		bool flag = (EmbedTexturesCheckBox.Checked = false);
		embedTexturesCheckBox.Checked = flag;
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

	private void SyncTextureFormatControls()
	{
		bool flag = EmbedTexturesCheckBox.Checked;
		TextureFormatLabel.Enabled = !flag;
		TextureFormatComboBox.Enabled = !flag;
	}

	private void EmbedTexturesCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		FilmboxPanel.EmbedTexturesCheckBox.Checked = EmbedTexturesCheckBox.Checked;
		SyncTextureFormatControls();
	}

	private void TextureFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		exportTextureSaving1.TextureFormatComboBox.SelectedIndex = TextureFormatComboBox.SelectedIndex;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(palDotNet.palDialog.AutodeskExportDialog));
		this.cancelBtn = new System.Windows.Forms.Button();
		this.okBtn = new System.Windows.Forms.Button();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.exportRange1 = new edEditorDotNet.ExportRange();
		this.exportOptions1 = new edEditorDotNet.ExportOptions();
		this.exportTileGrouping1 = new edEditorDotNet.ExportTileGrouping();
		this.exportMeshMerging1 = new edEditorDotNet.ExportMeshMerging();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.exportBuildings1 = new edEditorDotNet.ExportBuildings();
		this.exportTextureSaving1 = new edEditorDotNet.ExportTextureSaving();
		this.exportCoordinate1 = new edEditorDotNet.ExportCoordinate();
		this.exportRelativePathControl1 = new edEditorDotNet.ExportRelativePathControl();
		this.exportTextureTypes1 = new edEditorDotNet.ExportTextureTypes();
		this.exportProps1 = new edEditorDotNet.ExportProps();
		this.customTab = new System.Windows.Forms.TabPage();
		this.ExportCustomOptions = new edEditorDotNet.ExportCustomBase();
		this.ExportOptionsGroup = new System.Windows.Forms.GroupBox();
		this.TextureFormatLabel = new System.Windows.Forms.Label();
		this.TextureFormatComboBox = new System.Windows.Forms.ComboBox();
		this.EmbedTexturesCheckBox = new System.Windows.Forms.CheckBox();
		this.ExportOptionsInfoLabel = new System.Windows.Forms.Label();
		this.ExportIconPictureBox = new System.Windows.Forms.PictureBox();
		this.exportType1 = new edEditorDotNet.ExportType();
		this.exportPath1 = new edEditorDotNet.ExportDialogItems.ExportPathControl();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.customTab.SuspendLayout();
		this.ExportOptionsGroup.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ExportIconPictureBox).BeginInit();
		base.SuspendLayout();
		this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelBtn.Location = new System.Drawing.Point(402, 215);
		this.cancelBtn.Name = "cancelBtn";
		this.cancelBtn.Size = new System.Drawing.Size(75, 23);
		this.cancelBtn.TabIndex = 8;
		this.cancelBtn.Text = "Cancel";
		this.cancelBtn.UseVisualStyleBackColor = true;
		this.okBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okBtn.Location = new System.Drawing.Point(321, 215);
		this.okBtn.Name = "okBtn";
		this.okBtn.Size = new System.Drawing.Size(75, 23);
		this.okBtn.TabIndex = 7;
		this.okBtn.Text = "OK";
		this.okBtn.UseVisualStyleBackColor = true;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.customTab);
		this.tabControl1.Location = new System.Drawing.Point(632, 60);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(519, 464);
		this.tabControl1.TabIndex = 37;
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
		this.exportRange1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportRange1.Location = new System.Drawing.Point(6, 6);
		this.exportRange1.Name = "exportRange1";
		this.exportRange1.Size = new System.Drawing.Size(499, 137);
		this.exportRange1.TabIndex = 2;
		this.exportOptions1.AlwaysAllowMergeToSingleFile = false;
		this.exportOptions1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportOptions1.Location = new System.Drawing.Point(6, 149);
		this.exportOptions1.Name = "exportOptions1";
		this.exportOptions1.Size = new System.Drawing.Size(499, 147);
		this.exportOptions1.TabIndex = 3;
		this.exportTileGrouping1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportTileGrouping1.Extension = "dae";
		this.exportTileGrouping1.Location = new System.Drawing.Point(6, 302);
		this.exportTileGrouping1.Name = "exportTileGrouping1";
		this.exportTileGrouping1.Size = new System.Drawing.Size(363, 110);
		this.exportTileGrouping1.TabIndex = 4;
		this.exportMeshMerging1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportMeshMerging1.Location = new System.Drawing.Point(375, 302);
		this.exportMeshMerging1.Name = "exportMeshMerging1";
		this.exportMeshMerging1.Size = new System.Drawing.Size(130, 110);
		this.exportMeshMerging1.TabIndex = 5;
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
		this.exportTextureSaving1.Location = new System.Drawing.Point(6, 6);
		this.exportTextureSaving1.Name = "exportTextureSaving1";
		this.exportTextureSaving1.Size = new System.Drawing.Size(268, 130);
		this.exportTextureSaving1.TabIndex = 2;
		this.exportCoordinate1.Location = new System.Drawing.Point(280, 221);
		this.exportCoordinate1.Name = "exportCoordinate1";
		this.exportCoordinate1.Size = new System.Drawing.Size(225, 95);
		this.exportCoordinate1.TabIndex = 6;
		this.exportRelativePathControl1.Location = new System.Drawing.Point(6, 322);
		this.exportRelativePathControl1.Name = "exportRelativePathControl1";
		this.exportRelativePathControl1.Size = new System.Drawing.Size(499, 101);
		this.exportRelativePathControl1.TabIndex = 7;
		this.exportTextureTypes1.Location = new System.Drawing.Point(280, 6);
		this.exportTextureTypes1.Name = "exportTextureTypes1";
		this.exportTextureTypes1.Size = new System.Drawing.Size(225, 130);
		this.exportTextureTypes1.TabIndex = 3;
		this.exportProps1.Location = new System.Drawing.Point(6, 142);
		this.exportProps1.Name = "exportProps1";
		this.exportProps1.Size = new System.Drawing.Size(268, 174);
		this.exportProps1.TabIndex = 4;
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
		this.ExportOptionsGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ExportOptionsGroup.Controls.Add(this.TextureFormatLabel);
		this.ExportOptionsGroup.Controls.Add(this.TextureFormatComboBox);
		this.ExportOptionsGroup.Controls.Add(this.EmbedTexturesCheckBox);
		this.ExportOptionsGroup.Location = new System.Drawing.Point(13, 74);
		this.ExportOptionsGroup.Name = "ExportOptionsGroup";
		this.ExportOptionsGroup.Size = new System.Drawing.Size(465, 72);
		this.ExportOptionsGroup.TabIndex = 38;
		this.ExportOptionsGroup.TabStop = false;
		this.TextureFormatLabel.AutoSize = true;
		this.TextureFormatLabel.Location = new System.Drawing.Point(6, 44);
		this.TextureFormatLabel.Name = "TextureFormatLabel";
		this.TextureFormatLabel.Size = new System.Drawing.Size(109, 13);
		this.TextureFormatLabel.TabIndex = 2;
		this.TextureFormatLabel.Text = "Output texture format:";
		this.TextureFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.TextureFormatComboBox.FormattingEnabled = true;
		this.TextureFormatComboBox.Location = new System.Drawing.Point(159, 41);
		this.TextureFormatComboBox.Name = "TextureFormatComboBox";
		this.TextureFormatComboBox.Size = new System.Drawing.Size(70, 21);
		this.TextureFormatComboBox.TabIndex = 1;
		this.TextureFormatComboBox.SelectedIndexChanged += new System.EventHandler(TextureFormatComboBox_SelectedIndexChanged);
		this.EmbedTexturesCheckBox.AutoSize = true;
		this.EmbedTexturesCheckBox.Location = new System.Drawing.Point(9, 19);
		this.EmbedTexturesCheckBox.Name = "EmbedTexturesCheckBox";
		this.EmbedTexturesCheckBox.Size = new System.Drawing.Size(99, 17);
		this.EmbedTexturesCheckBox.TabIndex = 0;
		this.EmbedTexturesCheckBox.Text = "Embed textures";
		this.EmbedTexturesCheckBox.UseVisualStyleBackColor = true;
		this.EmbedTexturesCheckBox.CheckedChanged += new System.EventHandler(EmbedTexturesCheckBox_CheckedChanged);
		this.ExportOptionsInfoLabel.Location = new System.Drawing.Point(19, 24);
		this.ExportOptionsInfoLabel.Margin = new System.Windows.Forms.Padding(10, 15, 15, 15);
		this.ExportOptionsInfoLabel.Name = "ExportOptionsInfoLabel";
		this.ExportOptionsInfoLabel.Size = new System.Drawing.Size(406, 32);
		this.ExportOptionsInfoLabel.TabIndex = 0;
		this.ExportOptionsInfoLabel.Text = "This will export all tiles and their props into a single FBX file.";
		this.ExportIconPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ExportIconPictureBox.Image = (System.Drawing.Image)resources.GetObject("ExportIconPictureBox.Image");
		this.ExportIconPictureBox.Location = new System.Drawing.Point(430, 12);
		this.ExportIconPictureBox.Name = "ExportIconPictureBox";
		this.ExportIconPictureBox.Size = new System.Drawing.Size(48, 48);
		this.ExportIconPictureBox.TabIndex = 39;
		this.ExportIconPictureBox.TabStop = false;
		this.exportType1.Location = new System.Drawing.Point(556, 134);
		this.exportType1.Name = "exportType1";
		this.exportType1.Size = new System.Drawing.Size(196, 30);
		this.exportType1.TabIndex = 35;
		this.exportPath1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.exportPath1.Filter = "";
		this.exportPath1.Location = new System.Drawing.Point(13, 152);
		this.exportPath1.Name = "exportPath1";
		this.exportPath1.Path = "";
		this.exportPath1.Size = new System.Drawing.Size(466, 57);
		this.exportPath1.TabIndex = 9;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(490, 251);
		base.Controls.Add(this.ExportIconPictureBox);
		base.Controls.Add(this.ExportOptionsInfoLabel);
		base.Controls.Add(this.exportType1);
		base.Controls.Add(this.ExportOptionsGroup);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.exportPath1);
		base.Controls.Add(this.cancelBtn);
		base.Controls.Add(this.okBtn);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "AutodeskExportDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Export";
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.customTab.ResumeLayout(false);
		this.ExportOptionsGroup.ResumeLayout(false);
		this.ExportOptionsGroup.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ExportIconPictureBox).EndInit();
		base.ResumeLayout(false);
	}
}
