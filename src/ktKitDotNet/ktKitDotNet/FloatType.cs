using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ktKitDotNet;

public class FloatType : UserControl
{
	private bool Initializing;

	private IContainer components;

	private ComboBox UnitsValue;

	private Label label1;

	private GroupBox groupBox2;

	private NumericUpDown StepValue;

	private NumericUpDown MaxValue;

	private NumericUpDown MinValue;

	private Label label4;

	private Label label3;

	private Label label2;

	private GroupBox groupBox1;

	private NumericUpDown FloatValue;

	public event KitCommon.FloatChangedHandler FloatChanged;

	public event KitCommon.FloatChangedHandler MinChanged;

	public event KitCommon.FloatChangedHandler MaxChanged;

	public event KitCommon.FloatChangedHandler StepChanged;

	public event KitCommon.UnitsChangedHandler UnitsChanged;

	public FloatType()
	{
		InitializeComponent();
		Initializing = false;
	}

	public void Initialize(float x, float minVal, float maxVal, float stepVal, KitCommon.Units units)
	{
		Initializing = true;
		FloatValue.Value = new decimal(x);
		MinValue.Value = new decimal(minVal);
		MaxValue.Value = new decimal(maxVal);
		StepValue.Value = new decimal(stepVal);
		switch (units)
		{
		case KitCommon.Units.eNone:
			UnitsValue.SelectedIndex = 0;
			break;
		case KitCommon.Units.eMetric:
			UnitsValue.SelectedIndex = 1;
			break;
		case KitCommon.Units.eAngle:
			UnitsValue.SelectedIndex = 2;
			break;
		case KitCommon.Units.ePercent:
			UnitsValue.SelectedIndex = 3;
			break;
		}
		Initializing = false;
	}

	private void FloatValue_ValueChanged(object sender, EventArgs e)
	{
		if (!Initializing && FloatChanged != null)
		{
			FloatChanged(decimal.ToSingle(FloatValue.Value));
		}
	}

	private void MinValue_ValueChanged(object sender, EventArgs e)
	{
		if (!Initializing && MinChanged != null)
		{
			MinChanged(decimal.ToSingle(MinValue.Value));
		}
	}

	private void MaxValue_ValueChanged(object sender, EventArgs e)
	{
		if (!Initializing && MaxChanged != null)
		{
			MaxChanged(decimal.ToSingle(MaxValue.Value));
		}
	}

	private void StepValue_ValueChanged(object sender, EventArgs e)
	{
		if (!Initializing && StepChanged != null)
		{
			StepChanged(decimal.ToSingle(StepValue.Value));
		}
	}

