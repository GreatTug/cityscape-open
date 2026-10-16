using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using edEditorDotNet;

internal unsafe class edAssignMetaDataTemplatesImpl(ktTemplateCollection* templateCollection, arHashSet_003CktMetaDataTemplate_0020const_0020_002A_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E* assignedTemplates)
{
	private object Dialog;

	private unsafe ktTemplateCollection* TemplateCollection = templateCollection;

	private unsafe arHashSet_003CktMetaDataTemplate_0020const_0020_002A_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E* AssignedTemplates = assignedTemplates;

	public void Setup(object dialog)
	{
		Dialog = dialog;
		AssignMetaDataTemplates assignMetaDataTemplates = (AssignMetaDataTemplates)dialog;
		PopulateItems();
		assignMetaDataTemplates.AddTemplatesButton.Click += AddTemplates;
		assignMetaDataTemplates.RemoveTemplatesButton.Click += RemoveTemplates;
		assignMetaDataTemplates.AssignedTemplatesList.KeyUp += HandleDeleteKey;
	}

	private unsafe void PopulateItems()
	{
		AssignMetaDataTemplates assignMetaDataTemplates = (AssignMetaDataTemplates)Dialog;
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E.ConstIterator constIterator);
		global::_003CModule_003E.arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EBegin((arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*)global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection), &constIterator);
		arForEachAnyType_003CarHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_003A_003AConstIterator_003E obj = (arForEachAnyType_003CarHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_003A_003AConstIterator_003E)constIterator;
		global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection);
		while (true)
		{
			global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection);
			if ((byte)((System.Runtime.CompilerServices.Unsafe.As<arForEachAnyType_003CarHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_003A_003AConstIterator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) != 0) ? 1 : 0) == 0)
			{
				break;
			}
			global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection);
			arBasicString_003Cchar_002CarAllocator_003E* ptr = (arBasicString_003Cchar_002CarAllocator_003E*)(System.Runtime.CompilerServices.Unsafe.As<arForEachAnyType_003CarHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_003A_003AConstIterator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) + 4);
			assignMetaDataTemplates.TemplateLibraryList.Items.Add(new string(global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002ECStr(ptr)));
			global::_003CModule_003E.ktTemplateCollection_002EGetTemplates(TemplateCollection);
			global::_003CModule_003E.arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EConstIterator_002E_002B_002B((arHashTable_003CarPair_003CarBasicString_003Cchar_002CarAllocator_003E_0020const_0020_002CktMetaDataTemplate_0020_002A_003E_002Cchar_0020const_0020_002A_002CarStringPairKey_003CarAllocator_002CktMetaDataTemplate_0020_002A_003E_002CarHash_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarHashKeyEqual_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E.ConstIterator*)(&obj));
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E.Iterator iterator);
		arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E.Iterator* ptr2 = global::_003CModule_003E.arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E_002EBegin((arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E*)AssignedTemplates, &iterator);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arForEachAnyType_003CarHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E_003A_003AConstIterator_003E obj2);
		// IL cpblk instruction
		System.Runtime.CompilerServices.Unsafe.CopyBlock(ref obj2, ptr2, 12);
		while ((byte)((System.Runtime.CompilerServices.Unsafe.As<arForEachAnyType_003CarHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E_003A_003AConstIterator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8)) != 0) ? 1u : 0u) != 0)
		{
			uint num = *(uint*)global::_003CModule_003E.ktMetaDataTemplate_002EGetName((ktMetaDataTemplate*)(int)(*(uint*)(System.Runtime.CompilerServices.Unsafe.As<arForEachAnyType_003CarHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E_003A_003AConstIterator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8)) + 4)));
			sbyte* value = ((num == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num)));
			assignMetaDataTemplates.AssignedTemplatesList.Items.Add(new string(value));
			global::_003CModule_003E.arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E_002EConstIterator_002E_002B_002B((arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E.ConstIterator*)(&obj2));
		}
	}

	private unsafe void AddTemplates(object P_0, EventArgs P_1)
	{
		AssignMetaDataTemplates assignMetaDataTemplates = (AssignMetaDataTemplates)Dialog;
		ListView.SelectedListViewItemCollection selectedItems = assignMetaDataTemplates.TemplateLibraryList.SelectedItems;
		if (selectedItems.Count == 0)
		{
			return;
		}
		IEnumerator enumerator = selectedItems.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arPair_003CarHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E_003A_003AIterator_002Cbool_003E obj2);
		do
		{
			ListViewItem listViewItem = (ListViewItem)enumerator.Current;
			arBasicString_003Cchar_002CarAllocator_003E* ptr = StringConvert(&obj, listViewItem.Text);
			bool flag;
			try
			{
				ktMetaDataTemplate* val = global::_003CModule_003E.ktTemplateCollection_002EGetTemplate(TemplateCollection, ptr);
				flag = ((bool*)global::_003CModule_003E.arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E_002EInsert((arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E*)AssignedTemplates, &obj2, val))[12];
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
			if (flag)
			{
				assignMetaDataTemplates.AssignedTemplatesList.Items.Add((ListViewItem)listViewItem.Clone());
			}
		}
		while (enumerator.MoveNext());
	}

	private unsafe void RemoveTemplates(object P_0, EventArgs P_1)
	{
		AssignMetaDataTemplates assignMetaDataTemplates = (AssignMetaDataTemplates)Dialog;
		ListView.SelectedListViewItemCollection selectedItems = assignMetaDataTemplates.AssignedTemplatesList.SelectedItems;
		if (selectedItems.Count == 0)
		{
			return;
		}
		IEnumerator enumerator = selectedItems.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		do
		{
			ListViewItem listViewItem = (ListViewItem)enumerator.Current;
			arBasicString_003Cchar_002CarAllocator_003E* ptr = StringConvert(&obj, listViewItem.Text);
			try
			{
				ktMetaDataTemplate* key = global::_003CModule_003E.ktTemplateCollection_002EGetTemplate(TemplateCollection, ptr);
				global::_003CModule_003E.arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E_002EErase((arHashTable_003CktMetaDataTemplate_0020const_0020_002A_002CktMetaDataTemplate_0020const_0020_002A_002CarIdentity_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHash_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarHashKeyEqual_003CktMetaDataTemplate_0020const_0020_002A_003E_002CarAllocator_003E*)AssignedTemplates, key);
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
			assignMetaDataTemplates.AssignedTemplatesList.Items.Remove(listViewItem);
		}
		while (enumerator.MoveNext());
	}

	private void HandleDeleteKey(object __unnamed000, KeyEventArgs args)
	{
		if (args.KeyCode == Keys.Delete || args.KeyCode == Keys.Back)
		{
			RemoveTemplates(null, null);
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
