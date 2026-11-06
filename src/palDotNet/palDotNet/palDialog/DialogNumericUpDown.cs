using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogNumericUpDown : NumericUpDown
{
	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	public DialogNumericUpDown()
	{
		SaveToConfig = true;
	}

	public virtual void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
	}

	protected override void OnTextBoxKeyPress(object src, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ValidateEditText();
			e.Handled = true;
		}
		else
		{
			base.OnTextBoxKeyPress(src, e);
		}
	}

	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus(e);
		Select(0, Text.Length);
	}
}
