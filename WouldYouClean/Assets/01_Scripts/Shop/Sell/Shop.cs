using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoSingleton<Shop>
{
    [Header("==== Core ====")]
    public List<InventoryItem> mainShopItem; // 메인 인벤토리
    public Dictionary<ObjectType, InventoryItem> shopDictionary; // 인벤토리 딕셔너리

    [Header("==== Main ShopSystem ====")]
    [SerializeField] private Transform mainShopTrm;



    [Header("==== SELLING SHOP ====")]

    [Header("Slot")]
    [SerializeField] private ShopSlotUI slotPrefab;
    public ShopSlotUI[] shopSlots; // 슬롯 부모에서 가져올 slotUI들 (prefab)
    private int currentInventoryCnt = 0;

    [Header("UI")]
    [SerializeField] private Transform sellingShop;
    [SerializeField] private ShopTable shopTable;
    [SerializeField] private Transform shopSlotParent;



    [Header("==== PURCHASE SHOP ====")]
    [SerializeField] private Transform purchaseShop;
    [SerializeField] private PurchaseShopTable itemTable;



    //[Header("==== 나중에 다른쪽으로 옮겨야함 ====")]

    private void Start()
    {
        mainShopTrm.gameObject.SetActive(false);

        shopSlots = shopSlotParent.GetComponentsInChildren<ShopSlotUI>();
        mainShopItem = new List<InventoryItem>();
        shopDictionary = new Dictionary<ObjectType, InventoryItem>();
    }

    #region 버튼 클릭
    public void EnterShop()
    {
        mainShopTrm.gameObject.SetActive(true);

        foreach(var i in Inventory.Instance.ReturnInvenList())
        {
            mainShopItem.Add(i);
        }
        foreach (var i in Inventory.Instance.ReturnInvenDictionary())
        {
            shopDictionary.Add(i.Key, i.Value);
        }

        currentInventoryCnt = mainShopItem.Count;

        Time.timeScale = 0;

        OnClickSellingShopBtn(); // 기본은 판매로
        UpdateSlotUI();
    }
    public void ExitShop()
    {
        mainShopTrm.gameObject.SetActive(false);

        Inventory.Instance.SetShopItem(mainShopItem, shopDictionary);

        mainShopItem.Clear();
        shopDictionary.Clear();
        currentInventoryCnt = 0;
        
        Time.timeScale = 1;
    }

    public void OnClickPurchaseShopBtn() // 구매 클릭
    {
        sellingShop.gameObject.SetActive(false); // 판매는 끄고
        purchaseShop.gameObject.SetActive(true);// 구매는 킴
    }
    public void OnClickSellingShopBtn() // 판매
    {
        sellingShop.gameObject.SetActive(true); // 위랑 반대
        purchaseShop.gameObject.SetActive(false);//
    }
    #endregion

    #region 파는 곳
    public bool IsInTable() => shopTable.IsTable;
    public void SetTable(ObjectType item)
    {
        shopTable.SetItem(item);
        RemoveItem(item);
        UpdateSlotUI();
    }
    public void AddItem(ObjectType item)
    {
        if (shopDictionary.TryGetValue(item, out InventoryItem i)) // 해당 아이템이 inventory에 있을 경우
        {
            i.AddItemCnt();
        }
        else 
        {
            InventoryItem newItem = new InventoryItem(item);
            
            shopSlots[++currentInventoryCnt].SetItem(newItem);

            mainShopItem.Add(newItem);
            shopDictionary.Add(item, newItem);
        }

        UpdateSlotUI();
    }
    public void RemoveItem(ObjectType item, int cnt = 1)
    {
        if (shopDictionary.TryGetValue(item, out InventoryItem i))
        {
            if (i.itemCnt <= cnt)
            {
                shopDictionary.Remove(item);
                mainShopItem.Remove(i);
            }
            else
            {
                i.RemoveItemCnt(cnt);
            }
        }

        UpdateSlotUI();
    }
    public void UpdateSlotUI()
    {
        for (int i = 0; i < shopSlots.Length; ++i)
        {
            shopSlots[i].CleanUpSlot(); // cleanUp
        }

        for (int i = 0; i < mainShopItem.Count; ++i)
        {
            shopSlots[i].UpdateSlot(mainShopItem[i]); // redraw
        }
    }
    #endregion

    #region 사는 곳
    public void SetItem(ShopItemSO item)
    {
        itemTable.SetItem(item);
    }

    public void BuyItem(ShopItemSO item)
    {
        int price = item.itemPrice;
        switch (item.Item)
        {
            case PurchaseItem.inventoryUpgrade:
                UpgradeInventory(price); print("1");
                break;
            case PurchaseItem.cleanerUpgrade:
                UpgradeCleaner(price);
                break;
        }
    }

    public void UpgradeInventory(int price)
    {
        print("2");
        Inventory.Instance.UpgradeInventory();
        Coin.Instance.RemoveCoin(price);
    }

    public void UpgradeCleaner(int price)
    {
        print("청소기 업그레이드");
        Coin.Instance.RemoveCoin(price);
    }
    #endregion

}
