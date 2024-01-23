using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/ShopItem")]
public class ShopItemSO : ScriptableObject
{
    [Header("기본적인 아이템 정보")]
    public string itemName;
    [TextArea] public string itemInfo;
    public Sprite itemIcon;
    public int itemPrice;

    public void IsOnSale(int discountPercentage) // 세일 같은거? 나중에 쓰
    {
        itemPrice = (int)(itemPrice + itemPrice * (discountPercentage / 100));
    }
}
