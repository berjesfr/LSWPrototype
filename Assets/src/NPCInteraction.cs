using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : Interaction
{


    private bool m_PlayerNearby;
    public GameObject m_ShopPanel;
    public GameObject m_Player;

    public override void Setup()
    {
        m_ShopPanel.SetActive(false);
    }

    public override void InteractionAction()
    {
        Vector2 xDelta = m_Player.transform.position - gameObject.transform.position;
        transform.rotation = Quaternion.Euler(0.0f, xDelta.x < 0 ? 0.0f : 180.0f, 0.0f);

        if (m_PlayerNearby && Input.GetKeyDown(KeyCode.E)) {
            m_ShopPanel.SetActive(true);
        }
    
        if (m_ShopPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
            m_ShopPanel.SetActive(false);
        }
    }

    public override void RunPlayerNearby()
    {
        m_PlayerNearby = true;
    }

    public override void RunPlayerLeft()
    {
        m_PlayerNearby = false;
        m_ShopPanel.SetActive(false);
    }
}
