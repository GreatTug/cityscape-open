using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palDialog;

[WidgetCreator]
public class WidgetDialogBox(uint newId) : Widget<bool>(newId)
{
	public enum Messages
	{
		eUpdateValues = 161,
		eShow,
		eClosedOK,
		eClosedCancel,
		eUpdateComboBoxItems,
		eUpdateListBoxItems
	}

	public struct UpdateValue(string theValue, bool setOnlyOnInitial)
	{
		public string Value = theValue;

		public bool SetOnlyOnInitial = setOnlyOnInitial;
	}

	public delegate void UpdateThreadDel(ref bool stopThread, WidgetDialogBox lockObject);

	public static UpdateThreadDel UpdateThread;

	private Guid? TheGuid;

	private bool StopThread;

	private DialogBoxBase TheVisualDialog;

	private Dictionary<string, string> KeyValuePairs = new Dictionary<string, string>();

	public Guid? Guid => TheGuid;

	public DialogBoxBase VisualDialog => TheVisualDialog;

	private void ShowDialogUpdateDelegate()
	{
		palDotNet.palCore.Thread thread = new palDotNet.palCore.Thread(delegate
		{
			UpdateThread(ref StopThread, this);
		});
		thread.Inst.Name = "ShowDialog Thread";
		thread.Inst.SetApartmentState(ApartmentState.STA);
		thread.Inst.Start();
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		switch (message)
		{
		case 137u:
		{
			string inst8 = null;
			string inst9 = null;
			serializer.Serialize(ref TheParent);
			serializer.Serialize(ref TheName);
			serializer.Serialize(ref inst8);
			serializer.Serialize(ref inst9);
			string inst10 = null;
			serializer.Serialize(ref inst10);
			if (inst10 == null || inst10 == string.Empty)
			{
				TheGuid = null;
			}
			else
			{
				TheGuid = new Guid(inst10);
			}
			if (serializer.Reading)
			{
				WidgetMgr.Instance.Reader.End(clearIndex: false);
				TheVisualDialog = DialogBoxBase.CreateDialog(this, TheName, inst8, inst9);
				WidgetMgr.Instance.Reader.Begin();
			}
			break;
		}
		case 162u:
			WidgetMgr.Instance.Reader.End(clearIndex: false);
			Show(startThread: true);
			WidgetMgr.Instance.Reader.Begin();
			break;
		case 161u:
		{
			int inst4 = 0;
			if (serializer.Reading)
			{
				Dictionary<string, UpdateValue> dictionary = new Dictionary<string, UpdateValue>();
				serializer.Serialize(ref inst4);
				for (int j = 0; j < inst4; j++)
				{
					string inst5 = string.Empty;
					string inst6 = string.Empty;
					serializer.Serialize(ref inst5);
					serializer.Serialize(ref inst6);
					bool inst7 = false;
					serializer.Serialize(ref inst7);
					dictionary[inst5] = new UpdateValue(inst6, inst7);
				}
				TheVisualDialog.SetValues(dictionary);
			}
			break;
		}
		case 165u:
			if (serializer.Reading)
			{
				string inst11 = string.Empty;
				serializer.Serialize(ref inst11);
				uint inst12 = 0u;
				serializer.Serialize(ref inst12);
				string[] array2 = new string[inst12];
				for (int k = 0; k < inst12; k++)
				{
					string inst13 = string.Empty;
					serializer.Serialize(ref inst13);
					array2[k] = inst13;
				}
				TheVisualDialog.UpdateComboBoxItems(inst11, array2);
			}
			break;
		case 166u:
			if (serializer.Reading)
			{
				string inst = string.Empty;
				serializer.Serialize(ref inst);
				uint inst2 = 0u;
				serializer.Serialize(ref inst2);
				string[] array = new string[inst2];
				for (int i = 0; i < inst2; i++)
				{
					string inst3 = string.Empty;
					serializer.Serialize(ref inst3);
					array[i] = inst3;
				}
				TheVisualDialog.UpdateListBoxItems(inst, array);
			}
			break;
		default:
			base.Serialize(serializer, message);
			break;
		case 136u:
			break;
		}
	}

	public void Show(bool startThread)
	{
		DialogResult dialogResult = DialogResult.Cancel;
		if (TheVisualDialog != null)
		{
			SetValues(TheVisualDialog.CollectValues());
			if (startThread)
			{
				StopThread = false;
				ShowDialogUpdateDelegate();
				dialogResult = TheVisualDialog.CustomShow();
				lock (this)
				{
					StopThread = true;
				}
			}
			else
			{
				dialogResult = TheVisualDialog.CustomShow();
			}
		}
		if (dialogResult == DialogResult.Cancel)
		{
			WriteMessage(164u);
		}
		else
		{
			WriteMessage(163u);
		}
	}

	public void SendValues(Dictionary<string, string> controlValues)
	{
		SetValues(controlValues);
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		List<bool> list3 = new List<bool>();
		Dictionary<string, string>.Enumerator enumerator = controlValues.GetEnumerator();
		while (enumerator.MoveNext())
		{
			KeyValuePair<string, string> current = enumerator.Current;
			if (KeyValuePairs.ContainsKey(current.Key))
			{
				list.Add(current.Key);
				list2.Add(current.Value);
				list3.Add(KeyValuePairs[current.Key] != current.Value);
			}
		}
		ISerializer writer = WidgetMgr.Instance.Writer;
		writer.Begin();
		uint inst = 161u;
		writer.Serialize(ref inst);
		writer.Serialize(ref RemoteId);
		int inst2 = list.Count;
		writer.Serialize(ref inst2);
		for (int i = 0; i < inst2; i++)
		{
			string inst3 = list[i];
			string inst4 = list2[i];
			writer.Serialize(ref inst3);
			int inst5 = inst4.Length + 1;
			writer.Serialize(ref inst5);
			writer.Serialize(ref inst4);
			bool inst6 = list3[i];
			writer.Serialize(ref inst6);
		}
		writer.End(clearIndex: false);
	}

	public void SetValues(Dictionary<string, string> values)
	{
		KeyValuePairs = values;
	}

	protected override void OnWidgetDestroy()
	{
		if (TheVisualDialog != null)
		{
			TheVisualDialog.Destroy();
		}
	}
}
