using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool m_Hold;
    public bool IsHold => m_Hold;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
    }
}
