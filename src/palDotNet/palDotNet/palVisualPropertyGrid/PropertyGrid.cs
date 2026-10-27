using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using VisualHint.SmartPropertyGrid;
using palDotNet.Properties;
using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public class PropertyGrid : VisualHint.SmartPropertyGrid.PropertyGrid
{
	protected WidgetBase WidgetWithLastFocus;

	private Bank TheWidgetBank;

	private PropertyGridSettings TheConfigSettings;

	private bool PauseForced;

	private int UpdateCount;

	private WidgetBase LastInPlaceCtrlVisibleSlider;

	private Scrub Scrubber = new Scrub();

	private Dictionary<RemotePtr, RemotePtr> Parents;

	private Dictionary<RemotePtr, int> RemoteToParentId;

	private Dictionary<BankGroup, PropertyEnumerator> BankGroupToPropGroup = new Dictionary<BankGroup, PropertyEnumerator>();

	[Browsable(false)]
	[ReadOnly(true)]
	public Bank WidgetBank
	{
		get
		{
			return TheWidgetBank;
		}
		set
		{
			TheWidgetBank = value;
		}
	}

	public PropertyGrid()
	{
		Parents = new Dictionary<RemotePtr, RemotePtr>(new RemotePtr.EqualityComparer());
		RemoteToParentId = new Dictionary<RemotePtr, int>(new RemotePtr.EqualityComparer());
		InitializeComponent();
		base.ImageList = new ImageList();
		base.ImageList.TransparentColor = Color.FromArgb(255, 0, 255);
		base.ImageList.ImageSize = new Size(16, 15);
		Bitmap gridStrip = Resources.GridStrip;
		base.ImageList.Images.AddStrip(gridStrip);
		WidgetBase.EventCreate += palWidgets_EventCreate;
		WidgetBase.EventDestroy += palWidgets_EventDestroy;
		WidgetBase.EventSuspendLayout += palWidgets_EventSuspendLayout;
		WidgetBase.EventResumeLayout += palWidgets_EventResumeLayout;
		if (WidgetMgr.Instance != null)
		{
			WidgetMgr instance = WidgetMgr.Instance;
			instance.LayoutDelegate = (WidgetMgr.LayoutDel)Delegate.Combine(instance.LayoutDelegate, new WidgetMgr.LayoutDel(AffectLayout));
		}
		Units.UpdateUnitsDelegate = (Units.UpdateUnitsDel)Delegate.Combine(Units.UpdateUnitsDelegate, new Units.UpdateUnitsDel(UpdateUnits));
	}

	public void DeregisterEvents()
	{
		WidgetBase.EventCreate -= palWidgets_EventCreate;
		WidgetBase.EventDestroy -= palWidgets_EventDestroy;
		WidgetBase.EventSuspendLayout -= palWidgets_EventSuspendLayout;
		WidgetBase.EventResumeLayout -= palWidgets_EventResumeLayout;
		WidgetMgr instance = WidgetMgr.Instance;
		instance.LayoutDelegate = (WidgetMgr.LayoutDel)Delegate.Remove(instance.LayoutDelegate, new WidgetMgr.LayoutDel(AffectLayout));
		Units.UpdateUnitsDelegate = (Units.UpdateUnitsDel)Delegate.Remove(Units.UpdateUnitsDelegate, new Units.UpdateUnitsDel(UpdateUnits));
	}

	public void AddWidget(WidgetBase widget)
	{
		if (widget is Bank { IsWidgetPanel: not false })
		{
			return;
		}
		WidgetBase widgetBase = widget.Parent;
		if (widgetBase == null)
		{
			return;
		}
		RemotePtr key = WidgetMgr.Instance.MapLocalToRemote(widgetBase);
		if (widgetBase != WidgetBank && !Parents.ContainsKey(key))
		{
			return;
		}
		PropertyEnumerator propertyEnumerator = null;
		if (widget is Bank)
		{
			RemotePtr remotePtr = WidgetMgr.Instance.MapLocalToRemote(widget);
			if (Parents.ContainsKey(remotePtr))
			{
				throw new ApplicationException("Bank '" + widget.Name + "' is being added twice!");
			}
			Parents.Add(remotePtr, remotePtr);
			if (widgetBase == WidgetBank)
			{
				AppendRootCategory(remotePtr.Id, widget.Name);
				return;
			}
			Bank bank2 = widget as Bank;
			propertyEnumerator = FindProperty(key.Id);
			PropertyEnumerator propertyEnum = AppendSubCategory(propertyEnumerator, remotePtr.Id, bank2.Name);
			ExpandProperty(propertyEnum, bank2.WidgetValue);
			propertyEnum.Property.Tag = bank2;
			bank2.WidgetValueChanged += delegate
			{
				ExpandProperty(propertyEnum, bank2.WidgetValue);
			};
			return;
		}
		propertyEnumerator = FindProperty(key.Id);
		if (propertyEnumerator == base.RightBound)
		{
			propertyEnumerator = AppendRootCategory(key.Id, widget.Parent.Name);
		}
		RemotePtr propertyId = WidgetMgr.Instance.MapLocalToRemote(widget);
		PropertyEnumerator propEnum = null;
		switch (widget.GetType().Name)
		{
		case "Button":
		{
			WidgetButton widgetButton = new WidgetButton(widget, this, propertyEnumerator, null);
			propEnum = widgetButton.PropEnum;
			break;
		}
		case "Mutex":
		{
			WidgetMutex gridWidget3 = new WidgetMutex(widget, this, propertyEnumerator, null);
			propEnum = gridWidget3.PropEnum;
			Mutex mutex = widget as Mutex;
			mutex.AddValuesDelegate = delegate(List<string> names)
			{
				gridWidget3.PropEnum.Property.Value.ResetDisplayedValues(names.ToArray());
				gridWidget3.PropEnum.Property.HeightMultiplier = names.Count;
				if (mutex.FindNameIndex(mutex.WidgetValue) >= 0)
				{
					gridWidget3.WidgetValue = mutex.FindName(mutex.WidgetValue);
				}
			};
			gridWidget3.PropEnum.Property.Feel = GetRegisteredFeel("radiobutton");
			gridWidget3.PropEnum.Property.Value.Look = new PropertyRadioButtonLook();
			gridWidget3.PropEnum.Property.HeightMultiplier = 1;
			gridWidget3.PropEnum.Property.Value.ResetDisplayedValues(new string[1] { " " });
			break;
		}
		case "Color32":
		case "ColorVector3":
		case "ColorVector4":
		{
			WidgetColor widgetColor = new WidgetColor(widget, this, propertyEnumerator, new Attribute[1]
			{
				new PropertyDropDownContentAttribute(typeof(AlphaColorPicker))
			});
			propEnum = widgetColor.PropEnum;
			break;
		}
		case "ComboBoxInt":
		{
			WidgetComboBoxInt widgetComboBoxInt = new WidgetComboBoxInt(widget, this, propertyEnumerator, null);
			widgetComboBoxInt.PropEnum.Property.Feel = GetRegisteredFeel("list");
			propEnum = widgetComboBoxInt.PropEnum;
			break;
		}
		case "Label":
			propEnum = AppendLabelProperty(propertyEnumerator, propertyId.Id, widget.Name, widget.Name);
			break;
		case "TextBox":
		{
			Widget<string> widget3 = new Widget<string>(widget, this, propertyEnumerator, null);
			widget3.PropEnum.Property.Feel = GetRegisteredFeel("edit");
			propEnum = widget3.PropEnum;
			break;
		}
		case "FileLoader":
		{
			WidgetFileLoader widgetFileLoader = new WidgetFileLoader(widget, this, propertyEnumerator, null);
			widgetFileLoader.PropEnum.Property.Feel = GetRegisteredFeel("button");
			propEnum = widgetFileLoader.PropEnum;
			break;
		}
		case "GenericLoader":
		{
			WidgetGenericLoader widgetGenericLoader = new WidgetGenericLoader(widget, this, propertyEnumerator, null);
			widgetGenericLoader.PropEnum.Property.Feel = GetRegisteredFeel("button");
			propEnum = widgetGenericLoader.PropEnum;
			break;
		}
		case "SliderInt":
		{
			SliderInt slider = widget as SliderInt;
			Widget<int> widget2 = new Widget<int>(widget, this, propertyEnumerator, null);
			widget2.PropEnum.Property.Feel = GetRegisteredFeel("editupdown");
			widget2.PropEnum.Property.Value.Validator = new PropertyValidatorSlider(widget2.PropEnum, slider);
			propEnum = widget2.PropEnum;
			break;
		}
		case "SliderFloat":
		{
			Bank bank3 = widget.Parent as Bank;
			PropertyEnumerator gridEnum = propertyEnumerator;
			bool updateParent = false;
			if (bank3 != null && bank3.CurrentGroup != null)
			{
				gridEnum = FindOrCreateGroup(propertyEnumerator, bank3, propertyId);
				updateParent = true;
			}
			SliderFloat sliderFloat = widget as SliderFloat;
			Widget<float> gridWidget2 = new Widget<float>(widget, this, gridEnum, null);
			gridWidget2.UpdateParent = updateParent;
			gridWidget2.PropEnum.Property.Feel = GetRegisteredFeel("editupdown");
			gridWidget2.PropEnum.Property.Value.Validator = new PropertyValidatorSlider(gridWidget2.PropEnum, sliderFloat);
			sliderFloat.SetUnitsDelegate = (SliderFloat.SetUnitsDel)Delegate.Combine(sliderFloat.SetUnitsDelegate, (SliderFloat.SetUnitsDel)delegate(SliderFloat slider2, bool clear)
			{
				SetUnits(slider2, gridWidget2.PropEnum, clear);
			});
			propEnum = gridWidget2.PropEnum;
			break;
		}
		case "Toggle":
		{
			Widget<bool> gridWidget = new Widget<bool>(widget, this, propertyEnumerator, null);
			Toggle toggle = widget as Toggle;
			toggle.SetDisplayedValuesDelegate = delegate(Toggle toggleWidget)
			{
				gridWidget.PropEnum.Property.Value.ResetDisplayedValues(toggleWidget.DisplayedValues);
			};
			gridWidget.PropEnum.Property.Feel = GetRegisteredFeel("checkbox");
			gridWidget.PropEnum.Property.Value.Look = new PropertyCheckboxLook();
			propEnum = gridWidget.PropEnum;
			break;
		}
		}
		SetEvents(widget, propEnum);
	}

	protected override void OnPropertySelected(PropertySelectedEventArgs e)
	{
		if (e.PropertyEnum.Property.Tag is WidgetBase widgetBase)
		{
			if (WidgetWithLastFocus != null && widgetBase != WidgetWithLastFocus && !WidgetWithLastFocus.Destroyed)
			{
				WidgetWithLastFocus.WriteMessage(160u);
			}
			widgetBase.WriteMessage(159u);
			WidgetWithLastFocus = widgetBase;
		}
		base.OnPropertySelected(e);
	}

	private void SetEvents(WidgetBase widget, PropertyEnumerator propEnum)
	{
		widget.EventShowModified += delegate(object sender, EventArgs args)
		{
			WidgetBase widgetBase = sender as WidgetBase;
			_ = widgetBase.ShowModified;
			_ = propEnum.Property.ImageIndex;
			if (widgetBase.ShowModified)
			{
				if (propEnum.Property.ImageIndex != 0)
				{
					propEnum.Property.Font = new Font(propEnum.Property.Value.Font, FontStyle.Bold);
					propEnum.Property.ImageIndex = 0;
					propEnum.Property.InvalidateProperty(children: true);
				}
			}
			else if (propEnum.Property.ImageIndex != -1)
			{
				propEnum.Property.Font = new Font(propEnum.Property.Value.Font, FontStyle.Regular);
				propEnum.Property.ImageIndex = -1;
				propEnum.Property.InvalidateProperty(children: true);
			}
		};
		widget.EventEnabled += delegate(object sender, EventArgs args)
		{
			if (sender is WidgetBase widgetBase)
			{
				EnableProperty(propEnum, widgetBase.Enabled);
			}
		};
		widget.EventCanManuallyDisable += delegate(object sender, EventArgs args)
		{
			if (sender is WidgetBase widgetBase)
			{
				if (widgetBase.CanManuallyDisable)
				{
					SetManuallyDisabled(propEnum, widgetBase, "ManualEnable");
				}
				else
				{
					SetManuallyDisabled(propEnum, disabled: false);
				}
			}
		};
		widget.EventComment += palWidgets_EventComment;
	}

	public PropertyEnumerator FindOrCreateGroup(PropertyEnumerator parentEnum, Bank bank, RemotePtr propertyId)
	{
		if (!BankGroupToPropGroup.TryGetValue(bank.CurrentGroup, out var value))
		{
			value = AppendManagedProperty(parentEnum, propertyId.Id, bank.CurrentGroup.Name, typeof(string), "", "");
			value.Property.Feel = GetRegisteredFeel("none");
			value.Property.Value.NoLinkWithChildren = true;
			BankGroupToPropGroup.Add(bank.CurrentGroup, value);
		}
		return value;
	}

	public void RemoveWidget(WidgetBase widget)
	{
		if (widget is palDotNet.palWidgets.Menu)
		{
			return;
		}
		RemotePtr key = WidgetMgr.Instance.MapLocalToRemote(widget);
		bool flag = true;
		if (widget is Bank)
		{
			flag = Parents.Remove(key);
		}
		if (flag)
		{
			PropertyEnumerator propertyEnumerator = FindProperty(key.Id);
			if (propertyEnumerator != null && propertyEnumerator.Property != null)
			{
				DeleteProperty(propertyEnumerator);
			}
		}
	}

	private void SetUnits(SliderFloat slider, PropertyEnumerator propEnum, bool clear)
	{
		if (propEnum != null && propEnum.Property != null)
		{
			if (clear)
			{
				propEnum.Property.Look = null;
			}
			else if (!(propEnum.Property.Look is PropertyUnitLook))
			{
				propEnum.Property.AddValue((object)"unit", (object)slider, "UnitType", (Attribute[])null);
				propEnum.Property.Look = new PropertyUnitLook();
			}
			if (propEnum.Property.ParentGrid.InPlaceControl is IInPlaceControl inPlaceControl)
			{
				inPlaceControl.UpdateFromDisplay();
			}
			propEnum.Property.InvalidateProperty(children: true);
		}
	}

	public void PauseUpdate(bool forcedPause)
	{
		if (UpdateCount == 0 && !PauseForced)
		{
			BeginUpdate();
		}
		if (forcedPause)
		{
			if (!PauseForced)
			{
				UpdateCount++;
			}
			PauseForced = true;
		}
		else
		{
			UpdateCount++;
		}
	}

	public void UnPauseUpdate(bool forcedPause)
	{
		if (forcedPause)
		{
			if (PauseForced)
			{
				PauseForced = false;
				if (--UpdateCount == 0)
				{
					EndUpdate();
					Refresh();
				}
			}
		}
		else if (--UpdateCount == 0)
		{
			EndUpdate();
			Refresh();
		}
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.NavigationKeyMode = VisualHint.SmartPropertyGrid.PropertyGrid.NavigationKeyModes.TabKey | VisualHint.SmartPropertyGrid.PropertyGrid.NavigationKeyModes.TabKeySkipReadonlyProperties;
		base.ResumeLayout(false);
	}

	public bool CopySettingsTo(string settingsKey, string settingsKeyTo, PropertyGrid control)
	{
		if (control != null)
		{
			SaveComponentSettings(settingsKey);
			PropertyGridSettings configSettings = control.GetConfigSettings(settingsKeyTo);
			foreach (SettingsPropertyValue propertyValue in TheConfigSettings.PropertyValues)
			{
				configSettings[propertyValue.Name] = propertyValue.PropertyValue;
			}
			return true;
		}
		return false;
	}

	public void SaveComponentSettings(string settingsKey)
	{
		try
		{
			GetConfigSettings(settingsKey);
			TheConfigSettings.LabelColumnWidthRatio = base.LabelColumnWidthRatio;
			TheConfigSettings.CommentsHeight = base.CommentsHeight;
			TheConfigSettings.Save();
		}
		catch { }
	}

	public void ResetComponentSettings(string settingsKey)
	{
		GetConfigSettings(settingsKey);
		TheConfigSettings.Reload();
	}

	public void LoadComponentSettings(string settingsKey)
	{
		GetConfigSettings(settingsKey);
		double? labelColumnWidthRatio = TheConfigSettings.LabelColumnWidthRatio;
		if (labelColumnWidthRatio.HasValue)
		{
			base.LabelColumnWidthRatio = labelColumnWidthRatio.Value;
		}
		int? commentsHeight = TheConfigSettings.CommentsHeight;
		if (commentsHeight.HasValue)
		{
			base.CommentsHeight = commentsHeight.Value;
		}
	}

	private void UpdateUnits(Units units)
	{
		PropertyEnumerator propertyEnumerator = base.FirstProperty;
		while (propertyEnumerator != null && propertyEnumerator.Property != null)
		{
			WidgetBase widgetBase = propertyEnumerator.Property.Tag as WidgetBase;
			widgetBase?.UpdateUnits(units);
			if (widgetBase is ISlider slider)
			{
				slider.GetMinMax(out var min, out var max);
				propertyEnumerator.Property.Value.Validator = new PropertyValidatorMinMax(propertyEnumerator, min, max);
				propertyEnumerator.Property.InvalidateProperty(children: true);
			}
			propertyEnumerator = propertyEnumerator.MoveNext();
		}
	}

	private void palWidgets_EventCreate(object s, EventArgs e)
	{
		AddWidget(s as WidgetBase);
	}

	private void palWidgets_EventDestroy(object s, EventArgs e)
	{
		RemoveWidget(s as WidgetBase);
	}

	private void palWidgets_EventSuspendLayout(object s, EventArgs e)
	{
		PauseUpdate(forcedPause: false);
	}

	private void palWidgets_EventResumeLayout(object s, EventArgs e)
	{
		UnPauseUpdate(forcedPause: false);
	}

	private void palWidgets_EventComment(object s, EventArgs e)
	{
		WidgetBase widgetBase = s as WidgetBase;
		if (!(widgetBase is palDotNet.palWidgets.Menu))
		{
			PropertyEnumerator propertyEnumerator = FindProperty(WidgetMgr.Instance.MapLocalToRemote(widgetBase).Id);
			if (propertyEnumerator != null && propertyEnumerator.Property != null)
			{
				propertyEnumerator.Property.Comment = widgetBase.Comment;
			}
		}
	}

	private void palWidgets_EventEnabled(object s, EventArgs e)
	{
		WidgetBase widgetBase = s as WidgetBase;
		if (!(widgetBase is palDotNet.palWidgets.Menu))
		{
			PropertyEnumerator propertyEnumerator = FindProperty(WidgetMgr.Instance.MapLocalToRemote(widgetBase).Id);
			if (propertyEnumerator != null && propertyEnumerator.Property != null)
			{
				propertyEnumerator.Property.Value.ReadOnly = !widgetBase.Enabled;
			}
		}
	}

	private void AffectLayout(bool suspend)
	{
		if (suspend)
		{
			PauseUpdate(forcedPause: false);
		}
		else
		{
			UnPauseUpdate(forcedPause: false);
		}
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		if (e.Button != MouseButtons.Left)
		{
			Scrubber.MouseUp(this, e);
			return;
		}
		HitTests hitTests = HitTest(new Point(e.X, e.Y));
		if (hitTests == HitTests.Label)
		{
			MouseScrub(this, e);
		}
	}

	private void MouseScrub(object sender, MouseEventArgs e)
	{
		Scrubber.AddIncrement = null;
		PropertyEnumerator selectedPropertyEnumerator = base.SelectedPropertyEnumerator;
		if (selectedPropertyEnumerator.Property == null || (selectedPropertyEnumerator.Property.Value != null && selectedPropertyEnumerator.Property.Value.ReadOnly) || !selectedPropertyEnumerator.Property.Enabled)
		{
			return;
		}
		ISlider slider = selectedPropertyEnumerator.Property.Tag as ISlider;
		if (slider is IColorSlider)
		{
			slider = null;
		}
		if (slider != null)
		{
			Scrubber.NumPixelsTrigger = slider.NumPixelsTrigger;
			slider.AlwaysFlushEditing = false;
		}
		SliderVector3 sliderVector = selectedPropertyEnumerator.Property.Tag as SliderVector3;
		SliderVector4 sliderVector2 = selectedPropertyEnumerator.Property.Tag as SliderVector4;
		if (sliderVector != null)
		{
			switch (selectedPropertyEnumerator.Property.Name)
			{
			case "x":
				Scrubber.AddIncrement = sliderVector.AddIncrementX;
				break;
			case "y":
				Scrubber.AddIncrement = sliderVector.AddIncrementY;
				break;
			case "z":
				Scrubber.AddIncrement = sliderVector.AddIncrementZ;
				break;
			}
		}
		else if (sliderVector2 != null)
		{
			switch (selectedPropertyEnumerator.Property.Name)
			{
			case "x":
				Scrubber.AddIncrement = sliderVector2.AddIncrementX;
				break;
			case "y":
				Scrubber.AddIncrement = sliderVector2.AddIncrementY;
				break;
			case "z":
				Scrubber.AddIncrement = sliderVector2.AddIncrementZ;
				break;
			case "w":
				Scrubber.AddIncrement = sliderVector2.AddIncrementW;
				break;
			}
		}
		else if (slider != null)
		{
			Scrubber.AddIncrement = slider.AddIncrement;
		}
		if (Scrubber.AddIncrement != null)
		{
			Scrubber.MouseDown(sender, e);
			Scrubber.ScrubbingObject = base.SelectedPropertyEnumerator;
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		if (Scrubber.IsScrubbing)
		{
			PropertyEnumerator selectedPropertyEnumerator = base.SelectedPropertyEnumerator;
			if (Scrubber.ScrubbingObject as PropertyEnumerator != selectedPropertyEnumerator)
			{
				Scrubber.MouseUp(this, e);
				return;
			}
			Scrubber.MouseMove(this, e);
			WidgetBase widgetBase = selectedPropertyEnumerator.Property.Tag as WidgetBase;
			SliderVector3 sliderVector = selectedPropertyEnumerator.Property.Tag as SliderVector3;
			SliderVector4 sliderVector2 = selectedPropertyEnumerator.Property.Tag as SliderVector4;
			object obj = null;
			if (sliderVector != null)
			{
				switch (selectedPropertyEnumerator.Property.Name)
				{
				case "x":
					obj = sliderVector.x;
					break;
				case "y":
					obj = sliderVector.y;
					break;
				case "z":
					obj = sliderVector.z;
					break;
				}
			}
			else if (sliderVector2 != null)
			{
				switch (selectedPropertyEnumerator.Property.Name)
				{
				case "x":
					obj = sliderVector2.x;
					break;
				case "y":
					obj = sliderVector2.y;
					break;
				case "z":
					obj = sliderVector2.z;
					break;
				case "w":
					obj = sliderVector2.w;
					break;
				}
			}
			else
			{
				obj = widgetBase.WidgetValueObject;
			}
			selectedPropertyEnumerator.Property.Value.SetValue(obj);
			if (base.InPlaceControl is PropInPlaceUpDown propInPlaceUpDown)
			{
				propInPlaceUpDown.ActualText = obj.ToString();
			}
		}
		else
		{
			base.OnMouseMove(e);
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		if (e.Button == MouseButtons.Right)
		{
			Point point = new Point(e.X, e.Y);
			PropertyEnumerator propertyEnumerator = PropertyItemFromPoint(point, out var _);
			if (propertyEnumerator != null && propertyEnumerator.Property != null && propertyEnumerator.Property.Tag is WidgetBase widgetBase2)
			{
				widgetBase2.ShowMenu();
			}
		}
		else
		{
			if (e.Button != MouseButtons.Left || !Scrubber.IsScrubbing)
			{
				return;
			}
			Scrubber.MouseUp(this, e);
			PropertyEnumerator selectedPropertyEnumerator = base.SelectedPropertyEnumerator;
			if (selectedPropertyEnumerator != null)
			{
				WidgetBase widgetBase3 = selectedPropertyEnumerator.Property.Tag as WidgetBase;
				if (widgetBase3 is ISlider)
				{
					widgetBase3.SendEditingEndThenBegin();
					(widgetBase3 as ISlider).AlwaysFlushEditing = true;
				}
			}
		}
	}

	protected override void OnPropertyUpDown(PropertyUpDownEventArgs e)
	{
		base.OnPropertyUpDown(e);
		WidgetBase widgetBase = e.PropertyEnum.Property.Tag as WidgetBase;
		if (widgetBase is ISlider slider)
		{
			PropertyEnumerator propertyEnum = e.PropertyEnum;
			e.Value = slider.ProcessUpDown(e.ButtonPressed == PropertyUpDownEventArgs.UpDownButtons.Up, e.Value, propertyEnum.Property.Name);
			widgetBase.SetFlushEditing();
		}
		if (!Scrubber.IsScrubbing && (Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
		{
			MouseEventArgs e2 = new MouseEventArgs(Control.MouseButtons, 0, Control.MousePosition.X, Control.MousePosition.Y, 0);
			MouseScrub(this, e2);
		}
	}

	protected override void OnInPlaceCtrlVisible(InPlaceCtrlVisibleEventArgs e)
	{
		base.OnInPlaceCtrlVisible(e);
		if (e.InPlaceCtrl is PropInPlaceUpDown propInPlaceUpDown)
		{
			propInPlaceUpDown.Increment = 0.0m;
		}
		if (e.InPlaceCtrl is IInPlaceControl inPlaceControl)
		{
			inPlaceControl.RealtimeChange = true;
		}
		WidgetBase widgetBase = e.PropertyEnum.Property.Tag as WidgetBase;
		if (widgetBase is ISlider)
		{
			LastInPlaceCtrlVisibleSlider = widgetBase;
			widgetBase.SendEditingBegin(delayMsg: true);
		}
	}

	protected override void OnInPlaceCtrlHidden(InPlaceCtrlVisibleEventArgs e)
	{
		if (LastInPlaceCtrlVisibleSlider != null)
		{
			LastInPlaceCtrlVisibleSlider.SendEditingEnd(skipIfDelayed: true);
			LastInPlaceCtrlVisibleSlider = null;
			if (Scrubber.IsScrubbing)
			{
				Scrubber.MouseUp(this, EventArgs.Empty);
			}
		}
	}

	protected override void OnDisplayedValuesNeeded(DisplayedValuesNeededEventArgs e)
	{
		WidgetBase widgetBase = e.PropertyEnum.Property.Tag as WidgetBase;
		if (widgetBase is palDotNet.palWidgets.ComboBox)
		{
			e.DisplayedValues = (widgetBase as palDotNet.palWidgets.ComboBox).Items;
		}
		else if (widgetBase is Toggle)
		{
			e.DisplayedValues = (widgetBase as Toggle).DisplayedValues;
		}
	}

	protected override void OnButtonPropertyClicked(PropertyButtonClickedEventArgs e)
	{
		base.OnButtonPropertyClicked(e);
		WidgetBase widgetBase = e.PropertyEnum.Property.Tag as WidgetBase;
		if (widgetBase is palDotNet.palWidgets.Button)
		{
			(widgetBase as palDotNet.palWidgets.Button).WidgetValue = true;
		}
	}

	protected override void OnPropertyButtonClicked(PropertyButtonClickedEventArgs e)
	{
		base.OnPropertyButtonClicked(e);
		WidgetBase widgetBase = e.PropertyEnum.Property.Tag as WidgetBase;
		if (widgetBase is FileLoader)
		{
			FileLoader fileLoader = widgetBase as FileLoader;
			WidgetMgr.FileDialogDel fileDialogDelegate = WidgetMgr.Instance.FileDialogDelegate;
			if (fileDialogDelegate == null)
			{
				return;
			}
			bool flag;
			do
			{
				flag = false;
				string empty = string.Empty;
				empty = fileDialogDelegate(isSave: false, fileLoader.Types, fileLoader.Extensions, fileLoader.InitialDirectory, fileLoader.FilterIndex, fileLoader.Title);
				if (string.IsNullOrEmpty(empty))
				{
					continue;
				}
				string value = fileLoader.VerifyFileName(empty);
				if (!string.IsNullOrEmpty(value))
				{
					DialogResult dialogResult = WidgetMgr.Instance.ShowMessageBox(value, "File Type Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand);
					if (dialogResult == DialogResult.Retry)
					{
						flag = true;
					}
				}
				else
				{
					fileLoader.WidgetValue = empty;
				}
			}
			while (flag);
		}
		else if (widgetBase is GenericLoader)
		{
			(widgetBase as GenericLoader).SendButtonPressed();
		}
	}

	protected override void OnPropertyExpanded(PropertyExpandedEventArgs e)
	{
		if (e.PropertyEnum.Property.Tag is Bank bank)
		{
			bank.WidgetValue = e.Expanded;
		}
		base.OnPropertyExpanded(e);
	}

	protected override void OnLostFocus(EventArgs e)
	{
		if (Scrubber.IsScrubbing)
		{
			Scrubber.MouseUp(this, EventArgs.Empty);
		}
		base.OnLostFocus(e);
	}

	[ReadOnly(true)]
	[Browsable(false)]
	public PropertyGridSettings GetConfigSettings(string settingsKey)
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new PropertyGridSettings(settingsKey);
		}
		return TheConfigSettings;
	}
}
