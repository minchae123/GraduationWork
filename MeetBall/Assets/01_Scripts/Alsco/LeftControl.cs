using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct WASD
{
	public Vector3 w;
	public Vector3 s;
	public Vector3 a;
	public Vector3 d;
}

public class LeftControl : MonoBehaviour
{
	[SerializeField] private GameObject target;

	private WASD WASD;

	void Start()
	{
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			transform.position += WASD.w;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			transform.position += WASD.s;
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			transform.position += WASD.d;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			transform.position += WASD.a;
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			transform.position += Vector3.up;
		}
		if(Input.GetKeyDown (KeyCode.LeftShift))
		{
			transform.position += Vector3.down;
		}
	}

	public void Move(DIRECTION dir)
	{
		switch (dir)
		{
			case DIRECTION.East:
				{
					WASD.w = -Vector3.right;
					WASD.s = Vector3.right;
					WASD.a = -Vector3.forward;
					WASD.d = Vector3.forward;
				}
				break;
			case DIRECTION.West:
				{
					WASD.w = Vector3.right;
					WASD.s = -Vector3.right;
					WASD.a = Vector3.forward;
					WASD.d = -Vector3.forward;
				}
				break;
			case DIRECTION.South:
				{
					WASD.w = Vector3.forward;
					WASD.s = -Vector3.forward;
					WASD.a = -Vector3.right;
					WASD.d = Vector3.right;
				}
				break;
			case DIRECTION.North:
				{
					WASD.w = -Vector3.forward;
					WASD.s = Vector3.forward;
					WASD.a = Vector3.right;
					WASD.d = -Vector3.right;
				}
				break;
		}
	}
}
