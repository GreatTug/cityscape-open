using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using palDotNet.palCore;

namespace palDotNet.palVisual;

public class LibraryControlSettings : palDotNet.palCore.ApplicationSettingsBase
{
	[UserScopedSetting]
	public int? SplitContainer1SplitterDistance
	{
		get
		{
			return (int?)this["SplitContainer1SplitterDistance"];
		}
		set
		{
			this["SplitContainer1SplitterDistance"] = value;
		}
	}

	[UserScopedSetting]
	public int? SplitContainer2SplitterDistance
	{
		get
		{
			return (int?)this["SplitContainer2SplitterDistance"];
		}
		set
		{
			this["SplitContainer2SplitterDistance"] = value;
		}
	}

	[UserScopedSetting]
	public int? SplitContainer4SplitterDistance
	{
		get
		{
			return (int?)this["SplitContainer4SplitterDistance"];
		}
		set
		{
			this["SplitContainer4SplitterDistance"] = value;
		}
	}

	[UserScopedSetting]
	[DefaultSettingValue("0, 0")]
	public Size? LibraryControlSplitterSize
	{
		get
		{
			return (Size?)this["LibraryControlSplitterSize"];
		}
		set
		{
			this["LibraryControlSplitterSize"] = value;
		}
	}

	public LibraryControlSettings()
	{
	}

	public LibraryControlSettings(string settingsKey)
		: base(settingsKey)
	{
	}

	public LibraryControlSettings(IComponent owner)
		: base(owner)
	{
	}

	public LibraryControlSettings(IComponent owner, string settingsKey)
		: base(owner, settingsKey)
	{
	}
}
