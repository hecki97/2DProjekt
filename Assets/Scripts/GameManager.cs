using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameMode { TwoD, ThreeD };
public class GameManager : MonoBehaviour
{
	public float levelStartDelay = 2f;
	public float turnDelay = 0.1f;

	//Player Stats
	public int playerMaxFoodPoints = 100;
	public int playerFoodPoints = 100;
    public int playerCoinsCount = 0;
	public int playerHealthCount = 3;
	public int playerDamageCount = 1;

	public GameMode gameMode;
	public static GameManager instance = null;
	[HideInInspector]
	public bool
		playersTurn = true;

	private Text levelText;
	private Image levelImage;
	private BoardManager boardScript;
	private int level = 1;
	private List<Enemy> enemies;
	private bool enemiesMoving;
	private bool doingSetup = true;
	private bool sceneStarting = true;
	public float fadeSpeed = 1.5f;   

	public float delay = .25f;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy>();
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void OnLevelWasLoaded (int index)
	{
        level++;
		InitGame ();
	}

	void InitGame ()
	{
		doingSetup = true;
		sceneStarting = true;
		levelImage = GameObject.Find ("LevelImage").GetComponent<Image>();
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();

		//#if UNITY_IOS || UNITY_IPHONE
		Application.targetFrameRate = 60;
		//#endif

        //Test
        if (Application.loadedLevel == 1)
            gameMode = GameMode.TwoD;
        else if (Application.loadedLevel == 2)
            gameMode = GameMode.ThreeD;

		if (sceneStarting)
			StartScene ();

		levelText.text = "Level" + level;
		levelImage.gameObject.SetActive (true);
		//Invoke ("HideLevelImage", levelStartDelay);

		enemies.Clear();
		boardScript.SetupScene (level);
	}

	void HideLevelImage ()
	{
		levelImage.gameObject.SetActive (false);
		doingSetup = false;
	}

	void StartScene ()
	{
		FadeToClear();
		if(levelImage.color.a <= 0.05f)
		{
			levelImage.color = Color.clear;
			levelImage.enabled = false;

			sceneStarting = false;
		}
	}

	void FadeToClear() {
		levelImage.color = Color.Lerp(levelImage.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	// Update is called once per frame
	void Update ()
	{
		if (playersTurn || enemiesMoving || doingSetup)
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
