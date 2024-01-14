using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    private InventoryItem sellingItem;
    private int itemCnt = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemAmountText;
    [SerializeField] private Image itemImage;
    private Image dragImage;

    #region �巡�� �̺�Ʈ
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragImage = Instantiate(itemImage);
        dragImage.color = new Vector4(1, 1, 1, 0.6f);
        dragImage.transform.SetParent(transform.root, false);

        dragImage.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //print("�巡����");
        dragImage.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�� ��
        Destroy(dragImage.gameObject);

        if (Shop.Instance.IsInTable()) // ���̺��� ���� ���
        {
            Shop.Instance.SetTable(sellingItem.itemData);
        }
        else
        {

        }
    }
    #endregion

    public void UpdateSlot(InventoryItem newItem)
    {
        sellingItem = newItem;

        if (sellingItem != null)
        {
            itemImage.sprite = sellingItem.itemData._ItemIcon;
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

    public void SetItem(InventoryItem item)
    {
        sellingItem = item;
        itemCnt = item.itemCnt;

        itemAmountText.text = $"{itemCnt}";
        itemName.text = item.itemData._ObjectName;
        itemImage.sprite = item.itemData._ItemIcon;
    }
}