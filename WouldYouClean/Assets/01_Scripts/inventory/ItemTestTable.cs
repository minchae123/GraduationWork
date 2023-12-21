using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemTestTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;

    public void SetItem(ItemDataSO item)
    {
        itemImage.sprite = item.itemIcon; // 아이템 아이콘으로 해당 이미지 넣고..
        itemName.text = item.itemName; // 아이템 이름도 저장
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
