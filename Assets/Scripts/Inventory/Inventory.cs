using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : ScriptableObject {

	//public float money = 0;
	Dictionary<string, float> items = new Dictionary<string, float>();
	
	public void AddItems(string itemName, float quantity)
	{
		float oldValue = 0;
		
	 	itemName = itemName.ToLower();
		
		if (items.TryGetValue(itemName, out oldValue))
		{
			quantity += oldValue;
			items[itemName] = quantity;
		}
		else
		{
			items.Add(itemName,quantity);	
		}
	}

	public void SetItems(string itemName, float quantity)
	{
		float oldValue = 0;
		
	 	itemName = itemName.ToLower();
		
		if (items.TryGetValue(itemName, out oldValue))
		{
			items[itemName] = quantity;
		}
		else
		{
			items.Add(itemName,quantity);	
		}
	}

	public float GetItems(string itemName)
	{
		float currentValue = 0;
		float result = 0;
		
	 	itemName = itemName.ToLower();
		
		if (items.TryGetValue(itemName, out currentValue))
		{
			result = currentValue;
		}
		
		return result;
	}

	public void Clear()
	{
		items.Clear();	
	}
}
