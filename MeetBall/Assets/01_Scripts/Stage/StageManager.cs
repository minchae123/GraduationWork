using DG.Tweening;
using echo17.EndlessBook;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class StageManager : MonoSingleton<StageManager>
{
	private bool isInStage = false;
	private bool isReStart = false;

	public bool IsInStage => isInStage;
	public bool IsReStart => isReStart;

	[Header("===============")]
	[Header("Stage")]
	public Transform StageTrm;
	[SerializeField] private StageListSO stageList;

	private StageSO currentStageSO;
	public StageSO CurrentStageSO => currentStageSO;

	private GameObject curStageGameObject;

	[Header("===============")]
	[Header("Clear")]
	private Animator ClearAnim;
	private TextMeshProUGUI clearText;
	private ParticleSystem clearParticle;
	private GimmickExplain gimmick;


	[Header("===============")]
	[Header("UI")]
	//[SerializeField] private Transform stageSelectTrm;
	//[SerializeField] private StageUI stageUIPrefab;
	//private Transform stageSelectUITrm;

	private int selectStageNum = 0;
	private StageUI[] stagesUI;
	private float moveX = -500f;

	[Header("Minimap")]
	[SerializeField] private Transform minimapTrm;
	private GameObject currentMinimap = null;


	[Header("===============")]
	[Header("ETC")]
	[SerializeField] private GameObject gameCanvas;
	private Tutorial tutorialPanel;
	private EventSystem eventSystem;

	[SerializeField] private EndlessBook endlessBook;

	public void Awake()
	{
		clearParticle = GameObject.Find("ClearParticle").GetComponent<ParticleSystem>();
		ClearAnim = GameObject.Find("ClearUIAnim").GetComponent<Animator>();
		//stageSelectUITrm = stageSelectTrm.Find("StageSelect");
		gimmick = FindFirstObjectByType<GimmickExplain>();
		eventSystem = EventSystem.current;

		clearText = ClearAnim.transform.Find("ClearText").GetComponent<TextMeshProUGUI>();
		//tutorialPanel = gameCanvas.transform.GetComponentInChildren<Tutorial>();

		isInStage = false;

		//fadeCg.alpha = 0;
	}

	private void Start()
	{
		//SetSelectStageUI();
		//gameCanvas.SetActive(false);

		//tutorialPanel.gameObject.SetActive(false);
		//StartCoroutine(StageLoad());
	}

	public void CheckPaint()
	{
		GameManager.Instance.gameData.bigStage.TryGetValue(currentStageSO.bigStageName, out var keys);

		for (int i = 0; i < keys.Count; i++)
		{
			if (keys[i]) ; //EndlessBook Mat Change
		}
	}

	public void EnterStage()
	{
		if (IsInStage) return;
		/*if (!stagesUI[selectStageNum].CheckCanPlay())
		{
			print("아직 클리어X");
		}
		else
		{
			LoadStage();
		}*/
	}

	/*public void MoveStage(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                UpdateSelectStageUI(-1);
                break;
            case Direction.Right:
                UpdateSelectStageUI(1);
                break;
        }
    }*/

	private void Update()
	{
		if (!isInStage) // 스테이지 밖일 때
		{
			//if (Input.GetKeyDown(KeyCode.A))
			//{
			//    UpdateSelectStageUI(-1);
			//}
			//if (Input.GetKeyDown(KeyCode.D))
			//{
			//    UpdateSelectStageUI(1);
			//}
		}
		else // 스테이지 안일때
		{
			if (Input.GetKey(KeyCode.Tab) && Input.GetKeyDown(KeyCode.CapsLock))
			{
				isReStart = true;

				StopAllCoroutines();
				ClearStage(true);
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				ReStartBtn();
			}

			if (GameManager.Instance.Game.activeSelf && Input.GetKeyDown(KeyCode.Escape))
			{
				BackToMenu();
			}
		}
	}

	private IEnumerator WaitForGenerate()
	{
		yield return new WaitForSeconds(.5f);

		isInStage = true;
	}

	private IEnumerator WaitTouch()
	{
		yield return new WaitForSeconds(1f);
		print("true");
		eventSystem.enabled = true;
	}

	public void SetStageNumber(int num)
	{
		selectStageNum = num;
		LoadStage();
	}

	public bool IsClear(int num)
	{
		for (int i = 0; i < num; i++)
		{
			if (!stageList.Stages[i].IsClear)
			{
				return false;
			}
		}
		return true;
	}


	public void LoadStage()
	{
		GameManager.Instance.SetActive(true,false);
		StartCoroutine(FindBox());

		if (selectStageNum <= stageList.Stages.Count)
		{
			DestroyImmediate(curStageGameObject);
			PlayerManager.Instance.ResetPlayers();

			currentStageSO = stageList.Stages[selectStageNum]; // 현재 스테이지

			curStageGameObject = Instantiate(currentStageSO.stagePref, StageTrm); // 스테이지 생성

			//stageSelectTrm.gameObject.SetActive(false);
			gameCanvas.SetActive(true);
			gameCanvas.GetComponentInChildren<DescriptionPanel>().SetPanel(currentStageSO);

			PlayerManager.Instance.SetNewPlayers(currentStageSO);
			CameraMovement.Instance.FindItems();
			//eventSystem.enabled = false;

			StartCoroutine(gimmick.StartTutorial());
			StartCoroutine(WaitForGenerate());
			//StartCoroutine(WaitTouch());

			if (selectStageNum == 0)
			{
				//StartCoroutine(tutorialPanel.TutorialPannel());
			}
		}
		else
		{
			SceneManager.LoadScene(2);
			print("준비된 스테이지가 아닙니다람쥐");
		}
	}

	private IEnumerator Painting(string stageName, int stageIndex, bool IsLast)
	{
		yield return new WaitForSeconds(6);
		endlessBook.ChangePaint(stageName, stageIndex);

		//if(IsLast) // 마지막 스테이지 클리어시
		//{
		//    yield return new WaitForSeconds(1.5f);
		//    fadeCg.DOFade(1, 1f);
		//
		//    TextMeshProUGUI text = fadeCg.transform.GetComponentInChildren<TextMeshProUGUI>();
		//    text.text = string.Empty;
		//
		//    yield return new WaitForSeconds(1.0f);
		//
		//    text.text = "테스트 텍스트";
		//    DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, 1.5f).OnComplete(() => fadeCg.DOFade(0f, 1.4f));
		//}
	}

	public void ClearStage(bool isClear)
	{
		if (isClear)
		{
			currentStageSO.IsClear = true;

			if (GameManager.Instance.gameData.bigStage.TryGetValue(currentStageSO.bigStageName, out var keys))
			{
				keys[selectStageNum] = true;

				print(currentStageSO.bigStageName);

				StartCoroutine(Painting(currentStageSO.bigStageName, selectStageNum, keys.Count - 1 == selectStageNum));
				StartCoroutine(Painting(currentStageSO.bigStageName, selectStageNum, keys.Count - 1 == selectStageNum));
				print(keys.Count);
				print(selectStageNum);

				//print(keys[selectStageNum]);
			}
			else
			{
				print("실패");
			}

			isReStart = false;
			selectStageNum++;

			clearText.text = "Stage Clear!";
		}
		else
		{
			clearText.text = "Stage Fail. . .";
		}

		print("Clear");
		isInStage = false;


		ClearAnim.SetTrigger("Clear");
		BoxManager.Instance.CleanBox();

		gameCanvas.SetActive(false);
		Invoke(nameof(ClearParticle), 1);
		StartCoroutine(StageLoad());
		Invoke(nameof(openbook), 3f);
	}

	private void openbook()
	{
		GameManager.Instance.OpenBook();
	}
	private IEnumerator StageClearBackToMenu(float time)
	{
		yield return new WaitForSeconds(time);
		//BackToMenu();
	}

	private void ClearParticle()
	{
		clearParticle.Play();
	}

	private IEnumerator StageLoad()
	{
		yield return new WaitForSeconds(1f);
		DestroyImmediate(curStageGameObject);
		PlayerManager.Instance.ResetPlayers();
		//print("Destroy");
		yield return null;

		//LoadStage();
	}

	IEnumerator FindBox()
	{
		yield return new WaitForSeconds(0.2f);
		BoxManager.Instance.FindBox();
	}

	public void SetIsInStage(bool value)
	{
		isInStage = value;
	}

	public void ReStartBtn()
	{
		isReStart = true;

		DestroyImmediate(curStageGameObject);

		LoadStage();
	}

	public void BackToMenu()
	{
		isInStage = false;

		if (selectStageNum == 0)
		{
			Cursor.lockState = CursorLockMode.None;

			StopAllCoroutines();
		}

		openbook();
		BoxManager.Instance.CleanBox();

		gimmick.panel.ClearSequence();
		gimmick.panel.CloseTutorial();

		//CameraMovement.Instance.CameraReset();
	}

	private IEnumerator MenuCoroutine()
	{
		yield return new WaitForSeconds(2);
		DestroyImmediate(curStageGameObject);
		gameCanvas.SetActive(false);
		PlayerManager.Instance.ResetPlayers();
	}
}
