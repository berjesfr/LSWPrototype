using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public struct OutfitSprite {
    public Sprite sprite;
    public string type;
    public int price;
}

public class PlayerInventory : MonoBehaviour
{   

    //Careful with this one, it is not a true singleton
    public static PlayerInventory instance {get; private set;}
    public GameObject m_InventoryPanel;
    public List<OutfitSprite> m_OwnedOutfits;
    public int m_Coins;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad (gameObject);
    }

    private void Start()
    {
        m_Coins = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            m_InventoryPanel.SetActive(!m_InventoryPanel.activeSelf);
        }
    }
    
    public List<Sprite> GetOptions(string type)
    {
        List<Sprite> options = new List<Sprite>();
        foreach(var item in m_OwnedOutfits) {
            if (type == item.type) {
                options.Add(item.sprite);
            }
        }
        return options;
    }

    public void ItemBought(OutfitSprite item)
    {
        OutfitSprite outfit;
        outfit.sprite = item.sprite;
        outfit.type = item.type;
        outfit.price = item.price;
        m_OwnedOutfits.Add(outfit);

        m_Coins -= Math.Max(0, item.price);
    }

    public void ItemSold(OutfitSprite item)
    {
        OutfitSprite outfit = m_OwnedOutfits.Find(i => i.sprite == item.sprite);
        m_OwnedOutfits.Remove(outfit);
        m_Coins += Math.Max(0, item.price);
    }
}
