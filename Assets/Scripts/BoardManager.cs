using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 3;
	public int rows = 3;
	public Count wallCount = new Count (18, 27);
    public Count coinCount = new Count(9, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] coinTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;
    public GameObject[] foodTiles;

	private Transform boardHolder;
    private Transform dungeonBoard;

	private List<Vector3> gridPositions = new List<Vector3> ();
    private List<Vector3> cornerPositions = new List<Vector3>();

	void InitialiseList ()
	{
		gridPositions.Clear ();
        cornerPositions.Clear();
		for (int x = 1; x < columns - 1; x++) {
			for (int y = 1; y < rows - 1; y++) {
                gridPositions.Add(new Vector3(x, y, 0f));
			}
		}
        cornerPositions.Add(new Vector3(0f, columns - 1, 0f));
        cornerPositions.Add(new Vector3(rows - 1, columns - 1, 0f));
        cornerPositions.Add(new Vector3(rows - 1, 0f, 0f));
	}

	void BoardSetup ()
	{
		boardHolder = new GameObject ("Board").transform;
        dungeonBoard = new GameObject("DungeonBoard").transform;
        dungeonBoard.SetParent(boardHolder);

        //Test!!
        /*if (GameManager.instance.gameMode == GameMode.TwoD) {
			for (int x = -1; x < columns + 1; x++) {
				for (int y = -1; y < rows + 1; y++) {
					GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];

					if (x == -1 || x == columns || y == -1 || y == rows)
						toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];

					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(dungeonBoard);
					//instance.transform.SetParent (boardHolder);
				}
			}
		} else {*/
			for (int x = -1; x < columns + 1; x++) {
				for (int y = -1; y < rows + 1; y++) {
					if (x == -1 || x == columns || y == -1 || y == rows)
						InstantiateGameObject (outerWallTiles, "OuterWall", new Vector3 (x, y, 0f));
					else if (x == 0 || x == columns - 1f || y == 0 || y == rows - 1f) {
						InstantiateGameObject (floorTiles, "FloorSprite", new Vector3 (x, y, 0.5f));
						InstantiateGameObject (floorTiles, "FloorSprite", new Vector3 (x, y, -0.5f), new Vector3 (0f, 180f, 180f));
					}
				}
			}

			for (int i = 0; i < gridPositions.Count; i++) {
				InstantiateGameObject (floorTiles, "FloorSprite", new Vector3 (gridPositions [i].x, gridPositions [i].y, 0.5f));
				InstantiateGameObject (floorTiles, "FloorSprite", new Vector3 (gridPositions [i].x, gridPositions [i].y, -0.5f), new Vector3 (0f, 180f, 180f));
			}
		}
	//}

    void InstantiateGameObject(GameObject[] gameObject, string name ,Vector3 position)
    {
        int i = Random.Range(0, gameObject.Length);
        GameObject instance = (GameObject) Instantiate(gameObject[i], position, Quaternion.identity);
        instance.name = string.Format("{0}_({1}, {2})", gameObject[i].name, position.x, position.y);
        instance.transform.SetParent(dungeonBoard);
        //instance.transform.SetParent(boardHolder);
    }

    void InstantiateGameObject(GameObject[] gameObject, string name, Vector3 position, Vector3 rotation)
    {
        GameObject instance;
        int i = Random.Range(0, gameObject.Length);
        instance = (GameObject)Instantiate(gameObject[i], position, Quaternion.Euler(rotation));
        instance.name = string.Format("{0}_({1}, {2})", gameObject[i].name, position.x, position.y);
        instance.transform.SetParent(dungeonBoard);
        //instance.transform.SetParent(boardHolder);
    }

	Vector3 RandomPosition ()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];

        gridPositions.RemoveAt(randomIndex);
       return randomPosition;
	}

	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);
		for (int i = 0; i < objectCount; i++) {
			Vector3 randomPosition = RandomPosition ();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            GameObject go = (GameObject) Instantiate (tileChoice, randomPosition, Quaternion.identity);
            go.name = string.Format("{0}_({1}, {2})", tileChoice.name, randomPosition.x, randomPosition.y);
            go.transform.SetParent(boardHolder);
		}
	}

	public void SetupScene (int level)
	{
        wallCount = new Count(columns * 2, Mathf.RoundToInt(columns * Mathf.PI));
        coinCount = new Count(columns, columns * 2);

		//BoardSetup ();
		InitialiseList ();
        LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
        BoardSetup();
        LayoutObjectAtRandom (coinTiles, coinCount.minimum, coinCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
		int enemyCount = (int)Mathf.Log (level, 2f);
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, cornerPositions[Random.Range(0, cornerPositions.Count)], Quaternion.identity);
        //Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
	}

    /*
    void Start()
    {
        SetupScene(0);
    }
    */
}
