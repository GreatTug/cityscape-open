using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using _003CCppImplementationDetails_003E;
using palDotNet.palDialog;

internal class edGisImportBuildingsDialogManagedImpl
{
	public unsafe edGisImportBuildingsDialogManaged* Dialog;

	public GisImportBuildingsDialog ManagedDialog;

	public string BuildingsShapefilePath;

	public string BuildingsDbfFilePath;

	public string BuildingsPrjFilePath;

	public bool FilePathsAreValid;

	public edGisImportBuildingsDialogManagedImpl()
	{
		BuildingsPrjFilePath = (BuildingsDbfFilePath = (BuildingsShapefilePath = ""));
		FilePathsAreValid = false;
	}

	public void HandleOnShown(object P_0, EventArgs P_1)
	{
		if (string.IsNullOrEmpty(BuildingsShapefilePath = ManagedDialog.BuildingsShapefilePath.Path))
		{
			ManagedDialog.InvalidateBuildingsFilePath();
		}
		else
		{
			ManagedDialog.ValidateBuildingsFilePath();
		}
		SyncFilePaths();
		if (FilePathsAreValid)
		{
			SyncFieldNames();
		}
	}

	public void HandleBuildingsShapefilePathChanged(object P_0, EventArgs P_1)
	{
		string path = ManagedDialog.BuildingsShapefilePath.Path;
		if (!(BuildingsShapefilePath == path))
		{
			BuildingsShapefilePath = path;
			SyncFilePaths();
			if (FilePathsAreValid)
			{
				SyncFieldNames();
			}
		}
	}

