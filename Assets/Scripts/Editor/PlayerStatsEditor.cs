using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Xml.Serialization;

public class PlayerStatsEditor : EditorWindow {

    private static readonly string filePath = Application.dataPath + "/Resources/XML/";
    private static readonly string fileName = "playerStats.xml";

    private static List<PlayerStatsData> playerStats = new List<PlayerStatsData>();
    private static List<bool> itemFoldouts = new List<bool>();

    private static List<int> itemLockSwitch = new List<int>();
    private Vector2 scrollPosition;

    [MenuItem("Game Data/Player Stats Editor")]
    static void init()
    {
        EditorWindow.GetWindow<PlayerStatsEditor>();
        loadXMLFile();
    }

	void OnFocus()
	{
        if (playerStats.Count <= 0)
			loadXMLFile ();
	}

	void OnLostFocus()
	{
        if (playerStats.Count > 0)
            XMLFileHandler.saveXMLFile<PlayerStatsData>(playerStats, filePath, fileName);
	}

    private static void loadXMLFile()
    {
        playerStats.Clear();
		itemFoldouts.Clear ();
        if (File.Exists(filePath + fileName))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerStatsData>));

            using (FileStream file = new FileStream(filePath + fileName, FileMode.Open))
            {
                try
                {
                    playerStats = (List<PlayerStatsData>)serializer.Deserialize(file);
                    for (int i = 0; i < playerStats.Count; i++)
                        itemFoldouts.Add(false);
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
        if (GUILayout.Button(new GUIContent("Save Player Stats!")))
            XMLFileHandler.saveXMLFile<PlayerStatsData>(playerStats, filePath, fileName);

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent("Add Player Stats!")))
		{
            playerStats.Add(new PlayerStatsData());
			itemFoldouts.Add(false);
		}
		if (GUILayout.Button(new GUIContent("Load Item List!")))
			loadXMLFile();
		EditorGUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        for (int i = 0; i < playerStats.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            itemFoldouts[i] = EditorGUILayout.Foldout(itemFoldouts[i], new GUIContent("Player Stats (" + playerStats[i].difficulty + ")"));
            GUILayout.FlexibleSpace();
            playerStats[i].unlocked = GUILayout.Toolbar(playerStats[i].unlocked, new string[] { "U", "L" }, GUILayout.Height(15f));
            if (GUILayout.Button(new GUIContent("X"), GUILayout.MaxWidth(20f), GUILayout.Height(15f)))
            {
                if (playerStats[i].unlocked == 1 && !EditorUtility.DisplayDialog("Warning!", "Do you really want to clear the whole List?", "Yes!", "No!")) return;
                playerStats.RemoveAt(i);
                itemFoldouts.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
            if (itemFoldouts[i])
            {
                EditorGUI.BeginDisabledGroup(playerStats[i].unlocked == 1);
                playerStats[i].difficulty = (Difficulty)EditorGUILayout.EnumPopup(new GUIContent("Difficulty"), playerStats[i].difficulty);
                playerStats[i].playerCoinsCount = EditorGUILayout.IntField(new GUIContent("Coins:"), playerStats[i].playerCoinsCount);
                playerStats[i].playerDamageCount = EditorGUILayout.FloatField(new GUIContent("Damage Count:"), playerStats[i].playerDamageCount);
                playerStats[i].playerFoodPoints = EditorGUILayout.IntField(new GUIContent("Food Points:"), playerStats[i].playerFoodPoints);
                playerStats[i].playerMaxFoodPoints = EditorGUILayout.IntField(new GUIContent("Max Food Points:"), playerStats[i].playerMaxFoodPoints);
                playerStats[i].playerHealthPoints = EditorGUILayout.IntField(new GUIContent("Health Points:"), playerStats[i].playerHealthPoints);
                playerStats[i].playerMaxHealthPoints = EditorGUILayout.IntField(new GUIContent("Max Health Points:"), playerStats[i].playerMaxHealthPoints);
                EditorGUI.EndDisabledGroup();
            }
        }
        GUILayout.EndScrollView();
		if (GUILayout.Button(new GUIContent("Clear List!")))
		{
			if (EditorUtility.DisplayDialog("Warning!", "Do you really want to clear the whole List?", "Yes!", "No!"))
			{
                playerStats.Clear();
				itemFoldouts.Clear();
                XMLFileHandler.saveXMLFile<PlayerStatsData>(playerStats, filePath, fileName);
			}
		}
    }

    private void OnDestroy()
    {
        XMLFileHandler.saveXMLFile<PlayerStatsData>(playerStats, filePath, fileName);
    }
}
