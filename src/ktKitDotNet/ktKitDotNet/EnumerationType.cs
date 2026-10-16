using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ktKitDotNet;

public class EnumerationType : UserControl
{
	private IContainer components;

	private GroupBox groupBox1;

	private ComboBox EnumValue;

	private GroupBox groupBox2;

	private ComboBox DisplayValue;

	private Label label1;

	private TextBox EnumList;

	private bool Initializing;

	public event KitCommon.EnumChangedHandler EnumChanged;

	public event KitCommon.EnumDisplayTypeChangedHandler DisplayTypeChanged;

	public event KitCommon.EnumListChangedHandler EnumListChanged;

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
		this.EnumValue = new System.Windows.Forms.ComboBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.EnumList = new System.Windows.Forms.TextBox();
		this.DisplayValue = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.EnumValue);
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(192, 50);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Default Value";
		this.EnumValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.EnumValue.FormattingEnabled = true;
		this.EnumValue.Location = new System.Drawing.Point(7, 20);
		this.EnumValue.Name = "EnumValue";
		this.EnumValue.Size = new System.Drawing.Size(179, 21);
		this.EnumValue.TabIndex = 0;
		this.EnumValue.SelectedValueChanged += new System.EventHandler(EnumValue_SelectedValueChanged);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.groupBox2.Controls.Add(this.EnumList);
		this.groupBox2.Controls.Add(this.DisplayValue);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Location = new System.Drawing.Point(4, 61);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(353, 321);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Parameters";
		this.EnumList.AcceptsReturn = true;
		this.EnumList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.EnumList.Location = new System.Drawing.Point(9, 47);
		this.EnumList.Multiline = true;
		this.EnumList.Name = "EnumList";
		this.EnumList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.EnumList.Size = new System.Drawing.Size(338, 268);
		this.EnumList.TabIndex = 5;
		this.EnumList.TextChanged += new System.EventHandler(EnumList_TextChanged);
		this.DisplayValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.DisplayValue.FormattingEnabled = true;
		this.DisplayValue.Items.AddRange(new object[2] { "Combo Box", "Radio Buttons" });
		this.DisplayValue.Location = new System.Drawing.Point(53, 19);
		this.DisplayValue.Name = "DisplayValue";
		this.DisplayValue.Size = new System.Drawing.Size(121, 21);
		this.DisplayValue.TabIndex = 1;
		this.DisplayValue.SelectedValueChanged += new System.EventHandler(DisplayValue_SelectedValueChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Display";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "EnumerationType";
		base.Size = new System.Drawing.Size(360, 385);
		this.groupBox1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}

	public EnumerationType()
	{
		InitializeComponent();
		Initializing = false;
	}

	public void Initialize(int selectedIndex, KitCommon.EnumDisplayType displayType, string[] enumList)
	{
		Initializing = true;
		try
		{
			EnumValue.SelectedIndex = selectedIndex;
		}
		catch (Exception)
		{
			try
			{
				EnumValue.SelectedIndex = 0;
			}
			catch (Exception)
			{
			}
		}
		switch (displayType)
		{
		case KitCommon.EnumDisplayType.eComboBox:
			DisplayValue.SelectedIndex = 0;
			break;
		case KitCommon.EnumDisplayType.eRadioButtons:
			DisplayValue.SelectedIndex = 1;
			break;
		}
		string text = "";
		bool flag = true;
		foreach (string text2 in enumList)
		{
			if (!flag)
			{
				text += "\r\n";
			}
			flag = false;
			text += text2;
		}
		EnumList.Text = text;
		Initializing = false;
	}

	private void EnumValue_SelectedValueChanged(object sender, EventArgs e)
	{
		if (!Initializing && EnumValue.SelectedIndex != -1 && EnumChanged != null)
		{
			EnumChanged(EnumValue.SelectedIndex);
		}
	}

	private void DisplayValue_SelectedValueChanged(object sender, EventArgs e)
	{
		if (!Initializing && DisplayTypeChanged != null)
		{
			switch (DisplayValue.SelectedIndex)
			{
			case 0:
				DisplayTypeChanged(KitCommon.EnumDisplayType.eComboBox);
				break;
			case 1:
				DisplayTypeChanged(KitCommon.EnumDisplayType.eRadioButtons);
				break;
			default:
				DisplayTypeChanged(KitCommon.EnumDisplayType.eComboBox);
				break;
			}
		}
	}

	private void EnumList_TextChanged(object sender, EventArgs e)
	{
		string[] array = EnumList.Text.Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
		string text = (string)EnumValue.SelectedItem;
		int num = EnumValue.SelectedIndex;
		bool flag = false;
		ComboBox.ObjectCollection items = EnumValue.Items;
		items.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			if (!flag && array[i] == text)
			{
				num = i;
				flag = true;
			}
			items.Add(array[i]);
		}
		if (num >= array.Length)
		{
			num = array.Length - 1;
		}
		else if (num < 0)
		{
			num = 0;
		}
		EnumValue.SelectedIndex = num;
		if (!Initializing && EnumListChanged != null)
		{
			EnumListChanged(array);
		}
	}
}
