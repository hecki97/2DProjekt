using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    public Maze maze_prefab;

    private Maze maze_instance;

    private void Start()
    {
        maze_instance = (Maze)Instantiate(maze_prefab);
        StartCoroutine(maze_instance.Generate());
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
        Start();
    }
}
