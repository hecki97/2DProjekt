using UnityEngine;
using System.Collections;

public class LevelDesigner : MonoBehaviour
{

    public GameObject prefab;
    public Vector2 gizmoPosition;
    public float depth = 0;
    public Color gizmosColor = Color.grey;
    public Vector3 rotation;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(new Vector3(gizmoPosition.x, gizmoPosition.y, depth), new Vector3(1, 1, 1));
    }
}
