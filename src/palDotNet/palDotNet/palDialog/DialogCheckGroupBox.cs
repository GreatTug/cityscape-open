using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace palDotNet.palDialog;

public class DialogCheckGroupBox : GroupBox, IDialogValue
{
	private const int CHECKBOX_X_OFFSET = 10;

	private const int CHECKBOX_Y_OFFSET = 0;

	private IContainer components;

	private CheckBox m_checkBox;

	private bool m_bDisableChildrenIfUnchecked;

	[ReadOnly(true)]
	[DialogValueType(EnumDialogValueTypes.eBoolean)]
	public object DialogValue
	{
		get
		{
			return Checked;
		}
		set
		{
			Checked = Convert.ToBoolean(value);
		}
	}

	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	public override string Text
	{
		get
		{
			if (Site != null && Site.DesignMode)
			{
				return m_checkBox.Text;
			}
			return GetFakeGroupBoxText();
		}
		set
		{
			base.Text = GetFakeGroupBoxText();
			m_checkBox.Text = value;
		}
	}

	[DefaultValue(true)]
	[Description("Indicates whether the component is checked or not.")]
	[Category("Appearance")]
	public bool Checked
	{
		get
		{
			return m_checkBox.Checked;
		}
		set
		{
			if (m_checkBox.Checked != value)
			{
				m_checkBox.Checked = value;
			}
		}
	}

	[DefaultValue(CheckState.Checked)]
	[Category("Appearance")]
	[Description("Indicates the state of the component.")]
	public CheckState CheckState
	{
		get
		{
			return m_checkBox.CheckState;
		}
		set
		{
			if (m_checkBox.CheckState != value)
			{
				m_checkBox.CheckState = value;
			}
		}
	}

	[Description("Determines if child controls of the GroupBox are disabled when the CheckBox is unchecked.")]
	[Category("Appearance")]
	[DefaultValue(true)]
	public bool DisableChildrenIfUnchecked
	{
		get
		{
			return m_bDisableChildrenIfUnchecked;
		}
		set
		{
			if (m_bDisableChildrenIfUnchecked != value)
			{
				m_bDisableChildrenIfUnchecked = value;
			}
		}
	}

	[Description("Occurs whenever the Checked property of the CheckBox is changed.")]
	public event EventHandler CheckedChanged;

	[Description("Occurs whenever the CheckState property of the CheckBox is changed.")]
	public event EventHandler CheckStateChanged;

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
		this.m_checkBox = new System.Windows.Forms.CheckBox();
		base.SuspendLayout();
		this.m_checkBox.AutoSize = true;
		this.m_checkBox.Checked = true;
		this.m_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
		this.m_checkBox.Location = new System.Drawing.Point(0, 0);
		this.m_checkBox.Name = "m_checkBox";
		this.m_checkBox.Size = new System.Drawing.Size(104, 24);
		this.m_checkBox.TabIndex = 0;
		this.m_checkBox.Text = "checkBox";
		this.m_checkBox.UseVisualStyleBackColor = true;
		this.m_checkBox.CheckStateChanged += new System.EventHandler(checkBox_CheckStateChanged);
		this.m_checkBox.CheckedChanged += new System.EventHandler(checkBox_CheckedChanged);
		base.ControlAdded += new System.Windows.Forms.ControlEventHandler(CheckGroupBox_ControlAdded);
		base.ResumeLayout(false);
	}

	public DialogCheckGroupBox()
	{
		SaveToConfig = true;
		InitializeComponent();
		m_bDisableChildrenIfUnchecked = true;
		m_checkBox.Parent = this;
		m_checkBox.Location = new Point(10, 0);
		Checked = true;
		if (VisualStyleInformation.IsEnabledByUser)
		{
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal);
			Color color = visualStyleRenderer.GetColor(ColorProperty.TextColor);
			m_checkBox.ForeColor = color;
		}
	}

	private string GetFakeGroupBoxText()
	{
		float num = m_checkBox.Width;
		Graphics graphics = CreateGraphics();
		float num2 = (float)Math.Floor(graphics.MeasureString(" ", m_checkBox.Font).Width);
		int num3 = (int)(num / num2) - 2;
		string text = " ";
		for (int i = 0; i < num3; i++)
		{
			text += " ";
		}
		return text;
	}

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
		string caption = "script command: " + Environment.NewLine + "'" + dialogName + " -" + varName + " <true|false>'";
		toolTip.SetToolTip(this, caption);
	}

	protected virtual void OnCheckedChanged(EventArgs e)
	{
	}

	protected virtual void OnCheckStateChanged(EventArgs e)
	{
	}

	private void checkBox_CheckedChanged(object sender, EventArgs e)
	{
		if (m_bDisableChildrenIfUnchecked)
		{
			bool enabled = m_checkBox.Checked;
			foreach (Control control in base.Controls)
			{
				if (control != m_checkBox)
				{
					control.Enabled = enabled;
				}
			}
		}
		if (CheckedChanged != null)
		{
			CheckedChanged(sender, e);
		}
	}

	private void checkBox_CheckStateChanged(object sender, EventArgs e)
	{
		if (CheckStateChanged != null)
		{
			CheckStateChanged(sender, e);
		}
	}

	private void CheckGroupBox_ControlAdded(object sender, ControlEventArgs e)
	{
		if (m_bDisableChildrenIfUnchecked)
		{
			e.Control.Enabled = Checked;
		}
	}
}
