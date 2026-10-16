using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using _003CCppImplementationDetails_003E;
using ktKitDotNet;
using mdMetaDataConstants;

internal class ktTemplateEditor
{
	private MetaDataTemplateEditor Dialog;

	private unsafe ktMetaDataTemplate* MetaData = null;

	public void Setup(MetaDataTemplateEditor dialog)
	{
		Dialog = dialog;
		PopulateItems();
		Dialog.NewElementButton.Click += NewElement;
		Dialog.DeleteElementButton.Click += DeleteElement;
		Dialog.ElementListBox.AfterLabelEdit += RenameElement;
		Dialog.ElementListBox.KeyUp += HandleDeleteKey;
		Dialog.ElementMoved += MoveElement;
		Dialog.LoadElement += LoadElement;
		Dialog.TypeChanged += TypeChanged;
		Dialog.BoolInfo.BoolChanged += BoolChanged;
		Dialog.ColorInfo.ColorChanged += ColorChanged;
		Dialog.EnumInfo.EnumChanged += EnumChanged;
		Dialog.EnumInfo.DisplayTypeChanged += EnumDisplayTypeChanged;
		Dialog.EnumInfo.EnumListChanged += EnumListChanged;
		Dialog.FloatInfo.FloatChanged += FloatChanged;
		Dialog.FloatInfo.MinChanged += MinFloatChanged;
		Dialog.FloatInfo.MaxChanged += MaxFloatChanged;
		Dialog.FloatInfo.StepChanged += StepFloatChanged;
		Dialog.FloatInfo.UnitsChanged += UnitsChanged;
		Dialog.IntInfo.IntChanged += IntChanged;
		Dialog.IntInfo.MinChanged += MinIntChanged;
		Dialog.IntInfo.MaxChanged += MaxIntChanged;
		Dialog.IntInfo.StepChanged += StepIntChanged;
		Dialog.StringInfo.StringChanged += StringChanged;
		Dialog.StringInfo.DisplayChanged += StringDisplayTypeChanged;
		Dialog.Vector2Info.Vector2Changed += Vector2Changed;
		Dialog.Vector2Info.MinChanged += MinFloatChanged;
		Dialog.Vector2Info.MaxChanged += MaxFloatChanged;
		Dialog.Vector2Info.StepChanged += StepFloatChanged;
		Dialog.Vector2Info.UnitsChanged += UnitsChanged;
		Dialog.Vector3Info.Vector3Changed += Vector3Changed;
		Dialog.Vector3Info.MinChanged += MinFloatChanged;
		Dialog.Vector3Info.MaxChanged += MaxFloatChanged;
		Dialog.Vector3Info.StepChanged += StepFloatChanged;
		Dialog.Vector3Info.UnitsChanged += UnitsChanged;
		Dialog.Vector4Info.Vector4Changed += Vector4Changed;
		Dialog.Vector4Info.MinChanged += MinFloatChanged;
		Dialog.Vector4Info.MaxChanged += MaxFloatChanged;
		Dialog.Vector4Info.StepChanged += StepFloatChanged;
		Dialog.Vector4Info.UnitsChanged += UnitsChanged;
	}

	public unsafe void SetMetaDataTemplate(ktMetaDataTemplate* metaData)
	{
		if (MetaData == metaData)
		{
			return;
		}
		MetaData = metaData;
		if (metaData != null)
		{
			Dialog.ElementGroupBox.Enabled = true;
			PopulateItems();
			return;
		}
		foreach (ListViewItem selectedItem in Dialog.ElementListBox.SelectedItems)
		{
			selectedItem.Selected = false;
		}
		Dialog.ElementListBox.Items.Clear();
		Dialog.ElementGroupBox.Enabled = false;
	}

