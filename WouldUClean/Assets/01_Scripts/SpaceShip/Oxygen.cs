using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
	[SerializeField] private GameObject children;

	private void Awake()
	{
		
	}

	private void Start()
	{
		SetActive(false);
	}

	public void SetActive(bool value)
	{
		children.gameObject.SetActive(value);
	}
}
