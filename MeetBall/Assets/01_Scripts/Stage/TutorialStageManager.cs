using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TutorialStageManager : MonoBehaviour
{
    public static TutorialStageManager Instance;

    private bool isInStage = false;

    [Header("Stage")]
    [SerializeField] private Transform stageTrm;
    [SerializeField] private StageUI[] stages;

    [SerializeField] private StageListSO stageList;

    private StageSO currentStageSO;
    public StageSO CurrentStageSO => currentStageSO;

    private GameObject curStageGameObject;
    private int curStage;

    [Header("Clear")]
    private Animator ClearAnim;
    private ParticleSystem clearParticle;

    [Header("ETC")]
    [SerializeField] private GameObject gameCanvas;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        clearParticle = GameObject.Find("ClearParticle").GetComponent<ParticleSystem>();
        ClearAnim = GameObject.Find("ClearUIAnim").GetComponent<Animator>();

        curStage = GameManager.Instance.curStage;

        LoadStage(curStage);
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
            LoadStage(curStage);
            StartCoroutine(FindBox());
        }
    }

    public void LoadStage(int stageNum)
    {
        if (stageNum <= stageList.Stages.Count)
        {
            currentStageSO = stageList.Stages[stageNum - 1]; // 현재 스테이지

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
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("GimmickExplain"); // 튜토리얼로(기믹)
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
}
