using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
	public SerializableDictionary<string, SerializableDictionary<int, bool>> bigStage = new SerializableDictionary<string, SerializableDictionary<int, bool>>();

	public GameData()
	{
		var snowStages = new SerializableDictionary<int, bool>();
		for (int i = 0; i < 5; i++)
		{
			snowStages.Add(i , false);
		}

		bigStage.Add("Snow", snowStages);

		var CinderellaStage = new SerializableDictionary<int, bool>();
		for (int i = 0; i < 6; i++)
		{
            CinderellaStage.Add(i, false);
		}
		bigStage.Add("Cinderella", CinderellaStage);

        var MermaidStage = new SerializableDictionary<int, bool>();
        for (int i = 0; i < 7; i++)
        {
            MermaidStage.Add(i, false);
        }
        bigStage.Add("Mermaid", MermaidStage);
    }
}

