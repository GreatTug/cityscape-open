using System;
using System.ComponentModel;
using System.Windows.Forms;
using palDotNet.palVisual;
using palDotNet.palWidgets;

namespace palDotNet.palDialog;

public class DialogWidgetHolder : Panel, IDialogValue
{
	private Control TheControl;

	[DialogValueType(EnumDialogValueTypes.eString)]
	[ReadOnly(true)]
	public object DialogValue
	{
		get
		{
			return Text;
		}
		set
		{
			Text = (string)value;
		}
	}

	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	[Browsable(false)]
	[ReadOnly(true)]
	public Control Control
	{
		get
		{
			return TheControl;
		}
		set
		{
			base.Controls.Remove(TheControl);
			TheControl = value;
			base.Controls.Add(TheControl);
			TheControl.Dock = DockStyle.Fill;
		}
	}

	public bool ContainsSelection { get; set; }

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
	}

	internal static void Register(Type widgetType, Type controlType)
	{
		WidgetBase.EventCreate += delegate(object s, EventArgs e)
		{
			WidgetPanel panel = s as WidgetPanel;
			CreateWidget(widgetType, controlType, panel);
		};
		WidgetBase.EventDestroy += delegate(object s, EventArgs e)
		{
			WidgetPanel panel = s as WidgetPanel;
			DestroyWidget(ref panel, widgetType);
		};
	}

	private static void CreateWidget(Type widgetType, Type controlType, WidgetPanel panel)
	{
		if (panel != null && panel.GetType() == widgetType && panel.Parent is WidgetDialogBox widgetDialogBox)
		{
			DialogWidgetHolder dialogWidgetHolder = widgetDialogBox.VisualDialog.FindVariable<DialogWidgetHolder>(panel.Name);
			if (dialogWidgetHolder != null)
			{
				IWidgetPanelControl widgetPanelControl = Activator.CreateInstance(controlType, null) as IWidgetPanelControl;
				widgetPanelControl.WidgetPanel = panel;
				Control control = widgetPanelControl as Control;
				control.Dock = DockStyle.Fill;
				dialogWidgetHolder.Controls.Clear();
				dialogWidgetHolder.Controls.Add(control);
				dialogWidgetHolder.TheControl = control;
				palDotNet.palVisual.ToolStrip.RegisterToolStripSupporter(panel, control as ISupportsToolStrip);
			}
		}
	}

	private static void DestroyWidget(ref WidgetPanel panel, Type widgetType)
	{
		if (panel != null && panel.GetType() == widgetType)
		{
			palDotNet.palVisual.ToolStrip.UnregisterToolStripSupporter(panel);
		}
	}
}
