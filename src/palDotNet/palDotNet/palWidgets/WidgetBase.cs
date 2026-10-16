using System;
using System.Collections.Generic;
using System.Threading;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

public abstract class WidgetBase : ISupportsMenus
{
	public enum CoreMessages
	{
		eNoMessage = -1,
		eProgressStart = 100,
		eProgressEnd = 101,
		eProgressRenderWindow = 102,
		eSetCurrentUnit = 103,
		eSendUnitType = 104,
		eSetTitle = 105,
		eHelpText = 106,
		eSuspendAll = 107,
		eResumeAll = 108,
		eRenderWindowAdd = 109,
		eRenderWindowRemove = 110,
		eLayoutCreatePanel = 111,
		eLoadLayout = 112,
		eDisableApp = 113,
		eEnableApp = 114,
		eDeactivatedApp = 115,
		ePauseApp = 116,
		eUnPauseApp = 117,
		eOpenFile = 118,
		eSaveFile = 119,
		eDirectoryBrowser = 120,
		eMessageBox = 121,
		eAboutBox = 122,
		eKeyPressed = 123,
		eKeyDown = 124,
		eKeyReleased = 125,
		eCursorSetCurrent = 126,
		eQuitReceived = 127,
		eQuitConfirm = 128,
		eQuitCheckCancel = 129,
		eQuitCheckCancelReceived = 130,
		eMinimized = 131,
		eNonMinimized = 132,
		eBringToFront = 133,
		eDragDropTargetValid = 134,
		eDragDropItem = 135,
		eCreate = 136,
		eInitialize = 137,
		eDestroy = 138,
		eComment = 139,
		eEnabled = 140,
		eCanManuallyDisable = 141,
		eManualEnable = 142,
		eShowModified = 143,
		eValueVisible = 144,
		eMsgEditingBegin = 145,
		eMsgEditingEnd = 146,
		eUpdate = 147,
		eBuildAndShowContextMenu = 148,
		eShowContextMenu = 149,
		eFileServerOpen = 150,
		eFileServerWrite = 151,
		eFileServerClose = 152,
		eHelpSetProvider = 153,
		eHelpShowWithProvider = 154,
		eHelpShow = 155,
		eHelpShowHelpPageInBrowser = 156,
		eMenuStripSuspendLayout = 157,
		eMenuStripResumeLayout = 158,
		eWidgetFocusEnter = 159,
		eWidgetFocusLeave = 160,
		eLastMsg = eWidgetFocusLeave
	}

	protected bool TheLastValueVisible = true;

	protected bool TheValueVisible = true;

	protected bool SerializeUpdate = true;

	protected bool DelayEditingBeginMsg;

	protected bool BeginMsgSent;

	protected bool FlushEditing;

	protected uint RemoteId;

	protected WidgetBase TheParent;

	protected string TheName;

	protected string TheComment;

	protected bool TheEnabled;

	protected bool TheCanManuallyDisable;

	protected bool TheShowModified;

	protected bool TheManualEnable = true;

	private object MenuVisual;

	private bool? DragDropResult;

	public WidgetBase Parent
	{
		get
		{
			return TheParent;
		}
		set
		{
			TheParent = value;
		}
	}

	public virtual List<WidgetBase> Children => null;

	public string Name => TheName;

	public string Comment => TheComment;

	public virtual bool Enabled => TheEnabled;

	public virtual bool CanManuallyDisable => TheCanManuallyDisable;

	public virtual bool ShowModified => TheShowModified;

	public virtual bool ManualEnable
	{
		get
		{
			return TheManualEnable;
		}
		set
		{
			if (TheCanManuallyDisable && value != TheManualEnable)
			{
				TheManualEnable = value;
				WriteMessage(142u);
			}
		}
	}

	public virtual string PropertyValueName => "WidgetValue";

	public virtual object WidgetValueObject
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public virtual bool SendEditingMessages => true;

	public bool ValueVisible
	{
		get
		{
			return TheValueVisible;
		}
		set
		{
			TheLastValueVisible = TheValueVisible;
			if (!value || SerializeUpdate)
			{
				TheValueVisible = value;
			}
			if (value && TheLastValueVisible != TheValueVisible && DelayEditingBeginMsg)
			{
				SendEditingBegin(delayMsg: false);
			}
		}
	}

