using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class ColorRenderingHelper
{
	private static Bitmap m_gradientBitmap;

	private ColorRenderingHelper()
	{
	}

	public static void DrawGreenColorField(Graphics g, int green)
	{
		int num = 255;
		for (int num2 = 0; num2 < 256; num2++)
		{
			Rectangle rect = new Rectangle(0, num2, 256, 1);
			using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(num, green, 0), Color.FromArgb(num, green, 255), LinearGradientMode.Horizontal))
			{
				g.FillRectangle(brush, rect);
			}
			num--;
		}
	}

	public static Bitmap GetGreenColorField(int green)
	{
		Bitmap bitmap = new Bitmap(256, 256);
		using Graphics graphics = Graphics.FromImage(bitmap);
		DrawGreenColorField(graphics, green);
		return bitmap;
	}

	public static void DrawBlueColorField(Graphics g, int blue)
	{
		int num = 255;
		for (int num2 = 0; num2 < 256; num2++)
		{
			Rectangle rect = new Rectangle(0, num2, 256, 1);
			using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0, num, blue), Color.FromArgb(255, num, blue), LinearGradientMode.Horizontal))
			{
				g.FillRectangle(brush, rect);
			}
			num--;
		}
	}

	public static Bitmap GetBlueColorField(int blue)
	{
		Bitmap bitmap = new Bitmap(256, 256);
		using Graphics graphics = Graphics.FromImage(bitmap);
		DrawBlueColorField(graphics, blue);
		return bitmap;
	}

	public static void DrawRedColorField(Graphics g, int red)
	{
		int num = 255;
		for (int num2 = 0; num2 < 256; num2++)
		{
			Rectangle rect = new Rectangle(0, num2, 256, 1);
			using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(red, num, 0), Color.FromArgb(red, num, 255), 0f, isAngleScaleable: false))
			{
				g.FillRectangle(brush, new Rectangle(0, num2, 256, 1));
			}
			num--;
		}
	}

	public static Bitmap GetRedColorField(int red)
	{
		Bitmap bitmap = new Bitmap(256, 256);
		using Graphics graphics = Graphics.FromImage(bitmap);
		DrawRedColorField(graphics, red);
		return bitmap;
	}

	public static void DrawHueColorField(Graphics g, Color sliderColor)
	{
		Color color = sliderColor;
		double num = (double)(255 - color.R) / 255.0;
		double num2 = (double)(255 - color.B) / 255.0;
		double num3 = (double)(255 - color.G) / 255.0;
		double num4 = 255.0;
		double num5 = 255.0;
		double num6 = 255.0;
		for (int num7 = 0; num7 < 256; num7++)
		{
			using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(num7, 0, 1, 256), Color.FromArgb((int)Math.Round(num4), (int)Math.Round(num5), (int)Math.Round(num6)), Color.FromArgb(0, 0, 0), 90f, isAngleScaleable: false))
			{
				g.FillRectangle(brush, new Rectangle(num7, 0, 1, 256));
			}
			num4 -= num;
			num5 -= num3;
			num6 -= num2;
		}
	}

	public static Bitmap GetHueColorField(Color color)
	{
		Bitmap bitmap = new Bitmap(256, 256);
		using Graphics graphics = Graphics.FromImage(bitmap);
		DrawHueColorField(graphics, color);
		return bitmap;
	}

	public static void DrawSaturationColorField(Graphics g, int saturation)
	{
		Rectangle rect = new Rectangle(0, 0, 256, 256);
		int alpha = (int)(255.0 - Math.Round(255.0 * ((double)saturation / 100.0)));
		if (m_gradientBitmap == null)
		{
			m_gradientBitmap = DrawMultiGradientBitmap();
		}
		g.DrawImage(m_gradientBitmap, 0, 0);
		Color color = Color.FromArgb(alpha, 255, 255, 255);
		using LinearGradientBrush brush = new LinearGradientBrush(rect, color, Color.Black, 90f);
		g.FillRectangle(brush, rect);
	}

	public static Bitmap GetSaturationColorField(int saturation)
	{
		Bitmap bitmap = new Bitmap(256, 256);
		using Graphics graphics = Graphics.FromImage(bitmap);
		DrawSaturationColorField(graphics, saturation);
		return bitmap;
	}

	public static void DrawBrightnessColorField(Graphics g, int brightness)
	{
		int alpha = (int)(255.0 - Math.Round((double)brightness * 2.55));
		if (m_gradientBitmap == null)
		{
			m_gradientBitmap = DrawMultiGradientBitmap();
		}
		g.DrawImage(m_gradientBitmap, 0, 0);
		using SolidBrush brush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
		g.FillRectangle(brush, 0, 0, 256, 256);
	}

	public static Bitmap GetBrightnessColorField(int brightness)
	{
		Bitmap bitmap = new Bitmap(256, 256);
		using Graphics graphics = Graphics.FromImage(bitmap);
		DrawBrightnessColorField(graphics, brightness);
		return bitmap;
	}

	private static Bitmap DrawMultiGradientBitmap()
	{
		int num = 255;
		Bitmap bitmap = new Bitmap(256, 256);
		using Graphics graphics = Graphics.FromImage(bitmap);
		for (int num2 = 0; num2 < 256; num2++)
		{
			Rectangle rect = new Rectangle(0, num2, 256, 1);
			using LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.Blue, Color.Red, 0f, isAngleScaleable: false);
			Color[] colors = new Color[7]
			{
				Color.FromArgb(num, num2, num2),
				Color.FromArgb(num, num, num2),
				Color.FromArgb(num2, num, num2),
				Color.FromArgb(num2, num, num),
				Color.FromArgb(num2, num2, num),
				Color.FromArgb(num, num2, num),
				Color.FromArgb(num, num2, num2)
			};
			float[] positions = new float[7] { 0f, 0.1667f, 0.3372f, 0.502f, 0.6686f, 0.8313f, 1f };
			ColorBlend colorBlend = new ColorBlend();
			colorBlend.Colors = colors;
			colorBlend.Positions = positions;
			linearGradientBrush.InterpolationColors = colorBlend;
			graphics.FillRectangle(linearGradientBrush, rect);
		}
		return bitmap;
	}
}
