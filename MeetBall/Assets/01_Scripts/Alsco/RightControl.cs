using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControl : MonoBehaviour
{
	private WASD WASD;

	[SerializeField] private int moveCount;
	private int curCount;
	private Vector3 startPos;

	private void Start()
	{
		startPos = transform.position;
		curCount = moveCount;
	}

	void Update()
	{
		if(curCount > 0)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				transform.position += WASD.w;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				transform.position += WASD.s;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				transform.position += WASD.d;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				transform.position += WASD.a;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.Return))
			{
				transform.position += Vector3.up;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.RightShift))
			{
				transform.position += Vector3.down;
				curCount--;
			}
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			transform.position = startPos;
			curCount = moveCount;
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