	public virtual bool AlwaysFlushEditing
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public bool Destroyed { get; private set; }

	public static event EventHandler EventCreate;

	public static event EventHandler EventDestroy;

	public static event EventHandler EventSuspendLayout;

	public static event EventHandler EventResumeLayout;

	public event EventHandler EventComment;

	public event EventHandler EventEnabled;

	public event EventHandler EventValueVisible;

	public event EventHandler EventCanManuallyDisable;

	public event EventHandler EventShowModified;

	public event EventHandler WidgetValueChanged;

	public event EventHandler<ContextMenuArgs> EventShowContextMenu;

	public override int GetHashCode()
	{
		return (int)RemoteId;
	}

	public uint Id => RemoteId;

	public WidgetBase(uint newId)
	{
		RemoteId = newId;
		Destroyed = false;
	}

	public virtual void AddWidget(WidgetBase widget)
	{
	}

	public virtual bool RemoveWidget(WidgetBase widget)
	{
		return true;
	}

	public virtual void Serialize(ISerializer serializer, uint msgType)
	{
		switch (msgType)
		{
		case 139u:
			serializer.Serialize(ref TheComment);
			OnEventComment();
			break;
		case 141u:
			serializer.Serialize(ref TheCanManuallyDisable);
			OnEventCanManuallyDisable();
			break;
		case 142u:
			serializer.Serialize(ref TheManualEnable);
			break;
		case 143u:
			serializer.Serialize(ref TheShowModified);
			OnEventShowModified();
			break;
		case 140u:
			serializer.Serialize(ref TheEnabled);
			OnEventEnabled();
			break;
		case 144u:
			serializer.Serialize(ref TheValueVisible);
			if (serializer.Reading)
			{
				TheLastValueVisible = TheValueVisible;
			}
			OnEventValueVisible();
			break;
		case 134u:
		case 135u:
		{
			bool inst = false;
			serializer.Serialize(ref inst);
			DragDropResult = inst;
			break;
		}
		case 149u:
		{
			RemotePtr remotePtr = default(RemotePtr);
			remotePtr.Serialize(serializer);
			if (serializer.Reading && EventShowContextMenu != null)
			{
				WidgetBase widgetBase = WidgetMgr.Instance.MapRemoteToLocal(remotePtr);
				if (!(widgetBase is Menu))
				{
					throw new ApplicationException("Widget needs to be a menu!");
				}
				EventShowContextMenu(this, new ContextMenuArgs(widgetBase as Menu));
			}
			break;
		}
		case 136u:
		case 137u:
		case 138u:
		case 145u:
		case 146u:
		case 147u:
		case 148u:
			break;
		}
	}

