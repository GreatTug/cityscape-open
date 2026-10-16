using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edPhysics\\PhysicsControllerDialog")]
public class PhysicsControllerDialog : DialogBoxBase
{
	private IContainer components;

	private Button mOkButton;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private PictureBox HelicopterControlsPictureBox;

	private PictureBox CarControlsPictureBox;

	private DialogLabel InfoLabel;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(edEditorDotNet.PhysicsControllerDialog));
		this.mOkButton = new System.Windows.Forms.Button();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.CarControlsPictureBox = new System.Windows.Forms.PictureBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.HelicopterControlsPictureBox = new System.Windows.Forms.PictureBox();
		this.InfoLabel = new palDotNet.palDialog.DialogLabel();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.CarControlsPictureBox).BeginInit();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.HelicopterControlsPictureBox).BeginInit();
		base.SuspendLayout();
		this.mOkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mOkButton.Location = new System.Drawing.Point(407, 416);
		this.mOkButton.Name = "mOkButton";
		this.mOkButton.Size = new System.Drawing.Size(75, 23);
		this.mOkButton.TabIndex = 0;
		this.mOkButton.Text = "OK";
		this.mOkButton.UseVisualStyleBackColor = true;
		this.mOkButton.Click += new System.EventHandler(mOkButton_Click);
		this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(12, 12);
		this.tabControl1.Multiline = true;
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(470, 379);
		this.tabControl1.TabIndex = 3;
		this.tabPage1.Controls.Add(this.CarControlsPictureBox);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(462, 353);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Car Controls";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.CarControlsPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.CarControlsPictureBox.Image = (System.Drawing.Image)resources.GetObject("CarControlsPictureBox.Image");
		this.CarControlsPictureBox.Location = new System.Drawing.Point(3, 3);
		this.CarControlsPictureBox.Name = "CarControlsPictureBox";
		this.CarControlsPictureBox.Size = new System.Drawing.Size(456, 347);
		this.CarControlsPictureBox.TabIndex = 0;
		this.CarControlsPictureBox.TabStop = false;
		this.tabPage2.Controls.Add(this.HelicopterControlsPictureBox);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(462, 353);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Helicopter Controls";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.HelicopterControlsPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.HelicopterControlsPictureBox.Image = (System.Drawing.Image)resources.GetObject("HelicopterControlsPictureBox.Image");
		this.HelicopterControlsPictureBox.Location = new System.Drawing.Point(3, 3);
		this.HelicopterControlsPictureBox.Name = "HelicopterControlsPictureBox";
		this.HelicopterControlsPictureBox.Size = new System.Drawing.Size(456, 347);
		this.HelicopterControlsPictureBox.TabIndex = 0;
		this.HelicopterControlsPictureBox.TabStop = false;
		this.InfoLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.InfoLabel.AutoSize = true;
		this.InfoLabel.Location = new System.Drawing.Point(12, 421);
		this.InfoLabel.Name = "InfoLabel";
		this.InfoLabel.SaveToConfig = false;
		this.InfoLabel.Size = new System.Drawing.Size(219, 13);
		this.InfoLabel.TabIndex = 4;
		this.InfoLabel.Text = "NOTE: buttons on other controllers may vary.";
		base.AcceptButton = this.mOkButton;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(494, 451);
		base.Controls.Add(this.InfoLabel);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.mOkButton);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "PhysicsControllerDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Controller Layout";
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.CarControlsPictureBox).EndInit();
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.HelicopterControlsPictureBox).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public PhysicsControllerDialog()
	{
		InitializeComponent();
	}

	private void mOkButton_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
