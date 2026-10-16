using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
public class SplitButton : Menu
{
	public enum EnumSplitButtonMessages
	{
		eSetDefaultOption = 166,
		eLastSplitButtonMsg = eSetDefaultOption
	}

	public delegate void DefaultOptionDel(ISupportsMenus itemToSelect);

	public DefaultOptionDel DefaultOptionDelegate;

	public SplitButton(uint newId)
		: base(newId)
	{
	}

	public override void Serialize(ISerializer serializer, uint message)
	{
		if (message == 166)
		{
			WidgetBase inst = null;
			serializer.Serialize(ref inst);
			ISupportsMenus supportsMenus = inst;
			if (serializer.Reading && DefaultOptionDelegate != null && supportsMenus != null)
			{
				DefaultOptionDelegate(supportsMenus);
			}
		}
		else
		{
			base.Serialize(serializer, message);
		}
	}
}
