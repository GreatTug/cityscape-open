using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

internal class ToolStrip : System.Windows.Forms.ToolStrip
{
	private palDotNet.palWidgets.ToolStrip ToolStripData;

	private static Dictionary<WidgetPanel, ISupportsToolStrip> WidgetPanelToToolStripParent = new Dictionary<WidgetPanel, ISupportsToolStrip>();

	public static void RegisterToolStripSupporter(WidgetPanel widgetPanel, ISupportsToolStrip supporter)
	{
		WidgetPanelToToolStripParent.Add(widgetPanel, supporter);
	}

	public static bool UnregisterToolStripSupporter(WidgetPanel widgetPanel)
	{
		return WidgetPanelToToolStripParent.Remove(widgetPanel);
	}

	public static void Register()
	{
		palDotNet.palWidgets.ToolStrip.ToolStripCreate += ToolStrip_Create;
	}

	public void AddWidget(WidgetBase widget)
	{
		if (!(widget.Parent is palDotNet.palWidgets.ToolStrip toolStrip) || toolStrip != ToolStripData)
		{
			return;
		}
		ToolStripItem item = null;
		if (widget is palDotNet.palWidgets.Button)
		{
			item = CreateButton(widget as palDotNet.palWidgets.Button);
		}
		else if (widget is palDotNet.palWidgets.ComboBox)
		{
			item = CreateComboBox(widget as palDotNet.palWidgets.ComboBox);
		}
		else
		{
			if (!(widget is Toggle))
			{
				throw new ApplicationException("Control '" + widget.Name + "' with type '" + widget.GetType().Name + "' not supported!");
			}
			item = CreateToggle(widget as Toggle);
		}
		widget.EventEnabled += delegate(object sender, EventArgs args)
		{
			if (sender is WidgetBase)
			{
				item.Enabled = widget.Enabled;
			}
		};
		widget.EventComment += delegate(object sender, EventArgs args)
		{
			if (sender is WidgetBase widgetBase)
			{
				item.ToolTipText = widgetBase.Comment;
				if (!string.IsNullOrEmpty(item.ToolTipText))
				{
					item.AutoToolTip = false;
				}
			}
		};
		if (item != null)
		{
			item.Tag = widget;
			Items.Add(item);
		}
	}

	public void RemoveWidget(WidgetBase widget)
	{
		if (base.Parent == null)
		{
			return;
		}
		ToolStripItem toolStripItem = null;
		foreach (ToolStripItem item in Items)
		{
			WidgetBase widgetBase = item.Tag as WidgetBase;
			if (widgetBase == widget)
			{
				toolStripItem = item;
				break;
			}
		}
		if (toolStripItem != null)
		{
			Items.Remove(toolStripItem);
		}
	}

	private void LoadWidgetImage(ToolStripItem item, string path, bool iconOnly)
	{
		Size value = new Size((int)ToolStripData.IconSize, (int)ToolStripData.IconSize);
		item.Image = IconLoaderHelper.Load(path, value);
		if (item.Image != null)
		{
			if (iconOnly)
			{
				item.DisplayStyle = ToolStripItemDisplayStyle.Image;
			}
			else
			{
				item.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
			}
		}
		else
		{
			item.DisplayStyle = ToolStripItemDisplayStyle.Text;
		}
	}

	private ToolStripItem CreateButton(palDotNet.palWidgets.Button widget)
	{
		ToolStripButton toolStripButton = new ToolStripButton();
		LoadWidgetImage(toolStripButton, widget.IconPath, widget.IconOnly);
		toolStripButton.ImageTransparentColor = Color.Magenta;
		toolStripButton.Name = widget.Name;
		toolStripButton.Text = widget.Name;
		toolStripButton.ImageAlign = (ContentAlignment)widget.IconAlignment;
		toolStripButton.Click += delegate
		{
			widget.WidgetValue = true;
		};
		return toolStripButton;
	}

	private ToolStripItem CreateComboBox(palDotNet.palWidgets.ComboBox widget)
	{
		ToolStripComboBox toolStripComboBox = new ToolStripComboBox();
		toolStripComboBox.Name = widget.Name;
		toolStripComboBox.Text = widget.Name;
		toolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		string[] items = widget.Items;
		foreach (string item in items)
		{
			toolStripComboBox.Items.Add(item);
		}
		toolStripComboBox.SelectedItem = widget.StringValue;
		BindHelper.Bind(toolStripComboBox, "Text", "SelectedIndexChanged", widget, widget.PropertyValueName, "WidgetValueChanged");
		return toolStripComboBox;
	}

	private ToolStripItem CreateToggle(Toggle widget)
	{
		ToolStripButton button = new ToolStripButton();
		LoadWidgetImage(button, widget.IconPath, widget.IconOnly);
		button.ImageTransparentColor = Color.Magenta;
		button.Name = widget.Name;
		button.CheckOnClick = true;
		button.Text = widget.Name;
		button.Checked = widget.WidgetValue;
		button.Click += delegate
		{
			widget.WidgetValue = button.Checked;
		};
		BindHelper.Bind(button, "Checked", "CheckedChanged", widget, widget.PropertyValueName, "WidgetValueChanged");
		return button;
	}

