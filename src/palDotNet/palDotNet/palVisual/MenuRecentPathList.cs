using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class MenuRecentPathList
{
	public delegate void HandlePathSelectedDelegate(string path);

	private readonly uint MaxListSize;

	private readonly MenuRecentPathListSettings TheMenuRecentPathListSettings;

	private readonly ToolStripItemCollection Collection;

	private readonly HandlePathSelectedDelegate HandlePathSelected;

	private ToolStripMenuItem[] MenuItems;

	public bool Enabled
	{
		set
		{
			ToolStripMenuItem[] menuItems = MenuItems;
			foreach (ToolStripMenuItem toolStripMenuItem in menuItems)
			{
				toolStripMenuItem.Enabled = value;
			}
		}
	}

	public MenuRecentPathList(uint maxListSize, string settingsKey, ToolStripItemCollection collection, HandlePathSelectedDelegate handlePathSelected)
	{
		Collection = collection;
		HandlePathSelected = handlePathSelected;
		TheMenuRecentPathListSettings = new MenuRecentPathListSettings(settingsKey);
		string[] array = TheMenuRecentPathListSettings.PathList;
		int num = ((array != null) ? array.Length : 0);
		if (maxListSize < num)
		{
			string[] array2 = new string[maxListSize];
			array.CopyTo(array2, num - maxListSize);
			TheMenuRecentPathListSettings.PathList = array2;
			array = array2;
		}
		MaxListSize = maxListSize;
		CreateMenuItems(array);
	}

	private void CreateMenuItems(string[] fileList)
	{
		if (MenuItems != null)
		{
			ToolStripMenuItem[] menuItems = MenuItems;
			foreach (ToolStripMenuItem value in menuItems)
			{
				Collection.Remove(value);
			}
		}
		if (fileList != null)
		{
			MenuItems = new ToolStripMenuItem[fileList.Length];
			for (int j = 0; j < fileList.Length; j++)
			{
				MenuItems[j] = new ToolStripMenuItem(fileList[j], null, HandleMenuItem);
				MenuItems[j].Enabled = true;
				try
				{
					Icon icon = null;
					icon = ((!Directory.Exists(fileList[j])) ? IconLoaderHelper.GetFileIcon(fileList[j], IconLoaderHelper.EnumIconSize.Small, addLinkOverlay: false) : IconLoaderHelper.GetFolderIcon(fileList[j], IconLoaderHelper.EnumIconSize.Small, IconLoaderHelper.EnumFolderType.Open));
					MenuItems[j].Image = icon.ToBitmap();
				}
				catch
				{
				}
			}
			Collection.AddRange(MenuItems);
		}
		else
		{
			MenuItems = new ToolStripMenuItem[1];
			MenuItems[0] = new ToolStripMenuItem("<none>");
			MenuItems[0].Enabled = false;
			MenuItems[0].Tag = this;
			Collection.Add(MenuItems[0]);
		}
	}

	public void AddFileToList(string file)
	{
		string[] pathList = TheMenuRecentPathListSettings.PathList;
		List<string> list = new List<string>();
		if (pathList == null)
		{
			list.Add(file);
		}
		else
		{
			for (int i = 0; i < pathList.Length; i++)
			{
				if (file.ToLower() != pathList[i].ToLower())
				{
					list.Add(pathList[i]);
				}
			}
			list.Insert(0, file);
			if (list.Count > MaxListSize)
			{
				list.RemoveRange((int)MaxListSize, (int)(list.Count - MaxListSize));
			}
		}
		pathList = list.ToArray();
		CreateMenuItems(pathList);
		TheMenuRecentPathListSettings.PathList = pathList;
		Save();
	}

	public void RemoveItem(ToolStripMenuItem item)
	{
		Collection.Remove(item);
		ToolStripMenuItem[] array = new ToolStripMenuItem[MenuItems.Length - 1];
		int num = 0;
		for (int i = 0; i < MenuItems.Length; i++)
		{
			if (MenuItems[i] != item)
			{
				array[num++] = MenuItems[i];
			}
		}
		MenuItems = array;
		string[] pathList = TheMenuRecentPathListSettings.PathList;
		string[] array2 = new string[pathList.Length - 1];
		int num2 = Array.IndexOf(pathList, item.Text);
		if (num2 == 0)
		{
			Array.Copy(pathList, 1, array2, 0, array2.Length);
		}
		else if (num2 == pathList.Length - 1)
		{
			Array.Copy(pathList, 0, array2, 0, array2.Length);
		}
		else
		{
			Array.Copy(pathList, 0, array2, 0, num2 + 1);
			Array.Copy(pathList, num2 + 1, array2, num2, pathList.Length - (num2 + 1));
		}
		TheMenuRecentPathListSettings.PathList = array2;
		Save();
	}

	public void HandleMenuItem(object sender, EventArgs args)
	{
		ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
		string text = toolStripMenuItem.Text;
		bool flag = text.EndsWith("/") || text.EndsWith("\\");
		if (((flag && !Directory.Exists(text)) || (!flag && !File.Exists(text))) && WidgetMgr.Instance.MessageBoxDelegate != null)
		{
			string text2 = (flag ? "folder" : "file");
			DialogResult dialogResult = WidgetMgr.Instance.MessageBoxDelegate("The " + text2 + " '" + text + "' cannot be found.\nDo you want to remove the reference(s) to it from the Recent list(s)?", "Path Doesn't Exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				RemoveItem(toolStripMenuItem);
			}
		}
		else
		{
			if (HandlePathSelected != null)
			{
				HandlePathSelected(text);
			}
			AddFileToList(text);
		}
	}

	public void Save()
	{
		TheMenuRecentPathListSettings.Save();
	}
}