	public virtual void ShowMenu()
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		uint inst = 148u;
		writer.Begin();
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		WidgetBase inst2 = this;
		writer.Serialize(ref inst2);
		writer.End(clearIndex: false);
	}

	public void WriteMessage(uint message)
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		writer.Serialize(ref message);
		writer.Serialize(ref RemoteId);
		Serialize(writer, message);
		writer.End(clearIndex: false);
	}

	public virtual void Destroy()
	{
		Destroyed = true;
		OnWidgetDestroy();
		if (Parent != null)
		{
			Parent.RemoveWidget(this);
		}
		WriteMessage(138u);
	}

	public void SendEditingEndThenBegin()
	{
		if (BeginMsgSent)
		{
			SendEditingEnd(skipIfDelayed: false);
			SendEditingBegin(delayMsg: false);
		}
	}

	public void SendEditingBegin(bool delayMsg)
	{
		if (SendEditingMessages)
		{
			DelayEditingBeginMsg = !ValueVisible || delayMsg;
			if (!DelayEditingBeginMsg)
			{
				WriteMessage(145u);
				BeginMsgSent = true;
			}
		}
	}

	public virtual void SendEditingEnd(bool skipIfDelayed)
	{
		if (SendEditingMessages && ValueVisible && (!skipIfDelayed || !DelayEditingBeginMsg))
		{
			if (DelayEditingBeginMsg)
			{
				DelayEditingBeginMsg = false;
				WriteMessage(145u);
			}
			WriteMessage(146u);
			BeginMsgSent = false;
		}
	}

	public virtual bool SendDragDrop(int remoteId, string fromType, string[] fromTexts, string toText, CoreMessages msg)
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		uint inst = (uint)msg;
		WidgetMgr.Instance.Writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		writer.Serialize(ref remoteId);
		writer.Serialize(ref fromType);
		int inst2 = fromTexts.Length;
		writer.Serialize(ref inst2);
		foreach (string text in fromTexts)
		{
			string inst3 = text;
			writer.Serialize(ref inst3);
		}
		writer.Serialize(ref toText);
		writer.End(clearIndex: false);
		WidgetMgr.Instance.UpdateTimer.Stop();
		do
		{
			WidgetMgr.Instance.Update();
			System.Threading.Thread.Sleep(0);
		}
		while (!DragDropResult.HasValue);
		WidgetMgr.Instance.UpdateTimer.Start();
		bool value = DragDropResult.Value;
		DragDropResult = null;
		return value;
	}

	public void SetFlushEditing()
	{
		FlushEditing = true;
	}

	public virtual void UpdateUnits(Units units)
	{
	}

	protected void OnWidgetCreate()
	{
		if (EventCreate != null)
		{
			EventCreate(this, EventArgs.Empty);
		}
	}

	protected virtual void OnWidgetDestroy()
	{
		if (EventDestroy != null)
		{
			EventDestroy(this, EventArgs.Empty);
		}
	}

	protected void OnEventSuspendLayout()
	{
		if (EventSuspendLayout != null)
		{
			EventSuspendLayout(this, EventArgs.Empty);
		}
	}

	protected void OnEventResumeLayout()
	{
		if (EventResumeLayout != null)
		{
			EventResumeLayout(this, EventArgs.Empty);
		}
	}

	protected void OnEventComment()
	{
		if (EventComment != null)
		{
			EventComment(this, EventArgs.Empty);
		}
	}

	protected void OnEventEnabled()
	{
		if (EventEnabled != null)
		{
			EventEnabled(this, EventArgs.Empty);
		}
	}

	protected void OnEventValueVisible()
	{
		if (EventValueVisible != null)
		{
			EventValueVisible(this, EventArgs.Empty);
		}
	}

	protected void OnEventCanManuallyDisable()
	{
		if (EventCanManuallyDisable != null)
		{
			EventCanManuallyDisable(this, EventArgs.Empty);
		}
	}

	protected void OnEventShowModified()
	{
		if (EventShowModified != null)
		{
			EventShowModified(this, EventArgs.Empty);
		}
	}

	protected void OnWidgetValueChanged(EventArgs e)
	{
		if (WidgetValueChanged != null)
		{
			WidgetValueChanged(this, e);
		}
		if (SerializeUpdate)
		{
			if (DelayEditingBeginMsg)
			{
				if (!ValueVisible)
				{
					ValueVisible = true;
				}
				else
				{
					SendEditingBegin(delayMsg: false);
				}
			}
			WriteMessage(147u);
			if (FlushEditing || AlwaysFlushEditing)
			{
				FlushEditing = false;
				SendEditingEnd(skipIfDelayed: false);
				SendEditingBegin(delayMsg: false);
			}
		}
		else
		{
			SerializeUpdate = true;
		}
	}

	protected void CheckValueVisible(bool valuesEqual)
	{
		if (TheLastValueVisible != ValueVisible)
		{
			if (valuesEqual && ValueVisible)
			{
				WriteMessage(144u);
			}
			TheLastValueVisible = ValueVisible;
		}
	}

	public virtual object MapMenuToVisual(ISupportsMenus menu)
	{
		if (MenuVisual is IMenuItem)
		{
			return (MenuVisual as IMenuItem).MenuItem;
		}
		return null;
	}

	public virtual void AddMapMenuToVisual(ISupportsMenus menu, IMenuItem visual)
	{
		MenuVisual = visual;
	}

	public virtual bool RemoveMapMenuToVisual(ISupportsMenus menu)
	{
		MenuVisual = null;
		return true;
	}
}
