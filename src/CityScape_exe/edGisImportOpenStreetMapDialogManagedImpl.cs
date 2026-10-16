using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;
using palDotNet.palDialog;

internal class edGisImportOpenStreetMapDialogManagedImpl
{
	public unsafe edGisImportOpenStreetMapDialogManaged* Dialog;

	public GisImportOpenStreetMapDialog ManagedDialog;

	public string OsmFilePath;

	public edGisImportOpenStreetMapDialogManagedImpl()
	{
		OsmFilePath = "";
	}

	public void Init()
	{
		if (string.IsNullOrEmpty(OsmFilePath = ManagedDialog.OsmFilePath.Path))
		{
			ManagedDialog.InvalidateOsmFilePath();
		}
		else
		{
			ManagedDialog.ValidateOsmFilePath();
		}
		ManagedDialog.SyncOsmFeaturesToImportPanel();
	}

	public void HandleOsmFilePathChanged(object P_0, EventArgs P_1)
	{
		string path = ManagedDialog.OsmFilePath.Path;
		if (!(OsmFilePath == path))
		{
			OsmFilePath = path;
			if (IsValidOsmFilePath())
			{
				ManagedDialog.ValidateOsmFilePath();
			}
			else
			{
				ManagedDialog.InvalidateOsmFilePath();
			}
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private unsafe bool IsValidOsmFilePath()
	{
		//IL_0048->IL004d: Incompatible stack types: I4 vs Ref
		sbyte* ptr = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(OsmFilePath);
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
}
