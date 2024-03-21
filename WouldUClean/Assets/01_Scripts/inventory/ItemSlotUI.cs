using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, 
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IDropHandler
{
    [SerializeField] public Image itemImage; // 아이템 이미지
    [SerializeField] public TextMeshProUGUI itemAmountText; // 해당 아이템 몇 개 있나요
    [SerializeField] private Image dragImage;

    public InventoryItem item;
    public bool _isDragging = false;

    #region 기본
    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;

        if (item != null)
        {
            itemImage.sprite = item.itemData._ItemSprite;
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
    #endregion

    /*
        // 드래그 앤 드랍
        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;

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
            _isDragging = false;

            dragImage.raycastTarget = true;
            Inventory.Instance.RemoveItem(item.itemData); // 끝나면 삭제
            Destroy(dragImage.gameObject);

            if (ItemManager.Instance.CheckInTable())
            {
                print("Table"); // 테이블 세팅
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
        }*/

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null) return; // null 이면 드래그 되지도 않도록
        _isDragging = true;

        dragImage = Instantiate(itemImage);
        dragImage.color = new Vector4(1, 1, 1, 0.6f);
        dragImage.transform.SetParent(transform.root, false);

        dragImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0f;
        dragImage.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;

        dragImage.raycastTarget = true;
        Destroy(dragImage.gameObject);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<ItemSlotUI>(out ItemSlotUI m))
        {
            InventoryItem temp = m.item;
            if (temp == null) return;
            m.item = item;
            item = temp;

            print(temp);
            print(m.item);
            print(item);
            Inventory.Instance.UpdateSlotUI();
        }
    }
}
