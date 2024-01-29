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
    [Header("�⺻���� ������ ����")]
    public PurchaseItem Item;

    // �̸�
    public string itemName;
    //����
    [TextArea] public string itemInfo;
    // ������
    public Sprite itemIcon;
    //����
    public int itemPrice;

    public void IsOnSale(int discountPercentage) // ���� ������? ���߿� ��
    {
        itemPrice = (int)(itemPrice + itemPrice * (discountPercentage / 100));
    }
}
