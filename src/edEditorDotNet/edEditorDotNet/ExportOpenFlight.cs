using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportOpenFlight : ExportCustomBase
{
	private IContainer components;

	private DialogCheckBox exportOpenFlightFlipTexCoords;

	private GroupBox groupBox2;

	private DialogLabel openFlightDialogLabel1;

	private DialogPathLoader exportOpenFlightPaletteFileList;

	private GroupBox groupBox1;

	private DialogNumericUpDownInt exportOpenFlightDiffuse2EffectNum;

	private Label label1;

	private Label label6;

	private DialogNumericUpDownInt exportOpenFlightDetail4EffectNum;

	private Label label4;

	private DialogNumericUpDownInt exportOpenFlightDetail3EffectNum;

	private Label label5;

	private Label label2;

	private Label label3;

	private DialogNumericUpDownInt exportOpenFlightDiffuse4EffectNum;

	private DialogNumericUpDownInt exportOpenFlightDetail2EffectNum;

	private DialogNumericUpDownInt exportOpenFlightDiffuse3EffectNum;

	private DialogCheckGroupBox exportOpenFlightTerrainBlendingDetailMaps;

	private GroupBox OpenFlightExportSupportDetailBlendMaps;

	private DialogComboBox exportOpenFlightFileFormatVersion;

	private Label label7;

	private DialogCheckBox exportOpenFlightUseMeshes;

	public override string ExporterTypeName => "OpenFlight";

	public ExportOpenFlight()
	{
		InitializeComponent();
		exportOpenFlightFileFormatVersion.SelectedIndex = exportOpenFlightFileFormatVersion.Items.Count - 1;
	}

	private void exportOpenFlightFileFormatVersion_SelectedIndexChanged(object sender, EventArgs e)
	{
		exportOpenFlightUseMeshes.Enabled = "15.6" != (string)exportOpenFlightFileFormatVersion.SelectedItem;
		if (!exportOpenFlightUseMeshes.Enabled)
		{
			exportOpenFlightUseMeshes.Checked = false;
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
		this.exportOpenFlightFlipTexCoords = new palDotNet.palDialog.DialogCheckBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.exportOpenFlightFileFormatVersion = new palDotNet.palDialog.DialogComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.openFlightDialogLabel1 = new palDotNet.palDialog.DialogLabel();
		this.exportOpenFlightPaletteFileList = new palDotNet.palDialog.DialogPathLoader();
		this.exportOpenFlightUseMeshes = new palDotNet.palDialog.DialogCheckBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.OpenFlightExportSupportDetailBlendMaps = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.exportOpenFlightDiffuse2EffectNum = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.label5 = new System.Windows.Forms.Label();
		this.exportOpenFlightDiffuse4EffectNum = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.label3 = new System.Windows.Forms.Label();
		this.exportOpenFlightDiffuse3EffectNum = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.exportOpenFlightTerrainBlendingDetailMaps = new palDotNet.palDialog.DialogCheckGroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.exportOpenFlightDetail4EffectNum = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.exportOpenFlightDetail2EffectNum = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.label6 = new System.Windows.Forms.Label();
		this.exportOpenFlightDetail3EffectNum = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.OpenFlightExportSupportDetailBlendMaps.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDiffuse2EffectNum).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDiffuse4EffectNum).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDiffuse3EffectNum).BeginInit();
		this.exportOpenFlightTerrainBlendingDetailMaps.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDetail4EffectNum).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDetail2EffectNum).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDetail3EffectNum).BeginInit();
		base.SuspendLayout();
		this.exportOpenFlightFlipTexCoords.AutoSize = true;
		this.exportOpenFlightFlipTexCoords.Location = new System.Drawing.Point(9, 44);
		this.exportOpenFlightFlipTexCoords.Name = "exportOpenFlightFlipTexCoords";
		this.exportOpenFlightFlipTexCoords.SaveToConfig = true;
		this.exportOpenFlightFlipTexCoords.Size = new System.Drawing.Size(156, 17);
		this.exportOpenFlightFlipTexCoords.TabIndex = 3;
		this.exportOpenFlightFlipTexCoords.Text = "Flip texture coordinates in Y";
		this.exportOpenFlightFlipTexCoords.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.exportOpenFlightFileFormatVersion);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.openFlightDialogLabel1);
		this.groupBox2.Controls.Add(this.exportOpenFlightPaletteFileList);
		this.groupBox2.Controls.Add(this.exportOpenFlightUseMeshes);
		this.groupBox2.Controls.Add(this.exportOpenFlightFlipTexCoords);
		this.groupBox2.Location = new System.Drawing.Point(3, 3);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(496, 132);
		this.groupBox2.TabIndex = 30;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "OpenFlight options";
		this.exportOpenFlightFileFormatVersion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.exportOpenFlightFileFormatVersion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.exportOpenFlightFileFormatVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.exportOpenFlightFileFormatVersion.FormattingEnabled = true;
		this.exportOpenFlightFileFormatVersion.Items.AddRange(new object[7] { "15.6", "15.7", "15.8", "16.0", "16.1", "16.2", "16.3" });
		this.exportOpenFlightFileFormatVersion.Location = new System.Drawing.Point(167, 13);
		this.exportOpenFlightFileFormatVersion.Name = "exportOpenFlightFileFormatVersion";
		this.exportOpenFlightFileFormatVersion.SaveToConfig = true;
		this.exportOpenFlightFileFormatVersion.Size = new System.Drawing.Size(72, 21);
		this.exportOpenFlightFileFormatVersion.TabIndex = 7;
		this.exportOpenFlightFileFormatVersion.SelectedIndexChanged += new System.EventHandler(exportOpenFlightFileFormatVersion_SelectedIndexChanged);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(6, 16);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(146, 13);
		this.label7.TabIndex = 6;
		this.label7.Text = "OpenFlight file format version:";
		this.openFlightDialogLabel1.AutoSize = true;
		this.openFlightDialogLabel1.Location = new System.Drawing.Point(3, 88);
		this.openFlightDialogLabel1.Name = "openFlightDialogLabel1";
		this.openFlightDialogLabel1.SaveToConfig = false;
		this.openFlightDialogLabel1.Size = new System.Drawing.Size(74, 13);
		this.openFlightDialogLabel1.TabIndex = 5;
		this.openFlightDialogLabel1.Text = "Palette file list:";
		this.exportOpenFlightPaletteFileList.BackColor = System.Drawing.Color.Transparent;
		this.exportOpenFlightPaletteFileList.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.exportOpenFlightPaletteFileList.CheckFileExists = false;
		this.exportOpenFlightPaletteFileList.Description = null;
		this.exportOpenFlightPaletteFileList.Filter = "Xml (*.xml)|*.xml";
		this.exportOpenFlightPaletteFileList.Location = new System.Drawing.Point(3, 104);
		this.exportOpenFlightPaletteFileList.Name = "exportOpenFlightPaletteFileList";
		this.exportOpenFlightPaletteFileList.Path = "";
		this.exportOpenFlightPaletteFileList.SaveToConfig = true;
		this.exportOpenFlightPaletteFileList.Size = new System.Drawing.Size(480, 24);
		this.exportOpenFlightPaletteFileList.TabIndex = 4;
		this.exportOpenFlightUseMeshes.AutoSize = true;
		this.exportOpenFlightUseMeshes.Location = new System.Drawing.Point(9, 67);
		this.exportOpenFlightUseMeshes.Name = "exportOpenFlightUseMeshes";
		this.exportOpenFlightUseMeshes.SaveToConfig = true;
		this.exportOpenFlightUseMeshes.Size = new System.Drawing.Size(84, 17);
		this.exportOpenFlightUseMeshes.TabIndex = 3;
		this.exportOpenFlightUseMeshes.Text = "Use meshes";
		this.exportOpenFlightUseMeshes.UseVisualStyleBackColor = true;
		this.exportOpenFlightUseMeshes.Visible = false;
		this.groupBox1.Controls.Add(this.OpenFlightExportSupportDetailBlendMaps);
		this.groupBox1.Controls.Add(this.exportOpenFlightTerrainBlendingDetailMaps);
		this.groupBox1.Location = new System.Drawing.Point(3, 141);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(496, 244);
		this.groupBox1.TabIndex = 31;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Terrain blending";
		this.OpenFlightExportSupportDetailBlendMaps.Controls.Add(this.label1);
		this.OpenFlightExportSupportDetailBlendMaps.Controls.Add(this.exportOpenFlightDiffuse2EffectNum);
		this.OpenFlightExportSupportDetailBlendMaps.Controls.Add(this.label5);
		this.OpenFlightExportSupportDetailBlendMaps.Controls.Add(this.exportOpenFlightDiffuse4EffectNum);
		this.OpenFlightExportSupportDetailBlendMaps.Controls.Add(this.label3);
		this.OpenFlightExportSupportDetailBlendMaps.Controls.Add(this.exportOpenFlightDiffuse3EffectNum);
		this.OpenFlightExportSupportDetailBlendMaps.Location = new System.Drawing.Point(6, 19);
		this.OpenFlightExportSupportDetailBlendMaps.Name = "OpenFlightExportSupportDetailBlendMaps";
		this.OpenFlightExportSupportDetailBlendMaps.Size = new System.Drawing.Size(484, 105);
		this.OpenFlightExportSupportDetailBlendMaps.TabIndex = 3;
		this.OpenFlightExportSupportDetailBlendMaps.TabStop = false;
		this.OpenFlightExportSupportDetailBlendMaps.Text = "Diffuse texture maps";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(92, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Diffuse 2 effect #:";
		this.exportOpenFlightDiffuse2EffectNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.exportOpenFlightDiffuse2EffectNum.Location = new System.Drawing.Point(375, 17);
		this.exportOpenFlightDiffuse2EffectNum.Maximum = new decimal(new int[4] { 65535, 0, 0, 0 });
		this.exportOpenFlightDiffuse2EffectNum.Minimum = new decimal(new int[4] { 101, 0, 0, 0 });
		this.exportOpenFlightDiffuse2EffectNum.Name = "exportOpenFlightDiffuse2EffectNum";
		this.exportOpenFlightDiffuse2EffectNum.SaveToConfig = true;
		this.exportOpenFlightDiffuse2EffectNum.Size = new System.Drawing.Size(102, 20);
		this.exportOpenFlightDiffuse2EffectNum.TabIndex = 0;
		this.exportOpenFlightDiffuse2EffectNum.Value = new decimal(new int[4] { 101, 0, 0, 0 });
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 70);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(92, 13);
		this.label5.TabIndex = 1;
		this.label5.Text = "Diffuse 4 effect #:";
		this.exportOpenFlightDiffuse4EffectNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.exportOpenFlightDiffuse4EffectNum.Location = new System.Drawing.Point(375, 68);
		this.exportOpenFlightDiffuse4EffectNum.Maximum = new decimal(new int[4] { 65535, 0, 0, 0 });
		this.exportOpenFlightDiffuse4EffectNum.Minimum = new decimal(new int[4] { 101, 0, 0, 0 });
		this.exportOpenFlightDiffuse4EffectNum.Name = "exportOpenFlightDiffuse4EffectNum";
		this.exportOpenFlightDiffuse4EffectNum.SaveToConfig = true;
		this.exportOpenFlightDiffuse4EffectNum.Size = new System.Drawing.Size(102, 20);
		this.exportOpenFlightDiffuse4EffectNum.TabIndex = 0;
		this.exportOpenFlightDiffuse4EffectNum.Value = new decimal(new int[4] { 101, 0, 0, 0 });
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 45);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(92, 13);
		this.label3.TabIndex = 1;
		this.label3.Text = "Diffuse 3 effect #:";
		this.exportOpenFlightDiffuse3EffectNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.exportOpenFlightDiffuse3EffectNum.Location = new System.Drawing.Point(375, 43);
		this.exportOpenFlightDiffuse3EffectNum.Maximum = new decimal(new int[4] { 65535, 0, 0, 0 });
		this.exportOpenFlightDiffuse3EffectNum.Minimum = new decimal(new int[4] { 101, 0, 0, 0 });
		this.exportOpenFlightDiffuse3EffectNum.Name = "exportOpenFlightDiffuse3EffectNum";
		this.exportOpenFlightDiffuse3EffectNum.SaveToConfig = true;
		this.exportOpenFlightDiffuse3EffectNum.Size = new System.Drawing.Size(102, 20);
		this.exportOpenFlightDiffuse3EffectNum.TabIndex = 0;
		this.exportOpenFlightDiffuse3EffectNum.Value = new decimal(new int[4] { 101, 0, 0, 0 });
		this.exportOpenFlightTerrainBlendingDetailMaps.Controls.Add(this.label2);
		this.exportOpenFlightTerrainBlendingDetailMaps.Controls.Add(this.exportOpenFlightDetail4EffectNum);
		this.exportOpenFlightTerrainBlendingDetailMaps.Controls.Add(this.exportOpenFlightDetail2EffectNum);
		this.exportOpenFlightTerrainBlendingDetailMaps.Controls.Add(this.label6);
		this.exportOpenFlightTerrainBlendingDetailMaps.Controls.Add(this.exportOpenFlightDetail3EffectNum);
		this.exportOpenFlightTerrainBlendingDetailMaps.Controls.Add(this.label4);
		this.exportOpenFlightTerrainBlendingDetailMaps.Location = new System.Drawing.Point(6, 130);
		this.exportOpenFlightTerrainBlendingDetailMaps.Name = "exportOpenFlightTerrainBlendingDetailMaps";
		this.exportOpenFlightTerrainBlendingDetailMaps.SaveToConfig = true;
		this.exportOpenFlightTerrainBlendingDetailMaps.Size = new System.Drawing.Size(484, 108);
		this.exportOpenFlightTerrainBlendingDetailMaps.TabIndex = 2;
		this.exportOpenFlightTerrainBlendingDetailMaps.TabStop = false;
		this.exportOpenFlightTerrainBlendingDetailMaps.Text = "Detail texture maps";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(4, 22);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(86, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Detail 2 effect #:";
		this.exportOpenFlightDetail4EffectNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.exportOpenFlightDetail4EffectNum.Location = new System.Drawing.Point(375, 72);
		this.exportOpenFlightDetail4EffectNum.Maximum = new decimal(new int[4] { 65535, 0, 0, 0 });
		this.exportOpenFlightDetail4EffectNum.Minimum = new decimal(new int[4] { 101, 0, 0, 0 });
		this.exportOpenFlightDetail4EffectNum.Name = "exportOpenFlightDetail4EffectNum";
		this.exportOpenFlightDetail4EffectNum.SaveToConfig = true;
		this.exportOpenFlightDetail4EffectNum.Size = new System.Drawing.Size(102, 20);
		this.exportOpenFlightDetail4EffectNum.TabIndex = 0;
		this.exportOpenFlightDetail4EffectNum.Value = new decimal(new int[4] { 101, 0, 0, 0 });
		this.exportOpenFlightDetail2EffectNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.exportOpenFlightDetail2EffectNum.Location = new System.Drawing.Point(375, 22);
		this.exportOpenFlightDetail2EffectNum.Maximum = new decimal(new int[4] { 65535, 0, 0, 0 });
		this.exportOpenFlightDetail2EffectNum.Minimum = new decimal(new int[4] { 101, 0, 0, 0 });
		this.exportOpenFlightDetail2EffectNum.Name = "exportOpenFlightDetail2EffectNum";
		this.exportOpenFlightDetail2EffectNum.SaveToConfig = true;
		this.exportOpenFlightDetail2EffectNum.Size = new System.Drawing.Size(102, 20);
		this.exportOpenFlightDetail2EffectNum.TabIndex = 0;
		this.exportOpenFlightDetail2EffectNum.Value = new decimal(new int[4] { 101, 0, 0, 0 });
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(4, 72);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(86, 13);
		this.label6.TabIndex = 1;
		this.label6.Text = "Detail 4 effect #:";
		this.exportOpenFlightDetail3EffectNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.exportOpenFlightDetail3EffectNum.Location = new System.Drawing.Point(375, 47);
		this.exportOpenFlightDetail3EffectNum.Maximum = new decimal(new int[4] { 65535, 0, 0, 0 });
		this.exportOpenFlightDetail3EffectNum.Minimum = new decimal(new int[4] { 101, 0, 0, 0 });
		this.exportOpenFlightDetail3EffectNum.Name = "exportOpenFlightDetail3EffectNum";
		this.exportOpenFlightDetail3EffectNum.SaveToConfig = true;
		this.exportOpenFlightDetail3EffectNum.Size = new System.Drawing.Size(102, 20);
		this.exportOpenFlightDetail3EffectNum.TabIndex = 0;
		this.exportOpenFlightDetail3EffectNum.Value = new decimal(new int[4] { 101, 0, 0, 0 });
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(4, 47);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(86, 13);
		this.label4.TabIndex = 1;
		this.label4.Text = "Detail 3 effect #:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Name = "ExportOpenFlight";
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.OpenFlightExportSupportDetailBlendMaps.ResumeLayout(false);
		this.OpenFlightExportSupportDetailBlendMaps.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDiffuse2EffectNum).EndInit();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDiffuse4EffectNum).EndInit();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDiffuse3EffectNum).EndInit();
		this.exportOpenFlightTerrainBlendingDetailMaps.ResumeLayout(false);
		this.exportOpenFlightTerrainBlendingDetailMaps.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDetail4EffectNum).EndInit();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDetail2EffectNum).EndInit();
		((System.ComponentModel.ISupportInitialize)this.exportOpenFlightDetail3EffectNum).EndInit();
		base.ResumeLayout(false);
	}
}
