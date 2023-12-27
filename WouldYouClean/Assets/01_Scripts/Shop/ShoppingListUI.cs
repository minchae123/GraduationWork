using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShoppingListUI : MonoBehaviour
{
    [SerializeField] private ShopItemSO currentItem; // ���� �ش� UI�� �餷�ä��Ӥ��� ������ 
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;

    private void Start()
    {
        SetItem(currentItem);
    }

    public void SetItem(ShopItemSO item)
    {
        currentItem = item;
        itemIcon.sprite = item.itemIcon;
        itemPrice.text = $"coin x {item.itemPrice}";
        itemName.text = $"{item.itemName}";
    }

    public void ClickPurchaseBtn()
    {
        if (ShoppingManager.Instance.CheckCurrentCoin() >= currentItem.itemPrice)
        {
            ShoppingManager.Instance.RemoveCoin(currentItem.itemPrice);
        }
        else
        {
            print("������");
            ShoppingManager.Instance.SetPopUpText("������ �����մϴ�.");
        }
    }
}
