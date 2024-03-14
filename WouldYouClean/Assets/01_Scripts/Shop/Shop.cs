using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoSingleton<Shop>
{
    [Header("==== Core ====")]
    public List<InventoryItem> mainShopItem; // ���� �κ��丮
    public Dictionary<ObjectType, InventoryItem> shopDictionary; // �κ��丮 ��ųʸ�

    [Header("==== Main ShopSystem ====")]
    [SerializeField] private Transform mainShopTrm;



    [Header("==== SELLING SHOP ====")]

    [Header("Slot")]
    [SerializeField] private ShopSlotUI slotPrefab;
    public ShopSlotUI[] shopSlots; // ���� �θ𿡼� ������ slotUI�� (prefab)
    private int currentInventoryCnt = 0;

    [Header("UI")]
    [SerializeField] private Transform sellingShop;
    [SerializeField] private Transform sellingParent;
    [SerializeField] private SellingShopTable sellingItemTable;



    [Header("==== PURCHASE SHOP ====")]
    [SerializeField] private SellingItemList sellingItemList;

    [Header("Slot")]
    [SerializeField] private PurchaseSlotUI purchaseSlot;
    
    [Header("UI")]
    [SerializeField] private Transform purchaseShop;
    [SerializeField] private Transform purchaseParent;
    [SerializeField] private PurchaseShopTable purchaseItemTable;


    private void Start()
    {
        mainShopTrm.gameObject.SetActive(false);

        shopSlots = sellingParent.GetComponentsInChildren<ShopSlotUI>();
        mainShopItem = new List<InventoryItem>();
        shopDictionary = new Dictionary<ObjectType, InventoryItem>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            EnterShop();
        }
    }

    #region ��ư Ŭ��
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

        OnClickSellingShopBtn(); // �⺻�� �Ǹŷ�
        UpdateSlotUI();
    }
    public void ExitShop()
    {
        print("CLick");
        mainShopTrm.gameObject.SetActive(false);

        Inventory.Instance.SetShopItem(mainShopItem, shopDictionary);

        mainShopItem.Clear();
        shopDictionary.Clear();
        currentInventoryCnt = 0;
        
        Time.timeScale = 1;
    }

    public void OnClickPurchaseShopBtn() // ���� Ŭ��
    {
        sellingShop.gameObject.SetActive(false); // �ǸŴ� ����
        purchaseShop.gameObject.SetActive(true);// ���Ŵ� Ŵ
        //print("Purchase");
        DestoryItemInShop();
        SetItemInShop();
    }
    public void OnClickSellingShopBtn() // �Ǹ�
    {
        sellingShop.gameObject.SetActive(true); // ���� �ݴ�
        purchaseShop.gameObject.SetActive(false);//
        //print("Sell");
        DestoryItemInShop();
    }
    #endregion


    #region �Ĵ� ��
    public bool IsInTable() => sellingItemTable.IsTable;

    public void SetTable(ObjectType item)
    {
        sellingItemTable.SetItem(item);
        UpdateSlotUI();
    }
    public void AddItem(ObjectType item, int cnt = 1)
    {
        if (shopDictionary.TryGetValue(item, out InventoryItem i)) // �ش� �������� inventory�� ���� ���
        {
            i.AddItemCnt(cnt);
        }
        else 
        {
            InventoryItem newItem = new InventoryItem(item);
            
            shopSlots[++currentInventoryCnt].SetItem(newItem);

            mainShopItem.Add(newItem);
            shopDictionary.Add(item, newItem);

            newItem.AddItemCnt(cnt - 1);
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

    #region ��� ��
    public void SetItemInShop()
    {
        for(int i=0;i< sellingItemList.itemList.Count;++i)
        {
            PurchaseSlotUI obj = Instantiate(purchaseSlot, purchaseParent);
            obj.sellingItem = sellingItemList.itemList[i];
            obj.SetItem();
        }
    }
    public void DestoryItemInShop()
    {
        foreach (Transform t in purchaseParent)
        {
            Destroy(t.gameObject);
        }
    }

    public void SetItem(ShopItemSO item)
    {
        purchaseItemTable.SetItem(item);
    }

    public void BuyItem(ShopItemSO item)
    {
        int price = item.itemPrice;
        switch (item.Item)
        {
            case PurchaseItem.NONE:
                break;
            case PurchaseItem.inventoryUpgrade:
                UpgradeInventory(price); print("1");
                break;
            case PurchaseItem.cleanerUpgrade:
                UpgradeCleaner(price);
                break;
            case PurchaseItem.o2Tank:
                UpgradeO2Tank(price);
                break;
            case PurchaseItem.item:
                break;
        }
    }

    public void UpgradeInventory(int price)
    {
        Inventory.Instance.UpgradeInventory();
        Coin.Instance.RemoveCoin(price);
    }

    public void UpgradeCleaner(int price)
    {
        print("û�ұ� ���׷��̵�");
        Coin.Instance.RemoveCoin(price);
    }

    public void UpgradeO2Tank(int price)
    {
        print("��������");
        Coin.Instance.RemoveCoin(price);
    }
    #endregion

}
