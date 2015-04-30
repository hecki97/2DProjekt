using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class LevelSerializerEditor : EditorWindow {

    private static readonly string filePath = Application.dataPath + "/Resources/XML/";
    private static readonly string fileName = "LevelData.xml";

    private static List<LevelSerializerData> items = new List<LevelSerializerData>();
    private static List<bool> itemFoldouts = new List<bool>();
    
    private Vector2 scrollPosition;

    [MenuItem("Game Data/Level/Level Serializer")]
    static void init()
    {
        EditorWindow.GetWindow<LevelSerializerEditor>();
        loadXMLFile();
    }

    void OnInspectorUpdate()
    {
        if (!EditorApplication.isPlaying)
            loadXMLFile();
    }

    private static void loadXMLFile()
    {
        items = new List<LevelSerializerData>();
        itemFoldouts = new List<bool>();
        
        if (File.Exists(filePath + fileName))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<LevelSerializerData>));

            using (FileStream file = new FileStream(filePath + fileName, FileMode.Open))
            {
                try
                {
                    items = serializer.Deserialize(file) as List<LevelSerializerData>;
                    itemFoldouts = new List<bool>(items.Count);

                    for (int i = 0; i < items.Count; i++)
                    {
                        itemFoldouts.Add(false);
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
        if (GUILayout.Button(new GUIContent("Save Level List!")))
            XMLFileHandler.saveXMLFile<LevelSerializerData>(items, filePath, fileName);

        if (GUILayout.Button(new GUIContent("Add New Level!")))
        {
            items.Add(new LevelSerializerData());
            itemFoldouts.Add(false);
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < items.Count; i++)
        {
            string guiContent = "New Item";
            if (!string.IsNullOrEmpty(items[i].name))
                guiContent = items[i].name;

            itemFoldouts[i] = EditorGUILayout.Foldout(itemFoldouts[i], new GUIContent(guiContent + " (" + items[i].dimension.x + ", " + items[i].dimension.y + ")"));

            if (itemFoldouts[i])
            {
                items[i].name = EditorGUILayout.TextField(new GUIContent("Item Name:"), items[i].name);

                if (GUILayout.Button(new GUIContent("Save Level!")))
                {
                    GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("LevelObject");
                    foreach (GameObject go in gameObjects)
                    {
                        items[i].go_names.Add(go.name);
                        items[i].go_pos.Add(go.transform.position);
                    }
                }

                if (GUILayout.Button(new GUIContent("Remove Level!")))
                {
                    items.RemoveAt(i);
                    itemFoldouts.RemoveAt(i);
                    i--;
                }
            }
        }
        GUILayout.EndScrollView();
    }

    void OnDestroy()
    {
        XMLFileHandler.saveXMLFile<LevelSerializerData>(items, filePath, fileName);
    }
}
