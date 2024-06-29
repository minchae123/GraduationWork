using echo17.EndlessBook;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SaveManager : MonoSingleton<SaveManager>
{
	private GameData data;
	private string savePath;
	private string fileName = "/SaveFile.txt";

	private void Awake()
	{
		data = new GameData();
		savePath = Application.dataPath + "/SaveData/";

		if(!Directory.Exists(savePath))
		{
			Directory.CreateDirectory(savePath);
		}
	}

	[ContextMenu("����")]
	public void Save(GameData gd)
	{
		data = gd;

		string json = JsonUtility.ToJson(data);
		File.WriteAllText(savePath + fileName, json);
		Debug.Log(json);
	}

	[ContextMenu("�ε�")]
	public void Load()
	{
		if (File.Exists(savePath + fileName))
		{
			string loadJson = File.ReadAllText(savePath + fileName);
			data = JsonUtility.FromJson<GameData>(loadJson);

			GameManager.Instance.gameData = data;
			
			Debug.Log("�ε� ���� !");
		}
		else
		{
			Debug.Log("���� ���� !!!");
		}
	}

	private void OnApplicationQuit()
	{
		Save(GameManager.Instance.gameData);
	}
}
