using UnityEngine;
using System.Collections;

public class MinimapIconScript : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    Camera main;

	// Use this for initialization
	void Start () {
        spriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.GetComponent<Renderer>().isVisible)
            spriteRenderer.enabled = true;
	}
}
