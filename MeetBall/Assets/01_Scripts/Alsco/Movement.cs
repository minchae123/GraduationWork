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

public class Movement : MonoBehaviour
{
	private CameraMovement camMovement;
	private GimmickExplain gimmick;

	private RaycastHit hit;
	private Ray[] ray = new Ray[6];

	[SerializeField] private LayerMask whatIsBox;

	public int curCount;
	private int totalCount;
	public int TotalCount => totalCount;

	public PlayerDir playerEnum;

	private OriginColorEnum playerColor;
	public OriginColorEnum PlayerColor => playerColor;

	public Vector3 direction { get; set; }

	[SerializeField] private bool[] isCanMove = new bool[6];

	public MeshRenderer render;

	private void Awake()
	{
		camMovement = FindObjectOfType<CameraMovement>();
		gimmick = FindObjectOfType<GimmickExplain>();

		curCount = 0;
		totalCount = 0;

		RayCheck();
	}

	private void Update()
	{
		if (FindObjectOfType<StageManager>() && StageManager.Instance.IsInStage) BoxManager.Instance.boxDec(transform);
		else if (FindObjectOfType<TutorialStageManager>() && TutorialStageManager.Instance.IsInStage) BoxManager.Instance.boxDec(transform);

	}

	public void SetPlayer(OriginColorEnum color, int moveCnt)
	{
		Color c = GameManager.Instance.FindColor(color);

		render = GetComponent<MeshRenderer>();
		render.sharedMaterial.SetColor("_PlayerColor", c);

		playerColor = color;
		totalCount = moveCnt;
	}
	public void SetPlayerColor(OriginColorEnum color)
	{
		Color c = GameManager.Instance.FindColor(color);
		render.sharedMaterial.SetColor("_PlayerColor", c);

		playerColor = color;
	}

	public void MoveLeft()
	{
		RayCheck();

		direction = (-camMovement.curTransfrom.transform.right);
		BoxManager.Instance.boxDec(transform);

		if (isCanMove[2] && curCount < totalCount && direction != Vector3.zero)
		{
			Move(direction);
		}
	}

	public void MoveRight()
	{
		RayCheck();

		direction = (camMovement.curTransfrom.transform.right);
		BoxManager.Instance.boxDec(transform);

		if (isCanMove[3] && curCount < totalCount && direction != Vector3.zero)
		{
			Move(direction);
		}
	}

	public void MoveUp()
	{
		RayCheck();

		direction = (camMovement.curTransfrom.transform.up);
		BoxManager.Instance.boxDec(transform);

		if (isCanMove[0] && curCount < totalCount && direction != Vector3.zero)
		{
			Move(direction);
		}
	}

	public void MoveDown()
	{
		RayCheck();

		direction = (-camMovement.curTransfrom.transform.up);
		BoxManager.Instance.boxDec(transform);

		if (isCanMove[1] && curCount < totalCount && direction != Vector3.zero)
		{
			Move(direction);
		}
	}

	private int Round(float f)
	{
		return Mathf.RoundToInt(f);
	}

	public void Move(Vector3 dir)
	{
		if(gimmick.panel.isWait)
			gimmick.panel.CloseTutorial();

		Vector3 pos = new Vector3(Round(dir.x+transform.localPosition.x),Round(dir.y+transform.localPosition.y),Round(dir.z+transform.localPosition.z));
		//print($"dir {dir} / pos {transform.localPosition} / result {pos} {pos.x} {pos.y} {pos.z}");

		transform.localPosition = pos;

		direction = Vector3.zero;

		curCount++;
	}

	public void RayCheck()
	{
		ray[0].direction = camMovement.curTransfrom.transform.up; // y up
		ray[1].direction = -camMovement.curTransfrom.transform.up; // y down
		ray[2].direction = -camMovement.curTransfrom.transform.right; // x left
		ray[3].direction = camMovement.curTransfrom.transform.right; // x right
		ray[4].direction = camMovement.curTransfrom.transform.forward; // z up
		ray[5].direction = -camMovement.curTransfrom.transform.forward; // z down

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