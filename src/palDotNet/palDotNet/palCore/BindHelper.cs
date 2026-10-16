using System;
using System.Reflection;

namespace palDotNet.palCore;

public class BindHelper
{
	protected object src;

	protected string srcProp;

	protected string srcEvent;

	protected object dest;

	protected string destProp;

	protected string destEvent;

	protected PropertyInfo srcPropInfo;

	protected PropertyInfo destPropInfo;

	public object Src => src;

	public string SrcProp => srcProp;

	public object Dest => dest;

	public string DestProp => destProp;

	public BindHelper(object src, string srcProp, string srcEvent, object dest, string destProp, string destEvent)
	{
		this.src = src;
		this.srcProp = srcProp;
		this.srcEvent = srcEvent;
		this.dest = dest;
		this.destProp = destProp;
		this.destEvent = destEvent;
		Type type = src.GetType();
		Type type2 = dest.GetType();
		try
		{
			srcPropInfo = type.GetProperty(srcProp, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			if (srcPropInfo == null)
			{
				srcPropInfo = type.GetProperty(srcProp);
			}
			destPropInfo = type2.GetProperty(destProp, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			if (destPropInfo == null)
			{
				destPropInfo = type2.GetProperty(destProp);
			}
		}
		catch (Exception)
		{
		}
	}

	public void ClearBind()
	{
		Type type = src.GetType();
		Type type2 = dest.GetType();
		EventInfo eventInfo = type.GetEvent(srcEvent);
		EventInfo eventInfo2 = type2.GetEvent(destEvent);
		eventInfo.RemoveEventHandler(src, new EventHandler(SourceChanged));
		eventInfo2.RemoveEventHandler(dest, new EventHandler(DestinationChanged));
	}

	public static BindHelper Bind(object src, string srcProp, string srcEvent, object dest, string destProp, string destEvent)
	{
		Type type = src.GetType();
		Type type2 = dest.GetType();
		EventInfo eventInfo = type.GetEvent(srcEvent);
		EventInfo eventInfo2 = type2.GetEvent(destEvent);
		if (eventInfo == null || eventInfo2 == null)
		{
			return null;
		}
		BindHelper bindHelper = new BindHelper(src, srcProp, srcEvent, dest, destProp, destEvent);
		eventInfo.AddEventHandler(src, new EventHandler(bindHelper.SourceChanged));
		eventInfo2.AddEventHandler(dest, new EventHandler(bindHelper.DestinationChanged));
		_ = eventInfo2.IsMulticast;
		bindHelper.DestinationChanged(bindHelper, EventArgs.Empty);
		return bindHelper;
	}

	private void SourceChanged(object sender, EventArgs e)
	{
		if (destPropInfo != null && srcPropInfo != null)
		{
			object value = srcPropInfo.GetValue(src, null);
			destPropInfo.SetValue(dest, value, null);
		}
	}

	private void DestinationChanged(object sender, EventArgs e)
	{
		if (destPropInfo != null && srcPropInfo != null)
		{
			object value = destPropInfo.GetValue(dest, null);
			srcPropInfo.SetValue(src, value, null);
		}
	}

	private void OnLeave(object sender, EventArgs e)
	{
		if (destPropInfo != null && srcPropInfo != null)
		{
			object value = srcPropInfo.GetValue(src, null);
			destPropInfo.SetValue(dest, value, null);
		}
	}
}
