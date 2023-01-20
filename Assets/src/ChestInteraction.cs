using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : Interaction
{   
    public Sprite m_OpenSprite;

    private SpriteRenderer m_ChestRenderer;
    private bool m_Closed = true;
    private bool m_PlayerNearby;

    public override void Setup()
    {
        m_Closed = true;
        m_PlayerNearby = false;
        m_ChestRenderer = GetComponent<SpriteRenderer>();
    }

    public override void InteractionAction()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_PlayerNearby && m_Closed) {
            m_Closed = false;
            m_ChestRenderer.sprite = m_OpenSprite;
            PlayerInventory.instance.m_Coins += 100;
            PlayerInventory.instance.UpdateCoinsUI();
            interactionIndicator.SetActive(false);
        }
    }

    public override void RunPlayerNearby()
    {
        m_PlayerNearby = true;
    }

    public override void RunPlayerLeft()
    {
        m_PlayerNearby = false;
    }
}
