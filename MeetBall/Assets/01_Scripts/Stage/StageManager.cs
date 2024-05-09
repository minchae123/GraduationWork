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

    [SerializeField] private Transform stageTrm;
    [SerializeField] private StageUI[] stages;

    [SerializeField] private StageListSO stageList;

    [Header("Clear")]
    public Animator ClearAnim;
    [SerializeField] private ParticleSystem clearParticle;

    private GameObject curStageGameObject;

    private int curStage;

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
            StageSO currentStage = stageList.Stages[stageNum - 1]; // ���� ��������

            curStageGameObject = Instantiate(currentStage.stagePref, Vector3.zero, Quaternion.identity, stageTrm); // �������� ����
            isInStage = true;
            
            CameraManager.Instance.NewControl();
            PlayerManager.Instance.SetNewPlayers(currentStage);
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
