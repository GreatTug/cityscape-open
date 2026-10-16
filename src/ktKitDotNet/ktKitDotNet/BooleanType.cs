using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ktKitDotNet;

public class BooleanType : UserControl
{
	private bool Initializing;

	private IContainer components;

	private CheckBox BoolValue;

	private GroupBox groupBox1;

	public event KitCommon.BoolChangedHandler BoolChanged;

	public BooleanType()
	{
		InitializeComponent();
		Initializing = false;
	}

	public void Initialize(bool value)
	{
		Initializing = true;
		BoolValue.Checked = value;
		Initializing = false;
	}

	private void BoolValue_CheckedChanged(object sender, EventArgs e)
	{
		if (!Initializing && BoolChanged != null)
		{
			BoolChanged(BoolValue.Checked);
		}
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
		this.BoolValue = new System.Windows.Forms.CheckBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.BoolValue.AutoSize = true;
		this.BoolValue.Location = new System.Drawing.Point(6, 19);
		this.BoolValue.Name = "BoolValue";
		this.BoolValue.Size = new System.Drawing.Size(53, 17);
		this.BoolValue.TabIndex = 0;
		this.BoolValue.Text = "Value";
		this.BoolValue.UseVisualStyleBackColor = true;
		this.BoolValue.CheckedChanged += new System.EventHandler(BoolValue_CheckedChanged);
		this.groupBox1.Controls.Add(this.BoolValue);
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(86, 43);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Default Value";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "BooleanType";
		base.Size = new System.Drawing.Size(400, 391);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
