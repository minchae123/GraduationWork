using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ObjectType itemData;
    public int itemCnt; // æ∆¿Ã≈€ cnt

    public InventoryItem(ObjectType item)
    {
        this.itemData = item;
        AddItemCnt();
    }

    public void AddItemCnt()
    {
        ++itemCnt;
    }

    public void RemoveItemCnt(int cnt = 1)
    {
        itemCnt -= cnt;
    }
}
