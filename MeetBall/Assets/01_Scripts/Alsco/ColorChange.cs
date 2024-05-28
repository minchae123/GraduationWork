using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour, Item
{
	[SerializeField] private OriginColorEnum colorToChange;
	[Header("=======================")]

	[SerializeField] private GameObject[] visuals;

	public void Init() {	}

	private void Awake()
	{
		visuals[(int)colorToChange - 1].SetActive(true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Movement>(out Movement m))
		{
			m.SetPlayerColor(colorToChange);
			Destroy(gameObject);
		}
	}
}
