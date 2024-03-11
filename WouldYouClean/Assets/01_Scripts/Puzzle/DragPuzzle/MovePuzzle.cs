using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuzzle : MonoBehaviour
{

	private Camera cam;

	private void Start()
	{
		cam = GameManager.Instance.mainCam;
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mouse = Input.mousePosition;
		}
	}
}
