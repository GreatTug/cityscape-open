using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class DocumentRenderView : DocumentView
{
	private static DocumentRenderView TheMainViewDocument;

	private IContainer components;

	private ControlRender ControlRender;

	[Browsable(false)]
	[ReadOnly(true)]
	public IntPtr RenderWindowHandle => ControlRender.Handle;

	[ReadOnly(true)]
	[Browsable(false)]
	public ControlRender Render => ControlRender;

	[Browsable(false)]
	[ReadOnly(true)]
	public static DocumentRenderView MainViewDocument
	{
		get
		{
			return TheMainViewDocument;
		}
		set
		{
			TheMainViewDocument = value;
		}
	}

	public static readonly Guid DefaultRenderGuid = new Guid("9aac009e-f759-44c1-a6b1-8637a180760d");

	[Browsable(false)]
	[ReadOnly(true)]
	public override Guid Guid => (ControlRender?.RenderWindow != null && ControlRender.RenderWindow.Guid != Guid.Empty)
		? ControlRender.RenderWindow.Guid
		: DefaultRenderGuid;

	public DocumentRenderView()
	{
		InitializeComponent();
		base.ShowHint = DockState.Document;
		Render.SendResize();
	}

	private void ViewDocument_SizeChanged(object sender, EventArgs e)
	{
		if (ControlRender.RenderWindow != null)
		{
			string name = ControlRender.RenderWindow.Name;
			object obj = name;
			name = string.Concat(obj, " (", base.Size.Width, " x ", base.Size.Height, ")");
			base.TabText = name;
		}
	}

	public static void Register(DockPanel dockPanel)
	{
		WidgetMgr.Instance.RenderWindowDelegate = delegate(bool adding, string title, uint id)
		{
			if (adding)
			{
				RenderWindow renderWindow = WidgetMgr.Instance.GetWidgetById(id) as RenderWindow;
				if (renderWindow == null)
				{
					foreach (WidgetBase wb in WidgetMgr.Instance.AllWidgets)
					{
						if (wb is RenderWindow rw)
						{
							renderWindow = rw;
							break;
						}
					}
				}
				if (renderWindow != null && TheMainViewDocument != null)
				{
					TheMainViewDocument.ControlRender.RenderWindow = renderWindow;
					TheMainViewDocument.ControlRender.SendResize();
					TheMainViewDocument.Init(null, renderWindow, TheMainViewDocument);
					if (!string.IsNullOrEmpty(title))
					{
						TheMainViewDocument.TabText = title;
					}
				}
			}
		};

		WidgetBase.EventCreate += delegate(object s, EventArgs e)
		{
			if (s is RenderWindow renderWindow)
			{
				if (renderWindow.IsMainRenderWindow || TheMainViewDocument?.ControlRender?.RenderWindow == null)
				{
					if (TheMainViewDocument != null)
					{
						TheMainViewDocument.ControlRender.RenderWindow = renderWindow;
						TheMainViewDocument.ControlRender.SendResize();
						TheMainViewDocument.Init(null, renderWindow, TheMainViewDocument);
					}
				}
				else
				{
					DocumentRenderView documentRenderView = new DocumentRenderView();
					documentRenderView.ControlRender.RenderWindow = renderWindow;
					documentRenderView.Init(dockPanel, renderWindow, documentRenderView);
					DocumentView.TheWindows.Add(renderWindow, documentRenderView);
					documentRenderView.ControlRender.SendResize();
				}
			}
		};
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		if (disposing)
		{
			ControlRender.SetNotVisible();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.ControlRender = new palDotNet.palVisual.ControlRender();
		base.SuspendLayout();
		this.ControlRender.AllowDrop = true;
		this.ControlRender.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ControlRender.Location = new System.Drawing.Point(0, 0);
		this.ControlRender.Name = "ControlRender";
		this.ControlRender.Size = new System.Drawing.Size(292, 273);
		this.ControlRender.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 273);
		base.Controls.Add(this.ControlRender);
		base.Name = "DocumentRenderView";
		base.SizeChanged += new System.EventHandler(ViewDocument_SizeChanged);
		base.Controls.SetChildIndex(this.ControlRender, 0);
		base.ResumeLayout(false);
	}
}
