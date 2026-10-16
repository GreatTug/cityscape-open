using System;
using System.Drawing;
using System.IO;
using VisualHint.SmartPropertyGrid;
using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public class WidgetFileLoader : WidgetIconLoader
{
	private class BackgroundTaskState : IBackgroundTaskState
	{
		public string FileRequested;

		private bool TheRequestCancel;

		private int ThePropertyHeight;

		private string[] TheBasePaths = new string[0];

		private object SyncObject = new object();

		public string SelectedItem
		{
			get
			{
				lock (SyncObject)
				{
					return FileRequested;
				}
			}
			set
			{
				lock (SyncObject)
				{
					FileRequested = value;
				}
			}
		}

		public string[] BasePaths
		{
			get
			{
				lock (SyncObject)
				{
					return TheBasePaths;
				}
			}
			set
			{
				lock (SyncObject)
				{
					TheBasePaths = value;
				}
			}
		}

		public bool RequestCancel
		{
			get
			{
				lock (SyncObject)
				{
					return TheRequestCancel;
				}
			}
			set
			{
				lock (SyncObject)
				{
					TheRequestCancel = value;
				}
			}
		}

		public int PropertyHeight
		{
			get
			{
				lock (SyncObject)
				{
					return ThePropertyHeight;
				}
			}
			set
			{
				lock (SyncObject)
				{
					ThePropertyHeight = value;
				}
			}
		}
	}

	private bool InvalidPath;

	public override string WidgetValue
	{
		get
		{
			string result = TheValue;
			lock (StateLockObj)
			{
				try
				{
					result = (((TheWidget as FileLoader).FileNameOnly && !InvalidPath) ? Path.GetFileName(TheValue) : TheValue);
				}
				catch (Exception)
				{
				}
			}
			return result;
		}
		set
		{
			InvalidPath = false;
			if (value == null)
			{
				InvalidPath = true;
			}
			else
			{
				char[] invalidPathChars = Path.GetInvalidPathChars();
				foreach (char value2 in invalidPathChars)
				{
					if (value.IndexOf(value2) >= 0)
					{
						InvalidPath = true;
						break;
					}
				}
			}
			lock (StateLockObj)
			{
				base.WidgetValue = value;
			}
		}
	}

	public string FullPathValue
	{
		get
		{
			lock (StateLockObj)
			{
				return TheValue;
			}
		}
		set
		{
			lock (StateLockObj)
			{
				WidgetValue = value;
			}
		}
	}

	protected override string VisualBindName => "FullPathValue";

	public WidgetFileLoader(WidgetBase widget, PropertyGrid grid, PropertyEnumerator gridEnum, Attribute[] attrs)
		: base(widget, grid, gridEnum, attrs)
	{
	}

	protected override IBackgroundTaskState InstantiateState()
	{
		IBackgroundTaskState backgroundTaskState = new BackgroundTaskState();
		if (TheWidget is FileLoader fileLoader)
		{
			backgroundTaskState.BasePaths = fileLoader.BasePaths;
		}
		return backgroundTaskState;
	}

	protected override Image GetImage(IBackgroundTaskState istate, ref uint multiplier)
	{
		BackgroundTaskState backgroundTaskState = (BackgroundTaskState)istate;
		string text = backgroundTaskState.FileRequested;
		Image image = null;
		if (!InvalidPath)
		{
			bool flag = false;
			if (!Path.IsPathRooted(text))
			{
				string[] basePaths = backgroundTaskState.BasePaths;
				foreach (string path in basePaths)
				{
					string text2 = Path.Combine(path, text);
					if (File.Exists(text2))
					{
						flag = true;
						text = text2;
						break;
					}
				}
			}
			if (flag || File.Exists(text))
			{
				string extension = Path.GetExtension(text);
				if (extension.ToLower() != ".dds")
				{
					try
					{
						image = Image.FromFile(text);
					}
					catch (Exception)
					{
					}
				}
				if (image == null)
				{
					try
					{
						bool timedOut = false;
						do
						{
							image = FreeImageLoader.Instance.Load(text, 10, out timedOut);
							if (timedOut && backgroundTaskState.RequestCancel)
							{
								return null;
							}
						}
						while (timedOut);
					}
					catch (Exception)
					{
					}
				}
				if (image == null)
				{
					try
					{
						using Icon icon = IconLoaderHelper.GetFileIcon(text, IconLoaderHelper.EnumIconSize.Small, addLinkOverlay: false);
						if (icon != null)
						{
							image = icon.ToBitmap();
							multiplier = 1u;
						}
					}
					catch
					{
						Output.WriteLine("Failed to load icon for '" + text + "'");
					}
				}
			}
		}
		return image;
	}
}
