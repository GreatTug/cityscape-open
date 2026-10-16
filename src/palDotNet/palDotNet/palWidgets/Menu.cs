using System;
using System.Collections.Generic;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
public class Menu(uint newId) : Widget<bool>(newId), ISupportsMenus
{
	public delegate void CheckedStateDel(bool enabled, bool checkState);

	public delegate void RecentFileListDel(EnumMenuMessages message, object[] data);

	public delegate void ThemeSelecterDel();

	public delegate void MenuStripLayoutDel(bool suspend);

	public enum EnumMenuMessages
	{
		eRecentPathSelected = 161,
		eSetEnableRecentPathList = 162,
		eAddRecentPathList = 163,
		eAddRecentPathToList = 164,
		eAddThemeSelector = 165,
		eLastMenuMsg = eAddThemeSelector
	}

	public enum EnumMenuStyle
	{
		eRegular,
		eToolStripStyle,
		eSplitButtonStyle,
		eSplitButtonSynchItemStyle
	}

	public CheckedStateDel CheckedStateDelegate;

	public RecentFileListDel RecentFileListDelegate;

	public ThemeSelecterDel ThemeSelecterDelegate;

	public static MenuStripLayoutDel MenuStripLayoutDelegate;

	public static EventHandler VisualCreator;

	private string TheIconName;

	private bool TheCheckedMenu;

	private bool TheCheckedState;

	private EnumMenuStyle TheMenuStyle;

	private uint TheShortcutKeys;

	private bool TheDisplayShortcutsOnly;

	private bool TheIconOnly;

	private int TheIndex;

	private List<Menu> SubMenus = new List<Menu>();

	private Dictionary<ISupportsMenus, IMenuItem> WidgetToVisual = new Dictionary<ISupportsMenus, IMenuItem>();

	public string IconName => TheIconName;

	public bool CheckedMenu => TheCheckedMenu;

	public bool CheckedState => TheCheckedState;

	public override bool Enabled => TheValue;

	public EnumMenuStyle MenuStyle => TheMenuStyle;

	public uint ShortcutKeys => TheShortcutKeys;

	public int Index => TheIndex;

	public bool IconOnly => TheIconOnly;

	public bool DisplayShortcutsOnly => TheDisplayShortcutsOnly;

	public ToolStrip ToolStripAncestor
	{
		get
		{
			if (base.Parent is ToolStrip)
			{
				return base.Parent as ToolStrip;
			}
			if (base.Parent is Menu)
			{
				return (base.Parent as Menu).ToolStripAncestor;
			}
			return null;
		}
	}

	public override object MapMenuToVisual(ISupportsMenus menu)
	{
		IMenuItem value = null;
		WidgetToVisual.TryGetValue(menu, out value);
		return value?.MenuItem;
	}

	public override void AddMapMenuToVisual(ISupportsMenus menu, IMenuItem visual)
	{
		WidgetToVisual.Add(menu, visual);
	}

	public override bool RemoveMapMenuToVisual(ISupportsMenus menu)
	{
		return WidgetToVisual.Remove(menu);
	}

	public override void Destroy()
	{
		while (SubMenus.Count > 0)
		{
			SubMenus[0].Destroy();
		}
		SubMenus.Clear();
		base.Destroy();
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
		{
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			serializer.Serialize(ref TheIconName);
			serializer.Serialize(ref TheShortcutKeys);
			serializer.Serialize(ref TheDisplayShortcutsOnly);
			serializer.Serialize(ref TheCheckedMenu);
			if (CheckedMenu)
			{
				serializer.Serialize(ref TheCheckedState);
			}
			serializer.Serialize(ref TheValue);
			uint inst4 = 0u;
			serializer.Serialize(ref inst4);
			TheMenuStyle = (EnumMenuStyle)inst4;
			serializer.Serialize(ref TheIconOnly);
			serializer.Serialize(ref TheIndex);
			if (VisualCreator != null)
			{
				VisualCreator(this, null);
			}
			break;
		}
		case 147u:
			if (CheckedMenu)
			{
				serializer.Serialize(ref TheCheckedState);
			}
			else
			{
				bool inst2 = false;
				serializer.Serialize(ref inst2);
			}
			serializer.Serialize(ref TheValue);
			if (serializer.Reading && CheckedStateDelegate != null)
			{
				CheckedStateDelegate(TheValue, TheCheckedState);
			}
			break;
		case 163u:
		{
			uint inst5 = 0u;
			serializer.Serialize(ref inst5);
			string inst6 = "";
			serializer.Serialize(ref inst6);
			if (serializer.Reading && RecentFileListDelegate != null)
			{
				object[] data = new object[2] { inst5, inst6 };
				RecentFileListDelegate(EnumMenuMessages.eAddRecentPathList, data);
			}
			break;
		}
		case 164u:
		{
			string inst = "";
			serializer.Serialize(ref inst);
			if (serializer.Reading && RecentFileListDelegate != null)
			{
				RecentFileListDelegate(EnumMenuMessages.eAddRecentPathToList, new object[1] { inst });
			}
			break;
		}
		case 162u:
		{
			bool inst3 = false;
			serializer.Serialize(ref inst3);
			if (serializer.Reading && RecentFileListDelegate != null)
			{
				RecentFileListDelegate(EnumMenuMessages.eSetEnableRecentPathList, new object[1] { inst3 });
			}
			break;
		}
		case 165u:
			if (serializer.Reading && ThemeSelecterDelegate != null)
			{
				ThemeSelecterDelegate();
			}
			break;
		default:
			base.Serialize(serializer, message);
			break;
		case 136u:
			break;
		}
	}

	public void HandleRecentPathSelected(string fileName)
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		uint inst = 161u;
		writer.Begin();
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		writer.Serialize(ref fileName);
		writer.End(clearIndex: false);
	}

	public override void AddWidget(WidgetBase widget)
	{
		SubMenus.Add(widget as Menu);
	}

	public override bool RemoveWidget(WidgetBase widget)
	{
		return SubMenus.Remove(widget as Menu);
	}

	public void Press()
	{
		WriteMessage(147u);
	}
}
