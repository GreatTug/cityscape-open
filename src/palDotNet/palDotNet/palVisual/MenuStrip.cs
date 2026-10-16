using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

internal class MenuStrip : ISupportsMenus
{
	private System.Windows.Forms.MenuStrip TheMenuStrip;

	private Dictionary<ISupportsMenus, IMenuItem> WidgetToVisual = new Dictionary<ISupportsMenus, IMenuItem>();

	private Dictionary<ToolStripItem, ToolStripItem> ToolStripItems = new Dictionary<ToolStripItem, ToolStripItem>();

	public MenuStrip(System.Windows.Forms.MenuStrip menuStrip)
	{
		TheMenuStrip = menuStrip;
	}

	public void AddMenuItem(palDotNet.palWidgets.Menu menu)
	{
		ToolStripItem toolStripItem;
		if (menu.Name == "-")
		{
			toolStripItem = new ToolStripSeparator();
		}
		else
		{
			EventHandler eventHandler = delegate
			{
				menu.Press();
			};
			ToolStripItem menuItem = null;
			if (menu.MenuStyle == palDotNet.palWidgets.Menu.EnumMenuStyle.eToolStripStyle)
			{
				ToolStripDropDownButton toolStripDropDownButton = new ToolStripDropDownButton(menu.Name, null, eventHandler);
				menuItem = toolStripDropDownButton;
				menu.CheckedStateDelegate = delegate(bool enabled, bool checkState)
				{
					menuItem.Enabled = enabled;
				};
			}
			else if (menu.MenuStyle == palDotNet.palWidgets.Menu.EnumMenuStyle.eSplitButtonStyle)
			{
				ToolStripSplitButton toolStripSplitButton = new ToolStripSplitButton(menu.Name, null);
				toolStripSplitButton.ButtonClick += eventHandler;
				menuItem = toolStripSplitButton;
				menu.CheckedStateDelegate = delegate(bool enabled, bool checkState)
				{
					menuItem.Enabled = enabled;
				};
			}
			else if (menu.MenuStyle == palDotNet.palWidgets.Menu.EnumMenuStyle.eSplitButtonSynchItemStyle)
			{
				SplitButton splitButton = menu as SplitButton;
				ToolStripSplitButton menuItemTemp = new ToolStripSplitButton(splitButton.Name, null);
				menuItemTemp.ImageScaling = ToolStripItemImageScaling.None;
				menuItemTemp.AutoSize = true;
				menuItemTemp.DefaultItemChanged += delegate(object sender, EventArgs e)
				{
					ToolStripSplitButton toolStripSplitButton3 = (ToolStripSplitButton)sender;
					if (toolStripSplitButton3.DefaultItem == null)
					{
						toolStripSplitButton3.Text = toolStripSplitButton3.DropDownItems[0].Text;
						toolStripSplitButton3.Image = toolStripSplitButton3.DropDownItems[0].Image;
					}
				};
				splitButton.DefaultOptionDelegate = (SplitButton.DefaultOptionDel)Delegate.Combine(splitButton.DefaultOptionDelegate, (SplitButton.DefaultOptionDel)delegate(ISupportsMenus itemToSelect)
				{
					ToolStripItem defaultItem = menu.MapMenuToVisual(itemToSelect) as ToolStripItem;
					menuItemTemp.DefaultItem = defaultItem;
					menuItemTemp.Text = menuItemTemp.DefaultItem.Text;
					menuItemTemp.Image = menuItemTemp.DefaultItem.Image;
				});
				menuItemTemp.DropDownItemClicked += delegate(object sender, ToolStripItemClickedEventArgs e)
				{
					ToolStripSplitButton toolStripSplitButton3 = (ToolStripSplitButton)sender;
					toolStripSplitButton3.DefaultItem = e.ClickedItem;
					toolStripSplitButton3.Text = toolStripSplitButton3.DefaultItem.Text;
					toolStripSplitButton3.Image = toolStripSplitButton3.DefaultItem.Image;
				};
				menuItem = menuItemTemp;
				menu.CheckedStateDelegate = delegate(bool enabled, bool checkState)
				{
					menuItem.Enabled = enabled;
				};
			}
			else
			{
				ToolStripMenuItem menuItemTemp2 = new ToolStripMenuItem(menu.Name, null, eventHandler);
				try
				{
					if (menu.DisplayShortcutsOnly)
					{
						if (menu.ShortcutKeys != 0)
						{
							KeysConverter keysConverter = new KeysConverter();
							string shortcutKeyDisplayString = keysConverter.ConvertToString((Keys)menu.ShortcutKeys);
							menuItemTemp2.ShortcutKeyDisplayString = shortcutKeyDisplayString;
						}
					}
					else
					{
						menuItemTemp2.ShortcutKeys = (Keys)menu.ShortcutKeys;
					}
				}
				catch (InvalidEnumArgumentException)
				{
					string message = string.Concat("Shortcut key '", (Keys)menu.ShortcutKeys, "' is not supported by .NET");
					throw new ApplicationException(message);
				}
				if (menu.CheckedMenu)
				{
					menuItemTemp2.Checked = menu.CheckedState;
				}
				menu.CheckedStateDelegate = delegate(bool enabled, bool checkState)
				{
					menuItemTemp2.Enabled = enabled;
					menuItemTemp2.Checked = checkState;
				};
				menu.RecentFileListDelegate = delegate(palDotNet.palWidgets.Menu.EnumMenuMessages enumMenuMessages, object[] data)
				{
					switch (enumMenuMessages)
					{
					case palDotNet.palWidgets.Menu.EnumMenuMessages.eAddRecentPathList:
					{
						uint maxListSize = (uint)data[0];
						string settingsKey = (string)data[1];
						if (menuItemTemp2.Tag is MenuRecentPathList menuRecentPathList3)
						{
							menuRecentPathList3.Save();
						}
						menuItemTemp2.Tag = new MenuRecentPathList(maxListSize, settingsKey, menuItemTemp2.DropDownItems, menu.HandleRecentPathSelected);
						break;
					}
					case palDotNet.palWidgets.Menu.EnumMenuMessages.eAddRecentPathToList:
					{
						string file = (string)data[0];
						MenuRecentPathList menuRecentPathList2 = menuItemTemp2.Tag as MenuRecentPathList;
						menuRecentPathList2.AddFileToList(file);
						break;
					}
					case palDotNet.palWidgets.Menu.EnumMenuMessages.eSetEnableRecentPathList:
					{
						MenuRecentPathList menuRecentPathList = menuItemTemp2.Tag as MenuRecentPathList;
						menuRecentPathList.Enabled = (bool)data[0];
						break;
					}
					}
				};
				menuItem = menuItemTemp2;
			}
			menuItem.Enabled = menu.Enabled;
			menuItem.DisplayStyle = (menu.IconOnly ? ToolStripItemDisplayStyle.Image : ToolStripItemDisplayStyle.ImageAndText);
			palDotNet.palWidgets.ToolStrip toolStripAncestor = menu.ToolStripAncestor;
			Size value = ((toolStripAncestor == null || toolStripAncestor.IconSize == 0) ? new Size(16, 16) : new Size((int)toolStripAncestor.IconSize, (int)toolStripAncestor.IconSize));
			menuItem.Padding = new Padding(1);
			menuItem.Image = IconLoaderHelper.Load(menu.IconName, value);
			toolStripItem = menuItem;
			menu.EventComment += delegate(object sender, EventArgs args)
			{
				if (sender is WidgetBase widgetBase)
				{
					menuItem.ToolTipText = widgetBase.Comment;
					if (!string.IsNullOrEmpty(menuItem.ToolTipText))
					{
						menuItem.AutoToolTip = false;
					}
				}
			};
		}
		ToolStripItems.Add(toolStripItem, toolStripItem);
		ISupportsMenus supportsMenus = menu.Parent;
		if (supportsMenus == null)
		{
			supportsMenus = this;
		}
		supportsMenus.AddMapMenuToVisual(menu, new MenuStripMenuItem(toolStripItem));
		ToolStripItemCollection toolStripItemCollection = FindParentItemCollection(menu, out var parentItem);
		if (toolStripItemCollection != null)
		{
			if (menu.Index >= 0)
			{
				toolStripItemCollection.Insert(menu.Index, toolStripItem);
			}
			else
			{
				toolStripItemCollection.Add(toolStripItem);
			}
			if (menu.Parent is palDotNet.palWidgets.Menu { MenuStyle: palDotNet.palWidgets.Menu.EnumMenuStyle.eSplitButtonSynchItemStyle } && parentItem is ToolStripSplitButton { DefaultItem: null } toolStripSplitButton2)
			{
				toolStripSplitButton2.DefaultItem = toolStripItem;
			}
		}
	}

