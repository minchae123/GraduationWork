using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePuzzle : MonoBehaviour
{
	private PiecePuzzle puzzle;

	public GameObject correctForm;
	private Camera cam;

	private Vector2 startPos;
	private Vector2 resetPos;
	private bool isMove;

	private void Start()
	{
		cam = GameManager.Instance.mainCam;
		puzzle = FindObjectOfType<PiecePuzzle>();
		resetPos = transform.position;
	}

	private void Update()
	{
		if(isMove)
		{
			Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

			transform.localPosition = new Vector2(mousePos.x - startPos.x, mousePos.y - startPos.y);
		}
	}

	private void OnMouseDown()
	{
		if (Input.GetMouseButtonDown(0))
		{
			print("as");
			Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);

			startPos = new Vector2(mouse.x - transform.localPosition.x, mouse.y - transform.localPosition.y);
			isMove = true;
		}
	}

	private void OnMouseUp()
	{
		isMove = false;

		if(Mathf.Abs(Vector2.Distance(transform.position, correctForm.transform.position)) < 0.5f )
		{
			transform.position = correctForm.transform.position;
			puzzle.CorrectPuzzle(this, true);
		}
		else
		{ 
			transform.position = resetPos;
		}
	}
}
