using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{


    private bool m_PlayerNearby;
    public GameObject m_ShopPanel;

    private void Awake()
    {
        m_ShopPanel.SetActive(false);
    }

    private void Update()
    {
        if (m_PlayerNearby && Input.GetKeyDown(KeyCode.E)) {
            m_ShopPanel.SetActive(true);
        }
        if (m_ShopPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
            m_ShopPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            m_PlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.CompareTag("Player")) {
            m_PlayerNearby = false;
            m_ShopPanel.SetActive(false);
        }
    }
}
