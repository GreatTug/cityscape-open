namespace palDotNet.palWidgets;

public interface ISupportsMenus
{
	object MapMenuToVisual(ISupportsMenus menu);

	void AddMapMenuToVisual(ISupportsMenus menu, IMenuItem visual);

	bool RemoveMapMenuToVisual(ISupportsMenus menu);
}
