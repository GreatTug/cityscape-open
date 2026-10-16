using System;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palCore;

internal class Scrub
{
	public delegate SliderEnums.PointModes AddIncrementDelegate(float increment);

	private const float NumPixelsPerInc = 10f;

	private bool Hidden;

	private bool Scrubbing;

	private int TheNumPixelsTrigger;

	private int Error;

	private Point StartingPosition;

	private Point LastPosition;

	private object TheScrubbingObject;

	private AddIncrementDelegate TheAddIncrement;

	public AddIncrementDelegate AddIncrement
	{
		get
		{
			return TheAddIncrement;
		}
		set
		{
			TheAddIncrement = value;
		}
	}

	public object ScrubbingObject
	{
		get
		{
			return TheScrubbingObject;
		}
		set
		{
			TheScrubbingObject = value;
		}
	}

	public bool IsScrubbing => Scrubbing;

	public int NumPixelsTrigger
	{
		set
		{
			TheNumPixelsTrigger = value;
		}
	}

	public void MouseDown(object sender, MouseEventArgs e)
	{
		Control control = sender as Control;
		Scrubbing = true;
		LastPosition = control.PointToClient(Cursor.Position);
		StartingPosition = Cursor.Position;
		control.Capture = true;
		if (!Hidden)
		{
			Cursor.Hide();
			Hidden = true;
		}
		Error = 0;
	}

	public void MouseMove(object sender, MouseEventArgs e)
	{
		if (!Scrubbing)
		{
			return;
		}
		Control control = sender as Control;
		float num = 1f;
		if (Control.ModifierKeys == Keys.Shift)
		{
			num *= 10f;
		}
		if (Control.ModifierKeys == Keys.Control)
		{
			num *= 0.1f;
		}
		int num2 = e.X - LastPosition.X;
		int num3 = e.Y - LastPosition.Y;
		LastPosition = new Point(e.X, e.Y);
		Error += num2 - num3;
		Point position = Cursor.Position;
		Rectangle clip = Cursor.Clip;
		if (position.X >= clip.Right - 1 || position.X <= clip.Left || position.Y >= clip.Bottom - 1 || position.Y <= clip.Top)
		{
			LastPosition = new Point(clip.Left + clip.Width / 2, clip.Top + clip.Height / 2);
			Cursor.Position = LastPosition;
			LastPosition = control.PointToClient(LastPosition);
		}
		if (Error > 3 || Error < -3)
		{
			float num4 = Error / 3;
			Error = 0;
			if (TheAddIncrement != null)
			{
				TheAddIncrement(num4 * num);
			}
		}
	}

	public void MouseUp(object sender, EventArgs e)
	{
		if (Scrubbing)
		{
			Cursor.Position = StartingPosition;
			Control control = sender as Control;
			control.Capture = (Scrubbing = false);
			if (Hidden)
			{
				Hidden = false;
				Cursor.Show();
			}
			TheScrubbingObject = null;
		}
	}
}
