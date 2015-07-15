using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using UnityStandardAssets.ImageEffects;

public enum MenuInstance { MainMenu, PauseMenu, OptionsMenu, AudioMenu, GraphicsMenu };
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private RandomGenerator generatorScript;
    private BoardManager boardScript;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup = true;

    //SecretMode!
    public bool secretModeActive = false;
    private BloomOptimized bloom;

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

    //       PlayerStats
    public TextAsset playerStatsXMLFile;

    private List<PlayerStatsData> playerStats;
    [HideInInspector] public int foodPoints;
    [HideInInspector] public int maxFoodPoints;
     public int coinsCount;
    [HideInInspector] public int healthPoints;
    [HideInInspector] public int maxHealthPoints;
//    private float damageCount;

	// Use this for initialization
	void Awake ()
	{
        if (instance == null)
        {
            instance = this;
            XMLFileHandler.DeserializeXMLFile<LevelColorData>(levelColorXMLFile, out colors);
            XMLFileHandler.DeserializeXMLFile<PlayerStatsData>(playerStatsXMLFile, out playerStats);
            LoadDefaultPlayerStats();
        }
        else if (instance != this)
            Destroy(gameObject);

        SecretEventHandler.OnTrigger += this.SecretEventHandler_OnTrigger;

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

    void OnDisable()
    {
        SecretEventHandler.OnTrigger -= this.SecretEventHandler_OnTrigger;
    }

    private void SecretEventHandler_OnTrigger()
    {
        secretModeActive = !secretModeActive;

        if (secretModeActive)
            SoundManager.instance.musicSource.clip = SoundManager.instance.secretBGM;
        else
            SoundManager.instance.musicSource.clip = SoundManager.instance.mainBGM;

        bloom.enabled = secretModeActive;
        SoundManager.instance.musicSource.Play();
    }

    public void LoadDefaultPlayerStats()
    {
        for (int i = 0; i < playerStats.Count; i++)
        {
            if (playerStats[i].difficulty == difficulty)
            {
                foodPoints = playerStats[i].GetFoodPoints();
                maxFoodPoints = playerStats[i].GetMaxFoodPoints();
                healthPoints = playerStats[i].GetHealthPoints();
                maxHealthPoints = playerStats[i].GetMaxHealthPoints();
                coinsCount = playerStats[i].playerCoinsCount;
//                damageCount = playerStats[i].GetDamageCount();
            }
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

        if (Application.loadedLevel != 0)
        {
            bloom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BloomOptimized>();
            bloom.enabled = secretModeActive;
        }
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

	public void AddEnemyToList (Enemy script)
	{
		enemies.Add (script);
	}

    public void RemoveEnemyFromList(Enemy script)
    {
        enemies.Remove(script);
    }

	public void GameOver ()
	{
        float score = Mathf.RoundToInt((foodPoints * 50 + maxFoodPoints * 100 + healthPoints * 50 + maxHealthPoints * 100 + coinsCount * 100) * level - (Mathf.PI * (time / stepCount)));
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
