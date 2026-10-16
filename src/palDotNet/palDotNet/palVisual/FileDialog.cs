using System;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class FileDialog
{
	private readonly FormAppBase TheForm;

	public FileDialog(FormAppBase form)
	{
		TheForm = form;
		WidgetMgr instance = WidgetMgr.Instance;
		instance.FileDialogDelegate = (WidgetMgr.FileDialogDel)Delegate.Combine(instance.FileDialogDelegate, new WidgetMgr.FileDialogDel(Show));
	}

	public string Show(bool isSave, string type, string ext, string initialPath, uint filterIndex, string title)
	{
		TheForm.Timer.Stop();
		System.Windows.Forms.FileDialog fileDialog = null;
		fileDialog = ((!isSave) ? ((System.Windows.Forms.FileDialog)new OpenFileDialog()) : ((System.Windows.Forms.FileDialog)new SaveFileDialog()));
		string text = "All files (*.*)|*.*";
		if (!string.IsNullOrEmpty(type))
		{
			string[] array = type.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			string text2 = string.Empty;
			if (ext == string.Empty)
			{
				ext = type;
			}
			string[] array2 = ext.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			if (array2.Length == array.Length)
			{
				for (int i = 0; i < array.Length; i++)
				{
					string[] array3 = array2[i].Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
					if (array3 == null)
					{
						string text3 = text2;
						text2 = text3 + array[i] + " (*." + array2[i] + ")|*." + array2[i];
					}
					else
					{
						text2 = text2 + array[i] + " (";
						for (int j = 0; j < array3.Length; j++)
						{
							text2 = text2 + "*." + array3[j];
							if (j < array3.Length - 1)
							{
								text2 += "; ";
							}
						}
						text2 += ")|";
						for (int k = 0; k < array3.Length; k++)
						{
							text2 = text2 + "*." + array3[k];
							if (k < array3.Length - 1)
							{
								text2 += ";";
							}
						}
					}
					if (i < array.Length - 1)
					{
						text2 += "|";
					}
				}
			}
			else
			{
				text2 = type + " (*." + ext + ")|*." + ext;
			}
			text = text2 + "|" + text;
		}
		fileDialog.Filter = text;
		fileDialog.FilterIndex = (int)filterIndex;
		if (!string.IsNullOrEmpty(initialPath))
		{
			fileDialog.InitialDirectory = initialPath;
		}
		if (!string.IsNullOrEmpty(title))
		{
			fileDialog.Title = title;
		}
		DialogResult dialogResult = fileDialog.ShowDialog(TheForm);
		TheForm.Timer.Start();
		if (dialogResult == DialogResult.OK)
		{
			return fileDialog.FileName;
		}
		return string.Empty;
	}
}
