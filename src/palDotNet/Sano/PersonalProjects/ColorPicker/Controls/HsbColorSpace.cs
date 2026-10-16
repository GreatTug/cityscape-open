using System.Drawing;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class HsbColorSpace : ColorSpace
{
	private ColorSpaceComponent m_csBrightness;

	private ColorSpaceComponent m_csSaturation;

	private ColorSpaceComponent m_csHue;

	internal sealed override IColorSpaceStructure Structure
	{
		get
		{
			return new HSB(m_csHue.Value, m_csSaturation.Value, m_csBrightness.Value);
		}
		set
		{
			HSB hSB = (HSB)value;
			m_csHue.Value = hSB.Hue;
			m_csSaturation.Value = hSB.Saturation;
			m_csBrightness.Value = hSB.Brightness;
		}
	}

	public HsbColorSpace()
	{
		InitializeComponent();
		base.ColorSpaceComponents.Add(m_csHue);
		base.ColorSpaceComponents.Add(m_csSaturation);
		base.ColorSpaceComponents.Add(m_csBrightness);
	}

	internal override void SetDefaultSelection()
	{
		ChangeCurrentlySelectedComponent(m_csHue);
	}

	protected override void UpdateValues(IColorSpaceStructure m_csStructure)
	{
		HSB hSB = (HSB)m_csStructure;
		m_csHue.Value = hSB.Hue;
		m_csSaturation.Value = hSB.Saturation;
		m_csBrightness.Value = hSB.Brightness;
	}

	internal override Color GetColor()
	{
		RGB rGB = ColorConverter.HsbToRgb(new HSB(m_csHue.Value, m_csSaturation.Value, m_csBrightness.Value));
		return Color.FromArgb(rGB.Red, rGB.Green, rGB.Blue);
	}

	private void InitializeComponent()
	{
		this.m_csBrightness = new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponent();
		this.m_csSaturation = new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponent();
		this.m_csHue = new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponent();
		base.SuspendLayout();
		this.m_csBrightness.DisplayCharacter = 'B';
		this.m_csBrightness.Location = new System.Drawing.Point(8, 56);
		this.m_csBrightness.Name = "m_csBrightness";
		this.m_csBrightness.Size = new System.Drawing.Size(96, 32);
		this.m_csBrightness.TabIndex = 2;
		this.m_csBrightness.Unit = Sano.PersonalProjects.ColorPicker.Controls.ComponentUnit.Percentage;
		this.m_csBrightness.Value = 0;
		this.m_csBrightness.MaximumValue = 100;
		this.m_csBrightness.ComponentTextKeyUp += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentTextKeyUp);
		this.m_csBrightness.ComponentSelected += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentSelected);
		this.m_csSaturation.DisplayCharacter = 'S';
		this.m_csSaturation.Location = new System.Drawing.Point(8, 32);
		this.m_csSaturation.Name = "m_csSaturation";
		this.m_csSaturation.Size = new System.Drawing.Size(96, 32);
		this.m_csSaturation.TabIndex = 1;
		this.m_csSaturation.Unit = Sano.PersonalProjects.ColorPicker.Controls.ComponentUnit.Percentage;
		this.m_csSaturation.Value = 0;
		this.m_csSaturation.MaximumValue = 100;
		this.m_csSaturation.ComponentTextKeyUp += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentTextKeyUp);
		this.m_csSaturation.ComponentSelected += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentSelected);
		this.m_csHue.DisplayCharacter = 'H';
		this.m_csHue.Location = new System.Drawing.Point(8, 8);
		this.m_csHue.Name = "m_csHue";
		this.m_csHue.Size = new System.Drawing.Size(96, 32);
		this.m_csHue.TabIndex = 0;
		this.m_csHue.Unit = Sano.PersonalProjects.ColorPicker.Controls.ComponentUnit.Degree;
		this.m_csHue.Value = 0;
		this.m_csHue.MaximumValue = 360;
		this.m_csHue.ComponentTextKeyUp += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentTextKeyUp);
		this.m_csHue.ComponentSelected += new Sano.PersonalProjects.ColorPicker.Controls.ColorSpaceComponentEventHandler(base.ComponentSelected);
		base.Controls.Add(this.m_csBrightness);
		base.Controls.Add(this.m_csSaturation);
		base.Controls.Add(this.m_csHue);
		base.Name = "HsbColorSpace";
		base.Size = new System.Drawing.Size(112, 96);
		base.ResumeLayout(false);
	}
}
