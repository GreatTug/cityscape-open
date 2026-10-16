using System.Drawing;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class RgbColorSpace : ColorSpace
{
	private ColorSpaceComponent m_csRed;

	private ColorSpaceComponent m_csGreen;

	private ColorSpaceComponent m_csBlue;

	internal override IColorSpaceStructure Structure
	{
		get
		{
			return new RGB(m_csRed.Value, m_csGreen.Value, m_csBlue.Value);
		}
		set
		{
			RGB rGB = (RGB)value;
			m_csRed.Value = rGB.Red;
			m_csGreen.Value = rGB.Green;
			m_csBlue.Value = rGB.Blue;
		}
	}

	internal RgbColorSpace()
	{
		InitializeComponent();
		base.ColorSpaceComponents.Add(m_csRed);
		base.ColorSpaceComponents.Add(m_csBlue);
		base.ColorSpaceComponents.Add(m_csGreen);
	}

	internal override void SetDefaultSelection()
	{
		ChangeCurrentlySelectedComponent(m_csRed);
	}

	internal override Color GetColor()
	{
		return Color.FromArgb(m_csRed.Value, m_csGreen.Value, m_csBlue.Value);
	}

	internal string ConvertToHex()
	{
		return $"{m_csRed.Value:x2}{m_csGreen.Value:x2}{m_csBlue.Value:x2}".ToUpper();
	}

	protected override void UpdateValues(IColorSpaceStructure csStructure)
	{
		RGB rGB = (RGB)csStructure;
		m_csRed.Value = rGB.Red;
		m_csGreen.Value = rGB.Green;
		m_csBlue.Value = rGB.Blue;
	}

	private void InitializeComponent()
	{
		this.m_csRed = new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponent();
		this.m_csBlue = new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponent();
		this.m_csGreen = new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponent();
		base.SuspendLayout();
		this.m_csRed.DisplayCharacter = 'R';
		this.m_csRed.Location = new System.Drawing.Point(8, 8);
		this.m_csRed.Name = "m_csRed";
		this.m_csRed.Selected = false;
		this.m_csRed.Size = new System.Drawing.Size(96, 32);
		this.m_csRed.TabIndex = 0;
		this.m_csRed.Unit = Sano.PersonalProjects.ColorPicker.Controls.ComponentUnit.Value;
		this.m_csRed.Value = 0;
		this.m_csRed.ComponentTextKeyUp += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentTextKeyUp);
		this.m_csRed.ComponentSelected += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentSelected);
		this.m_csBlue.DisplayCharacter = 'B';
		this.m_csBlue.Location = new System.Drawing.Point(8, 56);
		this.m_csBlue.Name = "m_csBlue";
		this.m_csBlue.Selected = false;
		this.m_csBlue.Size = new System.Drawing.Size(96, 32);
		this.m_csBlue.TabIndex = 2;
		this.m_csBlue.Unit = Sano.PersonalProjects.ColorPicker.Controls.ComponentUnit.Value;
		this.m_csBlue.Value = 0;
		this.m_csBlue.ComponentTextKeyUp += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentTextKeyUp);
		this.m_csBlue.ComponentSelected += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentSelected);
		this.m_csGreen.DisplayCharacter = 'G';
		this.m_csGreen.Location = new System.Drawing.Point(8, 32);
		this.m_csGreen.Name = "m_csGreen";
		this.m_csGreen.Selected = false;
		this.m_csGreen.Size = new System.Drawing.Size(96, 32);
		this.m_csGreen.TabIndex = 1;
		this.m_csGreen.Unit = Sano.PersonalProjects.ColorPicker.Controls.ComponentUnit.Value;
		this.m_csGreen.Value = 0;
		this.m_csGreen.ComponentTextKeyUp += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentTextKeyUp);
		this.m_csGreen.ComponentSelected += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentSelected);
		base.Controls.Add(this.m_csBlue);
		base.Controls.Add(this.m_csGreen);
		base.Controls.Add(this.m_csRed);
		base.Name = "RgbColorSpace";
		base.Size = new System.Drawing.Size(112, 96);
		base.ResumeLayout(false);
	}
}
