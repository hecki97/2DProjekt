using UnityEngine;
using System.Collections;

public class PlayerStatsData {

	//Player Stats
    public Difficulty difficulty = Difficulty.Normal;
    //public int unlocked = 0;
    protected int coinCount = 0;
	protected int foodPoints = 100;
    protected int maxFoodPoints = 100;
    protected int healthPoints = 3;
    protected int maxHealthPoints = 3;
	protected float damageCount = 1f;
    
    public PlayerStatsData(int coinCount, int foodPoints, int maxFoodPoints, int healthPoints, int maxHealthPoints) {
        this.coinCount = coinCount;
        this.foodPoints = foodPoints;
        this.maxFoodPoints = maxFoodPoints;
        this.healthPoints = healthPoints;
        this.maxHealthPoints = maxHealthPoints;
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
