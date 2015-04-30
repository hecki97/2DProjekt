using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

public enum ItemType {Item, Food, Drink};
[XmlRoot("Item")]
public class ItemData {

    [XmlAttribute("ItemType")]
    public ItemType type = ItemType.Item;
    [XmlAttribute("Name")]
    public string name = string.Empty;
    //[XmlAttribute("PointsPerItem")]
    public Vector3 scale2D = Vector3.one;
    public Vector3 scale3D = Vector3.one;
    public Vector3 offset2D = Vector3.zero;
    public Vector3 offset3D = Vector3.zero;
    public Vector2 pointsPerItem = Vector2.zero;
    [XmlAttribute("RotationSpeed")]
    public int rotSpeed = 0;
    [XmlAttribute("SpriteSheet")]
    public string spritePath = string.Empty;
    [XmlAttribute("SpriteSheetIndex")]
    public string spritePathIndex = string.Empty;
}
