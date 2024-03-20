using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HintManager : MonoSingleton<HintManager>
{
	[SerializeField] private GameObject curHintInfo;
	[SerializeField] private Image infoImage;

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

	public void CurrentHintSetting(HintNote info)
	{
		infoImage.sprite = info.hintSprite;
	}

	public void ShowHintPanel()
	{
		curHintInfo.transform.DOScale(Vector3.one, 1);
		isShow = true;
	}

	public void CloseHintPanel()
	{
		curHintInfo.transform.DOScale(Vector3.zero, 1);
		isShow = false;
	}
}
