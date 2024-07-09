using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TutorialStageManager : MonoSingleton<TutorialStageManager>
{
    private bool isInStage = false;
    public bool IsInStage => isInStage;

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

    private int selectStageNum = 0;
    private StageUI[] stageUISs;

    [Header("===============")]
    [Header("ETC")]
    [SerializeField] private GameObject gameCanvas;

    private void Start()
    {
        clearParticle = GameObject.Find("ClearParticle").GetComponent<ParticleSystem>();
        ClearAnim = GameObject.Find("ClearUIAnim").GetComponent<Animator>();
        
        LoadStage();
        StartCoroutine(FindBox());
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
    }

    public void LoadStage()
    {
        selectStageNum = PlayerPrefs.GetInt(GameManager.Instance.stage);

        if (selectStageNum <= stageList.Stages.Count)
        {
            isInStage = true;
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
        isInStage = false;
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
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("GimmickExplain");
    }

    IEnumerator FindBox()
    {
        yield return new WaitForSeconds(0.2f);

        BoxManager.Instance.FindBox();
    }

    public bool MergeColor(OriginColorEnum c1, OriginColorEnum c2)
    {
        TargetColorEnum final = StageManager.Instance.CurrentStageSO.targetColor;
        if ((int)c1 + (int)c2 == (int)final)
        {
            return true;
        }
        return false;
    }

    public void SetIsInStage(bool value)
    {
        isInStage = value;
    }
}
