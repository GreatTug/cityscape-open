using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class HexTextBox : ColorSpaceComponentTextBox
{
	private const int MINIMUM_VALUE = 0;

	private const int MAXIMUM_VALUE = 16777215;

	[DefaultValue("000000")]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	internal HexTextBox()
	{
		MaxLength = 6;
		Text = string.Empty;
	}

	protected override bool IsInputChar(char charCode)
	{
		bool result = base.IsInputChar(charCode);
		if (charCode >= 'A' && charCode <= 'F')
		{
			result = true;
		}
		return result;
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (Text.Length > 0 && ((e.KeyData | Keys.Shift) == (Keys.Up | Keys.Shift) || (e.KeyData | Keys.Shift) == (Keys.Down | Keys.Shift)))
		{
			string value = $"0x{Text}";
			int num = Convert.ToInt32(value, 16);
			int num2 = 0;
			num2 = (((e.KeyData & Keys.Shift) != Keys.Shift) ? 1 : 10);
			Text = (((e.KeyData & Keys.Up) == Keys.Up) ? ((num + num2 > 16777215) ? 16777215 : (num + num2)) : ((num - num2 >= 0) ? (num - num2) : 0)).ToString("x6").ToUpper();
			if (e.KeyData == (Keys.Down | Keys.Shift))
			{
				base.SelectionStart = Text.Length;
			}
		}
	}
}
