using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

internal static class Help
{
	private static string HelpProviderPath;

	private static Form DummyForm;

	public static void Read(WidgetBase.CoreMessages msgType, ISerializer reader)
	{
		string inst = string.Empty;
		switch (msgType)
		{
		case WidgetBase.CoreMessages.eHelpSetProvider:
			reader.Serialize(ref inst);
			HelpProviderPath = ComputePath(inst);
			break;
		case WidgetBase.CoreMessages.eHelpShowWithProvider:
			reader.Serialize(ref inst);
			inst = ComputePath(inst);
			ShowHelp(inst);
			break;
		case WidgetBase.CoreMessages.eHelpShow:
			if (string.IsNullOrEmpty(HelpProviderPath))
			{
				HelpProviderPath = Path.ChangeExtension(Application.ExecutablePath, "chm");
			}
			ShowHelp(HelpProviderPath);
			break;
		case WidgetBase.CoreMessages.eHelpShowHelpPageInBrowser:
			reader.Serialize(ref inst);
			if (!string.IsNullOrEmpty(inst))
			{
				ShowHelpInBrowser(inst);
			}
			break;
		}
	}

	private static string ComputePath(string providerPath)
	{
		string executablePath = Application.ExecutablePath;
		if (string.IsNullOrEmpty(providerPath))
		{
			return Path.ChangeExtension(executablePath, "chm");
		}
		if (Path.IsPathRooted(providerPath))
		{
			return providerPath;
		}
		return Path.Combine(Path.GetDirectoryName(executablePath), providerPath);
	}

	private static void ShowHelpInBrowser(string url)
	{
		try
		{
			Process.Start(url);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to show url:\n\n" + ex.Message, "Help Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private static void ShowHelp(string providerPath)
	{
		try
		{
			if (DummyForm == null)
			{
				DummyForm = new Form();
			}
			string url = "file://" + providerPath;
			if (!File.Exists(providerPath))
			{
				throw new FileNotFoundException("File not found: " + providerPath, providerPath, null);
			}
			System.Windows.Forms.Help.ShowHelp(DummyForm, url);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to show help:\n\n" + ex.Message, "Help Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}
}
