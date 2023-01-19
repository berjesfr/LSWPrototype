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

    public void SetupBuyData(OutfitSprite item)
    {
        m_Item = item;
        m_Image.sprite = m_Item.sprite;
        m_TextMeshPro.text = $"{m_Item.price} Coins";
        //TODO colocar cor vermelha nas coisas caso não tenha dim
        m_BuyButton.onClick.AddListener(Buy);
    }

    public void SetupSellData(OutfitSprite item)
    {
        m_Item = item;
        m_Image.sprite = m_Item.sprite;
        m_TextMeshPro.text = $"{m_Item.price} Coins";
        //TODO colocar cor vermelha nas coisas caso não tenha dim
        m_BuyButton.onClick.AddListener(Sell);
    }

    private void Buy()
    {   
        if (PlayerInventory.instance.m_Coins < m_Item.price) return;
        PlayerInventory.instance.ItemBought(m_Item);
    }

    private void Sell()
    {
        //if (PlayerInventory.instance.m_Coins < m_Item.price) return;
        PlayerInventory.instance.ItemSold(m_Item);
        Destroy(gameObject);
    }

}
