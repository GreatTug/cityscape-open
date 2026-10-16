using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edDialogs\\KeyLokNetworkDialog")]
[DialogBoxCreator]
public class KeyLokNetworkDialog : DialogBoxBase
{
	private IContainer components;

	private DialogTextBox KeyLokComputer;

	private Label label1;

	private PictureBox pictureBox1;

	private Button buttonOK;

	private Label label2;

	private Button buttonCancel;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(palDotNet.palDialog.KeyLokNetworkDialog));
		this.KeyLokComputer = new palDotNet.palDialog.DialogTextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.buttonOK = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.buttonCancel = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.KeyLokComputer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.KeyLokComputer.Location = new System.Drawing.Point(12, 72);
		this.KeyLokComputer.Name = "KeyLokComputer";
		this.KeyLokComputer.Size = new System.Drawing.Size(299, 20);
		this.KeyLokComputer.TabIndex = 0;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label1.Location = new System.Drawing.Point(75, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(236, 48);
		this.label1.TabIndex = 1;
		this.label1.Text = "Please input the name or the IP address of the computer on the network that has the PixelActive KEYLOK dongle attached:";
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(14, 18);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(47, 48);
		this.pictureBox1.TabIndex = 2;
		this.pictureBox1.TabStop = false;
		this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.buttonOK.Location = new System.Drawing.Point(153, 149);
		this.buttonOK.Name = "buttonOK";
		this.buttonOK.Size = new System.Drawing.Size(75, 23);
		this.buttonOK.TabIndex = 21;
		this.buttonOK.Text = "OK";
		this.buttonOK.UseVisualStyleBackColor = true;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label2.Location = new System.Drawing.Point(12, 106);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(299, 40);
		this.label2.TabIndex = 22;
		this.label2.Text = "Press 'OK' to check for the KeyLok and to run the application\r\nPress 'Exit' to exit the program\r\n";
		this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.buttonCancel.Location = new System.Drawing.Point(236, 149);
		this.buttonCancel.Name = "buttonCancel";
		this.buttonCancel.Size = new System.Drawing.Size(75, 23);
		this.buttonCancel.TabIndex = 21;
		this.buttonCancel.Text = "Exit";
		this.buttonCancel.UseVisualStyleBackColor = true;
		this.buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
		base.AcceptButton = this.buttonOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.buttonCancel;
		base.ClientSize = new System.Drawing.Size(323, 184);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.buttonCancel);
		base.Controls.Add(this.buttonOK);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.KeyLokComputer);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "KeyLokNetworkDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "KEYLOCK Computer";
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public KeyLokNetworkDialog()
	{
		InitializeComponent();
	}

	private void buttonCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}
}
