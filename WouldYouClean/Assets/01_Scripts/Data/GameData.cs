using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DInventoryItem // 인벤토리 아이템
{
	public string name;
	public int count;

	public DInventoryItem(string name, int count)
	{
		this.name = name;
		this.count = count;
	}
}

[Serializable]
public class DInventory // 아이템들이 들어있는 집합
{
	public List<DInventoryItem> items;
}

public class GameData 
{
	[Header("돈")]
	public int coin; // 재화

	[Header("업그레이드")]
	public int cleanerLevel;
	public int oxygenLevel;

	[Header("인벤토리")]
	public DInventory inventory;
}
