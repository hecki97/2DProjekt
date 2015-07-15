using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class DungeonNameEditor : EditorWindow
{
    /*private static readonly string filePath = Application.dataPath + "/Resources/XML/";
    private static readonly string fileName = "playerStats.xml";

    private List<bool> itemFoldouts = new List<bool>();
    private List<Vector2> scrollPositions = new List<Vector2>();

    //private static List<DungeonNameData> tables = new List<DungeonNameData>();

    private static List<float> percentages = new List<float>();
    public static List<DungeonNameData> table1 = new List<DungeonNameData>();
    public static List<DungeonNameData> table2 = new List<DungeonNameData>();
    public static List<DungeonNameData> table3 = new List<DungeonNameData>();
    public static List<DungeonNameData> table4 = new List<DungeonNameData>();

    [MenuItem("Game Data/Test")]
    static void init()
    {
        EditorWindow.GetWindow<DungeonNameEditor>();
        Start();
    }

    private static void Start()
    {
        //for (int i = 0; i < 4; i++)
            //itemF

            for (int i = 0; i < table1.Count; i++)
                percentages.Add(table1[i].Chance * 100f);
    }

    void OnGUI()
    {
        if (GUILayout.Button(new GUIContent("Save Tables!")))
            XMLFileHandler.saveXMLFile<DungeonNameData>(table1, filePath, fileName);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("Add Table!")))
        {
            //tables.Add(new TableClass());
            //itemFoldouts.Add(false);
        }
        //if (GUILayout.Button(new GUIContent("Load Table from XML!")))
            //loadXMLFile();
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.LabelField(new GUIContent("StringTable (Dungeon Names)"));
        //scrollPosition[0] = EditorGUILayout.BeginScrollView(scrollPosition[0]);
        /*foldouts[0] = EditorGUILayout.Foldout(foldouts[0], new GUIContent("Table 1"));
        if (foldouts[0])
        {
            if (GUILayout.Button(new GUIContent("Add!")))
                table1.Add(new DungeonNameData("", 100));
            scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1);
            for (int i = 0; i < table1.Count; i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Data"), GUILayout.Width(50));
                table1[i].Data = EditorGUILayout.TextArea(table1[i].Data, GUILayout.Width(100));
                table1[i].Chance = EditorGUILayout.FloatField(percentages[i], GUILayout.Width(35));
                EditorGUILayout.LabelField(new GUIContent("%"), GUILayout.Width(25));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }

        foldouts[1] = EditorGUILayout.Foldout(foldouts[1], new GUIContent("Table 2"));
        if (foldouts[1])
        {
            if (GUILayout.Button(new GUIContent("Add!")))
                table2.Add(new DungeonNameData("", 100));
            scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);
            for (int i = 0; i < table2.Count; i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Data"), GUILayout.Width(50));
                table2[i].Data = EditorGUILayout.TextArea(table2[i].Data, GUILayout.Width(100));
                EditorGUILayout.LabelField(new GUIContent("Percentage"), GUILayout.Width(75));
                table2[i].Chance = EditorGUILayout.FloatField(table2[i].Chance, GUILayout.Width(35));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        //}
        //GUILayout.EndScrollView();
        
    }*/
}