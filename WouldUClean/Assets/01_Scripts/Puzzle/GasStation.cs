
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStation : MonoBehaviour
{
	public GameObject puzzle;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
		{
			puzzle.SetActive(true);
			print("hi");
		}
	}
}
