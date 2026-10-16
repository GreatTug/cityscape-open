using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace BugReporter;

public class RecipientCollection : CollectionBase
{
	internal struct InteropRecipientCollection : IDisposable
	{
		private IntPtr _handle;

		private int _count;

		public IntPtr Handle => _handle;

		public InteropRecipientCollection(RecipientCollection outer)
		{
			_count = outer.Count;
			if (_count == 0)
			{
				_handle = IntPtr.Zero;
				return;
			}
			int num = Marshal.SizeOf(typeof(MapiMailMessage.MAPIHelperInterop.MapiRecipDesc));
			_handle = Marshal.AllocHGlobal(_count * num);
			int num2 = (int)_handle;
			foreach (Recipient item in outer)
			{
				MapiMailMessage.MAPIHelperInterop.MapiRecipDesc interopRepresentation = item.GetInteropRepresentation();
				Marshal.StructureToPtr(interopRepresentation, (IntPtr)num2, fDeleteOld: false);
				num2 += num;
			}
		}

		public void Dispose()
		{
			if (_handle != IntPtr.Zero)
			{
				Type typeFromHandle = typeof(MapiMailMessage.MAPIHelperInterop.MapiRecipDesc);
				int num = Marshal.SizeOf(typeFromHandle);
				int num2 = (int)_handle;
				for (int i = 0; i < _count; i++)
				{
					Marshal.DestroyStructure((IntPtr)num2, typeFromHandle);
					num2 += num;
				}
				Marshal.FreeHGlobal(_handle);
				_handle = IntPtr.Zero;
				_count = 0;
			}
		}
	}

	public Recipient this[int index] => (Recipient)base.List[index];

	public void Add(Recipient value)
	{
		base.List.Add(value);
	}

	public void Add(string address)
	{
		Add(new Recipient(address));
	}

	public void Add(string address, string displayName)
	{
		Add(new Recipient(address, displayName));
	}

	public void Add(string address, MapiMailMessage.RecipientType recipientType)
	{
		Add(new Recipient(address, recipientType));
	}

	public void Add(string address, string displayName, MapiMailMessage.RecipientType recipientType)
	{
		Add(new Recipient(address, displayName, recipientType));
	}

	internal InteropRecipientCollection GetInteropRepresentation()
	{
		return new InteropRecipientCollection(this);
	}
}
