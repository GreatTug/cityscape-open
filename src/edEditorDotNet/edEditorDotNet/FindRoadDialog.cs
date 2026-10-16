using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edRoad\\FindRoadDialog")]
[DialogBoxCreator]
public class FindRoadDialog : DialogBoxBase
{
	private IContainer components;

	private DialogTextBox inputTextBox;

	private Button okButton;

	private Button cancelButton;

	private DialogCheckBox SearchSegments;

	private DialogCheckBox SearchNodes;

	private GroupBox SearchOptionsGroupBox;

	private DialogRadioButton SearchUnnamed;

	private DialogRadioButton SearchNamed;

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
		this.inputTextBox = new palDotNet.palDialog.DialogTextBox();
		this.okButton = new System.Windows.Forms.Button();
		this.cancelButton = new System.Windows.Forms.Button();
		this.SearchSegments = new palDotNet.palDialog.DialogCheckBox();
		this.SearchNodes = new palDotNet.palDialog.DialogCheckBox();
		this.SearchOptionsGroupBox = new System.Windows.Forms.GroupBox();
		this.SearchUnnamed = new palDotNet.palDialog.DialogRadioButton();
		this.SearchNamed = new palDotNet.palDialog.DialogRadioButton();
		this.SearchOptionsGroupBox.SuspendLayout();
		base.SuspendLayout();
		this.inputTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.inputTextBox.Location = new System.Drawing.Point(6, 67);
		this.inputTextBox.Margin = new System.Windows.Forms.Padding(10);
		this.inputTextBox.Name = "inputTextBox";
		this.inputTextBox.SaveToConfig = true;
		this.inputTextBox.Size = new System.Drawing.Size(356, 20);
		this.inputTextBox.TabIndex = 2;
		this.inputTextBox.TextChanged += new System.EventHandler(inputTextBox_TextChanged);
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okButton.Location = new System.Drawing.Point(226, 174);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(75, 23);
		this.okButton.TabIndex = 1;
		this.okButton.Text = "OK";
		this.okButton.UseVisualStyleBackColor = true;
		this.okButton.Click += new System.EventHandler(okButton_Click);
		this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(307, 174);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(75, 23);
		this.cancelButton.TabIndex = 2;
		this.cancelButton.Text = "Cancel";
		this.cancelButton.UseVisualStyleBackColor = true;
		this.SearchSegments.AccessibleName = "SearchSegments";
		this.SearchSegments.AutoSize = true;
		this.SearchSegments.Checked = true;
		this.SearchSegments.CheckState = System.Windows.Forms.CheckState.Checked;
		this.SearchSegments.Location = new System.Drawing.Point(6, 100);
		this.SearchSegments.Name = "SearchSegments";
		this.SearchSegments.SaveToConfig = true;
		this.SearchSegments.Size = new System.Drawing.Size(108, 17);
		this.SearchSegments.TabIndex = 3;
		this.SearchSegments.Text = "Search segments";
		this.SearchSegments.UseVisualStyleBackColor = true;
		this.SearchSegments.CheckedChanged += new System.EventHandler(SearchBoxes_CheckedChanged);
		this.SearchNodes.AccessibleName = "SearchNodes";
		this.SearchNodes.AutoSize = true;
		this.SearchNodes.Checked = true;
		this.SearchNodes.CheckState = System.Windows.Forms.CheckState.Checked;
		this.SearchNodes.Location = new System.Drawing.Point(6, 123);
		this.SearchNodes.Name = "SearchNodes";
		this.SearchNodes.SaveToConfig = true;
		this.SearchNodes.Size = new System.Drawing.Size(92, 17);
		this.SearchNodes.TabIndex = 4;
		this.SearchNodes.Text = "Search nodes";
		this.SearchNodes.UseVisualStyleBackColor = true;
		this.SearchNodes.CheckedChanged += new System.EventHandler(SearchBoxes_CheckedChanged);
		this.SearchOptionsGroupBox.Controls.Add(this.SearchUnnamed);
		this.SearchOptionsGroupBox.Controls.Add(this.SearchNamed);
		this.SearchOptionsGroupBox.Controls.Add(this.inputTextBox);
		this.SearchOptionsGroupBox.Controls.Add(this.SearchSegments);
		this.SearchOptionsGroupBox.Controls.Add(this.SearchNodes);
		this.SearchOptionsGroupBox.Location = new System.Drawing.Point(12, 12);
		this.SearchOptionsGroupBox.Name = "SearchOptionsGroupBox";
		this.SearchOptionsGroupBox.Size = new System.Drawing.Size(370, 153);
		this.SearchOptionsGroupBox.TabIndex = 0;
		this.SearchOptionsGroupBox.TabStop = false;
		this.SearchOptionsGroupBox.Text = "Search Options";
		this.SearchUnnamed.AutoSize = true;
		this.SearchUnnamed.Location = new System.Drawing.Point(6, 19);
		this.SearchUnnamed.Name = "SearchUnnamed";
		this.SearchUnnamed.SaveToConfig = true;
		this.SearchUnnamed.Size = new System.Drawing.Size(150, 17);
		this.SearchUnnamed.TabIndex = 0;
		this.SearchUnnamed.Text = "Search for unnamed roads";
		this.SearchUnnamed.UseVisualStyleBackColor = true;
		this.SearchUnnamed.CheckedChanged += new System.EventHandler(RadioButtonsChanged);
		this.SearchNamed.AutoSize = true;
		this.SearchNamed.Checked = true;
		this.SearchNamed.Location = new System.Drawing.Point(6, 42);
		this.SearchNamed.Name = "SearchNamed";
		this.SearchNamed.SaveToConfig = true;
		this.SearchNamed.Size = new System.Drawing.Size(175, 17);
		this.SearchNamed.TabIndex = 1;
		this.SearchNamed.TabStop = true;
		this.SearchNamed.Text = "Search for roads with the name:";
		this.SearchNamed.UseVisualStyleBackColor = true;
		this.SearchNamed.CheckedChanged += new System.EventHandler(RadioButtonsChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoSize = true;
		base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		base.CancelButton = this.cancelButton;
		base.ClientSize = new System.Drawing.Size(394, 209);
		base.Controls.Add(this.SearchOptionsGroupBox);
		base.Controls.Add(this.cancelButton);
		base.Controls.Add(this.okButton);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FindRoadDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Select Roads";
		this.SearchOptionsGroupBox.ResumeLayout(false);
		this.SearchOptionsGroupBox.PerformLayout();
		base.ResumeLayout(false);
	}

	public FindRoadDialog()
	{
		InitializeComponent();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		SearchNamed.Checked = true;
		inputTextBox.Focus();
		inputTextBox.SelectAll();
		SyncOkButton();
	}

	private void inputTextBox_TextChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private void SearchBoxes_CheckedChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private void RadioButtonsChanged(object sender, EventArgs e)
	{
		inputTextBox.Enabled = SearchNamed.Checked;
		if (inputTextBox.Enabled)
		{
			inputTextBox.Focus();
			inputTextBox.SelectAll();
		}
		SyncOkButton();
	}

	private bool IsValidInput()
	{
		if (string.IsNullOrEmpty(inputTextBox.Text) && !SearchUnnamed.Checked)
		{
			return false;
		}
		if (!SearchNodes.Checked && !SearchSegments.Checked)
		{
			return false;
		}
		return true;
	}

	private void SyncOkButton()
	{
		AllowSubmitDialog = IsValidInput();
		okButton.Enabled = AllowSubmitDialog;
	}

	private void okButton_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
