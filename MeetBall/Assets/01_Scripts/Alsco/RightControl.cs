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

		curCount = -1;
		maxCount = stageinfo.RmoveCnt;
	}

	void Update()
	{
		if (Input.anyKeyDown)
		{
			RayCheck();
		}

		if (curCount < maxCount)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow) && isCanMove[4] && WASD.w != returnBox(box)?._player2Dir)
			{
				transform.position += WASD.w;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) && isCanMove[5] && WASD.s != returnBox(box)?._player2Dir)
			{
				transform.position += WASD.s;
			}
			if (Input.GetKeyDown(KeyCode.RightArrow) && isCanMove[3] && WASD.d != returnBox(box)?._player2Dir)
			{
				transform.position += WASD.d;
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow) && isCanMove[2] && WASD.a != returnBox(box)?._player2Dir)
			{
				transform.position += WASD.a;
			}
			if (Input.GetKeyDown(KeyCode.Return) && isCanMove[0] && Vector3.up != returnBox(box)?._player2Dir)
			{
				transform.position += Vector3.up;
			}
			if (Input.GetKeyDown(KeyCode.RightShift) && isCanMove[1] && Vector3.down != returnBox(box)?._player2Dir)
			{
				transform.position += Vector3.down;
			}

			returnBox(box)?.Determine();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			transform.position = startPos;
			curCount = stageinfo.RmoveCnt;
		}
	}

	private Box returnBox(Box box)
    {
		if(box != null)
			return box;

		return null;
    }

	public void RayCheck()
	{
		for (int i = 0; i < ray.Length; i++)
		{
			ray[i].origin = transform.position;
			Debug.DrawRay(ray[i].origin, ray[i].direction);

			if (Physics.Raycast(ray[i], out hit, 0.5f, whatIsBox))
			{
				if (hit.collider.TryGetComponent<MapCube>(out MapCube m))
				{
					if (m.isVisit) // 방문을 한 곳인데
					{
						if (mapVisited.TryPeek(out MapCube checkM))
						{
							if (m == checkM) // 전에 바로 왔던 곳일 경우
							{
								isCanMove[i] = true; // 가능
							}
							else
							{
								isCanMove[i] = false; // 불가능
							}
						}
					}
					else // 방문 안 한 곳이면
					{
						isCanMove[i] = true; // 가능
					}
				}
				else
				{
					isCanMove[i] = true; // 가능
				}
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

	public MapCube beforeCube;
	private Stack<MapCube> mapVisited = new Stack<MapCube>();

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Moveable")) // 
		{
			if (other.TryGetComponent<MapCube>(out MapCube m))
			{
				if (m.isVisit)
				{
					if (mapVisited.TryPeek(out MapCube checkM))
					{
						if (m == checkM)
						{
							mapVisited.Pop();
							checkM.CancelVisit();

							if (mapVisited.Count > 0) beforeCube = mapVisited.Peek();
							else beforeCube = m;

							curCount--;
						}
					}
				}
				else
				{
					if (beforeCube != null)
					{
						mapVisited.Push(beforeCube);
						beforeCube.SetVisit();
					}
					beforeCube = m;
					curCount++;
				}
			}
			else
			{
				curCount++;
			}
		}
	}
}
