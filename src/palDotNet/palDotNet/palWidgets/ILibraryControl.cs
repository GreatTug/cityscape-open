using System;

namespace palDotNet.palWidgets;

public interface ILibraryControl
{
	bool VisibleTreeView { set; }

	bool VisibleListView { set; }

	bool VisiblePropertyGridView { set; }

	bool VisibleRenderWindow { set; }

	void OnListViewAdded(object sender, EventArgs args);

	void OnRenderWindowAdded(object sender, EventArgs args);

	void OnTreeViewAdded(object sender, EventArgs args);

	void OnPropertyGridViewAdded(object sender, EventArgs args);
}
