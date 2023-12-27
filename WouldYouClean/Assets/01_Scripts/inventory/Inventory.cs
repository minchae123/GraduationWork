using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> mainInventory; // 메인 인벤토리
    public Dictionary<ObjectType, InventoryItem> invenDictionary; // 인벤토리 딕셔너리

    [Header("Inventory UI")]
    [SerializeField] private Transform invenSlotParent; // 슬롯 부모
    [SerializeField] private ItemSlotUI slotPrefab;

    private int inventoryLength = 3; // 초기 인벤토리
    private int maxInventoryLength = 10; // 최대 인벤토리
    private ItemSlotUI[] itemSlots; // 슬롯 부모에서 가져올 slotUI들 (prefab)

    [Header("ItemUI")]
    [SerializeField] private Transform checker;
    [SerializeField] private PopUpItem popUpPanel;

    private void Awake()
    {
        if(Instance!=null) { Debug.LogError("Inventory Error"); }
        Instance = this;
        
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

    public bool CheckInventoryIdx(ObjectType item)
    {
        if (!invenDictionary.TryGetValue(item, out InventoryItem i) // 해당 아이템이 인벤토리에 없음
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

    public void AddItem(ObjectType item, bool isTable)
    { 
        if(invenDictionary.TryGetValue(item,out InventoryItem i)) // 해당 아이템이 inventory에 있을 경우
        {
            i.AddItemCnt();
        }
        else // 없으면
        {
            if(inventoryLength <= invenDictionary.Count) // 체크
            {
                print("인벤토리 부족! 업그레이드 필요!");
                return;
            }
            // 새로 등록
            InventoryItem newItem = new InventoryItem(item);
            mainInventory.Add(newItem);
            invenDictionary.Add(item, newItem);
        }

        if(!isTable) // 테이블 통해서 들어온 게 아닐 경우
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

    public void UpgradeInventory(int upgradeLength = 1)
    {
        if (inventoryLength + upgradeLength > maxInventoryLength) 
        {
            print("maxInventory 초과");
            return;
        }

        for (int i = 0; i < upgradeLength; ++i)
        {
            ItemSlotUI newSlot = Instantiate(slotPrefab, invenSlotParent);
            // 0, 1, 2 들어있음 (3->5)
            // 2개가 들어옴 (3, 4 에 있어야함)
            itemSlots[inventoryLength++] = newSlot;
        }
        UpdateSlotUI(); // 업덱
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpgradeInventory();
        }
    }
}
