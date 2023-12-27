using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image dragImage; // �巡�� �� �� �̹���
    [SerializeField] private Image itemImage; // ������ �̹���
    [SerializeField] private TextMeshProUGUI itemAmountText; // �ش� ������ �� �� �ֳ���
    
    public InventoryItem item;
    private InventoryItem oldItem;

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

    // �巡�� �� ���
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("start");
        oldItem = item;
        
        dragImage = Instantiate(itemImage);
        dragImage.color = new Vector4(1, 1, 1, 0.6f);
        dragImage.transform.SetParent(transform.root, false);

        dragImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragImage.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragImage.raycastTarget = true;
        Inventory.Instance.RemoveItem(item.itemData); // ������ ����
        Destroy(dragImage.gameObject);
        if (ItemManager.Instance.CheckInTable())
        {
            print("Table"); // ���̺� ����
            ItemManager.Instance.SetItemTable(oldItem.itemData);
        }
        else
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            CreateItem(worldPos);
        }
    }

    public void CreateItem(Vector2 pos)
    {
        ItemManager.Instance.CreateItem(oldItem.itemData, pos);
    }
}
