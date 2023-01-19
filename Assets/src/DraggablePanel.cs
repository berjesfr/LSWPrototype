using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggablePanel : EventTrigger
{
    
    private bool m_Dragging;
    private Vector2 m_PreviousMouse;

    private void Update()
    {
        if (m_Dragging) {
            Vector2 currentMouse = Input.mousePosition;
            Vector2 mouseDelta = currentMouse - m_PreviousMouse;
            transform.position = new Vector2(transform.position.x + mouseDelta.x, transform.position.y + mouseDelta.y);
        }
        m_PreviousMouse = Input.mousePosition;
    }

    public override void OnPointerDown(PointerEventData data)
    {
        m_Dragging = true;
    }


    public override void OnPointerUp(PointerEventData data)
    {
        m_Dragging = false;
    }
}
