using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
	public void Rotate(Direction dir)
	{
		switch (dir)
		{
			case Direction.Left:
				//transform.DORotate(transform.localRotation.y, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => isBusy = false); // isbusy일땐 못움직이게
				break;
			case Direction.Up:
				break;
			case Direction.Right:
				break;
			case Direction.Down:
				break;
		}
	}
}
