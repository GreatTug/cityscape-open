using System.ComponentModel;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogDataString : Component, IDialogValue
{
	private IContainer components;

	[ReadOnly(true)]
	[DialogValueType(EnumDialogValueTypes.eString)]
	public object DialogValue { get; set; }

	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	public string Name { get; set; }

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
		components = new Container();
	}

	public DialogDataString()
	{
		SaveToConfig = true;
		InitializeComponent();
		DialogValue = "";
	}

	public DialogDataString(IContainer container)
	{
		container.Add(this);
		InitializeComponent();
		DialogValue = "";
	}

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
	}
}
