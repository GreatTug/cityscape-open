using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edStreetView\\StreetViewDialog")]
[DialogBoxCreator]
public class StreetViewDialog : DialogBoxBase
{
	private IContainer components;

	private DialogTextBox serverName;

	private DialogLabel serverNameLabel;

	private DialogNumericUpDownInt portNumber;

	private DialogLabel portNumberLabel;

	private Button connectButton;

	private Button cancelButton;

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
		this.serverName = new palDotNet.palDialog.DialogTextBox();
		this.serverNameLabel = new palDotNet.palDialog.DialogLabel();
		this.portNumber = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.portNumberLabel = new palDotNet.palDialog.DialogLabel();
		this.connectButton = new System.Windows.Forms.Button();
		this.cancelButton = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.portNumber).BeginInit();
		base.SuspendLayout();
		this.serverName.Location = new System.Drawing.Point(86, 12);
		this.serverName.Name = "serverName";
		this.serverName.SaveToConfig = true;
		this.serverName.Size = new System.Drawing.Size(180, 20);
		this.serverName.TabIndex = 0;
		this.serverName.Text = "127.0.0.1";
		this.serverNameLabel.AutoSize = true;
		this.serverNameLabel.Location = new System.Drawing.Point(11, 15);
		this.serverNameLabel.Name = "serverNameLabel";
		this.serverNameLabel.SaveToConfig = false;
		this.serverNameLabel.Size = new System.Drawing.Size(69, 13);
		this.serverNameLabel.TabIndex = 1;
		this.serverNameLabel.Text = "Server Name";
		this.portNumber.Location = new System.Drawing.Point(85, 42);
		this.portNumber.Maximum = new decimal(new int[4] { 65535, 0, 0, 0 });
		this.portNumber.Name = "portNumber";
		this.portNumber.SaveToConfig = true;
		this.portNumber.Size = new System.Drawing.Size(58, 20);
		this.portNumber.TabIndex = 2;
		this.portNumber.Value = new decimal(new int[4] { 8221, 0, 0, 0 });
		this.portNumberLabel.AutoSize = true;
		this.portNumberLabel.Location = new System.Drawing.Point(53, 44);
		this.portNumberLabel.Name = "portNumberLabel";
		this.portNumberLabel.SaveToConfig = false;
		this.portNumberLabel.Size = new System.Drawing.Size(26, 13);
		this.portNumberLabel.TabIndex = 3;
		this.portNumberLabel.Text = "Port";
		this.connectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.connectButton.Location = new System.Drawing.Point(56, 68);
		this.connectButton.Name = "connectButton";
		this.connectButton.Size = new System.Drawing.Size(78, 24);
		this.connectButton.TabIndex = 4;
		this.connectButton.Text = "Connect";
		this.connectButton.UseVisualStyleBackColor = true;
		this.connectButton.Click += new System.EventHandler(connectButton_Click);
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(140, 67);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(78, 24);
		this.cancelButton.TabIndex = 5;
		this.cancelButton.Text = "Cancel";
		this.cancelButton.UseVisualStyleBackColor = true;
		this.cancelButton.Click += new System.EventHandler(cancelButton_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(280, 103);
		base.Controls.Add(this.cancelButton);
		base.Controls.Add(this.connectButton);
		base.Controls.Add(this.portNumberLabel);
		base.Controls.Add(this.portNumber);
		base.Controls.Add(this.serverNameLabel);
		base.Controls.Add(this.serverName);
		base.Name = "StreetViewDialog";
		this.Text = "Street View";
		((System.ComponentModel.ISupportInitialize)this.portNumber).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public StreetViewDialog()
	{
		InitializeComponent();
	}

	private void connectButton_Click(object sender, EventArgs e)
	{
	}

	private void cancelButton_Click(object sender, EventArgs e)
	{
	}
}
