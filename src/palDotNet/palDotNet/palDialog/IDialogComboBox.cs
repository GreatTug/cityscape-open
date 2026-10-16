using System.Windows.Forms;

namespace palDotNet.palDialog;

public interface IDialogComboBox
{
	ComboBox.ObjectCollection Items { get; }
}
