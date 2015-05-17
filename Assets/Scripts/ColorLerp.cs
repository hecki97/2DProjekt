using UnityEngine;
using System.Collections;

public class ColorLerp : MonoBehaviour {

    public float duration = 0.15f;

    private Color32 colorStart;
    private Color32 colorEnd;
    private bool isActive = false;
    private MeshRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<MeshRenderer>();

        renderer.material.color = GameManager.instance.levelColor;
        colorStart = ColorUtil.getRandomColor();
        colorEnd = ColorUtil.getRandomColor();
	}

	// Update is called once per frame
	void Update () {
	    if (GameManager.instance.secretModeActive)
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            renderer.material.color = Color.Lerp(colorStart, colorEnd, lerp);
        }
        else
        {
            if (renderer.material.color == GameManager.instance.levelColor) return;
            renderer.material.color = GameManager.instance.levelColor;
        }
	}
}
