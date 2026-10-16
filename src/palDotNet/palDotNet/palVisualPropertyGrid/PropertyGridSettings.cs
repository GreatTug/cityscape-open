using System.ComponentModel;
using System.Configuration;
using palDotNet.palCore;

namespace palDotNet.palVisualPropertyGrid;

public class PropertyGridSettings : palDotNet.palCore.ApplicationSettingsBase
{
	[UserScopedSetting]
	public double? LabelColumnWidthRatio
	{
		get
		{
			return (double?)this["LabelColumnWidthRatio"];
		}
		set
		{
			this["LabelColumnWidthRatio"] = value;
		}
	}

	[UserScopedSetting]
	public int? CommentsHeight
	{
		get
		{
			return (int?)this["CommentsHeight"];
		}
		set
		{
			this["CommentsHeight"] = value;
		}
	}

	public PropertyGridSettings()
	{
	}

	public PropertyGridSettings(string settingsKey)
		: base(settingsKey)
	{
	}

	public PropertyGridSettings(IComponent owner)
		: base(owner)
	{
	}

	public PropertyGridSettings(IComponent owner, string settingsKey)
		: base(owner, settingsKey)
	{
	}
}
