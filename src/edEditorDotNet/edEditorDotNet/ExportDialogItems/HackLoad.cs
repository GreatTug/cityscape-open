using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet.ExportDialogItems;

public class HackLoad : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox groupBox1;

	private DialogRadioButton LaunchXBox360;

	private DialogRadioButton LaunchPS3;

	private DialogRadioButton LaunchNone;

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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.LaunchXBox360 = new palDotNet.palDialog.DialogRadioButton();
		this.LaunchPS3 = new palDotNet.palDialog.DialogRadioButton();
		this.LaunchNone = new palDotNet.palDialog.DialogRadioButton();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.LaunchXBox360);
		this.groupBox1.Controls.Add(this.LaunchPS3);
		this.groupBox1.Controls.Add(this.LaunchNone);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(102, 89);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Launch console";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.LaunchXBox360.AutoSize = true;
		this.LaunchXBox360.Location = new System.Drawing.Point(7, 66);
		this.LaunchXBox360.Name = "LaunchXBox360";
		this.LaunchXBox360.Size = new System.Drawing.Size(68, 17);
		this.LaunchXBox360.TabIndex = 2;
		this.LaunchXBox360.TabStop = true;
		this.LaunchXBox360.Text = "XBox360";
		this.LaunchXBox360.UseVisualStyleBackColor = true;
		this.LaunchXBox360.CheckedChanged += new System.EventHandler(dialogRadioButton1_CheckedChanged);
		this.LaunchPS3.AutoSize = true;
		this.LaunchPS3.Location = new System.Drawing.Point(7, 43);
		this.LaunchPS3.Name = "LaunchPS3";
		this.LaunchPS3.Size = new System.Drawing.Size(45, 17);
		this.LaunchPS3.TabIndex = 1;
		this.LaunchPS3.TabStop = true;
		this.LaunchPS3.Text = "PS3";
		this.LaunchPS3.UseVisualStyleBackColor = true;
		this.LaunchNone.AutoSize = true;
		this.LaunchNone.Checked = true;
		this.LaunchNone.Location = new System.Drawing.Point(6, 19);
		this.LaunchNone.Name = "LaunchNone";
		this.LaunchNone.Size = new System.Drawing.Size(51, 17);
		this.LaunchNone.TabIndex = 0;
		this.LaunchNone.TabStop = true;
		this.LaunchNone.Text = "None";
		this.LaunchNone.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "HackLoad";
		base.Size = new System.Drawing.Size(102, 89);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	public HackLoad()
	{
		InitializeComponent();
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void dialogRadioButton1_CheckedChanged(object sender, EventArgs e)
	{
	}
}
