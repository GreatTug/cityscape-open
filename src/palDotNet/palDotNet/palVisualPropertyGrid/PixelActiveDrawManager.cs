using System;
using System.Drawing;
using System.Windows.Forms;
using VisualHint.SmartPropertyGrid;

namespace palDotNet.palVisualPropertyGrid;

public class PixelActiveDrawManager : DotnetDrawManager
{
	private bool _drawSubCategoryBackground = true;

	public override bool ShowCategoryVerticalLine => false;

	public override void DrawButtonsBackground(Graphics graphics, Rectangle rect)
	{
		SolidBrush solidBrush = new SolidBrush(base.Grid.BackColor);
		graphics.FillRectangle(solidBrush, rect);
		solidBrush.Dispose();
	}

	public override void DrawCommentsBackground(Graphics graphics, Rectangle commentRect)
	{
		Brush brush = new SolidBrush(base.Grid.CommentsBackColor);
		graphics.FillRectangle(brush, commentRect);
		commentRect.Width--;
		commentRect.Height--;
		graphics.DrawRectangle(SystemPens.FromSystemColor(SystemColors.GrayText), commentRect);
		brush.Dispose();
	}

	public override void DrawCommentsGap(Graphics graphics, Rectangle rect)
	{
		SolidBrush solidBrush = new SolidBrush(base.Grid.BackColor);
		graphics.FillRectangle(solidBrush, rect);
		solidBrush.Dispose();
	}

	public override void DrawGridBackground(Graphics graphics, Rectangle rect)
	{
		Rectangle rect2 = Rectangle.Intersect(rect, Rectangle.Round(graphics.ClipBounds));
		if (!rect2.IsEmpty)
		{
			Brush brush = new SolidBrush(base.Grid.GridBackColor);
			graphics.FillRectangle(brush, rect2);
			brush.Dispose();
		}
	}

	public override void DrawLeftColumnBackground(Graphics graphics, Rectangle rect, PropertyEnumerator propEnum)
	{
		Rectangle rect2 = Rectangle.Intersect(rect, Rectangle.Round(graphics.ClipBounds));
		if (!rect2.IsEmpty)
		{
			SolidBrush solidBrush = new SolidBrush(base.Grid.GridColor);
			graphics.FillRectangle(solidBrush, rect2);
			solidBrush.Dispose();
		}
	}

	public override void NcCalcSize(ref Rectangle rect)
	{
		rect.Inflate(-1, -1);
	}

	public override void DrawGridNonClientArea(Graphics graphics, Rectangle rect)
	{
		rect.Width--;
		rect.Height--;
		Pen pen = new Pen(Color.FromArgb(255, 46, 46, 46));
		graphics.DrawRectangle(pen, rect);
		pen.Dispose();
	}

	public override void DrawCategoryLabelBackground(Graphics graphics, Rectangle rect, PropertyEnumerator enumSelf)
	{
		Rectangle rect2 = Rectangle.Intersect(rect, Rectangle.Round(graphics.ClipBounds));
		if (!rect2.IsEmpty)
		{
			Brush brush;
			if (base.DrawSelectedStateAsFocusRect)
			{
				brush = new SolidBrush(base.Grid.GridColor);
			}
			else if (enumSelf.Property.Selected)
			{
				Color color = ((!base.Grid.GridContainsFocus) ? base.Grid.SelectedNotFocusedBackColor : base.Grid.SelectedBackColor);
				brush = new SolidBrush(color);
			}
			else
			{
				brush = new SolidBrush(base.Grid.GridColor);
			}
			graphics.FillRectangle(brush, rect2);
			brush.Dispose();
		}
	}

	public override void DrawCategoryLabelText(Graphics graphics, Rectangle rect, Color textColor, PropertyEnumerator enumSelf)
	{
		Font font = new Font(base.Grid.Font, FontStyle.Bold);
		if (base.DrawSelectedStateAsFocusRect)
		{
			textColor = base.Grid.HighlightedTextColor;
		}
		Win32Calls.DrawText(graphics, enumSelf.Property.DisplayName, ref rect, font, textColor, 0x824 | (((base.Grid.EllipsisMode & VisualHint.SmartPropertyGrid.PropertyGrid.EllipsisModes.EllipsisOnLabels) != VisualHint.SmartPropertyGrid.PropertyGrid.EllipsisModes.None) ? 32768 : 0));
		if (base.DrawSelectedStateAsFocusRect && enumSelf.Property.Selected && base.Grid.GridContainsFocus)
		{
			Size textExtent = Win32Calls.GetTextExtent(graphics, enumSelf.Property.DisplayName, font);
			Rectangle rectangle = rect;
			rectangle.Y++;
			rectangle.Height -= 2;
			rectangle.X -= VisualHint.SmartPropertyGrid.PropertyGrid.GlobalTextMargin / 2 + 1;
			rectangle.Width = VisualHint.SmartPropertyGrid.PropertyGrid.GlobalTextMargin + textExtent.Width;
			if (rectangle.Right > rect.Right)
			{
				rectangle.Width = rect.Right - rectangle.X;
			}
			ControlPaint.DrawFocusRectangle(graphics, rectangle);
		}
	}

