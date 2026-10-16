using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using _003CCppImplementationDetails_003E;
using palDotNet.palDialog;

internal class edTerrainSettingsDialogManagedImpl
{
	public unsafe edTerrainSettingsDialogManaged* Dialog;

	public TerrainSettingsDialog ManagedDialog;

	public TerrainSettingsPanel SettingsPanel;

	private bool IsHandling = false;

	public unsafe void Init()
	{
		SettingsPanel = ManagedDialog.GetTerrainSettingsPanel();
		System.Runtime.CompilerServices.Unsafe.SkipInit(out gisAlignedBox2d gisAlignedBox2d2);
		GetGeoRegionValues(&gisAlignedBox2d2);
		CheckTerrainRegionSize(&gisAlignedBox2d2);
	}

	public void HandleGeolocatedCheckChanged(object obj, EventArgs eventArgs)
	{
		if (!IsHandling)
		{
			if (SettingsPanel.GeolocatedCheckGroupBox.Checked)
			{
				SettingsPanel.ShowMessagePanel(bShow: true);
				HandleSizeOriginValueChanged(obj, eventArgs);
			}
			else
			{
				SettingsPanel.ShowMessagePanel(bShow: false);
			}
		}
	}

	public unsafe void HandleSetRegionFromFile(object P_0, EventArgs P_1)
	{
		//IL_01a4->IL01a9: Incompatible stack types: I4 vs Ref
		//IL_0265->IL026a: Incompatible stack types: I4 vs Ref
		if (IsHandling)
		{
			return;
		}
		IsHandling = true;
		SettingsPanel.ClearMessageInfo();
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		System.Runtime.CompilerServices.Unsafe.SkipInit(out edFileFilter edFileFilter2);
		try
		{
			*(int*)(&edFileFilter2) = 0;
			System.Runtime.CompilerServices.Unsafe.As<edFileFilter, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref edFileFilter2, 4)) = 0;
			System.Runtime.CompilerServices.Unsafe.As<edFileFilter, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref edFileFilter2, 8)) = 0;
			try
			{
				System.Runtime.CompilerServices.Unsafe.As<edFileFilter, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref edFileFilter2, 16)) = 0;
				System.Runtime.CompilerServices.Unsafe.As<edFileFilter, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref edFileFilter2, 17)) = 0;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arVector_003CedFileFilter_003A_003AEntry_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arVector_003CedFileFilter_003A_003AEntry_002CarAllocator_003E_002E_007Bdtor_007D), &edFileFilter2);
				throw;
			}
			try
			{
				global::_003CModule_003E.edTerrainSettingsHelpers_002EGetFileFilterForSupportedBounds(&edFileFilter2);
				OpenFileDialog openFileDialog = new OpenFileDialog();
				System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj2);
				arBasicString_003Cchar_002CarAllocator_003E* ptr = global::_003CModule_003E.edFileFilter_002EGetFilterString(&edFileFilter2, &obj2);
				try
				{
					uint num = *(uint*)ptr;
					sbyte* value = ((num == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num)));
					openFileDialog.Filter = new string(value);
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
				openFileDialog.FilterIndex = 0;
				openFileDialog.ShowHelp = false;
				openFileDialog.Multiselect = false;
				openFileDialog.CheckFileExists = true;
				openFileDialog.RestoreDirectory = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					sbyte* ptr3 = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(openFileDialog.FileName);
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj3);
					*(int*)(&obj3) = 0;
					sbyte* ptr4 = ptr3;
					uint num2;
					if (ptr3 == null)
					{
						num2 = 0u;
					}
					else
					{
						uint num3 = 0u;
						if (*ptr3 != 0)
						{
							do
							{
								ptr4++;
								num3++;
							}
							while (*ptr4 != 0);
						}
						num2 = num3;
					}
					global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EInsert(&obj3, null, ptr3, (int)num2 + ptr3);
					try
					{
						if (*(int*)(&obj3) != 0)
						{
							global::_003CModule_003E.arAtomic_002EInterlockIncrementWrapper((int*)(*(int*)(&obj3) + 16));
						}
						global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002EFreeRef(&obj);
						*(int*)(&obj) = *(int*)(&obj3);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj3);
						throw;
					}
					global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj3);
					goto IL_0187;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<edFileFilter*, void>)(&global::_003CModule_003E.edFileFilter_002E_007Bdtor_007D), &edFileFilter2);
				throw;
			}
			goto end_IL_0021;
			IL_0187:
			global::_003CModule_003E.edFileFilter_002E_007Bdtor_007D(&edFileFilter2);
			ManagedDialog.BeginWaitCursor();
			if (global::_003CModule_003E.arFile_002EExists((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj))))))
			{
				goto IL_01e1;
			}
			ManagedDialog.EndWaitCursor();
			IsHandling = false;
			goto IL_01d4;
			end_IL_0021:;
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
				IsHandling = false;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<edFileFilter*, void>)(&global::_003CModule_003E.edFileFilter_002E_007Bdtor_007D), &edFileFilter2);
				throw;
			}
			global::_003CModule_003E.edFileFilter_002E_007Bdtor_007D(&edFileFilter2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return;
		IL_01d4:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return;
		IL_01e1:
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj4);
		System.Runtime.CompilerServices.Unsafe.SkipInit(out gisAlignedBox2d gisAlignedBox2d2);
		try
		{
			global::_003CModule_003E.arPath_002EGetFileName(&obj4, &obj);
			try
			{
				global::_003CModule_003E.edTerrainSettingsHelpers_002EExtractBoundsFromFile(&gisAlignedBox2d2, &obj);
				if (!global::_003CModule_003E.gisBoundsHelpers_002EIsValidLL(&gisAlignedBox2d2))
				{
					sbyte* ptr5 = ((*(int*)(&obj) == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)(*(uint*)(&obj)))));
					global::_003CModule_003E.arErrorf((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0CJ_0040PPGPLPAG_0040Failed_003F5to_003F5extract_003F5bounds_003F5from_003F5fi_0040), __arglist(ptr5));
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj5);
					arBasicString_003Cchar_002CarAllocator_003E* left = global::_003CModule_003E.operator_002B_003Cchar_002Cclass_0020arAllocator_003E(&obj5, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_0CF_0040CMGBOAJD_0040Failed_003F5to_003F5extract_003F5bounds_003F5from_003F5fi_0040), &obj4);
					System.Runtime.CompilerServices.Unsafe.SkipInit(out arBasicString_003Cchar_002CarAllocator_003E obj6);
					try
					{
						global::_003CModule_003E.operator_002B_003Cchar_002Cclass_0020arAllocator_003E(&obj6, left, (sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_02DGLLHPKE_0040_003F8_003F4_003F_0024AA_0040));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj5);
						throw;
					}
					try
					{
						global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj5);
						string msg = new string((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref *(int*)(&obj6) == 0 ? ref System.Runtime.CompilerServices.Unsafe.As<_0024ArrayType_0024_0024_0024BY00D, _003F>(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA) : ref *(_003F*)(*(int*)(int)(*(uint*)(&obj6)))));
						SettingsPanel.ShowErrorMessage(msg);
						ManagedDialog.EndWaitCursor();
						IsHandling = false;
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj6);
						throw;
					}
					global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj6);
					goto IL_02b7;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj4);
				throw;
			}
			goto end_IL_01e1;
			IL_02b7:
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj4);
			goto IL_02ce;
			end_IL_01e1:;
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
				CheckTerrainRegionSize(&gisAlignedBox2d2);
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: false);
				SetTerrainOriginValues(&gisAlignedBox2d2);
				SetGeoRegionValues(&gisAlignedBox2d2);
				SetTerrainSizeValues(&gisAlignedBox2d2);
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: true);
				SettingsPanel.ComputeTileCountValues();
				ManagedDialog.EndWaitCursor();
				IsHandling = false;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj4);
				throw;
			}
			global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj4);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arBasicString_003Cchar_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
		return;
		IL_02ce:
		global::_003CModule_003E.arBasicString_003Cchar_002CarAllocator_003E_002E_007Bdtor_007D(&obj);
	}

	public void HandleClearGeolocationValues(object P_0, EventArgs P_1)
	{
		if (!IsHandling)
		{
			IsHandling = true;
			SettingsPanel.ClearMessageInfo();
			SettingsPanel.SetEnableComputeTileCountValues(bEnabled: false);
			SettingsPanel.SetTerrainSize(0f, 0f);
			SettingsPanel.ClearTerrainOrigin();
			SettingsPanel.ClearRegionValues();
			SettingsPanel.SetEnableComputeTileCountValues(bEnabled: true);
			SettingsPanel.ComputeTileCountValues();
			IsHandling = false;
		}
	}

	public unsafe void HandleSizeOriginValueChanged(object P_0, EventArgs P_1)
	{
		if (!IsHandling && SettingsPanel.IsGeolocatedTerrain())
		{
			IsHandling = true;
			SettingsPanel.ClearMessageInfo();
			System.Runtime.CompilerServices.Unsafe.SkipInit(out gisAlignedBox2d gisAlignedBox2d2);
			ComputeBoundsFromTerrainSizeAndOrigin(&gisAlignedBox2d2);
			if (!global::_003CModule_003E.gisBoundsHelpers_002EIsValidLL(&gisAlignedBox2d2))
			{
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: false);
				SettingsPanel.ClearRegionValues();
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: true);
				ReportInvalidGeoRegion(&gisAlignedBox2d2);
				IsHandling = false;
			}
			else
			{
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: false);
				SetGeoRegionValues(&gisAlignedBox2d2);
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: true);
				SettingsPanel.ComputeTileCountValues();
				IsHandling = false;
			}
		}
	}

	public unsafe void HandleGeoRegionValueChanged(object P_0, EventArgs P_1)
	{
		if (!IsHandling)
		{
			IsHandling = true;
			SettingsPanel.ClearMessageInfo();
			System.Runtime.CompilerServices.Unsafe.SkipInit(out gisAlignedBox2d gisAlignedBox2d2);
			GetGeoRegionValues(&gisAlignedBox2d2);
			if (!global::_003CModule_003E.gisBoundsHelpers_002EIsValidLL(&gisAlignedBox2d2))
			{
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: false);
				SettingsPanel.ClearTerrainOrigin();
				SettingsPanel.SetTerrainSize(0f, 0f);
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: true);
				SettingsPanel.ComputeTileCountValues();
				ReportInvalidGeoRegion(&gisAlignedBox2d2);
				IsHandling = false;
			}
			else
			{
				CheckTerrainRegionSize(&gisAlignedBox2d2);
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: false);
				System.Runtime.CompilerServices.Unsafe.SkipInit(out gisVector2d gisVector2d2);
				*(double*)(&gisVector2d2) = (*(double*)(&gisAlignedBox2d2) + System.Runtime.CompilerServices.Unsafe.As<gisAlignedBox2d, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisAlignedBox2d2, 16))) * 0.5;
				System.Runtime.CompilerServices.Unsafe.As<gisVector2d, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisVector2d2, 8)) = (System.Runtime.CompilerServices.Unsafe.As<gisAlignedBox2d, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisAlignedBox2d2, 24)) + System.Runtime.CompilerServices.Unsafe.As<gisAlignedBox2d, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisAlignedBox2d2, 8))) * 0.5;
				SettingsPanel.SetTerrainOrigin(System.Runtime.CompilerServices.Unsafe.As<gisVector2d, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisVector2d2, 8)), *(double*)(&gisVector2d2));
				SetTerrainSizeValues(&gisAlignedBox2d2);
				SettingsPanel.SetEnableComputeTileCountValues(bEnabled: true);
				SettingsPanel.ComputeTileCountValues();
				IsHandling = false;
			}
		}
	}

	private unsafe void ComputeBoundsFromTerrainSizeAndOrigin(gisAlignedBox2d* outBoundsLL)
	{
		double num = (double)SettingsPanel.TerrainSizeX.Value;
		double num2 = (double)SettingsPanel.TerrainSizeY.Value;
		double num3 = (double)SettingsPanel.TerrainOriginLongitude.Value;
		double num4 = (double)SettingsPanel.TerrainOriginLatitude.Value;
		System.Runtime.CompilerServices.Unsafe.SkipInit(out gisLLPoint gisLLPoint2);
		*(int*)(&gisLLPoint2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisProjectionPoint_0040_00406B_0040);
		try
		{
			*(int*)(&gisLLPoint2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisLLPoint_0040_00406B_0040);
			System.Runtime.CompilerServices.Unsafe.As<gisLLPoint, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisLLPoint2, 8)) = num3;
			System.Runtime.CompilerServices.Unsafe.As<gisLLPoint, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisLLPoint2, 16)) = num4;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisProjectionPoint*, void>)(&global::_003CModule_003E.gisProjectionPoint_002E_007Bdtor_007D), &gisLLPoint2);
			throw;
		}
		try
		{
			System.Runtime.CompilerServices.Unsafe.SkipInit(out gisUTMPoint gisUTMPoint2);
			int num5 = (int)((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, gisUTMPoint*, gisUTMPoint*>)(int)(*(uint*)(*(int*)(&gisLLPoint2) + 12)))((nint)(&gisLLPoint2), &gisUTMPoint2);
			System.Runtime.CompilerServices.Unsafe.SkipInit(out gisUTMPoint gisUTMPoint3);
			try
			{
				*(int*)(&gisUTMPoint3) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisProjectionPoint_0040_00406B_0040);
				try
				{
					*(int*)(&gisUTMPoint3) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisUTMPoint_0040_00406B_0040);
					// IL cpblk instruction
					System.Runtime.CompilerServices.Unsafe.CopyBlock(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint3, 8), num5 + 8, 16);
					// IL cpblk instruction
					System.Runtime.CompilerServices.Unsafe.CopyBlockUnaligned(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint3, 24), num5 + 24, 2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisProjectionPoint*, void>)(&global::_003CModule_003E.gisProjectionPoint_002E_007Bdtor_007D), &gisUTMPoint3);
					throw;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisUTMPoint*, void>)(&global::_003CModule_003E.gisUTMPoint_002E_007Bdtor_007D), &gisUTMPoint2);
				throw;
			}
			try
			{
				System.Runtime.CompilerServices.Unsafe.SkipInit(out gisUTMPoint gisUTMPoint4);
				*(int*)(&gisUTMPoint4) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisProjectionPoint_0040_00406B_0040);
				try
				{
					*(int*)(&gisUTMPoint4) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisUTMPoint_0040_00406B_0040);
					// IL cpblk instruction
					System.Runtime.CompilerServices.Unsafe.CopyBlock(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint4, 8), ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint3, 8), 16);
					// IL cpblk instruction
					System.Runtime.CompilerServices.Unsafe.CopyBlock(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint4, 24), ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint3, 24), 2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisProjectionPoint*, void>)(&global::_003CModule_003E.gisProjectionPoint_002E_007Bdtor_007D), &gisUTMPoint4);
					throw;
				}
				try
				{
					System.Runtime.CompilerServices.Unsafe.As<gisUTMPoint, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint4, 8)) = num * 0.5 + System.Runtime.CompilerServices.Unsafe.As<gisUTMPoint, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint4, 8));
					System.Runtime.CompilerServices.Unsafe.As<gisUTMPoint, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint4, 16)) = num2 * 0.5 + System.Runtime.CompilerServices.Unsafe.As<gisUTMPoint, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMPoint4, 16));
					System.Runtime.CompilerServices.Unsafe.SkipInit(out gisUTMBounds gisUTMBounds2);
					*(int*)(&gisUTMBounds2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisProjectionPoint_0040_00406B_0040);
					try
					{
						*(int*)(&gisUTMBounds2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisUTMPoint_0040_00406B_0040);
						// IL cpblk instruction
						System.Runtime.CompilerServices.Unsafe.CopyBlock(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 8), ref global::_003CModule_003E._003FZERO_0040gisVector2d_0040_00402V1_0040B, 16);
						System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 24)) = 90;
						System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 25)) = 0;
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisProjectionPoint*, void>)(&global::_003CModule_003E.gisProjectionPoint_002E_007Bdtor_007D), &gisUTMBounds2);
						throw;
					}
					try
					{
						// IL cpblk instruction
						System.Runtime.CompilerServices.Unsafe.CopyBlock(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 32), ref global::_003CModule_003E._003FZERO_0040gisVector2d_0040_00402V1_0040B, 16);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisUTMPoint*, void>)(&global::_003CModule_003E.gisUTMPoint_002E_007Bdtor_007D), &gisUTMBounds2);
						throw;
					}
					try
					{
						global::_003CModule_003E.gisUTMBounds_002EInitFromCenterAndCorner(&gisUTMBounds2, (gisProjectionPoint*)(&gisUTMPoint3), (gisProjectionPoint*)(&gisUTMPoint4));
						global::_003CModule_003E.gisBoundsHelpers_002EComputeLLFromUTM(outBoundsLL, &gisUTMBounds2);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisUTMBounds*, void>)(&global::_003CModule_003E.gisUTMBounds_002E_007Bdtor_007D), &gisUTMBounds2);
						throw;
					}
					*(int*)(&gisUTMBounds2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisProjectionPoint_0040_00406B_0040);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisUTMPoint*, void>)(&global::_003CModule_003E.gisUTMPoint_002E_007Bdtor_007D), &gisUTMPoint4);
					throw;
				}
				*(int*)(&gisUTMPoint4) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisProjectionPoint_0040_00406B_0040);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisUTMPoint*, void>)(&global::_003CModule_003E.gisUTMPoint_002E_007Bdtor_007D), &gisUTMPoint3);
				throw;
			}
			*(int*)(&gisUTMPoint3) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisProjectionPoint_0040_00406B_0040);
			return;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisLLPoint*, void>)(&global::_003CModule_003E.gisLLPoint_002E_007Bdtor_007D), &gisLLPoint2);
			throw;
		}
	}

	private unsafe void SetTerrainSizeValues(gisAlignedBox2d* boundsLL)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out gisUTMBounds gisUTMBounds2);
		*(int*)(&gisUTMBounds2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisProjectionPoint_0040_00406B_0040);
		try
		{
			*(int*)(&gisUTMBounds2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisUTMPoint_0040_00406B_0040);
			// IL cpblk instruction
			System.Runtime.CompilerServices.Unsafe.CopyBlock(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 8), ref global::_003CModule_003E._003FZERO_0040gisVector2d_0040_00402V1_0040B, 16);
			System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 24)) = 90;
			System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 25)) = 0;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisProjectionPoint*, void>)(&global::_003CModule_003E.gisProjectionPoint_002E_007Bdtor_007D), &gisUTMBounds2);
			throw;
		}
		try
		{
			// IL cpblk instruction
			System.Runtime.CompilerServices.Unsafe.CopyBlock(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 32), ref global::_003CModule_003E._003FZERO_0040gisVector2d_0040_00402V1_0040B, 16);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisUTMPoint*, void>)(&global::_003CModule_003E.gisUTMPoint_002E_007Bdtor_007D), &gisUTMBounds2);
			throw;
		}
		try
		{
			global::_003CModule_003E.gisBoundsHelpers_002EComputeUTMFromLL(&gisUTMBounds2, boundsLL);
			float sizeX = (float)System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 32));
			float sizeY = (float)System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 40));
			SettingsPanel.SetTerrainSize(sizeX, sizeY);
			return;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisUTMBounds*, void>)(&global::_003CModule_003E.gisUTMBounds_002E_007Bdtor_007D), &gisUTMBounds2);
			throw;
		}
	}

	private unsafe void GetTerrainSizeValues(double* outWidth, double* outHeight)
	{
		decimal value = SettingsPanel.TerrainSizeX.Value;
		*outWidth = (double)value;
		decimal value2 = SettingsPanel.TerrainSizeY.Value;
		*outHeight = (double)value2;
	}

	private void ClearTerrainSizeValues()
	{
		SettingsPanel.SetTerrainSize(0f, 0f);
	}

	private unsafe void SetTerrainOriginValues(gisAlignedBox2d* boundsLL)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out gisVector2d gisVector2d2);
		*(double*)(&gisVector2d2) = (*(double*)boundsLL + ((double*)boundsLL)[2]) * 0.5;
		System.Runtime.CompilerServices.Unsafe.As<gisVector2d, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisVector2d2, 8)) = (((double*)((byte*)boundsLL + 16))[1] + ((double*)boundsLL)[1]) * 0.5;
		SettingsPanel.SetTerrainOrigin(System.Runtime.CompilerServices.Unsafe.As<gisVector2d, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisVector2d2, 8)), *(double*)(&gisVector2d2));
	}

	private unsafe void GetTerrainOriginValues(double* originLat, double* originLon)
	{
		decimal value = SettingsPanel.TerrainOriginLongitude.Value;
		*originLon = (double)value;
		decimal value2 = SettingsPanel.TerrainOriginLatitude.Value;
		*originLat = (double)value2;
	}

	private void ClearTerrainOriginValues()
	{
		SettingsPanel.ClearTerrainOrigin();
	}

	private unsafe void SetGeoRegionValues(gisAlignedBox2d* boundsLL)
	{
		decimal value = (decimal)(*(double*)boundsLL);
		SettingsPanel.RegionWestLongitude.Value = value;
		decimal value2 = (decimal)((double*)boundsLL)[1];
		SettingsPanel.RegionSouthLatitude.Value = value2;
		decimal value3 = (decimal)((double*)boundsLL)[2];
		SettingsPanel.RegionEastLongitude.Value = value3;
		decimal value4 = (decimal)((double*)boundsLL)[3];
		SettingsPanel.RegionNorthLatitude.Value = value4;
	}

	private unsafe void GetGeoRegionValues(gisAlignedBox2d* outBoundsLL)
	{
		decimal value = SettingsPanel.RegionWestLongitude.Value;
		*(double*)outBoundsLL = (double)value;
		decimal value2 = SettingsPanel.RegionSouthLatitude.Value;
		((double*)outBoundsLL)[1] = (double)value2;
		decimal value3 = SettingsPanel.RegionEastLongitude.Value;
		((double*)outBoundsLL)[2] = (double)value3;
		decimal value4 = SettingsPanel.RegionNorthLatitude.Value;
		((double*)outBoundsLL)[3] = (double)value4;
	}

	private void ClearGeoRegionValues()
	{
		SettingsPanel.ClearRegionValues();
	}

	private unsafe void CheckTerrainRegionSize(gisAlignedBox2d* boundsLL)
	{
		System.Runtime.CompilerServices.Unsafe.SkipInit(out gisUTMBounds gisUTMBounds2);
		*(int*)(&gisUTMBounds2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisProjectionPoint_0040_00406B_0040);
		try
		{
			*(int*)(&gisUTMBounds2) = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7gisUTMPoint_0040_00406B_0040);
			// IL cpblk instruction
			System.Runtime.CompilerServices.Unsafe.CopyBlock(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 8), ref global::_003CModule_003E._003FZERO_0040gisVector2d_0040_00402V1_0040B, 16);
			System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 24)) = 90;
			System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, sbyte>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 25)) = 0;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisProjectionPoint*, void>)(&global::_003CModule_003E.gisProjectionPoint_002E_007Bdtor_007D), &gisUTMBounds2);
			throw;
		}
		try
		{
			// IL cpblk instruction
			System.Runtime.CompilerServices.Unsafe.CopyBlock(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 32), ref global::_003CModule_003E._003FZERO_0040gisVector2d_0040_00402V1_0040B, 16);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisUTMPoint*, void>)(&global::_003CModule_003E.gisUTMPoint_002E_007Bdtor_007D), &gisUTMBounds2);
			throw;
		}
		try
		{
			global::_003CModule_003E.gisBoundsHelpers_002EComputeUTMFromLL(&gisUTMBounds2, boundsLL);
			float num = (float)System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 32));
			float num2 = (float)System.Runtime.CompilerServices.Unsafe.As<gisUTMBounds, double>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref gisUTMBounds2, 40));
			float terrainMaxSizeX = SettingsPanel.GetTerrainMaxSizeX();
			float terrainMaxSizeY = SettingsPanel.GetTerrainMaxSizeY();
			bool flag = ((num > terrainMaxSizeX) ? true : false);
			bool flag2 = ((num2 > terrainMaxSizeY) ? true : false);
			if (flag)
			{
				if (flag2)
				{
					SettingsPanel.ShowWarningMessage("Region exceeds Max Terrain Size X and Y. Terrain Size has been clamped.");
				}
				else
				{
					SettingsPanel.ShowWarningMessage("Region exceeds Max Terrain Size X. Terrain Size has been clamped.");
				}
			}
			else if (flag2)
			{
				SettingsPanel.ShowWarningMessage("Region exceeds Max Terrain Size Y. Terrain Size has been clamped.");
			}
			return;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gisUTMBounds*, void>)(&global::_003CModule_003E.gisUTMBounds_002E_007Bdtor_007D), &gisUTMBounds2);
			throw;
		}
	}

	private unsafe void ReportInvalidGeoRegion(gisAlignedBox2d* boundsLL)
	{
		bool flag = !(*(double*)boundsLL < ((double*)boundsLL)[2]);
		if (!(((double*)boundsLL)[1] < ((double*)((byte*)boundsLL + 16))[1]))
		{
			if (flag)
			{
				SettingsPanel.ShowErrorMessage("Region latitude and longitude values are invalid.");
			}
			else
			{
				SettingsPanel.ShowErrorMessage("Region latitude values are invalid.");
			}
		}
		else if (flag)
		{
			SettingsPanel.ShowErrorMessage("Region longitude values are invalid.");
		}
		else
		{
			SettingsPanel.ShowErrorMessage("Region values are invalid.");
		}
	}
}
