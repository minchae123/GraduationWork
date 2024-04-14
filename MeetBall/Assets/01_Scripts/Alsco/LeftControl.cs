using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftControl : MonoBehaviour
{
	private Vector3 targetPosition; // �÷��̾ �̵��� ��ǥ ��ġ
	[SerializeField] private GameObject target;

	void Start()
	{
		targetPosition = transform.position; // �ʱ� ��ġ�� ��ǥ ��ġ�� ����
	}

	void Update()
	{
		// �÷��̾� �̵�
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
