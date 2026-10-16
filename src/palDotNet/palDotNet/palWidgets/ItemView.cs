using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

public class ItemView(uint newId) : WidgetPanel(newId), ISupportsMenus
{
	public enum Messages
	{
		eBuildAndShowContextMenuItemView = 168,
		eItemAdd = 169,
		eItemRemove = 170,
		eItemsSelected = 171,
		eKeyPressed = 172,
		eKeyReleased = 173,
		eLabelEdit = 174,
		eBeginRenameItem = 175,
		ePauseUpdate = 176,
		eIconSize = 177,
		eAllowDragAndDrop = 178,
		eClearAllItems = 179,
		eItemClicked = 180,
		eItemDoubleClicked = 181,
		eLastItemViewMsg = eItemDoubleClicked
	}

	public class ItemArgs : EventArgs
	{
		public int SortOrder;

		public string Name;

		public string IconPath;

		public byte[] BitmapBytes;

		public bool AllowLabelEdit;

		public ItemArgs(string name, string iconPath, byte[] bitmapBytes, bool allowLabelEdit, int sortOrder)
		{
			Name = name;
			IconPath = iconPath;
			BitmapBytes = bitmapBytes;
			AllowLabelEdit = allowLabelEdit;
			SortOrder = sortOrder;
		}
	}

	public class SelectArgs : EventArgs
	{
		public string[] Items;

		public SelectArgs(string[] items)
		{
			Items = items;
		}
	}

	public class PauseUpdateArgs : EventArgs
	{
		public bool Pause;

		public PauseUpdateArgs(bool pause)
		{
			Pause = pause;
		}
	}

	public class IconSizeArgs : EventArgs
	{
		public uint Width;

		public uint Height;

		public IconSizeArgs(uint width, uint height)
		{
			Width = width;
			Height = height;
		}
	}

	private Dictionary<ISupportsMenus, IMenuItem> WidgetToVisual = new Dictionary<ISupportsMenus, IMenuItem>();

	private bool ChangeAllowed;

	private bool WaitingForLabelEdit;

	private int ThePauseCount;

	private bool TheAllowDragAndDrop;

	public int PauseCount
	{
		get
		{
			return ThePauseCount;
		}
		set
		{
			ThePauseCount = value;
		}
	}

	public bool AllowDragAndDrop => TheAllowDragAndDrop;

	public event EventHandler<ItemArgs> EventAddItem;

	public event EventHandler<ItemArgs> EventRemoveItem;

	public event EventHandler<SelectArgs> EventSelectItems;

	public event EventHandler<SelectArgs> EventGetSelectedItems;

	public event EventHandler EventClearAllItems;

	public event EventHandler<LabelEditEventArgs> EventBeginRenameItem;

	public event EventHandler<PauseUpdateArgs> EventPauseUpdate;

	public event EventHandler<IconSizeArgs> EventIconSize;

