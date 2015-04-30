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
        transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floor_material;
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

    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initialized_edge_count += 1;
    }
}
