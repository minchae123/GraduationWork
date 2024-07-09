using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
	public List<bool> insdieStages = new List<bool>();

	public Stage()
	{
		for(int i = 0; i < 10; i++)
		{
			insdieStages.Add(false);
		}
	}
}

public class GameData 
{
	public SerializableDictionary<string, Stage> bigStage = new SerializableDictionary<string, Stage>();
	
	public GameData()
	{
		bigStage.Add("Snow", new Stage());
	}
}
