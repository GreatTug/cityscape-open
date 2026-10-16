using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edDialogs\\GoToTileDialog")]
[DialogBoxCreator]
public class GoToTileDialog : DialogBoxBase
{
	private IContainer components;

	private Button mCancelButton;

	private Button mOkButton;

	private Label InfoLabel;

	private DialogTextBox TileIdInput;

	private DialogNumericUpDownInt MinTileIdInput;

	private DialogNumericUpDownInt MaxTileIdInput;

	private bool IsTileInputValid;

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
		this.mCancelButton = new System.Windows.Forms.Button();
		this.mOkButton = new System.Windows.Forms.Button();
		this.InfoLabel = new System.Windows.Forms.Label();
		this.TileIdInput = new palDotNet.palDialog.DialogTextBox();
		this.MinTileIdInput = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.MaxTileIdInput = new palDotNet.palDialog.DialogNumericUpDownInt();
		((System.ComponentModel.ISupportInitialize)this.MinTileIdInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.MaxTileIdInput).BeginInit();
		base.SuspendLayout();
		this.mCancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.mCancelButton.Location = new System.Drawing.Point(205, 87);
		this.mCancelButton.Name = "mCancelButton";
		this.mCancelButton.Size = new System.Drawing.Size(75, 23);
		this.mCancelButton.TabIndex = 0;
		this.mCancelButton.Text = "Cancel";
		this.mCancelButton.UseVisualStyleBackColor = true;
		this.mOkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mOkButton.Location = new System.Drawing.Point(124, 87);
		this.mOkButton.Name = "mOkButton";
		this.mOkButton.Size = new System.Drawing.Size(75, 23);
		this.mOkButton.TabIndex = 1;
		this.mOkButton.Text = "OK";
		this.mOkButton.UseVisualStyleBackColor = true;
		this.mOkButton.Click += new System.EventHandler(mOkButton_Click);
		this.InfoLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.InfoLabel.Location = new System.Drawing.Point(14, 19);
		this.InfoLabel.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
		this.InfoLabel.Name = "InfoLabel";
		this.InfoLabel.Size = new System.Drawing.Size(266, 18);
		this.InfoLabel.TabIndex = 2;
		this.InfoLabel.Text = "Tile number (# - #):";
		this.TileIdInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileIdInput.Location = new System.Drawing.Point(17, 46);
		this.TileIdInput.Name = "TileIdInput";
		this.TileIdInput.SaveToConfig = true;
		this.TileIdInput.Size = new System.Drawing.Size(263, 20);
		this.TileIdInput.TabIndex = 3;
		this.TileIdInput.TextChanged += new System.EventHandler(TileInput_TextChanged);
		this.TileIdInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TileInput_KeyPress);
		this.MinTileIdInput.Location = new System.Drawing.Point(17, 89);
		this.MinTileIdInput.Maximum = new decimal(new int[4] { 1410065408, 2, 0, 0 });
		this.MinTileIdInput.Name = "MinTileIdInput";
		this.MinTileIdInput.SaveToConfig = true;
		this.MinTileIdInput.Size = new System.Drawing.Size(23, 20);
		this.MinTileIdInput.TabIndex = 4;
		this.MinTileIdInput.Visible = false;
		this.MaxTileIdInput.Location = new System.Drawing.Point(46, 89);
		this.MaxTileIdInput.Maximum = new decimal(new int[4] { 1410065408, 2, 0, 0 });
		this.MaxTileIdInput.Name = "MaxTileIdInput";
		this.MaxTileIdInput.SaveToConfig = true;
		this.MaxTileIdInput.Size = new System.Drawing.Size(23, 20);
		this.MaxTileIdInput.TabIndex = 5;
		this.MaxTileIdInput.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.mCancelButton;
		base.ClientSize = new System.Drawing.Size(294, 122);
		base.Controls.Add(this.MaxTileIdInput);
		base.Controls.Add(this.MinTileIdInput);
		base.Controls.Add(this.TileIdInput);
		base.Controls.Add(this.InfoLabel);
		base.Controls.Add(this.mOkButton);
		base.Controls.Add(this.mCancelButton);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GoToTileDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Go To Tile";
		((System.ComponentModel.ISupportInitialize)this.MinTileIdInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.MaxTileIdInput).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public GoToTileDialog()
	{
		InitializeComponent();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		WriteInfoLabel();
		TileIdInput.Text = "";
		SyncOkButton();
	}

	private void WriteInfoLabel()
	{
		int num = (int)MinTileIdInput.Value;
		int num2 = (int)MaxTileIdInput.Value;
		InfoLabel.Text = $"Tile number ({num} - {num2}):";
	}

	private void TileInput_TextChanged(object sender, EventArgs e)
	{
		ValidateTileInput();
		SyncOkButton();
	}

	private void TileInput_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.KeyChar.ToString();
		if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private void ValidateTileInput()
	{
		if (TileIdInput.Text.Length > 0)
		{
			int num = (int)MinTileIdInput.Value;
			int num2 = (int)MaxTileIdInput.Value;
			int num3 = int.Parse(TileIdInput.Text);
			if (num3 >= num && num3 <= num2)
			{
				IsTileInputValid = true;
				return;
			}
		}
		IsTileInputValid = false;
	}

	private void SyncOkButton()
	{
		if (IsTileInputValid)
		{
			AllowSubmitDialog = true;
			mOkButton.Enabled = true;
		}
		else
		{
			AllowSubmitDialog = false;
			mOkButton.Enabled = false;
		}
	}

	private void mOkButton_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
