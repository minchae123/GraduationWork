using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialStageManager : MonoSingleton<TutorialStageManager>
{
    private bool isInStage = false;

    [Header("===============")]
    [Header("Stage")]
    [SerializeField] private Transform stageTrm;
    [SerializeField] private StageUI[] stages;

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
    [SerializeField] private Transform selectStageTrm;
    [SerializeField] private StageUI stageUIPrefab;

    private int selectStageNum = 0;
    private StageUI[] stageUISs;
    private float moveX = -300f;


    [Header("===============")]
    [Header("ETC")]
    [SerializeField] private GameObject gameCanvas;

    private void Start()
    {
        SetSelectStageUI();

        clearParticle = GameObject.Find("ClearParticle").GetComponent<ParticleSystem>();
        ClearAnim = GameObject.Find("ClearUIAnim").GetComponent<Animator>();

        //StartCoroutine(StageLoad());
    }

    private void Update()
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
            StartCoroutine(FindBox());
        }

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
            StartCoroutine(StageLoad());
        }
    }

    public void LoadStage()
    {
        if (selectStageNum <= stageList.Stages.Count)
        {
            currentStageSO = stageList.Stages[selectStageNum]; // 현재 스테이지

            curStageGameObject = Instantiate(currentStageSO.stagePref, Vector3.zero, Quaternion.identity, stageTrm); // 스테이지 생성
            isInStage = true;

            gameCanvas.SetActive(true);
            gameCanvas.GetComponentInChildren<DescriptionPanel>().SetPanel(currentStageSO);

            PlayerManager.Instance.SetNewPlayers(currentStageSO);
            CameraMovement.Instance.FindItems();
        }
        else
        {
            print("준비된 스테이지가 아닙니다람쥐");
        }
    }

    public void ClearStage()
    {
        ClearAnim.SetTrigger("Clear");

        BoxManager.Instance.CleanBox();

        gameCanvas.SetActive(false);
        Invoke(nameof(ClearParticle), 1);
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
        StartCoroutine(FindBox());
    }

    IEnumerator FindBox()
    {
        yield return new WaitForSeconds(0.2f);

        BoxManager.Instance.FindBox();
    }

    public void ResetStage()
    {
        isInStage = false;
    }


    #region UI
    public void SetSelectStageUI()
    {
        selectStageTrm.localPosition = Vector3.zero;

        for (int i = 0; i < stageList.Stages.Count; ++i)
        {
            StageUI stage = Instantiate(stageUIPrefab, selectStageTrm);
            stage.SetUI(i + 1);
        }

        stageUISs = selectStageTrm.GetComponentsInChildren<StageUI>();
        stageUISs[selectStageNum].Selected();
    }

    public void UpdateSelectStageUI(int value)
    {
        stageUISs[selectStageNum].UnSelected();

        selectStageNum += value;
        selectStageNum = Mathf.Clamp(selectStageNum, 0, stageUISs.Length - 1);

        selectStageTrm.DOLocalMoveX(moveX * selectStageNum, 1.2f);

        stageUISs[selectStageNum].Selected();
    }
    #endregion
}
