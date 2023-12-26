using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage; // 아이템 이미지
    [SerializeField] private TextMeshProUGUI itemAmountText; // 해당 아이템 몇 개 있나요
    public InventoryItem item;

    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;

        if (item != null)
        {
            itemImage.sprite = item.itemData.itemIcon;
            if (itemAmountText == null) return;
            if (item.itemCnt > 1)
            {
                itemAmountText.text = $"{item.itemCnt}";
            }
            else
            {
                itemAmountText.text = string.Empty;
            }
        }
    }

    public void CleanUpSlot()
    {
        item = null;
        itemImage.sprite = null;
        if (itemAmountText == null) return;
        itemAmountText.text = string.Empty;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(item.itemData._ObjectName);
        if (item == null) return;
        if (!Keyboard.current.ctrlKey.isPressed) return; // 컨트롤이랑 같이 눌려야
        Inventory.Instance.RemoveItem(item.itemData);
    }
}
