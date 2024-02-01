using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("SO/Shop/SellingItemList"))]
public class SellingItemList : ScriptableObject
{
    public List<ShopItemSO> itemList;
}
