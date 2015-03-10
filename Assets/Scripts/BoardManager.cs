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
	//public Count coinCount = new Count (9, 18);
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
	private List<Vector3> gridPositions = new List<Vector3> ();

	void InitialiseList ()
	{
		gridPositions.Clear ();
		for (int x = 1; x < columns - 1; x++) {
			for (int y = 1; y < rows - 1; y++) {
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}
	}

	void BoardSetup ()
	{
		boardHolder = new GameObject ("Board").transform;

        //Test!!
        if (GameManager.instance.gameMode == GameMode.TwoD)
        {
            for (int x = -1; x < columns + 1; x++)
            {
                for (int y = -1; y < rows + 1; y++)
                {
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    if (x == -1 || x == columns || y == -1 || y == rows)
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.Euler(new Vector3(90f, 180f, 0f))) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
        else
        {
            for (int x = -1; x < columns + 1; x++)
            {
                for (int y = -1; y < rows + 1; y++)
                {
                    GameObject instance;
                    if (x == -1 || x == columns || y == -1 || y == rows)
                        instance = Instantiate(outerWallTiles[Random.Range(0, outerWallTiles.Length)], new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    else
                    {
                         instance = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector3(x, y, 0.5f), Quaternion.Euler(new Vector3(270f, 0f, 0f))) as GameObject;
                         instance = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector3(x, y, -0.5f), Quaternion.Euler(new Vector3(90f, 0f, 0f))) as GameObject;
                    }


                    //GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
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
            Instantiate (tileChoice, randomPosition, Quaternion.identity);
		}
	}

	public void SetupScene (int level)
	{
        wallCount = new Count(columns * 2, Mathf.RoundToInt(columns * Mathf.PI));
        coinCount = new Count(columns, columns * 2);

		BoardSetup ();
		InitialiseList ();
        LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
		LayoutObjectAtRandom (coinTiles, coinCount.minimum, coinCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
		int enemyCount = (int)Mathf.Log (level, 2f);
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
	}
}
