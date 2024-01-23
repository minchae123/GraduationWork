using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory Instance;

    public List<InventoryItem> mainInventory; // ���� �κ��丮
    public Dictionary<ObjectType, InventoryItem> invenDictionary; // �κ��丮 ��ųʸ�

    [Header("Inventory UI")]
    [SerializeField] private Transform invenSlotParent; // ���� �θ�
    [SerializeField] private ItemSlotUI slotPrefab;

    private int inventoryLength = 3; // �ʱ� �κ��丮
    private int maxInventoryLength = 10; // �ִ� �κ��丮
    private ItemSlotUI[] itemSlots; // ���� �θ𿡼� ������ slotUI�� (prefab)

    [Header("ItemUI")]
    [SerializeField] private Transform checker;
    [SerializeField] private PopUpItem popUpPanel;

    private NameToObjectType NameToObjectType;

    private void Awake()
    {
        if(Instance!=null) { Debug.LogError("Inventory Error"); }
        Instance = this;
        
        NameToObjectType = FindObjectOfType<NameToObjectType>();
        mainInventory = new List<InventoryItem>();
        invenDictionary = new Dictionary<ObjectType, InventoryItem>();
        itemSlots = new ItemSlotUI[maxInventoryLength];
    }
    private void Start()
    {
        for(int i = 0; i < inventoryLength; ++i)
        {
            ItemSlotUI slot = Instantiate(slotPrefab, invenSlotParent);
            itemSlots[i] = slot;
        }

        UpdateSlotUI();
    }

    public List<InventoryItem> ReturnInvenList() => mainInventory;
    public Dictionary<ObjectType, InventoryItem> ReturnInvenDictionary() => invenDictionary;

    public bool CheckInventoryIdx(ObjectType item)
    {
        if (!invenDictionary.TryGetValue(item, out InventoryItem i) // �ش� �������� �κ��丮�� ����
            && inventoryLength <= invenDictionary.Count)
        { 
            print("idx NOT OK");
            return false;
        }
        return true;
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < inventoryLength; ++i) 
        {
            itemSlots[i].CleanUpSlot(); // cleanUp
        }

        for(int i = 0; i < mainInventory.Count; ++i)
        {
            itemSlots[i].UpdateSlot(mainInventory[i]); // redraw
        }
    }
    public void ClearItem()
    {
        for (int i = 0; i < inventoryLength; ++i)
        {
            itemSlots[i].CleanUpSlot(); // cleanUp
        }
    }

    public void ResetInvenItem()
    {
        mainInventory.Clear();
        invenDictionary.Clear();
    }

    public void SetShopItem(List<InventoryItem> list, Dictionary<ObjectType, InventoryItem> dic)
    {
        mainInventory.Clear();
        invenDictionary.Clear();

        foreach(var i in list)
        {
            mainInventory.Add(i);
        }
        foreach(var i in dic)
        {
            invenDictionary.Add(i.Key, i.Value);
        }
        
        UpdateSlotUI();
    }

    public void AddItem(ObjectType item, bool isTable)
    { 
        if(invenDictionary.TryGetValue(item, out InventoryItem i)) // �ش� �������� inventory�� ���� ���
        {
            i.AddItemCnt();
        }
        else // ������
        {
            if(inventoryLength <= invenDictionary.Count) // üũ
            {
                print("�κ��丮 ����! ���׷��̵� �ʿ�!");
                return;
            }
            // ���� ���
            InventoryItem newItem = new InventoryItem(item);
            mainInventory.Add(newItem);
            invenDictionary.Add(item, newItem);
        }

        if(!isTable) // ���̺� ���ؼ� ���� �� �ƴ� ���
        {
            PopUpItem popUp = Instantiate(popUpPanel, checker);
            popUp.SetItemPanel(item._ObjectName, item._ItemIcon);
        }
        
        UpdateSlotUI();
    }

    public void RemoveItem(ObjectType item, int cnt = 1)
    {
        if (invenDictionary.TryGetValue(item, out InventoryItem i)) 
        {
            if(i.itemCnt <= cnt) // �����մ� ���� ������ �ϴ� �纸�� �۰ų� ���� ���?
            {
                // �ƿ� ����
                invenDictionary.Remove(item);
                mainInventory.Remove(i);
            }
            else // �ش� �������� �� ���� ���
            {
                i.RemoveItemCnt(cnt); // cnt��ŭ ���ֱ�
            }
        }

        UpdateSlotUI();
    }

    public void UpgradeInventory(int upgradeLength = 1)
    {
        if (inventoryLength + upgradeLength > maxInventoryLength) 
        {
            print("maxInventory �ʰ�");
            return;
        }

        for (int i = 0; i < upgradeLength; ++i)
        {
            ItemSlotUI newSlot = Instantiate(slotPrefab, invenSlotParent);
            // 0, 1, 2 ������� (3->5)
            // 2���� ���� (3, 4 �� �־����)
            itemSlots[inventoryLength++] = newSlot;
        }
        UpdateSlotUI(); // ����
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpgradeInventory();
        }
    }

	public void LoadData(GameData data)
	{
        print("load start");
        print(data.inventory.items.Count);

        if(data.inventory.items.Count > 0)
		{
            for (int i = 0; i < data.inventory.items.Count; i++)
            {
                print(data.inventory.items[i].count);
                for (int j = 0; j < data.inventory.items[i].count; j++)
                {
                    string itemName = data.inventory.items[i].name;
                    ObjectType type = NameToObjectType.FindType(itemName);
                    print(type);
                    AddItem(type, true);
                }
            }
        }
	}

	public void SaveData(ref GameData data) // �κ��丮 �ȿ� �̸��̶� ���� �����ϱ�
	{
        DInventory dInven = new DInventory();

        for (int i = 0; i < mainInventory.Count; i++)
		{
            string name = mainInventory[i].itemData._ObjectName;
            int count = mainInventory[i].itemCnt;

            DInventoryItem invenData = new DInventoryItem(name, count);

            dInven.items.Add(invenData);
        }

        data.inventory = dInven;
	}
}
