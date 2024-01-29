using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseShopTable : MonoBehaviour
{
    public ShopItemSO buyItem; //

    [Header("UI")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemInfoText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    public void SetItem(ShopItemSO newItem)
    {
        ResetItem();
        buyItem = newItem;

        itemIcon.sprite = buyItem.itemIcon;
        itemInfoText.text = buyItem.itemInfo;
        itemPriceText.text = $"{buyItem.itemPrice}¿ø";
    }
    public void ResetItem()
    {
        itemIcon.sprite = null;
        itemInfoText.text = string.Empty;
        itemPriceText.text = string.Empty;
    }

    public void OnClickBuyButton()
    {
        Shop.Instance.BuyItem(buyItem);
    }
}
