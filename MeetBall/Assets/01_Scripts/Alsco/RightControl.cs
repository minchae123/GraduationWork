using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControl : MonoBehaviour
{
	private WASD WASD;

	[SerializeField] private int moveCount;
	private int curCount;
	private Vector3 startPos;

	private RaycastHit hit;
	private Ray[] ray = new Ray[6];
	[SerializeField] private LayerMask whatIsBox;

	private void Start()
	{
		//상하좌우앞뒤
		ray[0].direction = transform.up;
		ray[1].direction = -transform.up;
		ray[2].direction = -transform.right;
		ray[3].direction = transform.right;
		ray[4].direction = transform.forward;
		ray[5].direction = -transform.forward;

		startPos = transform.position;
		curCount = moveCount;
	}

	void Update()
	{
		ray[0].origin = transform.position;
		ray[1].origin = transform.position;
		ray[2].origin = transform.position;
		ray[3].origin = transform.position;
		ray[4].origin = transform.position;
		ray[5].origin = transform.position;

		for (int i = 0; i < ray.Length; i++)
		{
			Debug.DrawRay(ray[i].origin, ray[i].direction);
			if (Physics.Raycast(ray[i], out hit, 0.5f, whatIsBox))
			{
				Debug.DrawRay(ray[i].origin, ray[i].direction, Color.red);
			}
		}


		if (curCount > 0)
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
