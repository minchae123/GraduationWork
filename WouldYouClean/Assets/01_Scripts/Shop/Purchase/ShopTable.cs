using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Main")]
    public ObjectType currentItem;

    [Header("UI")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;

    private bool isTable = false;
    public bool IsTable => isTable;

    public void SetItem(ObjectType item)
    {
        if (currentItem != null) // 현재 테이블에 아이템 있을 경우
        {
            Shop.Instance.AddItem(currentItem);
        }

        currentItem = item;
        itemImage.sprite = item._ItemIcon; // 아이템 아이콘으로 해당 이미지 넣고..
        itemName.text = item._ObjectName; // 아이템 이름도 저장
        itemPrice.text = $"{item.itemPrice}원에 판매"; // 아이템 가격
    }

    public void ResetItem()
    {
        currentItem = null;
        itemImage.sprite = null;
        itemName.text = string.Empty;
        itemPrice.text = string.Empty;
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
        Coin.Instance.AddCoin(currentItem.itemPrice);
        ResetItem();
    }
}