	public bool RemoveMenuItem(palDotNet.palWidgets.Menu menu)
	{
		ToolStripItemCollection toolStripItemCollection = FindParentItemCollection(menu, out var _);
		ISupportsMenus supportsMenus = menu.Parent;
		if (supportsMenus == null)
		{
			supportsMenus = this;
		}
		ToolStripItem toolStripItem = supportsMenus.MapMenuToVisual(menu) as ToolStripItem;
		if (toolStripItem != null)
		{
			ToolStripItems.Remove(toolStripItem);
		}
		toolStripItemCollection?.Remove(toolStripItem);
		return supportsMenus.RemoveMapMenuToVisual(menu);
	}

	public ToolStripItemCollection FindParentItemCollection(palDotNet.palWidgets.Menu menu, out ToolStripDropDownItem parentItem)
	{
		parentItem = null;
		ISupportsMenus parent = menu.Parent;
		ToolStripItemCollection result = null;
		if (parent != null)
		{
			ISupportsMenus parent2 = menu.Parent.Parent;
			ToolStripDropDownItem toolStripDropDownItem = null;
			toolStripDropDownItem = (parentItem = ((parent2 == null) ? (MapMenuToVisual(parent) as ToolStripDropDownItem) : (parent2.MapMenuToVisual(parent) as ToolStripDropDownItem)));
			if (toolStripDropDownItem != null)
			{
				result = toolStripDropDownItem.DropDownItems;
			}
			return result;
		}
		return TheMenuStrip.Items;
	}

	public object MapMenuToVisual(ISupportsMenus menu)
	{
		IMenuItem value = null;
		WidgetToVisual.TryGetValue(menu, out value);
		return value?.MenuItem;
	}

	public void AddMapMenuToVisual(ISupportsMenus menu, IMenuItem visual)
	{
		WidgetToVisual.Add(menu, visual);
	}

	public bool RemoveMapMenuToVisual(ISupportsMenus menu)
	{
		return WidgetToVisual.Remove(menu);
	}
}
