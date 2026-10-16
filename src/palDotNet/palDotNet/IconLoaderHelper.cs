using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace palDotNet;

[StructLayout(LayoutKind.Sequential, Size = 1)]
internal struct IconLoaderHelper
{
	[Flags]
	private enum EnumFileInfoFlags : uint
	{
		LARGEICON = 0u,
		SMALLICON = 1u,
		OPENICON = 2u,
		SHELLICONSIZE = 4u,
		PIDL = 8u,
		USEFILEATTRIBUTES = 0x10u,
		ADDOVERLAYS = 0x20u,
		OVERLAYINDEX = 0x40u,
		ICON = 0x100u,
		DISPLAYNAME = 0x200u,
		TYPENAME = 0x400u,
		ATTRIBUTES = 0x800u,
		ICONLOCATION = 0x1000u,
		EXETYPE = 0x2000u,
		SYSICONINDEX = 0x4000u,
		LINKOVERLAY = 0x8000u,
		SELECTED = 0x10000u,
		ATTR_SPECIFIED = 0x20000u
	}

	public enum EnumIconSize
	{
		Large,
		Small
	}

	public enum EnumFolderType
	{
		Open,
		Closed
	}

	private struct ShellFileInfo
	{
		public const int conNameSize = 80;

		public IntPtr hIcon;

		public int iIndex;

		public uint dwAttributes;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string szDisplayName;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
		public string szTypeName;
	}

	private const int conMAX_PATH = 260;

	private const uint conFILE_ATTRIBUTE_DIRECTORY = 16u;

	private const uint conFILE_ATTRIBUTE_NORMAL = 128u;

	public static Bitmap Load(string name, Size? size)
	{
		Icon icon = null;
		return Load(name, size, out icon);
	}

	public static Bitmap Load(string name, Size? size, out Icon icon)
	{
		icon = null;
		if (name != string.Empty)
		{
			icon = LoadFromResource(name);
			if (icon != null)
			{
				if (size.HasValue)
				{
					Icon icon2 = new Icon(icon, size.Value);
					return icon2.ToBitmap();
				}
				return null;
			}
			try
			{
				string text = name;
				if (!Path.IsPathRooted(text))
				{
					string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
					text = Path.Combine(directoryName, text);
				}
				if (File.Exists(text))
				{
					if (text.EndsWith(".ico"))
					{
						if (size.HasValue)
						{
							icon = new Icon(text, size.Value);
							return icon.ToBitmap();
						}
						icon = new Icon(text);
						return icon.ToBitmap();
					}
					if (size.HasValue)
					{
						return new Bitmap(Image.FromFile(text), size.Value);
					}
					return new Bitmap(Image.FromFile(text));
				}
			}
			catch (FileNotFoundException)
			{
			}
			try
			{
				IconLoaderHelper iconLoaderHelper = default(IconLoaderHelper);
				if (name.EndsWith(".ico"))
				{
					icon = new Icon(iconLoaderHelper.GetType(), "Icons." + name);
					if (size.HasValue)
					{
						Icon icon3 = new Icon(icon, size.Value);
						return icon3.ToBitmap();
					}
					return null;
				}
				if (size.HasValue)
				{
					return new Bitmap(iconLoaderHelper.GetType(), "Icons." + name);
				}
				return new Bitmap(iconLoaderHelper.GetType(), "Icons." + name);
			}
			catch (Exception)
			{
			}
		}
		return null;
	}

	private static Icon LoadFromResource(string path)
	{
		string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
		Icon icon = null;
		if (path.StartsWith("$Dll:"))
		{
			string[] array = path.Split(new char[1] { '$' }, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 2)
			{
				string text = null;
				string text2 = null;
				if (array[0].StartsWith("Dll:"))
				{
					text = array[0].Replace("Dll:", "");
					text = Path.Combine(directoryName, text);
				}
				if (array[1].StartsWith("Resource:"))
				{
					text2 = array[1].Replace("Resource:", "");
				}
				if (string.IsNullOrEmpty(text))
				{
					throw new Exception("dll path wasn't found");
				}
				if (string.IsNullOrEmpty(text2))
				{
					throw new Exception("Resource name wasn't found");
				}
				Assembly assembly = Assembly.LoadFrom(text);
				if (assembly != null)
				{
					if (Path.GetExtension(text2).ToLower() != ".ico")
					{
						Bitmap bitmap = new Bitmap(assembly.GetManifestResourceStream(text2));
						icon = Icon.FromHandle(bitmap.GetHicon());
					}
					else
					{
						icon = new Icon(assembly.GetManifestResourceStream(text2));
					}
					if (icon == null)
					{
						throw new Exception("Failed to find icon in " + text);
					}
				}
			}
		}
		return icon;
	}

	[DllImport("User32.dll")]
	private static extern int DestroyIcon(IntPtr hIcon);

	[DllImport("Shell32.dll")]
	private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref ShellFileInfo psfi, uint cbFileInfo, uint uFlags);

	public static Icon GetFileIcon(string filePath, EnumIconSize size, bool addLinkOverlay)
	{
		EnumFileInfoFlags enumFileInfoFlags = EnumFileInfoFlags.USEFILEATTRIBUTES | EnumFileInfoFlags.ICON;
		if (addLinkOverlay)
		{
			enumFileInfoFlags |= EnumFileInfoFlags.LINKOVERLAY;
		}
		enumFileInfoFlags = ((size != EnumIconSize.Small) ? enumFileInfoFlags : (enumFileInfoFlags | EnumFileInfoFlags.SMALLICON));
		ShellFileInfo psfi = default(ShellFileInfo);
		SHGetFileInfo(filePath, 128u, ref psfi, (uint)Marshal.SizeOf(psfi), (uint)enumFileInfoFlags);
		Icon result = (Icon)Icon.FromHandle(psfi.hIcon).Clone();
		DestroyIcon(psfi.hIcon);
		return result;
	}

	public static Icon GetFolderIcon(EnumIconSize size, EnumFolderType folderType)
	{
		return GetFolderIcon(null, size, folderType);
	}

	public static Icon GetFolderIcon(string folderPath, EnumIconSize size, EnumFolderType folderType)
	{
		EnumFileInfoFlags enumFileInfoFlags = EnumFileInfoFlags.USEFILEATTRIBUTES | EnumFileInfoFlags.ICON;
		if (folderType == EnumFolderType.Open)
		{
			enumFileInfoFlags |= EnumFileInfoFlags.OPENICON;
		}
		enumFileInfoFlags = ((EnumIconSize.Small != size) ? enumFileInfoFlags : (enumFileInfoFlags | EnumFileInfoFlags.SMALLICON));
		ShellFileInfo psfi = default(ShellFileInfo);
		SHGetFileInfo(folderPath, 16u, ref psfi, (uint)Marshal.SizeOf(psfi), (uint)enumFileInfoFlags);
		Icon result = (Icon)Icon.FromHandle(psfi.hIcon).Clone();
		DestroyIcon(psfi.hIcon);
		return result;
	}
}
