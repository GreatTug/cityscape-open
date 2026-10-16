using System;
using System.Runtime.InteropServices;
using System.Text;
using palDotNet.palWidgets;

namespace palDotNet.palCore;

public class SerializerReader : ISerializer
{
	public WidgetMgr TheBankMgr;

	public virtual bool AtEnd
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool Reading => true;

	public bool Writing => false;

	public virtual int EnterCount
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	protected virtual byte[] ReadBytes(int numBytes)
	{
		throw new NotImplementedException();
	}

	public void Serialize(ref byte[] bytes, int count)
	{
		bytes = ReadBytes(count);
	}

	public void Serialize(ref char inst)
	{
		inst = (char)ReadBytes(1)[0];
	}

	public void Serialize(ref byte inst)
	{
		inst = ReadBytes(1)[0];
	}

	public void Serialize(ref int inst)
	{
		inst = BitConverter.ToInt32(ReadBytes(Marshal.SizeOf(inst)), 0);
	}

	public void Serialize(ref uint inst)
	{
		inst = BitConverter.ToUInt32(ReadBytes(Marshal.SizeOf(inst)), 0);
	}

	public void Serialize(ref long inst)
	{
		inst = BitConverter.ToInt64(ReadBytes(Marshal.SizeOf(inst)), 0);
	}

	public void Serialize(ref ulong inst)
	{
		inst = BitConverter.ToUInt64(ReadBytes(Marshal.SizeOf(inst)), 0);
	}

	public void Serialize(ref bool inst)
	{
		inst = BitConverter.ToBoolean(ReadBytes(1), 0);
	}

	public void Serialize(ref float inst)
	{
		inst = BitConverter.ToSingle(ReadBytes(Marshal.SizeOf(inst)), 0);
	}

	public void Serialize(ref Vector3 inst)
	{
		inst.x = BitConverter.ToSingle(ReadBytes(Marshal.SizeOf(inst.x)), 0);
		inst.y = BitConverter.ToSingle(ReadBytes(Marshal.SizeOf(inst.y)), 0);
		inst.z = BitConverter.ToSingle(ReadBytes(Marshal.SizeOf(inst.z)), 0);
	}

	public void Serialize(ref Vector4 inst)
	{
		inst.x = BitConverter.ToSingle(ReadBytes(Marshal.SizeOf(inst.x)), 0);
		inst.y = BitConverter.ToSingle(ReadBytes(Marshal.SizeOf(inst.y)), 0);
		inst.z = BitConverter.ToSingle(ReadBytes(Marshal.SizeOf(inst.z)), 0);
		inst.w = BitConverter.ToSingle(ReadBytes(Marshal.SizeOf(inst.w)), 0);
	}

	public void Serialize(ref WidgetBase inst)
	{
		RemotePtr remotePtr = default(RemotePtr);
		remotePtr.Serialize(this);
		inst = TheBankMgr.MapRemoteToLocal(remotePtr);
	}

	public void Serialize(ref Bank inst)
	{
		WidgetBase inst2 = null;
		Serialize(ref inst2);
		inst = inst2 as Bank;
	}

	public virtual void Serialize(ref string inst)
	{
		throw new NotImplementedException();
	}

	public virtual void SerializeArString(ref string inst)
	{
		uint num = 0u;
		num = BitConverter.ToUInt32(ReadBytes(Marshal.SizeOf(num)), 0);
		inst = Encoding.ASCII.GetString(ReadBytes((int)num));
	}

	public virtual bool Begin()
	{
		throw new NotImplementedException();
	}

	public virtual void End(bool clearIndex)
	{
		throw new NotImplementedException();
	}
}
