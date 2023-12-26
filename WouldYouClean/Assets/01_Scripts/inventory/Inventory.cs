using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> mainInventory; // ���� �κ��丮
    public Dictionary<ObjectType, InventoryItem> invenDictionary; // �κ��丮 ��ųʸ�

    [Header("Inventory UI")]
    [SerializeField] private Transform invenSlotParent; // ���� �θ�
    private ItemSlotUI[] itemSlots; // ���� �θ𿡼� ������ slotUI�� (prefab)

    private void Awake()
    {
        if(Instance!=null) { Debug.LogError("Inventory Error"); }
        Instance = this;
        
        mainInventory = new List<InventoryItem>();
        invenDictionary = new Dictionary<ObjectType, InventoryItem>();
        itemSlots = invenSlotParent.GetComponentsInChildren<ItemSlotUI>();
    }

    private void Start()
    {
        UpdateSlotUI();
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < itemSlots.Length; ++i) 
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
        for (int i = 0; i < itemSlots.Length; ++i)
        {
            itemSlots[i].CleanUpSlot(); // cleanUp
        }
    }

    public void AddItem(ObjectType item)
    {
        if(invenDictionary.TryGetValue(item,out InventoryItem i)) // �ش� �������� inventory�� ���� ���
        {
            i.AddItemCnt();
        }
        else // ������
        {
            // ���� ���
            InventoryItem newItem = new InventoryItem(item);
            mainInventory.Add(newItem);
            invenDictionary.Add(item, newItem);
        }

        UpdateSlotUI();
    }


    public void RemoveItem(ObjectType item, int cnt = 1)
    {
        print("dd");
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
}
