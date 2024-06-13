using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class GimmickExplain : MonoBehaviour
{
    [SerializeField] Gimmick gimmick;
    [SerializeField] private int objLayer;

    private TextMeshProUGUI explainTxt;
    private TutorialPanel panel;
    private VideoPlayer video;

    private bool isClick = false;

    private int stageNum = 0;
    private int tutorialNum = 0;

    private void Awake()
    {
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
        yield return new WaitForSeconds(1.5f);

        if (StageManager.Instance.CurrentStageSO.IsStartGimmick)
        {
            explainTxt.text = gimmick.Explain[tutorialNum];
            video.clip = gimmick.video[tutorialNum];

            panel.ShowTutorial(() => video.Play());
            tutorialNum++;
        }
    }

    private void CickObj()
    {
        if (!Input.GetMouseButtonDown(0) || panel.isTwin) return;

        panel?.CloseTutorial(() => video.Stop());
        if (!isClick)
            ExplainPanel();
        else
            isClick = false;
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
