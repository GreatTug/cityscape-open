using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class ControlRenderComposite : UserControl, ISupportsToolStrip, IRenderWindowVisual
{
	private ContainerControl TheTopToolStripContainer;

	private ContainerControl TheBottomToolStripContainer;

	private ContainerControl TheRightToolStripContainer;

	private ContainerControl TheLeftToolStripContainer;

	private IContainer components;

	private ControlRender controlRender1;

	public Cursors.EnumCursor CurrentCursor
	{
		set
		{
			Cursors.SetCurrent(controlRender1, value);
		}
	}

	[Browsable(false)]
	[ReadOnly(true)]
	public ContainerControl TopToolStripContainer
	{
		get
		{
			return TheTopToolStripContainer;
		}
		set
		{
			TheTopToolStripContainer = value;
		}
	}

	[Browsable(false)]
	[ReadOnly(true)]
	public ContainerControl BottomToolStripContainer
	{
		get
		{
			return TheBottomToolStripContainer;
		}
		set
		{
			TheBottomToolStripContainer = value;
		}
	}

	[ReadOnly(true)]
	[Browsable(false)]
	public ContainerControl LeftToolStripContainer
	{
		get
		{
			return TheLeftToolStripContainer;
		}
		set
		{
			TheLeftToolStripContainer = value;
		}
	}

	[ReadOnly(true)]
	[Browsable(false)]
	public ContainerControl RightToolStripContainer
	{
		get
		{
			return TheRightToolStripContainer;
		}
		set
		{
			TheRightToolStripContainer = value;
		}
	}

	[Browsable(false)]
	[ReadOnly(true)]
	public RenderWindow RenderWindow
	{
		get
		{
			return controlRender1.RenderWindow;
		}
		set
		{
			controlRender1.RenderWindow = value;
		}
	}

	public ControlRenderComposite()
	{
		InitializeComponent();
	}

	public int GetValue(bool isHorizontal)
	{
		return GetValue(isHorizontal);
	}

	public void SetVisible(bool isHorizontal, bool visible)
	{
		controlRender1.SetVisible(isHorizontal, visible);
	}

	public void SetValue(bool isHorizontal, int barValue)
	{
		controlRender1.SetValue(isHorizontal, barValue);
	}

	public void SetLargeChange(bool isHorizontal, int largeChange)
	{
		controlRender1.SetLargeChange(isHorizontal, largeChange);
	}

	public void SetSmallChange(bool isHorizontal, int smallChange)
	{
		controlRender1.SetSmallChange(isHorizontal, smallChange);
	}

	public void SetLimits(bool isHorizontal, int min, int max)
	{
		controlRender1.SetLimits(isHorizontal, min, max);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		if (disposing)
		{
			controlRender1.SetNotVisible();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.controlRender1 = new palDotNet.palVisual.ControlRender();
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.SuspendLayout();
		this.controlRender1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.controlRender1.Location = new System.Drawing.Point(0, 0);
		this.controlRender1.Name = "controlRender1";
		this.controlRender1.Size = new System.Drawing.Size(292, 273);
		this.controlRender1.TabIndex = 0;
		this.controlRender1.Text = "control1";
		base.Controls.Add(this.controlRender1);
		base.ResumeLayout(false);
	}

	void ISupportsToolStrip.SuspendLayout()
	{
		SuspendLayout();
	}

	void ISupportsToolStrip.ResumeLayout()
	{
		ResumeLayout();
	}
}
