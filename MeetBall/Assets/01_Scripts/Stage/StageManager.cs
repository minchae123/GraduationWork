using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

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

        StartCoroutine(StageLoad());
    }

    private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
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
        if(stageNum <= stageList.Stages.Count)
        {
            currentStageSO = stageList.Stages[stageNum - 1]; // ���� ��������

            curStageGameObject = Instantiate(currentStageSO.stagePref, Vector3.zero, Quaternion.identity, stageTrm); // �������� ����
            isInStage = true;

            gameCanvas.SetActive(true);

            CameraManager.Instance.NewControl();
            PlayerManager.Instance.SetNewPlayers(currentStageSO);
        }
        else
        {
            print("�غ�� ���������� �ƴմϴٶ���");
        }
    }

    public void ClearStage()
    {
        ClearAnim.SetTrigger("Clear");

        BoxManager.Instance.CleanBox();
        clearParticle.Play();

        StartCoroutine(StageLoad());
    }

    private IEnumerator StageLoad()
    {
        gameCanvas.SetActive(false);
        GameManager.Instance.StageUp(); // �������� �� �÷��ְ�

        Destroy(curStageGameObject);
        curStage = GameManager.Instance.curStage;

        yield return new WaitForSeconds(2);
        LoadStage(curStage);
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
}
