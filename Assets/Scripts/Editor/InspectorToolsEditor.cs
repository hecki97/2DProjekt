using UnityEngine;
using System.Collections;
using UnityEditor;

public class InspectorToolsEditor : EditorWindow {

    private HelpBox helpBox = new HelpBox();
    private string input = string.Empty;
    private Object obj;
    private string path = string.Empty;
    
    private int toolbarCount = 0;

    public class HelpBox
    {
        public string message;
        public MessageType type;

        public HelpBox()
        {
            message = string.Empty;
            type = MessageType.Info;
        }

        public void SetContent(MessageType type, string message)
        {
            this.message = message;
            this.type = type;
        }
    }

    [MenuItem("Game Data/Inspector Tools Editor")]
    static void init()
    {
        EditorWindow.GetWindow<InspectorToolsEditor>();
    }

    void OnGUI()
    {
        toolbarCount = GUILayout.Toolbar(toolbarCount, new string[] { "Verify Path", "Get Path", "Color Editor" });
        EditorGUILayout.BeginVertical();
        switch (toolbarCount)
        {
            case 0:
                EditorGUILayout.BeginVertical();
                EditorGUILayout.HelpBox(helpBox.message, helpBox.type);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Assets/Resources/"), GUILayout.MaxWidth(110f));
                input = EditorGUILayout.TextField(string.Empty, input);
                EditorGUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(new GUIContent("Copy to Clipboard")))
                EditorGUIUtility.systemCopyBuffer = "Assets/Resources/" + input;
                EditorGUILayout.EndVertical();
                break;
            case 1:
                EditorGUILayout.BeginVertical ();
                //EditorGUILayout.HelpBox("Add Prefab to get Path", MessageType.Info);
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Add Prefab:"), GUILayout.MaxWidth(70f));
                obj = EditorGUILayout.ObjectField(obj, typeof(Object));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Prefab Path:"), GUILayout.MaxWidth(70f));
                EditorGUILayout.SelectableLabel(path);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                break;
            case 2:
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("RGB -> HEX"));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("HEX -> RGB"));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                break;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(new GUIContent("Copy to Clipboard")))
            EditorGUIUtility.systemCopyBuffer = path;
        EditorGUILayout.EndVertical();
    }

    void Update()
    {
        if (string.IsNullOrEmpty(input))
            helpBox.SetContent(MessageType.Warning, "Add to verify path!");
        else
        {
            bool _bool = Resources.Load(input);

            if (_bool)
                helpBox.SetContent(MessageType.Info, "Prefab found!");
            else
                helpBox.SetContent(MessageType.Error, "Prefab couldn't be found!");
        }

        if (object.ReferenceEquals(obj, null))
            path = "Object is null or empty!";
        else
            path = AssetDatabase.GetAssetPath(obj);
    }
}
