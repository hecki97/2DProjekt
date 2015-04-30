using UnityEngine;
using System.Collections;

public class PlayerStatsData {

	//Player Stats
    public Difficulty difficulty = Difficulty.Normal;
    public int playerCoinsCount = 0;
	public int playerFoodPoints = 100;
    public int playerMaxFoodPoints = 100;
    public int playerHealthPoints = 3;
    public int playerMaxHealthPoints = 3;
	public float playerDamageCount = 1f;

    public float GetDamageCount()
    {
        return playerDamageCount;
    }

    public int GetFoodPoints()
    {
        return playerFoodPoints;
    }

    public int GetMaxFoodPoints()
    {
        return playerMaxFoodPoints;
    }

    public int GetHealthPoints()
    {
        return playerHealthPoints;
    }

    public int GetMaxHealthPoints()
    {
        return playerMaxHealthPoints;
    }


    public void SetCoinsCount(int _int)
    {
        playerCoinsCount = _int;
    }

    public void SetFoodPoints(int _int)
    {
        playerFoodPoints = _int;
    }

    public void SetMaxFoodPoints(int _int)
    {
        playerMaxFoodPoints = _int;
    }

    public void SetHealthPoints(int _int)
    {
        playerHealthPoints = _int;
    }

    public void SetMaxHealthPoints(int _int)
    {
        playerMaxHealthPoints = _int;
    }
}
