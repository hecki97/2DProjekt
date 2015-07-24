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

    public void SetPlayerStats()
    {
        playerStats = PlayerStatsManager.instance.playerStats;
    }

    void UpdateButtons()
    {
        b_refillFoodPoints.interactable = (PlayerStatsManager.instance.playerStats.CoinCount <= 0 || PlayerStatsManager.instance.playerStats.FoodPoints >= PlayerStatsManager.instance.playerStats.MaxFoodPoints) ? false : true;
        b_refillHealthPoints.interactable = (PlayerStatsManager.instance.playerStats.CoinCount <= 0 || PlayerStatsManager.instance.playerStats.HealthPoints >= PlayerStatsManager.instance.playerStats.MaxHealthPoints) ? false : true;
        b_buy.interactable = (PlayerStatsManager.instance.playerStats.CoinCount == playerStats.CoinCount) ? false : true;
        b_decreaseMaxHealth.interactable = (PlayerStatsManager.instance.playerStats.MaxHealthPoints <= playerStats.MaxHealthPoints || PlayerStatsManager.instance.playerStats.HealthPoints >= PlayerStatsManager.instance.playerStats.MaxHealthPoints) ? false : true;
        b_increaseMaxHealth.interactable = (PlayerStatsManager.instance.playerStats.MaxHealthPoints >= maxMaxHealthPoints || PlayerStatsManager.instance.playerStats.CoinCount < 25) ? false : true;
        b_decreaseMaxFood.interactable = (PlayerStatsManager.instance.playerStats.MaxFoodPoints <= playerStats.MaxFoodPoints || PlayerStatsManager.instance.playerStats.FoodPoints >= PlayerStatsManager.instance.playerStats.MaxFoodPoints) ? false : true;
        b_increaseMaxFood.interactable = (PlayerStatsManager.instance.playerStats.MaxFoodPoints >= maxMaxFoodPoints || PlayerStatsManager.instance.playerStats.CoinCount < 25) ? false : true;
        b_returnToLevel.interactable = (PlayerStatsManager.instance.playerStats.CoinCount == playerStats.CoinCount) ? true : false;
    }

    public void OnClick(string function)
    {
        Invoke(function, 0f);
    }

    void UpdatePlayerStats()
    {
        t_coin.text = string.Format("x {0}", PlayerStatsManager.instance.playerStats.CoinCount.ToString("000"));
        t_refillFood.text = string.Format("{0}/{1}", PlayerStatsManager.instance.playerStats.FoodPoints.ToString("000"), PlayerStatsManager.instance.playerStats.MaxFoodPoints.ToString("000"));
        t_refillHealth.text = string.Format("{0}/{1}", PlayerStatsManager.instance.playerStats.HealthPoints.ToString("000"), PlayerStatsManager.instance.playerStats.MaxHealthPoints.ToString("000"));
        t_upgrMaxFood.text = string.Format("Max. {0}", PlayerStatsManager.instance.playerStats.MaxFoodPoints);
        t_upgrMaxHealth.text = string.Format("Max. {0}", PlayerStatsManager.instance.playerStats.MaxHealthPoints);

        refillCostFoodPoints = Mathf.RoundToInt((PlayerStatsManager.instance.playerStats.MaxFoodPoints - PlayerStatsManager.instance.playerStats.FoodPoints) / Mathf.PI);
        foodPointsByCoins = Mathf.RoundToInt(PlayerStatsManager.instance.playerStats.CoinCount * Mathf.PI);
        refillCostHealthPoints = (PlayerStatsManager.instance.playerStats.MaxHealthPoints - PlayerStatsManager.instance.playerStats.HealthPoints) * 5;
        healthPointsByCoins = Mathf.RoundToInt(PlayerStatsManager.instance.playerStats.CoinCount / 5);

        t_refillFoodPoints.text = (PlayerStatsManager.instance.playerStats.CoinCount >= refillCostFoodPoints) ? ("ALL (" + refillCostFoodPoints.ToString("000") + "G)") : ("RFL (" + foodPointsByCoins.ToString("000") + "P)");
        t_refillFoodPoints.text = (PlayerStatsManager.instance.playerStats.FoodPoints >= PlayerStatsManager.instance.playerStats.MaxFoodPoints) ? "FULL" : t_refillFoodPoints.text;
        t_refillHealthPoints.text = (PlayerStatsManager.instance.playerStats.CoinCount >= refillCostHealthPoints) ? ("ALL (" + refillCostHealthPoints.ToString("000") + "G)") : ("RFL (" + healthPointsByCoins.ToString("000") + "P)");
        t_refillHealthPoints.text = (PlayerStatsManager.instance.playerStats.HealthPoints >= PlayerStatsManager.instance.playerStats.MaxHealthPoints) ? "FULL" : t_refillHealthPoints.text;
        t_cancel.text = (PlayerStatsManager.instance.playerStats.CoinCount == playerStats.CoinCount) ? "Switch Level" : "Revert Purchase";
    }

    void IncreaseMaxFood()
    {
        if (PlayerStatsManager.instance.playerStats.CoinCount >= 25 && PlayerStatsManager.instance.playerStats.MaxFoodPoints < maxMaxFoodPoints)
        {
            PlayerStatsManager.instance.playerStats.CoinCount -= 25;
            PlayerStatsManager.instance.playerStats.MaxFoodPoints = (PlayerStatsManager.instance.playerStats.MaxFoodPoints >= maxMaxFoodPoints) ? maxMaxFoodPoints : (PlayerStatsManager.instance.playerStats.MaxFoodPoints += 50);
        }
    }

    void DecreaseMaxFood()
    {
        if (PlayerStatsManager.instance.playerStats.MaxFoodPoints > playerStats.FoodPoints)
        {
            PlayerStatsManager.instance.playerStats.CoinCount += 25;
            PlayerStatsManager.instance.playerStats.MaxFoodPoints = (PlayerStatsManager.instance.playerStats.MaxFoodPoints > playerStats.MaxFoodPoints) ? (PlayerStatsManager.instance.playerStats.MaxFoodPoints -= 50) : playerStats.MaxFoodPoints;
        }
    }

    void IncreaseMaxHealth()
    {
        if (PlayerStatsManager.instance.playerStats.CoinCount >= 25 && PlayerStatsManager.instance.playerStats.MaxHealthPoints < maxMaxHealthPoints)
        {
            PlayerStatsManager.instance.playerStats.CoinCount -= 25;
            PlayerStatsManager.instance.playerStats.MaxHealthPoints = (PlayerStatsManager.instance.playerStats.MaxHealthPoints >= maxMaxHealthPoints) ? maxMaxHealthPoints : (PlayerStatsManager.instance.playerStats.MaxHealthPoints += 1);
        }
    }

    void DecreaseMaxHealth()
    {
        if (PlayerStatsManager.instance.playerStats.MaxHealthPoints > playerStats.MaxHealthPoints)
        {
            PlayerStatsManager.instance.playerStats.CoinCount += 25;
            PlayerStatsManager.instance.playerStats.MaxHealthPoints = (PlayerStatsManager.instance.playerStats.MaxHealthPoints > playerStats.MaxHealthPoints) ? (PlayerStatsManager.instance.playerStats.MaxHealthPoints -= 1) : playerStats.MaxHealthPoints;
        }
    }

    void RefillHealthPoints()
    {
        if (PlayerStatsManager.instance.playerStats.CoinCount >= refillCostHealthPoints && PlayerStatsManager.instance.playerStats.HealthPoints < PlayerStatsManager.instance.playerStats.MaxHealthPoints)
        {
            PlayerStatsManager.instance.playerStats.CoinCount -= refillCostHealthPoints;
            PlayerStatsManager.instance.playerStats.HealthPoints = PlayerStatsManager.instance.playerStats.MaxHealthPoints;
        }
        else if (PlayerStatsManager.instance.playerStats.CoinCount >= 5 && PlayerStatsManager.instance.playerStats.HealthPoints < PlayerStatsManager.instance.playerStats.MaxHealthPoints)
        {
            PlayerStatsManager.instance.playerStats.HealthPoints += healthPointsByCoins;
            PlayerStatsManager.instance.playerStats.CoinCount = 0;
        }
    }

    void RefillFoodPoints()
    {
        if (PlayerStatsManager.instance.playerStats.CoinCount >= refillCostFoodPoints && PlayerStatsManager.instance.playerStats.FoodPoints < PlayerStatsManager.instance.playerStats.MaxFoodPoints)
        {
            PlayerStatsManager.instance.playerStats.CoinCount -= refillCostFoodPoints;
            PlayerStatsManager.instance.playerStats.FoodPoints = PlayerStatsManager.instance.playerStats.MaxFoodPoints;
        }
        else if (PlayerStatsManager.instance.playerStats.FoodPoints < PlayerStatsManager.instance.playerStats.MaxFoodPoints)
        {
            PlayerStatsManager.instance.playerStats.FoodPoints += foodPointsByCoins;
            PlayerStatsManager.instance.playerStats.CoinCount = 0;
        }
    }

    void Restart()
    {
        GameManager.instance.level++;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ButtonCancel()
    {
        if (playerStats.CoinCount == PlayerStatsManager.instance.playerStats.CoinCount)
            ButtonBuy();
        else
        {
            PlayerStatsManager.instance.playerStats = playerStats;
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