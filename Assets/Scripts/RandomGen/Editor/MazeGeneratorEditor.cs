using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Maze))]
public class MazeGeneratorEditor : Editor {

    private bool foldout = false;

    public override void OnInspectorGUI()
    {
        Maze script = (Maze)target;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Dungeon"));
        EditorGUILayout.LabelField(new GUIContent("Width:"), GUILayout.MaxWidth(45f));
        script.size.x = EditorGUILayout.IntField(script.size.x);
        EditorGUILayout.LabelField(new GUIContent("Height:"), GUILayout.MaxWidth(45f));
        script.size.y = EditorGUILayout.IntField(script.size.y);
        EditorGUILayout.EndHorizontal();

        foldout = EditorGUILayout.Foldout(foldout, new GUIContent("Default Inspector"));
        if (foldout)
            base.OnInspectorGUI();
    }
}
