using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edDialogs\\TextInputDialog")]
public class TextInputDialog : DialogBoxBase
{
	private IContainer components;

	private DialogLabel inputLabel;

	private DialogTextBox inputTextBox;

	private Button okButton;

	private Button cancelButton;

	private DialogLabel dialogName;

	private FlowLayoutPanel flowLayoutPanel1;

	private Panel panel1;

	private DialogCheckBox clearInputOnShow;

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
		this.inputLabel = new palDotNet.palDialog.DialogLabel();
		this.inputTextBox = new palDotNet.palDialog.DialogTextBox();
		this.okButton = new System.Windows.Forms.Button();
		this.cancelButton = new System.Windows.Forms.Button();
		this.dialogName = new palDotNet.palDialog.DialogLabel();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.clearInputOnShow = new palDotNet.palDialog.DialogCheckBox();
		this.flowLayoutPanel1.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.inputLabel.AutoSize = true;
		this.inputLabel.Location = new System.Drawing.Point(3, 0);
		this.inputLabel.Name = "inputLabel";
		this.inputLabel.Size = new System.Drawing.Size(73, 13);
		this.inputLabel.TabIndex = 2;
		this.inputLabel.Text = "Enter a value:";
		this.inputTextBox.Location = new System.Drawing.Point(3, 16);
		this.inputTextBox.Name = "inputTextBox";
		this.inputTextBox.Size = new System.Drawing.Size(325, 20);
		this.inputTextBox.TabIndex = 0;
		this.inputTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(inputTextBox_KeyUp);
		this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okButton.Location = new System.Drawing.Point(169, 0);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(75, 23);
		this.okButton.TabIndex = 0;
		this.okButton.Text = "OK";
		this.okButton.UseVisualStyleBackColor = true;
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(250, 0);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(75, 23);
		this.cancelButton.TabIndex = 1;
		this.cancelButton.Text = "Cancel";
		this.cancelButton.UseVisualStyleBackColor = true;
		this.dialogName.Location = new System.Drawing.Point(0, 0);
		this.dialogName.Name = "dialogName";
		this.dialogName.Size = new System.Drawing.Size(0, 20);
		this.dialogName.TabIndex = 0;
		this.dialogName.Visible = false;
		this.dialogName.TextChanged += new System.EventHandler(dialogName_TextChanged);
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.flowLayoutPanel1.Controls.Add(this.inputLabel);
		this.flowLayoutPanel1.Controls.Add(this.inputTextBox);
		this.flowLayoutPanel1.Controls.Add(this.panel1);
		this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(331, 70);
		this.flowLayoutPanel1.TabIndex = 4;
		this.panel1.Controls.Add(this.okButton);
		this.panel1.Controls.Add(this.cancelButton);
		this.panel1.Location = new System.Drawing.Point(3, 42);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(325, 25);
		this.panel1.TabIndex = 2;
		this.clearInputOnShow.AutoSize = true;
		this.clearInputOnShow.Checked = true;
		this.clearInputOnShow.CheckState = System.Windows.Forms.CheckState.Checked;
		this.clearInputOnShow.Location = new System.Drawing.Point(231, 88);
		this.clearInputOnShow.Name = "clearInputOnShow";
		this.clearInputOnShow.Size = new System.Drawing.Size(124, 17);
		this.clearInputOnShow.TabIndex = 5;
		this.clearInputOnShow.Text = "this box is not shown";
		this.clearInputOnShow.UseVisualStyleBackColor = true;
		this.clearInputOnShow.Visible = false;
		base.AcceptButton = this.okButton;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoSize = true;
		base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		base.CancelButton = this.cancelButton;
		base.ClientSize = new System.Drawing.Size(352, 110);
		base.Controls.Add(this.clearInputOnShow);
		base.Controls.Add(this.flowLayoutPanel1);
		base.Controls.Add(this.dialogName);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "TextInputDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Input Dialog";
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public TextInputDialog()
	{
		InitializeComponent();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		if (clearInputOnShow.Checked)
		{
			inputTextBox.Clear();
		}
		inputTextBox.Focus();
	}

	private void dialogName_TextChanged(object sender, EventArgs e)
	{
		Text = dialogName.Text;
	}

	private void inputTextBox_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			base.DialogResult = DialogResult.Yes;
			Close();
		}
	}
}
