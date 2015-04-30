using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour {

    private Text coinText;
    private Text healthText;
    private Text foodText;
    private Text fpsText;

    private Button buttonPauseMenu;

    private float count;

    // Use this for initialization
    void Awake()
    {
        coinText = transform.FindChild("CoinImage/Text").GetComponent<Text>();
        fpsText = transform.FindChild("FPSText").GetComponent<Text>();
        healthText = transform.FindChild("HealthPanel/Text").GetComponent<Text>();
        foodText = transform.FindChild("FoodPanel/Text").GetComponent<Text>();

        buttonPauseMenu = GameObject.Find("ButtonPauseMenu").GetComponent<Button>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            if (!GameManager.instance.isPaused)
            {
                yield return new WaitForSeconds(0.1f);
                count = (1 / Time.deltaTime);
                fpsText.text = "FPS: " + (Mathf.Round(count).ToString("00")) + " (" + GameManager.instance.difficulty + ")";
            }
            else
                fpsText.text = "Pause";

            yield return new WaitForSeconds(0.5f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        coinText.text = "x " + GameManager.instance.coinsCount;
        healthText.text = GameManager.instance.healthPoints + "/" + GameManager.instance.maxHealthPoints;
        foodText.text = GameManager.instance.foodPoints + "/" + GameManager.instance.maxFoodPoints;

        buttonPauseMenu.interactable = !GameManager.instance.isPaused;
	}
}
