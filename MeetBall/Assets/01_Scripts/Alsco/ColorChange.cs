using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoiorChangeEnum
{
	NONE = 0,
	RED = 1,
	GREEN = 2,
	BLUE = 3,
}
public class ColorChange : MonoBehaviour
{
	[SerializeField] private CoiorChangeEnum colorToChange;
	private Color changeColor;
	private MeshRenderer render;

	private GameObject[] visuals;

	private void Awake()
	{
		render = GetComponent<MeshRenderer>();
		visuals = GetComponentsInChildren<GameObject>();

		foreach (GameObject v in visuals)
		{
			v.SetActive(false);
		}
	}
	private void Start()
	{
		switch (colorToChange)
		{
			case CoiorChangeEnum.NONE:
				break;
			case CoiorChangeEnum.RED:
				changeColor = Color.red;
				break;
			case CoiorChangeEnum.GREEN:
				changeColor = Color.green;
				break;
			case CoiorChangeEnum.BLUE:
				changeColor = Color.blue;
				break;
		}

		visuals[(int)colorToChange - 1].SetActive(true);
		render.sharedMaterial.SetColor("_PlayerColor", changeColor);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_PlayerColor", changeColor);
			Destroy(gameObject);
		}
	}
}
