using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    /*
    private InventoryItem sellingItem;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemIcon;

    [SerializeField] private GameObject pointerHandler;

    private void Start()
    {
        pointerHandler.SetActive(false);
    }

    public void SetItem(InventoryItem item)
    {
        sellingItem = item;

        itemName.text = item.itemData._ObjectName;
        itemIcon.sprite = item.itemData.itemIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerHandler.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerHandler.SetActive(false);
    }
    */

    private InventoryItem sellingItem;
    private int itemCnt = 0;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemAmountText;
    [SerializeField] private Image itemImage;
    private Image dragImage;

    public void UpdateSlot(InventoryItem newItem)
    {
        sellingItem = newItem;

        if (sellingItem != null)
        {
            itemImage.sprite = sellingItem.itemData.itemIcon;
            itemImage.color = Vector4.one;
            if (itemAmountText == null) return;
            if (sellingItem.itemCnt > 1)
            {
                itemAmountText.text = $"{sellingItem.itemCnt}";
            }
            else
            {
                itemAmountText.text = string.Empty;
            }
        }
    }

    public void CleanUpSlot()
    {
        sellingItem = null;
        itemImage.color = new Color(1, 1, 1, 0);
        itemAmountText.text = string.Empty;
        itemName.text = string.Empty;
    }

    public void OnBeginDrag(PointerEventData eventData)
    { 
        dragImage = Instantiate(itemImage);
        dragImage.color = new Vector4(1, 1, 1, 0.6f);
        dragImage.transform.SetParent(transform.root, false);

        dragImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("드래그중");
        dragImage.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 끝
        Destroy(dragImage.gameObject);

        if (Shop.Instance.IsInTable()) // 테이블에 있을 경우
        {
            Shop.Instance.SetTable(sellingItem.itemData);
        }
        else
        {

        }
    }

    public void SetItem(InventoryItem item)
    {
        sellingItem = item;
        itemCnt = item.itemCnt;

        itemAmountText.text = $"{itemCnt}";
        itemName.text = item.itemData._ObjectName;
        itemImage.sprite = item.itemData.itemIcon;
    }
}
