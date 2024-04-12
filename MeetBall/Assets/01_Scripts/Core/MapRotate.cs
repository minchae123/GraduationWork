using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapRotate : MonoBehaviour
{
	private void Update()
	{
		if(Input.GetKey(KeyCode.LeftShift))
		{
			Vector3 cur = transform.rotation.eulerAngles;
			if (Input.GetKeyDown(KeyCode.W))
			{
				cur.x += -90;
				transform.DORotate(cur, 0.5f);
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				cur.y += -90;
				transform.DORotate(cur, 0.5f);
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				cur.x += 90;
				transform.DORotate(cur, 0.5f);
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				cur.y += 90;
				transform.DORotate(cur, 0.5f);
			}
		}
	}
}
