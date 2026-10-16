using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using edEditorDotNet.ExportDialogItems;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edConvert\\DefaultExportDialog")]
public class DefaultExportDialog : DialogBoxBase
{
	private IContainer components;

	private Button okBtn;

	private Button cancelBtn;

	private DialogCheckBox exportTraffic;

	private DialogPathLoader exportPath;

	private GroupBox groupBox1;

	private DialogCheckBox exportLevel;

	private DialogCheckBox exportPS3;

	private GroupBox platformBox;

	private DialogCheckBox exportWii;

	private DialogCheckBox exportXbox360;

	private DialogCheckBox exportPC;

	private GroupBox exportRange;

	private DialogLabel dialogLabel1;

	private DialogRadioButton exportTileRange;

	private DialogTextBox tileRange;

	private DialogNumericUpDownInt tileIdMax;

	private DialogRadioButton exportAllTiles;

	private GroupBox types;

	private DialogCheckBox exportTerrainTiles;

	private DialogCheckBox exportPowerLines;

	private DialogCheckBox exportSky;

	private GroupBox groupBox2;

	private DialogCheckBox exportAllObjects;

	private DialogCheckBox exportClouds;

	private DialogCheckBox exportPropClusters;

	private DialogCheckBox exportWater;

	private DialogCheckBox exportFloorPlans;

	private DialogCheckBox exportFences;

	private DialogCheckBox exportLevelOrigin;

	private HackLoad hackLoad1;

	private DialogCheckBox exportProps;

	public DefaultExportDialog()
	{
		InitializeComponent();
	}

	private void okBtn_Click(object sender, EventArgs e)
	{
		while (string.IsNullOrEmpty((string)exportPath.DialogValue))
		{
			DialogResult dialogResult = exportPath.ShowDialog();
			if (dialogResult == DialogResult.Cancel)
			{
				break;
			}
		}
	}

	private void label1_Click(object sender, EventArgs e)
	{
	}

	private void dialogCheckBox1_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void exportPerTile_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void DefaultExportDialog_Load(object sender, EventArgs e)
	{
	}

	private void regionSelection_Enter(object sender, EventArgs e)
	{
	}

	private void exportTileRange_CheckedChanged(object sender, EventArgs e)
	{
		tileRange.Enabled = exportTileRange.Checked;
	}

