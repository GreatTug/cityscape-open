using System.Drawing;

namespace palDotNet.palVisual;

public interface IItemViewItem
{
	string DragString { get; }

	Color BackColor { get; set; }
}
