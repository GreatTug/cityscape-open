using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class PalTabbedView : PalDockContent
{
	private IContainer components;

	private Type WidgetType;

	private Type ControlType;

	private List<IWidgetPanelControl> WidgetPanelControls = new List<IWidgetPanelControl>();

	private List<Control> controlList = new List<Control>();

	private Dictionary<IWidgetPanelControl, List<Control>> WidgetPanelControlLists = new Dictionary<IWidgetPanelControl, List<Control>>();

	private static Dictionary<WidgetPanel, PalTabbedView> TheViews = new Dictionary<WidgetPanel, PalTabbedView>();

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			PropagateSettings();
			SaveComponentSettings();
			if (components != null)
			{
				components.Dispose();
			}
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 266);
		base.CloseButton = false;
		base.CloseButtonVisible = false;
		base.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom;
		base.Name = "PalTabbedView";
		base.ResumeLayout(false);
	}

	internal PalTabbedView()
	{
		InitializeComponent();
	}

	internal PalTabbedView(Type widgetType, Type controlType)
	{
		InitializeComponent();
		WidgetType = widgetType;
		ControlType = controlType;
	}

	protected override string GetPersistString()
	{
		string text = string.Empty;
		foreach (IWidgetPanelControl widgetPanelControl in WidgetPanelControls)
		{
			text = text + "Guid=" + widgetPanelControl.WidgetPanel.Guid.ToString() + ";";
		}
		return text;
	}

	private void AddWidgetPanelControl(IWidgetPanelControl control)
	{
		SuspendLayout();
		if (WidgetPanelControls.Count <= 0)
		{
			base.Controls.Add(control as Control);
			base.TabText = control.WidgetPanel.Name;
			control.Visible = true;
		}
		WidgetPanelControls.Add(control);
		ResumeLayout();
	}

	private void RemoveWidgetPanelControl(IWidgetPanelControl control)
	{
		SuspendLayout();
		Control value = control as Control;
		base.Controls.Remove(value);
		WidgetPanelControls.Remove(control);
		ResumeLayout();
	}

	internal static void Register(DockPanel dockPanel, Type widgetType, Type controlType)
	{
		WidgetBase.EventCreate += delegate(object s, EventArgs e)
		{
			WidgetPanel panel = s as WidgetPanel;
			RegisterDockContent(widgetType, controlType, dockPanel, panel);
		};
		WidgetBase.EventDestroy += delegate(object s, EventArgs e)
		{
			WidgetPanel panel = s as WidgetPanel;
			UnRegisterDockContent(ref panel, widgetType);
		};
	}

	private static void RegisterDockContent(Type widgetType, Type controlType, DockPanel dockPanel, WidgetPanel panel)
	{
		if (panel == null || panel.GetType() != widgetType || (panel.Parent != null && !(panel.Parent is WidgetPanel)))
		{
			return;
		}
		PalTabbedView palTabbedView = null;
		if (panel.Parent is WidgetPanel widgetPanel)
		{
			PalDockContent palDockContent = PalDockContent.Find(widgetPanel.Guid);
			if (palDockContent != null)
			{
				palTabbedView = palDockContent as PalTabbedView;
			}
		}
		IWidgetPanelControl widgetPanelControl = Activator.CreateInstance(controlType, null) as IWidgetPanelControl;
		widgetPanelControl.Visible = false;
		widgetPanelControl.WidgetPanel = panel;
		if (palTabbedView == null)
		{
			palTabbedView = new PalTabbedView();
			palTabbedView.Init(dockPanel, panel, widgetPanelControl as ISupportsToolStrip);
		}
		else
		{
			ToolStrip.RegisterToolStripSupporter(panel, widgetPanelControl as ISupportsToolStrip);
		}
		Control control = widgetPanelControl as Control;
		control.Dock = DockStyle.Fill;
		panel.EventShowPanel += palTabbedView.WidgetPanel_ShowPanel;
		panel.EventEnabled += palTabbedView.WidgetPanel_Enabled;
		palTabbedView.AddWidgetPanelControl(widgetPanelControl);
		TheViews.Add(panel, palTabbedView);
	}

	private static void UnRegisterDockContent(ref WidgetPanel panel, Type widgetType)
	{
		if (panel != null && TheViews.ContainsKey(panel) && panel.GetType() == widgetType)
		{
			ToolStrip.UnregisterToolStripSupporter(panel);
			PalTabbedView palTabbedView = TheViews[panel];
			if (palTabbedView != null && panel.GetType() == widgetType)
			{
				panel.EventShowPanel -= palTabbedView.WidgetPanel_ShowPanel;
				panel.EventEnabled -= palTabbedView.WidgetPanel_Enabled;
				palTabbedView.Close();
				TheViews.Remove(panel);
				palTabbedView.RemoveFromDockContents(panel);
			}
		}
	}

	private void PropagateSettings()
	{
		IWidgetPanelControl widgetPanelControl = base.Controls[0] as IWidgetPanelControl;
		foreach (IWidgetPanelControl widgetPanelControl2 in WidgetPanelControls)
		{
			if (widgetPanelControl != widgetPanelControl2)
			{
				string settingsKey = widgetPanelControl.WidgetPanel.Guid.ToString();
				string text = widgetPanelControl2.WidgetPanel.Guid.ToString();
				if (widgetPanelControl.CopySettingsTo(settingsKey, text, widgetPanelControl2))
				{
					widgetPanelControl2.LoadComponentSettings(text);
				}
			}
		}
	}

	public override void SaveComponentSettings()
	{
		foreach (IWidgetPanelControl widgetPanelControl in WidgetPanelControls)
		{
			widgetPanelControl.SaveComponentSettings(widgetPanelControl.WidgetPanel.Guid.ToString());
		}
	}

	public override void ResetComponentSettings()
	{
		foreach (IWidgetPanelControl widgetPanelControl in WidgetPanelControls)
		{
			widgetPanelControl.ResetComponentSettings(widgetPanelControl.WidgetPanel.Guid.ToString());
		}
	}

	public override void LoadComponentSettings()
	{
		foreach (IWidgetPanelControl widgetPanelControl in WidgetPanelControls)
		{
			widgetPanelControl.LoadComponentSettings(widgetPanelControl.WidgetPanel.Guid.ToString());
		}
	}

	private void WidgetPanel_ShowPanel(object sender, EventArgs args)
	{
		WidgetPanel widgetPanel = sender as WidgetPanel;
		foreach (IWidgetPanelControl widgetPanelControl2 in WidgetPanelControls)
		{
			if (widgetPanelControl2.WidgetPanel == widgetPanel)
			{
				if (!base.Controls.Contains(widgetPanelControl2 as Control))
				{
					SuspendLayout();
					IWidgetPanelControl widgetPanelControl = base.Controls[0] as IWidgetPanelControl;
					PropagateSettings();
					widgetPanelControl.Visible = false;
					base.Controls.Clear();
					base.Controls.Add(widgetPanelControl2 as Control);
					ResumeLayout();
					widgetPanelControl2.Visible = true;
					base.TabText = widgetPanel.Name;
				}
				break;
			}
		}
	}

	private void WidgetPanel_Enabled(object sender, EventArgs args)
	{
		WidgetBase widgetBase = sender as WidgetBase;
		foreach (Control control in base.Controls)
		{
			control.Enabled = widgetBase.Enabled;
		}
		if (!widgetBase.Enabled)
		{
			string text = (base.TabText = " ");
			Text = text;
		}
		else
		{
			WidgetPanel widgetPanel = sender as WidgetPanel;
			string text3 = (base.TabText = widgetPanel.Name);
			Text = text3;
		}
	}
}
