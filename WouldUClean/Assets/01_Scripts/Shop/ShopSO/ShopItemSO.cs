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

    public void PayBackDebt()
    {
        if(Item != PurchaseItem.Debt) return;


    }
}
