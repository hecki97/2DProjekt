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
		EditorGUILayout.BeginVertical ();
		GUILayout.FlexibleSpace ();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Add Prefab:"), GUILayout.MaxWidth(70f));
        obj = EditorGUILayout.ObjectField(obj, typeof(Object));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Prefab Path:"), GUILayout.MaxWidth(70f));
        EditorGUILayout.SelectableLabel(path);
        EditorGUILayout.EndHorizontal();
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button(new GUIContent("Copy to Clipboard")))
			EditorGUIUtility.systemCopyBuffer = path;
		EditorGUILayout.EndVertical ();
    }

    void Update()
    {
        if (obj == null)
			path = "Object is null or empty!";
		else
			path = AssetDatabase.GetAssetPath (obj);
    }
}
