using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal abstract class ColorSpace : UserControl
{
	private ArrayList m_colorSpaceComponents;

	private ColorSpaceComponent m_selectedComponent;

	private bool m_displayRadioButtons;

	private bool m_readOnly;

	protected ArrayList ColorSpaceComponents => m_colorSpaceComponents;

	internal ColorSpaceComponent SelectedComponent => m_selectedComponent;

	internal bool DisplayRadioButtons
	{
		get
		{
			return m_displayRadioButtons;
		}
		set
		{
			m_displayRadioButtons = value;
			foreach (ColorSpaceComponent colorSpaceComponent in m_colorSpaceComponents)
			{
				colorSpaceComponent.RadioButtonVisible = m_displayRadioButtons;
			}
		}
	}

	internal bool ReadOnly
	{
		get
		{
			return m_readOnly;
		}
		set
		{
			m_readOnly = value;
			foreach (ColorSpaceComponent colorSpaceComponent in m_colorSpaceComponents)
			{
				colorSpaceComponent.ReadOnly = m_readOnly;
			}
		}
	}

	internal abstract IColorSpaceStructure Structure { get; set; }

	internal event ColorSpaceEventHandler ComponentValueChanged;

	internal event ColorSpaceEventHandler SelectedComponentChanged;

	protected ColorSpace()
	{
		m_colorSpaceComponents = new ArrayList();
		m_displayRadioButtons = true;
	}

	protected void ComponentSelected(ColorSpaceComponent sender, EventArgs e)
	{
		ChangeCurrentlySelectedComponent(sender);
	}

	protected void ChangeCurrentlySelectedComponent(ColorSpaceComponent component)
	{
		ResetComponents();
		component.Selected = true;
		m_selectedComponent = component;
		OnSelectedComponentChanged(EventArgs.Empty);
	}

	protected void ComponentTextKeyUp(ColorSpaceComponent sender, EventArgs e)
	{
		OnComponentValueChanged(e);
	}

	internal void ResetComponents()
	{
		foreach (ColorSpaceComponent colorSpaceComponent in m_colorSpaceComponents)
		{
			colorSpaceComponent.Selected = false;
		}
	}

	protected void OnSelectedComponentChanged(EventArgs e)
	{
		if (SelectedComponentChanged != null)
		{
			SelectedComponentChanged(this, e);
		}
	}

	protected void OnComponentValueChanged(EventArgs e)
	{
		if (ComponentValueChanged != null)
		{
			ComponentValueChanged(this, e);
		}
	}

	internal abstract void SetDefaultSelection();

	internal abstract Color GetColor();

	protected abstract void UpdateValues(IColorSpaceStructure csStructure);
}
