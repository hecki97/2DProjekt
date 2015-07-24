using UnityEngine;
using System.Collections;

public class PlayerStatsData {

	//Player Stats
    public Difficulty difficulty = Difficulty.Normal;
    public int unlocked = 0;
    protected int coinCount;
    protected int foodPoints;
    protected int maxFoodPoints;
    protected int healthPoints;
    protected int maxHealthPoints;
	protected float damageCount = 1f;
    
    public PlayerStatsData()
    {
        coinCount = 0;
        foodPoints = 100;
        maxFoodPoints = 100;
        healthPoints = 3;
        maxHealthPoints = 3;
    }

    public PlayerStatsData(int coinCount, int foodPoints, int maxFoodPoints, int healthPoints, int maxHealthPoints) {
        this.coinCount = coinCount;
        this.foodPoints = foodPoints;
        this.maxFoodPoints = maxFoodPoints;
        this.healthPoints = healthPoints;
        this.maxHealthPoints = maxHealthPoints;
    }
    
    public void SetPlayerStats(int coinCount, int foodPoints, int maxFoodPoints, int healthPoints, int maxHealthPoints)
    {
        this.coinCount = coinCount;
        this.foodPoints = foodPoints;
        this.maxFoodPoints = maxFoodPoints;
        this.healthPoints = healthPoints;
        this.maxHealthPoints = maxHealthPoints;
    }

    public PlayerStatsData GetPlayerStats()
    {
        return new PlayerStatsData(CoinCount, FoodPoints, MaxFoodPoints, HealthPoints, MaxHealthPoints);
    }

    public int CoinCount {
        get { return coinCount; }
        set { coinCount = value; }
    }
    
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
        get { return maxHealthPoints; }
        set { maxHealthPoints = value; }
    }
}
