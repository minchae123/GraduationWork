using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeTrash : MonoBehaviour
{
    [Header("==== Core ====")]
    public List<InventoryItem> mainList; // 메인 인벤토리
    public Dictionary<ObjectType, InventoryItem> mainDictionary; // 인벤토리 딕셔너리

    [Header("Transform")]
    public Transform inventoryParent;

    [Header("UI")]
    public MergeItemSlotUI slotPrefab;
    public MergeItemSlotUI[] itemSlots;


    [Header("변수들")]
    private bool isEnterPuzzle = false;
    private int currentInventoryCnt = 0;

    private void Start()
    {
        mainList = new List<InventoryItem>();
        mainDictionary = new Dictionary<ObjectType, InventoryItem>();
        itemSlots = new MergeItemSlotUI[8]; //인벤 최대 8개
    }

    // 디버깅용
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            isEnterPuzzle = !isEnterPuzzle;
            if (isEnterPuzzle) EnterPuzzle();
            else ExitPuzzle();
        }
    }

    public void EnterPuzzle()
    {
        foreach (var i in Inventory.Instance.ReturnInvenList())
        {
            mainList.Add(i);
        }
        foreach (var i in Inventory.Instance.ReturnInvenDictionary())
        {
            mainDictionary.Add(i.Key, i.Value);
        }
        currentInventoryCnt = mainList.Count;

        for (int i = 0; i < currentInventoryCnt; ++i)
        {
            MergeItemSlotUI s = Instantiate(slotPrefab, inventoryParent);
            itemSlots[i] = s;
        }

        UpdateSlotUI();
    }

    public void ExitPuzzle()
    {
        Inventory.Instance.SetChangedInventory(mainList, mainDictionary); // 바뀐 게 있다면 적용...

        // 초기화!
        currentInventoryCnt = 0;
        mainList.Clear();
        mainDictionary.Clear();

        foreach (Transform child in inventoryParent)
        {
            if (child.TryGetComponent<MergeItemSlotUI>(out MergeItemSlotUI m))
            {
                Destroy(m.gameObject);
            }
        }
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < currentInventoryCnt; ++i)
        {
            itemSlots[i].CleanUpSlot(); // cleanUp
        }

        for (int i = 0; i < currentInventoryCnt; ++i)
        {
            itemSlots[i].UpdateSlot(mainList[i]); // redraw
        }
    }
    public void ClearItem()
    {
        for (int i = 0; i < currentInventoryCnt; ++i)
        {
            itemSlots[i].CleanUpSlot(); // cleanUp
        }
    }

    public void UseItem(ObjectType item, int cnt = 1)
    {
        if (mainDictionary.TryGetValue(item, out InventoryItem i))
        {
            if (i.itemCnt <= cnt) // 남아잇는 양이 지워야 하는 양보다 작거나 같을 경우?
            {
                // 아예 삭제
                mainDictionary.Remove(item);
                mainList.Remove(i);
            }
            else // 해당 아이템이 더 많을 경우
            {
                i.RemoveItemCnt(cnt); // cnt만큼 빼주기
            }
        }
    }
}