	private unsafe void PopulateItems()
	{
		ktMetaDataTemplate* metaData = MetaData;
		if (metaData == null)
		{
			return;
		}
		uint num = 0u;
		if (0 < global::_003CModule_003E.ktMetaDataTemplate_002EGetMetaDataCount(metaData))
		{
			do
			{
				uint num2 = *(uint*)global::_003CModule_003E.ktMetaDataTemplate_002EGetMetaDataName(MetaData, num);
				sbyte* value = ((num2 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num2)));
				Dialog.ElementListBox.Items.Add(new string(value));
				num++;
			}
			while (num < global::_003CModule_003E.ktMetaDataTemplate_002EGetMetaDataCount(MetaData));
		}
	}

	private unsafe void LoadElement(int index)
	{
		//IL_02b1->IL02b6: Incompatible stack types: I4 vs Ref
		ktMetaDataTemplate* metaData = MetaData;
		if (metaData == null)
		{
			return;
		}
		arPair_003CarValue_002CmdMetaDataDetail_003E* ptr = global::_003CModule_003E.ktMetaDataTemplate_002EGetMetaData(metaData, (uint)index);
		if (ptr == null)
		{
			return;
		}
		switch (((int*)ptr)[1])
		{
		default:
			return;
		case 0:
			Dialog.SetType(KitCommon.DataType.eInteger);
			break;
		case 1:
			Dialog.SetType(KitCommon.DataType.eFloat);
			break;
		case 2:
			Dialog.SetType(KitCommon.DataType.eBoolean);
			break;
		case 3:
			Dialog.SetType(KitCommon.DataType.eEnumeration);
			break;
		case 4:
			Dialog.SetType(KitCommon.DataType.eColor);
			break;
		case 5:
			Dialog.SetType(KitCommon.DataType.eString);
			break;
		case 6:
			Dialog.SetType(KitCommon.DataType.eVector2);
			break;
		case 7:
			Dialog.SetType(KitCommon.DataType.eVector3);
			break;
		case 8:
			Dialog.SetType(KitCommon.DataType.eVector4);
			break;
		}
		KitCommon.Units units = ((int*)ptr)[3] switch
		{
			0 => KitCommon.Units.eNone, 
			1 => KitCommon.Units.eAngle, 
			2 => KitCommon.Units.eMetric, 
			3 => KitCommon.Units.ePercent, 
			_ => KitCommon.Units.eNone, 
		};
		switch (((int*)ptr)[1])
		{
		case 0:
			Dialog.IntInfo.Initialize(global::_003CModule_003E.arValueCast_003Cint_003E((arValue*)ptr, null), global::_003CModule_003E.arValueCast_003Cint_003E((arValue*)((byte*)ptr + 16), null), global::_003CModule_003E.arValueCast_003Cint_003E((arValue*)((byte*)ptr + 20), null), global::_003CModule_003E.arValueCast_003Cint_003E((arValue*)((byte*)ptr + 24), null));
			break;
		case 1:
			Dialog.FloatInfo.Initialize(global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)ptr, null), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 16), null), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 20), null), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 24), null), units);
			break;
		case 2:
			Dialog.BoolInfo.Initialize(global::_003CModule_003E.arValueCast_003Cbool_003E((arValue*)ptr, null));
			break;
		case 3:
		{
			uint num3 = ((uint*)((byte*)ptr + 32))[1];
			string[] array = new string[num3];
			uint num4 = 0u;
			if (0 < num3)
			{
				int num5 = 0;
				do
				{
					uint num6 = *(uint*)(num5 + ((int*)ptr)[8] + 4);
					sbyte* value2 = ((num6 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num6)));
					array[num4] = new string(value2);
					num4++;
					num5 += 12;
				}
				while (num4 < (uint)((int*)((byte*)ptr + 32))[1]);
			}
			KitCommon.EnumDisplayType displayType = ((((int*)ptr)[2] == 1) ? KitCommon.EnumDisplayType.eRadioButtons : KitCommon.EnumDisplayType.eComboBox);
			Dialog.EnumInfo.Initialize(global::_003CModule_003E.arValueCast_003Cint_003E((arValue*)ptr, null), displayType, array);
			break;
		}
		case 4:
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arColor32Template_003CarColor32Base_003E obj5);
			global::_003CModule_003E.arValueCast_003Cclass_0020arColor32Template_003Cclass_0020arColor32Base_003E_0020_003E(&obj5, (arValue*)ptr, null);
			Color color = Color.FromArgb(System.Runtime.CompilerServices.Unsafe.As<arColor32Template_003CarColor32Base_003E, byte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj5, 3)), System.Runtime.CompilerServices.Unsafe.As<arColor32Template_003CarColor32Base_003E, byte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj5, 2)), System.Runtime.CompilerServices.Unsafe.As<arColor32Template_003CarColor32Base_003E, byte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj5, 1)), *(byte*)(&obj5));
			Dialog.ColorInfo.Initialize(color);
			break;
		}
		case 5:
		{
			int num = ((int*)ptr)[2];
			KitCommon.StringDisplayType display = ((num != 3 && num == 4) ? KitCommon.StringDisplayType.eFileLoader : KitCommon.StringDisplayType.eTextBox);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj4);
			arBasicString_003Cchar_002CarAllocator_003E* ptr2 = global::_003CModule_003E.arValueCast_003Cclass_0020arBasicString_003Cchar_002Cclass_0020arAllocator_003E_0020_003E(&obj4, (arValue*)ptr, null);
			string value;
			try
			{
				uint num2 = *(uint*)ptr2;
				value = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref num2 == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)num2)));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj4);
				throw;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj4);
			Dialog.StringInfo.Initialize(value, display);
			break;
		}
		case 6:
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector2Impl_003Cfloat_003E obj3);
			global::_003CModule_003E.arValueCast_003Cclass_0020arVector2Impl_003Cfloat_003E_0020_003E(&obj3, (arValue*)ptr, null);
			Dialog.Vector2Info.Initialize(*(float*)(&obj3), System.Runtime.CompilerServices.Unsafe.As<arVector2Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj3, 4)), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 16), null), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 20), null), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 24), null), units);
			break;
		}
		case 7:
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector3Impl_003Cfloat_003E obj2);
			global::_003CModule_003E.arValueCast_003Cclass_0020arVector3Impl_003Cfloat_003E_0020_003E(&obj2, (arValue*)ptr, null);
			Dialog.Vector3Info.Initialize(*(float*)(&obj2), System.Runtime.CompilerServices.Unsafe.As<arVector3Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 4)), System.Runtime.CompilerServices.Unsafe.As<arVector3Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8)), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 16), null), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 20), null), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 24), null), units);
			break;
		}
		case 8:
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector4Impl_003Cfloat_003E obj);
			global::_003CModule_003E.arValueCast_003Cclass_0020arVector4Impl_003Cfloat_003E_0020_003E(&obj, (arValue*)ptr, null);
			Dialog.Vector4Info.Initialize(*(float*)(&obj), System.Runtime.CompilerServices.Unsafe.As<arVector4Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)), System.Runtime.CompilerServices.Unsafe.As<arVector4Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)), System.Runtime.CompilerServices.Unsafe.As<arVector4Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 12)), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 16), null), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 20), null), global::_003CModule_003E.arValueCast_003Cfloat_003E((arValue*)((byte*)ptr + 24), null), units);
			break;
		}
		}
	}

	private unsafe void NewElement(object P_0, EventArgs P_1)
	{
		//IL_0113->IL0118: Incompatible stack types: I4 vs Ref
		if (MetaData == null)
		{
			return;
		}
		global::_003CModule_003E.arAutoReleasePool_002ESetup();
		Dialog.ElementListBox.SelectedItems.Clear();
		uint num = 0u;
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		sbyte* ptr = (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0M_0040PLINOBKC_0040New_003F5Element_003F_0024AA_0040);
		uint num2 = 0u;
		do
		{
			ptr++;
			num2++;
		}
		while (*ptr != 0);
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0M_0040PLINOBKC_0040New_003F5Element_003F_0024AA_0040), ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0M_0040PLINOBKC_0040New_003F5Element_003F_0024AA_0040) + num2));
		System.Runtime.CompilerServices.Unsafe.SkipInit(out mdMetaDataDetail mdMetaDataDetail2);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		while (true)
		{
			try
			{
				mdMetaDataDetail* ptr2 = global::_003CModule_003E.mdMetaDataDetail_002E_007Bctor_007D(&mdMetaDataDetail2, (EnumDataType)2);
				bool flag;
				try
				{
					*(int*)(&arValue2) = 0;
					try
					{
						flag = !global::_003CModule_003E.ktMetaDataTemplate_002EAppendMetaData(MetaData, &obj, &arValue2, ptr2);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
						throw;
					}
					if (*(int*)(&arValue2) != 0)
					{
						((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<mdMetaDataDetail*, void>)(&global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D), &mdMetaDataDetail2);
					throw;
				}
				global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D(&mdMetaDataDetail2);
				if (flag)
				{
					sbyte* ptr3 = (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0M_0040PLINOBKC_0040New_003F5Element_003F_0024AA_0040);
					uint num3 = 0u;
					do
					{
						ptr3++;
						num3++;
					}
					while (*ptr3 != 0);
					global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EAssign_003Cchar_0020const_0020_002A_003E(&obj, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0M_0040PLINOBKC_0040New_003F5Element_003F_0024AA_0040), ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0M_0040PLINOBKC_0040New_003F5Element_003F_0024AA_0040) + num3));
					global::_003CModule_003E.operator_002B_003D_003Cchar_002Cclass_0020arAllocator_003E(&obj, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_01CLKCMJKC_0040_003F5_003F_0024AA_0040));
					num++;
					global::_003CModule_003E._002B_003D(&obj, num);
					continue;
				}
				string text = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj)))));
				Dialog.ElementListBox.Items.Add(text).Selected = true;
				Dialog.ElementListBox.Select();
				TypeChanged(KitCommon.DataType.eBoolean);
				global::_003CModule_003E.arAutoReleasePool_002EShutdown();
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
				throw;
			}
			break;
		}
		arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr4 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
		if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr4));
			*(int*)ptr4 = 0;
			((int*)ptr4)[2] = 0;
			((int*)ptr4)[1] = 0;
			global::_003CModule_003E.delete(ptr4);
		}
	}

	private unsafe void RenameElement(object __unnamed000, LabelEditEventArgs args)
	{
		if (MetaData != null && !(args.Label == null))
		{
			global::_003CModule_003E.arAutoReleasePool_002ESetup();
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
			arBasicString_003Cchar_002CarAllocator_003E* ptr = StringConvert(&obj, args.Label);
			bool flag;
			try
			{
				flag = !global::_003CModule_003E.ktMetaDataTemplate_002ERenameMetaData(MetaData, (uint)args.Item, ptr);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
				throw;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
			if (flag)
			{
				args.CancelEdit = true;
			}
			global::_003CModule_003E.arAutoReleasePool_002EShutdown();
		}
	}

	private unsafe void DeleteElement(object P_0, EventArgs P_1)
	{
		if (MetaData == null)
		{
			return;
		}
		ListView.SelectedListViewItemCollection selectedItems = Dialog.ElementListBox.SelectedItems;
		if (selectedItems.Count == 0 || MessageBox.Show("Do you wish to remove the selected items?", "Remove Items", MessageBoxButtons.OKCancel) != DialogResult.OK)
		{
			return;
		}
		global::_003CModule_003E.arAutoReleasePool_002ESetup();
		IEnumerator enumerator = selectedItems.GetEnumerator();
		if (enumerator.MoveNext())
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
			do
			{
				ListViewItem listViewItem = (ListViewItem)enumerator.Current;
				arBasicString_003Cchar_002CarAllocator_003E* ptr = StringConvert(&obj, listViewItem.Text);
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ERemoveMetaData(MetaData, ptr);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
					throw;
				}
				arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr2 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
				if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
				{
					global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr2));
					*(int*)ptr2 = 0;
					((int*)ptr2)[2] = 0;
					((int*)ptr2)[1] = 0;
					global::_003CModule_003E.delete(ptr2);
				}
				*(int*)(&obj) = 0;
				Dialog.ElementListBox.Items.Remove(listViewItem);
			}
			while (enumerator.MoveNext());
		}
		global::_003CModule_003E.arAutoReleasePool_002EShutdown();
	}

	private unsafe void HandleDeleteKey(object __unnamed000, KeyEventArgs args)
	{
		if (MetaData != null && (args.KeyCode == Keys.Delete || args.KeyCode == Keys.Back))
		{
			DeleteElement(null, null);
		}
	}

	private unsafe void MoveElement(int oldIndex, int newIndex)
	{
		ktMetaDataTemplate* metaData = MetaData;
		if (metaData != null)
		{
			global::_003CModule_003E.ktMetaDataTemplate_002EMoveMetaData(metaData, (uint)oldIndex, (uint)newIndex);
		}
	}

	private unsafe void TypeChanged(KitCommon.DataType dataType)
	{
		if (MetaData == null)
		{
			return;
		}
		ListView.SelectedIndexCollection selectedIndices = Dialog.ElementListBox.SelectedIndices;
		if (selectedIndices.Count != 1)
		{
			return;
		}
		int num = selectedIndices[0];
		if (num == -1)
		{
			return;
		}
		mdMetaDataDetail* oldDetail = global::_003CModule_003E.ktMetaDataTemplate_002EGetDetail(MetaData, (uint)num);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out mdMetaDataDetail mdMetaDataDetail2);
		global::_003CModule_003E.mdMetaDataDetail_002E_007Bctor_007D(&mdMetaDataDetail2, (EnumDataType)0);
		try
		{
			switch (dataType)
			{
			case KitCommon.DataType.eInteger:
			{
				int num4 = 0;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue9);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cint_003E(&arValue9, &num4);
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, &arValue9);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue9);
					throw;
				}
				if (*(int*)(&arValue9) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue9)))))((IntPtr)(*(int*)(&arValue9)), 1u);
				}
				*(int*)(&mdMetaDataDetail2) = 0;
				int num5 = 0;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue10);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cint_003E(&arValue10, &num5);
				try
				{
					global::_003CModule_003E.mdMetaDataDetail_002ESetMinValue(&mdMetaDataDetail2, &arValue10);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue10);
					throw;
				}
				if (*(int*)(&arValue10) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue10)))))((IntPtr)(*(int*)(&arValue10)), 1u);
				}
				int num6 = 1000;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue11);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cint_003E(&arValue11, &num6);
				try
				{
					global::_003CModule_003E.mdMetaDataDetail_002ESetMaxValue(&mdMetaDataDetail2, &arValue11);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue11);
					throw;
				}
				if (*(int*)(&arValue11) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue11)))))((IntPtr)(*(int*)(&arValue11)), 1u);
				}
				int num7 = 1;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue12);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cint_003E(&arValue12, &num7);
				try
				{
					global::_003CModule_003E.mdMetaDataDetail_002ESetStepValue(&mdMetaDataDetail2, &arValue12);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue12);
					throw;
				}
				if (*(int*)(&arValue12) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue12)))))((IntPtr)(*(int*)(&arValue12)), 1u);
				}
				break;
			}
			case KitCommon.DataType.eFloat:
			{
				float num3 = 0f;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue7);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cfloat_003E(&arValue7, &num3);
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, &arValue7);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue7);
					throw;
				}
				if (*(int*)(&arValue7) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue7)))))((IntPtr)(*(int*)(&arValue7)), 1u);
				}
				*(int*)(&mdMetaDataDetail2) = 1;
				InitFloatDetails(&mdMetaDataDetail2, oldDetail);
				break;
			}
			case KitCommon.DataType.eBoolean:
			{
				bool flag = false;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue13);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cbool_003E(&arValue13, &flag);
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, &arValue13);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue13);
					throw;
				}
				if (*(int*)(&arValue13) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue13)))))((IntPtr)(*(int*)(&arValue13)), 1u);
				}
				*(int*)(&mdMetaDataDetail2) = 2;
				break;
			}
			case KitCommon.DataType.eEnumeration:
			{
				int num2 = 0;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue6);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cint_003E(&arValue6, &num2);
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, &arValue6);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue6);
					throw;
				}
				if (*(int*)(&arValue6) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue6)))))((IntPtr)(*(int*)(&arValue6)), 1u);
				}
				*(int*)(&mdMetaDataDetail2) = 3;
				System.Runtime.CompilerServices.Unsafe.As<mdMetaDataDetail, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref mdMetaDataDetail2, 4)) = 2;
				break;
			}
			case KitCommon.DataType.eColor:
			{
				arColor32Template_003CarColor32Base_003E.EnumColors enumColors = (arColor32Template_003CarColor32Base_003E.EnumColors)(-1);
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue3);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cenum_0020arColor32Template_003Cclass_0020arColor32Base_003E_003A_003AEnumColors_003E(&arValue3, &enumColors);
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, &arValue3);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue3);
					throw;
				}
				if (*(int*)(&arValue3) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue3)))))((IntPtr)(*(int*)(&arValue3)), 1u);
				}
				*(int*)(&mdMetaDataDetail2) = 4;
				break;
			}
			case KitCommon.DataType.eString:
			{
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue5);
				global::_003CModule_003E.arValue_002E_007Bctor_007D(&arValue5, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_00CNPNBAHC_0040_003F_0024AA_0040));
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, &arValue5);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue5);
					throw;
				}
				if (*(int*)(&arValue5) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue5)))))((IntPtr)(*(int*)(&arValue5)), 1u);
				}
				*(int*)(&mdMetaDataDetail2) = 5;
				System.Runtime.CompilerServices.Unsafe.As<mdMetaDataDetail, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref mdMetaDataDetail2, 4)) = 3;
				break;
			}
			case KitCommon.DataType.eVector2:
			{
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue8);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cclass_0020arVector2Impl_003Cfloat_003E_0020_003E(&arValue8, (arVector2Impl_003Cfloat_003E*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FZERO_0040_003F_0024arVector2Impl_0040M_0040_00402V1_0040B));
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, &arValue8);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue8);
					throw;
				}
				if (*(int*)(&arValue8) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue8)))))((IntPtr)(*(int*)(&arValue8)), 1u);
				}
				*(int*)(&mdMetaDataDetail2) = 6;
				InitFloatDetails(&mdMetaDataDetail2, oldDetail);
				break;
			}
			case KitCommon.DataType.eVector3:
			{
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue4);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cclass_0020arVector3Impl_003Cfloat_003E_0020_003E(&arValue4, (arVector3Impl_003Cfloat_003E*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FZERO_0040_003F_0024arVector3Impl_0040M_0040_00402V1_0040B));
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, &arValue4);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue4);
					throw;
				}
				if (*(int*)(&arValue4) != 0)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue4)))))((IntPtr)(*(int*)(&arValue4)), 1u);
				}
				*(int*)(&mdMetaDataDetail2) = 7;
				InitFloatDetails(&mdMetaDataDetail2, oldDetail);
				break;
			}
			case KitCommon.DataType.eVector4:
			{
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
				global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cclass_0020arVector4Impl_003Cfloat_003E_0020_003E(&arValue2, (arVector4Impl_003Cfloat_003E*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FZERO_0040_003F_0024arVector4Impl_0040M_0040_00402V1_0040B));
				try
				{
					global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, &arValue2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
					throw;
				}
				global::_003CModule_003E.arValue_002E_007Bdtor_007D(&arValue2);
				*(int*)(&mdMetaDataDetail2) = 8;
				InitFloatDetails(&mdMetaDataDetail2, oldDetail);
				break;
			}
			}
			global::_003CModule_003E.ktMetaDataTemplate_002ESetDetail(MetaData, (uint)num, &mdMetaDataDetail2);
			LoadElement(num);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<mdMetaDataDetail*, void>)(&global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D), &mdMetaDataDetail2);
			throw;
		}
		global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D(&mdMetaDataDetail2);
	}

	private unsafe void BoolChanged([MarshalAs(UnmanagedType.U1)] bool value)
	{
		arValueStorage_003Cbool_002Cvoid_003E* ptr = (arValueStorage_003Cbool_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cbool_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040_NX_0040_00406B_0040);
					((sbyte*)ptr)[4] = (value ? ((sbyte)1) : ((sbyte)0));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetValue(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (*(int*)(&arValue2) != 0)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
		}
	}

	private unsafe void ColorChanged(Color color)
	{
		int a = color.A;
		int b = color.B;
		int g = color.G;
		int r = color.R;
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arColor32Template_003CarColor32Base_003E obj);
		global::_003CModule_003E.arColor32Template_003CarColor32Base_003E_002ESet(&obj, r, g, b, a);
		arValueStorage_003CarColor32Template_003CarColor32Base_003E_002Cvoid_003E* ptr = (arValueStorage_003CarColor32Template_003CarColor32Base_003E_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003CarColor32Template_003CarColor32Base_003E_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040V_003F_0024arColor32Template_0040VarColor32Base_0040_0040_0040_0040X_0040_00406B_0040);
					// IL cpblk instruction
					System.Runtime.CompilerServices.Unsafe.CopyBlock((byte*)ptr + 4, ref obj, 4);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetValue(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (*(int*)(&arValue2) != 0)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
		}
	}

	private unsafe void EnumChanged(int index)
	{
		arValueStorage_003Cint_002Cvoid_003E* ptr = (arValueStorage_003Cint_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cint_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040HX_0040_00406B_0040);
					((int*)ptr)[1] = index;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetValue(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (*(int*)(&arValue2) != 0)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
		}
	}

	private unsafe void FloatChanged(float x)
	{
		arValueStorage_003Cfloat_002Cvoid_003E* ptr = (arValueStorage_003Cfloat_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cfloat_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040MX_0040_00406B_0040);
					((float*)ptr)[1] = x;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetValue(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (*(int*)(&arValue2) != 0)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
		}
	}

	private unsafe void IntChanged(int i)
	{
		arValueStorage_003Cint_002Cvoid_003E* ptr = (arValueStorage_003Cint_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cint_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040HX_0040_00406B_0040);
					((int*)ptr)[1] = i;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetValue(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (*(int*)(&arValue2) != 0)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
		}
	}

	private unsafe void Vector2Changed(float x, float y)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector2Impl_003Cfloat_003E obj);
		*(float*)(&obj) = x;
		System.Runtime.CompilerServices.Unsafe.As<arVector2Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) = y;
		arValueStorage_003CarVector2Impl_003Cfloat_003E_002Cvoid_003E* ptr = (arValueStorage_003CarVector2Impl_003Cfloat_003E_002Cvoid_003E*)global::_003CModule_003E.@new(12u);
		arValueStorage_003CarVector2Impl_003Cfloat_003E_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040V_003F_0024arVector2Impl_0040M_0040_0040X_0040_00406B_0040);
					// IL cpblk instruction
					System.Runtime.CompilerServices.Unsafe.CopyBlock((byte*)ptr + 4, ref obj, 8);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetValue(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (*(int*)(&arValue2) != 0)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
		}
	}

	private unsafe void Vector3Changed(float x, float y, float z)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector3Impl_003Cfloat_003E obj);
		*(float*)(&obj) = x;
		System.Runtime.CompilerServices.Unsafe.As<arVector3Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) = y;
		System.Runtime.CompilerServices.Unsafe.As<arVector3Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) = z;
		arValueStorage_003CarVector3Impl_003Cfloat_003E_002Cvoid_003E* ptr = (arValueStorage_003CarVector3Impl_003Cfloat_003E_002Cvoid_003E*)global::_003CModule_003E.@new(16u);
		arValueStorage_003CarVector3Impl_003Cfloat_003E_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040V_003F_0024arVector3Impl_0040M_0040_0040X_0040_00406B_0040);
					// IL cpblk instruction
					System.Runtime.CompilerServices.Unsafe.CopyBlock((byte*)ptr + 4, ref obj, 12);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetValue(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (*(int*)(&arValue2) != 0)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
		}
	}

	private unsafe void Vector4Changed(float x, float y, float z, float w)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector4Impl_003Cfloat_003E obj);
		*(float*)(&obj) = x;
		System.Runtime.CompilerServices.Unsafe.As<arVector4Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) = y;
		System.Runtime.CompilerServices.Unsafe.As<arVector4Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) = z;
		System.Runtime.CompilerServices.Unsafe.As<arVector4Impl_003Cfloat_003E, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 12)) = w;
		arValueStorage_003CarVector4Impl_003Cfloat_003E_002Cvoid_003E* ptr = (arValueStorage_003CarVector4Impl_003Cfloat_003E_002Cvoid_003E*)global::_003CModule_003E.@new(20u);
		arValueStorage_003CarVector4Impl_003Cfloat_003E_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040V_003F_0024arVector4Impl_0040M_0040_0040X_0040_00406B_0040);
					// IL cpblk instruction
					System.Runtime.CompilerServices.Unsafe.CopyBlock((byte*)ptr + 4, ref obj, 16);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetValue(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (*(int*)(&arValue2) != 0)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
		}
	}

	private unsafe void StringChanged(string str)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		arBasicString_003Cchar_002CarAllocator_003E* value = StringConvert(&obj, str);
		try
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
			global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cclass_0020arBasicString_003Cchar_002Cclass_0020arAllocator_003E_0020_003E(&arValue2, value);
			try
			{
				SetValue(&arValue2);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
				throw;
			}
			if (*(int*)(&arValue2) != 0)
			{
				((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue2)))))((IntPtr)(*(int*)(&arValue2)), 1u);
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
		if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr));
			*(int*)ptr = 0;
			((int*)ptr)[2] = 0;
			((int*)ptr)[1] = 0;
			global::_003CModule_003E.delete(ptr);
		}
	}

	private unsafe void MinFloatChanged(float x)
	{
		arValueStorage_003Cfloat_002Cvoid_003E* ptr = (arValueStorage_003Cfloat_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cfloat_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040MX_0040_00406B_0040);
					((float*)ptr)[1] = x;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetMin(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, 1u);
		}
	}

	private unsafe void MaxFloatChanged(float x)
	{
		arValueStorage_003Cfloat_002Cvoid_003E* ptr = (arValueStorage_003Cfloat_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cfloat_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040MX_0040_00406B_0040);
					((float*)ptr)[1] = x;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetMax(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, 1u);
		}
	}

	private unsafe void StepFloatChanged(float x)
	{
		arValueStorage_003Cfloat_002Cvoid_003E* ptr = (arValueStorage_003Cfloat_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cfloat_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040MX_0040_00406B_0040);
					((float*)ptr)[1] = x;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetStep(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, 1u);
		}
	}

	private unsafe void MinIntChanged(int i)
	{
		arValueStorage_003Cint_002Cvoid_003E* ptr = (arValueStorage_003Cint_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cint_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040HX_0040_00406B_0040);
					((int*)ptr)[1] = i;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetMin(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, 1u);
		}
	}

	private unsafe void MaxIntChanged(int i)
	{
		arValueStorage_003Cint_002Cvoid_003E* ptr = (arValueStorage_003Cint_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cint_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040HX_0040_00406B_0040);
					((int*)ptr)[1] = i;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetMax(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, 1u);
		}
	}

	private unsafe void StepIntChanged(int i)
	{
		arValueStorage_003Cint_002Cvoid_003E* ptr = (arValueStorage_003Cint_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cint_002Cvoid_003E* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040HX_0040_00406B_0040);
					((int*)ptr)[1] = i;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr);
					throw;
				}
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
		*(int*)(&arValue2) = (int)ptr2;
		try
		{
			SetStep(&arValue2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
			throw;
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, 1u);
		}
	}

	private unsafe void EnumListChanged(string[] values)
	{
		if (MetaData == null)
		{
			return;
		}
		ListView.SelectedIndexCollection selectedIndices = Dialog.ElementListBox.SelectedIndices;
		if (selectedIndices.Count != 1)
		{
			return;
		}
		int num = selectedIndices[0];
		if (num == -1)
		{
			return;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) = 0;
		try
		{
			global::_003CModule_003E.arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EReserve(&obj, (uint)values.Length);
			int num2 = 0;
			if (0 < (nint)values.LongLength)
			{
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
				arBasicString_003Cchar_002CarAllocator_003E* ptr = &obj2;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E obj3);
				do
				{
					arBasicString_003Cchar_002CarAllocator_003E* second = StringConvert(&obj2, values[num2]);
					arPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E* value = global::_003CModule_003E.arMakePair_003Cint_002Cclass_0020arBasicString_003Cchar_002Cclass_0020arAllocator_003E_0020_003E(&obj3, num2, second);
					try
					{
						global::_003CModule_003E.arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EPushBack(&obj, value);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E*, void>)(&global::_003CModule_003E.arPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002E_007Bdtor_007D), &obj3);
						throw;
					}
					global::_003CModule_003E.arPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002E_007Bdtor_007D(&obj3);
					num2++;
				}
				while (num2 < (nint)values.LongLength);
			}
			mdMetaDataDetail* ptr2 = global::_003CModule_003E.ktMetaDataTemplate_002EGetDetail(MetaData, (uint)num);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out mdMetaDataDetail mdMetaDataDetail2);
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bctor_007D(&mdMetaDataDetail2, ptr2);
			try
			{
				global::_003CModule_003E.arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EAssign_003Cclass_0020arPair_003Cint_002Cclass_0020arBasicString_003Cchar_002Cclass_0020arAllocator_003E_0020_003E_0020const_0020_002A_003E((arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref mdMetaDataDetail2, 28)), (arPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E*)(int)(*(uint*)(&obj)), (arPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E*)(System.Runtime.CompilerServices.Unsafe.As<arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) * 12 + *(int*)(&obj)));
				global::_003CModule_003E.ktMetaDataTemplate_002ESetDetail(MetaData, (uint)num, &mdMetaDataDetail2);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<mdMetaDataDetail*, void>)(&global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D), &mdMetaDataDetail2);
				throw;
			}
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D(&mdMetaDataDetail2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arVector_003CarPair_003Cint_002CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EResetMemory(&obj);
	}

	private void EnumDisplayTypeChanged(KitCommon.EnumDisplayType display)
	{
		switch (display)
		{
		default:
			SetDisplayHint((EnumValueEditorHint)2);
			break;
		case KitCommon.EnumDisplayType.eRadioButtons:
			SetDisplayHint((EnumValueEditorHint)1);
			break;
		case KitCommon.EnumDisplayType.eComboBox:
			SetDisplayHint((EnumValueEditorHint)2);
			break;
		}
	}

	private void StringDisplayTypeChanged(KitCommon.StringDisplayType display)
	{
		switch (display)
		{
		default:
			SetDisplayHint((EnumValueEditorHint)3);
			break;
		case KitCommon.StringDisplayType.eFileLoader:
			SetDisplayHint((EnumValueEditorHint)4);
			break;
		case KitCommon.StringDisplayType.eTextBox:
			SetDisplayHint((EnumValueEditorHint)3);
			break;
		}
	}

	private void UnitsChanged(KitCommon.Units units)
	{
		switch (units)
		{
		case KitCommon.Units.eNone:
			SetUnits((EnumUnit)0);
			break;
		case KitCommon.Units.eAngle:
			SetUnits((EnumUnit)1);
			break;
		case KitCommon.Units.eMetric:
			SetUnits((EnumUnit)2);
			break;
		case KitCommon.Units.ePercent:
			SetUnits((EnumUnit)3);
			break;
		}
	}

	private unsafe void SetValue(arValue* value)
	{
		if (MetaData == null)
		{
			return;
		}
		ListView.SelectedIndexCollection selectedIndices = Dialog.ElementListBox.SelectedIndices;
		if (selectedIndices.Count == 1)
		{
			int num = selectedIndices[0];
			if (num != -1)
			{
				global::_003CModule_003E.ktMetaDataTemplate_002ESetValue(MetaData, (uint)num, value);
			}
		}
	}

	private unsafe void SetMin(arValue* value)
	{
		if (MetaData == null)
		{
			return;
		}
		ListView.SelectedIndexCollection selectedIndices = Dialog.ElementListBox.SelectedIndices;
		if (selectedIndices.Count != 1)
		{
			return;
		}
		int num = selectedIndices[0];
		if (num != -1)
		{
			mdMetaDataDetail* ptr = global::_003CModule_003E.ktMetaDataTemplate_002EGetDetail(MetaData, (uint)num);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out mdMetaDataDetail mdMetaDataDetail2);
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bctor_007D(&mdMetaDataDetail2, ptr);
			try
			{
				global::_003CModule_003E.mdMetaDataDetail_002ESetMinValue(&mdMetaDataDetail2, value);
				global::_003CModule_003E.ktMetaDataTemplate_002ESetDetail(MetaData, (uint)num, &mdMetaDataDetail2);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<mdMetaDataDetail*, void>)(&global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D), &mdMetaDataDetail2);
				throw;
			}
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D(&mdMetaDataDetail2);
		}
	}

	private unsafe void SetMax(arValue* value)
	{
		if (MetaData == null)
		{
			return;
		}
		ListView.SelectedIndexCollection selectedIndices = Dialog.ElementListBox.SelectedIndices;
		if (selectedIndices.Count != 1)
		{
			return;
		}
		int num = selectedIndices[0];
		if (num != -1)
		{
			mdMetaDataDetail* ptr = global::_003CModule_003E.ktMetaDataTemplate_002EGetDetail(MetaData, (uint)num);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out mdMetaDataDetail mdMetaDataDetail2);
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bctor_007D(&mdMetaDataDetail2, ptr);
			try
			{
				global::_003CModule_003E.mdMetaDataDetail_002ESetMaxValue(&mdMetaDataDetail2, value);
				global::_003CModule_003E.ktMetaDataTemplate_002ESetDetail(MetaData, (uint)num, &mdMetaDataDetail2);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<mdMetaDataDetail*, void>)(&global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D), &mdMetaDataDetail2);
				throw;
			}
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D(&mdMetaDataDetail2);
		}
	}

	private unsafe void SetStep(arValue* value)
	{
		if (MetaData == null)
		{
			return;
		}
		ListView.SelectedIndexCollection selectedIndices = Dialog.ElementListBox.SelectedIndices;
		if (selectedIndices.Count != 1)
		{
			return;
		}
		int num = selectedIndices[0];
		if (num != -1)
		{
			mdMetaDataDetail* ptr = global::_003CModule_003E.ktMetaDataTemplate_002EGetDetail(MetaData, (uint)num);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out mdMetaDataDetail mdMetaDataDetail2);
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bctor_007D(&mdMetaDataDetail2, ptr);
			try
			{
				global::_003CModule_003E.mdMetaDataDetail_002ESetStepValue(&mdMetaDataDetail2, value);
				global::_003CModule_003E.ktMetaDataTemplate_002ESetDetail(MetaData, (uint)num, &mdMetaDataDetail2);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<mdMetaDataDetail*, void>)(&global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D), &mdMetaDataDetail2);
				throw;
			}
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D(&mdMetaDataDetail2);
		}
	}

	private unsafe void SetUnits(EnumUnit unit)
	{
		if (MetaData == null)
		{
			return;
		}
		ListView.SelectedIndexCollection selectedIndices = Dialog.ElementListBox.SelectedIndices;
		if (selectedIndices.Count != 1)
		{
			return;
		}
		int num = selectedIndices[0];
		if (num != -1)
		{
			mdMetaDataDetail* ptr = global::_003CModule_003E.ktMetaDataTemplate_002EGetDetail(MetaData, (uint)num);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out mdMetaDataDetail mdMetaDataDetail2);
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bctor_007D(&mdMetaDataDetail2, ptr);
			try
			{
				System.Runtime.CompilerServices.Unsafe.As<mdMetaDataDetail, EnumUnit>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref mdMetaDataDetail2, 8)) = unit;
				global::_003CModule_003E.ktMetaDataTemplate_002ESetDetail(MetaData, (uint)num, &mdMetaDataDetail2);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<mdMetaDataDetail*, void>)(&global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D), &mdMetaDataDetail2);
				throw;
			}
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D(&mdMetaDataDetail2);
		}
	}

	private unsafe void SetDisplayHint(EnumValueEditorHint hint)
	{
		if (MetaData == null)
		{
			return;
		}
		ListView.SelectedIndexCollection selectedIndices = Dialog.ElementListBox.SelectedIndices;
		if (selectedIndices.Count != 1)
		{
			return;
		}
		int num = selectedIndices[0];
		if (num != -1)
		{
			mdMetaDataDetail* ptr = global::_003CModule_003E.ktMetaDataTemplate_002EGetDetail(MetaData, (uint)num);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out mdMetaDataDetail mdMetaDataDetail2);
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bctor_007D(&mdMetaDataDetail2, ptr);
			try
			{
				System.Runtime.CompilerServices.Unsafe.As<mdMetaDataDetail, EnumValueEditorHint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref mdMetaDataDetail2, 4)) = hint;
				global::_003CModule_003E.ktMetaDataTemplate_002ESetDetail(MetaData, (uint)num, &mdMetaDataDetail2);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<mdMetaDataDetail*, void>)(&global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D), &mdMetaDataDetail2);
				throw;
			}
			global::_003CModule_003E.mdMetaDataDetail_002E_007Bdtor_007D(&mdMetaDataDetail2);
		}
	}

	private unsafe void InitFloatDetails(mdMetaDataDetail* newDetail, mdMetaDataDetail* oldDetail)
	{
		//IL_0131->IL0136: Incompatible stack types: I vs Ref
		//IL_0321->IL0326: Incompatible stack types: I vs Ref
		//IL_047f->IL0484: Incompatible stack types: I vs Ref
		EnumUnit enumUnit = ((EnumUnit*)oldDetail)[2];
		((int*)newDetail)[2] = (int)enumUnit;
		float num = -1000f;
		float num2 = 1000f;
		float num3 = 0.1f;
		int num4 = *(int*)oldDetail;
		EnumDataType enumDataType = (EnumDataType)num4;
		if (enumDataType != (EnumDataType)1)
		{
			EnumDataType enumDataType2 = (EnumDataType)num4;
			if (enumDataType2 != (EnumDataType)6)
			{
				EnumDataType enumDataType3 = (EnumDataType)num4;
				if (enumDataType3 != (EnumDataType)7)
				{
					EnumDataType enumDataType4 = (EnumDataType)num4;
					if (enumDataType4 != (EnumDataType)8)
					{
						System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue2);
						global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cfloat_003E(&arValue2, &num);
						try
						{
							global::_003CModule_003E.mdMetaDataDetail_002ESetMinValue(newDetail, &arValue2);
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue2);
							throw;
						}
						global::_003CModule_003E.arValue_002E_007Bdtor_007D(&arValue2);
						System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue3);
						global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cfloat_003E(&arValue3, &num2);
						try
						{
							global::_003CModule_003E.mdMetaDataDetail_002ESetMaxValue(newDetail, &arValue3);
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue3);
							throw;
						}
						global::_003CModule_003E.arValue_002E_007Bdtor_007D(&arValue3);
						System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue4);
						global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cfloat_003E(&arValue4, &num3);
						try
						{
							global::_003CModule_003E.mdMetaDataDetail_002ESetStepValue(newDetail, &arValue4);
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue4);
							throw;
						}
						global::_003CModule_003E.arValue_002E_007Bdtor_007D(&arValue4);
						return;
					}
				}
			}
		}
		arValue* ptr = (arValue*)((byte*)oldDetail + 12);
		if ((global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_004051 & 1) == 0)
		{
			global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_004051 |= 1u;
			try
			{
				global::_003CModule_003E._003Finstance_0040_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_00404PAV3_0040A = global::_003CModule_003E.arSingleton_003CarValueConversionTable_003E_002ECreateInstance();
			}
			catch
			{
				//try-fault
				global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_004051 &= 4294967294u;
				throw;
			}
		}
		arValueConversionTable* ptr2 = global::_003CModule_003E._003Finstance_0040_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_00404PAV3_0040A;
		bool flag = false;
		uint num5 = *(uint*)ptr;
		ref arType_003Cvoid_003E reference;
		if (num5 != 0)
		{
			reference = ref *(_003F*)((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, arTypeElement*>)(int)(*(uint*)(*(int*)(int)num5 + 12)))((IntPtr)(int)num5);
		}
		else
		{
			reference = ref global::_003CModule_003E._003FId_0040_003F_0024arType_0040X_0040_00402V1_0040B;
		}
		float num6;
		if (System.Runtime.CompilerServices.Unsafe.AreSame(ref System.Runtime.CompilerServices.Unsafe.As<arType_003Cvoid_003E, byte>(ref reference), ref System.Runtime.CompilerServices.Unsafe.As<arType_003Cfloat_003E, byte>(ref global::_003CModule_003E._003FId_0040_003F_0024arType_0040M_0040_00402V1_0040B)))
		{
			num6 = *(float*)(*(int*)ptr + 4);
		}
		else
		{
			num6 = global::_003CModule_003E.arValueConversionTable_002EConvertFromMappings_003Cfloat_003E(ptr2, ptr, &flag);
			if (!flag)
			{
				arValueStorage_003Cfloat_002Cvoid_003E* ptr3 = (arValueStorage_003Cfloat_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
				arValueStorage_003Cfloat_002Cvoid_003E* ptr4;
				try
				{
					if (ptr3 != null)
					{
						*(int*)ptr3 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
						try
						{
							*(int*)ptr3 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040MX_0040_00406B_0040);
							((float*)ptr3)[1] = -1000f;
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr3);
							throw;
						}
						ptr4 = ptr3;
					}
					else
					{
						ptr4 = null;
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.delete(ptr3);
					throw;
				}
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue5);
				*(int*)(&arValue5) = (int)ptr4;
				try
				{
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue6);
					*(int*)(&arValue6) = 0;
					if (ptr4 != null)
					{
						arValueStorage_003Cfloat_002Cvoid_003E* intPtr = ptr4;
						*(int*)(&arValue6) = (int)((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, arValueStorageBase*>)(int)(*(uint*)(*(int*)intPtr + 4)))((nint)intPtr);
					}
					arValueStorageBase* ptr5;
					try
					{
						ptr5 = (arValueStorageBase*)(int)((uint*)newDetail)[3];
						((int*)newDetail)[3] = *(int*)(&arValue6);
						*(int*)(&arValue6) = (int)ptr5;
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue6);
						throw;
					}
					if (ptr5 != null)
					{
						((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr5)))((nint)ptr5, 1u);
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue5);
					throw;
				}
				if (ptr4 != null)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr4)))((nint)ptr4, 1u);
				}
				goto IL_02ca;
			}
		}
		arValueStorage_003Cfloat_002Cvoid_003E* ptr6 = (arValueStorage_003Cfloat_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cfloat_002Cvoid_003E* ptr7;
		try
		{
			if (ptr6 != null)
			{
				*(int*)ptr6 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr6 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040MX_0040_00406B_0040);
					((float*)ptr6)[1] = num6;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr6);
					throw;
				}
				ptr7 = ptr6;
			}
			else
			{
				ptr7 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr6);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue7);
		*(int*)(&arValue7) = (int)ptr7;
		try
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue8);
			*(int*)(&arValue8) = 0;
			if (ptr7 != null)
			{
				arValueStorage_003Cfloat_002Cvoid_003E* intPtr2 = ptr7;
				*(int*)(&arValue8) = (int)((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, arValueStorageBase*>)(int)(*(uint*)(*(int*)intPtr2 + 4)))((nint)intPtr2);
			}
			arValueStorageBase* ptr8;
			try
			{
				ptr8 = (arValueStorageBase*)(int)((uint*)newDetail)[3];
				((int*)newDetail)[3] = *(int*)(&arValue8);
				*(int*)(&arValue8) = (int)ptr8;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue8);
				throw;
			}
			if (ptr8 != null)
			{
				((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr8)))((nint)ptr8, 1u);
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue7);
			throw;
		}
		if (ptr7 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr7)))((nint)ptr7, 1u);
		}
		goto IL_02ca;
		IL_0428:
		arValue* ptr9 = (arValue*)((byte*)oldDetail + 20);
		if ((global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_004051 & 1) == 0)
		{
			global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_004051 |= 1u;
			try
			{
				global::_003CModule_003E._003Finstance_0040_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_00404PAV3_0040A = global::_003CModule_003E.arSingleton_003CarValueConversionTable_003E_002ECreateInstance();
			}
			catch
			{
				//try-fault
				global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_004051 &= 4294967294u;
				throw;
			}
		}
		arValueConversionTable* ptr10 = global::_003CModule_003E._003Finstance_0040_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_00404PAV3_0040A;
		flag = false;
		uint num7 = *(uint*)ptr9;
		ref arType_003Cvoid_003E reference2;
		if (num7 != 0)
		{
			reference2 = ref *(_003F*)((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, arTypeElement*>)(int)(*(uint*)(*(int*)(int)num7 + 12)))((IntPtr)(int)num7);
		}
		else
		{
			reference2 = ref global::_003CModule_003E._003FId_0040_003F_0024arType_0040X_0040_00402V1_0040B;
		}
		float num8;
		if (System.Runtime.CompilerServices.Unsafe.AreSame(ref System.Runtime.CompilerServices.Unsafe.As<arType_003Cvoid_003E, byte>(ref reference2), ref System.Runtime.CompilerServices.Unsafe.As<arType_003Cfloat_003E, byte>(ref global::_003CModule_003E._003FId_0040_003F_0024arType_0040M_0040_00402V1_0040B)))
		{
			flag = true;
			num8 = *(float*)(*(int*)ptr9 + 4);
		}
		else
		{
			num8 = global::_003CModule_003E.arValueConversionTable_002EConvertFromMappings_003Cfloat_003E(ptr10, ptr9, &flag);
		}
		float num9 = num8;
		if (flag)
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue9);
			global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cfloat_003E(&arValue9, &num9);
			try
			{
				global::_003CModule_003E.mdMetaDataDetail_002ESetStepValue(newDetail, &arValue9);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue9);
				throw;
			}
			if (*(int*)(&arValue9) != 0)
			{
				((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)(int)(*(uint*)(&arValue9)))))((IntPtr)(*(int*)(&arValue9)), 1u);
			}
		}
		else
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue10);
			global::_003CModule_003E.arValue_002E_007Bctor_007D_003Cfloat_003E(&arValue10, &num3);
			try
			{
				global::_003CModule_003E.mdMetaDataDetail_002ESetStepValue(newDetail, &arValue10);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue10);
				throw;
			}
			global::_003CModule_003E.arValue_002E_007Bdtor_007D(&arValue10);
		}
		return;
		IL_02ca:
		arValue* ptr11 = (arValue*)((byte*)oldDetail + 16);
		if ((global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_004051 & 1) == 0)
		{
			global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_004051 |= 1u;
			try
			{
				global::_003CModule_003E._003Finstance_0040_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_00404PAV3_0040A = global::_003CModule_003E.arSingleton_003CarValueConversionTable_003E_002ECreateInstance();
			}
			catch
			{
				//try-fault
				global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_004051 &= 4294967294u;
				throw;
			}
		}
		arValueConversionTable* ptr12 = global::_003CModule_003E._003Finstance_0040_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VarValueConversionTable_0040_0040_0040_0040SAAAVarValueConversionTable_0040_0040XZ_00404PAV3_0040A;
		flag = false;
		uint num10 = *(uint*)ptr11;
		ref arType_003Cvoid_003E reference3;
		if (num10 != 0)
		{
			reference3 = ref *(_003F*)((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, arTypeElement*>)(int)(*(uint*)(*(int*)(int)num10 + 12)))((IntPtr)(int)num10);
		}
		else
		{
			reference3 = ref global::_003CModule_003E._003FId_0040_003F_0024arType_0040X_0040_00402V1_0040B;
		}
		float num11;
		if (System.Runtime.CompilerServices.Unsafe.AreSame(ref System.Runtime.CompilerServices.Unsafe.As<arType_003Cvoid_003E, byte>(ref reference3), ref System.Runtime.CompilerServices.Unsafe.As<arType_003Cfloat_003E, byte>(ref global::_003CModule_003E._003FId_0040_003F_0024arType_0040M_0040_00402V1_0040B)))
		{
			num11 = *(float*)(*(int*)ptr11 + 4);
		}
		else
		{
			num11 = global::_003CModule_003E.arValueConversionTable_002EConvertFromMappings_003Cfloat_003E(ptr12, ptr11, &flag);
			if (!flag)
			{
				arValueStorage_003Cfloat_002Cvoid_003E* ptr13 = (arValueStorage_003Cfloat_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
				arValueStorage_003Cfloat_002Cvoid_003E* ptr14;
				try
				{
					if (ptr13 != null)
					{
						*(int*)ptr13 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
						try
						{
							*(int*)ptr13 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040MX_0040_00406B_0040);
							((float*)ptr13)[1] = 1000f;
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr13);
							throw;
						}
						ptr14 = ptr13;
					}
					else
					{
						ptr14 = null;
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.delete(ptr13);
					throw;
				}
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue11);
				*(int*)(&arValue11) = (int)ptr14;
				try
				{
					global::_003CModule_003E.mdMetaDataDetail_002ESetMaxValue(newDetail, &arValue11);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue11);
					throw;
				}
				if (ptr14 != null)
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr14)))((nint)ptr14, 1u);
				}
				goto IL_0428;
			}
		}
		arValueStorage_003Cfloat_002Cvoid_003E* ptr15 = (arValueStorage_003Cfloat_002Cvoid_003E*)global::_003CModule_003E.@new(8u);
		arValueStorage_003Cfloat_002Cvoid_003E* ptr16;
		try
		{
			if (ptr15 != null)
			{
				*(int*)ptr15 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arValueStorageBase_0040_00406B_0040);
				try
				{
					*(int*)ptr15 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7_003F_0024arValueStorage_0040MX_0040_00406B_0040);
					((float*)ptr15)[1] = num11;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValueStorageBase*, void>)(&global::_003CModule_003E.arValueStorageBase_002E_007Bdtor_007D), ptr15);
					throw;
				}
				ptr16 = ptr15;
			}
			else
			{
				ptr16 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr15);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arValue arValue12);
		*(int*)(&arValue12) = (int)ptr16;
		try
		{
			global::_003CModule_003E.mdMetaDataDetail_002ESetMaxValue(newDetail, &arValue12);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arValue*, void>)(&global::_003CModule_003E.arValue_002E_007Bdtor_007D), &arValue12);
			throw;
		}
		if (ptr16 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(int)(*(uint*)ptr16)))((nint)ptr16, 1u);
		}
		goto IL_0428;
	}

	private int GetSelectedIndex()
	{
		ListView.SelectedIndexCollection selectedIndices = Dialog.ElementListBox.SelectedIndices;
		if (selectedIndices.Count == 1)
		{
			return selectedIndices[0];
		}
		return -1;
	}

	private unsafe arBasicString_003Cchar_002CarAllocator_003E* StringConvert(arBasicString_003Cchar_002CarAllocator_003E* P_0, string str)
	{
		uint num = 0u;
		byte[] bytes = new ASCIIEncoding().GetBytes(str);
		if (bytes.Length == 0)
		{
			*(int*)P_0 = 0;
			try
			{
				num = 1u;
				return P_0;
			}
			catch
			{
				//try-fault
				if ((num & 1) != 0)
				{
					num &= 0xFFFFFFFEu;
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), P_0);
				}
				throw;
			}
		}
		fixed (byte* ptr = &bytes[0])
		{
			sbyte* ptr2 = (sbyte*)ptr;
			*(int*)P_0 = 0;
			uint num2;
			if (ptr2 == null)
			{
				num2 = 0u;
			}
			else
			{
				uint num3 = 0u;
				if (*ptr2 != 0)
				{
					do
					{
						num3++;
					}
					while (*((int)num3 + ptr2) != 0);
				}
				num2 = num3;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(P_0, null, ptr2, (int)num2 + ptr2);
			try
			{
				num = 1u;
				return P_0;
			}
			catch
			{
				//try-fault
				if ((num & 1) != 0)
				{
					num &= 0xFFFFFFFEu;
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), P_0);
				}
				throw;
			}
		}
	}
}
