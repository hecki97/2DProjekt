using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[XmlRoot("LevelColorData")]
public class LevelColorData
{
    [XmlAttribute("Name")]
    public string name;
    [XmlAttribute("HexColor")]
    public string hexColor;

    public void SetColorAsHex(Color32 color)
    {
        hexColor = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
    }

    public Color32 GetColor32()
    {
        byte r = byte.Parse(hexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
