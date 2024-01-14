using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DInventoryItem // �κ��丮 ������
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
public class DInventory // �����۵��� ����ִ� ����
{
	public List<DInventoryItem> items;
}

public class GameData
{
	[Header("��")]
	public int coin = 0; // ��ȭ

	[Header("���׷��̵�")]
	public int cleanerLevel = 0;
	public int oxygenLevel = 0;

	[Header("�κ��丮")]
	public DInventory inventory = new DInventory();

	public GameData()
	{
		coin = 0;
		cleanerLevel = 0;
		oxygenLevel = 0;
		inventory = new DInventory();
	}
}
