using System;
using System.Net.NetworkInformation;

namespace palDotNet.palDialog;

internal class ActivationHelpers
{
	public static string GetLocalMacAddress()
	{
		try
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				if (networkInterface.OperationalStatus != OperationalStatus.Up)
				{
					continue;
				}
				string text = "";
				PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
				byte[] addressBytes = physicalAddress.GetAddressBytes();
				for (int j = 0; j < addressBytes.Length; j++)
				{
					text += addressBytes[j].ToString("X2");
					if (j != addressBytes.Length - 1)
					{
						text += "-";
					}
				}
				return text;
			}
		}
		catch (Exception)
		{
		}
		return null;
	}

	public static string GetOSName()
	{
		OperatingSystem oSVersion = Environment.OSVersion;
		string text = "";
		string text2 = "Unknown";
		PlatformID platform = oSVersion.Platform;
		if (platform == PlatformID.Win32NT)
		{
			switch (oSVersion.Version.Major)
			{
			case 5:
				if (oSVersion.Version.Minor == 0)
				{
					text2 = "Windows 2000";
				}
				else if (oSVersion.Version.Minor == 1)
				{
					text2 = "Windows XP";
				}
				else if (oSVersion.Version.Minor == 2)
				{
					text2 = "Windows Server 2003";
				}
				break;
			case 6:
				if (oSVersion.Version.Minor == 0)
				{
					text2 = "Windows Vista";
				}
				else if (oSVersion.Version.Minor == 1)
				{
					text2 = "Windows 7";
				}
				break;
			}
		}
		text += text2;
		if (!string.IsNullOrEmpty(oSVersion.ServicePack))
		{
			text = text + " " + oSVersion.ServicePack;
		}
		return text;
	}
}
