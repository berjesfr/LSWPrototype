using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : Interaction
{


    private bool _playerNearby;
    public GameObject shopPanel;
    public GameObject player;

    public override void Setup()
    {
        shopPanel.SetActive(false);
    }

    public override void InteractionAction()
    {
        Vector2 xDelta = player.transform.position - gameObject.transform.position;
        transform.rotation = Quaternion.Euler(0.0f, xDelta.x < 0 ? 0.0f : 180.0f, 0.0f);

        if (_playerNearby && Input.GetKeyDown(KeyCode.E)) {
            shopPanel.SetActive(true);
        }
    
        if (shopPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
            shopPanel.SetActive(false);
        }
    }

    public override void RunPlayerNearby()
    {
        _playerNearby = true;
    }

    public override void RunPlayerLeft()
    {
        _playerNearby = false;
        shopPanel.SetActive(false);
    }
}
