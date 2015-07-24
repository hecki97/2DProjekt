using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Stringtable : MonoBehaviour {

    public static List<DungeonNameData> table1 = new List<DungeonNameData>();
    public static List<DungeonNameData> table2 = new List<DungeonNameData>();
    public static List<DungeonNameData> table3 = new List<DungeonNameData>();
    public static List<DungeonNameData> table4 = new List<DungeonNameData>(); 

	// Use this for initialization
	void Awake () {

        table1.Add(new DungeonNameData("Accursed", 5f));
        table1.Add(new DungeonNameData("Ancient", 5f));
        table1.Add(new DungeonNameData("Baneful", 1f));
        table1.Add(new DungeonNameData("Batrachian", 3f));
        table1.Add(new DungeonNameData("Black", 6f));
        table1.Add(new DungeonNameData("Bloodstained", 2f));
        table1.Add(new DungeonNameData("Cold", 1f));
        table1.Add(new DungeonNameData("Dark", 7f));
        table1.Add(new DungeonNameData("Devouring", 1f));
        table1.Add(new DungeonNameData("Diabolical", 1f));
        table1.Add(new DungeonNameData("Ebon", 1f));
        table1.Add(new DungeonNameData("Eldritch", 2f));
        table1.Add(new DungeonNameData("Forbidden", 3f));
        table1.Add(new DungeonNameData("Forgotten", 3f));
        table1.Add(new DungeonNameData("Haunted", 1f));
        table1.Add(new DungeonNameData("Hidden", 3f));
        table1.Add(new DungeonNameData("Lonely", 1f));
        table1.Add(new DungeonNameData("Lost", 3f));
        table1.Add(new DungeonNameData("Malevolent", 1f));
        table1.Add(new DungeonNameData("Misplaced", 1f));
        table1.Add(new DungeonNameData("Nameless", 1f));
        table1.Add(new DungeonNameData("Ophidian", 1f));
        table1.Add(new DungeonNameData("Scarlet", 1f));
        table1.Add(new DungeonNameData("Secret", 2f));
        table1.Add(new DungeonNameData("Shrouded", 1f));
        table1.Add(new DungeonNameData("Squamous", 1f));
        table1.Add(new DungeonNameData("Strange", 1f));
        table1.Add(new DungeonNameData("Tenebrous", 1f));
        table1.Add(new DungeonNameData("Uncanny", 1f));
        table1.Add(new DungeonNameData("Unspeakable", 1f));
        table1.Add(new DungeonNameData("Unvanquishable", 1f));
        table1.Add(new DungeonNameData("Unwholesome", 1f));
        table1.Add(new DungeonNameData("Vanishing", 1f));
        table1.Add(new DungeonNameData("Weird", 1f));

        table2.Add(new DungeonNameData("Abyss", 1f));
        table2.Add(new DungeonNameData("Catacombs", 2f));
        table2.Add(new DungeonNameData("Caverns", 4f));
        table2.Add(new DungeonNameData("Citadel", 2f));
        table2.Add(new DungeonNameData("City", 1f));
        table2.Add(new DungeonNameData("Cyst", 1f));
        table2.Add(new DungeonNameData("Depths", 3f));
        table2.Add(new DungeonNameData("Dungeons", 4f));
        table2.Add(new DungeonNameData("Fane", 3f));
        table2.Add(new DungeonNameData("Fortress", 2f));
        table2.Add(new DungeonNameData("Halls", 2f));
        table2.Add(new DungeonNameData("Haunts", 1f));
        table2.Add(new DungeonNameData("Isle", 1f));
        table2.Add(new DungeonNameData("Keep", 2f));
        table2.Add(new DungeonNameData("Labyrinth", 5f));
        table2.Add(new DungeonNameData("Manse", 3f));
        table2.Add(new DungeonNameData("Maze", 6f));
        table2.Add(new DungeonNameData("Milieu", 1f));
        table2.Add(new DungeonNameData("Mines", 4f));
        table2.Add(new DungeonNameData("Mountain", 3f));
        table2.Add(new DungeonNameData("Oubliette", 2f));
        table2.Add(new DungeonNameData("Panopticon", 2f));
        table2.Add(new DungeonNameData("Pits", 3f));
        table2.Add(new DungeonNameData("Ruins", 2f));
        table2.Add(new DungeonNameData("Sanctum", 2f));
        table2.Add(new DungeonNameData("Shambles", 1f));
        table2.Add(new DungeonNameData("Temple", 3f));
        table2.Add(new DungeonNameData("Tower", 1f));
        table2.Add(new DungeonNameData("Vault", 4f));

        table3.Add(new DungeonNameData("of", 10f));

        table4.Add(new DungeonNameData("the Axolotl", 1f));
        table4.Add(new DungeonNameData("Blood", 1f));
        table4.Add(new DungeonNameData("Bones", 1f));
        table4.Add(new DungeonNameData("Chaos", 1f));
        table4.Add(new DungeonNameData("Curses", 1f));
        table4.Add(new DungeonNameData("the Death", 1f));
        table4.Add(new DungeonNameData("Death", 1f));
        table4.Add(new DungeonNameData("Demons", 1f));
        table4.Add(new DungeonNameData("Despair", 1f));
        table4.Add(new DungeonNameData("Deviltry", 1f));
        table4.Add(new DungeonNameData("Doom", 1f));
        table4.Add(new DungeonNameData("Evil", 1f));
        table4.Add(new DungeonNameData("Fire", 1f));
        table4.Add(new DungeonNameData("Frost", 1f));
        table4.Add(new DungeonNameData("Gloom", 1f));
        table4.Add(new DungeonNameData("Hells", 1f));
        table4.Add(new DungeonNameData("Horrors", 1f));
        table4.Add(new DungeonNameData("Ichor", 1f));
        table4.Add(new DungeonNameData("Id Insinuation", 1f));
        table4.Add(new DungeonNameData("Iron", 1f));
        table4.Add(new DungeonNameData("Madness", 1f));
        table4.Add(new DungeonNameData("Mirrors", 1f));
        table4.Add(new DungeonNameData("Mists", 1f));
        table4.Add(new DungeonNameData("Monsters", 1f));
        table4.Add(new DungeonNameData("Mystery", 1f));
        table4.Add(new DungeonNameData("Necromancy", 1f));
        table4.Add(new DungeonNameData("Oblivion", 1f));
        table4.Add(new DungeonNameData("Peril", 1f));
        table4.Add(new DungeonNameData("Phantasms", 1f));
        table4.Add(new DungeonNameData("Random Harlots", 1f));
        table4.Add(new DungeonNameData("Secrets", 1f));
        table4.Add(new DungeonNameData("Shadows", 1f));
        table4.Add(new DungeonNameData("Sigils", 1f));
        table4.Add(new DungeonNameData("Skulls", 1f));
        table4.Add(new DungeonNameData("Slaughter", 1f));
        table4.Add(new DungeonNameData("Sorcery", 1f));
        table4.Add(new DungeonNameData("Syzygy", 1f));
        table4.Add(new DungeonNameData("Terror", 1f));
        table4.Add(new DungeonNameData("Torment", 1f));
        table4.Add(new DungeonNameData("Treasure", 1f));
        table4.Add(new DungeonNameData("the Undercity", 1f));
        table4.Add(new DungeonNameData("the Underworld", 1f));
        table4.Add(new DungeonNameData("the Unknown", 1f));

        Text dungeonName = GameObject.Find("LevelName").GetComponent<Text>();
        dungeonName.text = GenerateRandomDungeonName();

        DontDestroyOnLoad(this.gameObject);
	}

    protected string GetRandomString(List<DungeonNameData> table)
    {
        int i = Random.Range(0, table.Count);
        float value = Random.value;
        if (value > table[i].Chance)
            return GetRandomString(table);
        else
            return table[i].Data + " ";
    }

	public string GenerateRandomDungeonName()
    {
        return string.Format("~ " + GetRandomString(table1) + GetRandomString(table2) + GetRandomString(table3) + GetRandomString(table4) + " ~");
    }
}
