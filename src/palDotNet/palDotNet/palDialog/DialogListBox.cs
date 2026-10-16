using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogListBox : ListBox, IDialogValue, IDialogListBox
{
	public override SelectionMode SelectionMode
	{
		get
		{
			return base.SelectionMode;
		}
		set
		{
			if (value != SelectionMode.One)
			{
				throw new ApplicationException("Only single section mode is supported for DialogListBoxes.");
			}
			base.SelectionMode = value;
		}
	}

	[ReadOnly(true)]
	[DialogValueType(EnumDialogValueTypes.eString)]
	public object DialogValue
	{
		get
		{
			if (SelectedIndex < 0 || base.Items[SelectedIndex] == null)
			{
				return "";
			}
			return base.Items[SelectedIndex];
		}
		set
		{
			LastRequestedValue = string.Empty;
			for (int i = 0; i < base.Items.Count; i++)
			{
				if (base.Items[i].Equals(value))
				{
					SelectedIndex = i;
					return;
				}
			}
			LastRequestedValue = (string)value;
		}
	}

	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	[ReadOnly(true)]
	public string LastRequestedValue { get; private set; }

	public DialogListBox()
	{
		SaveToConfig = true;
	}

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (object item in base.Items)
		{
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Append("|");
			}
			stringBuilder.Append(item.ToString());
		}
		string caption = string.Concat("script command: ", Environment.NewLine, "'", dialogName, " -", varName, " <", stringBuilder, ">'");
		toolTip.SetToolTip(this, caption);
	}
}
