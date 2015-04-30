using UnityEngine;
using System.Collections;

public class ItemCoinCollider : MonoBehaviour {

    public AudioClip pickupSound1;
    public AudioClip pickupSound2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.coinsCount++;
            SoundManager.instance.RandomizePickupSfx(pickupSound1, pickupSound2);
            this.gameObject.SetActive(false);
        }
    }
}
