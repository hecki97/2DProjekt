using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class ItemsEditor : EditorWindow
{
    private static readonly string filePath = Application.dataPath + "/Resources/XML/";
    private static readonly string fileName = "Item.xml";

    private static List<ItemData> items = new List<ItemData>();
    private static List<bool> itemFoldouts = new List<bool>();
    private static List<bool> foldout = new List<bool>();
    private static List<Sprite> sprite = new List<Sprite>();

    private Vector2 scrollPosition;

    [MenuItem("Game Data/Item Editor")]
    static void init()
    {
        EditorWindow.GetWindow<ItemsEditor>();
        loadXMLFile();
    }

    void OnLostFocus()
    {
        XMLFileHandler.saveXMLFile<ItemData>(items, filePath, fileName);
    }

    private static void loadXMLFile()
    {
		items.Clear ();
		itemFoldouts.Clear ();
		foldout.Clear ();
		sprite.Clear ();
        if (File.Exists(filePath + fileName))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ItemData>));

            using (FileStream file = new FileStream(filePath + fileName, FileMode.Open))
            {
                try
                {
                    items = serializer.Deserialize(file) as List<ItemData>;
                    //itemFoldouts = new List<bool>(items.Count);

                    for (int i = 0; i < items.Count; i++)
                    {
                        Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/" + items[i].spritePath);
                        sprite.Add(new Sprite());
                        itemFoldouts.Add(false);
                        foldout.Add(false);
                        sprite[i] = new Sprite();
                        for (int j = 0; j < sprites.Length; j++)
                        {
                            if (sprites[j].name == items[i].spritePathIndex)
                                sprite[i] = sprites[j];
                        }
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
        if (GUILayout.Button(new GUIContent("Save Items List!")))
            XMLFileHandler.saveXMLFile<ItemData>(items, filePath, fileName);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("Add New Item!")))
        {
			if (items.Count <= 0)
				loadXMLFile();
            items.Add(new ItemData());
            itemFoldouts.Add(false);
            foldout.Add(false);
            sprite.Add(new Sprite());
        }

        if (GUILayout.Button(new GUIContent("Load Item List!")))
            loadXMLFile();
        EditorGUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < items.Count; i++)
        {
            string guiContent = "New Item";
            if (!string.IsNullOrEmpty(items[i].name))
                guiContent = items[i].name;

            itemFoldouts[i] = EditorGUILayout.Foldout(itemFoldouts[i], new GUIContent(guiContent + " (" + items[i].type + ")"));

            if (itemFoldouts[i])
            {
                sprite[i] = (Sprite) EditorGUILayout.ObjectField(new GUIContent("Item Sprite:"), sprite[i], typeof(Sprite), true);
                if (sprite[i] != null)
                {
                    items[i].spritePath = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(sprite[i]));
                    items[i].spritePathIndex = sprite[i].name;
                }
                items[i].name = EditorGUILayout.TextField(new GUIContent("Item Name:"), items[i].name);
                items[i].type = (ItemType) EditorGUILayout.EnumPopup("Item Type:", items[i].type);
                items[i].rotSpeed = EditorGUILayout.IntField(new GUIContent("RotationSpeed"), items[i].rotSpeed);
                items[i].pointsPerItem = EditorGUILayout.Vector2Field(new GUIContent("PointsPerItem:"), items[i].pointsPerItem);

                foldout[i] = EditorGUILayout.Foldout(foldout[i], new GUIContent("Set Scale & Offset"));
                if (foldout[i])
                {
                    items[i].scale2D = EditorGUILayout.Vector3Field(new GUIContent("Scale2D"), items[i].scale2D);
                    items[i].scale3D = EditorGUILayout.Vector3Field(new GUIContent("Scale3D"), items[i].scale3D);
                    items[i].offset2D = EditorGUILayout.Vector3Field(new GUIContent("Offset2D"), items[i].offset2D);
                    items[i].offset3D = EditorGUILayout.Vector3Field(new GUIContent("Offste3D"), items[i].offset3D);
                }

                if (GUILayout.Button(new GUIContent("Remove Item!")))
                {
                    items.RemoveAt(i);
                    sprite.RemoveAt(i);
                    itemFoldouts.RemoveAt(i);
                    i--;
                }
            }
        }
        GUILayout.EndScrollView();
        if (GUILayout.Button(new GUIContent("Clear List!")))
        {
            if (EditorUtility.DisplayDialog("Warning!", "Do you really want to clear the whole List?", "Yes!", "No!"))
            {
                items.Clear();
                itemFoldouts.Clear();
                foldout.Clear();
                sprite.Clear();
                XMLFileHandler.saveXMLFile<ItemData>(items, filePath, fileName);
            }
        }
    }

    void OnDestroy()
    {
        XMLFileHandler.saveXMLFile<ItemData>(items, filePath, fileName);
    }
}