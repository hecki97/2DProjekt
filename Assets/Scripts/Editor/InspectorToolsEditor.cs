using UnityEngine;
using System.Collections;
using UnityEditor;

public class InspectorToolsEditor : EditorWindow {

    private int editorWindowToolbarCount = 0;
    private int rgbToHexToolbarCount = 0;
    private int hexToRgbToolbarCount = 0;
    private Color32 tmp_rgbToHexColor32 = new Color32();
    private Color32 tmp_hexToRgbColor32 = new Color32();
    private string inputPrefabPath = string.Empty;
    private string prefabPath = string.Empty;
    private Object inputObject;
    private HelpBox helpBox = new HelpBox();
    
    public class HelpBox
    {
        protected string message;
        protected MessageType type;

        public string Message {
            get { return this.message; }
            set { this.message = value; }
        }
        
        public MessageType Type {
            get { return this.type; }
            set { this.type = value; }
        }

        public HelpBox()
        {
            message = string.Empty;
            type = MessageType.Info;
        }
        
        public HelpBox(MessageType type, string message)
        {
            this.message = message;
            this.type = type;
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
        editorWindowToolbarCount = GUILayout.Toolbar(editorWindowToolbarCount, new string[] { "Verify Path", "Get Path", "Color Editor" });
        EditorGUILayout.BeginVertical();
        switch (editorWindowToolbarCount)
        {
            case 0:
                EditorGUILayout.BeginVertical();
                EditorGUILayout.HelpBox(helpBox.Message, helpBox.Type);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Assets/Resources/"), GUILayout.MaxWidth(110f));
                inputPrefabPath = EditorGUILayout.TextField(string.Empty, inputPrefabPath);
                EditorGUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(new GUIContent("Copy to Clipboard")))
                    EditorGUIUtility.systemCopyBuffer = "Assets/Resources/" + inputPrefabPath;
                EditorGUILayout.EndVertical();
                break;
            case 1:
                EditorGUILayout.BeginVertical ();
                //EditorGUILayout.HelpBox("Add Prefab to get Path", MessageType.Info);
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Add Object:"), GUILayout.MaxWidth(70f));
                inputObject = EditorGUILayout.ObjectField(inputObject, typeof(Object));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Object Path:"), GUILayout.MaxWidth(70f));
                EditorGUILayout.SelectableLabel(prefabPath);
                EditorGUILayout.EndHorizontal();
                if (GUILayout.Button(new GUIContent("Copy to Clipboard")))
                    EditorGUIUtility.systemCopyBuffer = prefabPath;
                EditorGUILayout.EndVertical();
                break;
            case 2:
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("RGB -> HEX"), GUILayout.Width(75f));
                switch (rgbToHexToolbarCount) {
                    case 0:
                        tmp_rgbToHexColor32.r = (byte) EditorGUILayout.FloatField(tmp_rgbToHexColor32.r, GUILayout.Width(30f));
                        tmp_rgbToHexColor32.g = (byte) EditorGUILayout.FloatField(tmp_rgbToHexColor32.g, GUILayout.Width(30f));
                        tmp_rgbToHexColor32.b = (byte) EditorGUILayout.FloatField(tmp_rgbToHexColor32.b, GUILayout.Width(30f));
                        tmp_rgbToHexColor32.a = (byte) 255; 
                        break;
                    case 1:
                        tmp_rgbToHexColor32 = EditorGUILayout.ColorField(tmp_rgbToHexColor32, GUILayout.Width(100f));
                        break;
                }
                rgbToHexToolbarCount = GUILayout.Toolbar(rgbToHexToolbarCount, new string[] {"Float", "RGB"}, GUILayout.Height(15f));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Hexstring: "), GUILayout.Width(75f));
                string tmp_hexcol = ColorUtil.ConvertRGBtoHEX(tmp_rgbToHexColor32);
                EditorGUILayout.SelectableLabel(tmp_hexcol, GUILayout.Width(100f));
                if (GUILayout.Button(new GUIContent("Copy")))
                    EditorGUIUtility.systemCopyBuffer = tmp_hexcol;
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("HEX -> RGB"), GUILayout.Width(75f));
                string tmp_str = string.Empty;
                tmp_str = EditorGUILayout.TextArea(tmp_str, GUILayout.Width(100f));
                EditorGUILayout.EndHorizontal();//Enter the Pentagon
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("RGB: "), GUILayout.Width(75f));
                if (tmp_str.Length >= 6)
                 tmp_hexToRgbColor32 = ColorUtil.ConvertHEXtoRGB(tmp_str);
                switch (hexToRgbToolbarCount) {
                    case 0:
                        tmp_hexToRgbColor32.r = (byte) EditorGUILayout.FloatField(tmp_hexToRgbColor32.r, GUILayout.Width(30f));
                        tmp_hexToRgbColor32.g = (byte) EditorGUILayout.FloatField(tmp_hexToRgbColor32.g, GUILayout.Width(30f));
                        tmp_hexToRgbColor32.b = (byte) EditorGUILayout.FloatField(tmp_hexToRgbColor32.b, GUILayout.Width(30f));
                        tmp_hexToRgbColor32.a = (byte) 255; 
                        break;
                    case 1:
                       EditorGUILayout.ColorField(tmp_hexToRgbColor32, GUILayout.Width(100f));
                       break;
                }
                hexToRgbToolbarCount = GUILayout.Toolbar(hexToRgbToolbarCount, new string[] {"Float", "RGB"}, GUILayout.Height(15f));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                break;
        }
        EditorGUILayout.EndVertical();
    }

    void Update()
    {
        if (string.IsNullOrEmpty(inputPrefabPath))
            helpBox.SetContent(MessageType.Warning, "Add to verify path!");
        else       
           helpBox = Resources.Load(inputPrefabPath) ? new HelpBox(MessageType.Info, "Prefab found!") : new HelpBox(MessageType.Error, "Prefab couldn't be found!");

        prefabPath = object.ReferenceEquals(inputObject, null) ? "Object is null or empty!" : AssetDatabase.GetAssetPath(inputObject);
    }
}
