using System.Windows.Forms;

namespace palDotNet.palDialog;

public interface IDialogListBox
{
	ListBox.ObjectCollection Items { get; }
}
