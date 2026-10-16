using System;

namespace palDotNet.palDialog;

public class DialogValueType : Attribute
{
	public EnumDialogValueTypes ValueType;

	public DialogValueType(EnumDialogValueTypes type)
	{
		ValueType = type;
	}
}
