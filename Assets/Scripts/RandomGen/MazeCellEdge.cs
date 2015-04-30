using UnityEngine;
using System.Collections;

public abstract class MazeCellEdge : MonoBehaviour {

    public MazeCell cell, other_cell;
    public MazeDirection direction;

    public virtual void Initialize(MazeCell cell, MazeCell other_cell, MazeDirection direction)
    {
        this.cell = cell;
        this.other_cell = other_cell;
        this.direction = direction;
        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
    }
}
