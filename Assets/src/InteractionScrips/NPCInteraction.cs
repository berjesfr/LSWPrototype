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
            NPCShopHandler handler = shopPanel.GetComponent<NPCShopHandler>();
            handler.BuyShop();
            shopPanel.SetActive(true);
            _source.Play();
            _source.pitch = 3.0f;
            _source.volume = 1.5f;
        }
    
        if (shopPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
            shopPanel.SetActive(false);
            NPCShopHandler handler = shopPanel.GetComponent<NPCShopHandler>();
            handler.CleanShop(handler.buyShopContainer);
            handler.CleanShop(handler.sellShopContainer);
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
