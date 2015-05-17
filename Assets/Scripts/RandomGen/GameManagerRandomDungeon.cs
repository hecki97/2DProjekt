using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using UnityStandardAssets.ImageEffects;

public class GameManagerRandomDungeon : MonoBehaviour {

    public static GameManagerRandomDungeon instance = null;
    //private BoardManager boardScript;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup = true;

    //SecretMode!
    public float duration;
    public bool secretModeActive = false;

    private BloomOptimized bloom;
    private Color32 colorStart;
    private Color32 colorEnd;

    //      Score
    public float time = 0f;

    //      Game Settings
    //public GameMode gameMode;
    public Difficulty difficulty = Difficulty.Normal;
    public bool isPaused = false;
    public int level = 1;
    
    public float turnDelay = 0.1f;
    [HideInInspector] public bool playersTurn = true;

    //      UI
    public MenuInstance menu = MenuInstance.PauseMenu;
    public float levelStartDelay = 2f;

    private Text levelText;
    private Image levelImage;

    //      Level Color
    public TextAsset levelColorXMLFile;

    public Material mat;
    private List<LevelColorData> colors;
    private Color32 levelColor;

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
		//boardScript = GetComponent<BoardManager> ();
		InitGame ();
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
        {
            SoundManager.instance.musicSource.clip = SoundManager.instance.mainBGM;
            mat.color = levelColor;
        }
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
        InitGame();
    }

	void InitGame ()
	{
        if (GameObject.Find("LevelImage") == null && GameObject.Find("LevelText") == null) return;

		doingSetup = true;
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
        colorStart = ColorUtil.getRandomColor();
        colorEnd = ColorUtil.getRandomColor();
        levelColor = colors[Random.Range(0, colors.Count)].GetColor32();
        mat.color = levelColor;

		levelText.text = "Level " + level;
		levelImage.gameObject.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);

		enemies.Clear();
//		boardScript.SetupScene (level);
	}

	void HideLevelImage ()
	{
		levelImage.gameObject.SetActive (false);
		doingSetup = false;
	}

    public void GetRandomLvlColor()
    {
        mat.color = colors[Random.Range(0, colors.Count)].GetColor32();
    }

	// Update is called once per frame
	void Update ()
	{
        if (secretModeActive)
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            mat.color = Color.Lerp(colorStart, colorEnd, lerp);
        }

        if (!isPaused && Application.loadedLevel != 0 && !doingSetup)
            time += Time.deltaTime;

		if (playersTurn || enemiesMoving || doingSetup || isPaused)
			return;

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
		levelText.text = "After " + level + " levels, you died.";
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
