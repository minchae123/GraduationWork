using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public enum WireType
{
	A, B, C, D
}

public class Wire : MonoBehaviour
{
	public bool[] isColor = new bool[4];
	public WireType wireType;

	public Vector2 clickPos;
	public LineRenderer line;
	public LineRenderer[] lines;

	public bool isDrag;

	public Image panelObject;


	private void Update()
	{
		if (isDrag)
		{
			line.SetPosition(1, new Vector3(Input.mousePosition.x - clickPos.x, Input.mousePosition.y - clickPos.y, -10));

			if (Input.GetMouseButtonUp(0))
			{
				line.SetPosition(1, new Vector3(0, 0, -10));
				isDrag = false;
			}
		}
	}

	public void CheckAnswer()
	{
		if (isColor[0] && isColor[1] && isColor[2] && isColor[3])
		{
			panelObject.gameObject.SetActive(false);
			print("success");
		}
		else
		{
			FailGame();
			print("fail");
		}
	}

	public void ClickLine(LineRenderer click)
	{
		clickPos = Input.mousePosition;
		line = click;
		wireType = line.GetComponentInChildren<LeftWire>().WireType;

		isDrag = true;
	}

	public void ResetLine()
	{
		foreach (var i in lines)
		{
			i.SetPosition(1, Vector3.zero);
		}
	}

	public void FailGame()
	{
		ResetLine();
		panelObject.color = Color.red;
		panelObject.transform.DOShakePosition(0.6f, 1.5f);
		panelObject.color = new Color(1, 1, 1, 0.4f);
	}
}
