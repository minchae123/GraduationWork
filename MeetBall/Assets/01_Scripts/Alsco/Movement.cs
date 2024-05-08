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

	private CameraMovement camMovement;
	
	private RaycastHit hit;
	private Ray[] ray = new Ray[6];

	[SerializeField] private WASD WASD;
	[SerializeField] private LayerMask whatIsBox;
	[SerializeField] private StageSO stageInfo;

	public int curCount;
	public int moveCount;
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

		camMovement = FindObjectOfType<CameraMovement>();

		RayCheck();
	}

	public void ChangeCanMove(bool value)
	{
		playerDic[playerType] = value;
	}

	public void MoveLeft()
	{
		if (isCanMove[2])
		{
			transform.position += -camMovement.cinemachineCam.transform.right;
		}
		RayCheck();
	}

	public void MoveRight()
	{
		if (isCanMove[3] )
		{
			transform.position += camMovement.cinemachineCam.transform.right;
		}
		RayCheck();
	}

	public void MoveUp()
	{
		if (isCanMove[4])
		{
			print(WASD.w);
			transform.position += camMovement.cinemachineCam.transform.up;
		}
		RayCheck();
	}

	public void MoveDown()
	{
		if (isCanMove[5])
		{
			transform.position += -camMovement.cinemachineCam.transform.up;
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

}
