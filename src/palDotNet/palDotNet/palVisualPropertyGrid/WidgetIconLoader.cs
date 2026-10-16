using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using VisualHint.SmartPropertyGrid;
using palDotNet.palWidgets;

namespace palDotNet.palVisualPropertyGrid;

public abstract class WidgetIconLoader : Widget<string>
{
	public interface IBackgroundTaskState
	{
		string SelectedItem { get; set; }

		string[] BasePaths { get; set; }

		bool RequestCancel { get; set; }

		int PropertyHeight { get; set; }
	}

	private delegate void WaitCallbackCompleteDel(ImageList imageList, string selectedItem, int size, uint finalMultiplier);

	protected object StateLockObj = new object();

	protected IBackgroundTaskState CurrentState;

	public WidgetIconLoader(WidgetBase widget, PropertyGrid grid, PropertyEnumerator gridEnum, Attribute[] attrs)
		: base(widget, grid, gridEnum, attrs)
	{
		IconLoader obj = TheWidget as IconLoader;
		EventHandler eventUpdateView = obj.EventUpdateView;
		EventHandler b = delegate
		{
			UpdateImage();
		};
		obj.EventUpdateView = (EventHandler)Delegate.Combine(eventUpdateView, b);
		UpdateImage();
	}

	protected override void OnWidgetValueChanged(object sender, EventArgs e)
	{
		UpdateImage();
		base.OnWidgetValueChanged(sender, e);
	}

	private void WaitCallback(object stateObj)
	{
		IBackgroundTaskState backgroundTaskState = (IBackgroundTaskState)stateObj;
		bool flag;
		lock (StateLockObj)
		{
			flag = backgroundTaskState.SelectedItem != TheValue;
		}
		if (flag)
		{
			return;
		}
		uint multiplier = (TheWidget as IconLoader).HeightMultiplier;
		Image image = GetImage(backgroundTaskState, ref multiplier);
		int propertyHeight = backgroundTaskState.PropertyHeight;
		int num = (int)(propertyHeight * Math.Max(multiplier, 1u));
		if (!backgroundTaskState.RequestCancel)
		{
			ImageList imageList = null;
			if (image != null)
			{
				imageList = new ImageList();
				imageList.ImageSize = new Size(num, num);
				imageList.ColorDepth = ColorDepth.Depth32Bit;
				imageList.Images.Add(image);
			}
			object[] args = new object[4] { imageList, backgroundTaskState.SelectedItem, num, multiplier };
			WaitCallbackCompleteDel method = WaitCallbackComplete;
			base.PropEnum.Property.ParentGrid.BeginInvoke(method, args);
		}
	}

	private void WaitCallbackComplete(ImageList imageList, string selectedItem, int size, uint finalMultiplier)
	{
		lock (StateLockObj)
		{
			if (!TheWidget.ValueVisible)
			{
				base.PropEnum.Property.Value.ImageList = null;
				base.PropEnum.Property.Value.ImageIndex = -1;
			}
			else if (TheValue == selectedItem)
			{
				base.PropEnum.Property.ParentGrid.BeginUpdate();
				bool flag = base.PropEnum.Property.HeightMultiplier != finalMultiplier;
				if (flag)
				{
					base.PropEnum.Property.HeightMultiplier = (int)finalMultiplier;
				}
				if (imageList != null)
				{
					base.PropEnum.Property.Value.ImageList = imageList;
					base.PropEnum.Property.Value.ImageIndex = 0;
				}
				else
				{
					base.PropEnum.Property.Value.ImageList = null;
					base.PropEnum.Property.Value.ImageIndex = -1;
				}
				base.PropEnum.Property.Value.ImageSize = new Size(size, size);
				base.PropEnum.Property.ParentGrid.EndUpdate();
				if (flag)
				{
					base.PropEnum.Property.ParentGrid.Invalidate();
					base.PropEnum.Property.ParentGrid.Refresh();
				}
				else
				{
					base.PropEnum.Property.InvalidateProperty(children: true);
				}
			}
		}
	}

	private void UpdateImage()
	{
		lock (StateLockObj)
		{
			uint heightMultiplier = (TheWidget as IconLoader).HeightMultiplier;
			base.PropEnum.Property.HeightMultiplier = (int)heightMultiplier;
			if (CurrentState != null)
			{
				CurrentState.RequestCancel = true;
			}
		}
		CurrentState = InstantiateState();
		CurrentState.SelectedItem = TheValue;
		CurrentState.PropertyHeight = base.PropEnum.Property.ParentGrid.BasicPropertyHeight - base.PropEnum.Property.ParentGrid.PropertyVerticalMargin;
		ThreadPool.QueueUserWorkItem(WaitCallback, CurrentState);
	}

	protected abstract IBackgroundTaskState InstantiateState();

	protected abstract Image GetImage(IBackgroundTaskState state, ref uint multiplier);
}
