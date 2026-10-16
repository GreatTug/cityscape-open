using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace palDotNet.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[DebuggerNonUserCode]
	[UserScopedSetting]
	[DefaultSettingValue("")]
	public string DockPanelLayout
	{
		get
		{
			return (string)this["DockPanelLayout"];
		}
		set
		{
			this["DockPanelLayout"] = value;
		}
	}

	[DebuggerNonUserCode]
	[UserScopedSetting]
	[DefaultSettingValue("0, 0")]
	public Point FormLocation
	{
		get
		{
			return (Point)this["FormLocation"];
		}
		set
		{
			this["FormLocation"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("Normal")]
	public FormWindowState FormWindowState
	{
		get
		{
			return (FormWindowState)this["FormWindowState"];
		}
		set
		{
			this["FormWindowState"] = value;
		}
	}

	[DefaultSettingValue("False")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public bool Loaded
	{
		get
		{
			return (bool)this["Loaded"];
		}
		set
		{
			this["Loaded"] = value;
		}
	}

	[DebuggerNonUserCode]
	[UserScopedSetting]
	[DefaultSettingValue("0, 0")]
	public Size FormSize
	{
		get
		{
			return (Size)this["FormSize"];
		}
		set
		{
			this["FormSize"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("4")]
	public int FileVersion
	{
		get
		{
			return (int)this["FileVersion"];
		}
		set
		{
			this["FileVersion"] = value;
		}
	}

	[DebuggerNonUserCode]
	[UserScopedSetting]
	[DefaultSettingValue("True")]
	public bool CallUpgrade
	{
		get
		{
			return (bool)this["CallUpgrade"];
		}
		set
		{
			this["CallUpgrade"] = value;
		}
	}
}
