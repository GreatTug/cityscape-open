using System.Windows.Forms;

namespace palDotNet.palDialog;

public interface IDialogContainer
{
	Control.ControlCollection Controls { get; }
}
