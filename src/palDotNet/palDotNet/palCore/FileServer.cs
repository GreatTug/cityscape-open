using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace palDotNet.palCore;

public class FileServer
{
	private static Dictionary<uint, FileServer> FileServers;

	private static SerializerReader ReadStream;

	private static SerializerWriter WriteStream;

	private static string InitialDir;

	private FileStream File;

	private BinaryWriter Writer;

	public static void ClassInit(SerializerReader reader, SerializerWriter writer)
	{
		ReadStream = reader;
		WriteStream = writer;
		FileServers = new Dictionary<uint, FileServer>();
		GetDir();
	}

	private static void GetDir()
	{
		Process process = new Process();
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.FileName = "GetNdEnv";
		if (!process.Start())
		{
			InitialDir = "C:";
			return;
		}
		string text = process.StandardOutput.ReadToEnd();
		process.WaitForExit();
		string[] array = text.Split('\r', '\n');
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			string text3 = text2.Trim(' ', '"');
			string[] array3 = text3.Split('"');
			if (array3[0] == "DvdRoot")
			{
				InitialDir = array3[2];
				break;
			}
		}
	}

	public static void Open()
	{
		string inst = null;
		ReadStream.Serialize(ref inst);
		FileServer value = new FileServer(InitialDir + inst);
		uint inst2 = 0u;
		ReadStream.Serialize(ref inst2);
		FileServers.Add(inst2, value);
	}

	public static void Write()
	{
		uint inst = 0u;
		ReadStream.Serialize(ref inst);
		FileServer value = null;
		FileServers.TryGetValue(inst, out value);
		uint inst2 = 0u;
		ReadStream.Serialize(ref inst2);
		byte[] bytes = null;
		ReadStream.Serialize(ref bytes, (int)inst2);
		if (value != null && bytes != null)
		{
			value.Writer.Write(bytes);
		}
	}

	public static void Close()
	{
		uint inst = 0u;
		ReadStream.Serialize(ref inst);
		FileServer value = null;
		if (FileServers.TryGetValue(inst, out value))
		{
			value.Writer.Flush();
			value.Writer.Close();
			value.File.Close();
			FileServers.Remove(inst);
		}
	}

	public FileServer(string fileName)
	{
		File = new FileStream(fileName, FileMode.Create, FileAccess.Write);
		Writer = new BinaryWriter(File);
	}
}
