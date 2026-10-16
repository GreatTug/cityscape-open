using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogRadioButton : RadioButton, IDialogValue
{
	[ReadOnly(true)]
	[DialogValueType(EnumDialogValueTypes.eBoolean)]
	public object DialogValue
	{
		get
		{
			return base.Checked;
		}
		set
		{
			base.Checked = Convert.ToBoolean(value);
		}
	}

	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	public DialogRadioButton()
	{
		SaveToConfig = true;
	}

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
		string caption = "script command: " + Environment.NewLine + "'" + dialogName + " -" + varName + " <true|false>'";
		toolTip.SetToolTip(this, caption);
	}
}
