using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sano.PersonalProjects.ColorPicker.Controls;

[ComVisible(false)]
public class ColorPanel : UserControl
{
	private const int PANELSTATE_isLeftMouseDown = 1;

	private const int PANELSTATE_isLeftMouseDownAndMoving = 8;

	private const int ARROW_HEIGHT = 10;

	private const int ARROW_WIDTH = 6;

	private IContainer components;

	private ColorFieldPanel colorFieldPanel;

	private ColorSwatchPanel colorSwatchPanel;

	private DragForm dragForm;

	private HexTextBox hexTextBox;

	private HsbColorSpace hsbColorSpace;

	private Label hexLabel;

	private PictureBox picCurrentColor;

	private PictureBox picColorSlider;

	private RgbColorSpace rgbColorSpace;

	private Timer sliderTimer;

	private bool m_isLeftMouseButtonDown;

	private bool m_isLeftMouseButtonDownAndMoving;

	private int m_currentColorSliderArrowYLocation;

	private int m_currentYValue;

	private int m_oldValue;

	private int m_targetYValue;

	private Rectangle m_selectedColorRegion;

	private Rectangle m_selectedColorColorRegion;

	private Bitmap m_colorSliderBitmap = new Bitmap(18, 256);

	private Rectangle m_leftColorSliderArrowRegion;

	private Rectangle m_rightColorSliderArrowRegion;

	private ColorSpace m_currentColorSpace;

	private BitVector32 m_panelState;

	private readonly Rectangle m_colorSliderOuterRegion = new Rectangle(296, 6, 21, 259);

	private readonly Rectangle m_colorSliderInnerRegion = new Rectangle(298, 8, 18, 256);

	private readonly Rectangle m_colorFieldOuterRegion = new Rectangle(12, 4, 265, 265);

	private readonly Rectangle m_colorFieldInnerRegion = new Rectangle(15, 7, 257, 257);

	private readonly Rectangle m_valueFieldOuterRegion = new Rectangle(295, 4, 27, 265);

	private readonly Rectangle m_valueFieldInnerRegion = new Rectangle(298, 7, 19, 257);

	private readonly Rectangle m_swatchRegion = new Rectangle(464, 4, 164, 263);

	private string HexValue => rgbColorSpace.ConvertToHex().ToString();

	public RGB RGBValue
	{
		get
		{
			return (RGB)rgbColorSpace.Structure;
		}
		set
		{
			ColorSelected(ColorConverter.RgbToColor(value), updateSliderPosition: true);
		}
	}

	public ColorPanel()
	{
		InitializeComponent();
		InitializeCustomComponents();
		m_currentColorSliderArrowYLocation = m_colorSliderOuterRegion.Bottom - 2;
		m_leftColorSliderArrowRegion = GetLeftTrianglePointerInvalidationRegion(m_currentColorSliderArrowYLocation);
		m_rightColorSliderArrowRegion = GetRightTrianglePointerInvalidationRegion(m_currentColorSliderArrowYLocation);
		m_selectedColorRegion = new Rectangle(344, 8, 88, 40);
		m_selectedColorColorRegion = new Rectangle(346, 10, 85, 37);
		m_colorSliderBitmap = new Bitmap(18, 256);
		m_panelState = new BitVector32(0);
		SetStyle(ControlStyles.UserPaint | ControlStyles.DoubleBuffer, value: true);
		AllowDrop = true;
	}

	protected override void OnLoad(EventArgs e)
	{
		dragForm = new DragForm();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		using Graphics graphics = e.Graphics;
		Point[] points = CreateLeftTrianglePointer(m_currentColorSliderArrowYLocation);
		Point[] points2 = CreateRightTrianglePointer(m_currentColorSliderArrowYLocation);
		graphics.DrawPolygon(Pens.Black, points);
		graphics.DrawPolygon(Pens.Black, points2);
		ControlPaint.DrawBorder3D(graphics, m_colorFieldOuterRegion);
		graphics.DrawRectangle(Pens.Black, m_colorFieldInnerRegion);
		ControlPaint.DrawBorder3D(graphics, m_valueFieldOuterRegion);
		graphics.DrawRectangle(Pens.Black, m_valueFieldInnerRegion);
		ControlPaint.DrawBorder3D(graphics, new Rectangle(picCurrentColor.Location.X - 3, picCurrentColor.Location.Y - 3, picCurrentColor.Width + 7, picCurrentColor.Height + 7));
	}

