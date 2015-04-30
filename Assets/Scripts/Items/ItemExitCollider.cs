using UnityEngine;
using System.Collections;

public class ItemExitCollider : MonoBehaviour {

    public float restartLevelDelay = 1f;
    private Animator shopAnim;
    private ShopGUIController shopGUI;
    private Player player;

	// Use this for initialization
	void Start () {
        shopAnim = GameObject.Find("Menus/ShopMenu").GetComponent<Animator>();
        shopGUI = GameObject.Find("Menus/ShopMenu").GetComponent<ShopGUIController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopGUI.SetPlayerStats();
            shopAnim.SetTrigger("triggerMenu");
            GameManager.instance.isPaused = true;
            player.enabled = false;
        }
    }

    void Restart()
    {
        GameManager.instance.level++;
        Application.LoadLevel(Application.loadedLevel);
    }
}
