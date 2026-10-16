using System;
using System.Windows.Forms;

namespace Sano.PersonalProjects.ColorPicker;

internal class ComponentTextBox : TextBox
{
	internal ComponentTextBox()
	{
		MaxLength = 3;
	}

	protected override bool IsInputKey(Keys keyData)
	{
		bool result = base.IsInputKey(keyData);
		if (keyData > Keys.Help && keyData < (Keys.Print | Keys.ShiftKey))
		{
			result = true;
		}
		return result;
	}

	protected override bool ProcessDialogKey(Keys keyData)
	{
		bool result = true;
		if (keyData == Keys.Tab || keyData == Keys.Back || keyData == (Keys.Tab | Keys.Shift))
		{
			result = base.ProcessDialogKey(keyData);
		}
		return result;
	}

	protected override void OnLostFocus(EventArgs e)
	{
		if (Text.Trim().Equals(string.Empty))
		{
			Text = "0";
		}
	}
}