	private void tileRange_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			validateTileRange();
		}
	}

	private void tileRange_Leave(object sender, EventArgs e)
	{
		validateTileRange();
	}

	private void validateTileRange()
	{
		string text = tileRange.Text;
		string[] array = text.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			if (text2.Contains("-"))
			{
				string[] array3 = text2.Split(new char[1] { '-' }, StringSplitOptions.RemoveEmptyEntries);
				if (array3.Length == 2)
				{
					try
					{
						int num = int.Parse(array3[0]);
						int num2 = int.Parse(array3[1]);
						if (num < 0 || (decimal)num > tileIdMax.Value || num2 < 0 || (decimal)num2 > tileIdMax.Value)
						{
							MessageBox.Show("Tile id out of range", "Tile Range Error");
							tileRange.Text = "";
							break;
						}
						if (num > num2)
						{
							MessageBox.Show("Tile range is invalid", "Tile Range Error");
							tileRange.Text = "";
							break;
						}
					}
					catch (Exception)
					{
						MessageBox.Show("Tile id is not an integer", "Tile Range Error");
						tileRange.Text = "";
						break;
					}
					continue;
				}
				MessageBox.Show("Tile range is not in correct format", "Tile Range Error");
				tileRange.Text = "";
				break;
			}
			try
			{
				int num3 = int.Parse(text2);
				if (num3 < 0 || (decimal)num3 > tileIdMax.Value)
				{
					MessageBox.Show("Tile id out of range", "Tile Range Error");
					tileRange.Text = "";
					break;
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Tile id is not an integer", "Tile Range Error");
				tileRange.Text = "";
				break;
			}
		}
	}

	private void exportAll_CheckedChanged_1(object sender, EventArgs e)
	{
		exportTerrainTiles.Enabled = !exportAllObjects.Checked;
		exportPowerLines.Enabled = !exportAllObjects.Checked;
		exportSky.Enabled = !exportAllObjects.Checked;
		exportClouds.Enabled = !exportAllObjects.Checked;
		exportWater.Enabled = !exportAllObjects.Checked;
		exportProps.Enabled = !exportAllObjects.Checked;
		exportPropClusters.Enabled = !exportAllObjects.Checked;
		exportFloorPlans.Enabled = !exportAllObjects.Checked;
		exportFences.Enabled = !exportAllObjects.Checked;
	}

	private void exportWater_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void exportPath_Load(object sender, EventArgs e)
	{
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
		this.okBtn = new System.Windows.Forms.Button();
		this.cancelBtn = new System.Windows.Forms.Button();
		this.exportTraffic = new palDotNet.palDialog.DialogCheckBox();
		this.exportPath = new palDotNet.palDialog.DialogPathLoader();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.exportLevel = new palDotNet.palDialog.DialogCheckBox();
		this.exportPS3 = new palDotNet.palDialog.DialogCheckBox();
		this.platformBox = new System.Windows.Forms.GroupBox();
		this.hackLoad1 = new edEditorDotNet.ExportDialogItems.HackLoad();
		this.exportPC = new palDotNet.palDialog.DialogCheckBox();
		this.exportWii = new palDotNet.palDialog.DialogCheckBox();
		this.exportXbox360 = new palDotNet.palDialog.DialogCheckBox();
		this.exportRange = new System.Windows.Forms.GroupBox();
		this.dialogLabel1 = new palDotNet.palDialog.DialogLabel();
		this.exportTileRange = new palDotNet.palDialog.DialogRadioButton();
		this.tileRange = new palDotNet.palDialog.DialogTextBox();
		this.tileIdMax = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.exportAllTiles = new palDotNet.palDialog.DialogRadioButton();
		this.types = new System.Windows.Forms.GroupBox();
		this.exportLevelOrigin = new palDotNet.palDialog.DialogCheckBox();
		this.exportSky = new palDotNet.palDialog.DialogCheckBox();
		this.exportPowerLines = new palDotNet.palDialog.DialogCheckBox();
		this.exportTerrainTiles = new palDotNet.palDialog.DialogCheckBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.exportProps = new palDotNet.palDialog.DialogCheckBox();
		this.exportFences = new palDotNet.palDialog.DialogCheckBox();
		this.exportFloorPlans = new palDotNet.palDialog.DialogCheckBox();
		this.exportPropClusters = new palDotNet.palDialog.DialogCheckBox();
		this.exportWater = new palDotNet.palDialog.DialogCheckBox();
		this.exportClouds = new palDotNet.palDialog.DialogCheckBox();
		this.exportAllObjects = new palDotNet.palDialog.DialogCheckBox();
		this.groupBox1.SuspendLayout();
		this.platformBox.SuspendLayout();
		this.exportRange.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tileIdMax).BeginInit();
		this.types.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okBtn.Location = new System.Drawing.Point(405, 293);
		this.okBtn.Name = "okBtn";
		this.okBtn.Size = new System.Drawing.Size(75, 23);
		this.okBtn.TabIndex = 4;
		this.okBtn.Text = "OK";
		this.okBtn.UseVisualStyleBackColor = true;
		this.okBtn.Click += new System.EventHandler(okBtn_Click);
		this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelBtn.Location = new System.Drawing.Point(486, 293);
		this.cancelBtn.Name = "cancelBtn";
		this.cancelBtn.Size = new System.Drawing.Size(75, 23);
		this.cancelBtn.TabIndex = 5;
		this.cancelBtn.Text = "Cancel";
		this.cancelBtn.UseVisualStyleBackColor = true;
		this.exportTraffic.AutoSize = true;
		this.exportTraffic.Checked = true;
		this.exportTraffic.CheckState = System.Windows.Forms.CheckState.Checked;
		this.exportTraffic.Location = new System.Drawing.Point(163, 15);
		this.exportTraffic.Name = "exportTraffic";
		this.exportTraffic.Size = new System.Drawing.Size(85, 17);
		this.exportTraffic.TabIndex = 13;
		this.exportTraffic.Text = "Export traffic";
		this.exportTraffic.UseVisualStyleBackColor = true;
		this.exportPath.BackColor = System.Drawing.Color.Transparent;
		this.exportPath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eSave;
		this.exportPath.CheckFileExists = false;
		this.exportPath.Description = "Choose File Path to Save Exported Files";
		this.exportPath.Filter = "All files (*.*)|*.*";
		this.exportPath.Location = new System.Drawing.Point(6, 18);
		this.exportPath.Name = "exportPath";
		this.exportPath.Path = "";
		this.exportPath.Size = new System.Drawing.Size(537, 24);
		this.exportPath.TabIndex = 14;
		this.exportPath.Load += new System.EventHandler(exportPath_Load);
		this.groupBox1.Controls.Add(this.exportPath);
		this.groupBox1.Location = new System.Drawing.Point(10, 239);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(549, 48);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Export path";
		this.exportLevel.AutoSize = true;
		this.exportLevel.Checked = true;
		this.exportLevel.CheckState = System.Windows.Forms.CheckState.Checked;
		this.exportLevel.Location = new System.Drawing.Point(7, 15);
		this.exportLevel.Name = "exportLevel";
		this.exportLevel.Size = new System.Drawing.Size(81, 17);
		this.exportLevel.TabIndex = 19;
		this.exportLevel.Text = "Export level";
		this.exportLevel.UseVisualStyleBackColor = true;
		this.exportPS3.AutoSize = true;
		this.exportPS3.Enabled = false;
		this.exportPS3.Location = new System.Drawing.Point(9, 61);
		this.exportPS3.Name = "exportPS3";
		this.exportPS3.Size = new System.Drawing.Size(79, 17);
		this.exportPS3.TabIndex = 20;
		this.exportPS3.Text = "Export PS3";
		this.exportPS3.UseVisualStyleBackColor = true;
		this.platformBox.Controls.Add(this.hackLoad1);
		this.platformBox.Controls.Add(this.exportPC);
		this.platformBox.Controls.Add(this.exportWii);
		this.platformBox.Controls.Add(this.exportXbox360);
		this.platformBox.Controls.Add(this.exportPS3);
		this.platformBox.Location = new System.Drawing.Point(12, 12);
		this.platformBox.Name = "platformBox";
		this.platformBox.Size = new System.Drawing.Size(271, 109);
		this.platformBox.TabIndex = 21;
		this.platformBox.TabStop = false;
		this.platformBox.Text = "Platforms";
		this.hackLoad1.Enabled = false;
		this.hackLoad1.Location = new System.Drawing.Point(136, 12);
		this.hackLoad1.Name = "hackLoad1";
		this.hackLoad1.Size = new System.Drawing.Size(102, 89);
		this.hackLoad1.TabIndex = 24;
		this.hackLoad1.Visible = false;
		this.exportPC.AutoSize = true;
		this.exportPC.Checked = true;
		this.exportPC.CheckState = System.Windows.Forms.CheckState.Checked;
		this.exportPC.Location = new System.Drawing.Point(9, 15);
		this.exportPC.Name = "exportPC";
		this.exportPC.Size = new System.Drawing.Size(73, 17);
		this.exportPC.TabIndex = 23;
		this.exportPC.Text = "Export PC";
		this.exportPC.UseVisualStyleBackColor = true;
		this.exportWii.AutoSize = true;
		this.exportWii.Enabled = false;
		this.exportWii.Location = new System.Drawing.Point(9, 84);
		this.exportWii.Name = "exportWii";
		this.exportWii.Size = new System.Drawing.Size(77, 17);
		this.exportWii.TabIndex = 22;
		this.exportWii.Text = "Export Wii ";
		this.exportWii.UseVisualStyleBackColor = true;
		this.exportXbox360.AutoSize = true;
		this.exportXbox360.Enabled = false;
		this.exportXbox360.Location = new System.Drawing.Point(9, 38);
		this.exportXbox360.Name = "exportXbox360";
		this.exportXbox360.Size = new System.Drawing.Size(104, 17);
		this.exportXbox360.TabIndex = 21;
		this.exportXbox360.Text = "Export Xbox 360";
		this.exportXbox360.UseVisualStyleBackColor = true;
		this.exportRange.Controls.Add(this.dialogLabel1);
		this.exportRange.Controls.Add(this.exportTileRange);
		this.exportRange.Controls.Add(this.tileRange);
		this.exportRange.Controls.Add(this.tileIdMax);
		this.exportRange.Controls.Add(this.exportAllTiles);
		this.exportRange.Location = new System.Drawing.Point(12, 127);
		this.exportRange.Name = "exportRange";
		this.exportRange.Size = new System.Drawing.Size(271, 106);
		this.exportRange.TabIndex = 23;
		this.exportRange.TabStop = false;
		this.exportRange.Text = "Export range";
		this.dialogLabel1.Location = new System.Drawing.Point(27, 65);
		this.dialogLabel1.Name = "dialogLabel1";
		this.dialogLabel1.Size = new System.Drawing.Size(238, 30);
		this.dialogLabel1.TabIndex = 10;
		this.dialogLabel1.Text = "Type tile ranges and/or tile numbers separated by commas. For example, 1,3-5,17";
		this.exportTileRange.AutoSize = true;
		this.exportTileRange.Location = new System.Drawing.Point(6, 42);
		this.exportTileRange.Name = "exportTileRange";
		this.exportTileRange.Size = new System.Drawing.Size(47, 17);
		this.exportTileRange.TabIndex = 9;
		this.exportTileRange.Text = "Tiles";
		this.exportTileRange.UseVisualStyleBackColor = true;
		this.exportTileRange.CheckedChanged += new System.EventHandler(exportTileRange_CheckedChanged);
		this.tileRange.Enabled = false;
		this.tileRange.Location = new System.Drawing.Point(97, 42);
		this.tileRange.Name = "tileRange";
		this.tileRange.Size = new System.Drawing.Size(168, 20);
		this.tileRange.TabIndex = 8;
		this.tileRange.Leave += new System.EventHandler(tileRange_Leave);
		this.tileRange.KeyUp += new System.Windows.Forms.KeyEventHandler(tileRange_KeyUp);
		this.tileIdMax.Location = new System.Drawing.Point(145, 16);
		this.tileIdMax.Maximum = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tileIdMax.Name = "tileIdMax";
		this.tileIdMax.Size = new System.Drawing.Size(120, 20);
		this.tileIdMax.TabIndex = 7;
		this.tileIdMax.Visible = false;
		this.exportAllTiles.AutoSize = true;
		this.exportAllTiles.Checked = true;
		this.exportAllTiles.Location = new System.Drawing.Point(6, 19);
		this.exportAllTiles.Name = "exportAllTiles";
		this.exportAllTiles.Size = new System.Drawing.Size(36, 17);
		this.exportAllTiles.TabIndex = 4;
		this.exportAllTiles.TabStop = true;
		this.exportAllTiles.Text = "All";
		this.exportAllTiles.UseVisualStyleBackColor = true;
		this.types.Controls.Add(this.exportLevelOrigin);
		this.types.Controls.Add(this.exportTraffic);
		this.types.Controls.Add(this.exportLevel);
		this.types.Location = new System.Drawing.Point(289, 12);
		this.types.Name = "types";
		this.types.Size = new System.Drawing.Size(270, 66);
		this.types.TabIndex = 24;
		this.types.TabStop = false;
		this.types.Text = "Types";
		this.exportLevelOrigin.AutoSize = true;
		this.exportLevelOrigin.Checked = true;
		this.exportLevelOrigin.CheckState = System.Windows.Forms.CheckState.Checked;
		this.exportLevelOrigin.Location = new System.Drawing.Point(6, 38);
		this.exportLevelOrigin.Name = "exportLevelOrigin";
		this.exportLevelOrigin.Size = new System.Drawing.Size(109, 17);
		this.exportLevelOrigin.TabIndex = 20;
		this.exportLevelOrigin.Text = "Export level origin";
		this.exportLevelOrigin.UseVisualStyleBackColor = true;
		this.exportSky.AutoSize = true;
		this.exportSky.Enabled = false;
		this.exportSky.Location = new System.Drawing.Point(165, 39);
		this.exportSky.Name = "exportSky";
		this.exportSky.Size = new System.Drawing.Size(75, 17);
		this.exportSky.TabIndex = 20;
		this.exportSky.Text = "Export sky";
		this.exportSky.UseVisualStyleBackColor = true;
		this.exportPowerLines.AutoSize = true;
		this.exportPowerLines.Enabled = false;
		this.exportPowerLines.Location = new System.Drawing.Point(31, 59);
		this.exportPowerLines.Name = "exportPowerLines";
		this.exportPowerLines.Size = new System.Drawing.Size(112, 17);
		this.exportPowerLines.TabIndex = 21;
		this.exportPowerLines.Text = "Export power lines";
		this.exportPowerLines.UseVisualStyleBackColor = true;
		this.exportTerrainTiles.AutoSize = true;
		this.exportTerrainTiles.Enabled = false;
		this.exportTerrainTiles.Location = new System.Drawing.Point(31, 39);
		this.exportTerrainTiles.Name = "exportTerrainTiles";
		this.exportTerrainTiles.Size = new System.Drawing.Size(109, 17);
		this.exportTerrainTiles.TabIndex = 22;
		this.exportTerrainTiles.Text = "Export terrain tiles";
		this.exportTerrainTiles.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.exportProps);
		this.groupBox2.Controls.Add(this.exportFences);
		this.groupBox2.Controls.Add(this.exportFloorPlans);
		this.groupBox2.Controls.Add(this.exportPropClusters);
		this.groupBox2.Controls.Add(this.exportWater);
		this.groupBox2.Controls.Add(this.exportClouds);
		this.groupBox2.Controls.Add(this.exportAllObjects);
		this.groupBox2.Controls.Add(this.exportTerrainTiles);
		this.groupBox2.Controls.Add(this.exportSky);
		this.groupBox2.Controls.Add(this.exportPowerLines);
		this.groupBox2.Location = new System.Drawing.Point(287, 84);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(272, 149);
		this.groupBox2.TabIndex = 25;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Game objects";
		this.exportProps.AutoSize = true;
		this.exportProps.Enabled = false;
		this.exportProps.Location = new System.Drawing.Point(31, 79);
		this.exportProps.Name = "exportProps";
		this.exportProps.Size = new System.Drawing.Size(85, 17);
		this.exportProps.TabIndex = 28;
		this.exportProps.Text = "Export props";
		this.exportProps.UseVisualStyleBackColor = true;
		this.exportFences.AutoSize = true;
		this.exportFences.Enabled = false;
		this.exportFences.Location = new System.Drawing.Point(165, 99);
		this.exportFences.Name = "exportFences";
		this.exportFences.Size = new System.Drawing.Size(91, 17);
		this.exportFences.TabIndex = 27;
		this.exportFences.Text = "Export fences";
		this.exportFences.UseVisualStyleBackColor = true;
		this.exportFloorPlans.AutoSize = true;
		this.exportFloorPlans.Enabled = false;
		this.exportFloorPlans.Location = new System.Drawing.Point(31, 119);
		this.exportFloorPlans.Name = "exportFloorPlans";
		this.exportFloorPlans.Size = new System.Drawing.Size(107, 17);
		this.exportFloorPlans.TabIndex = 26;
		this.exportFloorPlans.Text = "Export floor plans";
		this.exportFloorPlans.UseVisualStyleBackColor = true;
		this.exportPropClusters.AutoSize = true;
		this.exportPropClusters.Enabled = false;
		this.exportPropClusters.Location = new System.Drawing.Point(31, 99);
		this.exportPropClusters.Name = "exportPropClusters";
		this.exportPropClusters.Size = new System.Drawing.Size(119, 17);
		this.exportPropClusters.TabIndex = 25;
		this.exportPropClusters.Text = "Export prop clusters";
		this.exportPropClusters.UseVisualStyleBackColor = true;
		this.exportWater.AutoSize = true;
		this.exportWater.Enabled = false;
		this.exportWater.Location = new System.Drawing.Point(165, 79);
		this.exportWater.Name = "exportWater";
		this.exportWater.Size = new System.Drawing.Size(85, 17);
		this.exportWater.TabIndex = 24;
		this.exportWater.Text = "Export water";
		this.exportWater.UseVisualStyleBackColor = true;
		this.exportWater.CheckedChanged += new System.EventHandler(exportWater_CheckedChanged);
		this.exportClouds.AutoSize = true;
		this.exportClouds.Enabled = false;
		this.exportClouds.Location = new System.Drawing.Point(165, 59);
		this.exportClouds.Name = "exportClouds";
		this.exportClouds.Size = new System.Drawing.Size(90, 17);
		this.exportClouds.TabIndex = 24;
		this.exportClouds.Text = "Export clouds";
		this.exportClouds.UseVisualStyleBackColor = true;
		this.exportAllObjects.AutoSize = true;
		this.exportAllObjects.Checked = true;
		this.exportAllObjects.CheckState = System.Windows.Forms.CheckState.Checked;
		this.exportAllObjects.Location = new System.Drawing.Point(8, 16);
		this.exportAllObjects.Name = "exportAllObjects";
		this.exportAllObjects.Size = new System.Drawing.Size(70, 17);
		this.exportAllObjects.TabIndex = 23;
		this.exportAllObjects.Text = "Export All";
		this.exportAllObjects.UseVisualStyleBackColor = true;
		this.exportAllObjects.CheckedChanged += new System.EventHandler(exportAll_CheckedChanged_1);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.ClientSize = new System.Drawing.Size(571, 328);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.types);
		base.Controls.Add(this.exportRange);
		base.Controls.Add(this.platformBox);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.cancelBtn);
		base.Controls.Add(this.okBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "DefaultExportDialog";
		this.Text = "PixelActive Export";
		base.Load += new System.EventHandler(DefaultExportDialog_Load);
		this.groupBox1.ResumeLayout(false);
		this.platformBox.ResumeLayout(false);
		this.platformBox.PerformLayout();
		this.exportRange.ResumeLayout(false);
		this.exportRange.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.tileIdMax).EndInit();
		this.types.ResumeLayout(false);
		this.types.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
