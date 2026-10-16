using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;
using palDotNet.palDialog;

internal class edOverlayImageDialogManagedImpl
{
	public unsafe edOverlayImageDialogManaged* Dialog;

	public OverlayImageDialog ManagedDialog;

	public unsafe void SetImageFilePath(arBasicString_003Cchar_002CarAllocator_003E* filePath)
	{
		//IL_000a->IL000f: Incompatible stack types: I4 vs Ref
		uint num = *(uint*)filePath;
		string imageOverlayFilePath = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref num == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)num)));
		ManagedDialog.ImageOverlayFilePath = imageOverlayFilePath;
	}

	public void SetEnableImageFilePath([MarshalAs(UnmanagedType.U1)] bool bEnabled)
	{
		ManagedDialog.GetImageOverlayFilePath().Enabled = bEnabled;
	}

	public unsafe void SetImageFilePathFilter(arBasicString_003Cchar_002CarAllocator_003E* filePathFilter)
	{
		//IL_000a->IL000f: Incompatible stack types: I4 vs Ref
		uint num = *(uint*)filePathFilter;
		string imageOverlayFilePathFilter = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref num == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)num)));
		ManagedDialog.ImageOverlayFilePathFilter = imageOverlayFilePathFilter;
	}
}
