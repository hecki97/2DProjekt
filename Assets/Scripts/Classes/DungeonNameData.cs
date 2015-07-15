using UnityEngine;
using System;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("DungeonNameData")]
public class DungeonNameData {

    [XmlAttribute("ID")]
    public int id;
    [XmlAttribute("Data")]
    public string data;
    [XmlAttribute("Chance")]
    public float chance;

    public DungeonNameData(string data, float percent)
    {
        this.data = data;
        this.chance = percent / 100;
    }

    public DungeonNameData()
    {
        id = 0;
        data = string.Empty;
        chance = 0f;
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public string Data
    {
        get { return data; }
        set { data = value; }
    }

    public float Chance
    {
        get { return chance; }
        set { chance = value; }
    }
}
