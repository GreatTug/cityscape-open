using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Sano.Utility;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class ColorFieldPanel : UserControl
{
	internal delegate void ColorSelectedHandler(object sender, ColorSelectedEventArgs e);

	private const int SAMPLE_CIRCLE_RADIUS = 5;

	private IContainer components;

	private Timer cursorTimer;

	private Bitmap m_currentColorBitmap;

	private bool m_isMouseDown;

	private bool m_mousePressedWithinPanel;

	private char m_component;

	private Color m_color;

	private ColorSpace m_colorSpace;

	private int m_selectedComponentValue;

	private int m_movingPointX;

	private int m_movingPointY;

	private int m_targetPointX;

	private int m_targetPointY;

	private Point m_currentPoint;

	private Point m_tempPoint;

	internal event ColorSelectedHandler ColorSelected;

	public ColorFieldPanel()
	{
		InitializeComponent();
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
		cursorTimer.Enabled = false;
	}

	private void InvalidateNewAndOldRegions(Point oldPoint, Point newPoint)
	{
		Rectangle rc = new Rectangle(oldPoint.X - 5, oldPoint.Y - 5, 10, 10);
		rc.Inflate(4, 4);
		Invalidate(rc);
		Rectangle rc2 = new Rectangle(newPoint.X - 5, newPoint.Y - 5, 10, 10);
		rc2.Inflate(4, 4);
		Invalidate(rc2);
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		UpdateCurrentColorBitmap(resetPreviouslyPickedPoint: false);
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		m_isMouseDown = true;
		m_mousePressedWithinPanel = true;
		Point point = new Point(e.X, e.Y);
		InvalidateNewAndOldRegions(m_currentPoint, point);
		m_currentPoint = point;
		m_tempPoint = m_currentPoint;
		ColorSelected(this, new ColorSelectedEventArgs(CalculateSelectedColor(point)));
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		m_isMouseDown = false;
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (m_isMouseDown && ((e.X >= 0 && e.X <= 255) || (e.Y >= 0 && e.Y <= 255)))
		{
			int num = 0;
			int targetPointX = ((e.X >= 0) ? ((e.X <= 255) ? e.X : 255) : 0);
			num = ((e.Y >= 0) ? ((e.Y <= 255) ? e.Y : 255) : 0);
			m_movingPointX = m_currentPoint.X;
			m_movingPointY = m_currentPoint.Y;
			m_targetPointX = targetPointX;
			m_targetPointY = num;
			Point currentPoint = new Point(targetPointX, num);
			Invalidate();
			m_currentPoint = currentPoint;
			if (!cursorTimer.Enabled)
			{
				cursorTimer.Start();
			}
		}
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		Cursor = new Cursor(Resources.GetFileResource("ColorFieldPanelCursor.cur"));
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		if (cursorTimer.Enabled)
		{
			cursorTimer.Stop();
		}
		Cursor.Show();
		base.ParentForm.Cursor = Cursors.Default;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		if (!base.DesignMode)
		{
			Graphics graphics = e.Graphics;
			graphics.DrawImage(m_currentColorBitmap, DisplayRectangle);
			int num = 0;
			Color pixel = m_currentColorBitmap.GetPixel(m_currentPoint.X, m_currentPoint.Y);
			Point location = new Point(m_currentPoint.X - 5, m_currentPoint.Y - 5);
			int num2 = (pixel.R + pixel.G + pixel.B) / 3;
			num = 10;
			if (num2 > 175)
			{
				graphics.DrawEllipse(Pens.Black, new Rectangle(location, new Size(num, num)));
			}
			else
			{
				graphics.DrawEllipse(Pens.White, new Rectangle(location, new Size(num, num)));
			}
		}
	}

	private void UpdateCurrentColorBitmap(bool resetPreviouslyPickedPoint)
	{
		if (m_isMouseDown)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		if (m_currentColorBitmap != null)
		{
			m_currentColorBitmap.Dispose();
		}
		if (m_colorSpace is HsbColorSpace)
		{
			HSB hSB = (HSB)m_colorSpace.Structure;
			if (m_component == 'H')
			{
				Color color = m_color;
				if (color.Equals(Color.FromArgb(0, 0, 0, 0)) | color.Equals(Color.FromArgb(0, 0, 0)))
				{
					color = Color.FromArgb(255, 0, 0);
				}
				m_currentColorBitmap = ColorRenderingHelper.GetHueColorField(color);
				num = (int)Math.Round((double)hSB.Saturation * 2.55);
				num2 = 255 - (int)Math.Round((double)hSB.Brightness * 2.55);
			}
			else if (m_component == 'S')
			{
				m_currentColorBitmap = ColorRenderingHelper.GetSaturationColorField(m_selectedComponentValue);
				num = (int)Math.Ceiling((double)hSB.Hue * (17.0 / 24.0));
				num2 = (int)(255.0 - (double)hSB.Brightness * 2.55);
			}
			else if (m_component == 'B')
			{
				m_currentColorBitmap = ColorRenderingHelper.GetBrightnessColorField(m_selectedComponentValue);
				num = (int)Math.Ceiling((double)hSB.Hue * (17.0 / 24.0));
				num2 = 255 - (int)Math.Round((double)hSB.Saturation * 2.55);
			}
		}
		else if (m_colorSpace is RgbColorSpace)
		{
			Color color2 = ((RgbColorSpace)m_colorSpace).GetColor();
			if (m_component == 'R')
			{
				m_currentColorBitmap = ColorRenderingHelper.GetRedColorField(m_selectedComponentValue);
				num = color2.B;
				num2 = 255 - color2.G;
			}
			else if (m_component == 'G')
			{
				m_currentColorBitmap = ColorRenderingHelper.GetGreenColorField(m_selectedComponentValue);
				num = color2.B;
				num2 = 255 - color2.R;
			}
			else if (m_component == 'B')
			{
				m_currentColorBitmap = ColorRenderingHelper.GetBlueColorField(m_selectedComponentValue);
				num = color2.R;
				num2 = 255 - color2.G;
			}
		}
		if (resetPreviouslyPickedPoint)
		{
			m_currentPoint = new Point(num, num2);
		}
	}

	public void UpdateColor(ColorSpace colorSpace, char component, Color color, bool resetPreviouslyPickedPoint)
	{
		m_color = color;
		UpdateColor(colorSpace, component, resetPreviouslyPickedPoint);
	}

	public void UpdateColor(ColorSpace colorSpace, char component, int selectedComponentValue, bool resetPreviouslyPickedPoint)
	{
		m_color = Color.Empty;
		m_selectedComponentValue = selectedComponentValue;
		UpdateColor(colorSpace, component, resetPreviouslyPickedPoint);
	}

	private void UpdateColor(ColorSpace colorSpace, char component, bool resetPreviouslyPickedPoint)
	{
		m_colorSpace = colorSpace;
		m_component = component;
		m_mousePressedWithinPanel = resetPreviouslyPickedPoint;
		UpdateCurrentColorBitmap(resetPreviouslyPickedPoint);
		Invalidate();
	}

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
		this.components = new System.ComponentModel.Container();
		this.cursorTimer = new System.Windows.Forms.Timer(this.components);
		this.cursorTimer.Interval = 10;
		this.cursorTimer.Tick += new System.EventHandler(cursorTimer_Tick);
		base.Name = "ColorFieldPanel";
		base.Size = new System.Drawing.Size(256, 256);
	}

	private void cursorTimer_Tick(object sender, EventArgs e)
	{
		if (!m_isMouseDown)
		{
			return;
		}
		int num = m_movingPointX - m_targetPointX;
		int num2 = m_movingPointY - m_targetPointY;
		int num3 = (int)Math.Round((double)num / 2.0);
		int num4 = (int)Math.Round((double)num2 / 2.0);
		bool flag = false;
		if (num3 == 0)
		{
			m_movingPointX = m_targetPointX;
			flag = true;
		}
		else
		{
			m_movingPointX += -num3;
		}
		if (num4 == 0)
		{
			m_movingPointY = m_targetPointY;
			if (flag)
			{
				cursorTimer.Stop();
			}
		}
		else
		{
			m_movingPointY += -num4;
		}
		if (ColorSelected != null)
		{
			ColorSelected(this, new ColorSelectedEventArgs(CalculateSelectedColor(new Point(m_movingPointX, m_movingPointY))));
		}
	}

	private Color CalculateSelectedColor(Point p)
	{
		IColorSpaceStructure colorSpaceStructure = null;
		if (m_colorSpace is HsbColorSpace)
		{
			HSB hSB = (HSB)m_colorSpace.Structure;
			if (m_component == 'H')
			{
				int brightness = (int)((255.0 - (double)p.Y) / 2.55);
				int saturation = (int)((double)p.X / 2.55);
				colorSpaceStructure = new HSB(hSB.Hue, saturation, brightness);
			}
			else if (m_component == 'S')
			{
				int num = (int)((double)p.X * 1.411764705882353);
				int brightness2 = (int)((255.0 - (double)p.Y) / 2.55);
				if (num == 360)
				{
					num = 0;
				}
				colorSpaceStructure = new HSB(num, hSB.Saturation, brightness2);
			}
			else if (m_component == 'B')
			{
				int num2 = (int)((double)p.X * 1.411764705882353);
				int saturation2 = (int)((255.0 - (double)p.Y) / 2.55);
				if (num2 == 360)
				{
					num2 = 0;
				}
				colorSpaceStructure = new HSB(num2, saturation2, hSB.Brightness);
			}
		}
		else if (m_colorSpace is RgbColorSpace)
		{
			RGB rGB = (RGB)m_colorSpace.Structure;
			if (m_component == 'R')
			{
				colorSpaceStructure = new RGB(rGB.Red, 255 - p.Y, p.X);
			}
			else if (m_component == 'G')
			{
				colorSpaceStructure = new RGB(255 - p.Y, rGB.Green, p.X);
			}
			else if (m_component == 'B')
			{
				colorSpaceStructure = new RGB(p.X, 255 - p.Y, rGB.Blue);
			}
		}
		RGB rgb = ((!(colorSpaceStructure is HSB hsb)) ? ((RGB)colorSpaceStructure) : ColorConverter.HsbToRgb(hsb));
		return ColorConverter.RgbToColor(rgb);
	}
}
