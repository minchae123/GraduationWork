using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlotUI : MonoBehaviour, IPointerClickHandler// IBeginDragHandler, IDragHandler,IEndDragHandler, 
{
    public InventoryItem sellingItem;
    private int itemCnt = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemAmountText;
    [SerializeField] private Image itemImage;
    private Image dragImage;

    /*#region 드래그 이벤트
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragImage = Instantiate(itemImage);
        dragImage.color = new Vector4(1, 1, 1, 0.6f);
        dragImage.transform.SetParent(transform.root, false);

        dragImage.GetComponent<Image>().enabled = true;
        dragImage.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //print("드래그중");
        Vector3 dragPos = GameManager.Instance.mainCam.ScreenToWorldPoint(eventData.position);
        dragImage.transform.position = new Vector3(dragPos.x, dragPos.y, 0);
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
    #endregion*/

    public void UpdateSlot(InventoryItem newItem)
    {
        sellingItem = newItem;

        if (sellingItem != null)
        {
            itemImage.sprite = sellingItem.itemData._ItemSprite;
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
        itemImage.sprite = item.itemData._ItemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sellingItem == null || sellingItem.itemData == null) { print("nothing to sell"); return; }
        Shop.Instance.SetTable(sellingItem.itemData); // 클릭
    }
}
