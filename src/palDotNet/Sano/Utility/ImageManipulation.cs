using System;
using System.Drawing;
using Sano.Utility.NativeMethods;

namespace Sano.Utility;

public sealed class ImageManipulation
{
	public static Image StretchImage(Image sourceImage, Point p, Rectangle zoomRegion, Size desiredSize)
	{
		Image image = new Bitmap(desiredSize.Width, desiredSize.Height);
		using Graphics graphics = Graphics.FromImage(image);
		IntPtr dC = User32.GetDC(IntPtr.Zero);
		IntPtr intPtr = Gdi32.CreateCompatibleDC(dC);
		IntPtr hbitmap = ((Bitmap)sourceImage).GetHbitmap();
		IntPtr hgdiobj = Gdi32.SelectObject(intPtr, hbitmap);
		Gdi32.DeleteObject(hgdiobj);
		IntPtr hdc = graphics.GetHdc();
		if (Gdi32.StretchBlt(hdc, 0, 0, desiredSize.Width, desiredSize.Height, intPtr, p.X, p.Y, zoomRegion.Width, zoomRegion.Height, 13369376) == 0)
		{
			throw new ImageManipulationException("There was a problem with the StretchBlt function call.");
		}
		graphics.ReleaseHdc(hdc);
		Gdi32.DeleteObject(hbitmap);
		Gdi32.DeleteDC(intPtr);
		User32.ReleaseDC(IntPtr.Zero, dC);
		return image;
	}

	public static Image CaptureDesktopRegion(Point location, Size size)
	{
		Image image = new Bitmap(size.Width, size.Height);
		using Graphics graphics = Graphics.FromImage(image);
		IntPtr hdc = graphics.GetHdc();
		IntPtr windowDC = User32.GetWindowDC(IntPtr.Zero);
		if (Gdi32.BitBlt(hdc, 0, 0, size.Width, size.Height, windowDC, location.X, location.Y, 13369376) == 0)
		{
			throw new ImageManipulationException("There was a problem with the BitBlt function call.");
		}
		User32.ReleaseDC(IntPtr.Zero, windowDC);
		graphics.ReleaseHdc(hdc);
		return image;
	}
}
