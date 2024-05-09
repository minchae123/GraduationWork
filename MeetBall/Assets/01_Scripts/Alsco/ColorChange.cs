using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
	[SerializeField] private Color changeColor;
	private MeshRenderer render;

    private void Start()
    {
		render = GetComponent<MeshRenderer>();
		render.sharedMaterial.SetColor("_PlayerColor", changeColor);
    }
    private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			other.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_PlayerColor", changeColor);
			Destroy(gameObject);
		}
	}
}
