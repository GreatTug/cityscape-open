using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sano.PersonalProjects.ColorPicker;

public class ColorDragForm : Form
{
	private Container components;

	public ColorDragForm()
	{
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.BackColor = System.Drawing.Color.Magenta;
		base.ClientSize = new System.Drawing.Size(85, 37);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "ColorDragForm";
		base.ShowInTaskbar = false;
		this.Text = "ColorDragForm";
		base.TransparencyKey = System.Drawing.Color.Indigo;
	}
}
