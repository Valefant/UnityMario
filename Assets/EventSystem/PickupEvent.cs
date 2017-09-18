using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupEvent : IEventable
{
	private int points;

	public PickupEvent(int points)
	{
		this.points = points;
	}

	public string GetName()
	{
		return "PickupEvent";
	}

	public int getPoints()
	{
		return points;
	}
}