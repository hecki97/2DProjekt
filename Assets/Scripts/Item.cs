using UnityEngine;
using System.Collections;

public enum ItemTypes {Food, Exit};
public class Item : MonoBehaviour {

	public AudioClip[] pickupSounds;
	public int pointsPerItemMin;
	public int pointsPerItemMax;
	public ItemTypes itemType;
	
	void OnTriggerEnter2D(Collider2D other) {
		SoundManager.instance.RandomizeSfx (pickupSounds);
//		other.SendMessage ("OnPickup", itemType, Random.Range (pointsPerItemMin, pointsPerItemMax));
	}
}