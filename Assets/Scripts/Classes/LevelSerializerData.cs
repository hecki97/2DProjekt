using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("LevelSerializerData")]
public class LevelSerializerData {

    [XmlAttribute("Name")]
    public string name;
    [XmlAttribute("Dimension")]
    public Vector2 dimension;
    [XmlArray("Tags"), XmlArrayItem("Tag")]
    public List<string> go_names = new List<string>();
    [XmlArray("Positions"), XmlArrayItem("Position")]
    public List<Vector2> go_pos = new List<Vector2>();
}
