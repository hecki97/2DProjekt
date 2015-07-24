using UnityEngine;
using System.Collections;

public class ItemCoinPickupBehaviour : MonoBehaviour {

    public AudioClip pickupSound1;
    public AudioClip pickupSound2;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStatsManager.instance.playerStats.CoinCount++;
            SoundManager.instance.RandomizePickupSfx(pickupSound1, pickupSound2);
            this.gameObject.SetActive(false);
        }
    }
}
