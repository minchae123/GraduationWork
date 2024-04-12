using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{
	private Camera cam;

	[SerializeField] private float rotateSpeed = 500f;
	[SerializeField] private float zoomScale = 10f;
	[SerializeField] private float minZoom = 0.1f;
	[SerializeField] private float maxZoom = 10f;

	private Vector3 mouseWorldPosStart;
	private Quaternion orginRotate;

	[SerializeField] private bool isVertical;

	[SerializeField] private GameObject otherCam;

	private void Awake()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		if (Input.GetMouseButton(1))
		{
			CameraOrbit();
		}
		else if(Input.GetMouseButtonUp(1))
		{
			transform.rotation = orginRotate;
		}

		if(Input.GetKey(KeyCode.LeftShift) && isVertical == false)
		{
			Vector3 rot = transform.rotation.eulerAngles;
			if(Input.GetKeyDown(KeyCode.A))
			{
				rot.x += 90;
				transform.DORotate(rot, 0.5f);
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				rot.x -= 90;
				transform.DORotate(rot, 0.5f);
			}
		}

		if (Input.GetKey(KeyCode.LeftShift) && isVertical)
		{
			Vector3 rot = transform.rotation.eulerAngles;
			if (Input.GetKeyDown(KeyCode.W))
			{
				rot.y += 90;
				transform.DORotate(rot, 0.5f);
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				rot.y -= 90;
				transform.DORotate(rot, 0.5f);
			}
		}

		CameraZoom(Input.GetAxis("Mouse ScrollWheel"));
	}

	public void CameraOrbit()
	{
		if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
		{
			orginRotate = cam.transform.localRotation;
			float vertical = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
			float horizontal = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
			transform.Rotate(Vector3.left, vertical);
			transform.Rotate(Vector3.down, horizontal, Space.World);
		}
	}

	public void CameraZoom(float scale)
	{
		if(scale != 0)
		{
			mouseWorldPosStart = cam.ScreenToWorldPoint(Input.mousePosition);
			cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scale * zoomScale, minZoom, maxZoom);
			Vector3 mouseDiff = mouseWorldPosStart - cam.ScreenToWorldPoint(Input.mousePosition);
			transform.position += mouseDiff;
		}
	}
}
