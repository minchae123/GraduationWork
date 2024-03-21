using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MergeClass
{
    //public Sprite sprite;
    public Color sprite; // ���߿� �����ɷ�...,.
    public float scale;
}
[CreateAssetMenu(menuName = "SO/Puzzle/MergeItem")]
public class MergeItemSO : ScriptableObject
{
    public List<MergeClass> itemList;
}
