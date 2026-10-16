using System;

namespace palDotNet.palWidgets;

[AttributeUsage(AttributeTargets.Class)]
internal class WidgetCreatorAttribute : Attribute
{
	private string[] TheAliases;

	public string[] Aliases
	{
		get
		{
			return TheAliases;
		}
		set
		{
			TheAliases = value;
		}
	}
}
