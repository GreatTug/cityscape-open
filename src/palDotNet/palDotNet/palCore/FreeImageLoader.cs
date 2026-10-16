using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palCore;

internal class FreeImageLoader
{
	public static FreeImageLoader Instance = new FreeImageLoader();

	private static Type FreeImageBitmapType;

	private static MethodInfo ConvertMethod;

	public Bitmap Load(string imagePath, int msTimeout, out bool timedOut)
	{
		Bitmap result = null;
		if (msTimeout >= 0)
		{
			if (!Monitor.TryEnter(this, msTimeout))
			{
				timedOut = true;
				return null;
			}
		}
		else
		{
			Monitor.Enter(this);
		}
		if (ConvertMethod == null)
		{
			string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
			string text = Path.Combine(directoryName, "FreeImageNET.dll");
			try
			{
				Assembly assembly = Assembly.LoadFrom(text);
				FreeImageBitmapType = assembly.GetType("FreeImageAPI.FreeImageBitmap");
				ConvertMethod = FreeImageBitmapType.GetMethod("op_Implicit", new Type[1] { FreeImageBitmapType });
			}
			catch (Exception ex)
			{
				Output.WriteLine("failed to load dll '" + text + "': " + ex.Message);
			}
		}
		if (FreeImageBitmapType != null && ConvertMethod != null)
		{
			object obj = Activator.CreateInstance(FreeImageBitmapType, imagePath);
			result = ConvertMethod.Invoke(null, new object[1] { obj }) as Bitmap;
		}
		timedOut = false;
		Monitor.Exit(this);
		return result;
	}
}
