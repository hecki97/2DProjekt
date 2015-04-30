using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryItem : ScriptableObject
{
    List<Item> itemList = new List<Item>();

    public class Item
    {
        public ItemType type;
        public string name;
        public int pointsPerItemMin;
        public int pointsPerItemMax;
        public Sprite[] sprite;
        public AudioClip[] pickupSounds;

        public Item(ItemType _type, string _name, int _pointsPerItemMin, int _pointsPerItemMax, Sprite[] _sprite, AudioClip[] _audioClip)
        {
            type = _type;
            name = _name;
            pointsPerItemMin = _pointsPerItemMin;
            pointsPerItemMax = _pointsPerItemMax;
            sprite = _sprite;
            pickupSounds = _audioClip;
        }

        public string GetItemName()
        {
            return name;
        }

        public Item GetItem()
        {
            return new Item(type, name, pointsPerItemMin, pointsPerItemMax, sprite, pickupSounds);
        }
    }

    public void AddItems(ItemType type, string name, int pointsPerItemMin, int pointsPerItemMax, Sprite[] sprite, AudioClip[] audioClip)
    {
        bool _bool = true;
        Item item = new Item(type, name, pointsPerItemMin, pointsPerItemMax, sprite, audioClip);
        
        foreach (Item _item in itemList)
        {
            if (_item.GetItemName() == name)
                _bool = false;
        }

        if (_bool)
        {
            itemList.Add(item);
            Debug.Log(itemList.Count);
            Debug.Log("Item succesfully added!");
        }
    }

    /*
    public void SetItems(string itemName, float quantity)
    {
        float oldValue = 0;

        itemName = itemName.ToLower();

        if (items.TryGetValue(itemName, out oldValue))
        {
            items[itemName] = quantity;
        }
        else
        {
            items.Add(itemName, quantity);
        }
    }
    */

    public Item GetRandomItem()
    {
        int randomIndex = Random.Range(1, itemList.Count);
            return itemList[randomIndex].GetItem();
    }

    public void Clear()
    {
        itemList.Clear();
    }
}
