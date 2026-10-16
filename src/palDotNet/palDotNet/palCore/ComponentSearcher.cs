using System;
using System.Collections.Generic;
using System.Reflection;

namespace palDotNet.palCore;

internal class ComponentSearcher
{
	private Type AttributeType;

	private Assembly[] Assemblies;

	public ComponentSearcher(Type type)
		: this(type, null)
	{
	}

	public ComponentSearcher(Type type, Assembly assem)
	{
		AttributeType = type;
		if (assem != null)
		{
			Assemblies = new Assembly[1] { assem };
		}
		else
		{
			Assemblies = AppDomain.CurrentDomain.GetAssemblies();
		}
	}

	public IEnumerator<Type> GetEnumerator()
	{
		try
		{
			Assembly[] assemblies = Assemblies;
			foreach (Assembly assem in assemblies)
			{
				try
				{
					Type[] types = assem.GetTypes();
					foreach (Type type in types)
					{
						object[] attributes = type.GetCustomAttributes(AttributeType, inherit: false);
						if (attributes.Length == 1)
						{
							yield return type;
						}
					}
				}
				finally
				{
				}
			}
		}
		finally
		{
		}
	}

	private static bool FilterMembersByAttributeType(MemberInfo info, object state)
	{
		Type attributeType = (Type)state;
		object[] customAttributes = info.GetCustomAttributes(attributeType, inherit: false);
		return 0 < customAttributes.Length;
	}
}
