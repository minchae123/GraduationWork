using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ObjectType currentItem;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;

    private bool isTable = false;
    public bool IsTable => isTable;

    public void SetItem(ObjectType item)
    {
        if (currentItem != null) // 현재 테이블에 아이템 있을 경우
        {

        }

        currentItem = item;
        itemImage.sprite = item.itemIcon; // 아이템 아이콘으로 해당 이미지 넣고..
        itemName.text = item._ObjectName; // 아이템 이름도 저장
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isTable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isTable = false;
    }
}