	private Point[] CreateLeftTrianglePointer(int y)
	{
		return new Point[3]
		{
			new Point(m_valueFieldOuterRegion.Left - 6 - 1, y - 5),
			new Point(m_valueFieldOuterRegion.Left - 2, y),
			new Point(m_valueFieldOuterRegion.Left - 6 - 1, y + 5)
		};
	}

	private Point[] CreateRightTrianglePointer(int y)
	{
		return new Point[3]
		{
			new Point(m_valueFieldOuterRegion.Right - 1 + 6, y - 5),
			new Point(m_valueFieldOuterRegion.Right, y),
			new Point(m_valueFieldOuterRegion.Right - 1 + 6, y + 5)
		};
	}

	private Rectangle GetLeftTrianglePointerInvalidationRegion(int arrowY)
	{
		int num = 8;
		int num2 = m_valueFieldOuterRegion.Left - num;
		int num3 = arrowY - 5 - 1;
		int num4 = 8;
		int num5 = 13;
		return new Rectangle(num2, num3, num4, num5);
	}

	private Rectangle GetRightTrianglePointerInvalidationRegion(int arrowY)
	{
		int right = m_valueFieldOuterRegion.Right;
		int num = arrowY - 5 - 1;
		int num2 = 8;
		int num3 = 13;
		return new Rectangle(right, num, num2, num3);
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		if (m_isLeftMouseButtonDown)
		{
			m_isLeftMouseButtonDownAndMoving = true;
			CheckCursorYRegion(e.Y);
		}
		base.OnMouseMove(e);
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		if (e.Button == MouseButtons.Left)
		{
			m_isLeftMouseButtonDown = true;
			CheckCursorYRegion(e.Y);
			UpdateColorPanels(updateSlider: false, resetPreviouslyPickedPointOnColorField: false, updateHexValue: true);
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		if (m_isLeftMouseButtonDown)
		{
			UpdateColorField(resetPreviouslyPickedPoint: false);
		}
		m_isLeftMouseButtonDown = false;
		m_isLeftMouseButtonDownAndMoving = false;
	}

	private void picColorSlider_MouseDown(object sender, MouseEventArgs e)
	{
		m_currentColorSliderArrowYLocation = m_colorSliderInnerRegion.Top + e.Y;
		InvalidateColorSliderArrowRegions();
		m_leftColorSliderArrowRegion = GetLeftTrianglePointerInvalidationRegion(m_currentColorSliderArrowYLocation);
		m_rightColorSliderArrowRegion = GetRightTrianglePointerInvalidationRegion(m_currentColorSliderArrowYLocation);
		InvalidateColorSliderArrowRegions();
		UpdateColorPanels(updateSlider: false, resetPreviouslyPickedPointOnColorField: false, updateHexValue: true);
		CheckCursorYRegion(e.Y + m_colorSliderInnerRegion.Top);
	}

	private void SelectedColorSpaceComponentChanged(ColorSpace sender, EventArgs e)
	{
		if (sender is RgbColorSpace)
		{
			hsbColorSpace.ResetComponents();
		}
		else if (sender is HsbColorSpace)
		{
			rgbColorSpace.ResetComponents();
		}
		m_currentColorSpace = sender;
		UpdateColorSliderArrowRegions();
		UpdateColorPanels(updateSlider: true, resetPreviouslyPickedPointOnColorField: true, updateHexValue: true);
	}

	private void ColorSpaceComponentValueChanged(ColorSpace sender, EventArgs e)
	{
		if (sender is RgbColorSpace)
		{
			hsbColorSpace.Structure = ColorConverter.RgbToHsb((RGB)rgbColorSpace.Structure);
		}
		else if (sender is HsbColorSpace)
		{
			rgbColorSpace.Structure = ColorConverter.HsbToRgb((HSB)hsbColorSpace.Structure);
		}
		UpdateColorSliderArrowRegions();
		UpdateColorPanels(updateSlider: true, resetPreviouslyPickedPointOnColorField: true, updateHexValue: true);
	}

	private void sliderTimer_Tick(object sender, EventArgs e)
	{
		int num = Math.Abs(m_currentYValue - m_targetYValue);
		int num2 = (int)Math.Round((double)num / 2.0);
		if (num2 == 0)
		{
			m_currentYValue = m_targetYValue;
			sliderTimer.Stop();
		}
		else if (m_currentYValue < m_targetYValue)
		{
			m_currentYValue += num2;
		}
		else
		{
			m_currentYValue += -num2;
		}
		if (m_currentColorSpace is RgbColorSpace)
		{
			hsbColorSpace.Structure = ColorConverter.RgbToHsb((RGB)rgbColorSpace.Structure);
		}
		else if (m_currentColorSpace is HsbColorSpace)
		{
			rgbColorSpace.Structure = ColorConverter.HsbToRgb((HSB)hsbColorSpace.Structure);
		}
		UpdateColorPanels(updateSlider: false, resetPreviouslyPickedPointOnColorField: false, updateHexValue: true);
	}

	private void ColorSelected(Color color, bool updateSliderPosition)
	{
		if (!ColorConverter.ColorToRgb(color).Equals(rgbColorSpace.Structure))
		{
			RGB rGB = ColorConverter.ColorToRgb(color);
			rgbColorSpace.Structure = rGB;
			hsbColorSpace.Structure = ColorConverter.RgbToHsb(rGB);
			if (updateSliderPosition)
			{
				UpdateColorSliderArrowRegions();
			}
			UpdateColorPanels(updateSlider: true, resetPreviouslyPickedPointOnColorField: true, updateHexValue: true);
		}
	}

	private void colorSwatchPanel_ColorSwatchSelected(object sender, ColorSelectedEventArgs e)
	{
		ColorSelected(e.Color, updateSliderPosition: true);
	}

	private void colorFieldPanel_ColorSelected(object sender, ColorSelectedEventArgs e)
	{
		ColorSelected(e.Color, updateSliderPosition: false);
	}

	private void InitializeCustomComponents()
	{
		SuspendLayout();
		hexTextBox = new HexTextBox();
		hexTextBox.Location = new Point(376, 231);
		hexTextBox.Size = new Size(55, 20);
		hexTextBox.BorderStyle = BorderStyle.FixedSingle;
		hexTextBox.ReadOnly = false;
		hexTextBox.KeyUp += hexTextBox_KeyUp;
		hexTextBox.KeyDown += hexTextBox_KeyDown;
		base.Controls.Add(hexTextBox);
		colorFieldPanel = new ColorFieldPanel();
		colorFieldPanel.BackColor = SystemColors.Desktop;
		colorFieldPanel.Location = new Point(16, 8);
		colorFieldPanel.Name = "colorFieldPanel";
		colorFieldPanel.Size = new Size(256, 256);
		colorFieldPanel.TabIndex = 52;
		colorFieldPanel.ColorSelected += colorFieldPanel_ColorSelected;
		base.Controls.Add(colorFieldPanel);
		colorSwatchPanel = new ColorSwatchPanel();
		colorSwatchPanel.Location = new Point(464, 4);
		colorSwatchPanel.Name = "colorSwatchPanel";
		colorSwatchPanel.Size = new Size(96, 264);
		colorSwatchPanel.TabIndex = 53;
		colorSwatchPanel.ColorSwatchSelected += colorSwatchPanel_ColorSwatchSelected;
		base.Controls.Add(colorSwatchPanel);
		rgbColorSpace = new RgbColorSpace();
		rgbColorSpace.DisplayRadioButtons = true;
		rgbColorSpace.Location = new Point(336, 138);
		rgbColorSpace.Name = "rgbColorSpace";
		rgbColorSpace.ReadOnly = false;
		rgbColorSpace.Size = new Size(112, 96);
		rgbColorSpace.TabIndex = 48;
		rgbColorSpace.ComponentValueChanged += ColorSpaceComponentValueChanged;
		rgbColorSpace.SelectedComponentChanged += SelectedColorSpaceComponentChanged;
		base.Controls.Add(rgbColorSpace);
		hsbColorSpace = new HsbColorSpace();
		hsbColorSpace.DisplayRadioButtons = true;
		hsbColorSpace.Location = new Point(336, 58);
		hsbColorSpace.Name = "hsbColorSpace";
		hsbColorSpace.ReadOnly = false;
		hsbColorSpace.Size = new Size(112, 96);
		hsbColorSpace.TabIndex = 47;
		hsbColorSpace.ComponentValueChanged += ColorSpaceComponentValueChanged;
		hsbColorSpace.SelectedComponentChanged += SelectedColorSpaceComponentChanged;
		hsbColorSpace.SetDefaultSelection();
		base.Controls.Add(hsbColorSpace);
		ResumeLayout(performLayout: false);
	}

	private void CheckCursorYRegion(int y)
	{
		int num = y;
		if (m_isLeftMouseButtonDown && !m_isLeftMouseButtonDownAndMoving)
		{
			if (y < m_colorSliderInnerRegion.Top || y > m_colorSliderInnerRegion.Bottom)
			{
				return;
			}
		}
		else
		{
			if (!m_isLeftMouseButtonDownAndMoving)
			{
				return;
			}
			if (y < m_colorSliderInnerRegion.Top)
			{
				num = m_colorSliderInnerRegion.Top;
			}
			else if (y >= m_colorSliderInnerRegion.Bottom)
			{
				num = m_colorSliderInnerRegion.Bottom - 1;
			}
		}
		m_currentColorSliderArrowYLocation = num;
		Invalidate(m_leftColorSliderArrowRegion);
		Invalidate(m_rightColorSliderArrowRegion);
		m_leftColorSliderArrowRegion = GetLeftTrianglePointerInvalidationRegion(num);
		m_rightColorSliderArrowRegion = GetRightTrianglePointerInvalidationRegion(num);
		Invalidate(m_leftColorSliderArrowRegion);
		Invalidate(m_rightColorSliderArrowRegion);
		ValueChanged(255 - (num - m_colorSliderInnerRegion.Top));
	}

	private void ValueChanged(int newValue)
	{
		int num = newValue;
		switch (m_currentColorSpace.SelectedComponent.Unit)
		{
		case ComponentUnit.Percentage:
			num = (int)Math.Ceiling((double)num / 2.55);
			break;
		case ComponentUnit.Degree:
			num = (int)Math.Ceiling((double)num / (17.0 / 24.0));
			if (num == 360)
			{
				num = 0;
			}
			break;
		}
		m_currentColorSpace.SelectedComponent.Value = num;
		if (m_oldValue != num && m_isLeftMouseButtonDownAndMoving)
		{
			m_targetYValue = newValue;
			m_currentYValue = m_oldValue;
			m_oldValue = newValue;
			sliderTimer.Start();
		}
		else
		{
			UpdateColorField(resetPreviouslyPickedPoint: false);
		}
	}

	private void SetCurrentSliderArrowYLocation(double sValue)
	{
		m_currentColorSliderArrowYLocation = (int)((double)m_colorSliderInnerRegion.Top + (255.0 - sValue));
	}

	private void UpdateColorSliderArrowRegions()
	{
		InvalidateColorSliderArrowRegions();
		CalculateValueRegionMapping(m_currentColorSpace.SelectedComponent);
		m_leftColorSliderArrowRegion = GetLeftTrianglePointerInvalidationRegion(m_currentColorSliderArrowYLocation);
		m_rightColorSliderArrowRegion = GetRightTrianglePointerInvalidationRegion(m_currentColorSliderArrowYLocation);
		InvalidateColorSliderArrowRegions();
	}

	private void InvalidateColorSliderArrowRegions()
	{
		Invalidate(m_leftColorSliderArrowRegion);
		Invalidate(m_rightColorSliderArrowRegion);
	}

	private void CalculateValueRegionMapping(ColorSpaceComponent csc)
	{
		int num = csc.Value;
		switch (csc.Unit)
		{
		case ComponentUnit.Percentage:
			num = (int)Math.Ceiling(2.55 * (double)num);
			break;
		case ComponentUnit.Degree:
			num = (int)Math.Ceiling(17.0 / 24.0 * (double)num);
			break;
		}
		SetCurrentSliderArrowYLocation(num);
	}

	private void UpdateColorPanels(bool updateSlider, bool resetPreviouslyPickedPointOnColorField, bool updateHexValue)
	{
		picCurrentColor.BackColor = rgbColorSpace.GetColor();
		if (updateSlider)
		{
			UpdateColorSlider();
		}
		UpdateColorField(resetPreviouslyPickedPointOnColorField);
		if (updateHexValue)
		{
			UpdateHexValue();
		}
	}

	private void UpdateHexValue()
	{
		hexTextBox.Text = HexValue;
		hexTextBox.SelectionStart = HexValue.Length;
	}

	private void UpdateColorField(bool resetPreviouslyPickedPoint)
	{
		int value = m_currentColorSpace.SelectedComponent.Value;
		char displayCharacter = m_currentColorSpace.SelectedComponent.DisplayCharacter;
		if (m_currentColorSpace is HsbColorSpace)
		{
			switch (displayCharacter)
			{
			case 'H':
			{
				int num = m_currentColorSliderArrowYLocation - m_colorSliderInnerRegion.Top;
				Color pixel = ((Bitmap)picColorSlider.Image).GetPixel(10, num);
				colorFieldPanel.UpdateColor(m_currentColorSpace, displayCharacter, pixel, resetPreviouslyPickedPoint);
				break;
			}
			case 'B':
			case 'S':
				colorFieldPanel.UpdateColor(m_currentColorSpace, displayCharacter, value, resetPreviouslyPickedPoint);
				break;
			}
		}
		else if (m_currentColorSpace is RgbColorSpace)
		{
			colorFieldPanel.UpdateColor(m_currentColorSpace, displayCharacter, value, resetPreviouslyPickedPoint);
		}
	}

	private void UpdateColorSlider()
	{
		RGB rGB = (RGB)rgbColorSpace.Structure;
		HSB hSB = (HSB)hsbColorSpace.Structure;
		Rectangle rect = new Rectangle(0, -1, 18, 257);
		using (Graphics graphics = Graphics.FromImage(m_colorSliderBitmap))
		{
			if (m_currentColorSpace is RgbColorSpace)
			{
				char displayCharacter = m_currentColorSpace.SelectedComponent.DisplayCharacter;
				int red = rGB.Red;
				int green = rGB.Green;
				int blue = rGB.Blue;
				Color color;
				Color color2;
				switch (displayCharacter)
				{
				case 'R':
					color = Color.FromArgb(0, green, blue);
					color2 = Color.FromArgb(255, green, blue);
					break;
				case 'G':
					color = Color.FromArgb(red, 0, blue);
					color2 = Color.FromArgb(red, 255, blue);
					break;
				default:
					color = Color.FromArgb(red, green, 0);
					color2 = Color.FromArgb(red, green, 255);
					break;
				}
				using LinearGradientBrush brush = new LinearGradientBrush(rect, color, color2, 270f);
				graphics.FillRectangle(brush, rect);
			}
			else if (m_currentColorSpace is HsbColorSpace)
			{
				if (m_currentColorSpace.SelectedComponent.DisplayCharacter == 'H')
				{
					Rectangle rect2 = new Rectangle(0, 0, 20, 256);
					using LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect2, Color.Blue, Color.Red, 90f, isAngleScaleable: false);
					Color[] colors = new Color[7]
					{
						Color.Red,
						Color.Magenta,
						Color.Blue,
						Color.Cyan,
						Color.FromArgb(0, 255, 0),
						Color.Yellow,
						Color.Red
					};
					float[] positions = new float[7] { 0f, 0.1667f, 0.3372f, 0.502f, 0.6686f, 0.8313f, 1f };
					ColorBlend colorBlend = new ColorBlend();
					colorBlend.Colors = colors;
					colorBlend.Positions = positions;
					linearGradientBrush.InterpolationColors = colorBlend;
					graphics.FillRectangle(linearGradientBrush, rect);
				}
				else if (m_currentColorSpace.SelectedComponent.DisplayCharacter == 'B')
				{
					RGB rGB2 = ColorConverter.HsbToRgb(new HSB(hSB.Hue, hSB.Saturation, 100));
					RGB rGB3 = ColorConverter.HsbToRgb(new HSB(hSB.Hue, hSB.Saturation, 0));
					using LinearGradientBrush brush2 = new LinearGradientBrush(rect, Color.FromArgb(rGB2.Red, rGB2.Green, rGB2.Blue), Color.FromArgb(rGB3.Red, rGB3.Green, rGB3.Blue), 90f);
					graphics.FillRectangle(brush2, rect);
				}
				else
				{
					RGB rGB4 = ColorConverter.HsbToRgb(new HSB(hSB.Hue, 100, hSB.Brightness));
					RGB rGB5 = ColorConverter.HsbToRgb(new HSB(hSB.Hue, 0, hSB.Brightness));
					using LinearGradientBrush brush3 = new LinearGradientBrush(rect, Color.FromArgb(rGB4.Red, rGB4.Green, rGB4.Blue), Color.FromArgb(rGB5.Red, rGB5.Green, rGB5.Blue), 90f);
					graphics.FillRectangle(brush3, rect);
				}
			}
		}
		picColorSlider.Image = m_colorSliderBitmap;
	}

