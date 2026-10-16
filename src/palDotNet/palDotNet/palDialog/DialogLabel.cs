using System.ComponentModel;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogLabel : Label, IDialogValue
{
	[ReadOnly(true)]
	[DialogValueType(EnumDialogValueTypes.eString)]
	public object DialogValue
	{
		get
		{
			return Text;
		}
		set
		{
			Text = (string)value;
		}
	}

	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	public DialogLabel()
	{
		SaveToConfig = false;
	}

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.ResumeLayout(false);
	}
}
