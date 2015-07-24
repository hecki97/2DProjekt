using UnityEngine;
using System.Collections.Generic;

public class PlayerStatsManager : MonoBehaviour {

	public static PlayerStatsManager instance = null;
	public TextAsset playerStatsXMLFile;

	private List<PlayerStatsData> list_playerStats;
    public PlayerStatsData playerStats;

	// Use this for initialization
    void Awake () {
		if (instance == null)
        {
            instance = this;
            XMLFileHandler.DeserializeXMLFile<PlayerStatsData>(playerStatsXMLFile, out list_playerStats);
        }
        else if (instance != this)
            Destroy(gameObject);

        Debug.Log(list_playerStats.Count);

		if (list_playerStats.Count <= 0) return;
		LoadPlayerStatsFromXML();
	}
	
	public void LoadPlayerStatsFromXML()
    {
        for (int i = 0; i < list_playerStats.Count; i++)
        {
            if (list_playerStats[i].difficulty == GameManager.instance.difficulty)
                playerStats = list_playerStats[i];
        }
    }
}
