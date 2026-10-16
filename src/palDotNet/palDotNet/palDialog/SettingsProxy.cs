using System.Collections;
using System.Collections.Generic;

namespace palDotNet.palDialog;

public class SettingsProxy
{
	private Dictionary<string, DialogBoxBase> SettingsHash = new Dictionary<string, DialogBoxBase>();

	public List<DictionaryEntry> GetSettings(string type, string dllName, string settingsKey)
	{
		if (!SettingsHash.ContainsKey(settingsKey))
		{
			new DialogBoxBaseControlSettings(settingsKey);
			DialogBoxBase dialogBoxBase = DialogBoxBase.CreateDialog(null, "<dummy name>", type, dllName);
			if (dialogBoxBase != null)
			{
				SettingsHash.Add(settingsKey, dialogBoxBase);
				dialogBoxBase.OverriddenSettingsKey = settingsKey;
				dialogBoxBase.LoadComponentSettings();
				dialogBoxBase.SaveComponentSettings();
				return dialogBoxBase.Settings.DynamicProperties;
			}
			return null;
		}
		return SettingsHash[settingsKey].Settings.DynamicProperties;
	}
}
