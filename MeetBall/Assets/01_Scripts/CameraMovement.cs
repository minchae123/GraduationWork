using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
	public List<Transform> transforms = new List<Transform>();

	public GameObject cinemachineCam;

	public void MoveCam(Direction dir)
	{
		switch (dir)
		{
			case Direction.Left:
				{

				}
				break;
			case Direction.Up:
				{

				}
				break;
			case Direction.Right:
				{

				}
				break;
			case Direction.Down:
				{

				}
				break;
		}
	}
}
