using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace palDotNet.palCore;

public class SerializerMemoryStreamReader : SerializerReader
{
	public int ReadIndex;

	public unsafe uint* CurrentIndex = null;

	public unsafe byte** Buffer = null;

	private int TheEnterCount;

	public unsafe override bool AtEnd => ReadIndex >= *CurrentIndex;

	public override int EnterCount => TheEnterCount;

	protected unsafe override byte[] ReadBytes(int numBytes)
	{
		byte[] array = new byte[numBytes];
		byte* buffer = *Buffer;
		Marshal.Copy((IntPtr)(buffer + ReadIndex), array, 0, array.Length);
		ReadIndex += array.Length;
		return array;
	}

	public unsafe override void Serialize(ref string inst)
	{
		List<byte> list = new List<byte>();
		byte[] array = new byte[1];
		byte* buffer = *Buffer;
		do
		{
			Marshal.Copy((IntPtr)(buffer + ReadIndex), array, 0, 1);
			ReadIndex++;
			if (array[0] == 0)
			{
				inst = Encoding.ASCII.GetString(list.ToArray());
				break;
			}
			list.Add(array[0]);
		}
		while (ReadIndex < *CurrentIndex);
	}

	public unsafe override bool Begin()
	{
		Monitor.Enter(this);
		TheEnterCount++;
		return *CurrentIndex != 0;
	}

	public unsafe override void End(bool clearIndex)
	{
		if (clearIndex)
		{
			ReadIndex = 0;
			*CurrentIndex = 0u;
		}
		TheEnterCount--;
		Monitor.Exit(this);
	}
}
