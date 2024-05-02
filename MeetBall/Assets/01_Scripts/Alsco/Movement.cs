using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] private WASD WASD;
	private RaycastHit hit;
	private Ray[] ray = new Ray[6];

	[SerializeField] private LayerMask whatIsBox;
	[SerializeField] private StageSO stageInfo;

	private int curCount;
	private int moveCount;
	public Vector3 direction;

	[SerializeField] private bool[] isCanMove = new bool[6];

	public bool isTurn;

	private void Start()
	{
		ray[0].direction = transform.up; // y up
		ray[1].direction = -transform.up; // y down
		ray[2].direction = -transform.right; // x left
		ray[3].direction = transform.right; // x right
		ray[4].direction = transform.forward; // z up
		ray[5].direction = -transform.forward; // z down

		moveCount = isTurn ? stageInfo.player1MoveCount : stageInfo.player2MoveCount;

		RayCheck();
	}

	public void MoveLeft()
	{
		if (isCanMove[2] && isTurn)
		{
			transform.position += WASD.a;
			RayCheck();
		}
	}

	public void MoveRight()
	{
		if (isCanMove[5] && isTurn)
		{
			transform.position += WASD.d;
			RayCheck();
		}
	}

	public void MoveUp()
	{
		if (isCanMove[4] && isTurn)
		{
			print(WASD.w);
			transform.position += WASD.w;
			RayCheck();
		}
	}

	public void MoveDown()
	{
		if (isCanMove[5] && isTurn)
		{
			transform.position += WASD.s;
			RayCheck();
		}
	}

	public void RayCheck()
	{
		for (int i = 0; i < ray.Length; i++)
		{
			ray[i].origin = transform.position;

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
