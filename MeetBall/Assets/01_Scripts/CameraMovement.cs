using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
	public List<Transform> transforms = new List<Transform>();
	public CinemachineVirtualCamera[] cinemachineCam;

	private CinemachineVirtualCamera previousCam;
	public Transform curTransfrom;

	public int left, up;

	public Direction _dir;

	private void Start()
	{
		previousCam = cinemachineCam[3];
	}

	public void ChangeCamera(Direction dir)
	{
		FindMovement();

		foreach(var c in cinemachineCam)
		{
			if(c.Priority == 10)
				previousCam = c;
		}

		switch (dir)
		{
			case Direction.Left:
				{
					left++;
					int i = left % 4;
					if (i == 0) i = 4;
					curTransfrom = cinemachineCam[i - 1].transform;
					cinemachineCam[i - 1].Priority = 10;
				}
				break;
			case Direction.Right:
				{
					left--;
					if (left == -1) left = 3;
					int i = left % 4;
					if (i == 0) i = 4;
					curTransfrom = cinemachineCam[i - 1].transform;
					cinemachineCam[i - 1].Priority = 10;
				}
				break;
			case Direction.Up:
				{
					curTransfrom = cinemachineCam[4].transform;
					cinemachineCam[4].Priority = 10;
				}
				break;
			case Direction.Down:
				{
					curTransfrom = cinemachineCam[5].transform;
					cinemachineCam[5].Priority = 10;
				}
				break;
		}

		previousCam.Priority = 5;
		_dir = dir;
	}

	private void FindMovement()
	{
		Movement[] movement = FindObjectsOfType<Movement>();

		foreach (Movement m in movement)
			m.RayCheck();
	}
}