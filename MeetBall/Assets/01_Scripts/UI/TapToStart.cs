using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class TapToStart : MonoBehaviour
{
	private TextMeshProUGUI text;

	private void Start()
	{
		text = GetComponent<TextMeshProUGUI>();

		AlphaRepeat();
	}

	private void Update()
	{
		if (Input.anyKey)
		{
			print("Start");
			GameStart();
		}
	}

	public void GameStart()
	{
		SceneManager.LoadScene(1);
	}

	public void AlphaRepeat()
	{
		text.DOFade(0.2f, 0.6f).SetLoops(-1, LoopType.Yoyo);
	}
}
