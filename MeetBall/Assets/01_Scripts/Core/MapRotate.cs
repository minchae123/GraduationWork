using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class MapRotate : MonoBehaviour
{
	private Vector2 delta;

	private bool isRoatating;
	private bool isBusy;

	[SerializeField] private float moveSpeed = 10;
	[SerializeField] private float rotationSpeed = 10;

	[SerializeField] private float xRotation;


	[SerializeField] private List<float> rotates = new List<float>();

	private void Awake()
	{
	}

	public void OnLook(InputAction.CallbackContext context)
	{
		delta = context.ReadValue<Vector2>(); // ���콺 ��ġ �޾ƿ���
	}

	public void OnRatate(InputAction.CallbackContext context)
	{
		isRoatating = context.started || context.performed;

		if (context.canceled) // ���� ����
		{
			isBusy = true;
			SnapRotation();
		}
	}

	private void Update()
	{
		/*if(Input.GetKey(KeyCode.CapsLock))
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
		}*/

		if (isRoatating)
		{
			transform.Rotate(new Vector3(xRotation, -delta.x * rotationSpeed, 0));
			transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, 0);
		}
	}

	private void SnapRotation()
	{
		transform.DORotate(SnapVector(), 0.5f).SetEase(Ease.OutBounce).OnComplete(() => isBusy = false); // isbusy�϶� �������̰�
	}

	private Vector3 SnapVector()
	{
		float curY = Mathf.Ceil(transform.rotation.eulerAngles.y); // �Ҽ� ����

		float endValue = curY switch
		{
			>= 0 and <= 90 => rotates[0], // 0 ~ 90 ���̴�
			>= 91 and <= 180 => rotates[1],
			>= 181 and <= 270 => rotates[2],
			_ => rotates[3]// �� �ƴϸ� 
		};

		return new Vector3(xRotation, endValue, 0);
	}
}
