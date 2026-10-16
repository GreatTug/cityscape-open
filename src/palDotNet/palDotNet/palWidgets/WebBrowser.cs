using palDotNet.palCore;

namespace palDotNet.palWidgets;

[WidgetCreator]
internal class WebBrowser : WidgetPanel, ISupportsMenus
{
	public enum Messages
	{
		eWebAddress = 168,
		eShowContextMenu = 169,
		eLastRenderWindowMsg = eShowContextMenu
	}

	private ISupportsMenus MenuSupporter;

	private IMenuItem MenuItem;

	public IWebBrowserVisual Visual { private get; set; }

	public WebBrowser(uint newId)
		: base(newId)
	{
	}

	public override object MapMenuToVisual(ISupportsMenus menu)
	{
		if (menu == MenuSupporter && MenuItem != null)
		{
			return MenuItem.MenuItem;
		}
		return null;
	}

	public override void AddMapMenuToVisual(ISupportsMenus menu, IMenuItem visual)
	{
		MenuSupporter = menu;
		MenuItem = visual;
	}

	public override bool RemoveMapMenuToVisual(ISupportsMenus menu)
	{
		if (MenuSupporter == menu)
		{
			MenuSupporter = null;
			MenuItem = null;
			return true;
		}
		return false;
	}

	public override void Serialize(ISerializer serializer, uint msgType)
	{
		switch (msgType)
		{
		case 168u:
			if (serializer.Reading)
			{
				string inst = "";
				serializer.Serialize(ref inst);
				if (Visual != null)
				{
					Visual.Navigate(inst, showIfHidden: true);
				}
			}
			break;
		case 169u:
			if (serializer.Reading && MenuItem != null)
			{
				MenuItem.Show();
			}
			break;
		default:
			base.Serialize(serializer, msgType);
			break;
		}
	}
}
