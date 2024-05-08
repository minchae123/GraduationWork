using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public enum DIRECTION
{
	East, West, South, North
}


public class CameraManager : MonoBehaviour
{
	public static CameraManager Instance;

	public DIRECTION direction;

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

	[SerializeField] private List<float> rotates = new List<float>();

	[SerializeField] private LeftControl leftControl;
	[SerializeField] private RightControl rightControl;

	[SerializeField] private Movement move;

	private void Awake()
	{
		Instance = this;

		xRotation = transform.rotation.eulerAngles.x;
		cam = Camera.main;
	}

	private void Start()
	{
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

		if (context.canceled) // 손을 떼면
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

	public void NewControl()
	{
		leftControl = FindObjectOfType<LeftControl>();
		rightControl = FindObjectOfType<RightControl>();
	}

	private void SnapRotation()
	{
		transform.DORotate(SnapVector(), 0.5f).SetEase(Ease.OutBounce).OnComplete(() => isBusy = false); // isbusy일땐 못움직이게
	}

	private Vector3 SnapVector()
	{
		float curY = Mathf.Ceil(transform.rotation.eulerAngles.y); // 소수 내림

		float endValue = curY switch
		{
			>= 0 and <= 90 => rotates[0], // 0 ~ 90 사이는
			>= 91 and <= 180 => rotates[1],
			>= 181 and <= 270 => rotates[2],
			_ => rotates[3]// 다 아니면 
		};

		direction = curY switch // 방향 상태 정해주기
		{
			>= 0 and <= 90 => DIRECTION.South,
			>= 91 and <= 180 => DIRECTION.West,
			>= 181 and <= 270 => DIRECTION.North,
			_ => DIRECTION.East
		};

		leftControl?.Move(direction);
		rightControl?.Move(direction);

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
