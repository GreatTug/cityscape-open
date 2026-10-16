using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\gisImagery\\gisDigitalGlobe\\DigitalGlobeTimestampDialog")]
[DialogBoxCreator]
public class DigitalGlobeTimestampDialog : DialogBoxBase
{
	private IContainer components;

	private MonthCalendar mCalendar;

	private Button mCancelButton;

	private Button mOkButton;

	private GroupBox TimestampGroup;

	private Label mTimestampInfoLabel;

	private DialogLabel HiddenUnixEpochTimestampLabel;

	private DialogTextBox DisplayTimestampTextBox;

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
		this.mCalendar = new System.Windows.Forms.MonthCalendar();
		this.mCancelButton = new System.Windows.Forms.Button();
		this.mOkButton = new System.Windows.Forms.Button();
		this.TimestampGroup = new System.Windows.Forms.GroupBox();
		this.DisplayTimestampTextBox = new palDotNet.palDialog.DialogTextBox();
		this.HiddenUnixEpochTimestampLabel = new palDotNet.palDialog.DialogLabel();
		this.mTimestampInfoLabel = new System.Windows.Forms.Label();
		this.TimestampGroup.SuspendLayout();
		base.SuspendLayout();
		this.mCalendar.Location = new System.Drawing.Point(27, 37);
		this.mCalendar.MaxSelectionCount = 1;
		this.mCalendar.Name = "mCalendar";
		this.mCalendar.TabIndex = 0;
		this.mCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(mCalendar_DateChanged);
		this.mCancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.mCancelButton.Location = new System.Drawing.Point(387, 275);
		this.mCancelButton.Name = "mCancelButton";
		this.mCancelButton.Size = new System.Drawing.Size(75, 23);
		this.mCancelButton.TabIndex = 2;
		this.mCancelButton.Text = "Cancel";
		this.mCancelButton.UseVisualStyleBackColor = true;
		this.mOkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mOkButton.Location = new System.Drawing.Point(306, 275);
		this.mOkButton.Name = "mOkButton";
		this.mOkButton.Size = new System.Drawing.Size(75, 23);
		this.mOkButton.TabIndex = 1;
		this.mOkButton.Text = "OK";
		this.mOkButton.UseVisualStyleBackColor = true;
		this.mOkButton.Click += new System.EventHandler(mOkButton_Click);
		this.TimestampGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TimestampGroup.Controls.Add(this.DisplayTimestampTextBox);
		this.TimestampGroup.Controls.Add(this.HiddenUnixEpochTimestampLabel);
		this.TimestampGroup.Controls.Add(this.mTimestampInfoLabel);
		this.TimestampGroup.Controls.Add(this.mCalendar);
		this.TimestampGroup.Location = new System.Drawing.Point(12, 12);
		this.TimestampGroup.Name = "TimestampGroup";
		this.TimestampGroup.Size = new System.Drawing.Size(450, 227);
		this.TimestampGroup.TabIndex = 0;
		this.TimestampGroup.TabStop = false;
		this.TimestampGroup.Text = "Timestamp Settings";
		this.DisplayTimestampTextBox.Location = new System.Drawing.Point(282, 71);
		this.DisplayTimestampTextBox.Name = "DisplayTimestampTextBox";
		this.DisplayTimestampTextBox.ReadOnly = true;
		this.DisplayTimestampTextBox.SaveToConfig = true;
		this.DisplayTimestampTextBox.Size = new System.Drawing.Size(140, 20);
		this.DisplayTimestampTextBox.TabIndex = 2;
		this.HiddenUnixEpochTimestampLabel.AutoSize = true;
		this.HiddenUnixEpochTimestampLabel.Location = new System.Drawing.Point(279, 99);
		this.HiddenUnixEpochTimestampLabel.Name = "HiddenUnixEpochTimestampLabel";
		this.HiddenUnixEpochTimestampLabel.SaveToConfig = false;
		this.HiddenUnixEpochTimestampLabel.Size = new System.Drawing.Size(92, 13);
		this.HiddenUnixEpochTimestampLabel.TabIndex = 3;
		this.HiddenUnixEpochTimestampLabel.Text = "HiddenTimestamp";
		this.HiddenUnixEpochTimestampLabel.Visible = false;
		this.mTimestampInfoLabel.AutoSize = true;
		this.mTimestampInfoLabel.Location = new System.Drawing.Point(279, 50);
		this.mTimestampInfoLabel.Name = "mTimestampInfoLabel";
		this.mTimestampInfoLabel.Size = new System.Drawing.Size(61, 13);
		this.mTimestampInfoLabel.TabIndex = 1;
		this.mTimestampInfoLabel.Text = "Timestamp:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.mCancelButton;
		base.ClientSize = new System.Drawing.Size(474, 310);
		base.Controls.Add(this.TimestampGroup);
		base.Controls.Add(this.mOkButton);
		base.Controls.Add(this.mCancelButton);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "DigitalGlobeTimestampDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "DigitalGlobe - Set Timestamp";
		this.TimestampGroup.ResumeLayout(false);
		this.TimestampGroup.PerformLayout();
		base.ResumeLayout(false);
	}

	public DigitalGlobeTimestampDialog()
	{
		InitializeComponent();
		DisplayTimestampTextBox.Text = "";
		HiddenUnixEpochTimestampLabel.Text = "";
		SyncOkButton();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		if (string.IsNullOrEmpty(HiddenUnixEpochTimestampLabel.Text))
		{
			SyncTimestampToCalendar();
			return;
		}
		string text = DisplayTimestampTextBox.Text;
		int month = Convert.ToInt32(text.Substring(0, 2));
		int day = Convert.ToInt32(text.Substring(3, 2));
		int year = Convert.ToInt32(text.Substring(6, 4));
		DateTime dateTime = new DateTime(year, month, day);
		mCalendar.SelectionStart = dateTime;
		mCalendar.SelectionEnd = dateTime;
	}

	private void mCalendar_DateChanged(object sender, DateRangeEventArgs e)
	{
		SyncTimestampToCalendar();
	}

	private void SyncTimestampToCalendar()
	{
		DateTime selectionStart = mCalendar.SelectionStart;
		string text = $"{selectionStart.Month:00}.{selectionStart.Day:00}.{selectionStart.Year:0000}";
		DisplayTimestampTextBox.Text = text;
		string text2 = $"{(int)(selectionStart - new DateTime(1970, 1, 1)).TotalSeconds}";
		HiddenUnixEpochTimestampLabel.Text = text2;
	}

	private void SyncOkButton()
	{
		mOkButton.Enabled = true;
		AllowSubmitDialog = true;
	}

	private void mOkButton_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
