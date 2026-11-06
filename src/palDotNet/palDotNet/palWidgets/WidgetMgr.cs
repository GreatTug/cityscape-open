using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palVisual;

namespace palDotNet.palWidgets;

public class WidgetMgr
{
	public delegate void RenderWindowIdDel(out IntPtr renderWindow, out IntPtr parentWindow);

	public delegate string FileDialogDel(bool isSave, string type, string ext, string initialPath, uint filterIndex, string title);

	public delegate string DirectoryDialogDel(string titleBar, string initialPath);

	public delegate DialogResult MessageBoxDel(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);

	public delegate void AboutBoxDel(string dllName);

	public delegate void LayoutDel(bool suspend);

	public delegate void ProgressBarDel(bool start, string message);

	public delegate void ProgressBarRenderWindowDel(bool enabled);

	public delegate void HelpTextDel(string message);

	public delegate void RenderWindowDel(bool adding, string title, uint id);

	public delegate void LoadLayoutDel();

	public delegate void EnableDisableAppDel(bool enable);

	private static uint PrevMsgType = 0u;

	private List<Keys> ModiferKeysDown = new List<Keys>();

	private LoadLayoutDel TheLoadLayoutDelegate;

	private int ThePauseCount;

	private bool? FoundPanelWidget;

	public bool FormDisposing;

	private bool ReceivedQuit;

	private bool ReceivedQuitCancelled;

	private bool ReceivedQuitCheckCancelReceived;

	private bool QuitCancelled;

	private bool ShouldSendFinalQuitConfirm;

	private Dictionary<WidgetBase, RemotePtr> LocalToRemote;

	private Dictionary<RemotePtr, WidgetBase> RemoteToLocal;

	private ISerializer TheReader;

	private ISerializer TheWriter;

	private RenderWindowIdDel TheRenderWindowIdDelegate;

	private FileDialogDel TheFileDialogDelegate;

	private DirectoryDialogDel TheDirectoryDialogDelegate;

	private MessageBoxDel TheMessageBoxDelegate;

	private AboutBoxDel TheAboutBoxDelegate;

	private LayoutDel TheLayoutDelegate;

	private ProgressBarDel TheProgressBarDelegate;

	private ProgressBarRenderWindowDel TheProgressBarRenderWindowDelegate;

	private HelpTextDel TheHelpTextDelegate;

	private HelpTextDel TheSetTitleDelegate;

	private RenderWindowDel TheRenderWindowDelegate;

	private EnableDisableAppDel TheEnableDisableAppDelegate;

	private System.Windows.Forms.Timer TheUpdateTimer;

	private static Dictionary<string, Type> WidgetCreators = new Dictionary<string, Type>();

	private static WidgetMgr TheInstance;

	public RenderWindowIdDel RenderWindowIdDelegate
	{
		get
		{
			return TheRenderWindowIdDelegate;
		}
		set
		{
			TheRenderWindowIdDelegate = value;
		}
	}

	public FileDialogDel FileDialogDelegate
	{
		get
		{
			return TheFileDialogDelegate;
		}
		set
		{
			TheFileDialogDelegate = value;
		}
	}

	public DirectoryDialogDel DirectoryDialogDelegate
	{
		get
		{
			return TheDirectoryDialogDelegate;
		}
		set
		{
			TheDirectoryDialogDelegate = value;
		}
	}

	public MessageBoxDel MessageBoxDelegate
	{
		get
		{
			return TheMessageBoxDelegate;
		}
		set
		{
			TheMessageBoxDelegate = value;
		}
	}

	public AboutBoxDel AboutBoxDelegate
	{
		get
		{
			return TheAboutBoxDelegate;
		}
		set
		{
			TheAboutBoxDelegate = value;
		}
	}

	public LayoutDel LayoutDelegate
	{
		get
		{
			return TheLayoutDelegate;
		}
		set
		{
			TheLayoutDelegate = value;
		}
	}

	public ProgressBarDel ProgressBarDelegate
	{
		set
		{
			TheProgressBarDelegate = value;
		}
	}

	public ProgressBarRenderWindowDel ProgressBarRenderWindowDelegate
	{
		set
		{
			TheProgressBarRenderWindowDelegate = value;
		}
	}

	public HelpTextDel SetTitleDelegate
	{
		set
		{
			TheSetTitleDelegate = value;
		}
	}

	public HelpTextDel HelpTextDelegate
	{
		set
		{
			TheHelpTextDelegate = value;
		}
	}

