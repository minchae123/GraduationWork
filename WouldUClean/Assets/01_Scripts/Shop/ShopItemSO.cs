using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PurchaseItem
{   
    NONE = 0,
    Debt = 1,
    Inventory = 2,
    O2Tank = 3,
    item = 4,  
}
[CreateAssetMenu(menuName ="SO/Shop/ShopItem")]
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
