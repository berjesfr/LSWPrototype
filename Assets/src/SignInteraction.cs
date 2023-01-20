using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignInteraction : Interaction
{
    public GameObject m_DialogBox;  
    public TextMeshProUGUI m_TextMeshPro;

    public string m_Text;
    public float m_TypingSpeed;

    private bool m_PlayerNearby;
    private Coroutine m_WriteText;

    public override void Setup()
    {
        m_TextMeshPro.text = string.Empty;
        m_DialogBox.SetActive(false);
    }

    public override void InteractionAction()
    {
        if (Input.GetKey(KeyCode.E) && m_PlayerNearby && !m_DialogBox.activeSelf) {
            interactionIndicator.SetActive(false);
            m_TextMeshPro.text = string.Empty;
            m_DialogBox.SetActive(true);
            m_WriteText = StartCoroutine(WriteText());
        }
        if (Input.GetKey(KeyCode.Escape) && m_DialogBox.activeSelf) {
            m_DialogBox.SetActive(false);
            if (m_WriteText != null) StopCoroutine(m_WriteText);

        }
    }

    public override void RunPlayerNearby()
    {
        m_PlayerNearby = true;
    }

    public override void RunPlayerLeft()
    {
            m_PlayerNearby = false;
            m_DialogBox.SetActive(false);
            if (m_WriteText != null) StopCoroutine(m_WriteText);
    }

    private IEnumerator WriteText() 
    {
        foreach (char character in m_Text.ToCharArray()) {
            m_TextMeshPro.text += character;
            yield return new WaitForSeconds(m_TypingSpeed);
        }
    }
}
