using System;
using System.Drawing;
using System.Windows.Forms;
using Sano.Utility.NativeMethods;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class DragForm : Form
{
	private int m_cursorXDifference;

	private int m_cursorYDifference;

	private Size m_clientRectangleSize;

	internal int CursorXDifference
	{
		get
		{
			return m_cursorXDifference;
		}
		set
		{
			m_cursorXDifference = value;
		}
	}

	internal int CursorYDifference
	{
		get
		{
			return m_cursorYDifference;
		}
		set
		{
			m_cursorYDifference = value;
		}
	}

	internal DragForm()
	{
		base.ShowInTaskbar = false;
		base.TopMost = true;
		base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
		base.Size = new Size(0, 0);
		ShowForm();
		Hide();
	}

	internal void ChangeSize(Size newSize)
	{
		m_clientRectangleSize = newSize;
		base.Size = newSize + new Size(6, 22);
		base.Region = new Region(new Rectangle(new Point(3, 19), newSize));
	}

	internal void UpdateLocation(Point newLocation)
	{
		base.Location = new Point(newLocation.X - 3, newLocation.Y - 19);
	}

	internal void ShowForm()
	{
		base.Opacity = 0.5;
		User32.ShowWindow(base.Handle, 4);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.DrawRectangle(Pens.Black, 0, 0, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
	}

	protected override void WndProc(ref Message m)
	{
		if (m.Msg == 132)
		{
			m.Result = (IntPtr)(-1);
		}
		else
		{
			base.WndProc(ref m);
		}
	}
}
