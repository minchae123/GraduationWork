using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class MapRotate : MonoBehaviour
{
	private bool isRotating = false;

	public void Rotate(Direction dir, int x, int y)
	{
		Vector3 curRotate = transform.rotation.eulerAngles;
		Vector3 targetRotate = curRotate;
		if (!isRotating)
		{
			isRotating = true;
			switch (dir)
			{
				case Direction.Left:
					{
					}
					break;
				case Direction.Right:
					{
					}
					break;
				case Direction.Up:
					{
					}
					break;
				case Direction.Down:
					{
					}
					break;
			}
			transform.DORotate(targetRotate, 0.7f).SetEase(Ease.OutBounce).OnComplete(() => isRotating = false);
		}
		else
			return;


	}
}
