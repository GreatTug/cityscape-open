using System;

namespace palDotNet.palVisualPropertyGrid;

[AttributeUsage(AttributeTargets.Class)]
internal class PropertyCreatorAttribute : Attribute
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
