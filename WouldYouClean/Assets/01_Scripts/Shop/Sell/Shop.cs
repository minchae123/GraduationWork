using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    private int currentCoin = 0;

    public List<InventoryItem> mainShopItem; // 메인 인벤토리
    public Dictionary<ObjectType, InventoryItem> shopDictionary; // 인벤토리 딕셔너리

    private void Awake()
    {
        if(Instance!=null) { print("shop 에러"); }
        Instance = this;
    }

    [Header("Slot")]
    [SerializeField] private ShopSlotUI slotPrefab;
    public ShopSlotUI[] shopSlots; // 슬롯 부모에서 가져올 slotUI들 (prefab)
    private int currentInventoryCnt = 0;

    [Header("UI")]
    [SerializeField] private Transform shopParent;
    [SerializeField] private ShopTable shopTable;
    [SerializeField] private Transform shopSlotParent;

    [Header("나중에 다른쪽으로 옮겨야함")]
    [SerializeField] private TextMeshProUGUI coinText;

    public bool IsInTable() => shopTable.IsTable;

    private void Start()
    {
        shopParent.gameObject.SetActive(false);

        UpdateCoinText();
        shopSlots = shopSlotParent.GetComponentsInChildren<ShopSlotUI>();
        mainShopItem = new List<InventoryItem>();
        shopDictionary = new Dictionary<ObjectType, InventoryItem>();
    }
    public void EnterShop()
    {
        shopParent.gameObject.SetActive(true);

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
        UpdateSlotUI();
    }
    public void ExitShop()
    {
        shopParent.gameObject.SetActive(false);

        Inventory.Instance.SetShopItem(mainShopItem, shopDictionary);

        mainShopItem.Clear();
        shopDictionary.Clear();
        currentInventoryCnt = 0;
        
        Time.timeScale = 1;
    }
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

    public void UpdateCoinText()
    {
        coinText.text = $"{currentCoin}원";
    }
    public void AddCoin(int price)
    {
        currentCoin += price;
        UpdateCoinText();
    }
}
