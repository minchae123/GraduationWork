using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class MapRotate : MonoBehaviour
{
	private bool isRotating = false;

	public void Rotate(Direction dir)
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
						targetRotate.y = curRotate.y + 90;
					}
					break;
				case Direction.Right:
					{
						targetRotate.y = curRotate.y - 90;
					}
					break;
				case Direction.Up:
					{
						if (Mathf.Abs(curRotate.y) % 180 == 0)
						{
							targetRotate.x = curRotate.x + 90;
						}
						else
						{
							targetRotate.z = curRotate.z - 90;
						}
					}
					break;
				case Direction.Down:
					{
						if (Mathf.Abs(curRotate.y) % 180 == 0)
						{
							targetRotate.x = curRotate.x - 90;
						}
						else
						{
							targetRotate.z = curRotate.z + 90;
						}
					}
					break;
			}
			transform.DORotate(targetRotate, 0.7f).SetEase(Ease.OutBounce).OnComplete(() => isRotating = false);
		}
		else
			return;


	}
}
