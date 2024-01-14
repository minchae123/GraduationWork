using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test2 : MonoBehaviour, ISaveManager
{
	public void LoadData(GameData data)
	{

	}

	public void SaveData(ref GameData data)
	{

	}

	private void Start()
	{
		Vector3 pos = transform.position;
		float r = Random.Range(1, 4);
		transform.DOMoveY(pos.y + 1, r).SetLoops(-1, LoopType.Yoyo);
	}
}
