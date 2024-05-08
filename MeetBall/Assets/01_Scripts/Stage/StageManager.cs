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

    private GameObject curStageGameObject;

    private int curStage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
		curStage = GameManager.Instance.curStage;

        ClearStage();
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
			Destroy(curStageGameObject);
            LoadStage(curStage);
            StartCoroutine(FindBox());
		}
	}

	public void LoadStage(int stageNum)
    {
        if(stageNum <= stageList.Stages.Count)
        {
            curStageGameObject = Instantiate(stageList.Stages[stageNum - 1].stagePref, Vector3.zero, Quaternion.identity, stageTrm); // 스테이지 생성
            CameraManager.Instance.NewControl();

            isInStage = true;
            PlayerManager.Instance.SetNewPlayers(stageList.Stages[stageNum - 1]);
        }
        else
        {
            print("준비된 스테이지가 아닙니다람쥐");
        }
    }

    public void ClearStage()
    {
        StartCoroutine(StageLoad());
    }

    private IEnumerator StageLoad()
    {
        GameManager.Instance.StageUp(); // 스테이지 수 올려주고

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
