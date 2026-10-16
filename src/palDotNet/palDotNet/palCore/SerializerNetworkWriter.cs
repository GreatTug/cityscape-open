using System.Net;
using System.Net.Sockets;

namespace palDotNet.palCore;

public class SerializerNetworkWriter : SerializerWriter
{
	private Socket S;

	private bool OwnsSocket;

	public override bool AtEnd
	{
		get
		{
			if (Socket.Connected)
			{
				return !Socket.Poll(0, SelectMode.SelectWrite);
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
		if (socket == null)
		{
			return false;
		}
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

	protected override void WriteBytes(byte[] bytes)
	{
		Socket.Send(bytes);
	}

	public override bool Begin()
	{
		return true;
	}

	public override void End(bool clearIndex)
	{
	}
}
