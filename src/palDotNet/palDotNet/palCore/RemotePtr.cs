using System;
using System.Collections.Generic;

namespace palDotNet.palCore;

public struct RemotePtr
{
	public class EqualityComparer : IEqualityComparer<RemotePtr>
	{
		public bool Equals(RemotePtr x, RemotePtr y)
		{
			if (Use64Bit)
			{
				return x.Value64 == y.Value64;
			}
			return x.Value32 == y.Value32;
		}

		public int GetHashCode(RemotePtr x)
		{
			if (!Use64Bit)
			{
				return x.Value32;
			}
			return (int)x.Value64;
		}
	}

	public static bool Use64Bit;

	public int Value32;

	public long Value64;

	private static uint CurrentId;

	public int Id => Value32;

	public RemotePtr(IntPtr ptr)
	{
		if (Use64Bit)
		{
			Value64 = ptr.ToInt64();
			Value32 = (int)CurrentId++;
		}
		else
		{
			Value64 = 0L;
			Value32 = ptr.ToInt32();
		}
	}

	public void SetId()
	{
		if (Use64Bit)
		{
			Value32 = (int)CurrentId++;
		}
	}

	public override string ToString()
	{
		if (Use64Bit)
		{
			return Value64.ToString();
		}
		return Value32.ToString();
	}

	public static implicit operator IntPtr(RemotePtr ptr)
	{
		if (Use64Bit)
		{
			return new IntPtr(ptr.Value64);
		}
		return new IntPtr(ptr.Value32);
	}

	public void Serialize(ISerializer serializer)
	{
		if (Use64Bit)
		{
			serializer.Serialize(ref Value64);
		}
		else
		{
			serializer.Serialize(ref Value32);
		}
	}
}
