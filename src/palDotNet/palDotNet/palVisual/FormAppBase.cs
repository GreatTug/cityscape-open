using System;
using System.Threading;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class FormAppBase : Form, IFormApp
{
	public delegate FormAppBase InstantiateFormDel(FormAppBaseInit init);

	protected delegate void theDelNoArgs();

	protected delegate void theDel(bool fullScreen);

	private int FullScreen = -1;

	public static int ClosedSync = 0;

	private static InstantiateFormDel m_InstantiateForm;

	protected static FormAppBase TheForm;

	protected static int Initialized = 0;

	public bool FormMinimize
	{
		set
		{
			if (base.InvokeRequired)
			{
				Invoke(args: new object[1] { value }, method: (theDel)delegate
				{
					if (base.WindowState != FormWindowState.Minimized && value)
					{
						base.WindowState = FormWindowState.Minimized;
					}
					else if (base.WindowState == FormWindowState.Minimized && !value)
					{
						base.WindowState = FormWindowState.Normal;
					}
				});
			}
			else if (base.WindowState != FormWindowState.Minimized && value)
			{
				base.WindowState = FormWindowState.Minimized;
			}
			else if (base.WindowState == FormWindowState.Minimized && !value)
			{
				base.WindowState = FormWindowState.Normal;
			}
		}
	}

	public virtual bool MenuVisible
	{
		set
		{
		}
	}

	public virtual System.Windows.Forms.Timer Timer
	{
		get
		{
			throw new Exception("Needs to be overridden.");
		}
	}

	public static InstantiateFormDel InstantiateForm
	{
		set
		{
			m_InstantiateForm = value;
		}
	}

	public static FormAppBase Instance => TheForm;

	public void SetFullScreen(bool fullScreen)
	{
		if (base.InvokeRequired)
		{
			if ((FullScreen != 1 || !fullScreen) && (FullScreen != 0 || fullScreen))
			{
				Invoke(args: new object[1] { fullScreen }, method: new theDel(SetFullScreen));
			}
		}
		else if (TheForm != null)
		{
			FullScreen = (fullScreen ? 1 : 0);
			if (fullScreen)
			{
				Hide();
				return;
			}
			Show();
			Focus();
		}
	}

	[STAThread]
	public static void StartRunThread(string appName, uint xRes, uint yRes, string toolWindowGuid, string outputWindowGuid, string widgetTheme, string appIconPath, bool lockRenderWindows, bool bits64)
	{
		FormAppBaseInit initParams = default(FormAppBaseInit);
		initParams.AppName = appName;
		initParams.XRes = xRes;
		initParams.YRes = yRes;
		initParams.ToolWindowGuidStr = toolWindowGuid;
		initParams.OutputWindowGuidStr = outputWindowGuid;
		initParams.WidgetTheme = widgetTheme;
		initParams.AppIconPath = appIconPath;
		initParams.LockRenderWindows = lockRenderWindows;
		RemotePtr.Use64Bit = bits64;
		WidgetMgr.Instance.EventQuit += delegate
		{
			Application.Exit();
		};
		ThreadStart threadStart = delegate
		{
			Application.EnableVisualStyles();
			if (m_InstantiateForm != null)
			{
				TheForm = m_InstantiateForm(initParams);
			}
			else
			{
				TheForm = new FormApp(initParams);
			}
			Application.Run(TheForm);
		};
		palDotNet.palCore.Thread thread = new palDotNet.palCore.Thread(threadStart);
		thread.Inst.Name = "Form Thread";
		thread.Inst.SetApartmentState(ApartmentState.STA);
		thread.Inst.Start();
		while (Initialized == 0)
		{
			System.Threading.Thread.Sleep(10);
		}
	}

	void IFormApp.BringToFront()
	{
		BringToFront();
	}
}
