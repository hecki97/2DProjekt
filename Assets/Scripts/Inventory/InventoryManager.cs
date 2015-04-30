using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	static public Inventory inventory;
	
	//public bool create = false;
	public float coin = 0;
	public float initHealth = 3;
	public float initFood = 100;

    public float level = 0;
	public bool initValues = false;
		
	void Awake()
	{
		//if (create)
		if (inventory == null)
			inventory = (Inventory)	ScriptableObject.CreateInstance(typeof(Inventory));
	
		if (initValues)
		{	
			//Werte initialisieren
			inventory.Clear();
            inventory.SetItems("Level", level);

			inventory.SetItems("Money", coin);
			inventory.SetItems("HealthPoints", initHealth);
			inventory.SetItems("FoodPoints", initFood);
		}
	}

	protected virtual void Update()
	{
		//coin = inventory.GetItems("coin");	
	}

}
