using System.Collections;
using System.Collections.Generic;

public class GenerateSectionEvent : IEventable
{
	public string GetName()
	{
		return GetType().Name;
	}
}
