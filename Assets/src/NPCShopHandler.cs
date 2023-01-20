using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class NPCShopHandler : MonoBehaviour
{
    public List<OutfitSprite> availableItems;
    public GameObject shopItemPrefab;
    public GameObject buyShopContainer;
    public GameObject sellShopContainer;

    public void BuyShop()
    {
        sellShopContainer.SetActive(false);
        buyShopContainer.SetActive(true);
        CleanShop(buyShopContainer);
        foreach(OutfitSprite item in availableItems) {
            GameObject shopItem = Instantiate(shopItemPrefab);
            shopItem.transform.SetParent(buyShopContainer.transform);
            shopItem.GetComponent<ShopItemHandler>().SetupBuyData(item, this);
        }
    }

    public void SellShop()
    {
        sellShopContainer.SetActive(true);
        buyShopContainer.SetActive(false);
        CleanShop(sellShopContainer);
        foreach(OutfitSprite item in PlayerInventory.instance.ownedOutfits) {
            if (item.sprite == null || item.price == 0) continue;
            GameObject shopItem = Instantiate(shopItemPrefab);
            shopItem.transform.SetParent(sellShopContainer.transform);
            shopItem.GetComponent<ShopItemHandler>().SetupSellData(item);
        }
    }

    public void UpdateShopItems()
    {
        int nbChildren = buyShopContainer.transform.childCount;
        for (int i = nbChildren - 1; i >= 0; i--) {
            buyShopContainer.transform.GetChild(i).gameObject.GetComponent<ShopItemHandler>().UpdateTextUI();
        }
    }


    public void CleanShop(GameObject target)
    {    
        int nbChildren = target.transform.childCount;
        for (int i = nbChildren - 1; i >= 0; i--) {
            DestroyImmediate(target.transform.GetChild(i).gameObject);
        }
    }

}