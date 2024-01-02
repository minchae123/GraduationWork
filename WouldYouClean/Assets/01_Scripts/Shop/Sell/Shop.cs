using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // 첫번째
    /*
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform shopParent;

    [SerializeField] private ShopSlotUI slotPrefab;

    public void OpenShop()
    {
        if (inventoryParent != null)
        {
            shopParent.gameObject.SetActive(true);
            foreach (Transform child in inventoryParent)
            {
                ItemSlotUI itemSlot = child.GetComponent<ItemSlotUI>();
                if (itemSlot != null)
                {
                    for(int i=0;i<itemSlot.item.itemCnt;++i)
                    {
                        ShopSlotUI slot = Instantiate(slotPrefab, shopParent);
                        slot.SetItem(itemSlot.item);
                    }
                }
            }
        }
    }
    */

    public static Shop Instance;

    public List<InventoryItem> mainShopItem; // 메인 인벤토리
    public Dictionary<ObjectType, InventoryItem> shopDictionary; // 인벤토리 딕셔너리

    private void Awake()
    {
        if(Instance!=null) { print("shop 에러"); }
        Instance = this;
    }

    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform shopParent;
    [SerializeField] private ShopTable shopTable;
    [SerializeField] private Transform shopSlotParent;

    [SerializeField] private ShopSlotUI slotPrefab;
    private ShopSlotUI[] itemSlots; // 슬롯 부모에서 가져올 slotUI들 (prefab)

    public bool IsInTable() => shopTable.IsTable;

    public void OpenSellingShop()
    {
        shopParent.gameObject.SetActive(true);

        if (inventoryParent != null)
        {
            shopSlotParent.gameObject.SetActive(true);
            foreach (Transform child in inventoryParent)
            {
                ItemSlotUI itemSlot = child.GetComponent<ItemSlotUI>();
                if (itemSlot != null)
                {
                    ShopSlotUI slot = Instantiate(slotPrefab, shopSlotParent);
                    slot.SetItem(itemSlot.item);

                    mainShopItem.Add(itemSlot.item);
                    shopDictionary.Add(itemSlot.item.itemData, itemSlot.item);
                }
            }
        }

        UpdateSlotUI();
    }

    public void SetTable(ObjectType item)
    {
        shopTable.SetItem(item);
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < itemSlots.Length; ++i)
        {
            itemSlots[i].CleanUpSlot(); // cleanUp
        }

        for (int i = 0; i < mainShopItem.Count; ++i)
        {
            itemSlots[i].UpdateSlot(mainShopItem[i]); // redraw
        }
    }

}
