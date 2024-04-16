using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private Material closeDoorMat;
	[SerializeField] private Material openDoorMat;

	private MeshRenderer mr;

	private void Awake()
	{
		mr = GetComponent<MeshRenderer>();
	}

	public void Open()
	{
		mr.material = openDoorMat;
		gameObject.layer = 6;
		print("open");
	}
	public void Close()
	{
		mr.material = closeDoorMat;
		gameObject.layer = 0;
		print("close");
	}
}