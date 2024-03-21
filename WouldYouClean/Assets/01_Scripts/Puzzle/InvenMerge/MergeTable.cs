using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public ObjectType itemData;

    private Image colorImage;
    [SerializeField] private Image childImage;

    private void Start()
    {
        colorImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        colorImage.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        colorImage.color = Color.white;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<MergeItemSlotUI>(out MergeItemSlotUI m))
        {
            if(itemData !=null)//이미 아이템이 해당 테이블에 있을 경우
            {
                //MergeTrash.Instance
                print("이미 있음");
                MergeTrash.Instance.AddItem(itemData);
            }
            childImage.sprite = m.itemImage.sprite;
            itemData = m.item.itemData;
            MergeTrash.Instance.UseItem(itemData);
        }
    }
}
