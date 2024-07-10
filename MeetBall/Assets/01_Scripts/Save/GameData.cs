using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
	public SerializableDictionary<string, SerializableDictionary<int, bool>> bigStage = new SerializableDictionary<string, SerializableDictionary<int, bool>>();

	public GameData()
	{
		var smallStages = new SerializableDictionary<int, bool>();
		for (int i = 0; i < 5; i++)
		{
			smallStages.Add(i , false);
		}

		bigStage.Add("Snow", smallStages);

		smallStages = new SerializableDictionary<int, bool>();
		for (int i = 0; i < 5; i++)
		{
			smallStages.Add(i, false);
		}
		bigStage.Add("Sea", smallStages);
	}
}
