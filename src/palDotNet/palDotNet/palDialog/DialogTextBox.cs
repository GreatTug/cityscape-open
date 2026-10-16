using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogTextBox : TextBox, IDialogValue
{
	[ReadOnly(true)]
	[DialogValueType(EnumDialogValueTypes.eString)]
	public object DialogValue
	{
		get
		{
			return Text;
		}
		set
		{
			Text = (string)value;
		}
	}

	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	public DialogTextBox()
	{
		SaveToConfig = true;
	}

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
		string caption = "script command: " + Environment.NewLine + "'" + dialogName + " -" + varName + " <text>'";
		toolTip.SetToolTip(this, caption);
	}

	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			e.Handled = true;
		}
		else
		{
			base.OnKeyPress(e);
		}
	}
}
