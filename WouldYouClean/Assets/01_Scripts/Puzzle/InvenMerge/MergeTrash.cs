using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeTrash : MonoSingleton<MergeTrash>
{
    [Header("==== Core ====")]
    [SerializeField] private GameObject mainPanel;
    public List<InventoryItem> mainList; // ���� �κ��丮
    public Dictionary<ObjectType, InventoryItem> mainDictionary; // �κ��丮 ��ųʸ�

    [Header("Transform")]
    public Transform inventoryParent;
    public Transform tableParent;

    [Header("UI")]
    public MergeItemSlotUI slotPrefab;
    public MergeItemSlotUI[] itemSlots;


    [Header("������")]
    private bool isEnterPuzzle = false;
    private int currentInventoryCnt = 0;

    private void Start()
    {
        mainPanel.SetActive(false);

        mainList = new List<InventoryItem>();
        mainDictionary = new Dictionary<ObjectType, InventoryItem>();
        itemSlots = new MergeItemSlotUI[8]; //�κ� �ִ� 8��
    }

    // ������
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
                    AddItem(m.itemData); // �ٽ� �־��ֱ�
                    m.ResetTable(); // �׸��� ����
                }
            }
        }

        Inventory.Instance.SetChangedInventory(mainList, mainDictionary); // �ٲ� �� �ִٸ� ����...

        // �ʱ�ȭ!
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
        if (mainDictionary.TryGetValue(item, out InventoryItem i)) // �ش� �������� inventory�� ���� ���
        {
            i.AddItemCnt(cnt);
        }
        else
        {
            // ���� ���
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
            if (i.itemCnt <= cnt) // �����մ� ���� ������ �ϴ� �纸�� �۰ų� ���� ���?
            {
                // �ƿ� ����
                mainDictionary.Remove(item);
                mainList.Remove(i);
            }
            else // �ش� �������� �� ���� ���
            {
                i.RemoveItemCnt(cnt); // cnt��ŭ ���ֱ�
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
                    print("�� �� ä�켼��");
                    return;
                }
                objs.Add(table.itemData); // ��ģ ��
                table.ResetTable(); // �������� ����
            }
        }
        
        // ��ģ �ɷ� ���� ������ �Ʒ��� �ۼ� (objs ����Ʈ Ȱ��)
    }
}
