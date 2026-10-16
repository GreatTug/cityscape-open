using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ktKitDotNet;

public class ColorType : UserControl
{
	private IContainer components;

	private Button ColorButton;

	private ColorDialog ColorPicker;

	private GroupBox groupBox1;

	public event KitCommon.ColorChangedHandler ColorChanged;

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
		this.ColorButton = new System.Windows.Forms.Button();
		this.ColorPicker = new System.Windows.Forms.ColorDialog();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.ColorButton.BackColor = System.Drawing.SystemColors.Control;
		this.ColorButton.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ColorButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ColorButton.Location = new System.Drawing.Point(6, 19);
		this.ColorButton.Name = "ColorButton";
		this.ColorButton.Padding = new System.Windows.Forms.Padding(3);
		this.ColorButton.Size = new System.Drawing.Size(131, 26);
		this.ColorButton.TabIndex = 0;
		this.ColorButton.Text = "Color";
		this.ColorButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.ColorButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.ColorButton.UseVisualStyleBackColor = false;
		this.ColorButton.Click += new System.EventHandler(ColorButton_Click);
		this.groupBox1.Controls.Add(this.ColorButton);
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
		this.groupBox1.Size = new System.Drawing.Size(143, 51);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Default Value";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "ColorType";
		base.Size = new System.Drawing.Size(303, 310);
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public ColorType()
	{
		InitializeComponent();
	}

	public void Initialize(Color color)
	{
		ColorPicker.Color = color;
		SetColorImage();
	}

	private void ColorButton_Click(object sender, EventArgs e)
	{
		if (ColorPicker.ShowDialog() == DialogResult.OK)
		{
			ColorChanged(ColorPicker.Color);
			SetColorImage();
		}
	}

	private void SetColorImage()
	{
		int num = 18;
		int num2 = 12;
		Bitmap bitmap = new Bitmap(num, num2);
		Color color = Color.FromArgb(255, ColorPicker.Color);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				if (i == 0 || j == 0 || i == num - 1 || j == num2 - 1)
				{
					bitmap.SetPixel(i, j, Color.Black);
				}
				else
				{
					bitmap.SetPixel(i, j, color);
				}
			}
		}
		ColorButton.Image = bitmap;
		Color color2 = ColorPicker.Color;
		ColorButton.Text = color2.R + "," + color2.G + "," + color2.B + "," + color2.A;
	}
}
