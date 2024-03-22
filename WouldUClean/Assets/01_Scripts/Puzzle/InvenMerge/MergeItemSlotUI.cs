using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeItemSlotUI : ItemSlotUI, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /*[SerializeField] private Image dragImage; // 드래그 할 때 이미지

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;

        dragImage = Instantiate(itemImage);
        dragImage.color = new Vector4(1, 1, 1, 0.6f);
        dragImage.transform.SetParent(transform.root, false);

        dragImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0f; 
        dragImage.transform.position = pos;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;

        dragImage.raycastTarget = true;
        Destroy(dragImage.gameObject);
    }*/
}
