using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopGUIController : MonoBehaviour {

    public float restartLevelDelay = 1f;
    public int maxMaxFoodPoints;
    public int maxMaxHealthPoints;

    private Button b_refillFoodPoints;
    private Button b_refillHealthPoints;
    private Button b_increaseMaxFood;
    private Button b_decreaseMaxFood;
    private Button b_increaseMaxHealth;
    private Button b_decreaseMaxHealth;
    private Button b_buy;
    private Button b_returnToLevel;

    private Text t_cancel;
    private Text t_coin;
    private Text t_refillFood;
    private Text t_refillHealth;
    private Text t_upgrMaxFood;
    private Text t_upgrMaxHealth;
    private Text t_refillHealthPoints;
    private Text t_refillFoodPoints;

    private Animator anim;
    private PlayerStatsData playerStats = new PlayerStatsData();

    private int refillCostFoodPoints;
    private int foodPointsByCoins;
    private int refillCostHealthPoints;
    private int healthPointsByCoins;

	// Use this for initialization
    void Start()
    {
        b_refillFoodPoints = GameObject.Find("RefillFoodPoints").GetComponent<Button>();
        b_refillHealthPoints = GameObject.Find("RefillHealthPoints").GetComponent<Button>();
        b_increaseMaxFood = GameObject.Find("ButtonIncreaseBy50").GetComponent<Button>();
        b_decreaseMaxFood = GameObject.Find("ButtonDecreaseBy50").GetComponent<Button>();
        b_increaseMaxHealth = GameObject.Find("ButtonPlus").GetComponent<Button>();
        b_decreaseMaxHealth = GameObject.Find("ButtonMinus").GetComponent<Button>();
        b_buy = GameObject.Find("ButtonBuy").GetComponent<Button>();
        b_returnToLevel = GameObject.Find("ButtonReturnToLvl").GetComponent<Button>();

        t_cancel = GameObject.Find("ButtonCancel/Text").GetComponent<Text>();
        t_coin = GameObject.Find("CoinText").GetComponent<Text>();
        t_refillFood = GameObject.Find("RefillFoodPanel/Text").GetComponent<Text>();
        t_refillHealth = GameObject.Find("RefillHealthPanel/Text").GetComponent<Text>();
        t_upgrMaxFood = GameObject.Find("UpgrMaxFoodPanel/Text").GetComponent<Text>();
        t_upgrMaxHealth = GameObject.Find("UpgrMaxHealthPanel/Text").GetComponent<Text>();
        t_refillHealthPoints = GameObject.Find("RefillHealthPoints/Text").GetComponent<Text>();
        t_refillFoodPoints = GameObject.Find("RefillFoodPoints/Text").GetComponent<Text>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateButtons();
        UpdatePlayerStats();
    }

    /*
    public void SetPlayerStats()
    {
        playerStats = PlayerStatsManager.instance.PlayerStats();
        playerStats.CoinCount = GameManager.instance.coinsCount;
        playerStats.SetFoodPoints(GameManager.instance.foodPoints);
        playerStats.SetMaxFoodPoints(GameManager.instance.maxFoodPoints);
        playerStats.SetHealthPoints(GameManager.instance.healthPoints);
        playerStats.SetMaxHealthPoints(GameManager.instance.maxHealthPoints);
    }
    */

    void UpdateButtons()
    {
        b_refillFoodPoints.interactable = (PlayerStatsManager.instance.CoinCount <= 0 || PlayerStatsManager.instance.FoodPoints >= PlayerStatsManager.instance.MaxFoodPoints) ? false : true;
        b_refillHealthPoints.interactable = (PlayerStatsManager.instance.CoinCount <= 0 || PlayerStatsManager.instance.HealthPoints >= PlayerStatsManager.instance.MaxHealthPoints) ? false : true;
        b_buy.interactable = (PlayerStatsManager.instance.CoinCount == playerStats.CoinCount) ? false : true;
        b_decreaseMaxHealth.interactable = (PlayerStatsManager.instance.MaxHealthPoints <= playerStats.MaxHealthPoints || PlayerStatsManager.instance.HealthPoints >= PlayerStatsManager.instance.MaxHealthPoints) ? false : true;
        b_increaseMaxHealth.interactable = (PlayerStatsManager.instance.MaxHealthPoints >= maxMaxHealthPoints || PlayerStatsManager.instance.CoinCount < 25) ? false : true;
        b_decreaseMaxFood.interactable = (PlayerStatsManager.instance.MaxFoodPoints <= playerStats.MaxFoodPoints || PlayerStatsManager.instance.FoodPoints >= PlayerStatsManager.instance.MaxFoodPoints) ? false : true;
        b_increaseMaxFood.interactable = (PlayerStatsManager.instance.MaxFoodPoints >= maxMaxFoodPoints || PlayerStatsManager.instance.CoinCount < 25) ? false : true;
        b_returnToLevel.interactable = (PlayerStatsManager.instance.CoinCount == playerStats.CoinCount) ? true : false;
    }

    public void OnClick(string function)
    {
        Invoke(function, 0f);
    }

    void UpdatePlayerStats()
    {
        t_coin.text = string.Format("x {0}", GameManager.instance.coinsCount.ToString("000"));
        t_refillFood.text = string.Format("{0}/{1}", GameManager.instance.foodPoints.ToString("000"), GameManager.instance.maxFoodPoints.ToString("000"));
        t_refillHealth.text = string.Format("{0}/{1}", GameManager.instance.healthPoints.ToString("000"), GameManager.instance.maxHealthPoints.ToString("000"));
        t_upgrMaxFood.text = string.Format("Max. {0}", GameManager.instance.maxFoodPoints);
        t_upgrMaxHealth.text = string.Format("Max. {0}", GameManager.instance.maxHealthPoints);

        refillCostFoodPoints = Mathf.RoundToInt((GameManager.instance.maxFoodPoints - GameManager.instance.foodPoints) / Mathf.PI);
        foodPointsByCoins = Mathf.RoundToInt(GameManager.instance.coinsCount * Mathf.PI);
        refillCostHealthPoints = (GameManager.instance.maxHealthPoints - GameManager.instance.healthPoints) * 5;
        healthPointsByCoins = Mathf.RoundToInt(GameManager.instance.coinsCount / 5);

        t_refillFoodPoints.text = (GameManager.instance.coinsCount >= refillCostFoodPoints) ? ("ALL (" + refillCostFoodPoints.ToString("000") + "G)") : ("RFL (" + foodPointsByCoins.ToString("000") + "P)");
        t_refillFoodPoints.text = (GameManager.instance.foodPoints >= GameManager.instance.maxFoodPoints) ? "FULL" : t_refillFoodPoints.text;
        t_refillHealthPoints.text = (GameManager.instance.coinsCount >= refillCostHealthPoints) ? ("ALL (" + refillCostHealthPoints.ToString("000") + "G)") : ("RFL (" + healthPointsByCoins.ToString("000") + "P)");
        t_refillHealthPoints.text = (GameManager.instance.healthPoints >= GameManager.instance.maxHealthPoints) ? "FULL" : t_refillHealthPoints.text;
        t_cancel.text = (GameManager.instance.coinsCount == playerStats.playerCoinsCount) ? "Switch Level" : "Revert Purchase";
    }

    void IncreaseMaxFood()
    {
        if (GameManager.instance.coinsCount >= 25 && GameManager.instance.maxFoodPoints < maxMaxFoodPoints)
        {
            GameManager.instance.coinsCount -= 25;
            GameManager.instance.maxFoodPoints = (GameManager.instance.maxFoodPoints >= maxMaxFoodPoints) ? maxMaxFoodPoints : (GameManager.instance.maxFoodPoints += 50);
        }
    }

    void DecreaseMaxFood()
    {
        if (GameManager.instance.maxFoodPoints > playerStats.GetMaxFoodPoints())
        {
            GameManager.instance.coinsCount += 25;
            GameManager.instance.maxFoodPoints = (GameManager.instance.maxFoodPoints > playerStats.GetMaxFoodPoints()) ? (GameManager.instance.maxFoodPoints -= 50) : playerStats.GetMaxFoodPoints();
        }
    }

    void IncreaseMaxHealth()
    {
        if (GameManager.instance.coinsCount >= 25 && GameManager.instance.maxHealthPoints < maxMaxHealthPoints)
        {
            GameManager.instance.coinsCount -= 25;
            GameManager.instance.maxHealthPoints = (GameManager.instance.maxHealthPoints >= maxMaxHealthPoints) ? maxMaxHealthPoints : (GameManager.instance.maxHealthPoints += 1);
        }
    }

    void DecreaseMaxHealth()
    {
        if (GameManager.instance.maxHealthPoints > playerStats.GetMaxHealthPoints())
        {
            GameManager.instance.coinsCount += 25;
            GameManager.instance.maxHealthPoints = (GameManager.instance.maxHealthPoints > playerStats.GetMaxHealthPoints()) ? (GameManager.instance.maxHealthPoints -= 1) : playerStats.GetMaxHealthPoints();
        }
    }

    void RefillHealthPoints()
    {
        if (GameManager.instance.coinsCount >= refillCostHealthPoints && GameManager.instance.healthPoints < GameManager.instance.maxHealthPoints)
        {
            GameManager.instance.coinsCount -= refillCostHealthPoints;
            GameManager.instance.healthPoints = GameManager.instance.maxHealthPoints;
        }
        else if (GameManager.instance.coinsCount >= 5 && GameManager.instance.healthPoints < GameManager.instance.maxHealthPoints)
        {
            GameManager.instance.healthPoints += healthPointsByCoins;
            GameManager.instance.coinsCount = 0;
        }
    }

    void RefillFoodPoints()
    {
        if (GameManager.instance.coinsCount >= refillCostFoodPoints && GameManager.instance.foodPoints < GameManager.instance.maxFoodPoints)
        {
            GameManager.instance.coinsCount -= refillCostFoodPoints;
            GameManager.instance.foodPoints = GameManager.instance.maxFoodPoints;
        }
        else if (GameManager.instance.foodPoints < GameManager.instance.maxFoodPoints)
        {
            GameManager.instance.foodPoints += foodPointsByCoins;
            GameManager.instance.coinsCount = 0;
        }
    }

    void Restart()
    {
        GameManager.instance.level++;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ButtonCancel()
    {
        if (playerStats.playerCoinsCount == PlayerStatsManager.instance.CoinCount)
            ButtonBuy();
        else
        {
            PlayerStatsManager.instance.list_playerStats = playerStats;
            PlayerStatsManager.instance.FoodPoints = playerStats.playerFoodPoints;
            PlayerStatsManager.instance.MaxFoodPoints = playerStats.playerMaxFoodPoints;
            PlayerStatsManager.instance.HealthPoints = playerStats.playerHealthPoints;
            PlayerStatsManager.instance.MaxHealthPoints = playerStats.playerMaxHealthPoints;
        }
    }

    public void ButtonBuy()
    {
        anim.SetTrigger("triggerMenu");
        GameManager.instance.isPaused = false;
        Invoke("Restart", restartLevelDelay);
    }
    
    public void ButtonReturnToLvl()
    {
        anim.SetTrigger("triggerMenu");
        GameManager.instance.isPaused = false;
    }
}