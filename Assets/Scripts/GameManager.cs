using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;

public enum MenuInstance { MainMenu, PauseMenu, OptionsMenu, AudioMenu, GraphicsMenu };
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private RandomGenerator generatorScript;
    private BoardManager boardScript;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup = true;

    //      Score
    public float time = 0f;
    public int stepCount = 0;

    //      Game Settings
    public Difficulty difficulty = Difficulty.Normal;
    public bool isPaused = false;
    public int level = 1;
    
    public float turnDelay = 0.1f;
    [HideInInspector] public bool playersTurn = true;

    //      UI
    public MenuInstance menu = MenuInstance.PauseMenu;
    public float levelStartDelay = 2f;

    private Text levelName;
    private Text levelText;
    private Image levelImage;

    //      Level Color
    public TextAsset levelColorXMLFile;

    private List<LevelColorData> colors;
    [HideInInspector] public Color32 levelColor;

	// Use this for initialization
	void Awake ()
	{
        if (instance == null)
        {
            instance = this;
            XMLFileHandler.DeserializeXMLFile<LevelColorData>(levelColorXMLFile, out colors);
//            PlayerStatsManager.instance.LoadPlayerStatsFromXML();
        }
        else if (instance != this)
            Destroy(gameObject);

		DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy>();
        if (Application.loadedLevel == 0) return;
		if (Application.loadedLevelName == "Classic3D")
        {
            boardScript = GetComponent<BoardManager>();
            InitGame();
            boardScript.SetupScene(level);
            Invoke("HideLevelImage", levelStartDelay);
        }
        else
        {
            generatorScript = GetComponent<RandomGenerator>();
            InitGame();
            StartCoroutine(generatorScript.BeginGame());
        }
	}
    
    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "Classic3D")
        {
            boardScript = GetComponent<BoardManager>();
            InitGame();
            boardScript.SetupScene(level);
            Invoke("HideLevelImage", levelStartDelay);
        }
        else
        {
            generatorScript = GetComponent<RandomGenerator>();
            InitGame();
            StartCoroutine(generatorScript.BeginGame());
        }
    }

	void InitGame ()
	{
        if (GameObject.Find("LevelImage") == null && GameObject.Find("LevelText") == null) return;

		doingSetup = true;
        levelName = GameObject.Find("LevelName").GetComponent<Text>();
		levelImage = GameObject.Find ("LevelImage").GetComponent<Image>();
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();

		//#if UNITY_IOS || UNITY_IPHONE
		Application.targetFrameRate = 60;
		//#endif
        
        levelColor = ColorUtil.ConvertHEXtoRGB(colors[Random.Range(0, colors.Count)].HexString);

		levelText.text = "Level " + level;
		levelImage.gameObject.SetActive (true);
		
		enemies.Clear();
	}

	public void HideLevelImage ()
	{
		levelImage.gameObject.SetActive (false);
		doingSetup = false;
	}

	// Update is called once per frame
	void Update ()
	{
        if (!isPaused && Application.loadedLevel != 0 && !doingSetup)
            time += Time.deltaTime;

		if (playersTurn || enemiesMoving || doingSetup || isPaused) return;

		StartCoroutine (MoveEnemies ());
	}

	public void AddEnemyToList(Enemy script)
	{
		enemies.Add (script);
	}

    public void RemoveEnemyFromList(Enemy script)
    {
        enemies.Remove(script);
    }

	public void GameOver ()
	{
        float score = Mathf.RoundToInt((PlayerStatsManager.instance.playerStats.FoodPoints * 50 + PlayerStatsManager.instance.playerStats.MaxFoodPoints * 100 + PlayerStatsManager.instance.playerStats.HealthPoints * 50 + PlayerStatsManager.instance.playerStats.MaxHealthPoints * 100 + PlayerStatsManager.instance.playerStats.CoinCount * 100) * level - Mathf.PI * (time / stepCount));
		levelText.text = "After " + level + " levels, you died. \n Score: " + score / 1000;
		levelImage.gameObject.SetActive (true);
		enabled = false;
	}

	IEnumerator MoveEnemies ()
	{
		enemiesMoving = true;
		yield return new WaitForSeconds (turnDelay);

		if (enemies.Count == 0)
			yield return new WaitForSeconds (turnDelay);

		for (int i = 0; i < enemies.Count; i++) {
			enemies [i].MoveEnemy ();
			yield return new WaitForSeconds (enemies [i].moveTime);
		}

		playersTurn = true;
		enemiesMoving = false;
	}
}
