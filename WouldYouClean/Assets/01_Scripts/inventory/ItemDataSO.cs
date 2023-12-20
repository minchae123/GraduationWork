using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Good,
    Bad
}
[CreateAssetMenu(menuName ="SO/Item")]  
public class ItemDataSO : ScriptableObject
{
    public ItemType itemType;   
    public string itemName;
    public Sprite itemIcon;
}
    