using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using palDotNet.palCore;

namespace palDotNet.palVisual;

public class ListViewControlSettings : palDotNet.palCore.ApplicationSettingsBase
{
	[UserScopedSetting]
	public View? View
	{
		get
		{
			return (View?)this["View"];
		}
		set
		{
			this["View"] = value;
		}
	}

	public ListViewControlSettings(IComponent owner, string settingsKey)
		: base(owner, settingsKey)
	{
	}

	public ListViewControlSettings(string settingsKey)
		: base(settingsKey)
	{
	}

	public ListViewControlSettings(IComponent owner)
		: base(owner)
	{
	}

	public ListViewControlSettings()
	{
	}
}
