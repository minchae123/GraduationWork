using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControl : MonoBehaviour
{
	private WASD WASD;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			transform.position += WASD.w;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			transform.position += WASD.s;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			transform.position += WASD.d;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.position += WASD.a;
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			transform.position += Vector3.up;
		}
		if (Input.GetKeyDown(KeyCode.RightShift))
		{
			transform.position += Vector3.down;
		}
	}

	public void Move(DIRECTION dir)
	{
		switch (dir)
		{
			case DIRECTION.East:
				{
					WASD.w = -Vector3.right;
					WASD.s = Vector3.right;
					WASD.a = -Vector3.forward;
					WASD.d = Vector3.forward;
				}
				break;
			case DIRECTION.West:
				{
					WASD.w = Vector3.right;
					WASD.s = -Vector3.right;
					WASD.a = Vector3.forward;
					WASD.d = -Vector3.forward;
				}
				break;
			case DIRECTION.South:
				{
					WASD.w = Vector3.forward;
					WASD.s = -Vector3.forward;
					WASD.a = -Vector3.right;
					WASD.d = Vector3.right;
				}
				break;
			case DIRECTION.North:
				{
					WASD.w = -Vector3.forward;
					WASD.s = Vector3.forward;
					WASD.a = Vector3.right;
					WASD.d = -Vector3.right;
				}
				break;
		}
	}
}