	public void AddMenuItem(WidgetBase data, object visual)
	{
		ToolStripItem toolStripItem = visual as ToolStripItem;
		toolStripItem.Tag = data;
		Items.Add(toolStripItem);
	}

	public static void ToolStrip_Create(object sender, EventArgs args)
	{
		palDotNet.palWidgets.ToolStrip toolStrip = sender as palDotNet.palWidgets.ToolStrip;
		WidgetPanel key = toolStrip.Parent as WidgetPanel;
		ToolStrip newToolStrip = new ToolStrip();
		toolStrip.AddMenuItemDelegate = newToolStrip.AddMenuItem;
		ISupportsToolStrip value = null;
		WidgetPanelToToolStripParent.TryGetValue(key, out value);
		newToolStrip.ToolStripData = toolStrip;
		value.SuspendLayout();
		ControlCollection controls = value.Controls;
		newToolStrip.ImageScalingSize = new Size((int)toolStrip.IconSize, (int)toolStrip.IconSize);
		newToolStrip.AllowItemReorder = false;
		newToolStrip.Dock = DockStyle.None;
		newToolStrip.Name = toolStrip.Name;
		newToolStrip.TabIndex = 0;
		newToolStrip.Stretch = true;
		newToolStrip.ShowItemToolTips = true;
		newToolStrip.GripStyle = ToolStripGripStyle.Hidden;
		newToolStrip.AutoSize = true;
		switch (toolStrip.LayoutStyle)
		{
		case palDotNet.palWidgets.ToolStrip.EnumLayoutStyle.eFlow:
			newToolStrip.LayoutStyle = ToolStripLayoutStyle.Flow;
			break;
		case palDotNet.palWidgets.ToolStrip.EnumLayoutStyle.eHorizontalStackWithOverflow:
			newToolStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			break;
		case palDotNet.palWidgets.ToolStrip.EnumLayoutStyle.eStackWithOverflow:
			newToolStrip.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
			break;
		case palDotNet.palWidgets.ToolStrip.EnumLayoutStyle.eTable:
			newToolStrip.LayoutStyle = ToolStripLayoutStyle.Table;
			break;
		case palDotNet.palWidgets.ToolStrip.EnumLayoutStyle.eVerticalStackWithOverflow:
			newToolStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
			break;
		}
		ToolStripPanel toolStripPanel = null;
		DockStyle dock = DockStyle.None;
		switch (toolStrip.Position)
		{
		case palDotNet.palWidgets.ToolStrip.EnumPosition.eTop:
			toolStripPanel = value.TopToolStripContainer as ToolStripPanel;
			dock = DockStyle.Top;
			break;
		case palDotNet.palWidgets.ToolStrip.EnumPosition.eBottom:
			toolStripPanel = value.BottomToolStripContainer as ToolStripPanel;
			dock = DockStyle.Bottom;
			break;
		case palDotNet.palWidgets.ToolStrip.EnumPosition.eRight:
			toolStripPanel = value.RightToolStripContainer as ToolStripPanel;
			dock = DockStyle.Right;
			newToolStrip.TextDirection = ToolStripTextDirection.Vertical90;
			break;
		case palDotNet.palWidgets.ToolStrip.EnumPosition.eLeft:
			toolStripPanel = value.LeftToolStripContainer as ToolStripPanel;
			dock = DockStyle.Left;
			newToolStrip.TextDirection = ToolStripTextDirection.Vertical90;
			break;
		}
		if (toolStripPanel == null)
		{
			toolStripPanel = new ToolStripPanel();
			toolStripPanel.Dock = dock;
			controls.Add(toolStripPanel);
			switch (toolStrip.Position)
			{
			case palDotNet.palWidgets.ToolStrip.EnumPosition.eTop:
				value.TopToolStripContainer = toolStripPanel;
				break;
			case palDotNet.palWidgets.ToolStrip.EnumPosition.eBottom:
				value.BottomToolStripContainer = toolStripPanel;
				break;
			case palDotNet.palWidgets.ToolStrip.EnumPosition.eRight:
				value.RightToolStripContainer = toolStripPanel;
				break;
			case palDotNet.palWidgets.ToolStrip.EnumPosition.eLeft:
				value.LeftToolStripContainer = toolStripPanel;
				break;
			}
		}
		toolStripPanel.Join(newToolStrip, toolStripPanel.Rows.Length);
		value.ResumeLayout();
		WidgetBase.EventCreate += delegate(object s, EventArgs e)
		{
			WidgetBase widget = s as WidgetBase;
			newToolStrip.AddWidget(widget);
		};
		WidgetBase.EventDestroy += delegate(object s, EventArgs e)
		{
			WidgetBase widget = s as WidgetBase;
			newToolStrip.RemoveWidget(widget);
			palDotNet.palWidgets.ToolStrip toolStrip2 = s as palDotNet.palWidgets.ToolStrip;
			if (newToolStrip.ToolStripData == toolStrip2)
			{
				newToolStrip.Parent.Controls.Remove(newToolStrip);
			}
		};
	}

	internal static void MoveControls(ControlCollection source, Control container, ControlCollection destination)
	{
		Control[] array = new Control[source.Count];
		source.CopyTo(array, 0);
		source.Clear();
		destination.AddRange(array);
		source.Add(container);
	}
}
