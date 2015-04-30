using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class XMLFileHandler : MonoBehaviour {

    /*
    public static void loadXMLFile<T>(string filePath, string fileName, out List<T> data, out List<bool> itemFoldouts)
    {
        //data = new List<T>();
        //itemFoldouts = new List<bool>();

        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            using (FileStream file = new FileStream(filePath + fileName, FileMode.Open))
            {
                try
                {
                    data = (List<T>) serializer.Deserialize(file);
                    itemFoldouts = new List<bool>(data.Count);

                    for (int i = 0; i < data.Count; i++)
                        itemFoldouts.Add(false);

                    Log("File successfully loaded!");
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }
    }
    */

    public static void saveXMLFile<T>(List<T> data, string filePath, string fileName)
    {
        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            serializer.Serialize(file, data);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
            Log("File successfully saved!");
        }
    }

    public static void DeserializeXMLFile<T>(TextAsset xmlFile, out List<T> data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

        using (StringReader reader = new StringReader(xmlFile.text))
        {
            data = (List<T>)serializer.Deserialize(reader);
            Log("File " + xmlFile.name + ".xml successfully deserialized!");
        }
    }

    private static void Log(string msg)
    {
        Debug.Log("[" + DateTime.Now.ToString("hh:mm:ss") + "] " + msg);
    }
}
