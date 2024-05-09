using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
	public List<Transform> transforms = new List<Transform>();
	public CinemachineVirtualCamera cinemachineCam;

	public int left, up;

	public Direction _dir;

	private void Start()
	{
		cinemachineCam.LookAt = gameObject.transform;
	}

	public void MoveCam(Direction dir)
	{
		cinemachineCam.LookAt = transform;
		FindMovement();

		switch (dir)
		{
			case Direction.Left:
				{
					left++;
					int i = left % 4;
					if (i == 0) i = 4;
					cinemachineCam.transform.DOMove(transforms[i - 1].position, .5f);
				}
				break;
			case Direction.Up:
				{
					cinemachineCam.transform.DOMove(transforms[4].position, .5f);
					cinemachineCam.LookAt = null;

					cinemachineCam.transform.Rotate(new Vector3(90, 0 ,0));
				}
				break;
			case Direction.Right:
				{
					left--;
					if (left == -1) left = 3;
					int i = left % 4;
					if (i == 0) i = 4;
					cinemachineCam.transform.DOMove(transforms[i - 1].position, .5f);
				}
				break;
			case Direction.Down:
				{
					cinemachineCam.transform.DOMove(transforms[5].position, .5f);

					cinemachineCam.LookAt = null;
					cinemachineCam.transform.Rotate(new Vector3(-90, 0, 0));
				}
				break;
		}
		_dir = dir;
	}

	private void FindMovement()
	{
		Movement[] movement = FindObjectsOfType<Movement>();

		foreach (Movement m in movement)
			m.RayCheck();
	}
}