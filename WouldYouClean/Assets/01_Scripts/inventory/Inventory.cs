using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> mainInventory; // 메인 인벤토리
    public Dictionary<ItemDataSO, InventoryItem> invenDictionary; // 인벤토리 딕셔너리

    [Header("Inventory UI")]
    [SerializeField] private Transform invenSlotParent; // 슬롯 부모
    private ItemSlotUI[] itemSlots; // 슬롯 부모에서 가져올 slotUI들 (prefab)

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
        if (!invenDictionary.TryGetValue(item, out InventoryItem i) // 해당 아이템이 인벤토리에 없음
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
        if(invenDictionary.TryGetValue(item,out InventoryItem i)) // 해당 아이템이 inventory에 있을 경우
        {
            i.AddItemCnt();
        }
        else // 없으면
        {
            if(itemSlots.Length <= invenDictionary.Count) // 체크
            {

            }
            // 새로 등록
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
            if(i.itemCnt <= cnt) // 남아잇는 양이 지워야 하는 양보다 작거나 같을 경우?
            {
                // 아예 삭제
                invenDictionary.Remove(item);
                mainInventory.Remove(i);
            }
            else // 해당 아이템이 더 많을 경우
            {
                i.RemoveItemCnt(cnt); // cnt만큼 빼주기
            }
        }

        UpdateSlotUI();
    }
}
