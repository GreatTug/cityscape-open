using System;
using System.IO;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class FileLoader(uint newId) : IconLoader(newId)
{
	private enum MessagesFileLoader
	{
		eFileTypes = 163,
		eFileNameOnly = 164,
		eInitialDirectory = 165,
		eFilterIndex = 166,
		eTitle = 167,
		eBasePaths = 168,
		eLastFileLoaderMsg = eBasePaths
	}

	private string TheExtensions;

	private bool TheFileNameOnly;

	private string TheInitialDirectory;

	private string TheTypes;

	private uint TheFilterIndex;

	private string TheTitle;

	private string[] TheBasePaths = new string[0];

	private bool EnforceFileTypes;

	public string Extensions => TheExtensions;

	public bool FileNameOnly => TheFileNameOnly;

	public string Types => TheTypes;

	public string InitialDirectory => TheInitialDirectory;

	public uint FilterIndex => TheFilterIndex;

	public string Title => TheTitle;

	public string[] BasePaths => TheBasePaths;

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 163u:
			serializer.Serialize(ref TheTypes);
			serializer.Serialize(ref TheExtensions);
			serializer.Serialize(ref EnforceFileTypes);
			break;
		case 164u:
			serializer.Serialize(ref TheFileNameOnly);
			if (EventUpdateView != null)
			{
				EventUpdateView(this, EventArgs.Empty);
			}
			break;
		case 165u:
			serializer.Serialize(ref TheInitialDirectory);
			break;
		case 166u:
			serializer.Serialize(ref TheFilterIndex);
			break;
		case 167u:
			serializer.Serialize(ref TheTitle);
			break;
		case 168u:
		{
			uint inst = 0u;
			serializer.Serialize(ref inst);
			TheBasePaths = new string[inst];
			for (int i = 0; i < inst; i++)
			{
				string inst2 = null;
				serializer.SerializeArString(ref inst2);
				TheBasePaths[i] = inst2;
			}
			if (EventUpdateView != null)
			{
				EventUpdateView(this, EventArgs.Empty);
			}
			break;
		}
		default:
			base.Serialize(serializer, message);
			break;
		}
	}

	public string VerifyFileName(string pathName)
	{
		if (EnforceFileTypes)
		{
			string extension = Path.GetExtension(pathName);
			string[] array = TheExtensions.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			bool flag = false;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
				string[] array3 = TheExtensions.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
				string[] array4 = array3;
				foreach (string text in array4)
				{
					if (extension.ToLower() == "." + text.ToLower())
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return "File type '" + extension + "' not allowed";
			}
		}
		return null;
	}
}
