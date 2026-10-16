using System;
using System.Drawing;
using System.IO;
using VisualHint.SmartPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public class WidgetGenericLoader : WidgetIconLoader
{
	private class BackgroundTaskState : IBackgroundTaskState
	{
		public string TheSelectedItem;

		private bool TheRequestCancel;

		private int ThePropertyHeight;

		private string[] TheBasePaths = new string[0];

		public byte[] BitmapBytes;

		private object SyncObject = new object();

		public string SelectedItem
		{
			get
			{
				lock (SyncObject)
				{
					return TheSelectedItem;
				}
			}
			set
			{
				lock (SyncObject)
				{
					TheSelectedItem = value;
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

	public WidgetGenericLoader(WidgetBase widget, PropertyGrid grid, PropertyEnumerator gridEnum, Attribute[] attrs)
		: base(widget, grid, gridEnum, attrs)
	{
	}

	protected override IBackgroundTaskState InstantiateState()
	{
		BackgroundTaskState backgroundTaskState = new BackgroundTaskState();
		byte[] bitmapBytes = (TheWidget as GenericLoader).BitmapBytes;
		if (bitmapBytes != null)
		{
			backgroundTaskState.BitmapBytes = new byte[bitmapBytes.Length];
			bitmapBytes.CopyTo(backgroundTaskState.BitmapBytes, 0);
		}
		return backgroundTaskState;
	}

	protected override Image GetImage(IBackgroundTaskState state, ref uint multiplier)
	{
		byte[] bitmapBytes = ((BackgroundTaskState)state).BitmapBytes;
		Image result = null;
		if (bitmapBytes != null)
		{
			using MemoryStream stream = new MemoryStream(bitmapBytes);
			result = new Bitmap(stream);
		}
		return result;
	}
}
