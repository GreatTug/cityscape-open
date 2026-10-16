namespace Sano.PersonalProjects.ColorPicker.Controls;

public sealed class RGB : IColorSpaceStructure
{
	private int m_red;

	private int m_green;

	private int m_blue;

	public int Red
	{
		get
		{
			return m_red;
		}
		set
		{
			m_red = value;
		}
	}

	public int Green
	{
		get
		{
			return m_green;
		}
		set
		{
			m_green = value;
		}
	}

	public int Blue
	{
		get
		{
			return m_blue;
		}
		set
		{
			m_blue = value;
		}
	}

	public RGB(int red, int green, int blue)
	{
		m_red = red;
		m_green = green;
		m_blue = blue;
	}

	public override string ToString()
	{
		return $"Red: {Red}; Green: {Green}; Blue: {Blue} ";
	}

	public override bool Equals(object obj)
	{
		bool result = false;
		if (obj is RGB)
		{
			RGB rGB = (RGB)obj;
			if (Red == rGB.Red && Blue == rGB.Blue && Green == rGB.Green)
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
