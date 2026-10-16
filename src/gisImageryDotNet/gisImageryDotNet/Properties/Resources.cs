using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace gisImageryDotNet.Properties;

[CompilerGenerated]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
[DebuggerNonUserCode]
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
				ResourceManager resourceManager = new ResourceManager("gisImageryDotNet.Properties.Resources", typeof(Resources).Assembly);
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

	internal static Bitmap Info16
	{
		get
		{
			object obj = ResourceManager.GetObject("Info16", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal Resources()
	{
	}
}
