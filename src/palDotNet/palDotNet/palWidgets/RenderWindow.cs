using System;
using System.Threading;
using System.Windows.Forms;
using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
public class RenderWindow : WidgetPanel, ISupportsMenus
{
	public enum Messages
	{
		eResize = 168,
		ePaint = 169,
		eValid = 170,
		eShowContextMenu = 171,
		eFocusEnter = 172,
		eFocusLeave = 173,
		eMouseInfo = 174,
		eScrollBarVisible = 175,
		eScrollBarLimits = 176,
		eScrollBarValue = 177,
		eScrollBarLargeChange = 178,
		eScrollBarSmallChange = 179,
		eLastRenderWindowMsg = eScrollBarSmallChange
	}

	private bool MainRenderWindow;

	private IntPtr TheHwnd;

	private IRenderWindowVisual TheVisual;

	private ISupportsMenus MenuSupporter;

	private IMenuItem MenuItem;

	private bool ReceivedValidAcknowledgment;

	private bool DontSendHorz;

	private bool DontSendVert;

	public bool IsMainRenderWindow => MainRenderWindow;

	public IntPtr Hwnd
	{
		get
		{
			return TheHwnd;
		}
		set
		{
			TheHwnd = value;
			WriteMessage(168u);
		}
	}

	public IRenderWindowVisual Visual
	{
		get
		{
			return TheVisual;
		}
		set
		{
			TheVisual = value;
		}
	}

	public RenderWindow(uint newId)
		: base(newId)
	{
	}

	public override object MapMenuToVisual(ISupportsMenus menu)
	{
		if (menu == MenuSupporter && MenuItem != null)
		{
			return MenuItem.MenuItem;
		}
		return null;
	}

	public override void AddMapMenuToVisual(ISupportsMenus menu, IMenuItem visual)
	{
		MenuSupporter = menu;
		MenuItem = visual;
	}

	public override bool RemoveMapMenuToVisual(ISupportsMenus menu)
	{
		if (MenuSupporter == menu)
		{
			MenuSupporter = null;
			MenuItem = null;
			return true;
		}
		return false;
	}

	public override void Serialize(ISerializer serializer, uint msgType)
	{
		switch (msgType)
		{
		case 168u:
			new RemotePtr(Hwnd).Serialize(serializer);
			break;
		case 171u:
			if (serializer.Reading && MenuItem != null)
			{
				MenuItem.Show();
			}
			break;
		case 176u:
		{
			bool inst9 = false;
			serializer.Serialize(ref inst9);
			int inst10 = 0;
			int inst11 = 0;
			serializer.Serialize(ref inst10);
			serializer.Serialize(ref inst11);
			if (TheVisual != null && serializer.Reading)
			{
				TheVisual.SetLimits(inst9, inst10, inst11);
			}
			break;
		}
		case 177u:
		{
			bool inst2 = false;
			serializer.Serialize(ref inst2);
			int inst3 = ((TheVisual != null) ? TheVisual.GetValue(inst2) : 0);
			serializer.Serialize(ref inst3);
			if (TheVisual != null && serializer.Reading)
			{
				DontSendHorz = inst2;
				DontSendVert = !inst2;
				TheVisual.SetValue(inst2, inst3);
				DontSendHorz = false;
				DontSendVert = false;
			}
			break;
		}
		case 175u:
		{
			bool inst5 = false;
			serializer.Serialize(ref inst5);
			bool inst6 = false;
			serializer.Serialize(ref inst6);
			if (TheVisual != null && serializer.Reading)
			{
				TheVisual.SetVisible(inst5, inst6);
			}
			break;
		}
		case 178u:
		{
			bool inst12 = false;
			serializer.Serialize(ref inst12);
			int inst13 = 0;
			serializer.Serialize(ref inst13);
			if (TheVisual != null && serializer.Reading)
			{
				TheVisual.SetLargeChange(inst12, inst13);
			}
			break;
		}
		case 179u:
		{
			bool inst7 = false;
			serializer.Serialize(ref inst7);
			int inst8 = 0;
			serializer.Serialize(ref inst8);
			if (TheVisual != null && serializer.Reading)
			{
				TheVisual.SetSmallChange(inst7, inst8);
			}
			break;
		}
		case 126u:
		{
			uint inst4 = 0u;
			serializer.Serialize(ref inst4);
			if (TheVisual != null && serializer.Reading)
			{
				TheVisual.CurrentCursor = (Cursors.EnumCursor)inst4;
			}
			break;
		}
		case 170u:
		{
			bool inst = false;
			serializer.Serialize(ref inst);
			if (!inst)
			{
				ReceivedValidAcknowledgment = true;
			}
			break;
		}
		default:
			base.Serialize(serializer, msgType);
			break;
		}
	}

	protected override void SerializeInitialization(ISerializer serializer)
	{
		serializer.Serialize(ref MainRenderWindow);
		RemotePtr remotePtr = new RemotePtr(Hwnd);
		remotePtr.Serialize(serializer);
		if (serializer.Reading)
		{
			Hwnd = remotePtr;
		}
	}

	public void SendMouseInfo(MouseButtons buttons, int mouseX, int mouseY, int wheelDelta)
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		uint inst = 174u;
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		uint inst2 = (uint)buttons;
		writer.Serialize(ref inst2);
		writer.Serialize(ref mouseX);
		writer.Serialize(ref mouseY);
		writer.Serialize(ref wheelDelta);
		writer.End(clearIndex: false);
	}

	public void SendSliderValueChanged(bool isHorizontal, int newValue)
	{
		if ((!isHorizontal || !DontSendHorz) && (isHorizontal || !DontSendVert))
		{
			ISerializer writer = WidgetMgr.Instance.Writer;
			writer.Begin();
			uint inst = 177u;
			writer.Serialize(ref inst);
			writer.Serialize(ref RemoteId);
			writer.Serialize(ref isHorizontal);
			uint inst2 = (uint)newValue;
			writer.Serialize(ref inst2);
			writer.End(clearIndex: false);
		}
	}

	public void SendIsValid(bool valid)
	{
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		uint inst = 170u;
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		writer.Serialize(ref valid);
		writer.End(clearIndex: false);
		if (!valid)
		{
			ReceivedValidAcknowledgment = false;
			while (!ReceivedValidAcknowledgment)
			{
				WidgetMgr.Instance.Update();
				System.Threading.Thread.Sleep(10);
			}
		}
	}
}
