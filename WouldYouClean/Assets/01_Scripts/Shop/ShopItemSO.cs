using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemPrice;

    public void IsOnSale(int discountPercentage) // ���� ������? ���߿� ��
    {
        itemPrice = (int)(itemPrice + itemPrice * (discountPercentage / 100));
    }
}
