using System.Collections.Generic;

public class EventManager
{
	public delegate void EventHandler(IEventable e);

	private static EventManager _eventManager;
	private readonly Dictionary<string, List<EventHandler>> _eventHandlers = new Dictionary<string, List<EventHandler>> ();

	private EventManager()
	{
	}

	public static EventManager GetInstance()
	{
		return _eventManager ?? (_eventManager = new EventManager());
	}

	public void AddEventHandler(string eventName, EventHandler eventHandler)
	{
		if (_eventHandlers.ContainsKey (eventName)) {
			_eventHandlers [eventName].Add (eventHandler);
		}
		else
		{
			var list = new List<EventHandler> {eventHandler};

			_eventHandlers.Add (eventName, list);
		}
	}

	public void PublishEvent(IEventable e)
	{
		if (!_eventHandlers.ContainsKey(e.GetName()))
		{
			return;
		}
		
		foreach (var eventHandler in _eventHandlers[e.GetName()])
		{
			eventHandler(e);
		}	
	}
}
