using System;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

public class WidgetPanel : WidgetBase
{
	public enum MessagesWidgetPanel
	{
		eToolStripAdd = 161,
		eToolStripRemove = 162,
		eShow = 163,
		eHide = 164,
		eUnhide = 165,
		eHiddenState = 166,
		eSetIcon = 167,
		eLastWidgetPanelMsg = eSetIcon
	}

	public enum EnumDockingType
	{
		eDockLeft,
		eDockRight,
		eDockTop,
		eDockBottom,
		eDockTabbed
	}

	protected Guid TheGuid;

	private Guid TheRelativeDockingGuid;

	private static bool SerializingLayout;

	private float TheDockingProportion;

	private EnumDockingType TheDockingType;

	private bool AllowCreationIfParent;

	private bool TheAllowClose;

	public Guid Guid => TheGuid;

	public Guid RelativeDockingGuid => TheRelativeDockingGuid;

	public static bool IsSerializingLayout
	{
		get
		{
			return SerializingLayout;
		}
		set
		{
			SerializingLayout = value;
		}
	}

	public float DockingProportion => TheDockingProportion;

	public EnumDockingType DockingType => TheDockingType;

	public bool AllowClose => TheAllowClose;

	public event EventHandler EventCreateToolStrip;

	public event EventHandler<EventHideArgs> EventHide;

	public event EventHandler EventShowPanel;

	public event EventHandler<EventSetIconArgs> EventSetIcon;

	public WidgetPanel(uint newId)
		: base(newId)
	{
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			SerializeGuids(serializer);
			SerializeInitialization(serializer);
			if (serializer.Reading && (TheParent == null || AllowCreationIfParent))
			{
				OnWidgetCreate();
			}
			break;
		case 161u:
			if (EventCreateToolStrip != null)
			{
				EventCreateToolStrip(this, null);
			}
			break;
		case 164u:
		{
			uint inst = 0u;
			serializer.Serialize(ref inst);
			if (EventHide != null)
			{
				WidgetMgr.Instance.Reader.End(clearIndex: false);
				EventHideArgs eventHideArgs = new EventHideArgs(hide: true);
				eventHideArgs.Type = (EventHideArgs.EnumHideType)inst;
				EventHide(this, eventHideArgs);
				WidgetMgr.Instance.Reader.Begin();
			}
			break;
		}
		case 165u:
			if (EventHide != null)
			{
				WidgetMgr.Instance.Reader.End(clearIndex: false);
				uint inst3 = 4u;
				serializer.Serialize(ref inst3);
				EventHideArgs eventHideArgs2 = new EventHideArgs(hide: false);
				eventHideArgs2.DockingType = (EventHideArgs.EnumDockingType)inst3;
				EventHide(this, eventHideArgs2);
				WidgetMgr.Instance.Reader.Begin();
			}
			break;
		case 167u:
		{
			string inst2 = null;
			serializer.Serialize(ref inst2);
			if (EventSetIcon != null)
			{
				EventSetIcon(this, new EventSetIconArgs(inst2));
			}
			break;
		}
		case 163u:
			if (EventShowPanel != null)
			{
				EventShowPanel(this, null);
			}
			break;
		default:
			base.Serialize(serializer, message);
			break;
		case 136u:
			break;
		}
	}

	protected virtual void SerializeInitialization(ISerializer serializer)
	{
	}

	protected void SerializeGuids(ISerializer serializer)
	{
		string inst = null;
		serializer.Serialize(ref inst);
		TheGuid = new Guid(inst);
		serializer.Serialize(ref inst);
		TheRelativeDockingGuid = new Guid(inst);
		uint inst2 = 0u;
		serializer.Serialize(ref inst2);
		TheDockingType = (EnumDockingType)inst2;
		serializer.Serialize(ref TheDockingProportion);
		AllowCreationIfParent = false;
		serializer.Serialize(ref AllowCreationIfParent);
		TheAllowClose = false;
		serializer.Serialize(ref TheAllowClose);
	}

	public override void Destroy()
	{
		base.Destroy();
	}

	public void SendHiddenState(bool hidden)
	{
		if (!WidgetMgr.Instance.FormDisposing)
		{
			ISerializer writer = WidgetMgr.Instance.Writer;
			writer.Begin();
			uint inst = 166u;
			writer.Serialize(ref inst);
			writer.Serialize(ref RemoteId);
			writer.Serialize(ref hidden);
			writer.End(clearIndex: false);
		}
	}
}
