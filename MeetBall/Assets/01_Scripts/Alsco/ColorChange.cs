using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
	private Material changeMat;

	private void Awake()
	{
		changeMat = GetComponent<Renderer>().material;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			other.GetComponent<Renderer>().material = changeMat;
		}
	}
}
