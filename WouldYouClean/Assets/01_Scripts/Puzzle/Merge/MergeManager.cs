using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoSingleton<MergeManager>
{
    [SerializeField] private MergeItem mergeItemPrefab;
    [SerializeField] private MergeItemSO info;

    private void Start()
    {
        SpawnMerge();
    }
    public void SpawnMerge()
    {
        Instantiate(mergeItemPrefab);
    }
    public MergeClass SetItem(int level)
    {
        print(level);
        return info.itemList[level];
    }
}
