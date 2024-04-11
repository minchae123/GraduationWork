using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PurchaseSlotUI : MonoBehaviour//, IPointerClickHandler
{
    [Header("ITEM")]
    private ShopItemSO sellingItem;

    [Header("UI")]
    [SerializeField] private Button buyButton;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;

    public void SetItem(ShopItemSO item)
    {
        sellingItem = item;

        itemIcon.sprite = sellingItem.itemIcon;
        itemName.text = sellingItem.itemName;
        itemPrice.text = $"{sellingItem.itemPrice}¿ø";
    }

    public void OnClickPurchaseBtn()
    {
        PurchaseItem type = sellingItem.Item;
        if (type == PurchaseItem.NONE)
        {
            return;
        }

        if(type == PurchaseItem.Debt)
        {
            Shop.Instance.SetDebtPanel(sellingItem);
        }
        else if(Purchase.Instance.CheckCanBuy(type))
        {
            Shop.Instance.SetCheckPanel(sellingItem);
        }
    }

    public void ButtonInteractive(bool b)
    {
        buyButton.interactable = b;
    }
}
