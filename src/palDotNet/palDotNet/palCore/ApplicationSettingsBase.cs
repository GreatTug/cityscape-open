using System.ComponentModel;
using System.Configuration;

namespace palDotNet.palCore;

public class ApplicationSettingsBase : System.Configuration.ApplicationSettingsBase
{
	public static bool DoUpgrade;

	public ApplicationSettingsBase()
	{
		if (DoUpgrade)
		{
			Upgrade();
		}
	}

	public ApplicationSettingsBase(string settingsKey)
		: base(settingsKey)
	{
		if (DoUpgrade)
		{
			Upgrade();
		}
	}

	public ApplicationSettingsBase(IComponent owner)
		: base(owner)
	{
		if (DoUpgrade)
		{
			Upgrade();
		}
	}

	public ApplicationSettingsBase(IComponent owner, string settingsKey)
		: base(owner, settingsKey)
	{
		if (DoUpgrade)
		{
			Upgrade();
		}
	}
}
