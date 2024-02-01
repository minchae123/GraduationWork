using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellingShopTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Main")]
    public ObjectType currentItem;
    [SerializeField] private int itemAmount;

    [Header("UI")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemAmountText;

    private bool isTable = false;
    public bool IsTable => isTable;

    private void Start()
    {
        ResetItem();
    }

    public void SetItem(ObjectType item)
    {
        if (currentItem != null) // 현재 테이블에 아이템 있을 경우
        {
            Shop.Instance.AddItem(currentItem, itemAmount);
            ResetItem();
        }

        currentItem = item;
        itemImage.sprite = item._ItemIcon; // 아이템 아이콘으로 해당 이미지 넣고..
        itemName.text = item._ObjectName; // 아이템 이름도 저장
        itemPrice.text = $"{item.itemPrice}원에 판매"; // 아이템 가격
        itemAmountText.text = $"{itemAmount}";
    }

    public void ResetItem()
    {
        currentItem = null;
        
        itemImage.sprite = null;
        
        itemName.text = "아이템을 드래그 해주세요";
        itemPrice.text = string.Empty;
        itemAmountText.text = $"{1}";
        
        itemAmount = 1;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isTable = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isTable = false;
    }

    public void OnClickSellItem()
    {
        Coin.Instance.AddCoin(currentItem.itemPrice * itemAmount);
        ResetItem();
    }

    public void IncreaseAmount()
    {
        if (Shop.Instance.shopDictionary.TryGetValue(currentItem, out InventoryItem i)) // shopdictionary에 있느면
        {
            Shop.Instance.RemoveItem(currentItem);
            itemAmount++;
        }
        else
        {
            print("없음");
        }

        UpdateText();
    }
    public void DecreaseAmount()
    {
        if (itemAmount <= 0) return;
        Shop.Instance.AddItem(currentItem);
        itemAmount--;

        UpdateText();
    }

    public void UpdateText()
    {
        itemPrice.text = $"{currentItem.itemPrice*itemAmount}원에 판매";
        itemAmountText.text = $"{itemAmount}";
    }
}
