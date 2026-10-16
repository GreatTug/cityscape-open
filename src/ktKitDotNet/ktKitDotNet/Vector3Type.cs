using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ktKitDotNet;

public class Vector3Type : UserControl
{
	private IContainer components;

	private GroupBox groupBox2;

	private NumericUpDown StepValue;

	private NumericUpDown MaxValue;

	private ComboBox UnitsValue;

	private Label label1;

	private NumericUpDown MinValue;

	private Label label4;

	private Label label3;

	private Label label2;

	private GroupBox groupBox1;

	private Label label6;

	private NumericUpDown YValue;

	private Label label5;

	private NumericUpDown XValue;

	private Label label7;

	private NumericUpDown ZValue;

	private bool Initializing;

	public event KitCommon.Vector3ChangedHandler Vector3Changed;

	public event KitCommon.FloatChangedHandler MinChanged;

	public event KitCommon.FloatChangedHandler MaxChanged;

	public event KitCommon.FloatChangedHandler StepChanged;

	public event KitCommon.UnitsChangedHandler UnitsChanged;

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
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.StepValue = new System.Windows.Forms.NumericUpDown();
		this.MaxValue = new System.Windows.Forms.NumericUpDown();
		this.UnitsValue = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.MinValue = new System.Windows.Forms.NumericUpDown();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label7 = new System.Windows.Forms.Label();
		this.ZValue = new System.Windows.Forms.NumericUpDown();
		this.label6 = new System.Windows.Forms.Label();
		this.YValue = new System.Windows.Forms.NumericUpDown();
		this.label5 = new System.Windows.Forms.Label();
		this.XValue = new System.Windows.Forms.NumericUpDown();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.StepValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.MaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.MinValue).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ZValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.YValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.XValue).BeginInit();
		base.SuspendLayout();
		this.groupBox2.Controls.Add(this.StepValue);
		this.groupBox2.Controls.Add(this.MaxValue);
		this.groupBox2.Controls.Add(this.UnitsValue);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.MinValue);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Location = new System.Drawing.Point(3, 107);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(190, 126);
		this.groupBox2.TabIndex = 15;
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
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.ZValue);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.YValue);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.XValue);
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(190, 98);
		this.groupBox1.TabIndex = 14;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Default Value";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(6, 73);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(14, 13);
		this.label7.TabIndex = 5;
		this.label7.Text = "Z";
		this.ZValue.DecimalPlaces = 3;
		this.ZValue.Increment = new decimal(new int[4] { 1, 0, 0, 196608 });
		this.ZValue.Location = new System.Drawing.Point(26, 71);
		this.ZValue.Maximum = new decimal(new int[4] { -64771072, 6, 0, 0 });
		this.ZValue.Minimum = new decimal(new int[4] { -64771072, 6, 0, -2147483648 });
		this.ZValue.Name = "ZValue";
		this.ZValue.Size = new System.Drawing.Size(120, 20);
		this.ZValue.TabIndex = 4;
		this.ZValue.ValueChanged += new System.EventHandler(Value_ValueChanged);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(6, 47);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(14, 13);
		this.label6.TabIndex = 3;
		this.label6.Text = "Y";
		this.YValue.DecimalPlaces = 3;
		this.YValue.Increment = new decimal(new int[4] { 1, 0, 0, 196608 });
		this.YValue.Location = new System.Drawing.Point(26, 45);
		this.YValue.Maximum = new decimal(new int[4] { -64771072, 6, 0, 0 });
		this.YValue.Minimum = new decimal(new int[4] { -64771072, 6, 0, -2147483648 });
		this.YValue.Name = "YValue";
		this.YValue.Size = new System.Drawing.Size(120, 20);
		this.YValue.TabIndex = 2;
		this.YValue.ValueChanged += new System.EventHandler(Value_ValueChanged);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 21);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(14, 13);
		this.label5.TabIndex = 1;
		this.label5.Text = "X";
		this.XValue.DecimalPlaces = 3;
		this.XValue.Increment = new decimal(new int[4] { 1, 0, 0, 196608 });
		this.XValue.Location = new System.Drawing.Point(26, 19);
		this.XValue.Maximum = new decimal(new int[4] { -64771072, 6, 0, 0 });
		this.XValue.Minimum = new decimal(new int[4] { -64771072, 6, 0, -2147483648 });
		this.XValue.Name = "XValue";
		this.XValue.Size = new System.Drawing.Size(120, 20);
		this.XValue.TabIndex = 0;
		this.XValue.ValueChanged += new System.EventHandler(Value_ValueChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "Vector3Type";
		base.Size = new System.Drawing.Size(362, 414);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.StepValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.MaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.MinValue).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ZValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.YValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.XValue).EndInit();
		base.ResumeLayout(false);
	}

	public Vector3Type()
	{
		InitializeComponent();
		Initializing = false;
	}

	public void Initialize(float x, float y, float z, float minVal, float maxVal, float stepVal, KitCommon.Units units)
	{
		Initializing = true;
		XValue.Value = new decimal(x);
		YValue.Value = new decimal(y);
		ZValue.Value = new decimal(z);
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

	private void Value_ValueChanged(object sender, EventArgs e)
	{
		if (!Initializing && Vector3Changed != null)
		{
			Vector3Changed(decimal.ToSingle(XValue.Value), decimal.ToSingle(YValue.Value), decimal.ToSingle(ZValue.Value));
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
}
