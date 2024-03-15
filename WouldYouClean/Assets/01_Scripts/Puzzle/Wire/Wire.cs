using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

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
			print("success");
		}
		else
		{
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
		foreach(var i in lines)
		{
			i.SetPosition(1, Vector3.zero);
		}
	}
}
