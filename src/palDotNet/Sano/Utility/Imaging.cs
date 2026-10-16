using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Sano.Utility;

public sealed class Imaging
{
	public enum RasterOperations
	{
		SRCCOPY = 13369376,
		SRCPAINT = 15597702,
		SRCAND = 8913094,
		SRCINVERT = 6684742,
		SRCERASE = 4457256,
		NOTSRCCOPY = 3342344,
		NOTSRCERASE = 1114278,
		MERGECOPY = 12583114,
		MERGEPAINT = 12255782,
		PATCOPY = 15728673,
		PATPAINT = 16452105,
		PATINVERT = 5898313,
		DSTINVERT = 5570569,
		BLACKNESS = 66,
		WHITENESS = 16711778
	}

	internal const int SRCCOPY = 13369376;

	internal const int CAPTUREBLT = 1073741824;

	internal const int SW_HIDE = 0;

	internal const int SW_SHOW = 6;

	[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern bool BitBlt(IntPtr targetDC, int targetX, int targetY, int targetWidth, int targetHeight, IntPtr sourceDC, int sourceStartX, int sourceStartY, RasterOperations ro);

	[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern bool StretchBlt(IntPtr destDC, int dX, int dY, int dWidth, int dHeight, IntPtr sourceDC, int sX, int sY, int sWidth, int sHeight, RasterOperations ro);

	[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr GetDesktopWindow();

	[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr GetWindowDC(IntPtr windowHandle);

	[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern int ReleaseDC(IntPtr windowHandle, IntPtr dc);

	[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern bool ShowWindow(IntPtr handle, int message);

	[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

	[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

	[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern bool SetLayeredWindowAttributes(HandleRef hwnd, int crKey, byte bAlpha, int dwFlags);

	[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

	[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern bool DeleteDC(IntPtr hdc);

	[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

	[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern bool DeleteObject(IntPtr hObject);

	public static Image CaptureActiveWindow()
	{
		SendKeys.Send("%{PRTSC}");
		Thread.Sleep(10);
		Bitmap result = null;
		if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap))
		{
			result = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
		}
		return result;
	}

	private static Image CaptureScreenRegion(Point location, Size size, RasterOperations opcode)
	{
		Image image = new Bitmap(size.Width, size.Height);
		using Graphics graphics = Graphics.FromImage(image);
		IntPtr hdc = graphics.GetHdc();
		IntPtr windowDC = GetWindowDC(IntPtr.Zero);
		BitBlt(hdc, 0, 0, size.Width, size.Height, windowDC, location.X, location.Y, opcode);
		ReleaseDC(IntPtr.Zero, windowDC);
		graphics.ReleaseHdc(hdc);
		return image;
	}

	public static Image CopyImageBits(Image currentImage, Rectangle region)
	{
		Image image = new Bitmap(region.Width, region.Height);
		using Graphics graphics = Graphics.FromImage(image);
		graphics.DrawImage(currentImage, 0, 0, region, GraphicsUnit.Pixel);
		return image;
	}

	public static Bitmap CopyBitmapBitsUsingBitBlt(Bitmap currentImage, Rectangle region)
	{
		Bitmap result = new Bitmap(region.Width, region.Height);
		using Graphics graphics = Graphics.FromHdc(GetWindowDC(IntPtr.Zero));
		graphics.DrawRectangle(Pens.Black, 0, 0, 100, 100);
		return result;
	}

	public static Image StretchImage(Point p, Rectangle zoomRegion, Size desiredSize, RasterOperations opcode)
	{
		Image image = new Bitmap(desiredSize.Width, desiredSize.Height);
		using Graphics graphics = Graphics.FromImage(image);
		IntPtr hdc = graphics.GetHdc();
		IntPtr windowDC = GetWindowDC(IntPtr.Zero);
		StretchBlt(hdc, 0, 0, desiredSize.Width, desiredSize.Height, windowDC, p.X, p.Y, zoomRegion.Width, zoomRegion.Height, RasterOperations.SRCCOPY);
		ReleaseDC(IntPtr.Zero, windowDC);
		graphics.ReleaseHdc(hdc);
		return image;
	}

	public static Image StretchImage(Point p, Rectangle zoomRegion, Size desiredSize, RasterOperations opcode, bool captureLayeredWindows)
	{
		Image image = new Bitmap(desiredSize.Width, desiredSize.Height);
		using Graphics graphics = Graphics.FromImage(image);
		IntPtr hdc = graphics.GetHdc();
		IntPtr windowDC = GetWindowDC(IntPtr.Zero);
		StretchBlt(hdc, 0, 0, desiredSize.Width, desiredSize.Height, windowDC, p.X, p.Y, zoomRegion.Width, zoomRegion.Height, opcode);
		ReleaseDC(IntPtr.Zero, windowDC);
		graphics.ReleaseHdc(hdc);
		return image;
	}
}
