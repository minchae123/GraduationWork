using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchC : MonoBehaviour
{
	public MOVE btn;

	private void OnMouseDown()
	{
		print("as");
		switch (btn)
		{
			case MOVE.left:
				{
					print("l");
					PlayerManager.Instance.MoveLeft();
				}
				break;
			case MOVE.right:
				{
					print("r");
					PlayerManager.Instance.MoveRight();
				}
				break;
			case MOVE.up:
				{
					print("u");
					PlayerManager.Instance.MoveUp();
				}
				break;
			case MOVE.down:
				{
					print("d");
					PlayerManager.Instance.MoveDown();
				}
				break;
			case MOVE.change:
				{
					print("c");
					PlayerManager.Instance.ChangeMovePlayer();
				}
				break;
		}
	}

	void Update()
	{
		/*if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			print("as");
			if (Physics.Raycast(ray, out hit))
			{
				switch (btn)
				{
					case MOVE.left:
						{
							print("l");
							PlayerManager.Instance.MoveLeft();
						}
						break;
					case MOVE.right:
						{
							print("r");
							PlayerManager.Instance.MoveRight();
						}
						break;
					case MOVE.up:
						{
							print("u");
							PlayerManager.Instance.MoveUp();
						}
						break;
					case MOVE.down:
						{
							print("d");
							PlayerManager.Instance.MoveDown();
						}
						break;
					case MOVE.change:
						{
							print("c");
							PlayerManager.Instance.ChangeMovePlayer();
						}
						break;
				}
				Debug.Log(hit.transform.gameObject);
			}
		}*/
	}
}