	protected virtual void CreateInstance()
	{
		throw new NotSupportedException("Make sure all derived classes override this function");
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 169u:
		{
			string inst7 = string.Empty;
			string inst8 = string.Empty;
			serializer.Serialize(ref inst7);
			int inst9 = 0;
			serializer.Serialize(ref inst9);
			serializer.Serialize(ref inst8);
			bool inst10 = false;
			serializer.Serialize(ref inst10);
			int inst11 = 0;
			serializer.Serialize(ref inst11);
			byte[] bytes = null;
			if (inst11 > 0)
			{
				serializer.Serialize(ref bytes, inst11);
			}
			if (serializer.Reading && EventAddItem != null)
			{
				ItemArgs e2 = new ItemArgs(inst7, inst8, bytes, inst10, inst9);
				EventAddItem(this, e2);
			}
			break;
		}
		case 170u:
		{
			string inst3 = string.Empty;
			serializer.Serialize(ref inst3);
			if (serializer.Reading && EventRemoveItem != null)
			{
				ItemArgs e = new ItemArgs(inst3, null, null, allowLabelEdit: false, 0);
				EventRemoveItem(this, e);
			}
			break;
		}
		case 171u:
		{
			if (serializer.Reading)
			{
				uint inst5 = 0u;
				serializer.Serialize(ref inst5);
				string[] array = new string[inst5];
				for (int i = 0; i < inst5; i++)
				{
					serializer.Serialize(ref array[i]);
				}
				if (EventSelectItems != null)
				{
					EventSelectItems(this, new SelectArgs(array));
				}
				break;
			}
			string[] items = null;
			if (EventGetSelectedItems != null)
			{
				SelectArgs selectArgs = new SelectArgs(items);
				EventGetSelectedItems(this, selectArgs);
				items = selectArgs.Items;
			}
			else
			{
				items = new string[0];
			}
			int inst6 = items.Length;
			serializer.Serialize(ref inst6);
			for (int j = 0; j < inst6; j++)
			{
				serializer.Serialize(ref items[j]);
			}
			break;
		}
		case 175u:
			EventBeginRenameItem(this, null);
			break;
		case 174u:
			serializer.Serialize(ref ChangeAllowed);
			WaitingForLabelEdit = false;
			break;
		case 176u:
		{
			bool inst4 = false;
			serializer.Serialize(ref inst4);
			if (inst4)
			{
				PauseCount++;
			}
			else
			{
				PauseCount--;
				if (PauseCount < 0)
				{
					throw new ApplicationException("Too many unpauses!");
				}
			}
			if (EventPauseUpdate != null)
			{
				EventPauseUpdate(this, new PauseUpdateArgs(inst4));
			}
			break;
		}
		case 178u:
			serializer.Serialize(ref TheAllowDragAndDrop);
			break;
		case 179u:
			if (EventClearAllItems != null)
			{
				EventClearAllItems(this, null);
			}
			break;
		case 177u:
		{
			uint inst = 0u;
			uint inst2 = 0u;
			serializer.Serialize(ref inst);
			serializer.Serialize(ref inst2);
			if (EventIconSize != null)
			{
				EventIconSize(this, new IconSizeArgs(inst, inst2));
			}
			break;
		}
		default:
			base.Serialize(serializer, message);
			break;
		}
	}

	public override void ShowMenu()
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		uint inst = 168u;
		writer.Begin();
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		string[] array = new string[0];
		if (EventGetSelectedItems != null)
		{
			SelectArgs selectArgs = new SelectArgs(array);
			EventGetSelectedItems(this, selectArgs);
			array = selectArgs.Items;
		}
		int inst2 = array.Length;
		writer.Serialize(ref inst2);
		for (int i = 0; i < inst2; i++)
		{
			writer.Serialize(ref array[i]);
		}
		writer.End(clearIndex: false);
	}

	public void SelectItems(string[] items)
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		uint inst = 171u;
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		int inst2 = items.Length;
		writer.Serialize(ref inst2);
		foreach (string text in items)
		{
			string inst3 = text;
			writer.Serialize(ref inst3);
		}
		writer.End(clearIndex: false);
	}

	public void SendKeyEvent(Keys keys, Messages keyEventType)
	{
		if (keyEventType != Messages.eKeyPressed && keyEventType != Messages.eKeyReleased)
		{
			throw new ApplicationException("Incorrect data to SendKeyEvent!");
		}
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		uint inst = (uint)keyEventType;
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		uint inst2 = (uint)keys;
		writer.Serialize(ref inst2);
		writer.End(clearIndex: false);
	}

	public void ItemClicked(string name, uint message)
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		writer.Serialize(ref message);
		writer.Serialize(ref RemoteId);
		uint inst = (uint)(name.Length + 1);
		writer.Serialize(ref inst);
		writer.Serialize(ref name);
		writer.End(clearIndex: false);
	}

	public bool AfterLabelEdit(string oldText, string newText)
	{
		WaitingForLabelEdit = true;
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		uint inst = 174u;
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		writer.Serialize(ref oldText);
		if (newText == null)
		{
			newText = string.Empty;
		}
		writer.Serialize(ref newText);
		writer.End(clearIndex: false);
		WidgetMgr.Instance.UpdateTimer.Stop();
		do
		{
			WidgetMgr.Instance.Update();
			System.Threading.Thread.Sleep(0);
		}
		while (WaitingForLabelEdit);
		WidgetMgr.Instance.UpdateTimer.Start();
		return ChangeAllowed;
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
}
