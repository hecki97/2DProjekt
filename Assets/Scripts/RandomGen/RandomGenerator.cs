using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    public Player player_prefab;
    public Maze maze_prefab;
    public GameObject coin;
    public GameObject exit;

    private Maze maze_instance;
    private Player player_instance;

    public void SetupScene()
    {
        StartCoroutine(BeginGame());
    }

    public IEnumerator BeginGame()
    {
        maze_instance = (Maze)Instantiate(maze_prefab);
        yield return StartCoroutine(maze_instance.Generate());
        /*
        int randCoinsCount = Random.Range(15, 25);
        for (int i = 0; i < randCoinsCount; i++)
        {
            MazeCell randCell = maze_instance.GetCell(maze_instance.RandomCoordinates);
            GameObject go = (GameObject) Instantiate(coin, Vector3.zero, Quaternion.identity);
            go.transform.position = new Vector3(randCell.transform.position.x, randCell.transform.position.y, -.75f);
        }
        */
        //Instantiate(exit, (maze_instance.GetCell(maze_instance.RandomCoordinates)).transform.localPosition, Quaternion.identity);
        //player_instance = (Player)Instantiate(player_prefab);
        player_instance = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        MazeCell cell = maze_instance.GetCell(maze_instance.RandomCoordinates);
        player_instance.SetLocation(cell);
        player_instance.transform.localPosition = new Vector3(cell.transform.localPosition.x, cell.transform.localPosition.y, cell.transform.localPosition.z - .5f);
        GameManager.instance.HideLevelImage();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            RestartGame();
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(maze_instance.gameObject);
        //if (player_instance != null)
            //Destroy(player_instance.gameObject);
        StartCoroutine(BeginGame());
    }
}
