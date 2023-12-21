using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager Instance;

	public GameData data;

	private List<ISaveManager> saveManagers;

	private void Awake()
	{
		if(Instance != null)
			Debug.LogError("SaveManager Error");

		Instance = this;
	}

	private void Start()
	{
		saveManagers = FindAllSaveManagers();
	}

	private List<ISaveManager> FindAllSaveManagers() // ISaveManager의 인터페이스를 가지고 있는 게임오브젝트를 다 찾고 리스트에 넣어주기
	{
		return FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveManager>().ToList();
	}

	public void NewData()
	{
		data = new GameData();
	}

	public void LoadData()
	{
		
	}

	public void SaveData()
	{

	}

	public void DeleteData()
	{

	}
}
