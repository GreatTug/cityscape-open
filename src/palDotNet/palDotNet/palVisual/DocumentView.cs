using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class DocumentView : PalDockContent
{
	private IContainer components;

	protected static Dictionary<WidgetPanel, DocumentView> TheWindows = new Dictionary<WidgetPanel, DocumentView>();

	[Browsable(false)]
	[ReadOnly(true)]
	public override Guid Guid
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 273);
		base.CloseButton = false;
		base.CloseButtonVisible = false;
		base.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
		base.Name = "DocumentView";
		base.TabText = "";
		base.ToolTipText = "";
		base.ResumeLayout(false);
	}

	public DocumentView()
	{
		InitializeComponent();
		base.ShowHint = DockState.Document;
	}

	public static void Register()
	{
		WidgetBase.EventDestroy += delegate(object s, EventArgs e)
		{
			if (s is WidgetPanel widgetPanel && TheWindows.ContainsKey(widgetPanel))
			{
				DocumentView documentView = TheWindows[widgetPanel];
				if (documentView != null)
				{
					documentView.Close();
					TheWindows.Remove(widgetPanel);
					documentView.RemoveFromDockContents(widgetPanel);
				}
			}
		};
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		FormClosingEventArgs e2 = e as FormClosingEventArgs;
		base.OnClosing(e);
		if (e2 != null && e2.CloseReason == CloseReason.UserClosing && (!base.CloseButton || !base.CloseButtonVisible))
		{
			e.Cancel = true;
		}
	}
}
