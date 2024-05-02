using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction
{
	Left, Up, Right, Down, None
}

public class InputManage : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	Direction direction;
	Vector2 startPos, endPos;
	public float swipeThreshold = 100f;
	bool draggingStarted;

	[SerializeField] private MapRotate rotate;

	private void Awake()
	{
		draggingStarted = false;
		direction = Direction.None;
	}

	private void Start()
	{
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.M))
        {
	       FindMap();
        }
	}

	public void FindMap()
	{
		rotate = FindObjectOfType<MapRotate>();	
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		draggingStarted = true;
		startPos = eventData.pressPosition;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (draggingStarted)
		{
			endPos = eventData.position;

			Vector2 difference = endPos - startPos;

			if (difference.magnitude > swipeThreshold)
			{
				if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
				{
					direction = difference.x > 0 ? Direction.Right : Direction.Left;
				}
				else
				{
					direction = difference.y > 0 ? Direction.Up : Direction.Down;
				}
			}
			else
			{
				direction = Direction.None;
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (draggingStarted && direction != Direction.None)
		{
			//Debug.Log(direction);
			rotate.Rotate(direction);
		}

		startPos = Vector2.zero;
		endPos = Vector2.zero;
		draggingStarted = false;
	}
}
