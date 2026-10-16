using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edNavigationMesh\\NewNavMeshDialog")]
public class NewNavMeshDialog : DialogBoxBase
{
	private IContainer components;

	private DialogLabel nameLabel;

	private DialogLabel exportPostfixLabel;

	private DialogLabel exportExtensionLabel;

	private GroupBox groupBox1;

	private Button okButton;

	private Button cancelButton;

	private DialogTextBox exportExtensionTextbox;

	private DialogTextBox exportPostfixTextbox;

	private DialogTextBox nameTextBox;

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
		this.nameLabel = new palDotNet.palDialog.DialogLabel();
		this.exportPostfixLabel = new palDotNet.palDialog.DialogLabel();
		this.exportExtensionLabel = new palDotNet.palDialog.DialogLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.exportExtensionTextbox = new palDotNet.palDialog.DialogTextBox();
		this.exportPostfixTextbox = new palDotNet.palDialog.DialogTextBox();
		this.nameTextBox = new palDotNet.palDialog.DialogTextBox();
		this.okButton = new System.Windows.Forms.Button();
		this.cancelButton = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.nameLabel.AutoSize = true;
		this.nameLabel.Location = new System.Drawing.Point(6, 16);
		this.nameLabel.Name = "nameLabel";
		this.nameLabel.Size = new System.Drawing.Size(38, 13);
		this.nameLabel.TabIndex = 0;
		this.nameLabel.Text = "Name:";
		this.exportPostfixLabel.AutoSize = true;
		this.exportPostfixLabel.Location = new System.Drawing.Point(6, 39);
		this.exportPostfixLabel.Name = "exportPostfixLabel";
		this.exportPostfixLabel.Size = new System.Drawing.Size(73, 13);
		this.exportPostfixLabel.TabIndex = 1;
		this.exportPostfixLabel.Text = "Export postfix:";
		this.exportExtensionLabel.AutoSize = true;
		this.exportExtensionLabel.Location = new System.Drawing.Point(6, 61);
		this.exportExtensionLabel.Name = "exportExtensionLabel";
		this.exportExtensionLabel.Size = new System.Drawing.Size(88, 13);
		this.exportExtensionLabel.TabIndex = 2;
		this.exportExtensionLabel.Text = "Export extension:";
		this.groupBox1.Controls.Add(this.exportExtensionTextbox);
		this.groupBox1.Controls.Add(this.exportPostfixTextbox);
		this.groupBox1.Controls.Add(this.nameTextBox);
		this.groupBox1.Controls.Add(this.nameLabel);
		this.groupBox1.Controls.Add(this.exportExtensionLabel);
		this.groupBox1.Controls.Add(this.exportPostfixLabel);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(354, 96);
		this.groupBox1.TabIndex = 6;
		this.groupBox1.TabStop = false;
		this.exportExtensionTextbox.Location = new System.Drawing.Point(107, 58);
		this.exportExtensionTextbox.Name = "exportExtensionTextbox";
		this.exportExtensionTextbox.Size = new System.Drawing.Size(217, 20);
		this.exportExtensionTextbox.TabIndex = 8;
		this.exportPostfixTextbox.Location = new System.Drawing.Point(107, 36);
		this.exportPostfixTextbox.Name = "exportPostfixTextbox";
		this.exportPostfixTextbox.Size = new System.Drawing.Size(217, 20);
		this.exportPostfixTextbox.TabIndex = 7;
		this.nameTextBox.Location = new System.Drawing.Point(107, 13);
		this.nameTextBox.Name = "nameTextBox";
		this.nameTextBox.Size = new System.Drawing.Size(217, 20);
		this.nameTextBox.TabIndex = 6;
		this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okButton.Location = new System.Drawing.Point(230, 115);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(67, 19);
		this.okButton.TabIndex = 9;
		this.okButton.Text = "OK";
		this.okButton.UseVisualStyleBackColor = true;
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(303, 115);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(67, 19);
		this.cancelButton.TabIndex = 10;
		this.cancelButton.Text = "Cancel";
		this.cancelButton.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.CancelButton = this.cancelButton;
		base.ClientSize = new System.Drawing.Size(382, 143);
		base.Controls.Add(this.cancelButton);
		base.Controls.Add(this.okButton);
		base.Controls.Add(this.groupBox1);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "NewNavMeshDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "New NavMesh";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	public NewNavMeshDialog()
	{
		InitializeComponent();
	}
}
