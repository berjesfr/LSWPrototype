using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignInteraction : MonoBehaviour
{
    public GameObject m_DialogBox;  
    public TextMeshProUGUI m_TextMeshPro;

    public string m_Text;
    public float m_TypingSpeed;

    private bool m_PlayerNearby;
    private Coroutine m_WriteText;
    private Color m_OriginalColor;
    private SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        m_TextMeshPro.text = string.Empty;
        m_DialogBox.SetActive(false);
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && m_PlayerNearby && !m_DialogBox.activeSelf) {
            m_TextMeshPro.text = string.Empty;
            m_DialogBox.SetActive(true);
            m_WriteText = StartCoroutine(WriteText());
        }
        if (Input.GetKey(KeyCode.Escape) && m_DialogBox.activeSelf) {
            m_DialogBox.SetActive(false);
            if (m_WriteText != null) StopCoroutine(m_WriteText);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            m_PlayerNearby = true;
            m_OriginalColor = m_SpriteRenderer.material.GetColor("_Color");
            Color newColor = new Color(m_OriginalColor.r + 0.5f, m_OriginalColor.g + 0.5f, m_OriginalColor.b + 0.5f, m_OriginalColor.a);
            m_SpriteRenderer.material.SetColor("_Color", newColor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            m_PlayerNearby = false;
            m_DialogBox.SetActive(false);
            if (m_WriteText != null) StopCoroutine(m_WriteText);
            m_SpriteRenderer.material.SetColor("_Color", m_OriginalColor);
        }
    }

    private IEnumerator WriteText() 
    {
        foreach (char character in m_Text.ToCharArray()) {
            m_TextMeshPro.text += character;
            yield return new WaitForSeconds(m_TypingSpeed);
        }
    }
}
