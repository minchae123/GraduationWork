using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeTrash : MonoSingleton<MergeTrash>
{
    [Header("==== Core ====")]
    [SerializeField] private GameObject mainPanel;
    public List<InventoryItem> mainList; // 메인 인벤토리
    public Dictionary<ObjectType, InventoryItem> mainDictionary; // 인벤토리 딕셔너리

    [Header("Transform")]
    public Transform inventoryParent;
    public Transform tableParent;

    [Header("UI")]
    public MergeItemSlotUI slotPrefab;
    public MergeItemSlotUI[] itemSlots;


    [Header("변수들")]
    private bool isEnterPuzzle = false;
    private int currentInventoryCnt = 0;

    private void Start()
    {
        mainPanel.SetActive(false);

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
        mainPanel.SetActive(true);

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
        mainPanel.SetActive(false);

        foreach (Transform t in tableParent)
        {
            if (t.TryGetComponent<MergeTable>(out MergeTable m))
            {
                if (m.itemData != null) // not null
                {
                    AddItem(m.itemData); // 다시 넣어주기
                    m.ResetTable(); // 그리고 리셋
                }
            }
        }

        Inventory.Instance.SetChangedInventory(mainList, mainDictionary); // 바뀐 게 있다면 적용...

        // 초기화!
        currentInventoryCnt = 0;
        mainList.Clear();
        mainDictionary.Clear();

        foreach (Transform t in inventoryParent)
        {
            if (t.TryGetComponent<MergeItemSlotUI>(out MergeItemSlotUI m))
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

        for (int i = 0; i < mainList.Count; ++i)
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

    public void AddItem(ObjectType item, int cnt = 1)
    {
        if (mainDictionary.TryGetValue(item, out InventoryItem i)) // 해당 아이템이 inventory에 있을 경우
        {
            i.AddItemCnt(cnt);
        }
        else
        {
            // 새로 등록
            InventoryItem newItem = new InventoryItem(item);
            mainList.Add(newItem);
            mainDictionary.Add(item, newItem);
        }

        UpdateSlotUI();
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

        UpdateSlotUI();
    }

    public void OnClickMergeButton()
    {
        List<ObjectType> objs = new List<ObjectType>();
        foreach (Transform t in tableParent)
        {
            if (t.TryGetComponent<MergeTable>(out MergeTable table))
            {
                if (table.itemData == null)
                {
                    print("둘 다 채우세욤");
                    return;
                }
                objs.Add(table.itemData); // 합친 거
                table.ResetTable(); // 합쳤으니 제거
            }
        }
        
        // 합친 걸로 뭐할 건지는 아래에 작성 (objs 리스트 활용)
    }
}
