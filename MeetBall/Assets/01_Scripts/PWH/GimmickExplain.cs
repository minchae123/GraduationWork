using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class GimmickExplain : MonoBehaviour
{
    [SerializeField] Gimmick gimmick;
    [SerializeField] private int objLayer;

    public TutorialPanel panel { get; private set; }

    private StageManager stageManager;
    private TextMeshProUGUI explainTxt;
    private VideoPlayer video;

    private bool isClick = false;

    private int stageNum = 0;

    private void Awake()
    {
        stageManager = StageManager.Instance;

        panel = GetComponent<TutorialPanel>();
        video = GetComponentInChildren<VideoPlayer>();
        explainTxt = panel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        CickObj();
    }

    public IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(0.5f);

        if (stageManager.CurrentStageSO.IsStartGimmick && !stageManager.IsReStart)
        {
            explainTxt.text = gimmick.Explain[stageManager.CurrentStageSO.gimmickNum];
            video.clip = gimmick.video[stageManager.CurrentStageSO.gimmickNum];

            panel.ShowTutorial(() => video.Play());
        }
    }

    private void CickObj()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (panel.isTwin) return;

            if (panel.isWait)
                panel?.CloseTutorial(() => video.Stop());

            if (!isClick)
                ExplainPanel();
            else
                isClick = false;
        }
    }

    private void ExplainPanel()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        for (int i = 0; i < gimmick.Explain.Length; i++)
        {
            if (Physics.Raycast(ray, out hit))
            {
                LayerMask layer = hit.transform.gameObject.layer;
                if (layer.value == i + objLayer)
                {
                    isClick = true;
                    stageNum = i;

                    video.clip = gimmick.video[stageNum];
                    explainTxt.text = gimmick.Explain[stageNum];

                    Vector3 hitPos = Camera.main.WorldToScreenPoint(hit.transform.position);

                    transform.position = hitPos;

                    panel.ShowTutorial(() => video.Play());
                    break;
                }
            }
        }
    }
}
