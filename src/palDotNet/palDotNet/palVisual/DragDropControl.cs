using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class DragDropControl : UserControl
{
	private DateTime LastDragTime;

	private bool FirstDrag;

	protected DragDropData DragDropData;

	private IContainer components;

	public DragDropControl()
	{
		InitializeComponent();
	}

	protected void HandleDragOver(bool checkHover, DragEventArgs drgevent)
	{
		DragDropHelper(dragFinished: false, drgevent);
		DateTime now = DateTime.Now;
		if (checkHover)
		{
			LastDragTime = now;
		}
		else if (now.Subtract(LastDragTime).TotalSeconds > 1.100000023841858)
		{
			DoDragHover();
			LastDragTime = now;
		}
	}

	protected bool DragDropSendMessage(WidgetBase widget, DragEventArgs e, bool dragFinished, bool sendCheck, string toItemText)
	{
		DragDropData dragDropData = e.Data.GetData("palDotNet.palWidgets.DragDropData") as DragDropData;
		bool result = false;
		string[] array = null;
		string text = null;
		int remoteId = 0;
		if (dragDropData == null)
		{
			array = e.Data.GetData(DataFormats.FileDrop) as string[];
			text = "<FileName>";
		}
		else
		{
			array = new string[dragDropData.FromTexts.Length];
			for (int i = 0; i < dragDropData.FromTexts.Length; i++)
			{
				array[i] = dragDropData.FromTexts[i];
			}
			text = dragDropData.FromType;
			remoteId = dragDropData.From.GetHashCode();
		}
		if (array != null && (dragFinished || sendCheck || FirstDrag))
		{
			WidgetBase.CoreMessages msg = (dragFinished ? WidgetBase.CoreMessages.eDragDropItem : WidgetBase.CoreMessages.eDragDropTargetValid);
			bool flag = widget.SendDragDrop(remoteId, text, array, toItemText, msg);
			e.Effect = (flag ? DragDropEffects.Move : DragDropEffects.None);
			result = true;
		}
		FirstDrag = false;
		return result;
	}

	protected virtual void DragDropHelper(bool dragFinished, DragEventArgs e)
	{
	}

	protected virtual void StartDragging()
	{
		FirstDrag = true;
		DoDragDrop(DragDropData, DragDropEffects.Move);
	}

	protected virtual void DoDragHover()
	{
	}

	protected void DragDropControl_DragEnter(object sender, DragEventArgs e)
	{
		FirstDrag = true;
		LastDragTime = DateTime.Now;
	}

	protected void DragDropControl_DragDrop(object sender, DragEventArgs e)
	{
		DragDropHelper(dragFinished: true, e);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Name = "DragDropControl";
		base.ResumeLayout(false);
	}
}
