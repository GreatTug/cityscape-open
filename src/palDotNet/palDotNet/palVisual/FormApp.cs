using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using palDotNet.Properties;
using palDotNet.palCore;
using palDotNet.palDialog;
using palDotNet.palVisualPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class FormApp : FormAppBase
{
	private const int FileVersion = 4;

	private static bool LoadLayoutCalled;

	private static IntPtr RenderWindowId;

	private static IntPtr FormId;

	private static MenuStrip TheMenuStrip;

	private Guid ToolWindowGuid;

	private Guid OutputWindowGuid;

	private DocumentRenderView TheViewDocument;

	private ViewTool TheToolWidgets;

	private ViewOutput TheToolOutput;

	private List<DocumentView> TheViewDocuments = new List<DocumentView>();

	private int StartupXRes = 800;

	private int StartupYRes = 600;

	private List<string> ProgressBarStack = new List<string>();

	private Point LastLocation;

	private FileDialog FileDialog;

	private bool ProgressBarStarted;

	private bool Resizing;

	private DateTime ProgressBarTimeStamp = default(DateTime);

	private bool LoadedSettings;

	private string WidgetTheme;

	private IContainer components;

	private StatusStrip statusStrip1;

	private System.Windows.Forms.MenuStrip menuStrip1;

	private DockPanel dockPanel1;

	private System.Windows.Forms.Timer timer1;

	private ToolStripStatusLabel toolStripStatusLabelProgressBar;

	private ToolStripProgressBar toolStripProgressBar1;

	private ToolTip toolTip1;

	private ToolStripStatusLabel toolStripStatusLabelHelpText;

	private System.Windows.Forms.Timer timerProgressBar;

	internal Settings Settings => Settings.Default;

	public override System.Windows.Forms.Timer Timer => timer1;

	public DockPanel DockPanel => dockPanel1;

	public ViewTool ToolWidgets => TheToolWidgets;

	public ViewOutput ToolOutput => TheToolOutput as ViewOutput;

	public override bool MenuVisible
	{
		set
		{
			if (base.InvokeRequired)
			{
				Invoke(args: new object[1] { value }, method: (theDel)delegate
				{
					MenuVisible = value;
				});
			}
			else
			{
				menuStrip1.Visible = value;
			}
		}
	}

	public bool LockDocking
	{
		set
		{
			if (base.InvokeRequired)
			{
				Invoke(args: new object[1] { value }, method: (theDel)delegate
				{
					LockDocking = value;
				});
			}
			else
			{
				dockPanel1.AllowEndUserDocking = !value;
			}
		}
	}

	public bool ToolOutputVisible
	{
		set
		{
			if (base.InvokeRequired)
			{
				Invoke(args: new object[1] { value }, method: (theDel)delegate
				{
					ToolOutputVisible = value;
				});
			}
			else
			{
				TheToolOutput.IsHidden = !value;
				TheToolOutput.Visible = value;
			}
		}
	}

	private FormApp()
	{
		_ = System.Windows.Forms.Cursors.VSplit;
		InitializeComponent();
	}

	public FormApp(FormAppBaseInit init)
		: this()
	{
		WidgetTheme = init.WidgetTheme;
		LoadIcon(init.AppIconPath);
		CheckConfigurationFile();
		if (init.LockRenderWindows)
		{
			dockPanel1.DocumentStyle = DocumentStyle.DockingSdi;
		}
		else
		{
			dockPanel1.DocumentStyle = DocumentStyle.DockingMdi;
		}
		StartupXRes = (int)init.XRes;
		StartupYRes = (int)init.YRes;
		ToolWindowGuid = new Guid(init.ToolWindowGuidStr);
		OutputWindowGuid = new Guid(init.OutputWindowGuidStr);
		Text = init.AppName;
		WidgetMgr.Instance.UpdateTimer = Timer;
		WidgetMgr.Instance.LoadLayoutDelegate = LoadLayout;
		WidgetBase.EventCreate += delegate(object s, EventArgs e)
		{
			CreateWidgetPanel(s as Bank);
		};
		WidgetBase.EventDestroy += delegate(object s, EventArgs e)
		{
			if (s is palDotNet.palWidgets.Menu menu)
			{
				TheMenuStrip.RemoveMenuItem(menu);
			}
		};
		WidgetMgr.Instance.DirectoryDialogDelegate = delegate(string description, string initialPath)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			try
			{
				if (!string.IsNullOrEmpty(description))
				{
					folderBrowserDialog.Description = description;
				}
				if (!string.IsNullOrEmpty(initialPath) && Directory.Exists(initialPath))
				{
					folderBrowserDialog.SelectedPath = initialPath;
				}
			}
			catch
			{
			}
			DialogResult dialogResult = folderBrowserDialog.ShowDialog(this);
			return (dialogResult == DialogResult.OK) ? folderBrowserDialog.SelectedPath : null;
		};
		WidgetMgr.Instance.MessageBoxDelegate = delegate(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			BringToFront();
			return MessageBox.Show(this, text, caption, buttons, icon);
		};
		WidgetMgr.Instance.AboutBoxDelegate = AboutBox.Show;
		PalTabbedView.Register(dockPanel1, typeof(palDotNet.palWidgets.ListView), typeof(ListViewControl));
		PalTabbedView.Register(dockPanel1, typeof(palDotNet.palWidgets.TreeView), typeof(TreeViewControl));
		PalTabbedView.Register(dockPanel1, typeof(LibraryView), typeof(LibraryControl));
		PalTabbedView.Register(dockPanel1, typeof(PropertyGridView), typeof(PropertyGridControl));
		DialogWidgetHolder.Register(typeof(palDotNet.palWidgets.ListView), typeof(ListViewControl));
		DialogWidgetHolder.Register(typeof(palDotNet.palWidgets.TreeView), typeof(TreeViewControl));
		DialogWidgetHolder.Register(typeof(LibraryView), typeof(LibraryControl));
		DialogWidgetHolder.Register(typeof(PropertyGridView), typeof(PropertyGridControl));
		DocumentView.Register();
		DocumentRenderView.Register(dockPanel1);
		DocumentWebView.Register(dockPanel1);
		ToolStrip.Register();
		WidgetMgr.Instance.ProgressBarDelegate = UpdateProgressBar;
		WidgetMgr.Instance.ProgressBarRenderWindowDelegate = delegate(bool enabled)
		{
			ControlRender.ProgressBarRenderWindowEnabled = enabled;
		};
		WidgetMgr.Instance.HelpTextDelegate = UpdateHelpText;
		WidgetMgr.Instance.SetTitleDelegate = delegate(string titleString)
		{
			Text = titleString;
		};
		WidgetMgr.Instance.EnableDisableFormDelegate = delegate(bool enable)
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = enable;
			}
		};
		WidgetDialogBox.UpdateThread = (WidgetDialogBox.UpdateThreadDel)Delegate.Combine(WidgetDialogBox.UpdateThread, new WidgetDialogBox.UpdateThreadDel(ThreadedUpdate));
		FileDialog = new FileDialog(this);
	}

	public void InitializeWidgetView()
	{
		TheToolWidgets = new ViewPropertyGrid(ToolWindowGuid);
	}

	public void DefaultLayout()
	{
		TheMenuStrip = new MenuStrip(base.MainMenuStrip);
		palDotNet.palWidgets.Menu.VisualCreator = (EventHandler)Delegate.Combine(palDotNet.palWidgets.Menu.VisualCreator, (EventHandler)delegate(object s, EventArgs ea)
		{
			TheMenuStrip.AddMenuItem(s as palDotNet.palWidgets.Menu);
		});
		palDotNet.palWidgets.Menu.MenuStripLayoutDelegate = delegate(bool suspend)
		{
			if (suspend)
			{
				base.MainMenuStrip.SuspendLayout();
			}
			else
			{
				base.MainMenuStrip.ResumeLayout();
			}
		};
		dockPanel1.SuspendLayout(allWindows: true);
		TheViewDocument = new DocumentRenderView();
		TheViewDocument.Width = StartupXRes;
		TheViewDocument.Height = StartupYRes;
		TheViewDocument.TabText = "Render";
		DocumentRenderView.MainViewDocument = TheViewDocument;
		RenderWindowId = TheViewDocument.RenderWindowHandle;
		FormId = base.Handle;
		WidgetMgr.Instance.RenderWindowIdDelegate = delegate(out IntPtr renderWindow, out IntPtr parentWindow)
		{
			renderWindow = RenderWindowId;
			parentWindow = FormId;
		};
		TheToolOutput = new ViewOutput(OutputWindowGuid);
		CommandBar.WriteLineDelegate = (CommandBar.WriteLineDel)Delegate.Combine(CommandBar.WriteLineDelegate, new CommandBar.WriteLineDel(TheToolOutput.WriteLine));
	}

	public void LoadLayout()
	{
		if (base.InvokeRequired)
		{
			Invoke(new theDelNoArgs(LoadLayout));
			return;
		}
		bool flag = false;
		if (LoadedSettings)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(Settings.DockPanelLayout);
			MemoryStream stream = new MemoryStream(bytes);
			if (Settings.FormSize.Width != 0 && Settings.FormSize.Height != 0)
			{
				base.Size = Settings.FormSize;
			}
			if (bytes != null && bytes.Length > 0)
			{
				try
				{
					dockPanel1.ResumeLayout(performLayout: true, allWindows: true);
					dockPanel1.LoadFromXml(stream, GetContentFromPersistString);
					flag = true;
					foreach (PalDockContent value in PalDockContent.DockContents.Values)
					{
						value.LoadComponentSettings();
					}
					TheToolOutput.LoadComponentSettings();
				}
				catch
				{
					MessageBox.Show("Failed to load window layout file - reverting to default layout", "Layout File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}
		if (!flag)
		{
			dockPanel1.ResumeLayout(performLayout: true, allWindows: true);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
			fileNameWithoutExtension += ".DefaultLayout.xml";
			bool flag2 = File.Exists(fileNameWithoutExtension);
			if (flag2)
			{
				try
				{
					using StreamReader streamReader = new StreamReader(fileNameWithoutExtension);
					dockPanel1.LoadFromXml(streamReader.BaseStream, GetContentFromPersistString);
				}
				catch
				{
					flag2 = false;
				}
			}
			if (!flag2)
			{
				Assembly assembly = typeof(FormApp).Assembly;
				Stream manifestResourceStream = assembly.GetManifestResourceStream("palDotNet.palVisual.DefaultLayout.xml");
				dockPanel1.LoadFromXml(manifestResourceStream, GetContentFromPersistString);
			}
			if (TheViewDocument.IsFloat)
			{
				throw new NotImplementedException();
			}
			Size size = new Size(StartupXRes - TheViewDocument.Size.Width, StartupYRes - TheViewDocument.Size.Height);
			base.Size = new Size(base.Width + size.Width, base.Height + size.Height);
		}
		WidgetMgr.Instance.SendMsg(WidgetBase.CoreMessages.eLoadLayout);
		TheViewDocument.Focus();
		LoadLayoutCalled = true;
	}

	private void Update(object sender, EventArgs e)
	{
		Timer.Stop();
		WidgetMgr.Instance.Update();
		ViewPropertyGrid viewPropertyGrid = TheToolWidgets as ViewPropertyGrid;
		viewPropertyGrid.TheGrid.UnPauseUpdate(forcedPause: true);
		Timer.Start();
	}

	private void ThreadedUpdate(ref bool stopThread, object lockObject)
	{
		bool flag = false;
		do
		{
			if (base.InvokeRequired)
			{
				Invoke(new MethodInvoker(WidgetMgr.Instance.Update));
			}
			else
			{
				WidgetMgr.Instance.Update();
			}
			System.Threading.Thread.Sleep(10);
			lock (lockObject)
			{
				flag = stopThread;
			}
		}
		while (!flag);
	}

	private void UpdateHelpText(string message)
	{
		toolStripStatusLabelHelpText.Text = message;
	}

	private void UpdateProgressBar(bool start, string message)
	{
		SuspendLayout();
		toolStripStatusLabelProgressBar.Visible = true;
		ProgressBarStarted = start;
		if (start)
		{
			toolStripStatusLabelProgressBar.Text = message;
			if (ProgressBarStack.Count == 0)
			{
				toolStripProgressBar1.Visible = true;
				ProgressBarTimeStamp = DateTime.Now;
				base.UseWaitCursor = true;
				timerProgressBar.Start();
			}
			ProgressBarStack.Add(message);
		}
		else
		{
			int count = ProgressBarStack.Count;
			if (count != 0)
			{
				if (count > 1)
				{
					toolStripStatusLabelProgressBar.Text = ProgressBarStack[count - 2];
				}
				else
				{
					toolStripProgressBar1.Visible = false;
					ControlRender.UpdateProgressBar(startBar: false, null);
					base.UseWaitCursor = false;
					Cursor = System.Windows.Forms.Cursors.Default;
					TimeSpan timeSpan = DateTime.Now - ProgressBarTimeStamp;
					string text = ProgressBarStack[0] + " completed in approx. ";
					bool flag = false;
					bool flag2 = false;
					if (timeSpan.Hours > 0)
					{
						text = text + timeSpan.Hours + " hours";
						flag = true;
						flag2 = true;
					}
					if (timeSpan.Minutes > 0)
					{
						if (flag)
						{
							text += ",";
						}
						text = text + timeSpan.Minutes + " min.";
						flag = true;
						flag2 = true;
					}
					if (timeSpan.Seconds > 0)
					{
						if (flag)
						{
							text += ",";
						}
						text = text + timeSpan.Seconds + " secs.";
						flag = true;
						flag2 = true;
					}
					if (!flag2)
					{
						text += "< 1 sec.";
					}
					toolStripStatusLabelProgressBar.Text = text;
				}
				ProgressBarStack.RemoveAt(count - 1);
			}
		}
		toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
		ResumeLayout();
	}

	private IDockContent GetContentFromPersistString(string persistString)
	{
		string[] array = persistString.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
		string text = null;
		IDockContent dockContent = null;
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			string[] array3 = text2.Split(new char[1] { '=' }, StringSplitOptions.RemoveEmptyEntries);
			if (array3.Length % 2 != 0)
			{
				continue;
			}
			for (int j = 0; j < array3.Length; j += 2)
			{
				if (array3[j] == "Guid")
				{
					text = array3[j + 1];
					try
					{
						Guid guid = new Guid(text);
						if (guid == TheToolOutput.Guid)
						{
							return TheToolOutput;
						}
						if (guid == TheViewDocument.Guid)
						{
							return TheViewDocument;
						}
						if (guid == TheToolWidgets.Guid)
						{
							return TheToolWidgets;
						}
						WidgetMgr.Instance.SendLayoutCreatePanel(guid);
						if (dockContent == null)
						{
							dockContent = PalDockContent.Find(guid);
						}
					}
					catch
					{
						return dockContent;
					}
				}
				else if (array3[j] == "PrevDockState" && dockContent is PalDockContent palDockContent)
				{
					palDockContent.PrevDockState = (DockState)Enum.Parse(typeof(DockState), array3[j + 1]);
				}
			}
		}
		return dockContent;
	}

	private static void CheckConfigurationFile()
	{
		try
		{
			ConfigurationManager.GetSection("dummyName");
			if (Settings.Default.CallUpgrade)
			{
				Settings.Default.Upgrade();
				Settings.Default.CallUpgrade = false;
				palDotNet.palCore.ApplicationSettingsBase.DoUpgrade = true;
				Settings.Default.Save();
			}
		}
		catch (ConfigurationErrorsException ex)
		{
			if (Path.GetFileName(ex.Filename).ToLower().EndsWith(".exe.config"))
			{
				WidgetMgr.Instance.ShowMessageBox("It looks like the file '" + ex.Filename + "' has been corrupted.  Please reinstall the application.", "Possible File Corruption", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Environment.Exit(-1);
				return;
			}
			int exitCode = -1;
			File.Delete(ex.Filename);
			if (!File.Exists(ex.Filename))
			{
				try
				{
					Process process = new Process();
					process.StartInfo = new ProcessStartInfo();
					process.StartInfo.FileName = Application.ExecutablePath;
					process.StartInfo.Arguments = Environment.CommandLine;
					process.Start();
					process.WaitForExit();
					exitCode = process.ExitCode;
				}
				catch (Exception)
				{
					WidgetMgr.Instance.ShowMessageBox("Configuration Error.  Please restart the application.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				Environment.Exit(exitCode);
			}
		}
		catch (Exception ex3)
		{
			WidgetMgr.Instance.ShowMessageBox("UnHandled .config file exception: " + ex3.Message, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void LoadIcon(string appIconPath)
	{
		try
		{
			if (!string.IsNullOrEmpty(appIconPath))
			{
				Icon icon = null;
				IconLoaderHelper.Load(appIconPath, null, out icon);
				base.Icon = icon;
			}
		}
		catch (Exception)
		{
		}
	}

	private void FormApp_Load(object sender, EventArgs e)
	{
		CheckConfigurationFile();
		InitializeWidgetView();
		bool flag = Settings.FileVersion == 4;
		if (!flag)
		{
			palDotNet.palCore.ApplicationSettingsBase.DoUpgrade = false;
		}
		LoadedSettings = Settings.Loaded && flag;
		bool flag2 = false;
		if (Control.ModifierKeys == Keys.Shift)
		{
			Form form = new Form();
			form.TopMost = true;
			DialogResult dialogResult = MessageBox.Show(form, "Would you like to reset your window layouts?", "Reset Window Layout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			flag2 = dialogResult == DialogResult.Yes;
		}
		else
		{
			flag2 = !flag;
		}
		if (flag2)
		{
			Settings.Default.Reset();
			Settings.Default.CallUpgrade = false;
			Settings.Default.Save();
		}
		Settings.Loaded = true;
		DefaultLayout();
		if (!LoadLayoutCalled)
		{
			LoadLayout();
		}
		LastLocation = base.Location;
		try
		{
			base.Location = Settings.FormLocation;
			base.WindowState = Settings.FormWindowState;
			LoadedSettings = Settings.Loaded && Settings.FileVersion == 4;
			Settings.FileVersion = 4;
		}
		catch
		{
		}
		TheViewDocument.Render.Focus();
		Interlocked.Add(ref FormAppBase.Initialized, 1);
		timer1.Start();
	}

	private void FormApp_FormClosed(object sender, FormClosedEventArgs e)
	{
		ViewPropertyGrid viewPropertyGrid = TheToolWidgets as ViewPropertyGrid;
		viewPropertyGrid.TheGrid.Clear();
		Focus();
		Application.DoEvents();
		WidgetMgr.Instance.SendQuitMessage();
		Interlocked.Add(ref FormAppBase.ClosedSync, 1);
	}

	private void FormApp_FormClosing(object sender, FormClosingEventArgs e)
	{
		bool enabled = timer1.Enabled;
		if (enabled)
		{
			timer1.Stop();
		}
		if (WidgetMgr.Instance.SendQuitCheckCancelMessage())
		{
			e.Cancel = true;
			if (enabled)
			{
				timer1.Start();
			}
		}
		else
		{
			if (!LoadLayoutCalled)
			{
				return;
			}
			Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
			using MemoryStream memoryStream = new MemoryStream();
			dockPanel1.SaveAsXml(memoryStream, Encoding.ASCII);
			byte[] buffer = memoryStream.GetBuffer();
			char[] array = Encoding.ASCII.GetChars(buffer);
			for (int num = array.Length - 1; num >= 0; num--)
			{
				if (array[num] != 0)
				{
					Array.Resize(ref array, num + 1);
					break;
				}
			}
			string text = new string(array);
			text = text.Trim();
			Settings.DockPanelLayout = text;
			if (base.WindowState == FormWindowState.Minimized)
			{
				Settings.FormWindowState = FormWindowState.Normal;
				Settings.FormLocation = LastLocation;
				Settings.FormSize = new Size(0, 0);
			}
			else
			{
				if (base.WindowState == FormWindowState.Maximized)
				{
					Settings.FormLocation = LastLocation;
					Settings.FormSize = new Size(0, 0);
				}
				else
				{
					Settings.FormLocation = base.Location;
					Settings.FormSize = base.Size;
				}
				Settings.FormWindowState = base.WindowState;
			}
			Settings.FileVersion = 4;
			Settings.Default.Save();
		}
	}

	private void FormApp_Resize(object sender, EventArgs e)
	{
		if (base.WindowState == FormWindowState.Normal)
		{
			LastLocation = base.Location;
		}
		if (!Resizing)
		{
			bool minimized = base.WindowState == FormWindowState.Minimized;
			WidgetMgr.Instance.SendMinimized(minimized);
		}
	}

	private void FormApp_ResizeBegin(object sender, EventArgs e)
	{
		Resizing = true;
		WidgetMgr.Instance.SendMinimized(minimized: true);
	}

	private void FormApp_ResizeEnd(object sender, EventArgs e)
	{
		Resizing = false;
		WidgetMgr.Instance.SendMinimized(minimized: false);
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void CreateWidgetPanel(Bank bank)
	{
		if (bank != null && bank.IsWidgetPanel)
		{
			ViewPropertyGrid viewPropertyGrid = TheToolWidgets as ViewPropertyGrid;
			if (viewPropertyGrid.TheGrid.WidgetBank != null)
			{
				throw new ApplicationException("WidgetPanel already set for tool grid!");
			}
			viewPropertyGrid.TheGrid.WidgetBank = bank;
		}
	}

	private void timerProgressBar_Tick(object sender, EventArgs e)
	{
		timerProgressBar.Stop();
		if (ProgressBarStarted)
		{
			ControlRender.UpdateProgressBar(startBar: true, toolStripStatusLabelProgressBar.Text);
		}
	}

	protected override void Dispose(bool disposing)
	{
		FormAppBase.TheForm = null;
		WidgetMgr.Instance.FormDisposing = true;
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
		WidgetMgr.Instance.SendFinalQuitConfirm();
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
		WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
		WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
		WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient = new WeifenLuo.WinFormsUI.Docking.TabGradient();
		WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
		WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
		WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
		WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
		WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
		WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
		WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
		WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
		WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
		WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
		WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.toolStripStatusLabelHelpText = new System.Windows.Forms.ToolStripStatusLabel();
		this.toolStripStatusLabelProgressBar = new System.Windows.Forms.ToolStripStatusLabel();
		this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.timerProgressBar = new System.Windows.Forms.Timer(this.components);
		this.statusStrip1.SuspendLayout();
		base.SuspendLayout();
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.toolStripStatusLabelHelpText, this.toolStripStatusLabelProgressBar, this.toolStripProgressBar1 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 570);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(870, 25);
		this.statusStrip1.TabIndex = 0;
		this.statusStrip1.Text = "statusStrip1";
		this.toolStripStatusLabelHelpText.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
		this.toolStripStatusLabelHelpText.Image = palDotNet.Properties.Resources.help;
		this.toolStripStatusLabelHelpText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.toolStripStatusLabelHelpText.Name = "toolStripStatusLabelHelpText";
		this.toolStripStatusLabelHelpText.Size = new System.Drawing.Size(855, 20);
		this.toolStripStatusLabelHelpText.Spring = true;
		this.toolStripStatusLabelHelpText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.toolStripStatusLabelProgressBar.Name = "toolStripStatusLabelProgressBar";
		this.toolStripStatusLabelProgressBar.Size = new System.Drawing.Size(118, 20);
		this.toolStripStatusLabelProgressBar.Text = "toolStripStatusLabel1";
		this.toolStripStatusLabelProgressBar.Visible = false;
		this.toolStripProgressBar1.Name = "toolStripProgressBar1";
		this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 19);
		this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
		this.toolStripProgressBar1.Value = 100;
		this.toolStripProgressBar1.Visible = false;
		this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
		this.menuStrip1.Location = new System.Drawing.Point(0, 0);
		this.menuStrip1.MinimumSize = new System.Drawing.Size(0, 26);
		this.menuStrip1.Name = "menuStrip1";
		this.menuStrip1.Size = new System.Drawing.Size(870, 26);
		this.menuStrip1.TabIndex = 1;
		this.menuStrip1.Text = "menuStrip1";
		this.timer1.Interval = 16;
		this.timer1.Tick += new System.EventHandler(Update);
		this.dockPanel1.ActiveAutoHideContent = null;
		this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
		this.dockPanel1.DockBottomFixedHeight = -36;
		this.dockPanel1.DockBottomPortion = 0.3663003742694855;
		this.dockPanel1.DockLeftFixedWidth = 54;
		this.dockPanel1.DockLeftPortion = 0.2298850566148758;
		this.dockPanel1.DockRightFixedWidth = 123;
		this.dockPanel1.DockRightPortion = 0.2298850566148758;
		this.dockPanel1.DockTopFixedHeight = 41;
		this.dockPanel1.DockTopPortion = 0.3663003742694855;
		this.dockPanel1.Location = new System.Drawing.Point(0, 26);
		this.dockPanel1.Name = "dockPanel1";
		this.dockPanel1.Size = new System.Drawing.Size(870, 544);
		dockPanelGradient.EndColor = System.Drawing.SystemColors.ControlLight;
		dockPanelGradient.StartColor = System.Drawing.SystemColors.ControlLight;
		autoHideStripSkin.DockStripGradient = dockPanelGradient;
		tabGradient.EndColor = System.Drawing.SystemColors.Control;
		tabGradient.StartColor = System.Drawing.SystemColors.Control;
		tabGradient.TextColor = System.Drawing.SystemColors.ControlDarkDark;
		autoHideStripSkin.TabGradient = tabGradient;
		dockPanelSkin.AutoHideStripSkin = autoHideStripSkin;
		tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
		tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
		tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
		dockPaneStripGradient.ActiveTabGradient = tabGradient2;
		dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
		dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
		dockPaneStripGradient.DockStripGradient = dockPanelGradient2;
		tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
		tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
		tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
		dockPaneStripGradient.InactiveTabGradient = tabGradient3;
		dockPaneStripSkin.DocumentGradient = dockPaneStripGradient;
		tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
		tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
		tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
		tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
		dockPaneStripToolWindowGradient.ActiveCaptionGradient = tabGradient4;
		tabGradient5.EndColor = System.Drawing.SystemColors.Control;
		tabGradient5.StartColor = System.Drawing.SystemColors.Control;
		tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
		dockPaneStripToolWindowGradient.ActiveTabGradient = tabGradient5;
		dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
		dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
		dockPaneStripToolWindowGradient.DockStripGradient = dockPanelGradient3;
		tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
		tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
		tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
		tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
		dockPaneStripToolWindowGradient.InactiveCaptionGradient = tabGradient6;
		tabGradient7.EndColor = System.Drawing.Color.Transparent;
		tabGradient7.StartColor = System.Drawing.Color.Transparent;
		tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
		dockPaneStripToolWindowGradient.InactiveTabGradient = tabGradient7;
		dockPaneStripSkin.ToolWindowGradient = dockPaneStripToolWindowGradient;
		dockPanelSkin.DockPaneStripSkin = dockPaneStripSkin;
		this.dockPanel1.Skin = dockPanelSkin;
		this.dockPanel1.TabIndex = 2;
		this.toolTip1.Active = false;
		this.timerProgressBar.Interval = 3000;
		this.timerProgressBar.Tick += new System.EventHandler(timerProgressBar_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(870, 595);
		base.Controls.Add(this.dockPanel1);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.menuStrip1);
		base.IsMdiContainer = true;
		base.KeyPreview = true;
		base.MainMenuStrip = this.menuStrip1;
		base.Name = "FormApp";
		this.Text = "FormApp";
		base.Load += new System.EventHandler(FormApp_Load);
		base.ResizeBegin += new System.EventHandler(FormApp_ResizeBegin);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormApp_FormClosed);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormApp_FormClosing);
		base.Resize += new System.EventHandler(FormApp_Resize);
		base.ResizeEnd += new System.EventHandler(FormApp_ResizeEnd);
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
