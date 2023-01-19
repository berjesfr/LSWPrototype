using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryClothingHandler : MonoBehaviour
{   
    public SpriteRenderer m_BodyPart;
    public string m_Type;

    private List<Sprite> m_Options;
    private Image m_Image;
    public int m_CurrentOption = 0;

    private void Start()
    {
        m_Image = GetComponentsInChildren<Image>()[0];
        LoadOptions();
    }


    private void UpdateOptions()
    {        
        m_Options = new List<Sprite>(PlayerInventory.instance.GetOptions(m_Type));
    }

    private void LoadOptions() 
    {
        m_Options = new List<Sprite>(PlayerInventory.instance.GetOptions(m_Type));
        if (m_Options.Count <= 0) return;
        m_BodyPart.sprite = m_Options[m_CurrentOption];
        m_Image.sprite = m_BodyPart.sprite;
        if (m_Image.sprite != null) {
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 1.0f);
        } else {
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 0.0f);
        }

    }

    public void NextOption () 
    {
        UpdateOptions();
        if (m_Options.Count <= 0) return;

        m_CurrentOption++;
        if (m_CurrentOption >= m_Options.Count) {
            m_CurrentOption = 0;
        }
        m_BodyPart.sprite = m_Options[m_CurrentOption];
        m_Image.sprite = m_BodyPart.sprite;
        if (m_Image.sprite != null) {
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 1.0f);
        } else {
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 0.0f);
        }
        
    }
    public void PreviousOption()
    {
        UpdateOptions();
        if (m_Options.Count <= 0) return;

        m_CurrentOption--;
        if (m_CurrentOption < 0) {
            m_CurrentOption = m_Options.Count - 1;
        }
        m_BodyPart.sprite = m_Options[m_CurrentOption];
        m_Image.sprite = m_BodyPart.sprite;
        if (m_Image.sprite != null) {
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 1.0f);
        } else {
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 0.0f);
        }    
    }

    public int GetIndexOnOptions(OutfitSprite item)
    {
        return m_Options.IndexOf(m_Options.Find(i => i == item.sprite));
    }


    public void HandleWornItemSold()
    {
        UpdateOptions();
        m_CurrentOption = 0;
        m_BodyPart.sprite = m_Options[m_CurrentOption];
        m_Image.sprite = m_BodyPart.sprite;
        if (m_Image.sprite != null) {
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 1.0f);
        } else {
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 0.0f);
        }
    }
}
