using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager Instance;

	private FileDataHandler fileDataHandler;

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
		fileDataHandler = GetComponent<FileDataHandler>();
		saveManagers = FindAllSaveManagers();

		LoadGameData();
	}

	private List<ISaveManager> FindAllSaveManagers() // ISaveManager�� �������̽��� ������ �ִ� ���ӿ�����Ʈ�� �� ã�� ����Ʈ�� �־��ֱ�
	{
		return FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveManager>().ToList();
	}

	public void NewGameData()
	{
		data = new GameData();
	}

	public void LoadGameData()
	{
		data = fileDataHandler.Load();

		if(data == null)
		{
			print("No Save Data");
			NewGameData();
		}
	}

	public void SaveGameData()
	{
		foreach(var save in saveManagers)
		{
			save.SaveData(ref data);
		}

		fileDataHandler.Save(data);
	}

	public void DeleteGameData()
	{
		fileDataHandler = GetComponent<FileDataHandler>();
		fileDataHandler.DeleteGameData();
	}

	private void OnApplicationQuit()
	{
		SaveGameData();
		print("Save");
	}
}
