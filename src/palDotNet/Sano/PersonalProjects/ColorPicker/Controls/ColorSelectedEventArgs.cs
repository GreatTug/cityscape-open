using System;
using System.Drawing;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class ColorSelectedEventArgs : EventArgs
{
	private Color m_color;

	internal Color Color => m_color;

	internal ColorSelectedEventArgs(Color color)
	{
		m_color = color;
	}
}
