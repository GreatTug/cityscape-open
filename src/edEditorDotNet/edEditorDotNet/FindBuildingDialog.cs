using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edBuilding\\FindBuildingDialog")]
public class FindBuildingDialog : DialogBoxBase
{
	private IContainer components;

	private GroupBox SearchOptionsGroupBox;

	private DialogTextBox inputTextBox;

	private DialogRadioButton SearchNamed;

	private DialogRadioButton SearchUnnamed;

	private Button okButton;

	private Button cancelButton;

	public FindBuildingDialog()
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

	private void FindBuildingDialog_Load(object sender, EventArgs e)
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
		this.SearchOptionsGroupBox = new System.Windows.Forms.GroupBox();
		this.SearchUnnamed = new palDotNet.palDialog.DialogRadioButton();
		this.SearchNamed = new palDotNet.palDialog.DialogRadioButton();
		this.inputTextBox = new palDotNet.palDialog.DialogTextBox();
		this.okButton = new System.Windows.Forms.Button();
		this.cancelButton = new System.Windows.Forms.Button();
		this.SearchOptionsGroupBox.SuspendLayout();
		base.SuspendLayout();
		this.SearchOptionsGroupBox.Controls.Add(this.inputTextBox);
		this.SearchOptionsGroupBox.Controls.Add(this.SearchNamed);
		this.SearchOptionsGroupBox.Controls.Add(this.SearchUnnamed);
		this.SearchOptionsGroupBox.Location = new System.Drawing.Point(12, 12);
		this.SearchOptionsGroupBox.Name = "SearchOptionsGroupBox";
		this.SearchOptionsGroupBox.Size = new System.Drawing.Size(400, 108);
		this.SearchOptionsGroupBox.TabIndex = 0;
		this.SearchOptionsGroupBox.TabStop = false;
		this.SearchOptionsGroupBox.Text = "Search Options";
		this.SearchUnnamed.AutoSize = true;
		this.SearchUnnamed.Location = new System.Drawing.Point(7, 20);
		this.SearchUnnamed.Name = "SearchUnnamed";
		this.SearchUnnamed.SaveToConfig = true;
		this.SearchUnnamed.Size = new System.Drawing.Size(165, 17);
		this.SearchUnnamed.TabIndex = 0;
		this.SearchUnnamed.Text = "Search for unnamed buildings";
		this.SearchUnnamed.UseVisualStyleBackColor = true;
		this.SearchUnnamed.CheckedChanged += new System.EventHandler(RadioButtonsChanged);
		this.SearchNamed.AutoSize = true;
		this.SearchNamed.Checked = true;
		this.SearchNamed.Location = new System.Drawing.Point(7, 44);
		this.SearchNamed.Name = "SearchNamed";
		this.SearchNamed.SaveToConfig = true;
		this.SearchNamed.Size = new System.Drawing.Size(190, 17);
		this.SearchNamed.TabIndex = 1;
		this.SearchNamed.TabStop = true;
		this.SearchNamed.Text = "Search for buildings with the name:";
		this.SearchNamed.UseVisualStyleBackColor = true;
		this.SearchNamed.CheckedChanged += new System.EventHandler(RadioButtonsChanged);
		this.inputTextBox.Location = new System.Drawing.Point(7, 68);
		this.inputTextBox.Name = "inputTextBox";
		this.inputTextBox.SaveToConfig = true;
		this.inputTextBox.Size = new System.Drawing.Size(387, 20);
		this.inputTextBox.TabIndex = 2;
		this.inputTextBox.TextChanged += new System.EventHandler(inputTextBox_TextChanged);
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okButton.Location = new System.Drawing.Point(250, 126);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(75, 23);
		this.okButton.TabIndex = 2;
		this.okButton.Text = "OK";
		this.okButton.UseVisualStyleBackColor = true;
		this.okButton.Click += new System.EventHandler(okButton_Click);
		this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(331, 126);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(75, 23);
		this.cancelButton.TabIndex = 3;
		this.cancelButton.Text = "Cancel";
		this.cancelButton.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(424, 161);
		base.Controls.Add(this.cancelButton);
		base.Controls.Add(this.okButton);
		base.Controls.Add(this.SearchOptionsGroupBox);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FindBuildingDialog";
		this.Text = "Select Buildings";
		base.Load += new System.EventHandler(FindBuildingDialog_Load);
		this.SearchOptionsGroupBox.ResumeLayout(false);
		this.SearchOptionsGroupBox.PerformLayout();
		base.ResumeLayout(false);
	}
}
