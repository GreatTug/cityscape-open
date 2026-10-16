using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace palDotNet.palCore;

public class SerializerMemoryStreamWriter : SerializerWriter
{
	public unsafe uint* BufferSize = null;

	public unsafe uint* CurrentIndex = null;

	public unsafe byte** Buffer = null;

	private IntPtr _defaultBuffer = IntPtr.Zero;
	private IntPtr _defaultBufferPtr = IntPtr.Zero;
	private IntPtr _defaultBufferSize = IntPtr.Zero;
	private IntPtr _defaultCurrentIndex = IntPtr.Zero;

	private int TheEnterCount;

	public unsafe SerializerMemoryStreamWriter()
	{
		_defaultBuffer = Marshal.AllocHGlobal(65536);
		_defaultBufferPtr = Marshal.AllocHGlobal(IntPtr.Size);
		Marshal.WriteIntPtr(_defaultBufferPtr, _defaultBuffer);
		Buffer = (byte**)(void*)_defaultBufferPtr;

		_defaultBufferSize = Marshal.AllocHGlobal(sizeof(uint));
		*(uint*)(void*)_defaultBufferSize = 65536u;
		BufferSize = (uint*)(void*)_defaultBufferSize;

		_defaultCurrentIndex = Marshal.AllocHGlobal(sizeof(uint));
		*(uint*)(void*)_defaultCurrentIndex = 0u;
		CurrentIndex = (uint*)(void*)_defaultCurrentIndex;
	}

	public override bool AtEnd => false;

	public override int EnterCount => TheEnterCount;

	[DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
	private unsafe static extern int CopyMemory(IntPtr Destination, byte* Source, int Length);

	protected unsafe override void WriteBytes(byte[] bytes)
	{
		if (BufferSize == null || CurrentIndex == null || Buffer == null || *Buffer == null)
		{
			return;
		}
		uint bufferSize = *BufferSize;
		uint currentIndex = *CurrentIndex;
		if (currentIndex + bytes.Length >= bufferSize)
		{
			*BufferSize = (uint)Math.Max(bufferSize * 2, bufferSize + bytes.Length);
			IntPtr intPtr = Marshal.AllocHGlobal((int)(*BufferSize));
			CopyMemory(intPtr, *Buffer, (int)bufferSize);
			byte* buffer = *Buffer;
			Marshal.FreeHGlobal((IntPtr)buffer);
			*Buffer = (byte*)(void*)intPtr;
		}
		byte* buffer2 = *Buffer;
		Marshal.Copy(bytes, 0, (IntPtr)(buffer2 + (int)currentIndex), bytes.Length);
		*CurrentIndex = currentIndex + (uint)bytes.Length;
	}

	public unsafe override bool Begin()
	{
		Monitor.Enter(this);
		TheEnterCount++;
		if (CurrentIndex == null)
		{
			return true;
		}
		return *CurrentIndex == 0;
	}

	public unsafe override void End(bool clearIndex)
	{
		TheEnterCount--;
		if (clearIndex && CurrentIndex != null)
		{
			*CurrentIndex = 0u;
		}
		Monitor.Exit(this);
	}
}
