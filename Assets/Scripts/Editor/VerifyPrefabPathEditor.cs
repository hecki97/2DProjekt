using UnityEngine;
using System.Collections;
using UnityEditor;

public class VerifyPrefabPathEditor : EditorWindow {

    private HelpBox helpBox = new HelpBox();
    private string input = string.Empty;

    [MenuItem("Game Data/Verify Prefab Path")]
    static void init()
    {
        EditorWindow.GetWindow<VerifyPrefabPathEditor>();
    }
	
	void OnGUI()
    {
        EditorGUILayout.HelpBox(helpBox.message, helpBox.type);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Assets/Resources/"), GUILayout.MaxWidth(Screen.width / 4));
        input = EditorGUILayout.TextField(string.Empty, input);
        EditorGUILayout.EndHorizontal();
    }

    void Update()
    {
        if (string.IsNullOrEmpty(input))
        {
            helpBox.message = "Add to verify path!";
            helpBox.type = MessageType.Warning;
        }
        else
        {
            bool _bool = Resources.Load(input);

            if (_bool)
            {
                helpBox.message = "Prefab found!";
                helpBox.type = MessageType.Info;
            }
            else
            {
                helpBox.message = "Prefab couldn't be found!";
                helpBox.type = MessageType.Error;
            }
        }
    }

    public class HelpBox
    {
        public string message;
        public MessageType type;

        public HelpBox()
        {
            message = string.Empty;
            type = MessageType.Info;
        }
    }
}
