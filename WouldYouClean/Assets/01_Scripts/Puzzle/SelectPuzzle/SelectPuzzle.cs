using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPuzzle : MonoBehaviour
{
	public Button[] btns;
	public void Answer()
	{
		print("정답 !");
	}

	public void WrongAnswer()
	{
		print("오답");
		StartCoroutine(WrongPenalty());
	}

	IEnumerator WrongPenalty()
	{
		foreach(var p in btns)
		{
			p.interactable = false;
		}

		yield return new WaitForSeconds(30f);

		foreach (var p in btns)
		{
			p.interactable = true;
		}
	}
}
