using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Sano.Utility;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class ColorSwatchPanel : Panel
{
	private const int PADDING = 2;

	private const int SWATCH_WIDTH = 10;

	private const int SWATCH_HEIGHT = 10;

	private const int OUTER_PADDING = 6;

	private IContainer components;

	private ToolTip colorTip;

	private DragForm dragFormSwatch;

	private ContextMenu contextMenu;

	private MenuItem renameSwatchMenuItem;

	private MenuItem deleteSwatchMenuItem;

	private MenuItem addNewSwatchMenuItem;

	private readonly Rectangle m_swatchRegion = new Rectangle(0, 0, 164, 263);

	private readonly string m_customSwatchesFile = "CustomSwatches.xml";

	private ArrayList m_swatches;

	private bool m_paintActiveEmptySwatch;

	private Bitmap m_swatchBitmap;

	private ColorSwatch[,] m_swatchArray;

	private ColorSwatch m_lastSwatch;

	private int m_startX;

	private int m_startY;

	private int m_swatchOuterRegionWidth;

	private int m_swatchOuterRegionHeight;

	private int m_totalNumberOfSwatches;

	private int m_numberOfEmptySwatches;

	private int m_swatchRowNum;

	private int m_swatchColumnNum;

	private Point m_nextEmptySwatchPoint;

	internal event ColorSwatchSelectedHandler ColorSwatchSelected;

	internal ColorSwatchPanel()
	{
		InitializeComponent();
		m_startX = m_swatchRegion.X + 6;
		m_startY = m_swatchRegion.Y + 6;
		colorTip.SetToolTip(this, "blah");
		dragFormSwatch = new DragForm();
		AllowDrop = true;
	}

	protected override void OnLayout(LayoutEventArgs levent)
	{
		base.OnLayout(levent);
		if (File.Exists(m_customSwatchesFile))
		{
			m_swatches = ColorSwatchXml.ReadSwatches(m_customSwatchesFile, isResourceFile: false);
		}
		else
		{
			CreateCustomSwatchesFile();
			m_swatches = ColorSwatchXml.ReadSwatches("ColorSwatches.xml", isResourceFile: true);
		}
		m_swatchOuterRegionWidth = base.Width - (base.Width - 12) % 12;
		m_swatchOuterRegionHeight = base.Height - (base.Height - 12) % 12;
		int num = (m_swatchOuterRegionWidth - 12) / 12;
		int num2 = (m_swatchOuterRegionHeight - 12) / 12;
		m_swatchArray = new ColorSwatch[num, num2];
	}

	private void CreateCustomSwatchesFile()
	{
		StreamReader streamReader = new StreamReader(Resources.GetFileResource("ColorSwatches.xml"));
		if (streamReader != null)
		{
			StreamWriter streamWriter = null;
			try
			{
				streamWriter = new StreamWriter(m_customSwatchesFile);
				streamWriter.Write(streamReader.ReadToEnd());
				streamWriter.Flush();
			}
			finally
			{
				streamReader.Close();
				streamWriter?.Close();
			}
		}
	}

	private Bitmap BuildSwatchBitmap()
	{
		int startX = m_startX;
		int startY = m_startY;
		bool flag = false;
		int swatchColumnNum = 0;
		int swatchRowNum = 0;
		ColorSwatch[] array = (ColorSwatch[])m_swatches.ToArray(typeof(ColorSwatch));
		Bitmap bitmap = new Bitmap(m_swatchOuterRegionWidth, m_swatchOuterRegionHeight);
		using (Graphics graphics = Graphics.FromImage(bitmap))
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Location = new Point(startX, startY);
				array[i].Size = new Size(10, 10);
				ref ColorSwatch reference = ref m_swatchArray[swatchColumnNum++, swatchRowNum];
				reference = array[i];
				using (SolidBrush brush = new SolidBrush(array[i].Color))
				{
					graphics.FillRectangle(brush, startX, startY, 10, 10);
				}
				graphics.DrawRectangle(Pens.Black, startX, startY, 10, 10);
				UpdatePositions(ref startX, ref startY, ref swatchColumnNum, ref swatchRowNum);
				if (startY + 10 > m_swatchOuterRegionHeight)
				{
					flag = true;
					break;
				}
				m_totalNumberOfSwatches++;
			}
			m_swatchRowNum = swatchRowNum;
			m_swatchColumnNum = swatchColumnNum;
			if (!flag)
			{
				m_numberOfEmptySwatches = 0;
				while (true)
				{
					if (++m_numberOfEmptySwatches == 1)
					{
						m_nextEmptySwatchPoint = new Point(startX, startY);
					}
					graphics.DrawRectangle(Pens.DarkGray, startX, startY, 10, 10);
					UpdatePositions(ref startX, ref startY, ref swatchColumnNum, ref swatchRowNum);
					if (startY + 10 > m_swatchOuterRegionHeight)
					{
						break;
					}
					m_totalNumberOfSwatches++;
				}
				flag = true;
			}
		}
		return bitmap;
	}

	private void UpdatePositions(ref int x, ref int y, ref int swatchColumnNum, ref int swatchRowNum)
	{
		if (x + 24 > m_swatchOuterRegionWidth)
		{
			x = m_startX;
			y += 12;
			swatchColumnNum = 0;
			swatchRowNum++;
		}
		else
		{
			x += 12;
		}
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		ControlPaint.DrawBorder3D(pe.Graphics, new Rectangle(0, 0, m_swatchOuterRegionWidth, m_swatchOuterRegionHeight));
		if (m_swatchBitmap == null)
		{
			m_swatchBitmap = BuildSwatchBitmap();
		}
		pe.Graphics.DrawImage(m_swatchBitmap, new Rectangle(new Point(0, 0), new Size(m_swatchBitmap.Width, m_swatchBitmap.Height)));
		if (m_paintActiveEmptySwatch)
		{
			Rectangle rect = new Rectangle(m_nextEmptySwatchPoint, new Size(10, 10));
			pe.Graphics.DrawRectangle(Pens.Yellow, rect);
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		int num = m_swatchOuterRegionWidth - 12;
		int num2 = m_swatchOuterRegionHeight - 12;
		int num3 = e.X - 6;
		int num4 = e.Y - 6;
		Rectangle rect = new Rectangle(m_startX, m_startY, num, num2);
		if (!new Rectangle(e.X, e.Y, 1, 1).IntersectsWith(rect))
		{
			return;
		}
		int num5 = num3 / 12;
		int num6 = num4 / 12;
		ColorSwatch colorSwatch = m_swatchArray[num5, num6];
		Rectangle rect2 = new Rectangle(colorSwatch.Location, colorSwatch.Size);
		Point location = new Point(e.X, e.Y);
		if (new Rectangle(location, new Size(1, 1)).IntersectsWith(rect2))
		{
			if (!m_lastSwatch.Equals(colorSwatch))
			{
				colorTip.Active = false;
			}
			if (!colorSwatch.Description.Equals(colorTip.GetToolTip(this)))
			{
				colorTip.SetToolTip(this, colorSwatch.Description);
			}
			colorTip.Active = true;
			m_lastSwatch = colorSwatch;
			Cursor = Cursors.Hand;
		}
		else
		{
			Cursor = Cursors.Default;
			colorTip.SetToolTip(this, "");
			colorTip.Active = false;
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		int num = m_swatchOuterRegionWidth - 12;
		int num2 = m_swatchOuterRegionHeight - 12;
		int num3 = e.X - 6;
		int num4 = e.Y - 6;
		Rectangle rect = new Rectangle(m_startX, m_startY, num, num2);
		if (new Rectangle(e.X, e.Y, 1, 1).IntersectsWith(rect))
		{
			int num5 = num3 / 12;
			int num6 = num4 / 12;
			ColorSwatch colorSwatch = m_swatchArray[num5, num6];
			Rectangle rect2 = new Rectangle(colorSwatch.Location, colorSwatch.Size);
			Point location = new Point(e.X, e.Y);
			if (new Rectangle(location, new Size(1, 1)).IntersectsWith(rect2) && ColorSwatchSelected != null)
			{
				ColorSwatchSelected(this, new ColorSelectedEventArgs(colorSwatch.Color));
			}
		}
	}

	protected override void OnDragEnter(DragEventArgs drgevent)
	{
		ToggleEmptySwatchState(isActive: true);
		base.OnDragEnter(drgevent);
	}

	protected override void OnDragLeave(EventArgs e)
	{
		ToggleEmptySwatchState(isActive: false);
		base.OnDragLeave(e);
	}

	protected override void OnDragOver(DragEventArgs e)
	{
		e.Effect = DragDropEffects.Move;
		base.OnDragOver(e);
	}

	protected override void OnDragDrop(DragEventArgs e)
	{
		Color c = (Color)e.Data.GetData(typeof(Color));
		AddColor(c);
		e.Effect = DragDropEffects.None;
		base.OnDragDrop(e);
	}

	private void AddColor(Color c)
	{
		bool flag = false;
		Point nextEmptySwatchPoint = m_nextEmptySwatchPoint;
		if (DoesColorAlreadyExist(c))
		{
			MessageBox.Show(base.Parent, "Color already exists.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else if (m_numberOfEmptySwatches <= 0)
		{
			MessageBox.Show(base.Parent, "There are no empty swatches available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else
		{
			using AddNewColorSwatchForm addNewColorSwatchForm = new AddNewColorSwatchForm(c);
			addNewColorSwatchForm.StartPosition = FormStartPosition.CenterParent;
			addNewColorSwatchForm.ShowInTaskbar = false;
			if (addNewColorSwatchForm.ShowDialog() == DialogResult.OK && m_swatchBitmap != null)
			{
				int num = m_nextEmptySwatchPoint.X;
				int num2 = m_nextEmptySwatchPoint.Y;
				using (Graphics graphics = Graphics.FromImage(m_swatchBitmap))
				{
					using (SolidBrush brush = new SolidBrush(c))
					{
						graphics.FillRectangle(brush, num, num2, 10, 10);
					}
					graphics.DrawRectangle(Pens.Black, num, num2, 10, 10);
				}
				m_numberOfEmptySwatches--;
				ColorSwatch colorSwatch = new ColorSwatch(c, addNewColorSwatchForm.ColorDescription, new Point(num, num2), new Size(10, 10));
				m_swatchArray[m_swatchColumnNum++, m_swatchRowNum] = colorSwatch;
				m_swatches.Add(colorSwatch);
				flag = true;
				UpdatePositions(ref num, ref num2, ref m_swatchColumnNum, ref m_swatchRowNum);
				m_nextEmptySwatchPoint = new Point(num, num2);
			}
		}
		if (flag)
		{
			ColorSwatchXml.WriteSwatches(m_customSwatchesFile, m_swatches);
		}
		m_paintActiveEmptySwatch = false;
		InvalidateEmptySwatch(nextEmptySwatchPoint);
	}

	private bool DoesColorAlreadyExist(Color c)
	{
		bool result = false;
		ColorSwatch[] array = (ColorSwatch[])m_swatches.ToArray(typeof(ColorSwatch));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Color.Equals(c))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private void ToggleEmptySwatchState(bool isActive)
	{
		if (m_numberOfEmptySwatches > 0)
		{
			m_paintActiveEmptySwatch = isActive;
			InvalidateEmptySwatch();
		}
		else
		{
			m_paintActiveEmptySwatch = false;
		}
	}

	private void InvalidateEmptySwatch()
	{
		InvalidateEmptySwatch(m_nextEmptySwatchPoint);
	}

	private void InvalidateEmptySwatch(Point swatchPoint)
	{
		Rectangle rc = new Rectangle(swatchPoint, new Size(10, 10));
		rc.Inflate(1, 1);
		Invalidate(rc);
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
		this.colorTip = new System.Windows.Forms.ToolTip(this.components);
		this.addNewSwatchMenuItem = new System.Windows.Forms.MenuItem("Add new swatch");
		this.deleteSwatchMenuItem = new System.Windows.Forms.MenuItem("Delete swatch");
		this.renameSwatchMenuItem = new System.Windows.Forms.MenuItem("Rename swatch");
		this.contextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[3] { this.addNewSwatchMenuItem, this.renameSwatchMenuItem, this.deleteSwatchMenuItem });
		this.colorTip.Active = false;
	}
}
