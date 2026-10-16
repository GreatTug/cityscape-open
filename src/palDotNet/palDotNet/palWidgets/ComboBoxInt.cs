namespace palDotNet.palWidgets;

[WidgetCreator(Aliases = new string[] { "ComboBoxInt", "ComboBoxStr" })]
internal class ComboBoxInt : ComboBox
{
	public ComboBoxInt(uint newId)
		: base(newId)
	{
	}
}
