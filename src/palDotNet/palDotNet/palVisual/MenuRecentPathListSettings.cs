using System.ComponentModel;
using System.Configuration;
using palDotNet.palCore;

namespace palDotNet.palVisual;

public class MenuRecentPathListSettings : palDotNet.palCore.ApplicationSettingsBase
{
	[UserScopedSetting]
	public string[] PathList
	{
		get
		{
			return (string[])this["PathList"];
		}
		set
		{
			this["PathList"] = value;
		}
	}

	public MenuRecentPathListSettings(IComponent owner, string settingsKey)
		: base(owner, settingsKey)
	{
	}

	public MenuRecentPathListSettings(string settingsKey)
		: base(settingsKey)
	{
	}

	public MenuRecentPathListSettings(IComponent owner)
		: base(owner)
	{
	}

	public MenuRecentPathListSettings()
	{
	}
}
