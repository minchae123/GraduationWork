using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleScene : MonoBehaviour
{
    public Transform back;

	private void Start()
	{
		Rotate();
	}

	public void Rotate()
    {
		print(1);
        back.DORotate(new Vector3(0, 0, 360), 4.5f, RotateMode.FastBeyond360)
					 .SetEase(Ease.Linear)
					 .SetLoops(-1);
	}
}