	public RenderWindowDel RenderWindowDelegate
	{
		set
		{
			TheRenderWindowDelegate = value;
		}
	}

	public LoadLayoutDel LoadLayoutDelegate
	{
		get
		{
			return TheLoadLayoutDelegate;
		}
		set
		{
			TheLoadLayoutDelegate = value;
		}
	}

	public EnableDisableAppDel EnableDisableFormDelegate
	{
		get
		{
			return TheEnableDisableAppDelegate;
		}
		set
		{
			TheEnableDisableAppDelegate = value;
		}
	}

	public ISerializer Reader
	{
		get
		{
			return TheReader;
		}
		set
		{
			TheReader = value;
		}
	}

	public ISerializer Writer
	{
		get
		{
			return TheWriter;
		}
		set
		{
			TheWriter = value;
		}
	}

	public int PauseCount => ThePauseCount;

	public static WidgetMgr Instance => TheInstance;

	public System.Windows.Forms.Timer UpdateTimer
	{
		get
		{
			return TheUpdateTimer;
		}
		set
		{
			TheUpdateTimer = value;
		}
	}

	public bool? HasFoundPanelWidget
	{
		get
		{
			return FoundPanelWidget;
		}
		set
		{
			FoundPanelWidget = value;
		}
	}

	public event EventHandler EventQuit;

