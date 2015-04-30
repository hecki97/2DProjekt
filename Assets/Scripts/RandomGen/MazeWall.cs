using UnityEngine;
using System.Collections;

public class MazeWall : MazeCellEdge {

    public Transform wall;

    public override void Initialize(MazeCell cell, MazeCell other_cell, MazeDirection direction)
    {
        base.Initialize(cell, other_cell, direction);
        wall.GetComponent<Renderer>().material = cell.room.settings.wall_material;
    }
}
