using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControl : MonoBehaviour
{
	private WASD WASD;
	private RaycastHit hit;
	private Ray[] ray = new Ray[6];

	[SerializeField] private LayerMask whatIsBox;
	[SerializeField] private StageSO stageinfo;
	[SerializeField] private Box box;

	private int curCount;
	private int maxCount;
	private Vector3 startPos;

	private bool[] isCanMove = new bool[6];

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

		curCount = 0;
		maxCount = stageinfo.LmoveCnt;
		//startPos = transform.position;
		//curCount = moveCount;
	}

	void Update()
	{
		if (Input.anyKeyDown)
		{
			RayCheck();
		}

		if (curCount <= maxCount)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow) && isCanMove[4]
				&& WASD.w != box?._player2Dir)
			{
				transform.position += WASD.w;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) && isCanMove[5]
				&& WASD.s != box?._player2Dir)
			{
				transform.position += WASD.s;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.RightArrow) && isCanMove[3]
				&& WASD.d != box?._player2Dir)
			{
				transform.position += WASD.d;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow) && isCanMove[2]
				&& WASD.a != box?._player2Dir)
			{
				transform.position += WASD.a;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.Return) && isCanMove[0]
				&& Vector3.up != box?._player2Dir)
			{
				transform.position += Vector3.up;
				curCount--;
			}
			if (Input.GetKeyDown(KeyCode.RightShift) && isCanMove[1]
				&& Vector3.down != box?._player2Dir)
			{
				transform.position += Vector3.down;
				curCount--;
			}

			box?.Determine();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			transform.position = startPos;
			curCount = stageinfo.LmoveCnt;
		}
	}

	public void RayCheck()
	{
		/*
		ray[0].origin = transform.position;
		ray[1].origin = transform.position;
		ray[2].origin = transform.position;
		ray[3].origin = transform.position;
		ray[4].origin = transform.position;
		ray[5].origin = transform.position;
		*/
		for (int i = 0; i < ray.Length; i++)
		{
			ray[i].origin = transform.position;
			Debug.DrawRay(ray[i].origin, ray[i].direction);

			if (Physics.Raycast(ray[i], out hit, 0.5f, whatIsBox))
			{
				isCanMove[i] = true;
			}
			else
			{
				isCanMove[i] = false;
			}
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

					ray[2].direction = -transform.forward;
					ray[3].direction = transform.forward;
					ray[4].direction = -transform.right;
					ray[5].direction = transform.right;
				}
				break;
			case DIRECTION.West:
				{
					WASD.w = Vector3.right;
					WASD.s = -Vector3.right;
					WASD.a = Vector3.forward;
					WASD.d = -Vector3.forward;

					ray[2].direction = transform.forward;
					ray[3].direction = -transform.forward;
					ray[4].direction = transform.right;
					ray[5].direction = -transform.right;
				}
				break;
			case DIRECTION.South:
				{
					WASD.w = Vector3.forward;
					WASD.s = -Vector3.forward;
					WASD.a = -Vector3.right;
					WASD.d = Vector3.right;

					ray[2].direction = -transform.right;
					ray[3].direction = transform.right;
					ray[4].direction = transform.forward;
					ray[5].direction = -transform.forward;
				}
				break;
			case DIRECTION.North:
				{
					WASD.w = -Vector3.forward;
					WASD.s = Vector3.forward;
					WASD.a = Vector3.right;
					WASD.d = -Vector3.right;

					ray[2].direction = transform.right;
					ray[3].direction = -transform.right;
					ray[4].direction = -transform.forward;
					ray[5].direction = transform.forward;
				}
				break;
		}
	}
}
