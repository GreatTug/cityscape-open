using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VisualHint.SmartPropertyGrid;
using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public class WidgetBase<_ValueType>
{
	public bool TheUpdateParent;

	protected WidgetBase TheWidget;

	protected List<PropertyEnumerator> TheSubPropEnums;

	protected _ValueType TheValue;

	public bool UpdateParent
	{
		set
		{
			TheUpdateParent = value;
			if (TheUpdateParent)
			{
				PropEnum.Parent.Property.UpdateValueFromChildren(PropEnum.Parent);
			}
		}
	}

	public PropertyEnumerator PropEnum { get; protected set; }

	public List<PropertyEnumerator> SubPropEnums
	{
		get
		{
			if (TheSubPropEnums == null)
			{
				TheSubPropEnums = new List<PropertyEnumerator>();
			}
			return TheSubPropEnums;
		}
	}

	public virtual string BindName => "WidgetValue";

	public string Name => TheWidget.Name;

	public object WidgetValueMultiple
	{
		get
		{
			if (!TheWidget.ValueVisible)
			{
				return null;
			}
			return WidgetValue;
		}
		set
		{
			TheWidget.ValueVisible = true;
		}
	}

	public virtual _ValueType WidgetValueGrid
	{
		get
		{
			return WidgetValue;
		}
		set
		{
			TheValue = value;
			WidgetValueMultiple = true;
			PropEnum.Property.InvalidateProperty(children: true);
			OnWidgetValueChanged(this, EventArgs.Empty);
			UpdateInPlaceControl();
			PropEnum.Property.ParentGrid.Invalidate(invalidateChildren: true);
			EventValueVisible(this, EventArgs.Empty);
		}
	}

	public virtual _ValueType WidgetValue
	{
		get
		{
			return TheValue;
		}
		set
		{
			if (ValuesDifferent(value))
			{
				TheValue = value;
				_ = PropEnum.Property.ParentGrid;
				PropertyValidatorBase validator = PropEnum.Property.Value.Validator;
				PropEnum.Property.Value.Validator = null;
				PropEnum.Property.Value.SetValue(TheValue);
				PropEnum.Property.Value.Validator = validator;
				OnWidgetValueChanged(this, EventArgs.Empty);
				UpdateInPlaceControl();
			}
		}
	}

	protected virtual string VisualBindName => "WidgetValue";

	public event EventHandler WidgetValueChanged;

	public WidgetBase(WidgetBase widget, PropertyGrid grid, PropertyEnumerator gridEnum, Attribute[] attrs)
	{
		WidgetBase<_ValueType> widgetBase = this;
		SetValue(widget);
		if (TheWidget == null)
		{
			throw new ApplicationException("Widget can't be null");
		}
		RemotePtr remotePtr = WidgetMgr.Instance.MapLocalToRemote(TheWidget);
		PropEnum = AppendProperty(grid, gridEnum, remotePtr.Id, widget.Name, "WidgetValueGrid", "", attrs);
		PropEnum.Property.Tag = TheWidget;
		BindHelper.Bind(this, VisualBindName, "WidgetValueChanged", TheWidget, BindName, "WidgetValueChanged");
		SupportMultipleValues(grid, PropEnum);
		widget.EventValueVisible += EventValueVisible;
		widget.EventShowContextMenu += delegate(object sender, ContextMenuArgs contextMenuArgs)
		{
			widgetBase.widget_EventShowContextMenu(sender, contextMenuArgs, grid);
		};
	}

	private void widget_EventShowContextMenu(object sender, ContextMenuArgs e, PropertyGrid grid)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = TheWidget.MapMenuToVisual(e.Menu) as ToolStripMenuItem;
		int count = toolStripMenuItem.DropDownItems.Count;
		if (count > 0)
		{
			ToolStripItem[] array = new ToolStripItem[count];
			toolStripMenuItem.DropDownItems.CopyTo(array, 0);
			contextMenuStrip.Items.AddRange(array);
		}
		contextMenuStrip.Show(grid, grid.PointToClient(Control.MousePosition));
	}

	protected virtual void OnWidgetValueChanged(object sender, EventArgs e)
	{
		if (WidgetValueChanged != null)
		{
			WidgetValueChanged(this, e);
		}
	}

	public virtual bool ValuesDifferent(_ValueType newValue)
	{
		throw new NotImplementedException("Function needs to be implemented in derived class");
	}

	public virtual void SetValue(WidgetBase widget)
	{
		throw new NotImplementedException("Function needs to be implemented in derived class");
	}

	public virtual PropertyEnumerator AppendProperty(PropertyGrid grid, PropertyEnumerator gridEnum, int id, string name, string memberName, string comment, Attribute[] attrs)
	{
		return grid.AppendProperty(gridEnum, id, name, this, "WidgetValueGrid", "", attrs);
	}

	public virtual void SupportMultipleValues(PropertyGrid grid, PropertyEnumerator propEnum)
	{
		grid.AddTargetInstance(propEnum, this, "WidgetValueMultiple");
	}

	public void EventValueVisible(object sender, EventArgs e)
	{
		PropEnum.Property.InvalidateProperty(children: true);
		if (PropEnum.Parent.Property.Value != null && TheUpdateParent)
		{
			PropEnum.Parent.Property.UpdateValueFromChildren(PropEnum.Parent);
		}
	}

	private void UpdateInPlaceControl()
	{
		if (TheSubPropEnums != null && TheSubPropEnums.Count > 0)
		{
			foreach (PropertyEnumerator theSubPropEnum in TheSubPropEnums)
			{
				IInPlaceControl inPlaceControl = theSubPropEnum.Property.ParentGrid.InPlaceControl as PropInPlaceUpDown;
				if (inPlaceControl != null && inPlaceControl.OwnerPropertyEnumerator == theSubPropEnum)
				{
					inPlaceControl.UpdateFromDisplay();
				}
			}
			return;
		}
		if (PropEnum.Property.ParentGrid.InPlaceControl != null && PropEnum.Property.ParentGrid.InPlaceControl is IInPlaceControl inPlaceControl2)
		{
			inPlaceControl2.UpdateFromDisplay();
		}
	}
}
