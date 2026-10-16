using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ktKitDotNet;

public class StringType : UserControl
{
	private bool Initializing;

	private IContainer components;

	private TextBox StringValue;

	private ComboBox DisplayValue;

	private Label label2;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	public event KitCommon.StringChangedHandler StringChanged;

	public event KitCommon.StringDisplayChangedHandler DisplayChanged;

	public StringType()
	{
		InitializeComponent();
		Initializing = false;
	}

	public void Initialize(string value, KitCommon.StringDisplayType display)
	{
		Initializing = true;
		StringValue.Text = value;
		switch (display)
		{
		case KitCommon.StringDisplayType.eTextBox:
			DisplayValue.SelectedIndex = 0;
			break;
		case KitCommon.StringDisplayType.eFileLoader:
			DisplayValue.SelectedIndex = 1;
			break;
		}
		Initializing = false;
	}

	private void StringValue_TextChanged(object sender, EventArgs e)
	{
		if (!Initializing && StringChanged != null)
		{
			StringChanged(StringValue.Text);
		}
	}

	private void DisplayValue_SelectedValueChanged(object sender, EventArgs e)
	{
		if (!Initializing && DisplayChanged != null)
		{
			switch (DisplayValue.SelectedIndex)
			{
			case 0:
				DisplayChanged(KitCommon.StringDisplayType.eTextBox);
				break;
			case 1:
				DisplayChanged(KitCommon.StringDisplayType.eFileLoader);
				break;
			default:
				DisplayChanged(KitCommon.StringDisplayType.eTextBox);
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
		this.StringValue = new System.Windows.Forms.TextBox();
		this.DisplayValue = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.StringValue.AcceptsReturn = true;
		this.StringValue.Location = new System.Drawing.Point(6, 19);
		this.StringValue.Name = "StringValue";
		this.StringValue.Size = new System.Drawing.Size(190, 20);
		this.StringValue.TabIndex = 1;
		this.StringValue.TextChanged += new System.EventHandler(StringValue_TextChanged);
		this.DisplayValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.DisplayValue.FormattingEnabled = true;
		this.DisplayValue.Items.AddRange(new object[2] { "Text Box", "File Loader" });
		this.DisplayValue.Location = new System.Drawing.Point(53, 19);
		this.DisplayValue.Name = "DisplayValue";
		this.DisplayValue.Size = new System.Drawing.Size(121, 21);
		this.DisplayValue.TabIndex = 2;
		this.DisplayValue.SelectedValueChanged += new System.EventHandler(DisplayValue_SelectedValueChanged);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 22);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "Display";
		this.groupBox1.Controls.Add(this.StringValue);
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(205, 47);
		this.groupBox1.TabIndex = 4;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Default Value";
		this.groupBox2.Controls.Add(this.DisplayValue);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Location = new System.Drawing.Point(3, 56);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(205, 48);
		this.groupBox2.TabIndex = 5;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Parameters";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "StringType";
		base.Size = new System.Drawing.Size(290, 365);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
