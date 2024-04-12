using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftControl : MonoBehaviour
{
	public int moveCount;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			Vector3 pos = transform.position;
			pos.x += 2;
			transform.position = pos;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			Vector3 pos = transform.position;
			pos.x -= 2;
			transform.position = pos;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			Vector3 pos = transform.position;
			pos.z -= 2;
			transform.position = pos;
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			Vector3 pos = transform.position;
			pos.z += 2;
			transform.position = pos;
		}
	}
}
