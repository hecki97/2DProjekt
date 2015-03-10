using UnityEngine;
using System.Collections;

public static class Utilities
{
    public static GameObject GetChild(this GameObject inside, string wanted, bool recursive = false)
    {
        foreach (Transform child in inside.transform)
        {
            if (child.name == wanted) return child.gameObject;
            if (recursive)
            {
                var within = GetChild(child.gameObject, wanted, true);
                if (within) return within;
            }
        }
        return null;
    }
}
