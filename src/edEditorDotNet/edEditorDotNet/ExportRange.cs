using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportRange : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox groupBox1;

	private DialogLabel TileDescription;

	private DialogRadioButton exportTileRange;

	private DialogTextBox tileRange;

	private DialogNumericUpDownInt tileIdMax;

	private DialogRadioButton exportAllTiles;

	private DialogCheckBox exportTileToSingleFile;

	public RadioButton ExportAllTilesRadioButton => exportAllTiles;

	public CheckBox ExportTileToSingleFileCheckBox => exportTileToSingleFile;

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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.exportTileToSingleFile = new palDotNet.palDialog.DialogCheckBox();
		this.TileDescription = new palDotNet.palDialog.DialogLabel();
		this.exportTileRange = new palDotNet.palDialog.DialogRadioButton();
		this.tileRange = new palDotNet.palDialog.DialogTextBox();
		this.tileIdMax = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.exportAllTiles = new palDotNet.palDialog.DialogRadioButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tileIdMax).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.exportTileToSingleFile);
		this.groupBox1.Controls.Add(this.TileDescription);
		this.groupBox1.Controls.Add(this.exportTileRange);
		this.groupBox1.Controls.Add(this.tileRange);
		this.groupBox1.Controls.Add(this.tileIdMax);
		this.groupBox1.Controls.Add(this.exportAllTiles);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(554, 150);
		this.groupBox1.TabIndex = 23;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Export range";
		this.exportTileToSingleFile.AutoSize = true;
		this.exportTileToSingleFile.Location = new System.Drawing.Point(6, 102);
		this.exportTileToSingleFile.Name = "exportTileToSingleFile";
		this.exportTileToSingleFile.SaveToConfig = true;
		this.exportTileToSingleFile.Size = new System.Drawing.Size(144, 17);
		this.exportTileToSingleFile.TabIndex = 3;
		this.exportTileToSingleFile.Text = "Export tiles to a single file";
		this.exportTileToSingleFile.UseVisualStyleBackColor = true;
		this.TileDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileDescription.Location = new System.Drawing.Point(6, 65);
		this.TileDescription.Name = "TileDescription";
		this.TileDescription.SaveToConfig = false;
		this.TileDescription.Size = new System.Drawing.Size(542, 34);
		this.TileDescription.TabIndex = 10;
		this.TileDescription.Text = "Type tile ranges and/or tile numbers separated by commas. For example, 1,3-5,17";
		this.exportTileRange.AutoSize = true;
		this.exportTileRange.ForeColor = System.Drawing.SystemColors.ControlText;
		this.exportTileRange.Location = new System.Drawing.Point(6, 42);
		this.exportTileRange.Name = "exportTileRange";
		this.exportTileRange.SaveToConfig = true;
		this.exportTileRange.Size = new System.Drawing.Size(50, 17);
		this.exportTileRange.TabIndex = 1;
		this.exportTileRange.TabStop = true;
		this.exportTileRange.Text = "Tiles:";
		this.exportTileRange.UseVisualStyleBackColor = true;
		this.exportTileRange.TextChanged += new System.EventHandler(exportTileRange_CheckedChanged);
		this.tileRange.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.tileRange.Location = new System.Drawing.Point(364, 42);
		this.tileRange.Name = "tileRange";
		this.tileRange.SaveToConfig = true;
		this.tileRange.Size = new System.Drawing.Size(184, 20);
		this.tileRange.TabIndex = 2;
		this.tileRange.Leave += new System.EventHandler(tileRange_Leave);
		this.tileRange.KeyUp += new System.Windows.Forms.KeyEventHandler(tileRange_KeyUp);
		this.tileIdMax.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.tileIdMax.Location = new System.Drawing.Point(428, 16);
		this.tileIdMax.Maximum = new decimal(new int[4] { 999999, 0, 0, 0 });
		this.tileIdMax.Name = "tileIdMax";
		this.tileIdMax.SaveToConfig = true;
		this.tileIdMax.Size = new System.Drawing.Size(120, 20);
		this.tileIdMax.TabIndex = 7;
		this.tileIdMax.Visible = false;
		this.exportAllTiles.AutoSize = true;
		this.exportAllTiles.Checked = true;
		this.exportAllTiles.Location = new System.Drawing.Point(6, 19);
		this.exportAllTiles.Name = "exportAllTiles";
		this.exportAllTiles.SaveToConfig = true;
		this.exportAllTiles.Size = new System.Drawing.Size(36, 17);
		this.exportAllTiles.TabIndex = 0;
		this.exportAllTiles.TabStop = true;
		this.exportAllTiles.Text = "All";
		this.exportAllTiles.UseVisualStyleBackColor = true;
		this.exportAllTiles.CheckedChanged += new System.EventHandler(exportAllTiles_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "ExportRange";
		base.Size = new System.Drawing.Size(554, 150);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.tileIdMax).EndInit();
		base.ResumeLayout(false);
	}

	public ExportRange()
	{
		InitializeComponent();
		tileRange.Enabled = exportTileRange.Checked;
	}

	private void tileRange_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			validateTileRange();
		}
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

	private void tileRange_Click(object sender, EventArgs e)
	{
		exportTileRange.Checked = true;
	}

	private void tileRange_Leave(object sender, EventArgs e)
	{
		validateTileRange();
	}

	private void exportTileRange_CheckedChanged(object sender, EventArgs e)
	{
		tileRange.Enabled = exportTileRange.Checked;
	}

	private void exportAllTiles_CheckedChanged(object sender, EventArgs e)
	{
		tileRange.Enabled = !exportAllTiles.Checked;
	}
}
