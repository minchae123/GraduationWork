using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
	private Vector2 delta;

	private bool isMoving;
	private bool isRoatating;
	private bool isBusy;

	[SerializeField] private float moveSpeed = 10;
	[SerializeField] private float rotationSpeed = 10;

	[SerializeField] private float zoomScale = 10f;
	[SerializeField] private float minZoom = 0.1f;
	[SerializeField] private float maxZoom = 10f;

	private float xRotation;
	private Camera cam;

	private void Awake()
	{
		xRotation = transform.rotation.eulerAngles.x;
		cam = Camera.main;
	}

	public void OnLook(InputAction.CallbackContext context)
	{
		delta = context.ReadValue<Vector2>(); // 마우스 위치 받아오기
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		if (isBusy) return;

		isMoving = context.started || context.performed;

		if (context.canceled)
		{
			isBusy = true;
			SnapRotation();
		}
	}

	public void OnRatate(InputAction.CallbackContext context)
	{
		isRoatating = context.started || context.performed;

		if (context.canceled)
		{
			isBusy = true;
			SnapRotation();
		}
	}

	private void LateUpdate()
	{
		/*if (isMoving) 움직임
        {
            var pos = transform.right * (delta.x * -moveSpeed);
            pos += transform.up * (delta.y * -moveSpeed);
            transform.position += pos * Time.deltaTime;
        }*/

		if (isRoatating)
		{
			transform.Rotate(new Vector3(xRotation, delta.x * rotationSpeed, 0));
			transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, 0);
		}

		CameraZoom(Input.GetAxis("Mouse ScrollWheel"));
	}

	private void SnapRotation()
	{
		transform.DORotate(SnapVector(), 0.5f).SetEase(Ease.OutBounce).OnComplete(() => isBusy = false);
	}

	private Vector3 SnapVector()
	{
		float curY = Mathf.Ceil(transform.rotation.eulerAngles.y);

		float endValue = curY switch
		{
			>= 0 and <= 90 => 45f, // 0 ~ 90 사이는 45 
			>= 91 and <= 180 => 135f,
			>= 181 and <= 270 => 225f,
			_ => 315f // 다 아니면 315
		};

		return new Vector3(xRotation, endValue, 0);
	}

	public void CameraZoom(float scale)
	{
		if (scale != 0)
		{
			cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scale * zoomScale, minZoom, maxZoom);
		}
	}
}
