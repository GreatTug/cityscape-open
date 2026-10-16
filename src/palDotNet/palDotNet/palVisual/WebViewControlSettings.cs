using System.ComponentModel;
using System.Configuration;
using palDotNet.palCore;

namespace palDotNet.palVisual;

public class WebViewControlSettings : palDotNet.palCore.ApplicationSettingsBase
{
	[UserScopedSetting]
	public string Url
	{
		get
		{
			return (string)this["Url"];
		}
		set
		{
			this["Url"] = value;
		}
	}

	public WebViewControlSettings()
	{
	}

	public WebViewControlSettings(string settingsKey)
		: base(settingsKey)
	{
	}

	public WebViewControlSettings(IComponent owner)
		: base(owner)
	{
	}

	public WebViewControlSettings(IComponent owner, string settingsKey)
		: base(owner, settingsKey)
	{
	}
}
