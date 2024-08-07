using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
	[SerializeField] private HintNote note;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PuzzleManager.Instance.GetHint(note);
		Destroy(gameObject);
	}
}
