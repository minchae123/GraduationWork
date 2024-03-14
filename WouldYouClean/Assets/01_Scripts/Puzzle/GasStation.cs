
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStation : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
		{
			print("hi");
		}
	}
}
