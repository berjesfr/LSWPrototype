using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCShopHandler : MonoBehaviour
{
    public List<OutfitSprite> m_AvailableItems;
    public GameObject m_ShopItemPrefab;
    public GameObject m_BuyContainer;
    public GameObject m_SellContainer;
    private void Start() 
    {
        BuyShop();
    }

    public void BuyShop()
    {

        m_SellContainer.SetActive(false);
        m_BuyContainer.SetActive(true);
        DestroyAllShopChildren(m_BuyContainer);
        foreach(OutfitSprite item in m_AvailableItems) {
            GameObject shopItem = Instantiate(m_ShopItemPrefab);
            shopItem.transform.SetParent(m_BuyContainer.transform);
            shopItem.GetComponent<ShopItemHandler>().SetupBuyData(item);
        }
    }

    public void SellShop()
    {
        m_SellContainer.SetActive(true);
        m_BuyContainer.SetActive(false);
        DestroyAllShopChildren(m_SellContainer);
        foreach(OutfitSprite item in PlayerInventory.instance.m_OwnedOutfits) {
            if (item.sprite == null || item.price == 0) continue;
            GameObject shopItem = Instantiate(m_ShopItemPrefab);
            shopItem.transform.SetParent(m_SellContainer.transform);
            shopItem.GetComponent<ShopItemHandler>().SetupSellData(item);
        }
    }

    public void DestroyAllShopChildren(GameObject target)
    {    
        int nbChildren = target.transform.childCount;

        for (int i = nbChildren - 1; i >= 0; i--) {
            DestroyImmediate(target.transform.GetChild(i).gameObject);
        }
    
    }

}