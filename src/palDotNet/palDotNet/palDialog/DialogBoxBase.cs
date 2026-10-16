using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palDialog;

public class DialogBoxBase : Form, IPersistComponentSettings
{
	public delegate void AddEventsDelegate();

	private DialogResult Result = DialogResult.Cancel;

	private WidgetDialogBox TheWidgetDialogBox;

	private DialogBoxBaseControlSettings ConfigSettings;

	public string OverriddenSettingsKey = string.Empty;

	public AddEventsDelegate AddEvents;

	public static readonly Dictionary<Guid, DialogBoxBase> GuidToDialog = new Dictionary<Guid, DialogBoxBase>();

	protected bool AllowSubmitDialog = true;

	private IContainer components;

	private ToolTip toolTip;

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
			if (!string.IsNullOrEmpty(OverriddenSettingsKey))
			{
				return OverriddenSettingsKey;
			}
			if (TheWidgetDialogBox != null && TheWidgetDialogBox.Guid.HasValue)
			{
				return TheWidgetDialogBox.Guid.ToString();
			}
			return null;
		}
		set
		{
		}
	}

	public DialogBoxBaseControlSettings Settings => ConfigSettings;

	public DialogBoxBase()
	{
		InitializeComponent();
		base.KeyPreview = true;
	}

	public _Type FindVariable<_Type>(string name)
	{
		Type type = GetType();
		BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		FieldInfo[] fields = type.GetFields(bindingAttr);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (fieldInfo.Name == name && typeof(_Type) == fieldInfo.FieldType)
			{
				return (_Type)fieldInfo.GetValue(this);
			}
		}
		return default(_Type);
	}

	public IDialogValue[] FindComponents()
	{
		List<IDialogValue> list = new List<IDialogValue>();
		BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		FieldInfo[] fields = GetType().GetFields(bindingAttr);
		foreach (FieldInfo fieldInfo in fields)
		{
			bool flag = false;
			bool flag2 = false;
			Type[] interfaces = fieldInfo.FieldType.GetInterfaces();
			foreach (Type type in interfaces)
			{
				if (type == typeof(IDialogValue))
				{
					flag = true;
				}
				else if (type == typeof(Control))
				{
					flag2 = true;
				}
			}
			if (flag && !flag2)
			{
				list.Add(fieldInfo.GetValue(this) as IDialogValue);
			}
		}
		return list.ToArray();
	}

	protected void SetToolTipsOnControls(Control.ControlCollection controls)
	{
		foreach (IDialogValue item in new DialogValueEnumerator(controls, null))
		{
			Control control = item as Control;
		}
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		SetToolTipsOnControls(base.Controls);
	}

	public void SetValues(Dictionary<string, WidgetDialogBox.UpdateValue> values)
	{
		foreach (IDialogValue item in new DialogValueEnumerator(base.Controls, FindComponents()))
		{
			if (!values.ContainsKey(item.Name))
			{
				continue;
			}
			try
			{
				WidgetDialogBox.UpdateValue updateValue = values[item.Name];
				if (!item.LoadedFromSettings || !updateValue.SetOnlyOnInitial)
				{
					item.DialogValue = updateValue.Value;
				}
			}
			catch (Exception ex)
			{
				string line = string.Concat("FAILED to set value '", values[item.Name], "' to dialog box value for control '", item.Name, "': ", ex.Message);
				Output.WriteLine(line, Output.EnumModes.eError);
			}
		}
	}

	public void UpdateComboBoxItems(string comboBoxName, string[] items)
	{
		foreach (IDialogValue item in new DialogValueEnumerator(base.Controls, null))
		{
			if (!(item is DialogComboBox dialogComboBox) || !(dialogComboBox.Name.ToLower() == comboBoxName.ToLower()))
			{
				continue;
			}
			string text = ((dialogComboBox.SelectedItem != null && !string.IsNullOrEmpty(dialogComboBox.SelectedItem.ToString())) ? dialogComboBox.SelectedItem.ToString() : dialogComboBox.LastRequestedValue);
			int num = -1;
			dialogComboBox.BeginUpdate();
			dialogComboBox.Items.Clear();
			for (int num2 = 0; num2 < items.Length; num2++)
			{
				dialogComboBox.Items.Add(items[num2]);
				if (items[num2] == text)
				{
					num = num2;
				}
			}
			dialogComboBox.SelectedIndex = ((num > 0) ? num : 0);
			dialogComboBox.EndUpdate();
		}
	}

	public void UpdateListBoxItems(string listBoxName, string[] items)
	{
		foreach (IDialogValue item in new DialogValueEnumerator(base.Controls, null))
		{
			if (!(item is DialogListBox dialogListBox) || !(dialogListBox.Name.ToLower() == listBoxName.ToLower()))
			{
				continue;
			}
			string text = ((dialogListBox.SelectedItem != null && !string.IsNullOrEmpty(dialogListBox.SelectedItem.ToString())) ? dialogListBox.SelectedItem.ToString() : dialogListBox.LastRequestedValue);
			int num = -1;
			dialogListBox.BeginUpdate();
			dialogListBox.Items.Clear();
			for (int num2 = 0; num2 < items.Length; num2++)
			{
				dialogListBox.Items.Add(items[num2]);
				if (items[num2] == text)
				{
					num = num2;
				}
			}
			dialogListBox.SelectedIndex = ((num > 0) ? num : 0);
			dialogListBox.EndUpdate();
		}
	}

	public static DialogBoxBase FindByGuid(Guid guid)
	{
		GuidToDialog.TryGetValue(guid, out var value);
		return value;
	}

	public DialogResult CustomShow()
	{
		if (AddEvents != null)
		{
			AddEvents();
		}
		Result = ShowDialog();
		return Result;
	}

	public DialogResult ShowDialogFromParentDialog()
	{
		Result = DialogResult.Cancel;
		if (TheWidgetDialogBox != null)
		{
			TheWidgetDialogBox.Show(startThread: false);
		}
		return Result;
	}

	public void Destroy()
	{
		if (TheWidgetDialogBox.Guid.HasValue)
		{
			GuidToDialog.Remove(TheWidgetDialogBox.Guid.Value);
		}
	}

	public List<DictionaryEntry> CollectSettingsForSaving()
	{
		List<DictionaryEntry> list = new List<DictionaryEntry>();
		foreach (IDialogValue item2 in new DialogValueEnumerator(base.Controls, FindComponents()))
		{
			DictionaryEntry item = default(DictionaryEntry);
			if (item2.SaveToConfig)
			{
				item.Key = item2.Name;
				item.Value = item2.DialogValue;
				list.Add(item);
			}
		}
		return list;
	}

	public void ApplySettings(List<DictionaryEntry> properties)
	{
		foreach (IDialogValue item in new DialogValueEnumerator(base.Controls, FindComponents()))
		{
			object dynamicProperty = DialogBoxBaseControlSettings.GetDynamicProperty(properties, item.Name);
			item.LoadedFromSettings = dynamicProperty != null;
			if (dynamicProperty != null)
			{
				item.DialogValue = dynamicProperty;
			}
		}
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		SaveComponentSettings();
		if (TheWidgetDialogBox != null)
		{
			TheWidgetDialogBox.SendValues(CollectValues());
		}
		base.OnFormClosing(e);
	}

	public Dictionary<string, string> CollectValues()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (IDialogValue item in new DialogValueEnumerator(base.Controls, FindComponents()))
		{
			dictionary[item.Name] = item.DialogValue.ToString();
		}
		return dictionary;
	}

	public static DialogBoxBase CreateDialog(WidgetDialogBox dialogBox, string name, string type, string dllName)
	{
		Assembly assem = null;
		if (dllName != null && dllName != string.Empty)
		{
			try
			{
				string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
				if (!dllName.ToLower().EndsWith(".dll"))
				{
					dllName += ".dll";
				}
				dllName = Path.Combine(directoryName, dllName);
				assem = Assembly.LoadFrom(dllName);
			}
			catch (Exception ex)
			{
				Output.WriteLine("failed to load dll '" + dllName + "': " + ex.Message);
				return null;
			}
		}
		DialogBoxCreatorAttribute dialogBoxCreatorAttribute = new DialogBoxCreatorAttribute();
		ComponentSearcher componentSearcher = new ComponentSearcher(dialogBoxCreatorAttribute.GetType(), assem);
		foreach (Type item in componentSearcher)
		{
			if (!(item.Name == type))
			{
				continue;
			}
			DialogBoxBase dialogBoxBase = Activator.CreateInstance(item, null) as DialogBoxBase;
			if (dialogBoxBase != null)
			{
				dialogBoxBase.Text = name;
				dialogBoxBase.TheWidgetDialogBox = dialogBox;
				if (dialogBoxBase.TheWidgetDialogBox != null)
				{
					dialogBoxBase.LoadComponentSettings();
					dialogBoxBase.TheWidgetDialogBox.SendValues(dialogBoxBase.CollectValues());
				}
				if (dialogBox != null && dialogBox.Guid.HasValue)
				{
					try
					{
						GuidToDialog.Add(dialogBox.Guid.Value, dialogBoxBase);
					}
					catch (ArgumentException)
					{
						throw new Exception("Dialog box '" + name + "' of type " + type + " adding guid (" + dialogBox.Guid.ToString() + ") that already exists.");
					}
				}
			}
			return dialogBoxBase;
		}
		return null;
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (AllowSubmitDialog && e.Control && e.KeyCode == Keys.Return)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private bool SettingsCommon()
	{
		if (SettingsKey == null)
		{
			return false;
		}
		if (ConfigSettings == null)
		{
			ConfigSettings = new DialogBoxBaseControlSettings(SettingsKey);
		}
		return true;
	}

	public void SaveComponentSettings()
	{
		if (SettingsCommon())
		{
			ConfigSettings.DynamicProperties = CollectSettingsForSaving();
			ConfigSettings.Save();
		}
	}

	public void ResetComponentSettings()
	{
		if (SettingsCommon())
		{
			ConfigSettings.Reload();
		}
	}

	public void LoadComponentSettings()
	{
		if (SettingsCommon())
		{
			ApplySettings(ConfigSettings.DynamicProperties);
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
		this.components = new System.ComponentModel.Container();
		this.toolTip = new System.Windows.Forms.ToolTip(this.components);
		base.SuspendLayout();
		this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(551, 413);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "DialogBoxBase";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		this.Text = "DialogBase";
		base.ResumeLayout(false);
	}
}
