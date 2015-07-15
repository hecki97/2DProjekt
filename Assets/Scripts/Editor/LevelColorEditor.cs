using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class LevelColorEditor : EditorWindow {

    private static readonly string filePath = Application.dataPath + "/Resources/XML/";
    private static readonly string fileName = "LevelColor.xml";

    private static List<LevelColorData> items = new List<LevelColorData>();
    private static List<bool> itemEditName = new List<bool>();
    private static List<int> itemColorSwitch = new List<int>();

    private static List<Color32> colors = new List<Color32>();

    private Vector2 scrollPosition;

    [MenuItem("Game Data/Level Color Editor")]
    static void init()
    {
        EditorWindow.GetWindow<LevelColorEditor>();
        loadXMLFile();
    }

	void OnFocus()
	{
		if (items.Count <= 0)
			loadXMLFile ();
	}

    void OnLostFocus()
    {
		if (items.Count > 0)
        	XMLFileHandler.saveXMLFile<LevelColorData>(items, filePath, fileName);
    }

    private static void loadXMLFile()
    {
        items.Clear();
        itemEditName.Clear();
        colors.Clear();
        itemColorSwitch.Clear();
        if (File.Exists(filePath + fileName))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<LevelColorData>));

            using (FileStream file = new FileStream(filePath + fileName, FileMode.Open))
            {
                try
                {
                    items = (List<LevelColorData>) serializer.Deserialize(file);
                    for (int i = 0; i < items.Count; i++)
                    {
                        itemEditName.Add(false);
                        itemColorSwitch.Add(0);
                        colors.Add(ColorUtil.ConvertHEXtoRGB(items[i].HexString));
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button(new GUIContent("Save Color List!")))
            XMLFileHandler.saveXMLFile<LevelColorData>(items, filePath, fileName);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("Add New Color!")))
        {
            items.Add(new LevelColorData());
            itemEditName.Add(true);
            colors.Add(new Color32(0, 0, 0, 255));
            itemColorSwitch.Add(0);
        }
        if (GUILayout.Button(new GUIContent("Load Item List!")))
            loadXMLFile();
        EditorGUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < items.Count; i++)
        {
            if (string.IsNullOrEmpty(items[i].Name))
                items[i].Name = "New Color";

            EditorGUILayout.BeginHorizontal();
            if (itemEditName[i])
                items[i].Name = EditorGUILayout.TextField(items[i].Name, GUILayout.Width(Screen.width / 3));
            else
                EditorGUILayout.LabelField(items[i].Name, GUILayout.Width(Screen.width / 3));
            itemEditName[i] = GUILayout.Toggle(itemEditName[i], new GUIContent("Edit"), GUILayout.Width(40f));
            if (itemColorSwitch[i] == 0)
                colors[i] = EditorGUILayout.ColorField(colors[i], GUILayout.Width(Screen.width / 3), GUILayout.Height(15f));
            else
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(Screen.width / 3));
                EditorGUILayout.LabelField(new GUIContent("HEX"), GUILayout.Height(14f), GUILayout.Width(30f));
				EditorGUILayout.SelectableLabel(items[i].HexString, GUILayout.Height(14f), GUILayout.Width(50f));
                EditorGUILayout.EndHorizontal();
            }
            items[i].HexString = ColorUtil.ConvertRGBtoHEX(colors[i]);
			GUILayout.FlexibleSpace();
            itemColorSwitch[i] = GUILayout.Toolbar(itemColorSwitch[i], new string[] {"RGB", "HEX"}, GUILayout.Height(15f));
            
            if (GUILayout.Button(new GUIContent("X"), GUILayout.MaxWidth(20f), GUILayout.Height(15f)))
            {
                items.RemoveAt(i);
                itemEditName.RemoveAt(i);
                colors.RemoveAt(i);
                itemColorSwitch.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
        if (GUILayout.Button(new GUIContent("Clear List!")))
        {
            if (EditorUtility.DisplayDialog("Warning!", "Do you really want to clear the whole List?", "Yes!", "No!"))
            {
                items.Clear();
                itemEditName.Clear();
                colors.Clear();
                itemColorSwitch.Clear();
                XMLFileHandler.saveXMLFile<LevelColorData>(items, filePath, fileName);
            }
        }
    }

    private void OnDestroy()
    {
        XMLFileHandler.saveXMLFile<LevelColorData>(items, filePath, fileName);
    }
}