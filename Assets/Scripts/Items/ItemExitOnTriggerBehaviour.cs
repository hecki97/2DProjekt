using UnityEngine;
using System.Collections;

public class ItemExitOnTriggerBehaviour : MonoBehaviour {

    public float restartLevelDelay = 1f;
    private Animator shopAnim;
    private ShopGUIController shopGUI;
    private Player player;
    //public float cooldown;

	// Use this for initialization
	void Start () {
        shopAnim = GameObject.Find("Menus/ShopMenu").GetComponent<Animator>();
        shopGUI = GameObject.Find("Menus/ShopMenu").GetComponent<ShopGUIController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shopGUI.SetPlayerStats();
            shopAnim.SetTrigger("triggerMenu");
            GameManager.instance.isPaused = true;
            //player.enabled = false;
        }
    }
    
    void Update() {
        //if (!GameManager.instance.isPaused)
        //    cooldown = (cooldown > 0) ? (cooldown -= 1 * Time.deltaTime) : 0;
    }
    
    void Restart()
    {
        GameManager.instance.level++;
        Application.LoadLevel(Application.loadedLevel);
    }
}
