using System.Windows.Forms;

namespace palDotNet.palDialog;

public interface IDialogValue
{
	object DialogValue { get; set; }

	bool LoadedFromSettings { get; set; }

	bool SaveToConfig { get; set; }

	string Name { get; }

	void AddToolTip(ToolTip toolTip, string dialogName, string varName);
}
