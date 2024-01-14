using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTester : MonoBehaviour, ISaveManager
{
	[SerializeField] private int level;
	public DInventory inven;
	public List<DInventoryItem> invenItems;

	private void Start()
	{
		LoadData(SaveManager.Instance.data);
	}

	public void LoadData(GameData data)
	{
		inven = data.inventory;
		level = data.cleanerLevel;

		for (int i = 0; i < inven.items.Count; i++)
		{
			var inv = inven.items[i];
			invenItems.Add(inv);
		}
	}

	public void SaveData(ref GameData data)
	{
		data.inventory = inven;
		data.cleanerLevel = level;
	}
}