	public WidgetMgr()
	{
		LocalToRemote = new Dictionary<WidgetBase, RemotePtr>();
		RemoteToLocal = new Dictionary<RemotePtr, WidgetBase>(new RemotePtr.EqualityComparer());
		WidgetCreatorAttribute widgetCreatorAttribute = new WidgetCreatorAttribute();
		ComponentSearcher componentSearcher = new ComponentSearcher(widgetCreatorAttribute.GetType());
		foreach (Type item in componentSearcher)
		{
			object[] customAttributes = item.GetCustomAttributes(widgetCreatorAttribute.GetType(), inherit: false);
			object[] array = customAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				WidgetCreatorAttribute widgetCreatorAttribute2 = (WidgetCreatorAttribute)array[i];
				if (widgetCreatorAttribute2.Aliases != null)
				{
					string[] aliases = widgetCreatorAttribute2.Aliases;
					foreach (string key in aliases)
					{
						WidgetCreators.Add(key, item);
					}
				}
				else
				{
					WidgetCreators.Add(item.Name, item);
				}
			}
		}
		TheInstance = this;
	}

	public DialogResult ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
	{
		return MessageBoxDelegate(text, caption, buttons, icon);
	}

	public void Update()
	{
		Keyboard.Update(this);
		List<WidgetBase> list = new List<WidgetBase>();
		int num = 0;
		_ = PrevMsgType;
		if (TheReader.Begin())
		{
			while (!TheReader.AtEnd)
			{
				uint inst = 0u;
				_ = PrevMsgType;
				TheReader.Serialize(ref inst);
				PrevMsgType = inst;
				RemotePtr remotePtr = default(RemotePtr);
				switch (inst)
				{
				case 127u:
					TheReader.End(clearIndex: true);
					if (EventQuit != null && !QuitCancelled)
					{
						ReceivedQuitCancelled = true;
						ReceivedQuit = true;
						ShouldSendFinalQuitConfirm = true;
						EventQuit(this, EventArgs.Empty);
					}
					return;
				case 128u:
					TheReader.End(clearIndex: true);
					ReceivedQuit = true;
					ShouldSendFinalQuitConfirm = true;
					return;
				case 129u:
					ReceivedQuitCancelled = true;
					TheReader.Serialize(ref QuitCancelled);
					break;
				case 130u:
					ReceivedQuitCheckCancelReceived = true;
					break;
				case 113u:
					if (TheEnableDisableAppDelegate != null)
					{
						TheEnableDisableAppDelegate(enable: false);
					}
					break;
				case 114u:
					TheReader.End(clearIndex: false);
					if (TheEnableDisableAppDelegate != null)
					{
						TheEnableDisableAppDelegate(enable: true);
					}
					TheReader.Begin();
					break;
				case 118u:
				case 119u:
				{
					bool isSave = inst == 119;
					string inst18 = null;
					string inst19 = null;
					string inst20 = null;
					string inst21 = null;
					uint inst22 = 0u;
					TheReader.Serialize(ref inst18);
					TheReader.Serialize(ref inst19);
					TheReader.Serialize(ref inst20);
					TheReader.Serialize(ref inst22);
					TheReader.Serialize(ref inst21);
					inst22++;
					string inst23 = string.Empty;
					if (TheFileDialogDelegate != null)
					{
						inst23 = TheFileDialogDelegate(isSave, inst18, inst19, inst20, inst22, inst21);
					}
					TheWriter.Begin();
					TheWriter.Serialize(ref inst);
					TheWriter.Serialize(ref inst23);
					TheWriter.End(clearIndex: false);
					break;
				}
				case 120u:
				{
					string inst7 = null;
					string inst8 = null;
					string inst9 = null;
					TheReader.Serialize(ref inst7);
					TheReader.Serialize(ref inst8);
					TheReader.Serialize(ref inst9);
					int inst10 = 0;
					TheReader.Serialize(ref inst10);
					string text = string.Empty;
					if (TheDirectoryDialogDelegate != null)
					{
						text = TheDirectoryDialogDelegate(inst7, inst8);
					}
					TheWriter.Begin();
					TheWriter.Serialize(ref inst);
					string inst11 = ((text == null) ? string.Empty : text);
					TheWriter.Serialize(ref inst11);
					TheWriter.End(clearIndex: false);
					break;
				}
				case 116u:
					ThePauseCount++;
					break;
				case 117u:
					ThePauseCount--;
					if (ThePauseCount < 0)
					{
						throw new ApplicationException("Too many application unpauses!");
					}
					break;
				case 121u:
				{
					string inst15 = null;
					string inst16 = null;
					uint inst17 = 0u;
					TheReader.Serialize(ref inst15);
					TheReader.Serialize(ref inst16);
					TheReader.Serialize(ref inst17);
					MessageBoxButtons buttons = (MessageBoxButtons)inst17;
					TheReader.Serialize(ref inst17);
					MessageBoxIcon icon = (MessageBoxIcon)inst17;
					TheReader.End(clearIndex: false);
					DialogResult dialogResult = DialogResult.None;
					if (TheMessageBoxDelegate != null)
					{
						dialogResult = TheMessageBoxDelegate(inst16, inst15, buttons, icon);
					}
					TheReader.Begin();
					TheWriter.Begin();
					TheWriter.Serialize(ref inst);
					inst17 = (uint)dialogResult;
					TheWriter.Serialize(ref inst17);
					TheWriter.End(clearIndex: false);
					break;
				}
				case 122u:
				{
					string inst14 = "";
					TheReader.Serialize(ref inst14);
					if (TheAboutBoxDelegate != null)
					{
						TheAboutBoxDelegate(inst14);
					}
					TheWriter.Begin();
					TheWriter.Serialize(ref inst);
					TheWriter.End(clearIndex: false);
					break;
				}
				case 100u:
				case 101u:
				{
					bool flag = inst == 100;
					string inst5 = " ";
					if (flag)
					{
						TheReader.Serialize(ref inst5);
					}
					if (TheProgressBarDelegate != null)
					{
						TheProgressBarDelegate(flag, inst5);
					}
					break;
				}
				case 102u:
				{
					bool inst4 = false;
					TheReader.Serialize(ref inst4);
					if (TheProgressBarRenderWindowDelegate != null)
					{
						TheProgressBarRenderWindowDelegate(inst4);
					}
					break;
				}
				case 105u:
				{
					string inst25 = " ";
					TheReader.Serialize(ref inst25);
					if (TheSetTitleDelegate != null)
					{
						TheSetTitleDelegate(inst25);
					}
					break;
				}
				case 106u:
				{
					string inst24 = " ";
					TheReader.Serialize(ref inst24);
					if (TheHelpTextDelegate != null)
					{
						TheHelpTextDelegate(inst24);
					}
					break;
				}
				case 107u:
					if (TheLayoutDelegate != null)
					{
						TheLayoutDelegate(suspend: true);
					}
					break;
				case 108u:
					if (list.Count > 0)
					{
						num++;
					}
					else if (TheLayoutDelegate != null)
					{
						TheLayoutDelegate(suspend: false);
					}
					break;
				case 109u:
				case 110u:
				{
					bool flag2 = inst == 109;
					uint inst12 = 0u;
					string inst13 = null;
					if (flag2)
					{
						TheReader.Serialize(ref inst13);
					}
					TheReader.Serialize(ref inst12);
					if (TheRenderWindowDelegate != null)
					{
						TheRenderWindowDelegate(flag2, inst13, inst12);
					}
					break;
				}
				case 112u:
					if (TheLoadLayoutDelegate != null)
					{
						TheLoadLayoutDelegate();
					}
					break;
				case 131u:
					FormAppBase.Instance.FormMinimize = true;
					break;
				case 132u:
					FormAppBase.Instance.FormMinimize = false;
					break;
				case 133u:
					FormAppBase.Instance.BringToFront();
					break;
				case 111u:
				{
					bool inst6 = false;
					TheReader.Serialize(ref inst6);
					FoundPanelWidget = inst6;
					break;
				}
				case 103u:
					Units.SerializeSetCurrent(TheReader);
					break;
				case 104u:
					Units.Serialize(TheReader);
					break;
				case 150u:
					FileServer.Open();
					break;
				case 151u:
					FileServer.Write();
					break;
				case 152u:
					FileServer.Close();
					break;
				case 153u:
				case 154u:
				case 155u:
				case 156u:
					Help.Read((WidgetBase.CoreMessages)inst, TheReader);
					break;
				case 157u:
					if (Menu.MenuStripLayoutDelegate != null)
					{
						Menu.MenuStripLayoutDelegate(suspend: true);
					}
					break;
				case 158u:
					if (Menu.MenuStripLayoutDelegate != null)
					{
						Menu.MenuStripLayoutDelegate(suspend: false);
					}
					break;
				case 136u:
				{
					remotePtr.Serialize(TheReader);
					string inst2 = "";
					TheReader.Serialize(ref inst2);
					uint inst3 = 0u;
					TheReader.Serialize(ref inst3);
					WidgetCreators.TryGetValue(inst2, out var value2);
					if (value2 != null)
					{
						WidgetBase widgetBase2 = Activator.CreateInstance(args: new object[1] { inst3 }, type: value2) as WidgetBase;
						remotePtr.SetId();
						LocalToRemote.Add(widgetBase2, remotePtr);
						RemoteToLocal.Add(remotePtr, widgetBase2);
						widgetBase2.Serialize(TheReader, 137u);
						if (widgetBase2.Parent != null)
						{
							widgetBase2.Parent.AddWidget(widgetBase2);
						}
					}
					break;
				}
				case 138u:
				{
					remotePtr.Serialize(TheReader);
					TheReader.End(clearIndex: false);
					WidgetBase widgetBase = MapRemoteToLocal(remotePtr);
					widgetBase.Destroy();
					RemotePtr key = LocalToRemote[widgetBase];
					LocalToRemote.Remove(widgetBase);
					RemoteToLocal.Remove(key);
					TheReader.Begin();
					break;
				}
				default:
				{
					remotePtr.Serialize(TheReader);
					RemoteToLocal.TryGetValue(remotePtr, out var value);
					value?.Serialize(TheReader, inst);
					break;
				}
				}
			}
		}
		TheReader.End(clearIndex: true);
		foreach (WidgetBase item in list)
		{
			item.Destroy();
			RemotePtr key2 = LocalToRemote[item];
			LocalToRemote.Remove(item);
			RemoteToLocal.Remove(key2);
		}
		while (--num >= 0 && TheLayoutDelegate != null)
		{
			TheLayoutDelegate(suspend: false);
		}
	}

	public void SendFinalQuitConfirm()
	{
		if (ShouldSendFinalQuitConfirm)
		{
			TheWriter.Begin();
			uint inst = 128u;
			TheWriter.Serialize(ref inst);
			TheWriter.End(clearIndex: false);
			ShouldSendFinalQuitConfirm = false;
		}
	}

	public IEnumerable<WidgetBase> AllWidgets => LocalToRemote.Keys;

	public WidgetBase GetWidgetById(uint id)
	{
		foreach (WidgetBase w in LocalToRemote.Keys)
		{
			if (w.Id == id)
				return w;
		}
		return null;
	}

	public WidgetBase MapRemoteToLocal(RemotePtr remotePtr)
	{
		RemoteToLocal.TryGetValue(remotePtr, out var value);
		return value;
	}

	public RemotePtr MapLocalToRemote(WidgetBase widget)
	{
		if (widget != null)
		{
			LocalToRemote.TryGetValue(widget, out var value);
			return value;
		}
		return default(RemotePtr);
	}

	public void SendQuitMessage()
	{
		if (!ReceivedQuit)
		{
			TheWriter.Begin();
			uint inst = 127u;
			TheWriter.Serialize(ref inst);
			TheWriter.End(clearIndex: false);
			QuitMessageWaitLoop(ref ReceivedQuit);
		}
	}

	public bool SendQuitCheckCancelMessage()
	{
		if (!ReceivedQuitCancelled)
		{
			TheWriter.Begin();
			uint inst = 129u;
			TheWriter.Serialize(ref inst);
			TheWriter.End(clearIndex: false);
			QuitMessageWaitLoop(ref ReceivedQuitCheckCancelReceived);
			while (!ReceivedQuitCancelled)
			{
				Update();
				System.Threading.Thread.Sleep(10);
			}
			if (QuitCancelled)
			{
				ReceivedQuitCancelled = false;
				ReceivedQuitCheckCancelReceived = false;
				QuitCancelled = false;
				return true;
			}
			return false;
		}
		return false;
	}

	private void QuitMessageWaitLoop(ref bool checkCondition)
	{
		DateTime now = DateTime.Now;
		while (true)
		{
			Update();
			System.Threading.Thread.Sleep(0);
			if (checkCondition)
			{
				return;
			}
			if ((DateTime.Now - now).TotalSeconds >= 15.0)
			{
				DialogResult dialogResult = MessageBox.Show("Application thread is not responding." + Environment.NewLine + "Press 'Yes' to continue waiting for application to exit." + Environment.NewLine + "Press 'No' to kill this process.", "Application Thread Not Responding", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (dialogResult == DialogResult.No)
				{
					break;
				}
				now = DateTime.Now;
			}
		}
		Process.GetCurrentProcess().Kill();
	}

	public void SendMinimized(bool minimized)
	{
		TheWriter.Begin();
		uint inst = (minimized ? 131u : 132u);
		TheWriter.Serialize(ref inst);
		TheWriter.End(clearIndex: false);
	}

	public void SendKeyInfo(Keys keys, WidgetBase.CoreMessages msg)
	{
		switch (msg)
		{
		case WidgetBase.CoreMessages.eKeyReleased:
			keys = Keyboard.AlreadySentRelease(keys);
			if (keys == Keys.None)
			{
				return;
			}
			break;
		case WidgetBase.CoreMessages.eKeyPressed:
			Keyboard.CheckAndAddModifier(keys);
			break;
		}
		SendKeyInfoNoCheck(keys, msg);
	}

	public void SendKeyInfoNoCheck(Keys keys, WidgetBase.CoreMessages msg)
	{
		TheWriter.Begin();
		uint inst = (uint)msg;
		TheWriter.Serialize(ref inst);
		uint inst2 = (uint)keys;
		TheWriter.Serialize(ref inst2);
		TheWriter.End(clearIndex: false);
	}

	public void SendMsg(WidgetBase.CoreMessages msg)
	{
		TheWriter.Begin();
		uint inst = (uint)msg;
		TheWriter.Serialize(ref inst);
		TheWriter.End(clearIndex: false);
	}

	public bool PauseApp()
	{
		if (FormDisposing)
		{
			return false;
		}
		int pauseCount = PauseCount;
		bool enabled = UpdateTimer.Enabled;
		if (enabled)
		{
			UpdateTimer.Stop();
		}
		SendMsg(WidgetBase.CoreMessages.ePauseApp);
		int enterCount = Reader.EnterCount;
		if (enterCount > 0)
		{
			Reader.End(clearIndex: false);
		}
		do
		{
			Update();
			System.Threading.Thread.Sleep(1);
		}
		while (pauseCount == PauseCount);
		if (enterCount > 0)
		{
			Reader.Begin();
		}
		if (enabled)
		{
			UpdateTimer.Start();
		}
		return true;
	}

	public void UnpauseApp()
	{
		Instance.SendMsg(WidgetBase.CoreMessages.eUnPauseApp);
	}

	public void SendLayoutCreatePanel(Guid guid)
	{
		WidgetPanel.IsSerializingLayout = true;
		HasFoundPanelWidget = null;
		if (Writer != null)
		{
			Writer.Begin();
			uint inst = 111u;
			Writer.Serialize(ref inst);
			string inst2 = guid.ToString();
			Writer.Serialize(ref inst2);
			Writer.End(clearIndex: false);
		}
		int enterCount = (Reader != null) ? Reader.EnterCount : 0;
		if (enterCount > 0)
		{
			Reader.End(clearIndex: false);
		}
		int retries = 0;
		do
		{
			Update();
			retries++;
			if (retries > 20 && !HasFoundPanelWidget.HasValue)
			{
				break;
			}
		}
		while (!HasFoundPanelWidget.HasValue);
		if (enterCount > 0)
		{
			Reader.Begin();
		}
		WidgetPanel.IsSerializingLayout = false;
	}
}