	public override void DrawSubCategoryLabelText(Graphics graphics, Rectangle rect, Color textColor, PropertyEnumerator enumSelf)
	{
		Property property = enumSelf.Property;
		if (base.DrawSelectedStateAsFocusRect)
		{
			textColor = enumSelf.Property.ForeColor;
		}
		Win32Calls.DrawText(graphics, property.DisplayName, ref rect, base.Grid.Font, textColor, 34852);
		if (base.DrawSelectedStateAsFocusRect && enumSelf.Property.Selected && base.Grid.GridContainsFocus)
		{
			Size textExtent = Win32Calls.GetTextExtent(graphics, enumSelf.Property.DisplayName, base.Grid.Font);
			Rectangle rectangle = rect;
			rectangle.Y++;
			rectangle.Height -= 2;
			rectangle.X -= VisualHint.SmartPropertyGrid.PropertyGrid.GlobalTextMargin / 2 + 1;
			rectangle.Width = VisualHint.SmartPropertyGrid.PropertyGrid.GlobalTextMargin + textExtent.Width;
			if (rectangle.Right > rect.Right)
			{
				rectangle.Width = rect.Right - rectangle.X;
			}
			ControlPaint.DrawFocusRectangle(graphics, rectangle);
		}
	}

	public override void DrawSubCategoryLabelBackground(Graphics graphics, int labelRectX, Rectangle rect, PropertyEnumerator enumSelf)
	{
		Rectangle rect2 = Rectangle.Intersect(rect, Rectangle.Round(graphics.ClipBounds));
		if (!rect2.IsEmpty)
		{
			Brush brush;
			if (base.DrawSelectedStateAsFocusRect)
			{
				brush = new SolidBrush(base.Grid.GridColor);
			}
			else if (enumSelf.Property.Selected)
			{
				Color color = ((!base.Grid.GridContainsFocus) ? base.Grid.SelectedNotFocusedBackColor : base.Grid.SelectedBackColor);
				brush = new SolidBrush(color);
			}
			else if (_drawSubCategoryBackground)
			{
				RGBtoHSL(base.Grid.GridColor, out var H, out var S, out var _);
				Color color2 = HSLtoRGB(H, S, 46.0 / 51.0);
				brush = new SolidBrush(color2);
			}
			else
			{
				brush = new SolidBrush(base.Grid.GridColor);
			}
			graphics.FillRectangle(brush, rect2);
			brush.Dispose();
		}
	}

	public override void DrawCategoryValue(Graphics graphics, Rectangle valueRect, Color textColor, PropertyEnumerator enumSelf)
	{
		Font font = new Font(base.Grid.Font, FontStyle.Bold);
		if (base.DrawSelectedStateAsFocusRect)
		{
			textColor = enumSelf.Property.ForeColor;
		}
		Win32Calls.DrawText(graphics, ((RootCategory)enumSelf.Property).ValueText, ref valueRect, font, textColor, 0x824 | (((base.Grid.EllipsisMode & VisualHint.SmartPropertyGrid.PropertyGrid.EllipsisModes.EllipsisOnValues) != VisualHint.SmartPropertyGrid.PropertyGrid.EllipsisModes.None) ? 32768 : 0));
	}

	public override void DrawPlusMinusSign(Graphics graphics, Control control, Rectangle itemRect, PropertyEnumerator propEnum)
	{
		Property property = propEnum.Property;
		Rectangle signRect = property.GetSignRect(itemRect, propEnum);
		if (!Rectangle.Intersect(signRect, Rectangle.Round(graphics.ClipBounds)).IsEmpty)
		{
			base.DrawPlusMinusSign(graphics, control, itemRect, propEnum);
		}
	}

	public static double HueToRGB(double m1, double m2, double h)
	{
		if (h < 0.0)
		{
			h++;
		}
		if (h > 1.0)
		{
			h--;
		}
		if (6.0 * h < 1.0)
		{
			return m1 + (m2 - m1) * h * 6.0;
		}
		if (2.0 * h < 1.0)
		{
			return m2;
		}
		if (3.0 * h < 2.0)
		{
			return m1 + (m2 - m1) * (2.0 / 3.0 - h) * 6.0;
		}
		return m1;
	}

	public static Color HSLtoRGB(double H, double S, double L)
	{
		double num;
		double num2;
		double num3;
		if (S == 0.0)
		{
			num = (num2 = (num3 = L));
		}
		else
		{
			double num4 = ((!(L <= 0.5)) ? (L + S - L * S) : (L * (1.0 + S)));
			double m = 2.0 * L - num4;
			num = HueToRGB(m, num4, H + 1.0 / 3.0);
			num2 = HueToRGB(m, num4, H);
			num3 = HueToRGB(m, num4, H - 1.0 / 3.0);
		}
		return Color.FromArgb((int)Math.Round(num * 255.0), (int)Math.Round(num2 * 255.0), (int)Math.Round(num3 * 255.0));
	}

	public static void RGBtoHSL(Color rgb, out double H, out double S, out double L)
	{
		double num = (double)(int)rgb.R / 255.0;
		double num2 = (double)(int)rgb.G / 255.0;
		double num3 = (double)(int)rgb.B / 255.0;
		double num4 = Math.Max(num, Math.Max(num2, num3));
		double num5 = Math.Min(num, Math.Min(num2, num3));
		L = (num4 + num5) / 2.0;
		if (num4 == num5)
		{
			S = 0.0;
			H = 0.0;
			return;
		}
		double num6 = num4 - num5;
		if (L < 0.5)
		{
			S = num6 / (num4 + num5);
		}
		else
		{
			S = num6 / (2.0 - num4 - num5);
		}
		if (num == num4)
		{
			H = (num2 - num3) / num6;
		}
		else if (num2 == num4)
		{
			H = 2.0 + (num3 - num) / num6;
		}
		else
		{
			H = 4.0 + (num - num2) / num6;
		}
		H /= 6.0;
		if (H < 0.0)
		{
			H++;
		}
	}
}
