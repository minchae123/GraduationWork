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
	private WASD WASD;

	private RaycastHit hit;
	private Ray[] ray = new Ray[6];

	private void Start()
	{
		//�����¿�յ�
		ray[0].direction = transform.up;
		ray[1].direction = -transform.up;
		ray[2].direction = -transform.right;
		ray[3].direction = transform.right;
		ray[4].direction = transform.forward;
		ray[5].direction = -transform.forward;		
	}

	void Update()
	{
		ray[0].origin = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
		ray[1].origin = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
		ray[2].origin = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);
		ray[3].origin = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
		ray[4].origin = new Vector3(transform.position.x, transform.position.y, transform.position.z + .5f);
		ray[5].origin = new Vector3(transform.position.x, transform.position.y, transform.position.z - .5f);

		for (int i = 0; i < ray.Length; i++)
		{
			Debug.DrawRay(ray[i].origin, ray[i].direction);

			if (Physics.Raycast(ray[i], out hit, 0.5f))
			{
				if (hit.collider.CompareTag("Moveable"))
				{
					Debug.DrawRay(ray[i].origin, ray[i].direction, Color.red);
					//Debug.Log(ray[i]);
				}
			}
		}

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
		if (Input.GetKeyDown(KeyCode.Space))
		{
			transform.position += Vector3.up;
		}
		if (Input.GetKeyDown(KeyCode.LeftShift))
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
