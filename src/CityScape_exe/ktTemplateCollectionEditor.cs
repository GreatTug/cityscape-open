using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using _003CCppImplementationDetails_003E;
using ktKitDotNet;

internal unsafe class ktTemplateCollectionEditor(ktTemplateCollection* templateCollection)
{
	private MetaDataTemplateEditor Dialog;

	private ktTemplateEditor TemplateEditor = new ktTemplateEditor();

	private unsafe ktTemplateCollection* TemplateCollection = templateCollection;

	public void Setup(MetaDataTemplateEditor dialog)
	{
		Dialog = dialog;
		PopulateItems();
		Dialog.NewTemplateButton.Click += NewTemplate;
		Dialog.DeleteTemplateButton.Click += DeleteTemplate;
		Dialog.TemplateListBox.SelectedIndexChanged += TemplateSelectionChanged;
		Dialog.TemplateListBox.AfterLabelEdit += RenameTemplate;
		Dialog.TemplateListBox.KeyUp += HandleDeleteKey;
		TemplateEditor.Setup(dialog);
	}

	private unsafe void PopulateItems()
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E.ConstIterator constIterator);
		global::_003CModule_003E.arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EBegin((arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*)global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection), &constIterator);
		arForEachAnyType_003CarHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_003A_003AConstIterator_003E obj = (arForEachAnyType_003CarHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_003A_003AConstIterator_003E)constIterator;
		global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection);
		while (true)
		{
			global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection);
			if ((byte)((System.Runtime.CompilerServices.Unsafe.As<arForEachAnyType_003CarHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_003A_003AConstIterator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) != 0) ? 1u : 0u) != 0)
			{
				global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection);
				arBasicString_003Cchar_002CarAllocator_003E* ptr = (arBasicString_003Cchar_002CarAllocator_003E*)(System.Runtime.CompilerServices.Unsafe.As<arForEachAnyType_003CarHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_003A_003AConstIterator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) + 4);
				Dialog.TemplateListBox.Items.Add(new string(global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002ECStr(ptr)));
				global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection);
				global::_003CModule_003E.arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EConstIterator_002E_002B_002B((arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E.ConstIterator*)(&obj));
				continue;
			}
			break;
		}
	}

	private unsafe void NewTemplate(object P_0, EventArgs P_1)
	{
		//IL_00f9->IL00fe: Incompatible stack types: I4 vs Ref
		global::_003CModule_003E.arAutoReleasePool_002ESetup();
		Dialog.TemplateListBox.SelectedItems.Clear();
		uint num = 0u;
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		sbyte* ptr = (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0O_0040BPMEMLNL_0040New_003F5Meta_003F5Data_003F_0024AA_0040);
		uint num2 = 0u;
		do
		{
			ptr++;
			num2++;
		}
		while (*ptr != 0);
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0O_0040BPMEMLNL_0040New_003F5Meta_003F5Data_003F_0024AA_0040), (sbyte*)((ref *(_003F*)num2) + (ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY0O_0040_0024_0024CBD, _003F>(ref global::_003CModule_003E._003F_003F_C_0040_0O_0040BPMEMLNL_0040New_003F5Meta_003F5Data_003F_0024AA_0040))));
		try
		{
			ktMetaDataTemplate* ptr2 = (ktMetaDataTemplate*)global::_003CModule_003E.@new(112u);
			ktMetaDataTemplate* ptr3;
			try
			{
				ptr3 = ((ptr2 == null) ? null : global::_003CModule_003E.ktMetaDataTemplate_002E_007Bctor_007D(ptr2));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr2);
				throw;
			}
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(int)(*(uint*)ptr3)))((nint)ptr3);
			if (!global::_003CModule_003E.ktTemplateCollection_002EAddTemplate(TemplateCollection, &obj, ptr3))
			{
				do
				{
					sbyte* ptr4 = (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0O_0040BPMEMLNL_0040New_003F5Meta_003F5Data_003F_0024AA_0040);
					uint num3 = 0u;
					do
					{
						ptr4++;
						num3++;
					}
					while (*ptr4 != 0);
					global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EAssign_003Cchar_0020const_0020_002A_003E(&obj, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0O_0040BPMEMLNL_0040New_003F5Meta_003F5Data_003F_0024AA_0040), (sbyte*)((ref *(_003F*)num3) + (ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY0O_0040_0024_0024CBD, _003F>(ref global::_003CModule_003E._003F_003F_C_0040_0O_0040BPMEMLNL_0040New_003F5Meta_003F5Data_003F_0024AA_0040))));
					global::_003CModule_003E.operator_002B_003D_003Cchar_002Cclass_0020arAllocator_003E(&obj, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_01CLKCMJKC_0040_003F5_003F_0024AA_0040));
					num++;
					global::_003CModule_003E._002B_003D(&obj, num);
				}
				while (!global::_003CModule_003E.ktTemplateCollection_002EAddTemplate(TemplateCollection, &obj, ptr3));
			}
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)ptr3 + 4)))((nint)ptr3);
			string text = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj)))));
			Dialog.TemplateListBox.Items.Add(text).Selected = true;
			Dialog.TemplateListBox.Select();
			global::_003CModule_003E.arAutoReleasePool_002EShutdown();
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr5 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
		if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr5));
			*(int*)ptr5 = 0;
			((int*)ptr5)[2] = 0;
			((int*)ptr5)[1] = 0;
			global::_003CModule_003E.delete(ptr5);
		}
	}

	private unsafe void RenameTemplate(object __unnamed000, LabelEditEventArgs args)
	{
		if (args.Label == null)
		{
			return;
		}
		global::_003CModule_003E.arAutoReleasePool_002ESetup();
		string text = Dialog.TemplateListBox.Items[args.Item].Text;
		string label = args.Label;
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		arBasicString_003Cchar_002CarAllocator_003E* ptr = StringConvert(&obj, label);
		bool flag;
		try
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
			arBasicString_003Cchar_002CarAllocator_003E* ptr2 = StringConvert(&obj2, text);
			try
			{
				flag = !global::_003CModule_003E.ktTemplateCollection_002ERenameTemplate(TemplateCollection, ptr2, ptr);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr3 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
			if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
			{
				global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr3));
				*(int*)ptr3 = 0;
				((int*)ptr3)[2] = 0;
				((int*)ptr3)[1] = 0;
				global::_003CModule_003E.delete(ptr3);
			}
			*(int*)(&obj2) = 0;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
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
		if (flag)
		{
			args.CancelEdit = true;
		}
		global::_003CModule_003E.arAutoReleasePool_002EShutdown();
	}

	private unsafe void DeleteTemplate(object P_0, EventArgs P_1)
	{
		ListView.SelectedListViewItemCollection selectedItems = Dialog.TemplateListBox.SelectedItems;
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
					global::_003CModule_003E.ktTemplateCollection_002ERemoveTemplate(TemplateCollection, ptr);
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
				Dialog.TemplateListBox.Items.Remove(listViewItem);
			}
			while (enumerator.MoveNext());
		}
		global::_003CModule_003E.arAutoReleasePool_002EShutdown();
	}

	private unsafe void TemplateSelectionChanged(object P_0, EventArgs P_1)
	{
		ListView.SelectedListViewItemCollection selectedItems = Dialog.TemplateListBox.SelectedItems;
		if (selectedItems.Count == 1)
		{
			ListViewItem listViewItem = selectedItems[0];
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
			arBasicString_003Cchar_002CarAllocator_003E* ptr = StringConvert(&obj, listViewItem.Text);
			ktMetaDataTemplate* metaDataTemplate;
			try
			{
				metaDataTemplate = global::_003CModule_003E.ktTemplateCollection_002EGetTemplate(TemplateCollection, ptr);
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
			TemplateEditor.SetMetaDataTemplate(metaDataTemplate);
		}
		else
		{
			TemplateEditor.SetMetaDataTemplate(null);
		}
	}

	private void HandleDeleteKey(object __unnamed000, KeyEventArgs args)
	{
		if (args.KeyCode == Keys.Delete || args.KeyCode == Keys.Back)
		{
			DeleteTemplate(null, null);
		}
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
