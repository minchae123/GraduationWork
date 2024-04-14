using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftControl : MonoBehaviour
{
	private Vector3 targetPosition; // 플레이어가 이동할 목표 위치
	[SerializeField] private GameObject target;

	void Start()
	{
		targetPosition = transform.position; // 초기 위치를 목표 위치로 설정
	}

	void Update()
	{
		// 플레이어 이동
		if (Input.GetKeyDown(KeyCode.W))
		{
			targetPosition += target.transform.forward;
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			targetPosition -= target.transform.forward;
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			targetPosition -= target.transform.right;
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			targetPosition += target.transform.right;
		}
		else if (Input.GetKeyDown(KeyCode.Space))
		{
			targetPosition += target.transform.up;
		}
		else if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			targetPosition -= target.transform.up;
		}

		transform.position = targetPosition;
	}
}
