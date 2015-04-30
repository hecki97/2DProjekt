using UnityEngine;
using System.Collections.Generic;

public class ItemGenericCollider : MonoBehaviour {

    // Event Handler
    public delegate void OnTriggerPickupEvent(ItemType type);
    public static event OnTriggerPickupEvent OnPickup;

    private Vector3 scale2D = new Vector3(1f, 1f, 1f);
    private Vector3 scale3D = new Vector3(1f, 1f, 1f);
    private Vector3 offset2D;
    private Vector3 offset3D;
    public TextAsset itemDataXMLFile;
    private List<ItemData> items;
    private ItemType type = ItemType.Item;
    private int pointsPerItem;
    private int speed;
    private int b_speed;

    public SpriteRenderer[] spriteRenderer;

	// Use this for initialization
	void Start () {
        SecretEventHandler.OnTrigger += this.SecretEventHandler_OnTrigger;
        XMLFileHandler.DeserializeXMLFile<ItemData>(itemDataXMLFile, out items);
        LoadRandomItem();
	}

    void OnDisable()
    {
        SecretEventHandler.OnTrigger -= this.SecretEventHandler_OnTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 1f) * Time.deltaTime * speed);
    }

    public static void Pickup(ItemType type)
    {
        if (OnPickup != null)
            OnPickup(type);
    }

    void LoadRandomItem()
    {
        int i = Random.Range(0, items.Count);

        Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/" + items[i].spritePath);
        for (int j = 0; j < sprites.Length; j++)
        {
            if (sprites[j].name == items[i].spritePathIndex)
            {
                foreach (SpriteRenderer srenderer in spriteRenderer)
                    srenderer.sprite = sprites[j];
            }
        }
        type = items[i].type;
        speed = items[i].rotSpeed;
        scale2D = items[i].scale2D;
        scale3D = items[i].scale3D;
        offset2D = items[i].offset2D;
        offset3D = items[i].offset3D;
        pointsPerItem = Mathf.RoundToInt(Random.Range(items[i].pointsPerItem.x, items[i].pointsPerItem.y));

        transform.localScale = (GameManager.instance.gameMode == GameMode.TwoD) ? scale2D : scale3D;
        transform.position = (GameManager.instance.gameMode == GameMode.TwoD) ? (transform.position + offset2D) : (transform.position + offset3D);
    }

    void SecretEventHandler_OnTrigger()
    {
        if (speed != 750)
        {
            b_speed = speed;
            speed = 750;
        }
        else
            speed = b_speed;
    }

    void AddFoodPoints(int _float)
    {
        int tmp_foodPoints = GameManager.instance.foodPoints + _float;
        if (tmp_foodPoints < GameManager.instance.maxFoodPoints)
            GameManager.instance.foodPoints = tmp_foodPoints;
        else
            GameManager.instance.foodPoints = GameManager.instance.maxFoodPoints;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch (type)
            {
                case ItemType.Food:
                    AddFoodPoints(pointsPerItem);
                    Pickup(type);
                    break;
            }
            this.gameObject.SetActive(false);
        }
    }
}
