namespace Sano.PersonalProjects.ColorPicker.Controls;

internal sealed class HSB : IColorSpaceStructure
{
	private int m_hue;

	private int m_saturation;

	private int m_brightness;

	internal int Hue
	{
		get
		{
			return m_hue;
		}
		set
		{
			m_hue = value;
		}
	}

	internal int Saturation
	{
		get
		{
			return m_saturation;
		}
		set
		{
			m_saturation = value;
		}
	}

	internal int Brightness
	{
		get
		{
			return m_brightness;
		}
		set
		{
			m_brightness = value;
		}
	}

	internal HSB(int hue, int saturation, int brightness)
	{
		m_hue = hue;
		m_saturation = saturation;
		m_brightness = brightness;
	}

	public override string ToString()
	{
		return $"Hue: {Hue}; Saturation: {Saturation}; Brightness: {Brightness} ";
	}

	public override bool Equals(object obj)
	{
		bool result = false;
		if (obj is HSB)
		{
			HSB hSB = (HSB)obj;
			if (Hue == hSB.Hue && Saturation == hSB.Saturation && Brightness == hSB.Brightness)
			{
				result = true;
			}
		}
		return result;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
