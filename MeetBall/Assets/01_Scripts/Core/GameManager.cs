using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	public int curStage;

	private void Awake()
	{
		Instance = this;
	}

	public void StageUp()
	{
		curStage++;
	}
}
