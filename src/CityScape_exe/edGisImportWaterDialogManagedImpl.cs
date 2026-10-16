using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;
using palDotNet.palDialog;

internal class edGisImportWaterDialogManagedImpl
{
	public unsafe edGisImportWaterDialogManaged* Dialog;

	public GisImportWaterDialog ManagedDialog;

	public string WaterShapefilePath;

	public string WaterDbfFilePath;

	public string WaterPrjFilePath;

	public bool FilePathsAreValid;

	public edGisImportWaterDialogManagedImpl()
	{
		WaterPrjFilePath = (WaterDbfFilePath = (WaterShapefilePath = ""));
		FilePathsAreValid = false;
	}

	public void HandleOnShown(object P_0, EventArgs P_1)
	{
		if (string.IsNullOrEmpty(WaterShapefilePath = ManagedDialog.WaterShapefilePath.Path))
		{
			ManagedDialog.InvalidateWaterFilePath();
		}
		else
		{
			ManagedDialog.ValidateWaterFilePath();
		}
		SyncFilePaths();
	}

	public void HandleWaterPathChanged(object P_0, EventArgs P_1)
	{
		string path = ManagedDialog.WaterShapefilePath.Path;
		if (!(WaterShapefilePath == path))
		{
			WaterShapefilePath = path;
			SyncFilePaths();
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private unsafe bool DoesWaterShpFilePathExist()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(WaterShapefilePath);
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
	private unsafe bool DoesWaterDbfFilePathExist()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(WaterDbfFilePath);
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
	private unsafe bool DoesWaterPrjFilePathExist()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(WaterPrjFilePath);
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
		if (string.IsNullOrEmpty(WaterShapefilePath))
		{
			WaterDbfFilePath = "";
			WaterPrjFilePath = "";
			ManagedDialog.InvalidateWaterFilePath();
			FilePathsAreValid = false;
			return;
		}
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(WaterShapefilePath);
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
			WaterDbfFilePath = new string(value);
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
				WaterPrjFilePath = new string(value2);
				bool flag = DoesWaterShpFilePathExist();
				bool flag2 = DoesWaterDbfFilePathExist();
				bool flag3 = DoesWaterPrjFilePathExist();
				if (flag && flag2 && flag3)
				{
					ManagedDialog.ValidateWaterFilePath();
					FilePathsAreValid = true;
				}
				else
				{
					ManagedDialog.InvalidateWaterFilePath();
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
}
