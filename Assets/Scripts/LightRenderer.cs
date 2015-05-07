using UnityEngine;
using System.Collections.Generic;

public class LightRenderer : MonoBehaviour {

    public List<Light> lights = new List<Light>();

    void OnPreCull()
    {
        for (int i = 0; i < lights.Count; i++)
            lights[i].enabled = false;
    }

	void OnPostRender()
    {
        for (int i = 0; i < lights.Count; i++)
            lights[i].enabled = true;
    }
}
