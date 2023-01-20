using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignInteraction : Interaction
{
    private bool _playerNearby;

    public override void Setup()
    {}
    
    public override void InteractionAction()
    {
        if (Input.GetKey(KeyCode.E) && _playerNearby && !dialogBox.activeSelf) {
            interactionIndicator.SetActive(false);
            dialogText.text = string.Empty;
            dialogBox.SetActive(true);
            writeText = StartCoroutine(WriteText());
            _source.Play();
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
