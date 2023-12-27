using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemTestTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ObjectType currentItem;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;

    public void SetItem(ObjectType item)
    {
        if(currentItem != null) // 현재 테이블에 아이템 있을 경우
        {
            Inventory.Instance.AddItem(currentItem, true); // 갱신 전에 다시 인벤토리로 넣얻주기
        }

        currentItem = item;
        itemImage.sprite = item.itemIcon; // 아이템 아이콘으로 해당 이미지 넣고..
        itemName.text = item._ObjectName; // 아이템 이름도 저장
    }

    public void RemoveItem() // 초기화
    {
        itemImage.sprite = null;
        itemName.text = string.Empty;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemManager.Instance.CheckInTable(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemManager.Instance.CheckInTable(false);
    }
}
