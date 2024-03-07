using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftWire : MonoBehaviour
{
	[SerializeField] private RectTransform wireBody;
	[SerializeField] private float offset = 15f;

	[SerializeField] private Canvas canvas;

	private LeftWire selectWire;

	private Camera cam;

	private void Awake()
	{
		cam = Camera.main;
	}



	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right, 1f);

			if(hit.collider != null)
			{
				print("1");
				var left = hit.collider.GetComponent<LeftWire>();

				if(left != null)
				{
					selectWire = left;
				}	
			}
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(selectWire != null)
			{
				wireBody.localRotation = Quaternion.Euler(Vector3.zero);
				wireBody.sizeDelta = new Vector2(0, wireBody.sizeDelta.y);
				selectWire = null;
			}
		}

		if(selectWire != null)
		{
			float angle = Vector2.SignedAngle(transform.position + Vector3.right -transform.position, Input.mousePosition- transform.position);
			float dis = Vector2.Distance(wireBody.transform.position, Input.mousePosition) - offset;

			wireBody.localRotation = Quaternion.Euler(new Vector3(0,0, angle));
			wireBody.sizeDelta = new Vector2(dis *(1 / canvas.transform.localScale.x), wireBody.sizeDelta.y	);
		}	
	}
}
