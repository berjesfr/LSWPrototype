using UnityEngine;

public class Interaction: MonoBehaviour
{
    public GameObject interactionIndicator;
    private Color _originalColor;
    private SpriteRenderer _SpriteRenderer;
    public bool highlight;
    
    private void Start()
    {
        Setup();
        interactionIndicator.SetActive(false);
        if (highlight) {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void Update()
    {
        InteractionAction();
    }

    public virtual void Setup()
    {
        Debug.Log("Setup");
    }

    public virtual void InteractionAction()
    {
        Debug.Log("Interaction");
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
            _originalColor = _SpriteRenderer.material.GetColor("_Color");
            Color newColor = new Color(_originalColor.r + 0.5f, _originalColor.g + 0.5f, _originalColor.b + 0.5f, _originalColor.a);
            _SpriteRenderer.material.SetColor("_Color", newColor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            RunPlayerLeft();
            interactionIndicator.SetActive(false);
            _SpriteRenderer.material.SetColor("_Color", _originalColor);
        }
    }
}
