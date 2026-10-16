using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using _003CCppImplementationDetails_003E;
using _003FA0xdfe22a50;

internal class edActivationHelpers
{
	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool IsStoredActivationKeyValid(arBasicString_003Cchar_002CarAllocator_003E* csVersion)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		try
		{
			if (GetActivationKeyFromFile(&obj, csVersion))
			{
				goto IL_0053;
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
		return false;
		IL_0053:
		bool result;
		try
		{
			result = IsKeyValid(&obj, csVersion);
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
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool IsKeyValid(arBasicString_003Cchar_002CarAllocator_003E* userKey, arBasicString_003Cchar_002CarAllocator_003E* csVersion)
	{
		if (global::_003CModule_003E.arStringOps_002EIsNullOrEmpty(userKey))
		{
			return false;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) = 0;
		try
		{
			GenerateLocalKeys(&obj, csVersion);
			arBasicString_003Cchar_002CarAllocator_003E* ptr = (arBasicString_003Cchar_002CarAllocator_003E*)(int)(*(uint*)(&obj));
			arBasicString_003Cchar_002CarAllocator_003E* ptr2 = (arBasicString_003Cchar_002CarAllocator_003E*)(System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) * 8 + *(int*)(&obj));
			if ((arBasicString_003Cchar_002CarAllocator_003E*)(*(int*)(&obj)) != ptr2)
			{
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
				do
				{
					*(int*)(&obj2) = *(int*)ptr;
					if (*(int*)(&obj2) != 0)
					{
						global::_003CModule_003E.arAtomic_002EInterlockIncrementWrapper((int*)(*(int*)(&obj2) + 16));
					}
					try
					{
						if (!global::_003CModule_003E.arStringOps_002EIsNullOrEmpty(&obj2))
						{
							goto IL_00a8;
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
					goto IL_00fc;
					IL_00fc:
					ptr = (arBasicString_003Cchar_002CarAllocator_003E*)((byte*)ptr + 8);
					continue;
					IL_010a:
					arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr4 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
					if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
					{
						global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr4));
						*(int*)ptr4 = 0;
						((int*)ptr4)[2] = 0;
						((int*)ptr4)[1] = 0;
						global::_003CModule_003E.delete(ptr4);
					}
					*(int*)(&obj2) = 0;
					goto IL_014e;
					IL_00a8:
					try
					{
						if (global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002ECompare(&obj2, userKey) == 0)
						{
							goto IL_010a;
						}
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
						throw;
					}
					arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr5 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
					if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
					{
						global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr5));
						*(int*)ptr5 = 0;
						((int*)ptr5)[2] = 0;
						((int*)ptr5)[1] = 0;
						global::_003CModule_003E.delete(ptr5);
					}
					*(int*)(&obj2) = 0;
					goto IL_00fc;
				}
				while (ptr != ptr2);
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002EResetMemory(&obj);
		return false;
		IL_014e:
		global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002EResetMemory(&obj);
		return true;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool HasActivationKey(arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		//IL_0014->IL0019: Incompatible stack types: I4 vs Ref
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		GetActivationKeyFilePath(&obj, appVersion);
		bool result;
		try
		{
			result = global::_003CModule_003E.arPath_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj)))));
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
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool GenerateLocalKeys(arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E* outLocalKeys, arBasicString_003Cchar_002CarAllocator_003E* csVersion)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) = 0;
		try
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
			*(int*)(&obj2) = 0;
			sbyte* ptr = (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_01LFCBOECM_0040_003F4_003F_0024AA_0040);
			uint num = 0u;
			do
			{
				ptr++;
				num++;
			}
			while (*ptr != 0);
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj2, null, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_01LFCBOECM_0040_003F4_003F_0024AA_0040), ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_01LFCBOECM_0040_003F4_003F_0024AA_0040) + num));
			try
			{
				global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002ESplit_003Cclass_0020arVector_003Cclass_0020arBasicString_003Cchar_002Cclass_0020arAllocator_003E_002Cclass_0020arAllocator_003E_0020_003E(csVersion, &obj, &obj2, false);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr2 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
			if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
			{
				global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr2));
				*(int*)ptr2 = 0;
				((int*)ptr2)[2] = 0;
				((int*)ptr2)[1] = 0;
				global::_003CModule_003E.delete(ptr2);
			}
			if (System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) == 4)
			{
				goto IL_00b3;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002EResetMemory(&obj);
		return false;
		IL_00b3:
		bool result;
		try
		{
			string text = "";
			text += "CS";
			uint num2 = *(uint*)(int)(*(uint*)(&obj));
			sbyte* value = ((num2 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num2)));
			text += new string(value);
			uint num3 = *(uint*)(*(int*)(&obj) + 8);
			sbyte* value2 = ((num3 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num3)));
			text += new string(value2);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E obj3);
			*(int*)(&obj3) = 0;
			System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj3, 4)) = 0;
			System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj3, 8)) = 0;
			try
			{
				GetLocalMacAddresses(&obj3);
				arBasicString_003Cchar_002CarAllocator_003E* ptr3 = (arBasicString_003Cchar_002CarAllocator_003E*)(int)(*(uint*)(&obj3));
				arBasicString_003Cchar_002CarAllocator_003E* ptr4 = (arBasicString_003Cchar_002CarAllocator_003E*)(System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj3, 4)) * 8 + *(int*)(&obj3));
				if ((arBasicString_003Cchar_002CarAllocator_003E*)(*(int*)(&obj3)) != ptr4)
				{
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj4);
					do
					{
						if (!global::_003CModule_003E.arStringOps_002EIsNullOrEmpty(ptr3))
						{
							string text2 = "";
							text2 += "CityScape";
							text2 += new string(global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002ECStr(csVersion));
							uint num4 = *(uint*)ptr3;
							sbyte* value3 = ((num4 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num4)));
							text2 += new string(value3);
							text2 += "B00SH";
							string text3 = string.Concat(text + "-", Md5String(text2));
							*(int*)(&obj4) = 0;
							try
							{
								global::_003CModule_003E._003FA0xdfe22a50_002EManagedToPaString(&obj4, text3.ToUpper());
								global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002EPushBack(outLocalKeys, &obj4);
							}
							catch
							{
								//try-fault
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj4);
								throw;
							}
							arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr5 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj4));
							if (*(int*)(&obj4) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj4) + 16)) == 0)
							{
								global::_003CModule_003E.delete((void*)(int)(*(uint*)(int)(*(uint*)(&obj4))));
								*(int*)(int)(*(uint*)(&obj4)) = 0;
								*(int*)(*(int*)(&obj4) + 8) = 0;
								*(int*)(*(int*)(&obj4) + 4) = 0;
								global::_003CModule_003E.delete((void*)(int)(*(uint*)(&obj4)));
							}
							*(int*)(&obj4) = 0;
						}
						ptr3 = (arBasicString_003Cchar_002CarAllocator_003E*)((byte*)ptr3 + 8);
					}
					while (ptr3 != ptr4);
				}
				result = System.Runtime.CompilerServices.Unsafe.As<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj3, 4)) != 0;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002E_007Bdtor_007D), &obj3);
				throw;
			}
			global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002EResetMemory(&obj3);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002EResetMemory(&obj);
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool StoreActivationKey(arBasicString_003Cchar_002CarAllocator_003E* appVersion, arBasicString_003Cchar_002CarAllocator_003E* activationKey)
	{
		//IL_0014->IL0019: Incompatible stack types: I4 vs Ref
		//IL_0042->IL0047: Incompatible stack types: I4 vs Ref
		//IL_002b->IL0030: Incompatible stack types: I4 vs Ref
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		GetActivationKeyDirPath(&obj, appVersion);
		try
		{
			if (!global::_003CModule_003E.arPath_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj))))))
			{
				global::_003CModule_003E.arDirectory_002ECreateNewDirectory((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj)))), true);
			}
			if (global::_003CModule_003E.arPath_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj))))))
			{
				goto IL_0093;
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
		return false;
		IL_0105:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return false;
		IL_0093:
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arStreamFile arStreamFile2);
		try
		{
			GetActivationKeyFilePath(&obj2, appVersion);
			try
			{
				global::_003CModule_003E.arStreamFile_002E_007Bctor_007D(&arStreamFile2);
				try
				{
					sbyte* ptr2 = ((*(int*)(&obj2) == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)(*(uint*)(&obj2)))));
					if (!global::_003CModule_003E.arStreamFile_002EOpen(&arStreamFile2, ptr2, (arStream.EnumFileAccess)1, null, null))
					{
						goto end_IL_00a4;
					}
					goto end_IL_009c;
					end_IL_00a4:;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arStreamFile*, void>)(&global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D), &arStreamFile2);
					throw;
				}
				global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D(&arStreamFile2);
				goto IL_00ee;
				end_IL_009c:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			goto end_IL_0093;
			IL_00ee:
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
			goto IL_0105;
			end_IL_0093:;
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
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arSerializeTextWriter arSerializeTextWriter2);
					global::_003CModule_003E.arSerialize_002E_007Bctor_007D((arSerialize*)(&arSerializeTextWriter2));
					try
					{
						*(int*)(&arSerializeTextWriter2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arSerializeTextWriter_0040_00406B_0040);
						global::_003CModule_003E.arIndenter_002E_007Bctor_007D((arIndenter*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeTextWriter2, 272)));
						System.Runtime.CompilerServices.Unsafe.As<arSerializeTextWriter, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeTextWriter2, 4)) = (int)(&arStreamFile2);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSerialize*, void>)(&global::_003CModule_003E.arSerialize_002E_007Bdtor_007D), &arSerializeTextWriter2);
						throw;
					}
					try
					{
						global::_003CModule_003E.arSerializeTextWriter_002ESerialize(&arSerializeTextWriter2, activationKey);
						((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void>)(int)(*(uint*)(*(int*)(&arStreamFile2) + 32)))((nint)(&arStreamFile2));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSerializeTextWriter*, void>)(&global::_003CModule_003E.arSerializeTextWriter_002E_007Bdtor_007D), &arSerializeTextWriter2);
						throw;
					}
					global::_003CModule_003E.arSerialize_002E_007Bdtor_007D((arSerialize*)(&arSerializeTextWriter2));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arStreamFile*, void>)(&global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D), &arStreamFile2);
					throw;
				}
				global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D(&arStreamFile2);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return true;
	}

	public unsafe static void RemoveActivationKeyFile(arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		//IL_0014->IL0019: Incompatible stack types: I4 vs Ref
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		GetActivationKeyFilePath(&obj, appVersion);
		try
		{
			global::_003CModule_003E.arFile_002EDelete((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj)))));
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

	public unsafe static void RemoveTimestampFile(arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		//IL_0014->IL0019: Incompatible stack types: I4 vs Ref
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		GetTimestampFilePath(&obj, appVersion);
		try
		{
			global::_003CModule_003E.arFile_002EDelete((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj)))));
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

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool TimestampFileExists(arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		//IL_0014->IL0019: Incompatible stack types: I4 vs Ref
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		GetTimestampFilePath(&obj, appVersion);
		bool result;
		try
		{
			result = global::_003CModule_003E.arPath_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj)))));
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
		return result;
	}

	public unsafe static EnumTimestampCheckResult IsTimestampExpired(arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		if (!TimestampFileExists(appVersion))
		{
			return (EnumTimestampCheckResult)0;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		GetTimestampFilePath(&obj, appVersion);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arStreamFile arStreamFile2);
		try
		{
			global::_003CModule_003E.arStreamFile_002E_007Bctor_007D(&arStreamFile2);
			try
			{
				sbyte* ptr = ((*(int*)(&obj) == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)(*(uint*)(&obj)))));
				if (!global::_003CModule_003E.arStreamFile_002EOpen(&arStreamFile2, ptr, (arStream.EnumFileAccess)0, null, null))
				{
					goto end_IL_001b;
				}
				goto end_IL_0013;
				end_IL_001b:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arStreamFile*, void>)(&global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D), &arStreamFile2);
				throw;
			}
			global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D(&arStreamFile2);
			goto IL_0067;
			end_IL_0013:;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		DateTime dateTime;
		EnumTimestampCheckResult result;
		DateTime dateTime3;
		try
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out uint month);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out uint day);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out uint year);
			try
			{
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arSerializeBinaryReader arSerializeBinaryReader2);
				global::_003CModule_003E.arSerialize_002E_007Bctor_007D((arSerialize*)(&arSerializeBinaryReader2));
				try
				{
					*(int*)(&arSerializeBinaryReader2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arSerializeBinaryReader_0040_00406B_0040);
					System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryReader, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryReader2, 4)) = (int)(&arStreamFile2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSerialize*, void>)(&global::_003CModule_003E.arSerialize_002E_007Bdtor_007D), &arSerializeBinaryReader2);
					throw;
				}
				try
				{
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void*, uint, int>)(int)(*(uint*)(*(int*)(int)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryReader, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryReader2, 4)) + 44)))((IntPtr)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryReader, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryReader2, 4)), &month, 4u);
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void*, uint, int>)(int)(*(uint*)(*(int*)(int)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryReader, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryReader2, 4)) + 44)))((IntPtr)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryReader, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryReader2, 4)), &day, 4u);
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void*, uint, int>)(int)(*(uint*)(*(int*)(int)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryReader, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryReader2, 4)) + 44)))((IntPtr)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryReader, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryReader2, 4)), &year, 4u);
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void>)(int)(*(uint*)(*(int*)(&arStreamFile2) + 32)))((nint)(&arStreamFile2));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSerializeBinaryReader*, void>)(&global::_003CModule_003E.arSerializeBinaryReader_002E_007Bdtor_007D), &arSerializeBinaryReader2);
					throw;
				}
				global::_003CModule_003E.arSerialize_002E_007Bdtor_007D((arSerialize*)(&arSerializeBinaryReader2));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arStreamFile*, void>)(&global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D), &arStreamFile2);
				throw;
			}
			global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D(&arStreamFile2);
			dateTime = default(DateTime);
			try
			{
				DateTime dateTime2 = new DateTime((int)year, (int)month, (int)day);
				dateTime = dateTime2;
			}
			catch (Exception)
			{
				result = (EnumTimestampCheckResult)1;
				goto end_IL_0071;
			}
			DateTime today = DateTime.Today;
			dateTime3 = today;
			if (!(dateTime > today))
			{
				goto IL_016a;
			}
			goto IL_0160;
			end_IL_0071:;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return result;
		IL_016a:
		try
		{
			if (dateTime3.Subtract(dateTime).Days <= 30)
			{
				goto IL_019a;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return (EnumTimestampCheckResult)3;
		IL_019a:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return (EnumTimestampCheckResult)2;
		IL_0067:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return (EnumTimestampCheckResult)1;
		IL_0160:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return (EnumTimestampCheckResult)1;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool WriteTimestampFile(arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		//IL_0014->IL0019: Incompatible stack types: I4 vs Ref
		//IL_0042->IL0047: Incompatible stack types: I4 vs Ref
		//IL_002b->IL0030: Incompatible stack types: I4 vs Ref
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		GetActivationKeyDirPath(&obj, appVersion);
		try
		{
			if (!global::_003CModule_003E.arPath_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj))))))
			{
				global::_003CModule_003E.arDirectory_002ECreateNewDirectory((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj)))), true);
			}
			if (global::_003CModule_003E.arPath_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj))))))
			{
				goto IL_0093;
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
		return false;
		IL_0137:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return false;
		IL_0093:
		uint month;
		uint day;
		uint year;
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arStreamFile arStreamFile2);
		try
		{
			ValueType valueType = DateTime.Today;
			month = (uint)((DateTime)valueType).Month;
			day = (uint)((DateTime)valueType).Day;
			year = (uint)((DateTime)valueType).Year;
			GetTimestampFilePath(&obj2, appVersion);
			try
			{
				global::_003CModule_003E.arStreamFile_002E_007Bctor_007D(&arStreamFile2);
				try
				{
					sbyte* ptr2 = ((*(int*)(&obj2) == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)(*(uint*)(&obj2)))));
					if (!global::_003CModule_003E.arStreamFile_002EOpen(&arStreamFile2, ptr2, (arStream.EnumFileAccess)1, null, null))
					{
						goto end_IL_00d6;
					}
					goto end_IL_00ce;
					end_IL_00d6:;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arStreamFile*, void>)(&global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D), &arStreamFile2);
					throw;
				}
				global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D(&arStreamFile2);
				goto IL_0120;
				end_IL_00ce:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			goto end_IL_0093;
			IL_0120:
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
			goto IL_0137;
			end_IL_0093:;
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
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arSerializeBinaryWriter arSerializeBinaryWriter2);
					global::_003CModule_003E.arSerialize_002E_007Bctor_007D((arSerialize*)(&arSerializeBinaryWriter2));
					try
					{
						*(int*)(&arSerializeBinaryWriter2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arSerializeBinaryWriter_0040_00406B_0040);
						System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryWriter, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryWriter2, 4)) = (int)(&arStreamFile2);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSerialize*, void>)(&global::_003CModule_003E.arSerialize_002E_007Bdtor_007D), &arSerializeBinaryWriter2);
						throw;
					}
					try
					{
						uint num = month;
						((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void*, uint, int>)(int)(*(uint*)(*(int*)(int)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryWriter, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryWriter2, 4)) + 40)))((IntPtr)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryWriter, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryWriter2, 4)), &num, 4u);
						uint num2 = day;
						((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void*, uint, int>)(int)(*(uint*)(*(int*)(int)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryWriter, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryWriter2, 4)) + 40)))((IntPtr)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryWriter, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryWriter2, 4)), &num2, 4u);
						uint num3 = year;
						((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void*, uint, int>)(int)(*(uint*)(*(int*)(int)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryWriter, uint>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryWriter2, 4)) + 40)))((IntPtr)System.Runtime.CompilerServices.Unsafe.As<arSerializeBinaryWriter, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeBinaryWriter2, 4)), &num3, 4u);
						((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void>)(int)(*(uint*)(*(int*)(&arStreamFile2) + 32)))((nint)(&arStreamFile2));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSerializeBinaryWriter*, void>)(&global::_003CModule_003E.arSerializeBinaryWriter_002E_007Bdtor_007D), &arSerializeBinaryWriter2);
						throw;
					}
					global::_003CModule_003E.arSerialize_002E_007Bdtor_007D((arSerialize*)(&arSerializeBinaryWriter2));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arStreamFile*, void>)(&global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D), &arStreamFile2);
					throw;
				}
				global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D(&arStreamFile2);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return true;
	}

	private unsafe static arBasicString_003Cchar_002CarAllocator_003E* GetPixelActiveAppDataPath(arBasicString_003Cchar_002CarAllocator_003E* P_0)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out uint num);
		try
		{
			num = 0u;
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
			*(int*)(&obj) = 0;
			try
			{
				global::_003CModule_003E._003FA0xdfe22a50_002EManagedToPaString(&obj, folderPath);
				*(int*)P_0 = 0;
				num = 1u;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
				*(int*)(&obj2) = 0;
				sbyte* ptr = (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0M_0040IJHAAJON_0040PixelActive_003F_0024AA_0040);
				uint num2 = 0u;
				do
				{
					ptr++;
					num2++;
				}
				while (*ptr != 0);
				global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj2, null, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0M_0040IJHAAJON_0040PixelActive_003F_0024AA_0040), ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0M_0040IJHAAJON_0040PixelActive_003F_0024AA_0040) + num2));
				try
				{
					global::_003CModule_003E.arPath_002ECombineNormalizePath(P_0, &obj, &obj2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
					throw;
				}
				arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr2 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
				if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
				{
					global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr2));
					*(int*)ptr2 = 0;
					((int*)ptr2)[2] = 0;
					((int*)ptr2)[1] = 0;
					global::_003CModule_003E.delete(ptr2);
				}
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
			*(int*)(&obj) = 0;
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

	private unsafe static arBasicString_003Cchar_002CarAllocator_003E* GetActivationKeyDirPath(arBasicString_003Cchar_002CarAllocator_003E* P_0, arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out uint num);
		try
		{
			num = 0u;
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
			GetPixelActiveAppDataPath(&obj);
			try
			{
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
				global::_003CModule_003E.operator_002B_003Cchar_002Cclass_0020arAllocator_003E(&obj2, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0L_0040GANIFLPD_0040CityScape_003F5_003F_0024AA_0040), appVersion);
				try
				{
					*(int*)P_0 = 0;
					num = 1u;
					global::_003CModule_003E.arPath_002ECombineNormalizePath(P_0, &obj, &obj2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
					throw;
				}
				arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
				if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
				{
					global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr));
					*(int*)ptr = 0;
					((int*)ptr)[2] = 0;
					((int*)ptr)[1] = 0;
					global::_003CModule_003E.delete(ptr);
				}
				*(int*)(&obj2) = 0;
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

	private unsafe static arBasicString_003Cchar_002CarAllocator_003E* GetActivationKeyFilePath(arBasicString_003Cchar_002CarAllocator_003E* P_0, arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out uint num);
		try
		{
			num = 0u;
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
			GetActivationKeyDirPath(&obj, appVersion);
			try
			{
				*(int*)P_0 = 0;
				num = 1u;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
				*(int*)(&obj2) = 0;
				sbyte* ptr = (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0P_0040GOGGKJBG_0040activation_003F4csk_003F_0024AA_0040);
				uint num2 = 0u;
				do
				{
					ptr++;
					num2++;
				}
				while (*ptr != 0);
				global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj2, null, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0P_0040GOGGKJBG_0040activation_003F4csk_003F_0024AA_0040), ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0P_0040GOGGKJBG_0040activation_003F4csk_003F_0024AA_0040) + num2));
				try
				{
					global::_003CModule_003E.arPath_002ECombineNormalizePath(P_0, &obj, &obj2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
					throw;
				}
				arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr2 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
				if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
				{
					global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr2));
					*(int*)ptr2 = 0;
					((int*)ptr2)[2] = 0;
					((int*)ptr2)[1] = 0;
					global::_003CModule_003E.delete(ptr2);
				}
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
			*(int*)(&obj) = 0;
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

	private unsafe static void GetLocalMacAddresses(arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E* outMacAddresses)
	{
		global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002EClear(outMacAddresses);
		try
		{
			IEnumerator enumerator = NetworkInterface.GetAllNetworkInterfaces().GetEnumerator();
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
			while (enumerator.MoveNext())
			{
				NetworkInterface obj = (NetworkInterface)enumerator.Current;
				string text = "";
				byte[] addressBytes = obj.GetPhysicalAddress().GetAddressBytes();
				for (int i = 0; i < (nint)addressBytes.LongLength; i++)
				{
					byte b = addressBytes[i];
					text += b.ToString("X2");
					if (i != (nint)addressBytes.LongLength - 1)
					{
						text += "-";
					}
				}
				*(int*)(&obj2) = 0;
				try
				{
					global::_003CModule_003E._003FA0xdfe22a50_002EManagedToPaString(&obj2, text);
					global::_003CModule_003E.arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E_002EPushBack(outMacAddresses, &obj2);
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
		catch (Exception)
		{
		}
	}

	private static string Md5String(string strToMd5)
	{
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] bytes = new UTF8Encoding().GetBytes(strToMd5);
		bytes = mD5CryptoServiceProvider.ComputeHash(bytes);
		string text = "";
		int num = 0;
		if (0 < (nint)bytes.LongLength)
		{
			do
			{
				if ((uint)bytes[num] < 16u)
				{
					byte b = bytes[num];
					text = string.Concat(text, "0" + b.ToString("x"));
				}
				else
				{
					byte b2 = bytes[num];
					text += b2.ToString("x");
				}
				num++;
			}
			while (num < (nint)bytes.LongLength);
		}
		return Convert.ToBase64String(bytes);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private unsafe static bool GetActivationKeyFromFile(arBasicString_003Cchar_002CarAllocator_003E* outActivationKey, arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		GetActivationKeyFilePath(&obj, appVersion);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arStreamFile arStreamFile2);
		try
		{
			*(int*)(&obj2) = 0;
			try
			{
				global::_003CModule_003E.arStreamFile_002E_007Bctor_007D(&arStreamFile2);
				try
				{
					sbyte* ptr = ((*(int*)(&obj) == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)(*(uint*)(&obj)))));
					if (!global::_003CModule_003E.arStreamFile_002EOpen(&arStreamFile2, ptr, (arStream.EnumFileAccess)0, null, null))
					{
						goto end_IL_0015;
					}
					goto end_IL_000d;
					end_IL_0015:;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arStreamFile*, void>)(&global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D), &arStreamFile2);
					throw;
				}
				global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D(&arStreamFile2);
				goto IL_006f;
				end_IL_000d:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
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
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arSerializeTextReader arSerializeTextReader2);
					global::_003CModule_003E.arSerialize_002E_007Bctor_007D((arSerialize*)(&arSerializeTextReader2));
					try
					{
						*(int*)(&arSerializeTextReader2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7arSerializeTextReader_0040_00406B_0040);
						System.Runtime.CompilerServices.Unsafe.As<arSerializeTextReader, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref arSerializeTextReader2, 4)) = (int)(&arStreamFile2);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSerialize*, void>)(&global::_003CModule_003E.arSerialize_002E_007Bdtor_007D), &arSerializeTextReader2);
						throw;
					}
					try
					{
						global::_003CModule_003E.arSerializeTextReader_002ESerialize(&arSerializeTextReader2, &obj2);
						((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void>)(int)(*(uint*)(*(int*)(&arStreamFile2) + 32)))((nint)(&arStreamFile2));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arSerializeTextReader*, void>)(&global::_003CModule_003E.arSerializeTextReader_002E_007Bdtor_007D), &arSerializeTextReader2);
						throw;
					}
					global::_003CModule_003E.arSerialize_002E_007Bdtor_007D((arSerialize*)(&arSerializeTextReader2));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arStreamFile*, void>)(&global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D), &arStreamFile2);
					throw;
				}
				global::_003CModule_003E.arStreamFile_002E_007Bdtor_007D(&arStreamFile2);
				if (outActivationKey != &obj2)
				{
					if (*(int*)(&obj2) != 0)
					{
						global::_003CModule_003E.arAtomic_002EInterlockIncrementWrapper((int*)(*(int*)(&obj2) + 16));
					}
					global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EFreeRef(outActivationKey);
					*(int*)outActivationKey = *(int*)(&obj2);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr2 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
			if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
			{
				global::_003CModule_003E.delete((void*)(int)(*(uint*)(int)(*(uint*)(&obj2))));
				*(int*)(int)(*(uint*)(&obj2)) = 0;
				*(int*)(*(int*)(&obj2) + 8) = 0;
				*(int*)(*(int*)(&obj2) + 4) = 0;
				global::_003CModule_003E.delete((void*)(int)(*(uint*)(&obj2)));
			}
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
		return true;
		IL_006f:
		arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr4 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj));
		if (*(int*)(&obj) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj) + 16)) == 0)
		{
			global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr4));
			*(int*)ptr4 = 0;
			((int*)ptr4)[2] = 0;
			((int*)ptr4)[1] = 0;
			global::_003CModule_003E.delete(ptr4);
		}
		return false;
	}

	private unsafe static arBasicString_003Cchar_002CarAllocator_003E* GetTimestampFilePath(arBasicString_003Cchar_002CarAllocator_003E* P_0, arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out uint num);
		try
		{
			num = 0u;
			System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
			GetActivationKeyDirPath(&obj, appVersion);
			try
			{
				*(int*)P_0 = 0;
				num = 1u;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
				*(int*)(&obj2) = 0;
				sbyte* ptr = (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0P_0040CEDANKPP_0040activation_003F4tsd_003F_0024AA_0040);
				uint num2 = 0u;
				do
				{
					ptr++;
					num2++;
				}
				while (*ptr != 0);
				global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj2, null, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0P_0040CEDANKPP_0040activation_003F4tsd_003F_0024AA_0040), ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0P_0040CEDANKPP_0040activation_003F4tsd_003F_0024AA_0040) + num2));
				try
				{
					global::_003CModule_003E.arPath_002ECombineNormalizePath(P_0, &obj, &obj2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
					throw;
				}
				arBasicString_003Cchar_002CarAllocator_003E.StringData* ptr2 = (arBasicString_003Cchar_002CarAllocator_003E.StringData*)(int)(*(uint*)(&obj2));
				if (*(int*)(&obj2) != 0 && global::_003CModule_003E.arAtomic_002EInterlockDecrementWrapper((int*)(*(int*)(&obj2) + 16)) == 0)
				{
					global::_003CModule_003E.delete((void*)(int)(*(uint*)ptr2));
					*(int*)ptr2 = 0;
					((int*)ptr2)[2] = 0;
					((int*)ptr2)[1] = 0;
					global::_003CModule_003E.delete(ptr2);
				}
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
			*(int*)(&obj) = 0;
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
