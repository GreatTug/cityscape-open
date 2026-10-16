using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;
using palDotNet.palDialog;

internal class edGisImportElevationDialogManagedImpl
{
	public unsafe edGisImportElevationDialogManaged* Dialog;

	public GisImportElevationDialog ManagedDialog;

	public unsafe void SetElevationFilePathInfo(arBasicString_003Cchar_002CarAllocator_003E* filePathInfo)
	{
		//IL_000a->IL000f: Incompatible stack types: I4 vs Ref
		uint num = *(uint*)filePathInfo;
		string text = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref num == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)num)));
		ManagedDialog.GetImportFilePathLabel().Text = text;
	}

	public unsafe void SetElevationFilePathFilter(arBasicString_003Cchar_002CarAllocator_003E* filePathFilter)
	{
		//IL_000a->IL000f: Incompatible stack types: I4 vs Ref
		uint num = *(uint*)filePathFilter;
		string filter = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref num == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)num)));
		ManagedDialog.GetElevationFilePathInput().Filter = filter;
	}

	public void SetShowAuxiliaryPanel([MarshalAs(UnmanagedType.U1)] bool bShow)
	{
		ManagedDialog.GetAuxiliaryInfoPanel().Visible = bShow;
	}

	public unsafe void SetAuxiliaryPanelLabels(arBasicString_003Cchar_002CarAllocator_003E* infoLabel, arBasicString_003Cchar_002CarAllocator_003E* exampleLabel)
	{
		//IL_000a->IL000f: Incompatible stack types: I4 vs Ref
		//IL_0030->IL0035: Incompatible stack types: I4 vs Ref
		uint num = *(uint*)infoLabel;
		string text = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref num == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)num)));
		ManagedDialog.GetAuxiliaryInfoLabel().Text = text;
		uint num2 = *(uint*)exampleLabel;
		string text2 = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref num2 == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)num2)));
		ManagedDialog.GetAuxiliaryExampleLabel().Text = text2;
	}

	public void HandleElevationFilePathChanged(object P_0, EventArgs P_1)
	{
		ManagedDialog.SyncOkButton();
	}
}
