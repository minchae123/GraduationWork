using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FileDataHandler : MonoBehaviour
{
	/*private string filePath;
	private string fileName = "/GameData.txt";

	public FileDataHandler()
	{
		filePath = Application.dataPath + "/SaveGameData/";
		fileName = "/GameData.txt";
	}

	private void Awake()
	{
		if(!Directory.Exists(filePath))
		{
			print("Not exists");
			Directory.CreateDirectory(filePath);
			print("create");
		}
	}

	public void Save(GameData data)
	{
		filePath = Application.dataPath + "/SaveGameData/";

		string json = JsonUtility.ToJson(data);
		File.WriteAllText(filePath + fileName, json);
	}

	public GameData Load()
	{
		GameData loadedData = null;

		if (File.Exists(filePath))
		{
			try
			{
				string dataToLoad = "";
				using (FileStream readStream = new FileStream(filePath, FileMode.Open))
				{
					using (StreamReader reader = new StreamReader(readStream))
					{
						dataToLoad = reader.ReadToEnd(); // 끝까지 다 읽기
					}
				}

				loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
			}
			catch (Exception ex)
			{
				Debug.LogError($"Error on trying to load data from file {filePath}");
			}
		}

		return loadedData;
	}*/

	public GameData gameData;

	private string filePath;
	private string fileName = "/GameData.txt";

	private void Awake()
	{
		gameData = new GameData();
		filePath = Application.dataPath + "/SaveData/";

		if (!Directory.Exists(filePath))
		{
			Directory.CreateDirectory(filePath);
		}
	}

	[ContextMenu("Save")]
	public void Save(GameData data)
	{
		gameData = data;

		string json = JsonUtility.ToJson(gameData, true);
		File.WriteAllText(filePath + fileName, json);
	}

	[ContextMenu("Load")]
	public GameData Load()
	{
		if (File.Exists(filePath + fileName))
		{
			string loadJson = File.ReadAllText(filePath + fileName);
			gameData = JsonUtility.FromJson<GameData>(loadJson);

			return gameData;
		}
		else
		{
			Debug.Log("Save Failed");
			return null;
		}
	}

	public void DeleteGameData()
	{
		if (File.Exists(filePath))
		{
			try
			{
				File.Delete(filePath);
			}
			catch (Exception ex)
			{
				Debug.LogError($"Error on trying to load data from file {filePath}");
			}
		}
	}
}
