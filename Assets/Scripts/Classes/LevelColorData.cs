using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[XmlRoot("LevelColorData")]
public class LevelColorData
{
    [XmlAttribute("Name")]
    public string name;
    [XmlAttribute("HexString")]
    public string hexString;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

	public string HexString
	{
        get { return hexString; }
        set { hexString = value; }
	}
}
