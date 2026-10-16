using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace palDotNet.palCore;

public class SerializerNetworkReader : SerializerReader
{
	private Socket S;

	private bool OwnsSocket;

	public override bool AtEnd
	{
		get
		{
			if (Socket.Connected)
			{
				return !Socket.Poll(0, SelectMode.SelectRead);
			}
			return true;
		}
	}

	public Socket Socket => S;

	public bool Connect(string hostname, ushort port)
	{
		S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		if (Socket == null)
		{
			return false;
		}
		OwnsSocket = true;
		IPHostEntry hostEntry = Dns.GetHostEntry(hostname);
		bool flag = false;
		IPAddress[] addressList = hostEntry.AddressList;
		foreach (IPAddress address in addressList)
		{
			Socket.Connect(new IPEndPoint(address, port));
			if (Socket.Connected)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return false;
		}
		return true;
	}

	public bool Connect(SerializerNetworkReader reader)
	{
		if (reader.Socket == null)
		{
			return false;
		}
		S = reader.Socket;
		OwnsSocket = false;
		return true;
	}

	public bool Connect(SerializerNetworkWriter writer)
	{
		if (writer.Socket == null)
		{
			return false;
		}
		S = writer.Socket;
		OwnsSocket = false;
		return true;
	}

	public bool Accept(ushort port)
	{
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPEndPoint localEP = new IPEndPoint(IPAddress.Any, port);
		socket.Bind(localEP);
		socket.Listen(1);
		S = socket.Accept();
		OwnsSocket = true;
		socket.Close();
		if (S == null)
		{
			return false;
		}
		return true;
	}

	public void Close()
	{
		if (OwnsSocket)
		{
			S.Shutdown(SocketShutdown.Both);
			S.Close();
		}
	}

	protected override byte[] ReadBytes(int numBytes)
	{
		byte[] array = new byte[numBytes];
		int num = 0;
		while (true)
		{
			num += Socket.Receive(array, num, numBytes - num, SocketFlags.None);
			if (num == numBytes)
			{
				break;
			}
			System.Threading.Thread.Sleep(1);
		}
		return array;
	}

	public override void Serialize(ref string inst)
	{
		List<byte> list = new List<byte>();
		byte[] array = new byte[1];
		while (true)
		{
			if (Socket.Receive(array) != array.Length)
			{
				throw new ApplicationException("Num bytes received not equal to # requested");
			}
			if (array[0] == 0)
			{
				break;
			}
			list.Add(array[0]);
		}
		inst = Encoding.ASCII.GetString(list.ToArray());
	}

	public override bool Begin()
	{
		return true;
	}

	public override void End(bool clearIndex)
	{
	}
}