	public unsafe void HandleBuildingTypeDataGridCellClick(object __unnamed000, DataGridViewCellEventArgs e)
	{
		//IL_00be->IL00c3: Incompatible stack types: I4 vs Ref
		DataGridView styleDataGrid = ManagedDialog.TypeStyleChooser.StyleDataGrid;
		string name = styleDataGrid.Columns[e.ColumnIndex].Name;
		string text = "ChooseStyleColumn";
		string columnName = "StyleNameColumn";
		if (!(name == text))
		{
			return;
		}
		edGisImportBuildingsDialogManaged* dialog = Dialog;
		palDialogBoxView* ptr = (palDialogBoxView*)(int)((uint*)dialog)[44];
		ifLibraryBrowser* ptr2 = (ifLibraryBrowser*)(int)((uint*)dialog)[43];
		if (ptr == null || ptr2 == null)
		{
			return;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arGuid arGuid2);
		// IL cpblk instruction
		System.Runtime.CompilerServices.Unsafe.CopyBlock(ref arGuid2, (byte*)ptr + 112, 16);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY0CF_0040D obj);
		global::_003CModule_003E.arGuid_002EToString(&arGuid2, (sbyte*)(&obj));
		Guid guid = new Guid(new string((sbyte*)(&obj)));
		DialogBoxBase dialogBoxBase = DialogBoxBase.FindByGuid(guid);
		if (dialogBoxBase == null || dialogBoxBase.ShowDialogFromParentDialog() != DialogResult.OK)
		{
			return;
		}
		ifFile* ptr3 = global::_003CModule_003E.ifLibraryBrowser_002EGetSelectedFile(ptr2);
		if (ptr3 != null)
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
			global::_003CModule_003E.ifFile_002EGetPath(ptr3, &obj2);
			try
			{
				string value = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj2) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj2)))));
				styleDataGrid.CurrentRow.Cells[columnName].Value = value;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
		}
	}

	public void HandleExtractStylesFromFile(object P_0, EventArgs P_1)
	{
		Cursor.Current = Cursors.WaitCursor;
		ExtractStylesFromFile();
		Cursor.Current = Cursors.Default;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private unsafe bool DoesBuildingsShpFilePathExist()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(BuildingsShapefilePath);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		uint num;
		if (ptr == null)
		{
			num = 0u;
		}
		else
		{
			uint num2 = 0u;
			if (*ptr != 0)
			{
				do
				{
					num2++;
				}
				while (*((int)num2 + ptr) != 0);
			}
			num = num2;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, ptr, (int)num + ptr);
		try
		{
			if (global::_003CModule_003E.arFile_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj))))))
			{
				goto IL_0098;
			}
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
		return false;
		IL_0098:
		arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr3 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
		if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr3));
			*(int*)ptr3 = 0;
			((int*)ptr3)[2] = 0;
			((int*)ptr3)[1] = 0;
			global::_003CModule_003E.delete(ptr3);
		}
		return true;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private unsafe bool DoesBuildingsDbfFilePathExist()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(BuildingsDbfFilePath);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		uint num;
		if (ptr == null)
		{
			num = 0u;
		}
		else
		{
			uint num2 = 0u;
			if (*ptr != 0)
			{
				do
				{
					num2++;
				}
				while (*((int)num2 + ptr) != 0);
			}
			num = num2;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, ptr, (int)num + ptr);
		try
		{
			if (global::_003CModule_003E.arFile_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj))))))
			{
				goto IL_0098;
			}
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
		return false;
		IL_0098:
		arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr3 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
		if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr3));
			*(int*)ptr3 = 0;
			((int*)ptr3)[2] = 0;
			((int*)ptr3)[1] = 0;
			global::_003CModule_003E.delete(ptr3);
		}
		return true;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private unsafe bool DoesBuildingsPrjFilePathExist()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(BuildingsPrjFilePath);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		uint num;
		if (ptr == null)
		{
			num = 0u;
		}
		else
		{
			uint num2 = 0u;
			if (*ptr != 0)
			{
				do
				{
					num2++;
				}
				while (*((int)num2 + ptr) != 0);
			}
			num = num2;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, ptr, (int)num + ptr);
		try
		{
			if (global::_003CModule_003E.arFile_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj))))))
			{
				goto IL_0098;
			}
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
		return false;
		IL_0098:
		arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr3 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
		if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr3));
			*(int*)ptr3 = 0;
			((int*)ptr3)[2] = 0;
			((int*)ptr3)[1] = 0;
			global::_003CModule_003E.delete(ptr3);
		}
		return true;
	}

	private unsafe void SyncFilePaths()
	{
		//IL_0220->IL0225: Incompatible stack types: I4 vs Ref
		//IL_0276->IL027b: Incompatible stack types: I4 vs Ref
		//IL_01c7->IL01cc: Incompatible stack types: I4 vs Ref
		ManagedDialog.MessagePanel.ClearMessage();
		if (string.IsNullOrEmpty(BuildingsShapefilePath))
		{
			BuildingsDbfFilePath = "";
			BuildingsPrjFilePath = "";
			ManagedDialog.InvalidateBuildingsFilePath();
			FilePathsAreValid = false;
			return;
		}
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(BuildingsShapefilePath);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		uint num;
		if (ptr == null)
		{
			num = 0u;
		}
		else
		{
			uint num2 = 0u;
			if (*ptr != 0)
			{
				do
				{
					num2++;
				}
				while (*((int)num2 + ptr) != 0);
			}
			num = num2;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, ptr, (int)num + ptr);
		try
		{
			global::_003CModule_003E.arPath_002ERemoveExtension(&obj);
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EAppend(&obj, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_04ILJMCKLM_0040_003F4dbf_003F_0024AA_0040));
			sbyte* value = ((*(int*)(&obj) == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)(*(uint*)(&obj)))));
			BuildingsDbfFilePath = new string(value);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
			*(int*)(&obj2) = 0;
			uint num3;
			if (ptr == null)
			{
				num3 = 0u;
			}
			else
			{
				uint num4 = 0u;
				if (*ptr != 0)
				{
					do
					{
						num4++;
					}
					while (*((int)num4 + ptr) != 0);
				}
				num3 = num4;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj2, null, ptr, (int)num3 + ptr);
			try
			{
				global::_003CModule_003E.arPath_002ERemoveExtension(&obj2);
				global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EAppend(&obj2, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_04OEHEAGAI_0040_003F4prj_003F_0024AA_0040));
				sbyte* value2 = ((*(int*)(&obj2) == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)(*(uint*)(&obj2)))));
				BuildingsPrjFilePath = new string(value2);
				bool flag = DoesBuildingsShpFilePathExist();
				bool flag2 = DoesBuildingsDbfFilePathExist();
				bool flag3 = DoesBuildingsPrjFilePathExist();
				if (flag && flag2 && flag3)
				{
					ManagedDialog.ValidateBuildingsFilePath();
					FilePathsAreValid = true;
				}
				else
				{
					ManagedDialog.InvalidateBuildingsFilePath();
					FilePathsAreValid = false;
					string msg = "";
					if (!flag)
					{
						sbyte* ptr2 = global::_003CModule_003E.arPath_002EGetFileName(ptr);
						System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj3);
						*(int*)(&obj3) = 0;
						uint num5;
						if (ptr2 == null)
						{
							num5 = 0u;
						}
						else
						{
							uint num6 = 0u;
							if (*ptr2 != 0)
							{
								do
								{
									num6++;
								}
								while (*((int)num6 + ptr2) != 0);
							}
							num5 = num6;
						}
						global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj3, null, ptr2, (int)num5 + ptr2);
						try
						{
							string text = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj3) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj3)))));
							msg = string.Concat("SHP file '" + text, "' does not exist.");
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj3);
							throw;
						}
						global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj3);
					}
					else if (!flag2)
					{
						System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj4);
						global::_003CModule_003E.arPath_002EGetFileName(&obj4, &obj);
						try
						{
							string text2 = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj4) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj4)))));
							msg = string.Concat("DBF file '" + text2, "' does not exist.");
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj4);
							throw;
						}
						global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj4);
					}
					else if (!flag3)
					{
						System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj5);
						global::_003CModule_003E.arPath_002EGetFileName(&obj5, &obj2);
						try
						{
							string text3 = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj5) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj5)))));
							msg = string.Concat("PRJ file '" + text3, "' does not exist.");
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj5);
							throw;
						}
						global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj5);
					}
					ManagedDialog.MessagePanel.ErrorMessage(msg);
				}
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
	}

	private unsafe void PopulateFieldNames()
	{
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(BuildingsDbfFilePath);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		uint num;
		if (ptr == null)
		{
			num = 0u;
		}
		else
		{
			uint num2 = 0u;
			if (*ptr != 0)
			{
				do
				{
					num2++;
				}
				while (*((int)num2 + ptr) != 0);
			}
			num = num2;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, ptr, (int)num + ptr);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out gisDbfParser gisDbfParser2);
		try
		{
			global::_003CModule_003E.gisDbfParser_002E_007Bctor_007D(&gisDbfParser2, &obj);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		try
		{
			arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr2 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
			if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
			{
				global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr2));
				*(int*)ptr2 = 0;
				((int*)ptr2)[2] = 0;
				((int*)ptr2)[1] = 0;
				global::_003CModule_003E.delete(ptr2);
			}
			uint num3 = System.Runtime.CompilerServices.Unsafe.As<gisDbfParser, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisDbfParser2, 40));
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E obj2);
			*(int*)(&obj2) = 0;
			System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 4)) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 12));
			System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8)) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 12));
			System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 12)) = 0;
			System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 16)) = 0;
			System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 20)) = 0;
			System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 24)) = 1;
			System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 25)) = 1;
			try
			{
				if (0 < num3)
				{
					int num4 = 0;
					uint num5 = num3;
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arTreeNodeBase* ptr3);
					do
					{
						arBasicString_003Cchar_002CarAllocator_003E* value = (arBasicString_003Cchar_002CarAllocator_003E*)(num4 + System.Runtime.CompilerServices.Unsafe.As<gisDbfParser, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisDbfParser2, 36)));
						global::_003CModule_003E.arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EInsertNode((arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*)(&obj2), value, (arTreeNodeBase*)(int)System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 4)), &ptr3);
						num4 += 20;
						num5--;
					}
					while (num5 != 0);
				}
				string[] array = new string[num3];
				string[] array2 = new string[num3];
				string[] array3 = new string[num3];
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E.ConstIterator constIterator);
				*(int*)(&constIterator) = System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
				uint num6 = 0u;
				while ((void*)(*(int*)(&constIterator)) != System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 12)))
				{
					arBasicString_003Cchar_002CarAllocator_003E* ptr4 = (arBasicString_003Cchar_002CarAllocator_003E*)(*(int*)(&constIterator) + 16);
					uint num7 = *(uint*)ptr4;
					sbyte* value2 = ((num7 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num7)));
					array[num6] = new string(value2);
					uint num8 = *(uint*)ptr4;
					sbyte* value3 = ((num8 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num8)));
					array2[num6] = new string(value3);
					uint num9 = *(uint*)ptr4;
					sbyte* value4 = ((num9 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num9)));
					array3[num6] = new string(value4);
					num6++;
					global::_003CModule_003E.arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EConstIterator_002E_002B_002B(&constIterator);
				}
				ManagedDialog.HeightTab_SetHeightFieldNameItems(array);
				ManagedDialog.AggregateDataTab_SetGroupIdFieldNameItems(array2);
				ManagedDialog.SetBuildingTypeFieldNameItems(array3);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			global::_003CModule_003E.arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EClear((arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*)(&obj2));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisDbfParser*, void>)(&global::_003CModule_003E.gisDbfParser_002E_007Bdtor_007D), &gisDbfParser2);
			throw;
		}
		global::_003CModule_003E.gisDbfParser_002E_007Bdtor_007D(&gisDbfParser2);
	}

	private unsafe void SyncFieldNames()
	{
		PopulateFieldNames();
		if ((global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VgisGisBlackboard_0040_0040_0040_0040SAAAVgisGisBlackboard_0040_0040XZ_004051 & 1) == 0)
		{
			global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VgisGisBlackboard_0040_0040_0040_0040SAAAVgisGisBlackboard_0040_0040XZ_004051 |= 1u;
			try
			{
				global::_003CModule_003E._003Finstance_0040_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VgisGisBlackboard_0040_0040_0040_0040SAAAVgisGisBlackboard_0040_0040XZ_00404PAV3_0040A = global::_003CModule_003E.arSingleton_003CgisGisBlackboard_003E_002ECreateInstance();
			}
			catch
			{
				//try-fault
				global::_003CModule_003E._003F_003F_B_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VgisGisBlackboard_0040_0040_0040_0040SAAAVgisGisBlackboard_0040_0040XZ_004051 &= 4294967294u;
				throw;
			}
		}
		gisBuildingImportParameters* ptr = (gisBuildingImportParameters*)global::_003CModule_003E._003Finstance_0040_003F1_003F_003FGetInstance_0040_003F_0024arSingleton_0040VgisGisBlackboard_0040_0040_0040_0040SAAAVgisGisBlackboard_0040_0040XZ_00404PAV3_0040A;
		ManagedDialog.HeightTab_SetRandomHeightMin(*(float*)ptr);
		ManagedDialog.HeightTab_SetRandomHeightMax(((float*)ptr)[1]);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector_003CedUnitMeterConversion_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CedUnitMeterConversion_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CedUnitMeterConversion_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) = 0;
		try
		{
			arVector_003CedUnitMeterConversion_002CarAllocator_003E* ptr2 = (arVector_003CedUnitMeterConversion_002CarAllocator_003E*)((byte*)Dialog + 156);
			if (ptr2 != &obj)
			{
				int num = *(int*)ptr2;
				edUnitMeterConversion* first = (edUnitMeterConversion*)num;
				global::_003CModule_003E.arVector_003CedUnitMeterConversion_002CarAllocator_003E_002EAssign_003Cclass_0020edUnitMeterConversion_0020const_0020_002A_003E(&obj, first, (edUnitMeterConversion*)(((int*)ptr2)[1] * 16 + num));
			}
			string[] array = new string[System.Runtime.CompilerServices.Unsafe.As<arVector_003CedUnitMeterConversion_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4))];
			string[] array2 = new string[System.Runtime.CompilerServices.Unsafe.As<arVector_003CedUnitMeterConversion_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4))];
			uint num2 = 0u;
			if (0u < (uint)System.Runtime.CompilerServices.Unsafe.As<arVector_003CedUnitMeterConversion_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)))
			{
				int num3 = *(int*)(&obj);
				System.Runtime.CompilerServices.Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY0IA_0040D obj2);
				do
				{
					uint num4 = *(uint*)num3;
					sbyte* value = ((num4 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num4)));
					array[num2] = new string(value);
					double num5 = *(double*)(num3 + 8);
					global::_003CModule_003E.sprintf((sbyte*)(&obj2), (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_02NJPGOMH_0040_003F_0024CFf_003F_0024AA_0040), __arglist(num5));
					array2[num2] = new string((sbyte*)(&obj2));
					num2++;
					num3 += 16;
				}
				while (num2 < (uint)System.Runtime.CompilerServices.Unsafe.As<arVector_003CedUnitMeterConversion_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)));
			}
			ManagedDialog.HeightTab_SetHeightUnitItems(array, array2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arVector_003CedUnitMeterConversion_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arVector_003CedUnitMeterConversion_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arVector_003CedUnitMeterConversion_002CarAllocator_003E_002EResetMemory(&obj);
	}

	private unsafe void ExtractStylesFromFile()
	{
		//IL_005d->IL0062: Incompatible stack types: I4 vs Ref
		//IL_0177->IL017c: Incompatible stack types: I4 vs Ref
		//IL_035f->IL0364: Incompatible stack types: I4 vs Ref
		if (string.IsNullOrEmpty(BuildingsShapefilePath))
		{
			return;
		}
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(BuildingsShapefilePath);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		uint num;
		if (ptr == null)
		{
			num = 0u;
		}
		else
		{
			uint num2 = 0u;
			if (*ptr != 0)
			{
				do
				{
					num2++;
				}
				while (*((int)num2 + ptr) != 0);
			}
			num = num2;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, ptr, (int)num + ptr);
		try
		{
			if (global::_003CModule_003E.arFile_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj))))))
			{
				goto IL_0088;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return;
		IL_0088:
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
		try
		{
			sbyte* ptr2 = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(ManagedDialog.GetBuildingTypeFieldName());
			*(int*)(&obj2) = 0;
			uint num3;
			if (ptr2 == null)
			{
				num3 = 0u;
			}
			else
			{
				uint num4 = 0u;
				if (*ptr2 != 0)
				{
					do
					{
						num4++;
					}
					while (*((int)num4 + ptr2) != 0);
				}
				num3 = num4;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj2, null, ptr2, (int)num3 + ptr2);
			try
			{
				int num5 = ((*(int*)(&obj2) == 0 || *(int*)(*(int*)(&obj2) + 4) == 1) ? 1 : 0);
				if ((byte)num5 != 0)
				{
					goto end_IL_00d5;
				}
				goto end_IL_0088;
				end_IL_00d5:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
			goto IL_0119;
			end_IL_0088:;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj3);
		try
		{
			try
			{
				*(int*)(&obj3) = 0;
				uint num6;
				if (ptr == null)
				{
					num6 = 0u;
				}
				else
				{
					uint num7 = 0u;
					if (*ptr != 0)
					{
						do
						{
							num7++;
						}
						while (*((int)num7 + ptr) != 0);
					}
					num6 = num7;
				}
				global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj3, null, ptr, (int)num6 + ptr);
				try
				{
					global::_003CModule_003E.arPath_002ERemoveExtension(&obj3);
					global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EAppend(&obj3, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_04ILJMCKLM_0040_003F4dbf_003F_0024AA_0040));
					if (!global::_003CModule_003E.arFile_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj3) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj3))))))
					{
						goto end_IL_0157;
					}
					goto end_IL_0126;
					end_IL_0157:;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj3);
					throw;
				}
				global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj3);
				goto IL_01ac;
				end_IL_0126:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			goto end_IL_0126_2;
			IL_01ac:
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
			goto IL_01c3;
			end_IL_0126_2:;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		try
		{
			try
			{
				try
				{
					ManagedDialog.ClearBuildingTypeStyles();
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E obj4);
					*(int*)(&obj4) = 0;
					System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 4)) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 12));
					System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 8)) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 12));
					System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 12)) = 0;
					System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 16)) = 0;
					System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 20)) = 0;
					System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 24)) = 1;
					System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 25)) = 1;
					try
					{
						System.Runtime.CompilerServices.Unsafe.SkipInit(out gisDbfParser gisDbfParser2);
						global::_003CModule_003E.gisDbfParser_002E_007Bctor_007D(&gisDbfParser2, &obj3);
						try
						{
							int num8 = -1;
							global::_003CModule_003E.gisDbfParser_002EAddFieldOfInterest(&gisDbfParser2, &num8, &obj2);
							System.Runtime.CompilerServices.Unsafe.SkipInit(out gisDbfRecord gisDbfRecord2);
							*(int*)(&gisDbfRecord2) = 0;
							System.Runtime.CompilerServices.Unsafe.As<gisDbfRecord, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisDbfRecord2, 4)) = 0;
							System.Runtime.CompilerServices.Unsafe.As<gisDbfRecord, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisDbfRecord2, 8)) = 0;
							try
							{
								if (global::_003CModule_003E.gisDbfParser_002EReadNextRecord(&gisDbfParser2, &gisDbfRecord2))
								{
									System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj5);
									System.Runtime.CompilerServices.Unsafe.SkipInit(out arTreeNodeBase* ptr5);
									do
									{
										if (0u >= (uint)System.Runtime.CompilerServices.Unsafe.As<gisDbfRecord, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisDbfRecord2, 4)))
										{
											continue;
										}
										int num9 = 0;
										uint num10 = System.Runtime.CompilerServices.Unsafe.As<gisDbfRecord, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisDbfRecord2, 4));
										do
										{
											gisDbfField* ptr3 = (gisDbfField*)(*(int*)(&gisDbfRecord2) + num9);
											if (*(int*)ptr3 == num8)
											{
												sbyte* ptr4 = global::_003CModule_003E.gisDbfField_002EExtractString(ptr3);
												*(int*)(&obj5) = 0;
												uint num11;
												if (ptr4 == null)
												{
													num11 = 0u;
												}
												else
												{
													uint num12 = 0u;
													if (*ptr4 != 0)
													{
														do
														{
															num12++;
														}
														while (*((int)num12 + ptr4) != 0);
													}
													num11 = num12;
												}
												global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj5, null, ptr4, (int)num11 + ptr4);
												try
												{
													global::_003CModule_003E.arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EInsertNode((arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*)(&obj4), &obj5, (arTreeNodeBase*)(int)System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 4)), &ptr5);
												}
												catch
												{
													//try-fault
													global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj5);
													throw;
												}
												arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr6 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj5));
												if (*(int*)(&obj5) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj5) + 16)) == 0)
												{
													global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr6));
													*(int*)ptr6 = 0;
													((int*)ptr6)[2] = 0;
													((int*)ptr6)[1] = 0;
													global::_003CModule_003E.delete(ptr6);
												}
												*(int*)(&obj5) = 0;
											}
											num9 += 268;
											num10--;
										}
										while (num10 != 0);
									}
									while (global::_003CModule_003E.gisDbfParser_002EReadNextRecord(&gisDbfParser2, &gisDbfRecord2));
								}
								System.Runtime.CompilerServices.Unsafe.SkipInit(out arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E.ConstIterator constIterator);
								*(int*)(&constIterator) = System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 8));
								while ((void*)(*(int*)(&constIterator)) != System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj4, 12)))
								{
									uint num13 = *(uint*)(*(int*)(&constIterator) + 16);
									string value = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref num13 == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)num13)));
									string styleName = "";
									ManagedDialog.TypeStyleChooser.AddStyle(value, styleName);
									global::_003CModule_003E.arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EConstIterator_002E_002B_002B(&constIterator);
								}
							}
							catch
							{
								//try-fault
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisDbfRecord*, void>)(&global::_003CModule_003E.gisDbfRecord_002E_007Bdtor_007D), &gisDbfRecord2);
								throw;
							}
							global::_003CModule_003E.delete((void*)(int)(*(uint*)(&gisDbfRecord2)));
							*(int*)(&gisDbfRecord2) = 0;
							System.Runtime.CompilerServices.Unsafe.As<gisDbfRecord, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisDbfRecord2, 8)) = 0;
							System.Runtime.CompilerServices.Unsafe.As<gisDbfRecord, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisDbfRecord2, 4)) = 0;
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisDbfParser*, void>)(&global::_003CModule_003E.gisDbfParser_002E_007Bdtor_007D), &gisDbfParser2);
							throw;
						}
						global::_003CModule_003E.gisDbfParser_002E_007Bdtor_007D(&gisDbfParser2);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002E_007Bdtor_007D), &obj4);
						throw;
					}
					global::_003CModule_003E.arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EClear((arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*)(&obj4));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj3);
					throw;
				}
				arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr7 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj3));
				if (*(int*)(&obj3) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj3) + 16)) == 0)
				{
					global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr7));
					*(int*)ptr7 = 0;
					((int*)ptr7)[2] = 0;
					((int*)ptr7)[1] = 0;
					global::_003CModule_003E.delete(ptr7);
				}
				*(int*)(&obj3) = 0;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr8 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
			if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
			{
				global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr8));
				*(int*)ptr8 = 0;
				((int*)ptr8)[2] = 0;
				((int*)ptr8)[1] = 0;
				global::_003CModule_003E.delete(ptr8);
			}
			*(int*)(&obj2) = 0;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr9 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
		if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr9));
			*(int*)ptr9 = 0;
			((int*)ptr9)[2] = 0;
			((int*)ptr9)[1] = 0;
			global::_003CModule_003E.delete(ptr9);
		}
		return;
		IL_01c3:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return;
		IL_0119:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
	}
}
