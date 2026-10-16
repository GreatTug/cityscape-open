using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogNumericUpDownFloat : DialogNumericUpDown, IDialogValue
{
	[DialogValueType(EnumDialogValueTypes.eFloat)]
	[ReadOnly(true)]
	public object DialogValue
	{
		get
		{
			return base.Value;
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
		string caption = "script command: " + Environment.NewLine + "'" + dialogName + " -" + varName + " <float>'";
		toolTip.SetToolTip(this, caption);
	}
}
