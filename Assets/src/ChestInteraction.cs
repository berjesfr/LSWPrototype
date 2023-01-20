using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : Interaction
{   
    public Sprite openSprite;

    private SpriteRenderer _chestRenderer;
    private bool _closed = true;
    private bool _playerNearby;

    public override void Setup()
    {
        _closed = true;
        _playerNearby = false;
        _chestRenderer = GetComponent<SpriteRenderer>();
        dialogBox.SetActive(false);
    }

    public override void InteractionAction()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerNearby && _closed) {
            _closed = false;
            _chestRenderer.sprite = openSprite;
            PlayerInventory.instance.coins += 100;
            PlayerInventory.instance.UpdateCoinsUI();
            interactionIndicator.SetActive(false);
            dialogText.text = string.Empty;
            dialogBox.SetActive(true);
            writeText = StartCoroutine(WriteText());
            _source.Play();
        } else if (Input.GetKeyDown(KeyCode.E) && _playerNearby && !_closed) {
            interactionIndicator.SetActive(false);
            dialogText.text = string.Empty;
            toWritetext = "It's empty...";
            dialogBox.SetActive(true);
            writeText = StartCoroutine(WriteText());
        }
    }

    public override void RunPlayerNearby()
    {
        _playerNearby = true;
    }

    public override void RunPlayerLeft()
    {
        _playerNearby = false;
        dialogBox.SetActive(false);
        if (writeText != null) StopCoroutine(writeText);
    }
}
