using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.IO;
using System;
using TMPro;

[System.Serializable]
public struct OutfitSprite {
    public Sprite sprite;
    public string type;
    public int price;
}

[System.Serializable]
public struct HandlerStruct {
    public InventoryClothingHandler handler;
    public string type;
}

public class PlayerInventory : MonoBehaviour
{   

    //Careful with this one, it is not a true singleton
    public static PlayerInventory instance {get; private set;}
    public GameObject m_InventoryPanel;
    public List<OutfitSprite> m_OwnedOutfits;
    public int m_Coins;

    public List<HandlerStruct> m_InventoryHandlers;
    public TextMeshProUGUI m_CoinsText;

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
        m_CoinsText.text = $"$ {m_Coins}";
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
        UpdateCoinsUI();
    }

    public void ItemSold(OutfitSprite item)
    {
        OutfitSprite outfit = m_OwnedOutfits.Find(i => i.sprite == item.sprite);
        m_OwnedOutfits.Remove(outfit);
        m_Coins += Math.Max(0, item.price); 
        CheckIfSoldEquippedItem(item);    
        UpdateCoinsUI();
    }

    private void CheckIfSoldEquippedItem(OutfitSprite item)
    {   
        HandlerStruct clothingHandler = m_InventoryHandlers.Find(h => h.type == item.type);
        int index = clothingHandler.handler.GetIndexOnOptions(item);
        if (clothingHandler.handler.m_CurrentOption == index) {
            clothingHandler.handler.HandleWornItemSold();
        }
    }

    public void UpdateCoinsUI()
    {
        m_CoinsText.text = $"$ {m_Coins}";
    }
}
