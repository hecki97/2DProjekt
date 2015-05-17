using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float smoothDampTime = 0.2f;
    public int zOffset;
    public int minimumHeight = 0;

    private Vector3 _smoothDampVelocity;
    private Player player;
    private Vector3 position;
    private float currentY;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        position = player.transform.position;
        position.z = minimumHeight;

        /*
        currentY = player.transform.position.y;
        if (currentY >= minimumHeight)
            position.y = currentY - orthoSize + 1;
        else
            position.y = minimumHeight;
        */

        transform.position = Vector3.SmoothDamp(transform.position, position, ref _smoothDampVelocity, smoothDampTime);
    }
}
