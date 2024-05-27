using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StageManager : MonoSingleton<StageManager>
{
    private bool isInStage = false;
    public bool IsInStage => isInStage;

    [Header("===============")]
    [Header("Stage")]
    [SerializeField] private Transform stageTrm;
    [SerializeField] private StageListSO stageList;

    private StageSO currentStageSO;
    public StageSO CurrentStageSO => currentStageSO;

    private GameObject curStageGameObject;

    [Header("===============")]
    [Header("Clear")]
    private Animator ClearAnim;
    private ParticleSystem clearParticle;


    [Header("===============")]
    [Header("UI")]
    [SerializeField] private Transform stageSelectTrm;
    [SerializeField] private StageUI stageUIPrefab;
    private Transform stageSelectUITrm;

    private int selectStageNum = 0;
    private StageUI[] stagesUI;
    private float moveX = -500f;

    [Header("Minimap")]
    [SerializeField] private Transform minimapTrm;
    private GameObject currentMinimap = null;


    [Header("===============")]
    [Header("ETC")]
    [SerializeField] private GameObject gameCanvas;

    private void Awake()
    {
        clearParticle = GameObject.Find("ClearParticle").GetComponent<ParticleSystem>();
        ClearAnim = GameObject.Find("ClearUIAnim").GetComponent<Animator>();
        stageSelectUITrm = stageSelectTrm.Find("StageSelect");

        isInStage = false;
    }

    private void Start()
    {
        SetSelectStageUI();
        gameCanvas.SetActive(false);


        //StartCoroutine(StageLoad());
    }

    private void Update()
    {
        if (!isInStage) // 스테이지 밖일 때
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                UpdateSelectStageUI(-1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                UpdateSelectStageUI(1);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!stagesUI[selectStageNum].CheckCanPlay())
                {
                    print("아직 클리어X");
                }
                else
                {
                    LoadStage();
                }
            }
        }
        else // 스테이지 안일때
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                StopAllCoroutines();
                ClearStage();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                DestroyImmediate(curStageGameObject);
                LoadStage();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackToMenu();
            }
        }
    }

    public void LoadStage()
    {
        StartCoroutine(FindBox());

        if (selectStageNum <= stageList.Stages.Count)
        {
            isInStage = true;
            print($"1: {isInStage}");

            if (currentMinimap != null)
            {
                DestroyImmediate(currentMinimap);
                currentMinimap = null;
            }

            currentStageSO = stageList.Stages[selectStageNum]; // 현재 스테이지

            curStageGameObject = Instantiate(currentStageSO.stagePref, Vector3.zero, Quaternion.identity, stageTrm); // 스테이지 생성

            stageSelectTrm.gameObject.SetActive(false);
            gameCanvas.SetActive(true);
            gameCanvas.GetComponentInChildren<DescriptionPanel>().SetPanel(currentStageSO);
            print($"2: {isInStage}");

            PlayerManager.Instance.SetNewPlayers(currentStageSO);
            CameraMovement.Instance.FindItems();
            print($"3: {isInStage}");
        }
        else
        {
            print("준비된 스테이지가 아닙니다람쥐");
        }
    }

    public void ClearStage()
    {
        print("Clear");
        isInStage = false;

        ClearAnim.SetTrigger("Clear");
        BoxManager.Instance.CleanBox();

        gameCanvas.SetActive(false);
        Invoke(nameof(ClearParticle), 1);

        currentStageSO.IsClear = true;
		selectStageNum++;

		StartCoroutine(StageLoad());
    }

    private void ClearParticle()
    {
        clearParticle.Play();
    }

    private IEnumerator StageLoad()
    {
        yield return new WaitForSeconds(1f);
        DestroyImmediate(curStageGameObject);
        yield return new WaitForSeconds(1f);

        LoadStage();
    }

    IEnumerator FindBox()
    {
        yield return new WaitForSeconds(0.2f);
        BoxManager.Instance.FindBox();
    }

    public void SetIsInStage(bool value)
    {
        print("set");
        isInStage = value;
    }

    #region UI
    public void SetSelectStageUI()
    {
        isInStage = false;
        stageSelectTrm.localPosition = Vector3.zero;

        bool isClear = true;

        for (int i = 0; i < stageList.Stages.Count; ++i)
        {
            StageUI stage = Instantiate(stageUIPrefab, stageSelectUITrm);
            stage.SetUI(i + 1, isClear);

            isClear = stageList.Stages[i].IsClear;
        }

        stagesUI = stageSelectUITrm.GetComponentsInChildren<StageUI>();
        UpdateSelectStageUI(0);
    }

    public void UpdateSelectStageUI(int value)
    {
        if (currentMinimap != null)
        {
            DestroyImmediate(currentMinimap.gameObject);
        }
        stagesUI[selectStageNum].UnSelected();
        selectStageNum += value;
        selectStageNum = Mathf.Clamp(selectStageNum, 0, stagesUI.Length - 1);

        stageSelectUITrm.DOLocalMoveX(moveX * selectStageNum, 0.3f);

        stagesUI[selectStageNum].Selected();
        currentMinimap = Instantiate(stageList.Stages[selectStageNum].stagePref, minimapTrm);
    }

    public void BackToMenu()
    {
        isInStage = false;

        for (int i = 0; i < stagesUI.Length; ++i)
        {
            DestroyImmediate(stagesUI[i].gameObject);
        }

        BoxManager.Instance.CleanBox();

        stageSelectTrm.gameObject.SetActive(true);
        gameCanvas.SetActive(false);
        
        DestroyImmediate(curStageGameObject);

        SetSelectStageUI();
    }
    #endregion
}
