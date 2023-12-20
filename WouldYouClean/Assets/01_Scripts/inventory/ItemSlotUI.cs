using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage; // ������ �̹���
    [SerializeField] private TextMeshProUGUI itemAmountText; // �ش� ������ �� �� �ֳ���
    public InventoryItem item;

    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;

        if (item != null)
        {
            itemImage.sprite = item.itemData.itemIcon;
            itemImage.color = Vector4.one;
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
        itemImage.color = new Color(1, 1, 1, 0);
        itemAmountText.text = string.Empty;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(item.itemData.itemName);
        if (item == null) return;
        if (!Keyboard.current.ctrlKey.isPressed) return; // ��Ʈ���̶� ���� ������
        Inventory.Instance.RemoveItem(item.itemData);
    }
}
