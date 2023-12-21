using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> mainInventory; // ���� �κ��丮
    public Dictionary<ItemDataSO, InventoryItem> invenDictionary; // �κ��丮 ��ųʸ�

    [Header("Inventory UI")]
    [SerializeField] private Transform invenSlotParent; // ���� �θ�
    private ItemSlotUI[] itemSlots; // ���� �θ𿡼� ������ slotUI�� (prefab)

    private void Awake()
    {
        if(Instance!=null) { Debug.LogError("Inventory Error"); }
        Instance = this;
        
        mainInventory = new List<InventoryItem>();
        invenDictionary = new Dictionary<ItemDataSO, InventoryItem>();
        itemSlots = invenSlotParent.GetComponentsInChildren<ItemSlotUI>();
    }

    private void Start()
    {
        UpdateSlotUI();
    }

    public bool CheckInventoryIdx(ItemDataSO item)
    {
        if (!invenDictionary.TryGetValue(item, out InventoryItem i) // �ش� �������� �κ��丮�� ����
            && itemSlots.Length <= invenDictionary.Count)
        { 
            print("idx NOT OK");
            return false;
        }
        return true;
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

    public void AddItem(ItemDataSO item)
    {
        if(invenDictionary.TryGetValue(item,out InventoryItem i)) // �ش� �������� inventory�� ���� ���
        {
            i.AddItemCnt();
        }
        else // ������
        {
            if(itemSlots.Length <= invenDictionary.Count) // üũ
            {

            }
            // ���� ���
            InventoryItem newItem = new InventoryItem(item);
            mainInventory.Add(newItem);
            invenDictionary.Add(item, newItem);
        }

        UpdateSlotUI();
    }

    public void RemoveItem(ItemDataSO item, int cnt = 1)
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
}
