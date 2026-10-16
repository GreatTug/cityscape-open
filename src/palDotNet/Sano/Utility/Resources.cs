using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Sano.Utility;

public sealed class Resources
{
	private Resources()
	{
	}

	public static Bitmap GetBitmapResource(string resourceFile)
	{
		Bitmap bitmap = null;
		if ((resourceFile != null) & (resourceFile.Length > 0))
		{
			Stream resourceStream = GetResourceStream(resourceFile);
			bitmap = (Bitmap)Image.FromStream(resourceStream);
			resourceStream.Close();
			return bitmap;
		}
		throw new ArgumentNullException("resourceFile", "resourceFile cannot be empty");
	}

	public static Icon GetIconResource(string resourceFile)
	{
		Icon icon = null;
		if ((resourceFile != null) & (resourceFile.Length > 0))
		{
			Stream resourceStream = GetResourceStream(resourceFile);
			icon = new Icon(resourceStream);
			resourceStream.Close();
			return icon;
		}
		throw new ArgumentNullException("resourceFile", "resourceFile cannot be empty.");
	}

	public static Stream GetFileResource(string resourceFile)
	{
		Stream result = null;
		if ((resourceFile != null) & (resourceFile.Length > 0))
		{
			result = GetResourceStream(resourceFile);
		}
		return result;
	}

	private static string CreateFullResourcePath(string resourceNamespace, string resourceFile)
	{
		string empty = string.Empty;
		if (resourceNamespace != null && resourceNamespace.Length > 0)
		{
			return $"{resourceNamespace}.{resourceFile}";
		}
		return resourceFile;
	}

	private static Stream GetResourceStream(string resourceFile)
	{
		StackTrace stackTrace = new StackTrace();
		StackFrame frame = stackTrace.GetFrame(2);
		MethodBase method = frame.GetMethod();
		Type declaringType = method.DeclaringType;
		Assembly assembly = declaringType.Assembly;
		return assembly.GetManifestResourceStream(CreateFullResourcePath(declaringType.Namespace, resourceFile));
	}
}
