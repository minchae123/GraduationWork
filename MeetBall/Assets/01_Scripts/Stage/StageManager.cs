using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //stages = stageTrm.GetComponentsInChildren<StageUI>();

		//for(int i = 0; i < stages.Length; ++i)
		//{
		//    stages[i].SetNumber(i + 1);
		//}

		curStage = GameManager.Instance.curStage;

        ClearStage();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
        {
            ClearStage();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
			Destroy(curStageGameObject);
            LoadStage(curStage);
		}
	}

	public void LoadStage(int stageNum)
    {
        if(stageNum <= stageList.Stages.Count)
        {
            curStageGameObject = Instantiate(stageList.Stages[stageNum - 1].stagePref, Vector3.zero, Quaternion.identity); // 스테이지 생성
            CameraManager.Instance.NewControl();

            isInStage = true;
        }
        else
        {
            print("준비된 스테이지가 아닙니다람쥐");
        }

    }

    public void ClearStage()
    {
        GameManager.Instance.StageUp(); // 스테이지 수 올려주고

        Destroy(curStageGameObject);
		curStage = GameManager.Instance.curStage;
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
