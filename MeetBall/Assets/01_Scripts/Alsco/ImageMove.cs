using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MOVE
{
	left, right, up, down, change
}


public class ImageMove : MonoBehaviour, IPointerClickHandler
{
	public MOVE btn;

	public void OnPointerClick(PointerEventData eventData)
	{
		switch (btn)
		{
			case MOVE.left:
				{
					PlayerManager.Instance.MoveLeft();
				}
				break;
			case MOVE.right:
				{
					PlayerManager.Instance.MoveRight();
				}
				break;
			case MOVE.up:
				{
					PlayerManager.Instance.MoveUp();
				}
				break;
			case MOVE.down:
				{
					PlayerManager.Instance.MoveDown();
				}
				break;
			case MOVE.change:
				{
					PlayerManager.Instance.ChangeMovePlayer();
				}
				break;
		}
	}
}
