using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using palDotNet.palCore;

namespace palDotNet.palDialog;

public class DialogBoxBaseControlSettings : palDotNet.palCore.ApplicationSettingsBase
{
	[UserScopedSetting]
	public List<DictionaryEntry> DynamicProperties
	{
		get
		{
			return (List<DictionaryEntry>)this["DynamicProperties"];
		}
		set
		{
			this["DynamicProperties"] = value;
		}
	}

	public DialogBoxBaseControlSettings()
	{
	}

	public DialogBoxBaseControlSettings(string settingsKey)
		: base(settingsKey)
	{
	}

	public DialogBoxBaseControlSettings(IComponent owner)
		: base(owner)
	{
	}

	public DialogBoxBaseControlSettings(IComponent owner, string settingsKey)
		: base(owner, settingsKey)
	{
	}

	public static object GetDynamicProperty(List<DictionaryEntry> properties, string key)
	{
		if (properties != null)
		{
			foreach (DictionaryEntry property in properties)
			{
				if (property.Key.ToString() == key)
				{
					return property.Value;
				}
			}
		}
		return null;
	}
}
