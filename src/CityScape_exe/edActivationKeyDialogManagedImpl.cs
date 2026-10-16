using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using palDotNet.palDialog;

internal class edActivationKeyDialogManagedImpl
{
	public unsafe edActivationKeyDialogManaged* Dialog;

	public ActivationKeyDialog ManagedDialog;

	public string AppVersion;

	public edActivationKeyDialogManagedImpl()
	{
		AppVersion = null;
	}

	public unsafe void Init(arBasicString_003Cchar_002CarAllocator_003E* appVersion)
	{
		uint num = *(uint*)appVersion;
		sbyte* value = ((num == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num)));
		AppVersion = new string(value);
	}

	public unsafe void HandleOkClick(object P_0, EventArgs P_1)
	{
		if (string.IsNullOrEmpty(AppVersion))
		{
			MessageBox.Show("Invalid user key!", "CityScape Registration");
			return;
		}
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(AppVersion);
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
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
		try
		{
			sbyte* ptr2 = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(ManagedDialog.GetActivationKey());
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
				if (global::_003CModule_003E.arStringOps_002EIsNullOrEmpty(&obj2))
				{
					MessageBox.Show("Invalid user key!", "CityScape Registration");
					goto IL_00cc;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			goto end_IL_005f;
			IL_00cc:
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
			goto IL_00e3;
			end_IL_005f:;
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
				if (!global::_003CModule_003E.edActivation_002ECheckActivationKey(&obj, &obj2))
				{
					MessageBox.Show("Invalid user key!", "CityScape Registration");
					goto IL_011d;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			goto end_IL_00f0;
			IL_011d:
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
			goto IL_0134;
			end_IL_00f0:;
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
				if (!global::_003CModule_003E.edActivation_002EStoreActivationKey(&obj, &obj2))
				{
					MessageBox.Show("Invalid user key!", "CityScape Registration");
					goto IL_016e;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			goto end_IL_0141;
			IL_016e:
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj2);
			goto IL_0185;
			end_IL_0141:;
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
				MessageBox.Show("Registration Successful!\nThank you for registering CityScape.", "CityScape Registration");
				ManagedDialog.SetDialogResultOk();
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
		return;
		IL_0185:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return;
		IL_0134:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return;
		IL_00e3:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
	}

	private void DisplayInvalidKeyMessage()
	{
		MessageBox.Show("Invalid user key!", "CityScape Registration");
	}

	private void DisplayActivationSuccessfulMessage()
	{
		MessageBox.Show("Registration Successful!\nThank you for registering CityScape.", "CityScape Registration");
	}
}
