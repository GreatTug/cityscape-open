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

	[Browsable(false)]
	[ReadOnly(true)]
	public override Guid Guid => ControlRender.RenderWindow.Guid;

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
		WidgetBase.EventCreate += delegate(object s, EventArgs e)
		{
			if (s is RenderWindow renderWindow)
			{
				if (renderWindow.IsMainRenderWindow)
				{
					TheMainViewDocument.ControlRender.RenderWindow = renderWindow;
					TheMainViewDocument.ControlRender.SendResize();
					TheMainViewDocument.Init(null, renderWindow, TheMainViewDocument);
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