	private void picCurrentColor_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			dragForm.UpdateLocation(PointToScreen(picCurrentColor.Location));
			dragForm.CursorXDifference = Cursor.Position.X - dragForm.Location.X;
			dragForm.CursorYDifference = Cursor.Position.Y - dragForm.Location.Y;
			dragForm.BackColor = picCurrentColor.BackColor;
			dragForm.ChangeSize(picCurrentColor.Size);
			dragForm.ShowForm();
			picCurrentColor.DoDragDrop(rgbColorSpace.GetColor(), DragDropEffects.Move);
		}
	}

	private void hexTextBox_KeyUp(object sender, KeyEventArgs e)
	{
		UpdateColorSpacesWithNewHexValue();
	}

	private void hexTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		UpdateColorSpacesWithNewHexValue();
	}

	private void UpdateColorSpacesWithNewHexValue()
	{
		RGB rGB = ColorConverter.HexToRgb(hexTextBox.Text);
		rgbColorSpace.Structure = rGB;
		hsbColorSpace.Structure = ColorConverter.RgbToHsb(rGB);
		UpdateColorSliderArrowRegions();
		UpdateColorPanels(updateSlider: true, resetPreviouslyPickedPointOnColorField: true, updateHexValue: false);
	}

	private void picCurrentColor_GiveFeedback(object sender, GiveFeedbackEventArgs e)
	{
		e.UseDefaultCursors = false;
		Cursor.Current = Cursors.Hand;
	}

	private void picCurrentColor_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
	{
		if (e.Action == DragAction.Cancel || e.Action == DragAction.Drop)
		{
			dragForm.Hide();
		}
		else
		{
			dragForm.Location = new Point(Cursor.Position.X - dragForm.CursorXDifference, Cursor.Position.Y - dragForm.CursorYDifference);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (components != null)
			{
				components.Dispose();
			}
			if (m_colorSliderBitmap != null)
			{
				m_colorSliderBitmap.Dispose();
			}
			if (dragForm != null)
			{
				dragForm.Dispose();
			}
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.picCurrentColor = new System.Windows.Forms.PictureBox();
		this.hexLabel = new System.Windows.Forms.Label();
		this.picColorSlider = new System.Windows.Forms.PictureBox();
		this.sliderTimer = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.picCurrentColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.picCurrentColor.Location = new System.Drawing.Point(352, 7);
		this.picCurrentColor.Name = "picCurrentColor";
		this.picCurrentColor.Size = new System.Drawing.Size(85, 37);
		this.picCurrentColor.TabIndex = 42;
		this.picCurrentColor.TabStop = false;
		this.picCurrentColor.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(picCurrentColor_GiveFeedback);
		this.picCurrentColor.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(picCurrentColor_QueryContinueDrag);
		this.picCurrentColor.MouseDown += new System.Windows.Forms.MouseEventHandler(picCurrentColor_MouseDown);
		this.hexLabel.Location = new System.Drawing.Point(344, 231);
		this.hexLabel.Name = "hexLabel";
		this.hexLabel.Size = new System.Drawing.Size(31, 23);
		this.hexLabel.TabIndex = 40;
		this.hexLabel.Text = "HEX:";
		this.hexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.picColorSlider.Location = new System.Drawing.Point(299, 8);
		this.picColorSlider.Name = "picColorSlider";
		this.picColorSlider.Size = new System.Drawing.Size(18, 256);
		this.picColorSlider.TabIndex = 50;
		this.picColorSlider.TabStop = false;
		this.picColorSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(picColorSlider_MouseDown);
		this.sliderTimer.Interval = 10;
		this.sliderTimer.Tick += new System.EventHandler(sliderTimer_Tick);
		base.Controls.Add(this.picColorSlider);
		base.Controls.Add(this.hexLabel);
		base.Controls.Add(this.picCurrentColor);
		base.Name = "ColorPanel";
		base.Size = new System.Drawing.Size(584, 272);
		base.ResumeLayout(false);
	}
}
