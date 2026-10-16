using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace palDotNet.Properties;

[DebuggerNonUserCode]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
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
				ResourceManager resourceManager = new ResourceManager("palDotNet.Properties.Resources", typeof(Resources).Assembly);
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

	internal static Bitmap Anchor
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Anchor_BottomLeft
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor_BottomLeft", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Anchor_BottomMiddle
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor_BottomMiddle", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Anchor_BottomRight
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor_BottomRight", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Anchor_MiddleGrid
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor_MiddleGrid", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Anchor_MiddleLeft
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor_MiddleLeft", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Anchor_MiddleRight
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor_MiddleRight", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Anchor_TopLeft
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor_TopLeft", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Anchor_TopMiddle
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor_TopMiddle", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Anchor_TopRight
	{
		get
		{
			object obj = ResourceManager.GetObject("Anchor_TopRight", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static byte[] ColorFieldPanelCursor
	{
		get
		{
			object obj = ResourceManager.GetObject("ColorFieldPanelCursor", resourceCulture);
			return (byte[])obj;
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

	internal static Bitmap GridStrip
	{
		get
		{
			object obj = ResourceManager.GetObject("GridStrip", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap help
	{
		get
		{
			object obj = ResourceManager.GetObject("help", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap ico_minus
	{
		get
		{
			object obj = ResourceManager.GetObject("ico_minus", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap ico_plus
	{
		get
		{
			object obj = ResourceManager.GetObject("ico_plus", resourceCulture);
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

	internal static Bitmap Open16
	{
		get
		{
			object obj = ResourceManager.GetObject("Open16", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static byte[] Pan
	{
		get
		{
			object obj = ResourceManager.GetObject("Pan", resourceCulture);
			return (byte[])obj;
		}
	}

	internal static byte[] PanDown
	{
		get
		{
			object obj = ResourceManager.GetObject("PanDown", resourceCulture);
			return (byte[])obj;
		}
	}

	internal static Bitmap views
	{
		get
		{
			object obj = ResourceManager.GetObject("views", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Warning16
	{
		get
		{
			object obj = ResourceManager.GetObject("Warning16", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static byte[] ZoomIn
	{
		get
		{
			object obj = ResourceManager.GetObject("ZoomIn", resourceCulture);
			return (byte[])obj;
		}
	}

	internal static byte[] ZoomOut
	{
		get
		{
			object obj = ResourceManager.GetObject("ZoomOut", resourceCulture);
			return (byte[])obj;
		}
	}

	internal Resources()
	{
	}
}
