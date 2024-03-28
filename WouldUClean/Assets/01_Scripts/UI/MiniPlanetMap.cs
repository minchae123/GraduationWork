using UnityEngine;
using UnityEngine.EventSystems;

public class MiniPlanetMap : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private RectTransform mapInfo;
    private Vector2 offset = Vector2.zero;

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mapInfo, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mapInfo.parent as RectTransform, eventData.position, 
            eventData.pressEventCamera, out localPos))
        {
            mapInfo.localPosition = localPos - offset;
        }
    }
}
