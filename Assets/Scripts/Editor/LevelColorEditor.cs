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
    private static List<bool> itemShowValues = new List<bool>();

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
        itemShowValues.Clear();
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
                        itemShowValues.Add(false);
                        colors.Add(items[i].GetColor32());
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
			//if (items.Count <= 0)
			//	loadXMLFile();
            items.Add(new LevelColorData());
            itemEditName.Add(true);
            colors.Add(new Color32(0, 0, 0, 255));
            itemShowValues.Add(false);
        }
        if (GUILayout.Button(new GUIContent("Load Item List!")))
            loadXMLFile();
        EditorGUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < items.Count; i++)
        {
            if (string.IsNullOrEmpty(items[i].name))
                items[i].name = "New Color";

            EditorGUILayout.BeginHorizontal();
            if (itemEditName[i])
                items[i].name = EditorGUILayout.TextField(items[i].name, GUILayout.Width(Screen.width / 3));
            else
                EditorGUILayout.LabelField(items[i].name, GUILayout.Width(Screen.width / 3));
            itemEditName[i] = GUILayout.Toggle(itemEditName[i], new GUIContent("Edit"), GUILayout.Width(40f));
            if (!itemShowValues[i])
                colors[i] = EditorGUILayout.ColorField(colors[i], GUILayout.Width(Screen.width / 3));
            else
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(Screen.width / 3));
                EditorGUILayout.LabelField(new GUIContent("HEX"), GUILayout.Width(25f));
				EditorGUILayout.SelectableLabel(items[i].GetHex(), GUILayout.Height(15f), GUILayout.Width(47f));
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.BeginHorizontal(GUILayout.Width(Screen.width / 3));
            items[i].SetColorAsHex(colors[i]);
			GUILayout.FlexibleSpace();
            if (GUILayout.Button(new GUIContent("C"), GUILayout.MaxWidth(20f), GUILayout.Height(15f)))
                itemShowValues[i] = !itemShowValues[i];
            
            if (GUILayout.Button(new GUIContent("X"), GUILayout.MaxWidth(20f), GUILayout.Height(15f)))
            {
                items.RemoveAt(i);
                itemEditName.RemoveAt(i);
                colors.RemoveAt(i);
                itemShowValues.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
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
                itemShowValues.Clear();
                XMLFileHandler.saveXMLFile<LevelColorData>(items, filePath, fileName);
            }
        }
    }

    private void OnDestroy()
    {
        XMLFileHandler.saveXMLFile<LevelColorData>(items, filePath, fileName);
    }
}