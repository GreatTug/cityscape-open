using System.ComponentModel;
using System.Configuration;
using palDotNet.palCore;

namespace palDotNet.palVisual;

public class CommandBarSettings : palDotNet.palCore.ApplicationSettingsBase
{
	[UserScopedSetting]
	public string[] AutoCompleteItems
	{
		get
		{
			return (string[])this["AutoCompleteItems"];
		}
		set
		{
			this["AutoCompleteItems"] = value;
		}
	}

	public CommandBarSettings()
	{
	}

	public CommandBarSettings(IComponent owner)
		: base(owner)
	{
	}
}
