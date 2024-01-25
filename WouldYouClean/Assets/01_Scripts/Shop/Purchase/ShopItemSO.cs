using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PurchaseItem
{
    inventoryUpgrade = 0,
    cleanerUpgrade = 1,
}
[CreateAssetMenu(menuName ="SO/ShopItem")]
public class ShopItemSO : ScriptableObject
{
    [Header("기본적인 아이템 정보")]
    public PurchaseItem Item;

    // 이름
    public string itemName;
    //설명
    [TextArea] public string itemInfo;
    // 아이콘
    public Sprite itemIcon;
    //가격
    public int itemPrice;

    public void IsOnSale(int discountPercentage) // 세일 같은거? 나중에 쓰
    {
        itemPrice = (int)(itemPrice + itemPrice * (discountPercentage / 100));
    }
}
