using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PurchaseItem
{       
    NONE = 0,
    Debt = 1,
    Inventory = 2,
    O2Tank = 3,
    HP = 4,  
    UFO = 5,
    Item = 6,  
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

    public void PayBackDebt()
    {
        if(Item != PurchaseItem.Debt) return;


    }
}
