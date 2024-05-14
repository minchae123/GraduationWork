using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerDir
{
	left,
	right
}

struct WASD
{
	public Vector3 w;
	public Vector3 s;
	public Vector3 a;
	public Vector3 d;
}

public class Movement : MonoBehaviour
{
	private CameraMovement camMovement;

	private RaycastHit hit;
	private Ray[] ray = new Ray[6];

	[SerializeField] private WASD WASD;
	[SerializeField] private LayerMask whatIsBox;
	[SerializeField] private StageSO stageInfo;

	public int curCount;
	public int moveCount;
	public Vector3 direction;
	public PlayerDir playerEnum;

	[SerializeField] private bool[] isCanMove = new bool[6];

	public MeshRenderer render;

	private void Awake()
	{
		//render = GetComponent<MeshRenderer>();
	}

	private void Start()
	{
		camMovement = FindObjectOfType<CameraMovement>();

		RayCheck();
	}

	private void Update()
	{
		BoxManager.Instance.boxDec(transform);

		RayCheck();
	}
	
	public void SetPlayer(Color color, int moveCnt)
	{
		render = GetComponent<MeshRenderer>();
		render.sharedMaterial.SetColor("_PlayerColor", color);
		moveCount = moveCnt;
	}

	public void MoveLeft()
	{
		BoxManager.Instance.boxDec(transform);

		RayCheck();
		if (isCanMove[2] && curCount < moveCount)
		{
			Vector3 pos = Vector3Int.FloorToInt(-camMovement.curTransfrom.transform.right); 
			print(pos);
			transform.position += pos;

			curCount++;
		}
	}

	public void MoveRight()
	{
		BoxManager.Instance.boxDec(transform);

		RayCheck();
		if (isCanMove[3] && curCount < moveCount)
		{
			Vector3 pos = Vector3Int.FloorToInt(camMovement.curTransfrom.transform.right);
			print(pos);
			transform.position += pos;

			curCount++;
		}
	}

	public void MoveUp()
	{
		BoxManager.Instance.boxDec(transform);

		RayCheck();
		if (isCanMove[4] && curCount < moveCount)
		{
			Vector3 pos = Vector3Int.RoundToInt(camMovement.curTransfrom.transform.up);
			print(pos);
			transform.position += pos;

			curCount++;
		}
	}

	public void MoveDown()
	{
		BoxManager.Instance.boxDec(transform);

		RayCheck();
		if (isCanMove[5] && curCount < moveCount)
		{
			Vector3 pos = Vector3Int.RoundToInt(-camMovement.curTransfrom.transform.up);
			print(pos);
			transform.position += pos;

			curCount++;
		}
	}

	public void RayCheck()
	{
		ray[0].direction = transform.up; // y up
		ray[1].direction = -transform.up; // y down
		ray[2].direction = -transform.right; // x left
		ray[3].direction = transform.right; // x right
		ray[4].direction = transform.forward; // z up
		ray[5].direction = -transform.forward; // z down

		for (int i = 0; i < ray.Length; i++)
		{
			ray[i].origin = transform.position;

			Debug.DrawRay(ray[i].origin, ray[i].direction);

			if (camMovement._dir == Direction.Down || camMovement._dir == Direction.Up)
			{
				ray[4].direction = -transform.forward; // z up
				ray[5].direction = transform.forward; // z down
			}
			else
			{
				ray[4].direction = transform.forward; // z up
				ray[5].direction = -transform.forward; // z down
			}

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