using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour {

    public IntVector2 coordinates;
    public MazeRoom room;

    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
    private int initialized_edge_count;

    public void Initialize(MazeRoom room)
    {
        room.Add(this);
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<Renderer>().material = room.settings.floor_material;
    }

    public MazeCellEdge GetEdge(MazeDirection direction)
    {
        return edges[(int)direction];
    }

    public bool IsFullyInitialized
    {
        get {
            return initialized_edge_count == MazeDirections.Count;
        }
    }

    public MazeDirection RandomUninitializedDirection
    {
        get {
            int skips = Random.Range(0, MazeDirections.Count - initialized_edge_count);
            for (int i = 0; i < MazeDirections.Count; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left!");
        }
    }

    public void OnPlayerEntered()
    {
        room.Show();
        for (int i = 0; i < edges.Length; i++)
            edges[i].OnPlayerEntered();
    }

    public void OnPlayerExited()
    {
        room.Hide();
        for (int i = 0; i < edges.Length; i++)
            edges[i].OnPlayerExited();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initialized_edge_count += 1;
    }
}
