using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemHandler : MonoBehaviour
{

    public Image m_Image;
    public TextMeshProUGUI m_TextMeshPro;
    public Button m_BuyButton;

    private OutfitSprite m_Item;
    private NPCShopHandler m_ShopHandler;

    public void SetupBuyData(OutfitSprite item, NPCShopHandler handler)
    {
        m_Item = item;
        m_Image.sprite = m_Item.sprite;
        m_TextMeshPro.text = $"$ {m_Item.price}";
        if (PlayerInventory.instance.m_Coins < m_Item.price) {
            m_TextMeshPro.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        m_BuyButton.onClick.AddListener(Buy);
        m_ShopHandler = handler;
    }

    public void SetupSellData(OutfitSprite item)
    {
        m_Item = item;
        m_Image.sprite = m_Item.sprite;
        m_TextMeshPro.text = $"$ {m_Item.price}";
        m_BuyButton.onClick.AddListener(Sell);
    }

    private void Buy()
    {   
        if (PlayerInventory.instance.m_Coins < m_Item.price) return;
        PlayerInventory.instance.ItemBought(m_Item);
        m_ShopHandler.UpdateShopItems();
    }

    private void Sell()
    {
        PlayerInventory.instance.ItemSold(m_Item);
        Destroy(gameObject);
    }

    public void UpdateTextUI()
    {
        if (PlayerInventory.instance.m_Coins < m_Item.price) {
            m_TextMeshPro.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}
