using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class PalDockContent : DockContent, ISupportsToolStrip, IPersistComponentSettings
{
	private WidgetPanel TheWidgetPanel;

	private DockState m_PrevDockState;

	private ContainerControl TheTopToolStripContainer;

	private ContainerControl TheBottomToolStripContainer;

	private ContainerControl TheRightToolStripContainer;

	private ContainerControl TheLeftToolStripContainer;

	private static Dictionary<Guid, PalDockContent> TheDockContents = new Dictionary<Guid, PalDockContent>();

	private static Dictionary<WidgetPanel, PalDockContent> TheViews = new Dictionary<WidgetPanel, PalDockContent>();

	[ReadOnly(true)]
	[Browsable(false)]
	public virtual Guid Guid
	{
		get
		{
			throw new NotImplementedException("must override this in a derived class");
		}
	}

	[Browsable(false)]
	public DockState PrevDockState
	{
		get
		{
			return m_PrevDockState;
		}
		set
		{
			m_PrevDockState = value;
		}
	}

	public ContainerControl TopToolStripContainer
	{
		get
		{
			return TheTopToolStripContainer;
		}
		set
		{
			TheTopToolStripContainer = value;
		}
	}

	public ContainerControl BottomToolStripContainer
	{
		get
		{
			return TheBottomToolStripContainer;
		}
		set
		{
			TheBottomToolStripContainer = value;
		}
	}

	public ContainerControl LeftToolStripContainer
	{
		get
		{
			return TheLeftToolStripContainer;
		}
		set
		{
			TheLeftToolStripContainer = value;
		}
	}

	public ContainerControl RightToolStripContainer
	{
		get
		{
			return TheRightToolStripContainer;
		}
		set
		{
			TheRightToolStripContainer = value;
		}
	}

	public static Dictionary<Guid, PalDockContent> DockContents => TheDockContents;

	public bool SaveSettings
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public virtual string SettingsKey
	{
		get
		{
			return Guid.ToString();
		}
		set
		{
		}
	}

	protected override string GetPersistString()
	{
		return "Guid=" + Guid.ToString() + ";PrevDockState=" + PrevDockState;
	}

	public static PalDockContent Find(Guid guid)
	{
		PalDockContent value = null;
		TheDockContents.TryGetValue(guid, out value);
		return value;
	}

	public virtual void RemoveFromDockContents(WidgetPanel panel)
	{
		TheDockContents.Remove(Guid);
		ToolStrip.UnregisterToolStripSupporter(panel);
	}

	internal void Init(DockPanel dockPanel, WidgetPanel widgetPanel, ISupportsToolStrip toolStripSupporter)
	{
		TheWidgetPanel = widgetPanel;
		TheDockContents[widgetPanel.Guid] = this;
		ToolStrip.RegisterToolStripSupporter(widgetPanel, toolStripSupporter);
		base.TabText = widgetPanel.Name;
		widgetPanel.EventHide += HandleEvent;
		widgetPanel.EventSetIcon += widgetPanel_EventSetIcon;
		bool flag = (base.HideOnClose = widgetPanel.AllowClose);
		bool closeButtonVisible = (base.CloseButton = flag);
		base.CloseButtonVisible = closeButtonVisible;
		if (dockPanel == null || WidgetPanel.IsSerializingLayout)
		{
			return;
		}
		DockAlignment alignment = DockAlignment.Top;
		switch (widgetPanel.DockingType)
		{
		case WidgetPanel.EnumDockingType.eDockBottom:
			alignment = DockAlignment.Bottom;
			break;
		case WidgetPanel.EnumDockingType.eDockTop:
			alignment = DockAlignment.Top;
			break;
		case WidgetPanel.EnumDockingType.eDockLeft:
			alignment = DockAlignment.Left;
			break;
		case WidgetPanel.EnumDockingType.eDockRight:
			alignment = DockAlignment.Right;
			break;
		}
		PalDockContent palDockContent = Find(widgetPanel.RelativeDockingGuid);
		if (palDockContent != null && ((palDockContent is DocumentView && this is DocumentView) || (!(palDockContent is DocumentView) && !(this is DocumentView))))
		{
			if (widgetPanel.DockingType == WidgetPanel.EnumDockingType.eDockTabbed)
			{
				Show(palDockContent.Pane, palDockContent);
			}
			else
			{
				Show(palDockContent.Pane, alignment, widgetPanel.DockingProportion);
			}
			return;
		}
		if (widgetPanel.DockingType == WidgetPanel.EnumDockingType.eDockTabbed)
		{
			Show(dockPanel);
			return;
		}
		if (this is DocumentView)
		{
			Show(DocumentRenderView.MainViewDocument.Pane, alignment, widgetPanel.DockingProportion);
			return;
		}
		DockState dockState = DockState.DockBottom;
		switch (widgetPanel.DockingType)
		{
		case WidgetPanel.EnumDockingType.eDockBottom:
			dockState = DockState.DockBottom;
			break;
		case WidgetPanel.EnumDockingType.eDockTop:
			dockState = DockState.DockTop;
			break;
		case WidgetPanel.EnumDockingType.eDockLeft:
			dockState = DockState.DockLeft;
			break;
		case WidgetPanel.EnumDockingType.eDockRight:
			dockState = DockState.DockRight;
			break;
		}
		Show(dockPanel, dockState);
	}

	private void widgetPanel_EventSetIcon(object sender, EventSetIconArgs e)
	{
		IconLoaderHelper.Load(e.Path, new Size(16, 16), out var icon);
		base.Icon = icon;
	}

	private void HandleEvent(object sender, EventHideArgs e)
	{
		if (e.Hide)
		{
			if (e.Type == EventHideArgs.EnumHideType.eAutoHide)
			{
				switch ((base.DockState == DockState.Hidden) ? PrevDockState : base.DockState)
				{
				case DockState.DockBottom:
					base.DockState = DockState.DockBottomAutoHide;
					break;
				case DockState.DockTop:
					base.DockState = DockState.DockTopAutoHide;
					break;
				case DockState.DockLeft:
					base.DockState = DockState.DockLeftAutoHide;
					break;
				case DockState.DockRight:
					base.DockState = DockState.DockRightAutoHide;
					break;
				}
			}
			else if (base.DockState != DockState.Hidden)
			{
				PrevDockState = base.DockState;
				base.DockState = DockState.Hidden;
			}
			return;
		}
		switch (base.DockState)
		{
		case DockState.DockBottomAutoHide:
			base.DockState = DockState.DockBottom;
			break;
		case DockState.DockTopAutoHide:
			base.DockState = DockState.DockTop;
			break;
		case DockState.DockLeftAutoHide:
			base.DockState = DockState.DockLeft;
			break;
		case DockState.DockRightAutoHide:
			base.DockState = DockState.DockRight;
			break;
		case DockState.Hidden:
			switch (e.DockingType)
			{
			case EventHideArgs.EnumDockingType.ePrevState:
				base.DockState = (PrevDockState != DockState.Hidden && PrevDockState != DockState.Unknown) ? PrevDockState : ProcessDockState(DockState.DockBottom);
				break;
			case EventHideArgs.EnumDockingType.eBottom:
				base.DockState = ProcessDockState(DockState.DockBottom);
				break;
			case EventHideArgs.EnumDockingType.eLeft:
				base.DockState = ProcessDockState(DockState.DockLeft);
				break;
			case EventHideArgs.EnumDockingType.eRight:
				base.DockState = ProcessDockState(DockState.DockRight);
				break;
			case EventHideArgs.EnumDockingType.eTop:
				base.DockState = ProcessDockState(DockState.DockTop);
				break;
			}
			PrevDockState = DockState.Unknown;
			Show();
			break;
		}
	}

	private DockState ProcessDockState(DockState newState)
	{
		if ((newState == DockState.DockBottom && (base.DockAreas & DockAreas.DockBottom) == DockAreas.DockBottom) ||
			(newState == DockState.DockTop && (base.DockAreas & DockAreas.DockTop) == DockAreas.DockTop) ||
			(newState == DockState.DockLeft && (base.DockAreas & DockAreas.DockLeft) == DockAreas.DockLeft) ||
			(newState == DockState.DockRight && (base.DockAreas & DockAreas.DockRight) == DockAreas.DockRight))
		{
			return newState;
		}
		if ((base.DockAreas & DockAreas.Document) == DockAreas.Document)
		{
			return DockState.Document;
		}
		if ((base.DockAreas & DockAreas.Float) == DockAreas.Float)
		{
			return DockState.Float;
		}
		if ((base.DockAreas & DockAreas.DockBottom) != 0)
		{
			return DockState.DockBottom;
		}
		if ((base.DockAreas & DockAreas.DockRight) != 0)
		{
			return DockState.DockRight;
		}
		return DockState.DockBottom;
	}

	internal static void RegisterInternal(DockPanel dockPanel, Type widgetType, Type controlType)
	{
		WidgetBase.EventCreate += delegate(object s, EventArgs e)
		{
			if (s is WidgetPanel widgetPanel && widgetPanel.GetType() == widgetType)
			{
				PalDockContent palDockContent = Activator.CreateInstance(controlType, null) as PalDockContent;
				palDockContent.Init(dockPanel, widgetPanel, null);
				TheViews.Add(widgetPanel, palDockContent);
			}
		};
		WidgetBase.EventDestroy += delegate(object s, EventArgs e)
		{
			if (s is WidgetPanel widgetPanel && TheViews.ContainsKey(widgetPanel) && widgetPanel.GetType() == widgetType)
			{
				ToolStrip.UnregisterToolStripSupporter(widgetPanel);
				PalDockContent palDockContent = TheViews[widgetPanel];
				if (palDockContent != null && palDockContent.GetType() == controlType)
				{
					palDockContent.Close();
					TheViews.Remove(widgetPanel);
					palDockContent.RemoveFromDockContents(widgetPanel);
				}
			}
		};
	}

	protected override void OnDockStateChanged(EventArgs e)
	{
		if (TheWidgetPanel != null)
		{
			if (base.DockState == DockState.Hidden)
			{
				TheWidgetPanel.SendHiddenState(hidden: true);
			}
			else
			{
				TheWidgetPanel.SendHiddenState(hidden: false);
			}
		}
		base.OnDockStateChanged(e);
	}

	public virtual void SaveComponentSettings()
	{
	}

	public virtual void ResetComponentSettings()
	{
	}

	public virtual void LoadComponentSettings()
	{
	}

	void ISupportsToolStrip.SuspendLayout()
	{
		SuspendLayout();
	}

	void ISupportsToolStrip.ResumeLayout()
	{
		ResumeLayout();
	}
}
