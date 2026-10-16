using System;
using System.Text;
using palDotNet.palWidgets;

namespace palDotNet.palCore;

public class SerializerWriter : ISerializer
{
	public WidgetMgr TheBankMgr;

	public virtual bool AtEnd
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool Reading => false;

	public bool Writing => true;

	public virtual int EnterCount
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	protected virtual void WriteBytes(byte[] bytes)
	{
		throw new NotImplementedException();
	}

	public void Serialize(ref byte[] bytes, int count)
	{
		WriteBytes(bytes);
	}

	public void Serialize(ref char inst)
	{
		byte[] bytes = new byte[1] { (byte)inst };
		WriteBytes(bytes);
	}

	public void Serialize(ref int inst)
	{
		WriteBytes(BitConverter.GetBytes(inst));
	}

	public void Serialize(ref uint inst)
	{
		WriteBytes(BitConverter.GetBytes(inst));
	}

	public void Serialize(ref long inst)
	{
		WriteBytes(BitConverter.GetBytes(inst));
	}

	public void Serialize(ref ulong inst)
	{
		WriteBytes(BitConverter.GetBytes(inst));
	}

	public void Serialize(ref bool inst)
	{
		WriteBytes(BitConverter.GetBytes(inst));
	}

	public void Serialize(ref float inst)
	{
		WriteBytes(BitConverter.GetBytes(inst));
	}

	public void Serialize(ref Vector3 inst)
	{
		WriteBytes(BitConverter.GetBytes(inst.x));
		WriteBytes(BitConverter.GetBytes(inst.y));
		WriteBytes(BitConverter.GetBytes(inst.z));
	}

	public void Serialize(ref Vector4 inst)
	{
		WriteBytes(BitConverter.GetBytes(inst.x));
		WriteBytes(BitConverter.GetBytes(inst.y));
		WriteBytes(BitConverter.GetBytes(inst.z));
		WriteBytes(BitConverter.GetBytes(inst.w));
	}

	public void Serialize(ref string inst)
	{
		if (inst != null)
		{
			WriteBytes(Encoding.ASCII.GetBytes(inst));
		}
		byte[] bytes = new byte[1];
		WriteBytes(bytes);
	}

	public void SerializeArString(ref string inst)
	{
		if (inst != null)
		{
			WriteBytes(BitConverter.GetBytes(inst.Length));
			WriteBytes(Encoding.ASCII.GetBytes(inst));
		}
		else
		{
			WriteBytes(BitConverter.GetBytes(0));
		}
	}

	public void Serialize(ref WidgetBase inst)
	{
		TheBankMgr.MapLocalToRemote(inst).Serialize(this);
	}

	public void Serialize(ref Bank inst)
	{
		WidgetBase inst2 = inst;
		Serialize(ref inst2);
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
