using System.Collections;
using System.Collections.Generic;

public class TheEventSystem
{
	public delegate void EventHandler(IEventable e);

	private static TheEventSystem eventSystem;
	private Dictionary<string, List<EventHandler>> eventHandlers = new Dictionary<string, List<EventHandler>> ();

	private TheEventSystem()
	{
	}

	public static TheEventSystem getInstance()
	{
		if (eventSystem == null)
		{
			eventSystem = new TheEventSystem();
		}

		return eventSystem;
	}

	public void addEventHandler(string eventName, EventHandler eventHandler)
	{
		if (eventHandlers.ContainsKey (eventName)) {
			eventHandlers [eventName].Add (eventHandler);
		}
		else
		{
			List<EventHandler> list = new List<EventHandler> ();
			list.Add (eventHandler);

			eventHandlers.Add (eventName, list);
		}
	}

	public void publishEvent(IEventable e)
	{
		if (!eventHandlers.ContainsKey(e.GetName()))
		{
			return;
		}
			
		foreach (EventHandler eventHandler in eventHandlers[e.GetName()])
		{
			eventHandler(e);
		}	
	}
}