	private void UnitsValue_SelectedValueChanged(object sender, EventArgs e)
	{
		if (!Initializing && UnitsChanged != null)
		{
			switch (UnitsValue.SelectedIndex)
			{
			case 0:
				UnitsChanged(KitCommon.Units.eNone);
				break;
			case 1:
				UnitsChanged(KitCommon.Units.eMetric);
				break;
			case 2:
				UnitsChanged(KitCommon.Units.eAngle);
				break;
			case 3:
				UnitsChanged(KitCommon.Units.ePercent);
				break;
			default:
				UnitsChanged(KitCommon.Units.eNone);
				break;
			}
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
		this.UnitsValue = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.StepValue = new System.Windows.Forms.NumericUpDown();
		this.MaxValue = new System.Windows.Forms.NumericUpDown();
		this.MinValue = new System.Windows.Forms.NumericUpDown();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.FloatValue = new System.Windows.Forms.NumericUpDown();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.StepValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.MaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.MinValue).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.FloatValue).BeginInit();
		base.SuspendLayout();
		this.UnitsValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.UnitsValue.FormattingEnabled = true;
		this.UnitsValue.Items.AddRange(new object[4] { "None", "Meters", "Degrees", "Percent" });
		this.UnitsValue.Location = new System.Drawing.Point(59, 97);
		this.UnitsValue.Name = "UnitsValue";
		this.UnitsValue.Size = new System.Drawing.Size(121, 21);
		this.UnitsValue.TabIndex = 9;
		this.UnitsValue.SelectedValueChanged += new System.EventHandler(UnitsValue_SelectedValueChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 100);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(31, 13);
		this.label1.TabIndex = 8;
		this.label1.Text = "Units";
		this.groupBox2.Controls.Add(this.StepValue);
		this.groupBox2.Controls.Add(this.MaxValue);
		this.groupBox2.Controls.Add(this.UnitsValue);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.MinValue);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Location = new System.Drawing.Point(3, 60);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(190, 126);
		this.groupBox2.TabIndex = 11;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Parameters";
		this.StepValue.DecimalPlaces = 3;
		this.StepValue.Increment = new decimal(new int[4] { 1, 0, 0, 196608 });
		this.StepValue.Location = new System.Drawing.Point(60, 71);
		this.StepValue.Maximum = new decimal(new int[4] { -64771072, 6, 0, 0 });
		this.StepValue.Minimum = new decimal(new int[4] { 1, 0, 0, 196608 });
		this.StepValue.Name = "StepValue";
		this.StepValue.Size = new System.Drawing.Size(120, 20);
		this.StepValue.TabIndex = 9;
		this.StepValue.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.StepValue.ValueChanged += new System.EventHandler(StepValue_ValueChanged);
		this.MaxValue.DecimalPlaces = 3;
		this.MaxValue.Increment = new decimal(new int[4] { 1, 0, 0, 196608 });
		this.MaxValue.Location = new System.Drawing.Point(60, 45);
		this.MaxValue.Maximum = new decimal(new int[4] { -64771072, 6, 0, 0 });
		this.MaxValue.Minimum = new decimal(new int[4] { -64771072, 6, 0, -2147483648 });
		this.MaxValue.Name = "MaxValue";
		this.MaxValue.Size = new System.Drawing.Size(120, 20);
		this.MaxValue.TabIndex = 8;
		this.MaxValue.ValueChanged += new System.EventHandler(MaxValue_ValueChanged);
		this.MinValue.DecimalPlaces = 3;
		this.MinValue.Increment = new decimal(new int[4] { 1, 0, 0, 196608 });
		this.MinValue.Location = new System.Drawing.Point(60, 19);
		this.MinValue.Maximum = new decimal(new int[4] { -64771072, 6, 0, 0 });
		this.MinValue.Minimum = new decimal(new int[4] { -64771072, 6, 0, -2147483648 });
		this.MinValue.Name = "MinValue";
		this.MinValue.Size = new System.Drawing.Size(120, 20);
		this.MinValue.TabIndex = 1;
		this.MinValue.ValueChanged += new System.EventHandler(MinValue_ValueChanged);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 74);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(29, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Step";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 47);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(51, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "Maximum";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 21);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(48, 13);
		this.label2.TabIndex = 5;
		this.label2.Text = "Minimum";
		this.groupBox1.Controls.Add(this.FloatValue);
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(190, 50);
		this.groupBox1.TabIndex = 10;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Default Value";
		this.FloatValue.DecimalPlaces = 3;
		this.FloatValue.Increment = new decimal(new int[4] { 1, 0, 0, 196608 });
		this.FloatValue.Location = new System.Drawing.Point(7, 20);
		this.FloatValue.Maximum = new decimal(new int[4] { -64771072, 6, 0, 0 });
		this.FloatValue.Minimum = new decimal(new int[4] { -64771072, 6, 0, -2147483648 });
		this.FloatValue.Name = "FloatValue";
		this.FloatValue.Size = new System.Drawing.Size(120, 20);
		this.FloatValue.TabIndex = 0;
		this.FloatValue.ValueChanged += new System.EventHandler(FloatValue_ValueChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "FloatType";
		base.Size = new System.Drawing.Size(348, 350);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.StepValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.MaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.MinValue).EndInit();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.FloatValue).EndInit();
		base.ResumeLayout(false);
	}
}
