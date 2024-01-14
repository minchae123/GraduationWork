using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    private int currentCoin = 0;

    public List<InventoryItem> mainShopItem; // ���� �κ��丮
    public Dictionary<ObjectType, InventoryItem> shopDictionary; // �κ��丮 ��ųʸ�

    private void Awake()
    {
        if(Instance!=null) { print("shop ����"); }
        Instance = this;
    }

    [Header("Slot")]
    [SerializeField] private ShopSlotUI slotPrefab;
    public ShopSlotUI[] shopSlots; // ���� �θ𿡼� ������ slotUI�� (prefab)
    private int currentInventoryCnt = 0;

    [Header("UI")]
    [SerializeField] private Transform shopParent;
    [SerializeField] private ShopTable shopTable;
    [SerializeField] private Transform shopSlotParent;

    [Header("���߿� �ٸ������� �Űܾ���")]
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
        if (shopDictionary.TryGetValue(item, out InventoryItem i)) // �ش� �������� inventory�� ���� ���
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
        coinText.text = $"{currentCoin}��";
    }
    public void AddCoin(int price)
    {
        currentCoin += price;
        UpdateCoinText();
    }
}
