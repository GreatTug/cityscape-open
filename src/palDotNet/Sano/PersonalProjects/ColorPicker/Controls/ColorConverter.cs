using System;
using System.Drawing;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class ColorConverter
{
	private ColorConverter()
	{
	}

	internal static RGB ColorToRgb(Color color)
	{
		return new RGB(color.R, color.G, color.B);
	}

	internal static Color RgbToColor(RGB rgb)
	{
		return Color.FromArgb(rgb.Red, rgb.Green, rgb.Blue);
	}

	internal static HSB RgbToHsb(RGB rgb)
	{
		double num = (double)rgb.Red / 255.0;
		double num2 = (double)rgb.Green / 255.0;
		double num3 = (double)rgb.Blue / 255.0;
		double minimumValue = GetMinimumValue(num, num2, num3);
		double maximumValue = GetMaximumValue(num, num2, num3);
		double num4 = maximumValue - minimumValue;
		double num5 = 0.0;
		double num6 = 0.0;
		double a = maximumValue * 100.0;
		if (maximumValue == 0.0 || num4 == 0.0)
		{
			num5 = 0.0;
			num6 = 0.0;
		}
		else
		{
			num6 = ((minimumValue != 0.0) ? (num4 / maximumValue * 100.0) : 100.0);
			if (Math.Abs(num - maximumValue) < double.Epsilon)
			{
				num5 = (num2 - num3) / num4;
			}
			else if (Math.Abs(num2 - maximumValue) < double.Epsilon)
			{
				num5 = 2.0 + (num3 - num) / num4;
			}
			else if (Math.Abs(num3 - maximumValue) < double.Epsilon)
			{
				num5 = 4.0 + (num - num2) / num4;
			}
		}
		num5 *= 60.0;
		if (num5 < 0.0)
		{
			num5 += 360.0;
		}
		return new HSB((int)Math.Round(num5), (int)Math.Round(num6), (int)Math.Round(a));
	}

	internal static RGB HsbToRgb(HSB hsb)
	{
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		double num4 = hsb.Hue;
		double num5 = (double)hsb.Saturation / 100.0;
		double num6 = (double)hsb.Brightness / 100.0;
		if (num5 == 0.0)
		{
			num = num6;
			num2 = num6;
			num3 = num6;
		}
		else
		{
			double num7 = num4 / 60.0;
			int num8 = (int)Math.Floor(num7);
			double num9 = num7 - (double)num8;
			double num10 = num6 * (1.0 - num5);
			double num11 = num6 * (1.0 - num5 * num9);
			double num12 = num6 * (1.0 - num5 * (1.0 - num9));
			switch (num8)
			{
			case 0:
				num = num6;
				num2 = num12;
				num3 = num10;
				break;
			case 1:
				num = num11;
				num2 = num6;
				num3 = num10;
				break;
			case 2:
				num = num10;
				num2 = num6;
				num3 = num12;
				break;
			case 3:
				num = num10;
				num2 = num11;
				num3 = num6;
				break;
			case 4:
				num = num12;
				num2 = num10;
				num3 = num6;
				break;
			case 5:
				num = num6;
				num2 = num10;
				num3 = num11;
				break;
			}
		}
		int red = (int)Math.Round(num * 255.0);
		int green = (int)Math.Round(num2 * 255.0);
		int blue = (int)Math.Round(num3 * 255.0);
		return new RGB(red, green, blue);
	}

	internal static double GetMaximumValue(params double[] values)
	{
		double num = values[0];
		if (values.Length >= 2)
		{
			for (int i = 1; i < values.Length; i++)
			{
				double val = values[i];
				num = Math.Max(num, val);
			}
		}
		return num;
	}

	internal static double GetMinimumValue(params double[] values)
	{
		double num = values[0];
		if (values.Length >= 2)
		{
			for (int i = 1; i < values.Length; i++)
			{
				double val = values[i];
				num = Math.Min(num, val);
			}
		}
		return num;
	}

	internal static Color HexToColor(string hexValue)
	{
		string arg = hexValue;
		if ((hexValue == null) | (hexValue.Length == 0))
		{
			arg = "0";
		}
		arg = $"0x{arg}";
		int num = Convert.ToInt32(arg, 16);
		return Color.FromArgb((num & 0xFF0000) >> 16, (num & 0xFF00) >> 8, num & 0xFF);
	}

	internal static RGB HexToRgb(string hexValue)
	{
		Color color = HexToColor(hexValue);
		return new RGB(color.R, color.G, color.B);
	}
}
