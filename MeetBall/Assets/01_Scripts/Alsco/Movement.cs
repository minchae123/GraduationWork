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

	private MeshRenderer mr;

    private void Awake()
    {
		mr = GetComponent<MeshRenderer>();
    }

    private void Start()
	{
		ray[0].direction = transform.up; // y up
		ray[1].direction = -transform.up; // y down
		ray[2].direction = -transform.right; // x left
		ray[3].direction = transform.right; // x right
		ray[4].direction = transform.forward; // z up
		ray[5].direction = -transform.forward; // z down

		camMovement = FindObjectOfType<CameraMovement>();

		RayCheck();
	}

	public void SetPlayer(Color color, int moveCnt)
    {
		mr.material.color = color;
		moveCount = moveCnt;
    }

	public void MoveLeft()
	{
		RayCheck();
		if (isCanMove[2])
		{
			transform.position += -camMovement.cinemachineCam.transform.right;
		}
	}

	public void MoveRight()
	{
		RayCheck();
		if (isCanMove[3])
		{
			transform.position += camMovement.cinemachineCam.transform.right;
		}
	}

	public void MoveUp()
	{
		RayCheck();
		if (isCanMove[4])
		{
			transform.position += camMovement.cinemachineCam.transform.up;
		}
	}

	public void MoveDown()
	{
		RayCheck();
		if (isCanMove[5])
		{
			transform.position += -camMovement.cinemachineCam.transform.up;
		}
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
