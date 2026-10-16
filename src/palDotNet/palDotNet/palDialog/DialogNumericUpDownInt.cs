using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogNumericUpDownInt : DialogNumericUpDown, IDialogValue
{
	[DialogValueType(EnumDialogValueTypes.eInt)]
	[ReadOnly(true)]
	public object DialogValue
	{
		get
		{
			return Convert.ToInt32(base.Value);
		}
		set
		{
			decimal val = Convert.ToDecimal(value);
			val = Math.Max(base.Minimum, val);
			val = Math.Min(base.Maximum, val);
			base.Value = val;
		}
	}

	public override void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
		string caption = "script command: " + Environment.NewLine + "'" + dialogName + " -" + varName + " <integer>'";
		toolTip.SetToolTip(this, caption);
	}
}
