using System.Windows.Forms;

namespace palDotNet.palVisual;

internal interface ISupportsToolStrip
{
	ContainerControl TopToolStripContainer { get; set; }

	ContainerControl BottomToolStripContainer { get; set; }

	ContainerControl RightToolStripContainer { get; set; }

	ContainerControl LeftToolStripContainer { get; set; }

	Control.ControlCollection Controls { get; }

	int Height { get; set; }

	void SuspendLayout();

	void ResumeLayout();
}
