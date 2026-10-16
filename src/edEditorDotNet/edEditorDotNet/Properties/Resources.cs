using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace edEditorDotNet.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(resourceMan, null))
			{
				ResourceManager resourceManager = new ResourceManager("edEditorDotNet.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Bitmap AboutLogo
	{
		get
		{
			object obj = ResourceManager.GetObject("AboutLogo", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static string AboutText => ResourceManager.GetString("AboutText", resourceCulture);

	internal static Bitmap ActivationIcon64
	{
		get
		{
			object obj = ResourceManager.GetObject("ActivationIcon64", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Icon AppIcon
	{
		get
		{
			object obj = ResourceManager.GetObject("AppIcon", resourceCulture);
			return (Icon)obj;
		}
	}

	internal static Bitmap AutodeskChart
	{
		get
		{
			object obj = ResourceManager.GetObject("AutodeskChart", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap backgroundgradient
	{
		get
		{
			object obj = ResourceManager.GetObject("backgroundgradient", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap CarControls
	{
		get
		{
			object obj = ResourceManager.GetObject("CarControls", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Check16
	{
		get
		{
			object obj = ResourceManager.GetObject("Check16", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Error16
	{
		get
		{
			object obj = ResourceManager.GetObject("Error16", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Export
	{
		get
		{
			object obj = ResourceManager.GetObject("Export", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Info16
	{
		get
		{
			object obj = ResourceManager.GetObject("Info16", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap SplashBanner
	{
		get
		{
			object obj = ResourceManager.GetObject("SplashBanner", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Warning32
	{
		get
		{
			object obj = ResourceManager.GetObject("Warning32", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal Resources()
	{
	}
}
