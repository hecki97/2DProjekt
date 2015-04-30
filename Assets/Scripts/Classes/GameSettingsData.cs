using UnityEngine;
using System.Collections;

public enum Difficulty { Dev, Easy, Normal, Hard };
public enum DungeonSize { Small, Large }
public class GameSettingsData {

    public Difficulty difficulty = Difficulty.Normal;
    public DungeonSize dungeonSize = DungeonSize.Small;
    public float musicVolume = 10;
    public float soundVolume = 10;
}
