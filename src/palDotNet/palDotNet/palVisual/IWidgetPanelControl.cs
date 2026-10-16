using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public interface IWidgetPanelControl
{
	WidgetPanel WidgetPanel { get; set; }

	bool Visible { get; set; }

	bool CopySettingsTo(string settingsKey, string settingsKeyTo, IWidgetPanelControl panel);

	void SaveComponentSettings(string settingsKey);

	void ResetComponentSettings(string settingsKey);

	void LoadComponentSettings(string settingsKey);
}
