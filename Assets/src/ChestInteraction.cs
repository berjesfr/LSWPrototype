using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{   
    public Sprite m_OpenSprite;

    private SpriteRenderer m_ChestRenderer;
    private bool m_Closed = true;
    private bool m_PlayerNearby;

    void Start()
    {
        m_Closed = true;
        m_PlayerNearby = false;
        m_ChestRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_PlayerNearby && m_Closed) {
            m_Closed = false;
            m_ChestRenderer.sprite = m_OpenSprite;
            PlayerInventory.instance.m_Coins += 100;

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
        }
    }
    
}
