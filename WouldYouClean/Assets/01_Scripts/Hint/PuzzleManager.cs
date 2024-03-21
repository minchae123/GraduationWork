using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PuzzleManager : MonoSingleton<PuzzleManager>
{
	[SerializeField] private GameObject curHintInfo;
	[SerializeField] private Image infoImage;
	[SerializeField] private GameObject infoGet;

	private bool isShow = false;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.H))
		{
			if(isShow == false)
				ShowHintPanel();
			else
				CloseHintPanel();
		}
	}

	public void ShowHintPanel()
	{
		curHintInfo.transform.DOScale(Vector3.one, 1);
		isShow = true;
	}

	public void CloseHintPanel()
	{
		curHintInfo.transform.DOScale(Vector3.zero, 0.5f);
		isShow = false;
	}

	public void GetHint(HintNote info)
	{
		infoImage.sprite = info.hintSprite;

		Sequence seq = DOTween.Sequence();
		seq.Append(infoGet.transform.DOLocalMoveY(-470, 1));
		seq.AppendInterval(.4f);
		seq.Append(infoGet.transform.DOLocalMoveY(-650, 1.5f));
	}
}
