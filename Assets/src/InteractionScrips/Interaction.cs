using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class Interaction: MonoBehaviour
{
    public GameObject interactionIndicator;
    public bool highlight;
    public bool dialog;
    public GameObject dialogBox;  
    public TextMeshProUGUI dialogText;
    public string toWritetext;
    public float typingSpeed;
    public Coroutine writeText;
    public AudioClip interactionSound;
    
    public AudioSource _source;
    private Color _originalColor;
    private SpriteRenderer _SpriteRenderer;

    private void Start()
    {
        dialogText.text = string.Empty;
        dialogBox.SetActive(false);
        Setup();
        interactionIndicator.SetActive(false);
        if (highlight) {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = interactionSound;
        _source.volume = 0.5f;
    }

    public void Update()
    {
        InteractionAction();
        if (Input.GetKey(KeyCode.Escape) && dialogBox.activeSelf) {
            dialogBox.SetActive(false);
            if (writeText != null) StopCoroutine(writeText);
        }
    }

    public virtual void Setup()
    {
        Debug.Log("Setup");
    }

    public virtual void InteractionAction()
    {
        _source.Play();
    }

    public virtual void RunPlayerNearby()
    {
        Debug.Log("Run Player Nearby");
    }

    public virtual void RunPlayerLeft()
    {
        Debug.Log("Run Player Left");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            RunPlayerNearby();
            interactionIndicator.SetActive(true);
            if (highlight) {
                _originalColor = _SpriteRenderer.material.GetColor("_Color");
                Color newColor = new Color(_originalColor.r + 0.5f, _originalColor.g + 0.5f, _originalColor.b + 0.5f, _originalColor.a);
                _SpriteRenderer.material.SetColor("_Color", newColor);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            RunPlayerLeft();
            interactionIndicator.SetActive(false);

            if (highlight) {
                _SpriteRenderer.material.SetColor("_Color", _originalColor);
            }
        }
    }

    public IEnumerator WriteText() 
    {
        foreach (char character in toWritetext.ToCharArray()) {
            dialogText.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
