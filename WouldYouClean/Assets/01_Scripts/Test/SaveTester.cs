using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTester : MonoBehaviour, ISaveManager
{
	[SerializeField] private int level;
	public DInventory inven;
	public List<DInventoryItem> invenItems;

	private void Awake()
	{
		for (int i = 0; i < invenItems.Count; i++)
		{
			inven.items.Add(invenItems[i]);
		}
	}

	public void LoadData(GameData data)
	{
		inven = data.inventory;
		level = data.cleanerLevel;
	}

	public void SaveData(ref GameData data)
	{
		data.inventory = inven;
		data.cleanerLevel = level;
	}
}
