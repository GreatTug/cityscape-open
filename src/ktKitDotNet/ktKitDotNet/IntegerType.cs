using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ktKitDotNet;

public class IntegerType : UserControl
{
	private IContainer components;

	private Label label2;

	private Label label3;

	private Label label4;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private NumericUpDown IntValue;

	private NumericUpDown StepValue;

	private NumericUpDown MaxValue;

	private NumericUpDown MinValue;

	private bool Initializing;

	public event KitCommon.IntChangedHandler IntChanged;

	public event KitCommon.IntChangedHandler MinChanged;

	public event KitCommon.IntChangedHandler MaxChanged;

	public event KitCommon.IntChangedHandler StepChanged;

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
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.IntValue = new System.Windows.Forms.NumericUpDown();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.StepValue = new System.Windows.Forms.NumericUpDown();
		this.MaxValue = new System.Windows.Forms.NumericUpDown();
		this.MinValue = new System.Windows.Forms.NumericUpDown();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.IntValue).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.StepValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.MaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.MinValue).BeginInit();
		base.SuspendLayout();
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 21);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(48, 13);
		this.label2.TabIndex = 5;
		this.label2.Text = "Minimum";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 47);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(51, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "Maximum";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 74);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(29, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Step";
		this.groupBox1.Controls.Add(this.IntValue);
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(190, 50);
		this.groupBox1.TabIndex = 8;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Default Value";
		this.IntValue.Location = new System.Drawing.Point(7, 20);
		this.IntValue.Maximum = new decimal(new int[4] { 2147483647, 0, 0, 0 });
		this.IntValue.Minimum = new decimal(new int[4] { -2147483648, 0, 0, -2147483648 });
		this.IntValue.Name = "IntValue";
		this.IntValue.Size = new System.Drawing.Size(120, 20);
		this.IntValue.TabIndex = 0;
		this.IntValue.ValueChanged += new System.EventHandler(IntValue_ValueChanged);
		this.groupBox2.Controls.Add(this.StepValue);
		this.groupBox2.Controls.Add(this.MaxValue);
		this.groupBox2.Controls.Add(this.MinValue);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Location = new System.Drawing.Point(4, 61);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(190, 100);
		this.groupBox2.TabIndex = 9;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Parameters";
		this.StepValue.Location = new System.Drawing.Point(60, 71);
		this.StepValue.Maximum = new decimal(new int[4] { 2147483647, 0, 0, 0 });
		this.StepValue.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.StepValue.Name = "StepValue";
		this.StepValue.Size = new System.Drawing.Size(120, 20);
		this.StepValue.TabIndex = 9;
		this.StepValue.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.StepValue.ValueChanged += new System.EventHandler(StepValue_ValueChanged);
		this.MaxValue.Location = new System.Drawing.Point(60, 45);
		this.MaxValue.Maximum = new decimal(new int[4] { 2147483647, 0, 0, 0 });
		this.MaxValue.Minimum = new decimal(new int[4] { -2147483648, 0, 0, -2147483648 });
		this.MaxValue.Name = "MaxValue";
		this.MaxValue.Size = new System.Drawing.Size(120, 20);
		this.MaxValue.TabIndex = 8;
		this.MaxValue.ValueChanged += new System.EventHandler(MaxValue_ValueChanged);
		this.MinValue.Location = new System.Drawing.Point(60, 19);
		this.MinValue.Maximum = new decimal(new int[4] { 2147483647, 0, 0, 0 });
		this.MinValue.Minimum = new decimal(new int[4] { -2147483648, 0, 0, -2147483648 });
		this.MinValue.Name = "MinValue";
		this.MinValue.Size = new System.Drawing.Size(120, 20);
		this.MinValue.TabIndex = 1;
		this.MinValue.ValueChanged += new System.EventHandler(MinValue_ValueChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "IntegerType";
		base.Size = new System.Drawing.Size(334, 347);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.IntValue).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.StepValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.MaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.MinValue).EndInit();
		base.ResumeLayout(false);
	}

	public IntegerType()
	{
		InitializeComponent();
		Initializing = false;
	}

	public void Initialize(int i, int minVal, int maxVal, int stepVal)
	{
		Initializing = true;
		IntValue.Value = new decimal(i);
		MinValue.Value = new decimal(minVal);
		MaxValue.Value = new decimal(maxVal);
		StepValue.Value = new decimal(stepVal);
		Initializing = false;
	}

	private void IntValue_ValueChanged(object sender, EventArgs e)
	{
		if (!Initializing)
		{
			IntChanged(decimal.ToInt32(IntValue.Value));
		}
	}

	private void MinValue_ValueChanged(object sender, EventArgs e)
	{
		if (!Initializing)
		{
			MinChanged(decimal.ToInt32(MinValue.Value));
		}
	}

	private void MaxValue_ValueChanged(object sender, EventArgs e)
	{
		if (!Initializing)
		{
			MaxChanged(decimal.ToInt32(MaxValue.Value));
		}
	}

	private void StepValue_ValueChanged(object sender, EventArgs e)
	{
		if (!Initializing)
		{
			StepChanged(decimal.ToInt32(StepValue.Value));
		}
	}
}
