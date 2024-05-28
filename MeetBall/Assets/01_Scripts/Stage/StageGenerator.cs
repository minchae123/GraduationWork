using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class StageGenerator : MonoBehaviour
{
	public List<GameObject> Blocks;
	public List<Vector3> SaveBlocks;

	private float _radius = 2.5f;

	private void Awake()
	{
		foreach (var block in Blocks)
		{
			if(block != null) 
				SaveBlocks.Add(block.transform.position);
		}
	}

	private void Start()
	{
		ResetStage();

		StartCoroutine(StageLoad());
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
			StageLoad();
	}

	IEnumerator StageLoad()
	{
		yield return new WaitForSeconds(.3f);
		for (int i = 0; i < Blocks.Count; i++)
		{
			Blocks[i].transform.DOMove(SaveBlocks[i], .1f);
			//yield return new WaitForSeconds(.5f / Blocks.Count);
		}
	}

	void ResetStage()
	{
		//foreach (var block in Blocks)
		//{
		//    block.transform.position = new Vector3(block.transform.position.x, 10, block.transform.position.z);
		//}

		for (int i = 0; i < Blocks.Count; i++)
		{
			float angle = i * Mathf.PI * 2 / Blocks.Count;
			float x = Mathf.Cos(angle) * _radius;
			float y = Mathf.Sin(angle) * _radius;
			float z = Mathf.Tan(angle) * _radius;

			Blocks[i].transform.DOMove(new Vector3(x, y, z), .1f);

			//ȸ��PLZ
		}
	}
}
