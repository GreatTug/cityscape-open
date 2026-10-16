using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edDialogs\\ComboBoxSelectionDialog")]
[DialogBoxCreator]
public class ComboBoxSelectionDialog : DialogBoxBase
{
	private IContainer components;

	private DialogLabel selectionLabel;

	private Button okButton;

	private Button cancelButton;

	private DialogLabel dialogName;

	private FlowLayoutPanel flowLayoutPanel1;

	private Panel panel1;

	private DialogComboBox selectionBox;

	public ComboBoxSelectionDialog()
	{
		InitializeComponent();
	}

	private void dialogName_TextChanged(object sender, EventArgs e)
	{
		Text = dialogName.Text;
	}

	private void inputLabel_Click(object sender, EventArgs e)
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
		this.selectionLabel = new palDotNet.palDialog.DialogLabel();
		this.okButton = new System.Windows.Forms.Button();
		this.cancelButton = new System.Windows.Forms.Button();
		this.dialogName = new palDotNet.palDialog.DialogLabel();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.selectionBox = new palDotNet.palDialog.DialogComboBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.flowLayoutPanel1.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.selectionLabel.AutoSize = true;
		this.selectionLabel.Location = new System.Drawing.Point(3, 0);
		this.selectionLabel.Name = "selectionLabel";
		this.selectionLabel.Size = new System.Drawing.Size(91, 13);
		this.selectionLabel.TabIndex = 2;
		this.selectionLabel.Text = "Make a selection:";
		this.selectionLabel.Click += new System.EventHandler(inputLabel_Click);
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
		this.flowLayoutPanel1.Controls.Add(this.selectionLabel);
		this.flowLayoutPanel1.Controls.Add(this.selectionBox);
		this.flowLayoutPanel1.Controls.Add(this.panel1);
		this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(331, 71);
		this.flowLayoutPanel1.TabIndex = 4;
		this.selectionBox.FormattingEnabled = true;
		this.selectionBox.Location = new System.Drawing.Point(3, 16);
		this.selectionBox.Name = "selectionBox";
		this.selectionBox.Size = new System.Drawing.Size(325, 21);
		this.selectionBox.TabIndex = 5;
		this.panel1.Controls.Add(this.okButton);
		this.panel1.Controls.Add(this.cancelButton);
		this.panel1.Location = new System.Drawing.Point(3, 43);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(325, 25);
		this.panel1.TabIndex = 2;
		base.AcceptButton = this.okButton;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoSize = true;
		base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		base.CancelButton = this.cancelButton;
		base.ClientSize = new System.Drawing.Size(352, 110);
		base.Controls.Add(this.flowLayoutPanel1);
		base.Controls.Add(this.dialogName);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "ComboBoxSelectionDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Selection Dialog";
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
