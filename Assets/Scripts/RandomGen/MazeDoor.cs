using UnityEngine;
using System.Collections;

public class MazeDoor : MazePassage {

    public Transform hinge;

    private static Quaternion normalRotation = Quaternion.Euler(0f, 0f, 280f), mirroredRotation = Quaternion.Euler(0f, 0f, 90f);
    private static Vector3 normalPosition = new Vector3(-0.025f, -0.05f, 0f), mirroredPosition = new Vector3(1f, 0.005f, 0f);
    private bool isMirrored = false;

    private MazeDoor OtherSideOfDoor
    {
        get {
            return (MazeDoor) other_cell.GetEdge(direction.GetOpposite());
        }
    }

    public override void OnPlayerEntered()
    {
        OtherSideOfDoor.hinge.localRotation = hinge.localRotation = isMirrored ? mirroredRotation : normalRotation;
        OtherSideOfDoor.hinge.localPosition = hinge.localPosition = isMirrored ? mirroredPosition : normalPosition;
        OtherSideOfDoor.cell.room.Show();
    }

    public override void OnPlayerExited()
    {
        OtherSideOfDoor.hinge.localRotation = hinge.localRotation = Quaternion.Euler(0f, 0f, 0f);
        OtherSideOfDoor.hinge.localPosition = hinge.localPosition = Vector3.zero;
        OtherSideOfDoor.cell.room.Hide();
    }

    public override void Initialize(MazeCell primary, MazeCell other, MazeDirection direction)
    {
      
        base.Initialize(primary, other, direction);
        
        if (OtherSideOfDoor != null)
        {
            isMirrored = true;
            /*
            hinge.localScale = new Vector3(-1f, 1f, 1f);
            Vector3 p = hinge.localPosition;
            p.x = -p.x;
            hinge.localPosition = p;
            */
        }
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child != hinge)
                child.GetComponent<Renderer>().material = cell.room.settings.wall_material;
        }
    }
}
