using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogTrackBar : TrackBar, IDialogValue
{
	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.ExStyle |= 32;
			return createParams;
		}
	}

	[ReadOnly(true)]
	[DialogValueType(EnumDialogValueTypes.eInt)]
	public object DialogValue
	{
		get
		{
			return Convert.ToInt32(base.Value);
		}
		set
		{
			base.Value = Convert.ToInt32(value);
		}
	}

	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	public DialogTrackBar()
	{
		SaveToConfig = true;
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
	}

	protected void InvalidateEx()
	{
		if (base.Parent != null)
		{
			Rectangle rc = new Rectangle(base.Location, base.Size);
			base.Parent.Invalidate(rc, invalidateChildren: true);
		}
	}

	protected override void OnPaintBackground(PaintEventArgs pevent)
	{
	}

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
		string caption = "script command: " + Environment.NewLine + "'" + dialogName + " -" + varName + " <text>'";
		toolTip.SetToolTip(this, caption);
	}

	private void InitializeComponent()
	{
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.BackColor = System.Drawing.Color.Transparent;
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
