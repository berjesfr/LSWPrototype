using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggablePanel : EventTrigger
{
    
    private bool _dragging;
    private Vector2 _previousMouse;

    private void Update()
    {
        if (_dragging) {
            Vector2 currentMouse = Input.mousePosition;
            Vector2 mouseDelta = currentMouse - _previousMouse;
            transform.position = new Vector2(transform.position.x + mouseDelta.x, transform.position.y + mouseDelta.y);
        }
        _previousMouse = Input.mousePosition;
    }

    public override void OnPointerDown(PointerEventData data)
    {
        _dragging = true;
    }


    public override void OnPointerUp(PointerEventData data)
    {
        _dragging = false;
    }
}
