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
    public GameObject inventoryPanel;
    public List<OutfitSprite> ownedOutfits;
    public int coins;

    public List<HandlerStruct> inventoryHandlers;
    public TextMeshProUGUI coinsText;

    public AudioClip operationSound;
    private AudioSource _source;

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
        coins = 0;
        coinsText.text = $"$ {coins}";
        inventoryPanel.SetActive(true);
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = operationSound;
        _source.volume = 0.5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            inventoryPanel.SetActive(false);
        }
    }
    
    public List<Sprite> GetOptions(string type)
    {
        List<Sprite> options = new List<Sprite>();
        foreach(var item in ownedOutfits) {
            if (type.CompareTo(item.type) == 0) {
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
        ownedOutfits.Add(outfit);
        
        coins -= Math.Max(0, item.price);
        UpdateCoinsUI();
    }

    public void ItemSold(OutfitSprite item)
    {   
        OutfitSprite outfit = ownedOutfits.Find(i => i.sprite == item.sprite);
        ownedOutfits.Remove(outfit);
        coins += Math.Max(0, item.price); 
        CheckIfSoldEquippedItem(item);    
        UpdateCoinsUI();
    }

    private void CheckIfSoldEquippedItem(OutfitSprite item)
    {      
        HandlerStruct clothingHandler = inventoryHandlers.Find(h => h.type.CompareTo(item.type) == 0);
        int index = clothingHandler.handler.GetIndexOnOptions(item);
        if (clothingHandler.handler.currentOption == index) {
            clothingHandler.handler.HandleWornItemSold();
        }
    }

    public void UpdateCoinsUI()
    {
        coinsText.text = $"$ {coins}";
        _source.Play();
    }
}
