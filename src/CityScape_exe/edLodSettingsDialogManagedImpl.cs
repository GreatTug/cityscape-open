using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using palDotNet.palDialog;

internal class edLodSettingsDialogManagedImpl
{
	public unsafe edLodSettingsDialogManaged* Dialog;

	public LodSettingsDialog ManagedDialog;

	private unsafe palLodCountDialog* LodCountDialog = null;

	private int CurrentLodIndex = -1;

	private bool IsSwitchingLods = false;

	public unsafe void SetLodCountDialog(palLodCountDialog* lodCountDialog)
	{
		LodCountDialog = lodCountDialog;
	}

	public unsafe void Init(int lodCount, arVector_003CarBasicString_003Cchar_002CarAllocator_003E_002CarAllocator_003E* overheadResNames, edLodInfoRanges* lodInfoRanges)
	{
		ManagedDialog.SetCullDistanceRange(*(float*)lodInfoRanges, ((float*)lodInfoRanges)[1], ((float*)lodInfoRanges)[2]);
		ManagedDialog.SetHeightToleranceRange(((float*)lodInfoRanges)[3], ((float*)lodInfoRanges)[4], ((float*)lodInfoRanges)[5]);
		ManagedDialog.SetShorelineToleranceRange(((float*)lodInfoRanges)[6], ((float*)lodInfoRanges)[7], ((float*)lodInfoRanges)[8]);
		ManagedDialog.SetPathToleranceRange(((float*)lodInfoRanges)[9], ((float*)lodInfoRanges)[10], ((float*)lodInfoRanges)[11]);
		ManagedDialog.SetExtrusionToleranceRange(((float*)lodInfoRanges)[12], ((float*)lodInfoRanges)[13], ((float*)lodInfoRanges)[14]);
		ManagedDialog.SetMaxSegmentLengthRange(((float*)lodInfoRanges)[15], ((float*)lodInfoRanges)[16], ((float*)lodInfoRanges)[17]);
		ManagedDialog.SetBridgePostSlicesRange(((int*)lodInfoRanges)[18], ((int*)lodInfoRanges)[19], ((int*)lodInfoRanges)[20]);
		uint num = ((uint*)overheadResNames)[1];
		string[] array = new string[num];
		uint num2 = 0u;
		if (0 < num)
		{
			do
			{
				uint num3 = *(uint*)(int)(num2 * 8 + (uint)(*(int*)overheadResNames));
				sbyte* value = ((num3 == 0) ? ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::_003CModule_003E._003FEmptyString_0040_003F_0024arBasicString_0040DVarAllocator_0040_0040_0040_00400PADA)) : ((sbyte*)(int)(*(uint*)(int)num3)));
				array[num2] = new string(value);
				num2++;
			}
			while (num2 < num);
		}
		ManagedDialog.InitOverheadResolutionItems(array);
		ManagedDialog.GenerateLodListItems(lodCount);
		CurrentLodIndex = ManagedDialog.LodChooserListBox.SelectedIndex;
		RunValidationChecks();
	}

	public unsafe void HandleSetLodCount(object P_0, EventArgs P_1)
	{
		if (LodCountDialog == null)
		{
			return;
		}
		int num = ((int*)Dialog)[45];
		ManagedDialog.SetLodCount(num);
		if (ManagedDialog.ShowLodCountDialog() != DialogResult.OK)
		{
			return;
		}
		int lodCount = ManagedDialog.GetLodCount();
		if (num == lodCount)
		{
			return;
		}
		System.Runtime.CompilerServices.Unsafe.SkipInit(out arVector_003CedLodInfo_002CarAllocator_003E obj);
		*(int*)(&obj) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CedLodInfo_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) = 0;
		System.Runtime.CompilerServices.Unsafe.As<arVector_003CedLodInfo_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 8)) = 0;
		try
		{
			arVector_003CedLodInfo_002CarAllocator_003E* ptr = (arVector_003CedLodInfo_002CarAllocator_003E*)((byte*)Dialog + 176);
			int num2 = ((num >= lodCount) ? lodCount : num);
			if (0 < num2)
			{
				int num3 = 0;
				int num4 = num2;
				do
				{
					global::_003CModule_003E.arVector_003CedLodInfo_002CarAllocator_003E_002EPushBack(&obj, (edLodInfo*)(num3 + *(int*)ptr));
					num3 += 44;
					num4--;
				}
				while (num4 != 0);
			}
			int num5 = num2;
			if (num2 < lodCount)
			{
				int num6 = num2 * 44;
				System.Runtime.CompilerServices.Unsafe.SkipInit(out edLodInfo edLodInfo2);
				do
				{
					*(int*)(&edLodInfo2) = num5;
					global::_003CModule_003E.edLodInfo_002EMakeNextLod(&edLodInfo2, (edLodInfo*)(*(int*)(&obj) + num6 - 44));
					global::_003CModule_003E.arVector_003CedLodInfo_002CarAllocator_003E_002EPushBack(&obj, &edLodInfo2);
					num5++;
					num6 += 44;
				}
				while (num5 < lodCount);
			}
			CurrentLodIndex = -1;
			arVector_003CedLodInfo_002CarAllocator_003E* ptr2 = (arVector_003CedLodInfo_002CarAllocator_003E*)((byte*)Dialog + 176);
			if (&obj != ptr2)
			{
				global::_003CModule_003E.arVector_003CedLodInfo_002CarAllocator_003E_002EAssign_003Cclass_0020edLodInfo_0020const_0020_002A_003E(ptr2, (edLodInfo*)(int)(*(uint*)(&obj)), (edLodInfo*)(System.Runtime.CompilerServices.Unsafe.As<arVector_003CedLodInfo_002CarAllocator_003E, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 4)) * 44 + *(int*)(&obj)));
			}
			ManagedDialog.GenerateLodListItems(lodCount);
			CurrentLodIndex = ManagedDialog.LodChooserListBox.SelectedIndex;
			RunValidationChecks();
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<arVector_003CedLodInfo_002CarAllocator_003E*, void>)(&global::_003CModule_003E.arVector_003CedLodInfo_002CarAllocator_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.delete((void*)(int)(*(uint*)(&obj)));
	}

	public unsafe void HandleLodListSelectedIndexChanged(object P_0, EventArgs P_1)
	{
		SaveCurrentLodInfoSettings();
		IsSwitchingLods = true;
		int selectedIndex = ManagedDialog.LodChooserListBox.SelectedIndex;
		edLodInfo* ptr = (edLodInfo*)(selectedIndex * 44 + ((int*)Dialog)[44]);
		ManagedDialog.CullDistance = ((float*)ptr)[1];
		bool flag = ((bool*)ptr)[8];
		if (flag)
		{
			ManagedDialog.OverheadResolutionIndex = ((int*)ptr)[3];
			ManagedDialog.TrimRoadsInOverhead = ((bool*)ptr)[16];
			ManagedDialog.GlobalDetailInOverhead = ((bool*)ptr)[17];
		}
		else
		{
			ManagedDialog.OverheadResolutionIndex = -1;
			ManagedDialog.TrimRoadsInOverhead = false;
			ManagedDialog.GlobalDetailInOverhead = false;
		}
		ManagedDialog.OverheadRender = flag;
		ManagedDialog.HeightTolerance = ((float*)ptr)[5];
		ManagedDialog.ShorelineTolerance = ((float*)ptr)[6];
		ManagedDialog.PathTolerance = ((float*)ptr)[7];
		ManagedDialog.ExtrusionTolerance = ((float*)ptr)[8];
		ManagedDialog.MaxSegmentLength = ((float*)ptr)[9];
		ManagedDialog.BridgePostSlices = ((int*)ptr)[10];
		CurrentLodIndex = selectedIndex;
		IsSwitchingLods = false;
	}

	public void HandleOkClick(object P_0, EventArgs P_1)
	{
		SaveCurrentLodInfoSettings();
		ManagedDialog.SetOkResult();
	}

	public void HandleLodValueChanged(object P_0, EventArgs P_1)
	{
		if (!IsSwitchingLods)
		{
			SaveCurrentLodInfoSettings();
			RunValidationChecks();
		}
	}

	private unsafe void SaveCurrentLodInfoSettings()
	{
		int currentLodIndex = CurrentLodIndex;
		if (currentLodIndex != -1)
		{
			edLodInfo* ptr = (edLodInfo*)(currentLodIndex * 44 + ((int*)Dialog)[44]);
			((float*)ptr)[1] = ManagedDialog.CullDistance;
			byte overheadRender = (ManagedDialog.OverheadRender ? ((byte)1) : ((byte)0));
			((sbyte*)ptr)[8] = (sbyte)overheadRender;
			if (overheadRender != 0)
			{
				((int*)ptr)[3] = ManagedDialog.OverheadResolutionIndex;
				((sbyte*)ptr)[16] = (ManagedDialog.TrimRoadsInOverhead ? ((sbyte)1) : ((sbyte)0));
				((sbyte*)ptr)[17] = (ManagedDialog.GlobalDetailInOverhead ? ((sbyte)1) : ((sbyte)0));
			}
			else
			{
				((int*)ptr)[3] = -1;
				((sbyte*)ptr)[16] = 0;
				((sbyte*)ptr)[17] = 0;
			}
			((float*)ptr)[5] = ManagedDialog.HeightTolerance;
			((float*)ptr)[6] = ManagedDialog.ShorelineTolerance;
			((float*)ptr)[7] = ManagedDialog.PathTolerance;
			((float*)ptr)[8] = ManagedDialog.ExtrusionTolerance;
			((float*)ptr)[9] = ManagedDialog.MaxSegmentLength;
			((int*)ptr)[10] = ManagedDialog.BridgePostSlices;
		}
	}

	private void SaveGlobalLodInfoSettings()
	{
	}

	private unsafe void RunValidationChecks()
	{
		ManagedDialog.ClearDialogErrors();
		arVector_003CedLodInfo_002CarAllocator_003E* ptr = (arVector_003CedLodInfo_002CarAllocator_003E*)((byte*)Dialog + 176);
		uint num = ((uint*)ptr)[1];
		if (num < 2)
		{
			return;
		}
		int num2 = *(int*)ptr;
		float num3 = *(float*)(num2 + 4);
		uint num4 = 1u;
		if (1 >= num)
		{
			return;
		}
		int num5 = num2 + 48;
		while (!(*(float*)num5 < num3))
		{
			num3 = *(float*)num5;
			num4++;
			num5 += 44;
			if (num4 >= num)
			{
				return;
			}
		}
		ManagedDialog.PostDialogError("Transition Distances are not increasing.");
	}
}
