using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace BugReporter;

public class MapiMailMessage
{
	[StructLayout(LayoutKind.Sequential)]
	private class MapiFileDescriptor
	{
		public int reserved;

		public int flags;

		public int position;

		public string path;

		public string name;

		public IntPtr type = IntPtr.Zero;
	}

	public enum RecipientType
	{
		To = 1,
		CC,
		BCC
	}

	internal class MAPIHelperInterop
	{
		[StructLayout(LayoutKind.Sequential)]
		public class MapiMessage
		{
			public int Reserved;

			public string Subject;

			public string NoteText;

			public string MessageType;

			public string DateReceived;

			public string ConversationID;

			public int Flags;

			public IntPtr Originator = IntPtr.Zero;

			public int RecipientCount;

			public IntPtr Recipients = IntPtr.Zero;

			public int FileCount;

			public IntPtr Files = IntPtr.Zero;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class MapiRecipDesc
		{
			public int Reserved;

			public int RecipientClass;

			public string Name;

			public string Address;

			public int eIDSize;

			public IntPtr EntryID = IntPtr.Zero;
		}

		public const int MAPI_LOGON_UI = 1;

		private MAPIHelperInterop()
		{
		}

		[DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
		public static extern int MAPILogon(IntPtr hwnd, string prf, string pw, int flg, int rsv, ref IntPtr sess);

		[DllImport("MAPI32.DLL")]
		public static extern int MAPISendMail(IntPtr session, IntPtr hwnd, MapiMessage message, int flg, int rsv);
	}

	private string _subject;

	private string _body;

	private RecipientCollection _recipientCollection;

	private ArrayList _files;

	private ManualResetEvent _manualResetEvent;

	public string Subject
	{
		get
		{
			return _subject;
		}
		set
		{
			_subject = value;
		}
	}

	public string Body
	{
		get
		{
			return _body;
		}
		set
		{
			_body = value;
		}
	}

	public RecipientCollection Recipients => _recipientCollection;

	public ArrayList Files => _files;

	public MapiMailMessage()
	{
		_files = new ArrayList();
		_recipientCollection = new RecipientCollection();
		_manualResetEvent = new ManualResetEvent(initialState: false);
	}

	public MapiMailMessage(string subject)
		: this()
	{
		_subject = subject;
	}

	public MapiMailMessage(string subject, string body)
		: this()
	{
		_subject = subject;
		_body = body;
	}

	public void ShowDialog(bool wait)
	{
		Thread thread = new Thread((ThreadStart)_ShowMail);
		thread.IsBackground = true;
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
		_manualResetEvent.WaitOne();
		_manualResetEvent.Reset();
		if (wait)
		{
			thread.Join();
		}
	}

	private void _ShowMail(object ignore)
	{
		MAPIHelperInterop.MapiMessage mapiMessage = new MAPIHelperInterop.MapiMessage();
		using RecipientCollection.InteropRecipientCollection interopRecipientCollection = _recipientCollection.GetInteropRepresentation();
		mapiMessage.Subject = _subject;
		mapiMessage.NoteText = _body;
		mapiMessage.Recipients = interopRecipientCollection.Handle;
		mapiMessage.RecipientCount = _recipientCollection.Count;
		if (_files.Count > 0)
		{
			mapiMessage.Files = _AllocAttachments(out mapiMessage.FileCount);
		}
		_manualResetEvent.Set();
		int num = MAPIHelperInterop.MAPISendMail(IntPtr.Zero, IntPtr.Zero, mapiMessage, 8, 0);
		if (_files.Count > 0)
		{
			_DeallocFiles(mapiMessage);
		}
		if (num != 0)
		{
			_LogErrorMapi(num);
		}
	}

	private void _DeallocFiles(MAPIHelperInterop.MapiMessage message)
	{
		if (message.Files != IntPtr.Zero)
		{
			Type typeFromHandle = typeof(MapiFileDescriptor);
			int num = Marshal.SizeOf(typeFromHandle);
			int num2 = (int)message.Files;
			for (int i = 0; i < message.FileCount; i++)
			{
				Marshal.DestroyStructure((IntPtr)num2, typeFromHandle);
				num2 += num;
			}
			Marshal.FreeHGlobal(message.Files);
		}
	}

	private IntPtr _AllocAttachments(out int fileCount)
	{
		fileCount = 0;
		if (_files == null)
		{
			return IntPtr.Zero;
		}
		if (_files.Count <= 0 || _files.Count > 100)
		{
			return IntPtr.Zero;
		}
		Type typeFromHandle = typeof(MapiFileDescriptor);
		int num = Marshal.SizeOf(typeFromHandle);
		IntPtr intPtr = Marshal.AllocHGlobal(_files.Count * num);
		MapiFileDescriptor mapiFileDescriptor = new MapiFileDescriptor();
		mapiFileDescriptor.position = -1;
		int num2 = (int)intPtr;
		for (int i = 0; i < _files.Count; i++)
		{
			string path = _files[i] as string;
			mapiFileDescriptor.name = Path.GetFileName(path);
			mapiFileDescriptor.path = path;
			Marshal.StructureToPtr(mapiFileDescriptor, (IntPtr)num2, fDeleteOld: false);
			num2 += num;
		}
		fileCount = _files.Count;
		return intPtr;
	}

	private void _ShowMail()
	{
		_ShowMail(null);
	}

	private void _LogErrorMapi(int errorCode)
	{
		_ = string.Empty;
		switch (errorCode)
		{
		}
	}
}
