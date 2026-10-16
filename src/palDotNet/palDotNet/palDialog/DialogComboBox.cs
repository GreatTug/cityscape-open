using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogComboBox : ComboBox, IDialogValue, IDialogComboBox
{
	[DialogValueType(EnumDialogValueTypes.eString)]
	[ReadOnly(true)]
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

	public DialogComboBox()
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
