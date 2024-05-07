using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.UI;

struct WASD
{
	public Vector3 w;
	public Vector3 s;
	public Vector3 a;
	public Vector3 d;
}

public class Movement : MonoBehaviour
{
	public enum PlayerType
	{
		Player1, Player2
	}

	[SerializeField] private WASD WASD;
	private RaycastHit hit;
	private Ray[] ray = new Ray[6];

	[SerializeField] private LayerMask whatIsBox;
	[SerializeField] private StageSO stageInfo;

	private int curCount;
	private int moveCount;
	public Vector3 direction;

	[SerializeField] private bool[] isCanMove = new bool[6];

	public PlayerType playerType;
	private Dictionary<PlayerType, bool> playerDic = new Dictionary<PlayerType, bool>()
	{
		{PlayerType.Player1, true},
		{PlayerType.Player2, false}
	};


	private void Start()
	{
		ray[0].direction = transform.up; // y up
		ray[1].direction = -transform.up; // y down
		ray[2].direction = -transform.right; // x left
		ray[3].direction = transform.right; // x right
		ray[4].direction = transform.forward; // z up
		ray[5].direction = -transform.forward; // z down

		moveCount = PlayerType.Player1 == playerType ? stageInfo.player1MoveCount : stageInfo.player2MoveCount;

		playerDic[playerType] = playerType == PlayerType.Player1 ? true : false;
		print(playerDic[playerType]);

		Move(DIRECTION.South);
		RayCheck();
	}

	public void ChangeCanMove(bool value)
	{
		playerDic[playerType] = value;
	}

	public void MoveLeft()
	{
		if (isCanMove[2] && playerDic[playerType])
		{
			transform.position += WASD.a;
		}
		RayCheck();
	}

	public void MoveRight()
	{
		print(playerDic[playerType]);
		if (isCanMove[3] && playerDic[playerType])
		{
			transform.position += WASD.d;
		}
		RayCheck();
	}

	public void MoveUp()
	{
		if (isCanMove[4] && playerDic[playerType])
		{
			print(WASD.w);
			transform.position += WASD.w;
		}
		RayCheck();
	}

	public void MoveDown()
	{
		if (isCanMove[5] && playerDic[playerType])
		{
			transform.position += WASD.s;
		}
		RayCheck();
	}

	public void RayCheck()
	{
		for (int i = 0; i < ray.Length; i++)
		{
			ray[i].origin = transform.position;
			Debug.DrawRay(ray[i].origin, ray[i].direction);

			if (Physics.Raycast(ray[i], out hit, 0.5f, whatIsBox))
			{
				Debug.DrawRay(ray[i].origin, ray[i].direction, Color.red);
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
