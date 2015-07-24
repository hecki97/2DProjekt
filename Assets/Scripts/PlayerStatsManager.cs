using UnityEngine;
using System.Collections.Generic;

public class PlayerStatsManager : MonoBehaviour {

	public static PlayerStatsManager instance = null;
	public TextAsset playerStatsXMLFile;

	public List<PlayerStatsData> list_playerStats;
    protected int foodPoints = 100;
    protected int maxFoodPoints = 100;
    protected int coinCount = 0;
    protected int healthPoints = 3;
    protected int maxHealthPoints = 3;
//  private float damageCount;

	public int FoodPoints {
		get { return foodPoints; }
		set { foodPoints = value; }
	}

	public int MaxFoodPoints {
		get { return maxFoodPoints; }
		set { maxFoodPoints = value; }
	}
	
	public int HealthPoints {
		get { return healthPoints; }
		set { healthPoints = value; }
	}
	
	public int MaxHealthPoints {
		get { return maxHealthPoints; }
		set { maxHealthPoints = value; }
	}

	public int CoinCount {
		get { return coinCount; }
		set { coinCount = value; }
	}

	// Use this for initialization
	void Awake () {
		if (instance == null)
        {
            instance = this;
            XMLFileHandler.DeserializeXMLFile<PlayerStatsData>(playerStatsXMLFile, out list_playerStats);
        }
        else if (instance != this)
            Destroy(gameObject);
		
		if (list_playerStats.Count > 0) return;
		
		LoadPlayerStatsFromXML();
	}
	
	public void LoadPlayerStatsFromXML()
    {
        for (int i = 0; i < list_playerStats.Count; i++)
        {
            if (list_playerStats[i].difficulty != GameManager.instance.difficulty) return;
			
            foodPoints = list_playerStats[i].FoodPoints;
            maxFoodPoints = list_playerStats[i].MaxFoodPoints;
            healthPoints = list_playerStats[i].HealthPoints;
            maxHealthPoints = list_playerStats[i].MaxHealthPoints;
            coinCount = list_playerStats[i].CoinCount;
//          damageCount = list_playerStats[i].GetDamageCount();
        }
    }
	
	public PlayerStatsData PlayerStats() {
		return new PlayerStatsData(CoinCount, FoodPoints, MaxFoodPoints, HealthPoints, MaxHealthPoints);
	}
}
