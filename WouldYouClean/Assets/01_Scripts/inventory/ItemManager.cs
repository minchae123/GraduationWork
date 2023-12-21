using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemTestTable itemTable;

    public ItemObject itemPrefab;
    public static ItemManager Instance;

    private bool inTable = false;
    public bool CheckInTable() => inTable;

    private void Awake()
    {
        if (Instance != null) print("ItemManager Error");
        Instance = this;
    }

    public void CreateItem(ItemDataSO data, Vector2 pos)
    {
        ItemObject item = Instantiate(itemPrefab);
        item.SetItemData(data);
        item.SetPosition(pos);
    }

    public void CheckInTable(bool v)
    {
        inTable = v;
    }

    public void SetItemTable(ItemDataSO item)
    {
        itemTable.SetItem(item);
    }
}
