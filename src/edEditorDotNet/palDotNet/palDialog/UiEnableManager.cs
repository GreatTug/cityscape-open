using System.Collections;
using System.Windows.Forms;
using edEditorDotNet;

namespace palDotNet.palDialog;

internal class UiEnableManager
{
	private abstract class EnableElementData
	{
		private bool wasEnabled;

		public abstract bool Enabled { set; }

		public bool WasEnabled
		{
			get
			{
				return wasEnabled;
			}
			set
			{
				wasEnabled = value;
			}
		}

		public EnableElementData()
		{
			wasEnabled = false;
		}

		public virtual void Update()
		{
		}

		protected void EnableParent(Control parent)
		{
			if (parent != null)
			{
				EnableParent(parent.Parent);
				parent.Enabled = true;
			}
		}
	}

	private class RadioButtonData : EnableElementData
	{
		private DialogRadioButton radioButton;

		public override bool Enabled
		{
			set
			{
				if (radioButton.Enabled != value)
				{
					if (value)
					{
						EnableParent(radioButton.Parent);
					}
					radioButton.Enabled = value;
				}
			}
		}

		public RadioButtonData(DialogRadioButton rb)
		{
			radioButton = rb;
		}
	}

	private class CheckBoxData : EnableElementData
	{
		private DialogCheckBox checkBox;

		private bool prevValue;

		public override bool Enabled
		{
			set
			{
				if (checkBox.Enabled != value)
				{
					if (value)
					{
						checkBox.Checked = prevValue;
					}
					else
					{
						prevValue = checkBox.Checked;
						checkBox.Checked = false;
					}
					if (value)
					{
						EnableParent(checkBox.Parent);
					}
					checkBox.Enabled = value;
				}
			}
		}

		public CheckBoxData(DialogCheckBox cb)
		{
			checkBox = cb;
			prevValue = checkBox.Checked;
		}

		public override void Update()
		{
			if (checkBox.Enabled)
			{
				prevValue = checkBox.Checked;
			}
		}
	}

	private class CheckBoxTextData : EnableElementData
	{
		private string originalText;

		private string newText;

		private DialogCheckBox checkBox;

		public override bool Enabled
		{
			set
			{
				if (value)
				{
					if (checkBox.Text != newText)
					{
						checkBox.Text = newText;
					}
				}
				else if (checkBox.Text != originalText)
				{
					checkBox.Text = originalText;
				}
			}
		}

		public CheckBoxTextData(DialogCheckBox cb, string text)
		{
			checkBox = cb;
			originalText = checkBox.Text;
			newText = text;
		}
	}

	private class MergeOptionData : EnableElementData
	{
		private ExportOptions.MergingOption option;

		public override bool Enabled
		{
			set
			{
				option.Merge = value;
			}
		}

		public MergeOptionData(ExportOptions.MergingOption mo)
		{
			option = mo;
		}
	}

	private Hashtable uiElements;

	public UiEnableManager()
	{
		uiElements = new Hashtable();
	}

	public void AddCheckBox(DialogCheckBox checkBox)
	{
		uiElements.Add(checkBox.Name, new CheckBoxData(checkBox));
	}

	public void AddRadioButton(DialogRadioButton radioButton)
	{
		uiElements.Add(radioButton.Name, new RadioButtonData(radioButton));
	}

	public void AddCheckBoxPostfix(DialogCheckBox checkBox, string postFix)
	{
		uiElements.Add(checkBox.Name + "Text", new CheckBoxTextData(checkBox, checkBox.Text + postFix));
	}

	public void AddMerge(ExportOptions.MergingOption option)
	{
		uiElements.Add(option.Name, new MergeOptionData(option));
	}

	public void EnableElement(string name)
	{
		if (uiElements[name] is EnableElementData enableElementData)
		{
			enableElementData.Enabled = true;
			enableElementData.WasEnabled = true;
		}
	}

	public void UpdateValues()
	{
		foreach (EnableElementData value in uiElements.Values)
		{
			value.Update();
		}
	}

	public void DisableUnset()
	{
		foreach (EnableElementData value in uiElements.Values)
		{
			if (!value.WasEnabled)
			{
				value.Enabled = false;
			}
			else
			{
				value.WasEnabled = false;
			}
		}
	}
}
