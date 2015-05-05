using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    public Player player_prefab;
    public Maze maze_prefab;

    private Maze maze_instance;
    private GameObject player_instance;

    private void Start()
    {
        StartCoroutine(BeginGame());
    }

    private IEnumerator BeginGame()
    {
        maze_instance = (Maze)Instantiate(maze_prefab);
        yield return StartCoroutine(maze_instance.Generate());
        //player_instance = (Player)Instantiate(player_prefab);
        player_instance = GameObject.FindGameObjectWithTag("Player").gameObject;
        MazeCell cell = maze_instance.GetCell(maze_instance.RandomCoordinates);
        player_instance.transform.localPosition = new Vector3(cell.transform.localPosition.x, cell.transform.localPosition.y, cell.transform.localPosition.z - .5f);
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
        if (player_instance != null)
            Destroy(player_instance.gameObject);
        StartCoroutine(BeginGame());
    }
}
