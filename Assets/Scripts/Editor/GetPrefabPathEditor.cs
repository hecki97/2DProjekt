using UnityEngine;
using System.Collections;
using UnityEditor;

public class GetPrefabPathEditor : EditorWindow {

    private Object obj;
    private string path = string.Empty;

    [MenuItem("Game Data/Get Prefab Path")]
    static void init()
    {
        EditorWindow.GetWindow<GetPrefabPathEditor>();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Add Prefab:"), GUILayout.MaxWidth(Screen.width / 5));
        obj = EditorGUILayout.ObjectField(obj, typeof(Object));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Prefab Path:"), GUILayout.MaxWidth(Screen.width / 5));
        EditorGUILayout.SelectableLabel(path);
        EditorGUILayout.EndHorizontal();
    }

    void Update()
    {
        if (obj == null)
            path = "Object is null or empty!";
    }
}
