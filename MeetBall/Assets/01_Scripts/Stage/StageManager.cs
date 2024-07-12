using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
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
    private Tutorial tutorialPanel;

    public void Awake()
    {
        clearParticle = GameObject.Find("ClearParticle").GetComponent<ParticleSystem>();
        ClearAnim = GameObject.Find("ClearUIAnim").GetComponent<Animator>();
        stageSelectUITrm = stageSelectTrm.Find("StageSelect");
        gimmick = FindFirstObjectByType<GimmickExplain>();

        clearText = ClearAnim.transform.Find("ClearText").GetComponent<TextMeshProUGUI>();
        //tutorialPanel = gameCanvas.transform.GetComponentInChildren<Tutorial>();

        isInStage = false;


    }

    private void Start()
    {
        //SetSelectStageUI();
        //gameCanvas.SetActive(false);

        //tutorialPanel.gameObject.SetActive(false);
         //StartCoroutine(StageLoad());
    }

    public void EnterStage()
    {
        if (IsInStage) return;
        if (!stagesUI[selectStageNum].CheckCanPlay())
        {
            print("���� Ŭ����X");
        }
        else
        {
            LoadStage();
        }
    }

    public void MoveStage(Direction dir)
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
    }

    private void Update()
    {
        if (!isInStage) // �������� ���� ��
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
        else // �������� ���϶�
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

            if (Input.GetKeyDown(KeyCode.Escape))
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

    public void SetStageNumber(int num)
    {
        selectStageNum = num;
        LoadStage();
    }


    public void LoadStage()
    {
        StartCoroutine(FindBox());

        if (selectStageNum <= stageList.Stages.Count)
        {
            if (currentMinimap != null)
            {
                DestroyImmediate(currentMinimap);
                currentMinimap = null;
            }

            currentStageSO = stageList.Stages[selectStageNum]; // ���� ��������

            curStageGameObject = Instantiate(currentStageSO.stagePref, StageTrm); // �������� ����

            stageSelectTrm.gameObject.SetActive(false);
            gameCanvas.SetActive(true);
            gameCanvas.GetComponentInChildren<DescriptionPanel>().SetPanel(currentStageSO);

            PlayerManager.Instance.SetNewPlayers(currentStageSO);
            CameraMovement.Instance.FindItems();

            StartCoroutine(gimmick.StartTutorial());
            StartCoroutine(WaitForGenerate());

            if (selectStageNum == 0)
            {
                //StartCoroutine(tutorialPanel.TutorialPannel());
            }
        }
        else
        {
            SceneManager.LoadScene(2);
            print("�غ�� ���������� �ƴմϴٶ���");
        }
    }

    public void ClearStage(bool isClear)
    {
        if (isClear)
        {
            currentStageSO.IsClear = true;

            if(GameManager.Instance.gameData.bigStage.TryGetValue(currentStageSO.bigStageName, out var keys))
            {
                keys[selectStageNum] = true;
            }
            else
            {
                print("����");
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
        BackToMenu();
    }

    private void ClearParticle()
    {
        clearParticle.Play();
    }

    private IEnumerator StageLoad()
    {
        yield return new WaitForSeconds(1f);
        DestroyImmediate(curStageGameObject);
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
        currentMinimap.transform.position = Vector3.zero;
    }

    public void BackToMenu()
    {
        isInStage = false;
        print("ss");
        if (selectStageNum == 0)
        {
            Cursor.lockState = CursorLockMode.None;

            StopAllCoroutines();
            //tutorialPanel.ResetPanel();
        }

        for (int i = 0; i < stagesUI.Length; ++i)
        {
            DestroyImmediate(stagesUI[i].gameObject);
        }

        BoxManager.Instance.CleanBox();

        gimmick.panel.ClearSequence();
        gimmick.panel.CloseTutorial();
        stageSelectTrm.gameObject.SetActive(true);
        gameCanvas.SetActive(false);

        DestroyImmediate(curStageGameObject);
        PlayerManager.Instance.ResetPlayers();
        //CameraMovement.Instance.CameraReset();

        SetSelectStageUI();
    }
    #endregion
}
