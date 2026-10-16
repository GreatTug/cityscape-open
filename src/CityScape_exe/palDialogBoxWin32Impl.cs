using System;

internal class palDialogBoxWin32Impl
{
	public unsafe palDialogBox_Win32PC* UnmanagedDialog;

	public unsafe void AddCallbacks()
	{
		palDialogBox_Win32PC* unmanagedDialog = UnmanagedDialog;
		((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, void>)(int)(*(uint*)(*(int*)unmanagedDialog + 108)))((nint)unmanagedDialog);
	}
}
