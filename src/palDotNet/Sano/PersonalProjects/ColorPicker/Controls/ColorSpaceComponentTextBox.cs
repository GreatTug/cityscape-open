using System;
using System.ComponentModel;
using System.Windows.Forms;
using Sano.Utility.NativeMethods;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class ColorSpaceComponentTextBox : TextBox
{
	private bool m_caretHidden;

	[DefaultValue("0")]
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

	internal ColorSpaceComponentTextBox()
	{
		MaxLength = 3;
	}

	protected override bool IsInputKey(Keys keyData)
	{
		bool result = IsInputChar((char)keyData);
		if (keyData == (Keys.F4 | Keys.Alt) || keyData == (Keys.A | Keys.Control) || keyData == (Keys.C | Keys.Control) || keyData == (Keys.V | Keys.Control) || keyData == (Keys.X | Keys.Control))
		{
			result = true;
		}
		return result;
	}

	protected override bool IsInputChar(char charCode)
	{
		bool result = false;
		Keys modifierKeys = Control.ModifierKeys;
		if ((modifierKeys != Keys.Shift && charCode >= '0' && charCode <= '9') || (charCode >= '%' && charCode <= '(') || charCode == '\b')
		{
			result = true;
		}
		return result;
	}

	protected override void OnKeyUp(KeyEventArgs e)
	{
		if (e.KeyData == (Keys.A | Keys.Control))
		{
			SelectAll();
		}
		if ((IsKeyOrShiftPressed(e.KeyData, Keys.Up) || IsKeyOrShiftPressed(e.KeyData, Keys.Down)) && m_caretHidden)
		{
			Caret.ShowCaret(base.Handle);
			base.SelectionStart = Text.Length;
			m_caretHidden = false;
		}
		base.OnKeyUp(e);
	}

	private bool IsKeyOrShiftPressed(Keys keyData, Keys comparsionKey)
	{
		return (keyData | Keys.Shift) == (comparsionKey | Keys.Shift);
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		if ((IsKeyOrShiftPressed(e.KeyData, Keys.Up) || IsKeyOrShiftPressed(e.KeyData, Keys.Down)) && !m_caretHidden)
		{
			Caret.HideCaret(base.Handle);
			m_caretHidden = true;
		}
		base.OnKeyDown(e);
	}

	protected override bool ProcessKeyEventArgs(ref Message m)
	{
		bool result = false;
		int num = (int)m.WParam;
		int modifierKeys = (int)Control.ModifierKeys;
		if (IsInputKey((Keys)(num | modifierKeys)))
		{
			result = base.ProcessKeyEventArgs(ref m);
		}
		return result;
	}

	protected override bool ProcessDialogKey(Keys keyData)
	{
		bool result = true;
		Keys modifierKeys = Control.ModifierKeys;
		if (modifierKeys == Keys.Alt)
		{
			result = false;
		}
		else if (keyData == Keys.Alt || keyData == Keys.Tab || keyData == Keys.Back || keyData == (Keys.Tab | Keys.Shift))
		{
			result = base.ProcessDialogKey(keyData);
		}
		return result;
	}

	protected override void OnLostFocus(EventArgs e)
	{
		if (Text != null && Text.Length == 0)
		{
			AttributeCollection attributes = TypeDescriptor.GetProperties(this)["Text"].Attributes;
			DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
			Text = defaultValueAttribute.Value.ToString();
		}
		base.OnLostFocus(e);
	}
}
