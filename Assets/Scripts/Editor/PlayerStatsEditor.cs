using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Xml.Serialization;

public class PlayerStatsEditor : EditorWindow {

    private static readonly string filePath = Application.dataPath + "/Resources/XML/";
    private static readonly string fileName = "playerStats.xml";

    private static List<PlayerStatsData> l_psd = new List<PlayerStatsData>();
    private static List<bool> itemFoldouts = new List<bool>();

    private Vector2 scrollPosition;

    [MenuItem("Game Data/Player Stats Editor")]
    static void init()
    {
        EditorWindow.GetWindow<PlayerStatsEditor>();
        loadXMLFile();
    }

    void OnInspectorUpdate()
    {
        if (!EditorApplication.isPlaying)
            loadXMLFile();
    }

    private static void loadXMLFile()
    {
        if (File.Exists(filePath + fileName))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerStatsData>));

            using (FileStream file = new FileStream(filePath + fileName, FileMode.Open))
            {
                try
                {
                    l_psd = (List<PlayerStatsData>)serializer.Deserialize(file);
                    itemFoldouts = new List<bool>(l_psd.Count);

                    for (int i = 0; i < l_psd.Count; i++)
                        itemFoldouts.Add(false);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }
    }

    private void saveXMLFile()
    {
        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerStatsData>));
            serializer.Serialize(file, l_psd);
            AssetDatabase.Refresh();
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button(new GUIContent("Save Player Stats!")))
            XMLFileHandler.saveXMLFile<PlayerStatsData>(l_psd, filePath, fileName);

        if (GUILayout.Button(new GUIContent("Add New Player Stats!")))
        {
            l_psd.Add(new PlayerStatsData());
            itemFoldouts.Add(false);
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < l_psd.Count; i++)
        {
            itemFoldouts[i] = EditorGUILayout.Foldout(itemFoldouts[i], new GUIContent("Player Stats (" + l_psd[i].difficulty + ")"));
            if (itemFoldouts[i])
            {
                l_psd[i].difficulty = (Difficulty) EditorGUILayout.EnumPopup(new GUIContent("Difficulty"), l_psd[i].difficulty);
                l_psd[i].playerCoinsCount = EditorGUILayout.IntField(new GUIContent("Coins:"), l_psd[i].playerCoinsCount);
                l_psd[i].playerDamageCount = EditorGUILayout.FloatField(new GUIContent("Damage Count:"), l_psd[i].playerDamageCount);
                l_psd[i].playerFoodPoints = EditorGUILayout.IntField(new GUIContent("Food Points:"), l_psd[i].playerFoodPoints);
                l_psd[i].playerMaxFoodPoints = EditorGUILayout.IntField(new GUIContent("Max Food Points:"), l_psd[i].playerMaxFoodPoints);
                l_psd[i].playerHealthPoints = EditorGUILayout.IntField(new GUIContent("Health Points:"), l_psd[i].playerHealthPoints);
                l_psd[i].playerMaxHealthPoints = EditorGUILayout.IntField(new GUIContent("Max Health Points:"), l_psd[i].playerMaxHealthPoints);

                if (GUILayout.Button(new GUIContent("Remove PlayerStats!")))
                {
                    l_psd.RemoveAt(i);
                    itemFoldouts.RemoveAt(i);
                    i--;
                }
            }
        }
        GUILayout.EndScrollView();
    }

    private void OnDestroy()
    {
        XMLFileHandler.saveXMLFile<PlayerStatsData>(l_psd, filePath, fileName);
    }
}
