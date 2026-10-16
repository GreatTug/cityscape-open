using System.Collections.Generic;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogValueEnumerator
{
	private readonly Control.ControlCollection ControlCollection;

	private readonly IDialogValue[] Components;

	public DialogValueEnumerator(Control.ControlCollection controlCollection, IDialogValue[] components)
	{
		ControlCollection = controlCollection;
		Components = components;
	}

	public IEnumerator<IDialogValue> GetEnumerator()
	{
		foreach (Control control in ControlCollection)
		{
			if (control is IDialogValue dialogValue)
			{
				yield return dialogValue;
			}
			DialogValueEnumerator childEnum = new DialogValueEnumerator(control.Controls, null);
			foreach (IDialogValue item in childEnum)
			{
				yield return item;
			}
		}
		if (Components == null)
		{
			yield break;
		}
		try
		{
			IDialogValue[] components = Components;
			for (int i = 0; i < components.Length; i++)
			{
				yield return components[i];
			}
		}
		finally
		{
		}
	}
}
