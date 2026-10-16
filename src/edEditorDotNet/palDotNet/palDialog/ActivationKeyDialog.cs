using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using edEditorDotNet.ActivationDialogs;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edActivation\\ActivationKeyDialog", Win32PCOnly = true)]
public class ActivationKeyDialog : DialogBoxBase
{
	private IContainer components;

	private Button cancelButton;

	private Button okButton;

	private DialogTextBox ActivationKeyTextBox;

	private ActivationTitlePanel TitlePanel;

	private Panel panel1;

	private Label DirectionsLabel;

	private Label CheckEmailLabel;

	public Button CompleteActivationButton => okButton;

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
		this.cancelButton = new System.Windows.Forms.Button();
		this.okButton = new System.Windows.Forms.Button();
		this.ActivationKeyTextBox = new palDotNet.palDialog.DialogTextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.CheckEmailLabel = new System.Windows.Forms.Label();
		this.DirectionsLabel = new System.Windows.Forms.Label();
		this.TitlePanel = new edEditorDotNet.ActivationDialogs.ActivationTitlePanel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(407, 277);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(75, 23);
		this.cancelButton.TabIndex = 0;
		this.cancelButton.Text = "Exit";
		this.cancelButton.UseVisualStyleBackColor = true;
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.Location = new System.Drawing.Point(250, 277);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(151, 23);
		this.okButton.TabIndex = 1;
		this.okButton.Text = "Complete Registration";
		this.okButton.UseVisualStyleBackColor = true;
		this.ActivationKeyTextBox.Location = new System.Drawing.Point(24, 44);
		this.ActivationKeyTextBox.Name = "ActivationKeyTextBox";
		this.ActivationKeyTextBox.SaveToConfig = true;
		this.ActivationKeyTextBox.Size = new System.Drawing.Size(338, 20);
		this.ActivationKeyTextBox.TabIndex = 2;
		this.ActivationKeyTextBox.TextChanged += new System.EventHandler(ActivationKeyTextBox_TextChanged);
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.CheckEmailLabel);
		this.panel1.Controls.Add(this.DirectionsLabel);
		this.panel1.Controls.Add(this.ActivationKeyTextBox);
		this.panel1.Location = new System.Drawing.Point(38, 110);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(420, 123);
		this.panel1.TabIndex = 7;
		this.CheckEmailLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CheckEmailLabel.Location = new System.Drawing.Point(21, 78);
		this.CheckEmailLabel.Name = "CheckEmailLabel";
		this.CheckEmailLabel.Size = new System.Drawing.Size(370, 33);
		this.CheckEmailLabel.TabIndex = 13;
		this.CheckEmailLabel.Text = "Your registration key should have been sent to your email address.\r\nBe sure to check your spam filter.";
		this.DirectionsLabel.AutoSize = true;
		this.DirectionsLabel.Location = new System.Drawing.Point(21, 18);
		this.DirectionsLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.DirectionsLabel.Name = "DirectionsLabel";
		this.DirectionsLabel.Size = new System.Drawing.Size(166, 13);
		this.DirectionsLabel.TabIndex = 12;
		this.DirectionsLabel.Text = "Please enter your registration key:";
		this.TitlePanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TitlePanel.BackColor = System.Drawing.SystemColors.Window;
		this.TitlePanel.Location = new System.Drawing.Point(24, 14);
		this.TitlePanel.Margin = new System.Windows.Forms.Padding(15, 5, 15, 3);
		this.TitlePanel.Name = "TitlePanel";
		this.TitlePanel.Size = new System.Drawing.Size(446, 80);
		this.TitlePanel.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Window;
		base.CancelButton = this.cancelButton;
		base.ClientSize = new System.Drawing.Size(494, 312);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.TitlePanel);
		base.Controls.Add(this.okButton);
		base.Controls.Add(this.cancelButton);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "ActivationKeyDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "CityScape Activation - Finalize";
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}

	public ActivationKeyDialog()
	{
		InitializeComponent();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		ActivationKeyTextBox.Text = "";
		SyncOkButton();
	}

	private void ActivationKeyTextBox_TextChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	public string GetActivationKey()
	{
		return ActivationKeyTextBox.Text;
	}

	private void SyncOkButton()
	{
		if (ActivationKeyTextBox.Text.Length > 0)
		{
			AllowSubmitDialog = true;
			okButton.Enabled = true;
		}
		else
		{
			AllowSubmitDialog = false;
			okButton.Enabled = false;
		}
	}

	public void SetDialogResultOk()
	{
		base.DialogResult = DialogResult.OK;
	}
}
