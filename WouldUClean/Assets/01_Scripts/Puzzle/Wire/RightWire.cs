using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightWire : MonoBehaviour, IPointerClickHandler, IDropHandler
{
	private Wire wire;

	private void Awake()
	{
		wire = FindObjectOfType<Wire>();
	}

	public WireType wireType;

	public void OnPointerClick(PointerEventData eventData)
	{
		Vector3 pos = wire.line.GetPosition(1);
		Vector3 mouse = (Vector2)Input.mousePosition - wire.clickPos;
		pos.y = mouse.y;
		wire.line.SetPosition(1, pos);
		wire.isDrag = false;
	}

	public void OnDrop(PointerEventData eventData)
	{
		Vector3 pos = wire.line.GetPosition(1);
		Vector3 mouse = (Vector2)Input.mousePosition - wire.clickPos;
		pos.y = mouse.y;
		wire.line.SetPosition(1, pos);
		wire.isDrag = false;

		WireType ltype = wire.wireType;

		if(ltype == wireType)
		{
			wire.isColor[(int)wireType] = true;
		}
	}
}