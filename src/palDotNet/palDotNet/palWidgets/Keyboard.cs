using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace palDotNet.palWidgets;

public abstract class Keyboard
{
	[Flags]
	private enum KeyStates
	{
		None = 0,
		Down = 1,
		Toggled = 2
	}

	public enum Modifiers
	{
		Menu,
		ShiftKey,
		ControlKey
	}

	private const int NumModifiers = 3;

	public static readonly bool[] ModifiersPressed = new bool[3];

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	private static extern short GetKeyState(int keyCode);

	private static KeyStates GetKeyState(Keys key)
	{
		KeyStates keyStates = KeyStates.None;
		short keyState = GetKeyState((int)key);
		if ((keyState & 0x8000) == 32768)
		{
			keyStates |= KeyStates.Down;
		}
		if ((keyState & 1) == 1)
		{
			keyStates |= KeyStates.Toggled;
		}
		return keyStates;
	}

	public static bool IsKeyDown(Keys key)
	{
		return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
	}

	public static bool IsKeyToggled(Keys key)
	{
		return KeyStates.Toggled == (GetKeyState(key) & KeyStates.Toggled);
	}

	public static void CheckAndAddModifier(Keys keys)
	{
		switch (keys)
		{
		case Keys.Menu:
			ModifiersPressed[0] = true;
			break;
		case Keys.ShiftKey:
			ModifiersPressed[1] = true;
			break;
		case Keys.ControlKey:
			ModifiersPressed[2] = true;
			break;
		}
	}

	public static void Update(WidgetMgr mgr)
	{
		Keys[] array = new Keys[3]
		{
			Keys.Menu,
			Keys.ShiftKey,
			Keys.ControlKey
		};
		for (int i = 0; i < ModifiersPressed.Length; i++)
		{
			Keys keys = array[i];
			if (ModifiersPressed[i] && !IsKeyDown(keys))
			{
				mgr.SendKeyInfoNoCheck(keys, WidgetBase.CoreMessages.eKeyReleased);
				ModifiersPressed[i] = false;
			}
		}
	}

	public static Keys AlreadySentRelease(Keys keys)
	{
		if ((keys == Keys.Menu && !ModifiersPressed[0]) || (keys == Keys.ShiftKey && !ModifiersPressed[1]) || (keys == Keys.ControlKey && !ModifiersPressed[2]))
		{
			return Keys.None;
		}
		if (keys == Keys.Menu && ModifiersPressed[0])
		{
			ModifiersPressed[0] = false;
		}
		else if (keys == Keys.ShiftKey && ModifiersPressed[1])
		{
			ModifiersPressed[1] = false;
		}
		else if (keys == Keys.ControlKey && ModifiersPressed[2])
		{
			ModifiersPressed[2] = false;
		}
		return keys;
	}
}
