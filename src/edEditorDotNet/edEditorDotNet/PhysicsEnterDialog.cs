using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edPhysics\\PhysicsEnterDialog")]
public class PhysicsEnterDialog : DialogBoxBase
{
	private IContainer components;

	private DialogLabel TileDescription;

	private DialogRadioButton exportTileRange;

	private DialogTextBox tileRange;

	private DialogNumericUpDownInt tileIdMax;

	private DialogRadioButton exportAllTiles;

	private Button okButton;

	private Button cancelButton;

	private GroupBox groupBox1;

	private DialogRadioButton exportCurrentTiles;

	private Panel InfoPanel;

	private DialogLabel InfoLabel;

	private PictureBox InfoIconPictureBox;

	private Button mViewControlsButton;

	private Label AllTilesLabel;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(edEditorDotNet.PhysicsEnterDialog));
		this.TileDescription = new palDotNet.palDialog.DialogLabel();
		this.exportTileRange = new palDotNet.palDialog.DialogRadioButton();
		this.tileRange = new palDotNet.palDialog.DialogTextBox();
		this.tileIdMax = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.exportAllTiles = new palDotNet.palDialog.DialogRadioButton();
		this.okButton = new System.Windows.Forms.Button();
		this.cancelButton = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.AllTilesLabel = new System.Windows.Forms.Label();
		this.exportCurrentTiles = new palDotNet.palDialog.DialogRadioButton();
		this.InfoPanel = new System.Windows.Forms.Panel();
		this.InfoIconPictureBox = new System.Windows.Forms.PictureBox();
		this.InfoLabel = new palDotNet.palDialog.DialogLabel();
		this.mViewControlsButton = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.tileIdMax).BeginInit();
		this.groupBox1.SuspendLayout();
		this.InfoPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.InfoIconPictureBox).BeginInit();
		base.SuspendLayout();
		this.TileDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileDescription.Location = new System.Drawing.Point(17, 100);
		this.TileDescription.Name = "TileDescription";
		this.TileDescription.SaveToConfig = false;
		this.TileDescription.Size = new System.Drawing.Size(368, 35);
		this.TileDescription.TabIndex = 5;
		this.TileDescription.Text = "Enter tile ranges and/or tile numbers separated by commas.\r\nFor example: 1,3-5,17";
		this.TileDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.exportTileRange.AutoSize = true;
		this.exportTileRange.Location = new System.Drawing.Point(20, 74);
		this.exportTileRange.Name = "exportTileRange";
		this.exportTileRange.SaveToConfig = true;
		this.exportTileRange.Size = new System.Drawing.Size(50, 17);
		this.exportTileRange.TabIndex = 3;
		this.exportTileRange.Text = "Tiles:";
		this.exportTileRange.UseVisualStyleBackColor = true;
		this.exportTileRange.CheckedChanged += new System.EventHandler(exportTileRange_CheckedChanged);
		this.tileRange.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tileRange.Location = new System.Drawing.Point(116, 73);
		this.tileRange.Name = "tileRange";
		this.tileRange.SaveToConfig = true;
		this.tileRange.Size = new System.Drawing.Size(269, 20);
		this.tileRange.TabIndex = 4;
		this.tileRange.Click += new System.EventHandler(tileRange_Click);
		this.tileRange.Leave += new System.EventHandler(tileRange_Leave);
		this.tileRange.KeyUp += new System.Windows.Forms.KeyEventHandler(tileRange_KeyUp);
		this.tileRange.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tileRange_KeyPress);
		this.tileIdMax.Location = new System.Drawing.Point(361, 28);
		this.tileIdMax.Maximum = new decimal(new int[4] { 999999, 0, 0, 0 });
		this.tileIdMax.Name = "tileIdMax";
		this.tileIdMax.SaveToConfig = true;
		this.tileIdMax.Size = new System.Drawing.Size(23, 20);
		this.tileIdMax.TabIndex = 1;
		this.tileIdMax.TabStop = false;
		this.tileIdMax.Visible = false;
		this.exportAllTiles.AutoSize = true;
		this.exportAllTiles.Checked = true;
		this.exportAllTiles.Location = new System.Drawing.Point(20, 28);
		this.exportAllTiles.Name = "exportAllTiles";
		this.exportAllTiles.SaveToConfig = true;
		this.exportAllTiles.Size = new System.Drawing.Size(36, 17);
		this.exportAllTiles.TabIndex = 0;
		this.exportAllTiles.TabStop = true;
		this.exportAllTiles.Text = "All";
		this.exportAllTiles.UseVisualStyleBackColor = true;
		this.exportAllTiles.CheckedChanged += new System.EventHandler(exportAllTiles_CheckedChanged);
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okButton.Location = new System.Drawing.Point(266, 228);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(75, 23);
		this.okButton.TabIndex = 2;
		this.okButton.Text = "OK";
		this.okButton.UseVisualStyleBackColor = true;
		this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(347, 228);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(75, 23);
		this.cancelButton.TabIndex = 3;
		this.cancelButton.Text = "Cancel";
		this.cancelButton.UseVisualStyleBackColor = true;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.AllTilesLabel);
		this.groupBox1.Controls.Add(this.exportCurrentTiles);
		this.groupBox1.Controls.Add(this.exportTileRange);
		this.groupBox1.Controls.Add(this.exportAllTiles);
		this.groupBox1.Controls.Add(this.tileIdMax);
		this.groupBox1.Controls.Add(this.TileDescription);
		this.groupBox1.Controls.Add(this.tileRange);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(410, 150);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Tile selection";
		this.AllTilesLabel.AutoSize = true;
		this.AllTilesLabel.Location = new System.Drawing.Point(113, 30);
		this.AllTilesLabel.Name = "AllTilesLabel";
		this.AllTilesLabel.Size = new System.Drawing.Size(55, 13);
		this.AllTilesLabel.TabIndex = 6;
		this.AllTilesLabel.Text = "Tiles # - #";
		this.exportCurrentTiles.AutoSize = true;
		this.exportCurrentTiles.Location = new System.Drawing.Point(20, 51);
		this.exportCurrentTiles.Name = "exportCurrentTiles";
		this.exportCurrentTiles.SaveToConfig = true;
		this.exportCurrentTiles.Size = new System.Drawing.Size(59, 17);
		this.exportCurrentTiles.TabIndex = 2;
		this.exportCurrentTiles.Text = "Current";
		this.exportCurrentTiles.UseVisualStyleBackColor = true;
		this.exportCurrentTiles.CheckedChanged += new System.EventHandler(exportCurrentTiles_CheckedChanged);
		this.InfoPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.InfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.InfoPanel.Controls.Add(this.InfoIconPictureBox);
		this.InfoPanel.Controls.Add(this.InfoLabel);
		this.InfoPanel.Location = new System.Drawing.Point(32, 171);
		this.InfoPanel.Name = "InfoPanel";
		this.InfoPanel.Size = new System.Drawing.Size(368, 32);
		this.InfoPanel.TabIndex = 4;
		this.InfoIconPictureBox.Image = (System.Drawing.Image)resources.GetObject("InfoIconPictureBox.Image");
		this.InfoIconPictureBox.Location = new System.Drawing.Point(3, 3);
		this.InfoIconPictureBox.Name = "InfoIconPictureBox";
		this.InfoIconPictureBox.Size = new System.Drawing.Size(24, 24);
		this.InfoIconPictureBox.TabIndex = 1;
		this.InfoIconPictureBox.TabStop = false;
		this.InfoLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.InfoLabel.Location = new System.Drawing.Point(35, 0);
		this.InfoLabel.Name = "InfoLabel";
		this.InfoLabel.SaveToConfig = false;
		this.InfoLabel.Size = new System.Drawing.Size(328, 30);
		this.InfoLabel.TabIndex = 0;
		this.InfoLabel.Text = "Please plug in any game controllers at this time.";
		this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.mViewControlsButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.mViewControlsButton.Image = (System.Drawing.Image)resources.GetObject("mViewControlsButton.Image");
		this.mViewControlsButton.Location = new System.Drawing.Point(12, 227);
		this.mViewControlsButton.Name = "mViewControlsButton";
		this.mViewControlsButton.Size = new System.Drawing.Size(122, 24);
		this.mViewControlsButton.TabIndex = 5;
		this.mViewControlsButton.Text = "View Controls";
		this.mViewControlsButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.mViewControlsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.mViewControlsButton.UseVisualStyleBackColor = true;
		this.mViewControlsButton.Click += new System.EventHandler(mViewControlsButton_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoSize = true;
		base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		base.CancelButton = this.cancelButton;
		base.ClientSize = new System.Drawing.Size(434, 263);
		base.Controls.Add(this.mViewControlsButton);
		base.Controls.Add(this.InfoPanel);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.okButton);
		base.Controls.Add(this.cancelButton);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "PhysicsEnterDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Physics Simulation";
		((System.ComponentModel.ISupportInitialize)this.tileIdMax).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.InfoPanel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.InfoIconPictureBox).EndInit();
		base.ResumeLayout(false);
	}

	public PhysicsEnterDialog()
	{
		InitializeComponent();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		int num = 0;
		int num2 = (int)tileIdMax.Value;
		int num3 = num2 - num;
		if (num3 > 1)
		{
			exportAllTiles.Enabled = true;
			WriteAllTilesLabel();
			exportCurrentTiles.Enabled = true;
			exportTileRange.Enabled = true;
			tileRange.Enabled = exportTileRange.Checked;
			if (exportTileRange.Checked)
			{
				ValidateTileRange(silent: true);
			}
		}
		else
		{
			exportAllTiles.Enabled = true;
			exportAllTiles.Checked = true;
			ClearAllTilesLabel();
			exportCurrentTiles.Enabled = false;
			exportTileRange.Enabled = false;
			tileRange.Enabled = false;
			tileRange.Text = "";
			TileDescription.Enabled = false;
		}
	}

	private void WriteAllTilesLabel()
	{
		int num = 0;
		int num2 = (int)tileIdMax.Value;
		int num3 = num2 - num;
		if (num3 > 1)
		{
			AllTilesLabel.Text = $"Tiles {num} - {num2}";
		}
		else
		{
			AllTilesLabel.Text = $"Tile {num}";
		}
	}

	private void ClearAllTilesLabel()
	{
		AllTilesLabel.Text = "";
	}

	private void exportAllTiles_CheckedChanged(object sender, EventArgs e)
	{
		tileRange.Enabled = exportTileRange.Checked;
		okButton.Enabled = true;
	}

	private void exportCurrentTiles_CheckedChanged(object sender, EventArgs e)
	{
		tileRange.Enabled = exportTileRange.Checked;
		okButton.Enabled = true;
	}

	private void exportTileRange_CheckedChanged(object sender, EventArgs e)
	{
		tileRange.Enabled = exportTileRange.Checked;
		ValidateTileRange(silent: true);
	}

	private void ValidateTileRange(bool silent)
	{
		string text = tileRange.Text;
		okButton.Enabled = false;
		if (text.Length == 0)
		{
			return;
		}
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
							if (!silent)
							{
								MessageBox.Show("Tile id out of range", "Tile Range Error");
							}
							return;
						}
						if (num > num2)
						{
							if (!silent)
							{
								MessageBox.Show("Tile range is invalid", "Tile Range Error");
							}
							return;
						}
					}
					catch (Exception)
					{
						if (!silent)
						{
							MessageBox.Show("Tile id is not an integer", "Tile Range Error");
						}
						return;
					}
					continue;
				}
				if (!silent)
				{
					MessageBox.Show("Tile range is not in correct format", "Tile Range Error");
				}
				return;
			}
			try
			{
				int num3 = int.Parse(text2);
				if (num3 < 0 || (decimal)num3 > tileIdMax.Value)
				{
					if (!silent)
					{
						MessageBox.Show("Tile id out of range", "Tile Range Error");
					}
					return;
				}
			}
			catch (Exception)
			{
				if (!silent)
				{
					MessageBox.Show("Tile id is not an integer", "Tile Range Error");
				}
				return;
			}
		}
		okButton.Enabled = true;
	}

	private void tileRange_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.KeyChar.ToString();
		if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ',' && e.KeyChar != ' ' && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private void tileRange_KeyUp(object sender, KeyEventArgs e)
	{
		bool flag = e.KeyCode == Keys.Return;
		ValidateTileRange(!flag);
	}

	private void tileRange_Click(object sender, EventArgs e)
	{
		exportTileRange.Checked = true;
	}

	private void tileRange_Leave(object sender, EventArgs e)
	{
		ValidateTileRange(silent: true);
	}

	private void mViewControlsButton_Click(object sender, EventArgs e)
	{
		PhysicsControllerDialog physicsControllerDialog = new PhysicsControllerDialog();
		physicsControllerDialog.ShowDialog();
	}
}
