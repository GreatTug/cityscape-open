using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class ColorSpaceComponent : UserControl
{
	private ColorSpaceComponentTextBox txtComponentValue;

	private Label lblComponent;

	private Label lblComponentUnit;

	private RadioButton rdoComponent;

	private char m_displayCharacter;

	private ComponentUnit m_unit = ComponentUnit.Value;

	private int m_value;

	private int m_minimumValue;

	private int m_maximumValue = 255;

	private string m_name = string.Empty;

	public bool Selected
	{
		get
		{
			return rdoComponent.Checked;
		}
		set
		{
			rdoComponent.Checked = value;
		}
	}

	public bool RadioButtonVisible
	{
		get
		{
			return rdoComponent.Visible;
		}
		set
		{
			rdoComponent.Visible = value;
			lblComponent.Visible = !value;
		}
	}

	public bool ReadOnly
	{
		get
		{
			return txtComponentValue.ReadOnly;
		}
		set
		{
			txtComponentValue.ReadOnly = value;
			txtComponentValue.BackColor = Color.White;
		}
	}

	public ComponentUnit Unit
	{
		get
		{
			return m_unit;
		}
		set
		{
			m_unit = value;
			switch (m_unit)
			{
			case ComponentUnit.Degree:
				lblComponentUnit.Text = "°";
				break;
			case ComponentUnit.Percentage:
				lblComponentUnit.Text = "%";
				break;
			default:
				lblComponentUnit.Text = string.Empty;
				break;
			}
		}
	}

	public int Value
	{
		get
		{
			int num;
			if (txtComponentValue.Text.Length == 0)
			{
				num = 0;
			}
			else
			{
				num = int.Parse(txtComponentValue.Text);
				if (num > MaximumValue)
				{
					num = int.Parse(txtComponentValue.Text.Substring(0, txtComponentValue.Text.Length - 1));
				}
			}
			return num;
		}
		set
		{
			m_value = value;
			txtComponentValue.Text = m_value.ToString();
			txtComponentValue.SelectionStart = txtComponentValue.Text.Length;
		}
	}

	[DefaultValue(0)]
	public int MinimumValue
	{
		get
		{
			return m_minimumValue;
		}
		set
		{
			m_minimumValue = value;
		}
	}

	[DefaultValue(255)]
	public int MaximumValue
	{
		get
		{
			return m_maximumValue;
		}
		set
		{
			m_maximumValue = value;
		}
	}

	public char DisplayCharacter
	{
		get
		{
			return m_displayCharacter;
		}
		set
		{
			m_displayCharacter = value;
			rdoComponent.Text = $"{m_displayCharacter.ToString().ToUpper()}:";
			lblComponent.Text = $"{m_displayCharacter.ToString().ToUpper()}:";
		}
	}

	internal event ColorSpaceComponentEventHandler ComponentSelected;

	internal event ColorSpaceComponentEventHandler ComponentTextKeyUp;

	internal ColorSpaceComponent()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		this.lblComponentUnit = new System.Windows.Forms.Label();
		this.txtComponentValue = new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentTextBox();
		this.rdoComponent = new System.Windows.Forms.RadioButton();
		this.lblComponent = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.lblComponentUnit.Location = new System.Drawing.Point(80, 6);
		this.lblComponentUnit.Name = "lblComponentUnit";
		this.lblComponentUnit.Size = new System.Drawing.Size(16, 16);
		this.lblComponentUnit.TabIndex = 49;
		this.lblComponentUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtComponentValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtComponentValue.Location = new System.Drawing.Point(40, 4);
		this.txtComponentValue.MaxLength = 3;
		this.txtComponentValue.Name = "txtComponentValue";
		this.txtComponentValue.Size = new System.Drawing.Size(40, 20);
		this.txtComponentValue.TabIndex = 0;
		this.txtComponentValue.Text = "";
		this.txtComponentValue.KeyDown += new System.Windows.Forms.KeyEventHandler(txtComponentValue_KeyDown);
		this.txtComponentValue.LostFocus += new System.EventHandler(txtComponentValue_LostFocus);
		this.txtComponentValue.KeyUp += new System.Windows.Forms.KeyEventHandler(txtComponentValue_KeyUp);
		this.txtComponentValue.ContextMenu = new System.Windows.Forms.ContextMenu(null);
		this.rdoComponent.Location = new System.Drawing.Point(4, 2);
		this.rdoComponent.Name = "rdoComponent";
		this.rdoComponent.Size = new System.Drawing.Size(36, 24);
		this.rdoComponent.TabIndex = 50;
		this.rdoComponent.Click += new System.EventHandler(rdoComponent_Click);
		this.lblComponent.Location = new System.Drawing.Point(18, 2);
		this.lblComponent.Name = "lblComponent";
		this.lblComponent.Size = new System.Drawing.Size(20, 24);
		this.lblComponent.TabIndex = 51;
		this.lblComponent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblComponent.Visible = false;
		base.Controls.Add(this.lblComponent);
		base.Controls.Add(this.lblComponentUnit);
		base.Controls.Add(this.txtComponentValue);
		base.Controls.Add(this.rdoComponent);
		base.Name = "ColorSpaceComponent";
		base.Size = new System.Drawing.Size(112, 32);
		base.ResumeLayout(false);
	}

	private void rdoComponent_Click(object sender, EventArgs e)
	{
		RadioButton radioButton = (RadioButton)sender;
		if (radioButton.Checked)
		{
			OnComponentSelected(EventArgs.Empty);
		}
	}

	private void txtComponentValue_LostFocus(object sender, EventArgs e)
	{
		ColorSpaceComponentTextBox colorSpaceComponentTextBox = (ColorSpaceComponentTextBox)sender;
		bool flag = true;
		int num2;
		if (colorSpaceComponentTextBox.Text != null && colorSpaceComponentTextBox.Text.Length != 0)
		{
			int num = int.Parse(colorSpaceComponentTextBox.Text);
			num2 = num;
			if (num > MaximumValue)
			{
				num2 = MaximumValue;
			}
			else if (num < MinimumValue)
			{
				num2 = MinimumValue;
			}
			else
			{
				flag = false;
			}
		}
		else
		{
			num2 = MinimumValue;
		}
		if (flag)
		{
			MessageBox.Show(this, $"An integer between {MinimumValue} and {MaximumValue} is required. Closest value inserted.", "Color Picker", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		colorSpaceComponentTextBox.Text = num2.ToString();
	}

	protected virtual void OnComponentSelected(EventArgs e)
	{
		if (ComponentSelected != null)
		{
			ComponentSelected(this, EventArgs.Empty);
		}
	}

	protected virtual void OnComponentTextKeyUp(EventArgs e)
	{
		if (ComponentTextKeyUp != null)
		{
			ComponentTextKeyUp(this, EventArgs.Empty);
		}
	}

	private void txtComponentValue_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyData != Keys.Tab)
		{
			OnComponentTextKeyUp(EventArgs.Empty);
		}
	}

	private void txtComponentValue_KeyDown(object sender, KeyEventArgs e)
	{
		if (txtComponentValue.Text.Length > 0 && ((e.KeyData | Keys.Shift) == (Keys.Up | Keys.Shift) || (e.KeyData | Keys.Shift) == (Keys.Down | Keys.Shift)))
		{
			int num = short.Parse(txtComponentValue.Text);
			int num2 = 0;
			num2 = (((e.KeyData & Keys.Shift) != Keys.Shift) ? 1 : 10);
			if ((e.KeyData & Keys.Up) == Keys.Up)
			{
				num = ((num + num2 > MaximumValue) ? MaximumValue : (num + num2));
			}
			else if ((e.KeyData | Keys.Shift) == (Keys.Down | Keys.Shift))
			{
				num2 = ((e.KeyData != (Keys.Down | Keys.Shift)) ? (-1) : (-10));
				num = ((num + num2 <= MinimumValue) ? MinimumValue : (num + num2));
			}
			txtComponentValue.Text = num.ToString();
			ComponentTextKeyUp(this, EventArgs.Empty);
			if (e.KeyData == (Keys.Down | Keys.Shift))
			{
				txtComponentValue.SelectionStart = txtComponentValue.Text.Length;
			}
		}
	}
}
