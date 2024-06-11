using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class TapToStart : MonoBehaviour, IPointerClickHandler
{
	[Header("TitleScene")]
	private TextMeshProUGUI text;

	private int curScene = 0;
	private bool isClicked = false;
	public bool IsClicked {  get { return isClicked; } set => isClicked = value; }

	private void Start()
	{
		curScene = SceneManager.GetActiveScene().buildIndex;
		if (curScene == 0)
        {
			TitleScene();
		}
	}

	private void Update()
	{
		if (Input.anyKey)
		{
			if(curScene == 0 && !isClicked)
            {
				isClicked = true;
				print("Start");
				GameStart();
			}
		}
	}

	public void GameStart()
	{
		SceneManager.LoadScene(1);
	}

	public void TitleScene()
	{
		text = GetComponent<TextMeshProUGUI>();
		text.DOFade(0.2f, 1).SetLoops(-1, LoopType.Yoyo);
	}

    public void OnPointerClick(PointerEventData eventData)
    {
		if (curScene != 0) // 나중에 curScene ==1 로 변경 (게임씬일때만)
		{
			StageManager.Instance.EnterStage();
		}
	}
}
