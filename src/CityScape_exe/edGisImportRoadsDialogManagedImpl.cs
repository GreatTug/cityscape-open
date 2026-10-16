using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;
using palDotNet.palDialog;

internal class edGisImportRoadsDialogManagedImpl
{
	public unsafe edGisImportRoadsDialogManaged* Dialog;

	public GisImportRoadsDialog ManagedDialog;

	public string RoadsShapefilePath;

	public string RoadsDbfFilePath;

	public string RoadsPrjFilePath;

	public string LastAttributeMappingTemplateName;

	public bool FilePathsAreValid;

	public edGisImportRoadsDialogManagedImpl()
	{
		RoadsPrjFilePath = (RoadsDbfFilePath = (RoadsShapefilePath = ""));
		FilePathsAreValid = false;
	}

	public void HandleOnShown(object P_0, EventArgs P_1)
	{
		if (string.IsNullOrEmpty(RoadsShapefilePath = ManagedDialog.RoadsPath.Path))
		{
			ManagedDialog.InvalidateRoadsFilePath();
		}
		else
		{
			ManagedDialog.ValidateRoadsFilePath();
		}
		SyncFilePaths();
		if (FilePathsAreValid)
		{
			SyncFieldNames();
		}
	}

	public void HandleRoadsPathChanged(object P_0, EventArgs P_1)
	{
		string path = ManagedDialog.RoadsPath.Path;
		if (!(RoadsShapefilePath == path))
		{
			RoadsShapefilePath = path;
			SyncFilePaths();
			if (FilePathsAreValid)
			{
				SyncFieldNames();
			}
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private unsafe bool DoesRoadsShpFilePathExist()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(RoadsShapefilePath);
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
	private unsafe bool DoesRoadsDbfFilePathExist()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(RoadsDbfFilePath);
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
	private unsafe bool DoesRoadsPrjFilePathExist()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(RoadsPrjFilePath);
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
		if (string.IsNullOrEmpty(RoadsShapefilePath))
		{
			RoadsDbfFilePath = "";
			RoadsPrjFilePath = "";
			ManagedDialog.InvalidateRoadsFilePath();
			FilePathsAreValid = false;
			return;
		}
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(RoadsShapefilePath);
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
			RoadsDbfFilePath = new string(value);
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
				RoadsPrjFilePath = new string(value2);
				bool flag = DoesRoadsShpFilePathExist();
				bool flag2 = DoesRoadsDbfFilePathExist();
				bool flag3 = DoesRoadsPrjFilePathExist();
				if (flag && flag2 && flag3)
				{
					ManagedDialog.ValidateRoadsFilePath();
					FilePathsAreValid = true;
				}
				else
				{
					ManagedDialog.InvalidateRoadsFilePath();
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

	private unsafe void PopulateCustomizeTemplateDialog()
	{
		edGisImportRoadsCustomDialogManaged* ptr = (edGisImportRoadsCustomDialogManaged*)(int)((uint*)Dialog)[44];
		sbyte* ptr2 = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(RoadsDbfFilePath);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		uint num;
		if (ptr2 == null)
		{
			num = 0u;
		}
		else
		{
			uint num2 = 0u;
			if (*ptr2 != 0)
			{
				do
				{
					num2++;
				}
				while (*((int)num2 + ptr2) != 0);
			}
			num = num2;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, ptr2, (int)num + ptr2);
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
			arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr3 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
			if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
			{
				global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr3));
				*(int*)ptr3 = 0;
				((int*)ptr3)[2] = 0;
				((int*)ptr3)[1] = 0;
				global::_003CModule_003E.delete(ptr3);
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
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arTreeNodeBase* ptr4);
					do
					{
						arBasicString_003Cchar_002CarAllocator_003E* value = (arBasicString_003Cchar_002CarAllocator_003E*)(num4 + System.Runtime.CompilerServices.Unsafe.As<gisDbfParser, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisDbfParser2, 36)));
						global::_003CModule_003E.arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E_002EInsertNode((arTree_003CarBasicString_003Cchar_002CarAllocator_003E_002CarBasicString_003Cchar_002CarAllocator_003E_002CarIdentity_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E*)(&obj2), value, (arTreeNodeBase*)(int)System.Runtime.CompilerServices.Unsafe.As<arSet_003CarBasicString_003Cchar_002CarAllocator_003E_002CarLess_003CarBasicString_003Cchar_002CarAllocator_003E_0020_003E_002CarAllocator_003E, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 4)), &ptr4);
						num4 += 20;
						num5--;
					}
					while (num5 != 0);
				}
				global::_003CModule_003E.edGisImportRoadsCustomDialogManaged_002EPopulateFieldNameItems(ptr, &obj2);
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
		edGisImportRoadsCustomDialogManaged* ptr = (edGisImportRoadsCustomDialogManaged*)(int)((uint*)Dialog)[44];
		sbyte* ptr2 = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(RoadsShapefilePath);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		uint num;
		if (ptr2 == null)
		{
			num = 0u;
		}
		else
		{
			uint num2 = 0u;
			if (*ptr2 != 0)
			{
				do
				{
					num2++;
				}
				while (*((int)num2 + ptr2) != 0);
			}
			num = num2;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj, null, ptr2, (int)num + ptr2);
		try
		{
			global::_003CModule_003E.edGisImportRoadsCustomDialogManaged_002ESetRoadsShapefilePath(ptr, &obj);
			PopulateCustomizeTemplateDialog();
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr3 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
		if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr3));
			*(int*)ptr3 = 0;
			((int*)ptr3)[2] = 0;
			((int*)ptr3)[1] = 0;
			global::_003CModule_003E.delete(ptr3);
		}
	}
}
