using System.Management;

namespace BugReporter;

internal class ComputerInfo
{
	public static string GraphicsCard => LogClass("Win32_VideoController");

	public static string Processor => LogClass("Win32_Processor");

	public static string OperatingSystem => LogClass("Win32_OperatingSystem");

	private static string LogClass(string strTable)
	{
		string empty = string.Empty;
		if (strTable.Length <= 0)
		{
			return empty;
		}
		empty = empty + "--- " + strTable + " ---\r\n\r\n";
		WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM " + strTable);
		ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query);
		try
		{
			if (managementObjectSearcher.Get().Count <= 0)
			{
				empty += "Class has no instances\r\n\r\n";
			}
			foreach (ManagementObject item in managementObjectSearcher.Get())
			{
				empty = empty + "* " + item.ToString() + "\r\n";
				if (item.Properties.Count <= 0)
				{
					empty += "Class instance has no properties\r\n";
					continue;
				}
				foreach (PropertyData property in item.Properties)
				{
					empty += LogAttr(item, property.Name);
				}
				empty += "\r\n";
			}
		}
		catch
		{
			empty += "Class does not exist\r\n\r\n";
		}
		return empty;
	}

	private static string LogAttr(ManagementObject obj, string str)
	{
		if (str.Length <= 0)
		{
			return string.Empty;
		}
		string text = "";
		try
		{
			text = obj[str].ToString();
			try
			{
				string[] array = (string[])obj[str];
				if (array.Length > 0)
				{
					text = string.Join(", ", array);
				}
			}
			catch
			{
			}
		}
		catch
		{
			text = "[UNDEFINED]";
		}
		return str + ": " + text + "\r\n";
	}
}
