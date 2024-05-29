using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GimmickExplain : MonoBehaviour
{
    [SerializeField] Gimmick gimmick;
    [SerializeField] private int objLayer;

    private TextMeshProUGUI explainTxt;
    private TutorialPanel panel;

    private bool isClick = false;

    private int stageNum;

    private void Awake()
    {
        panel = FindObjectOfType<TutorialPanel>();
        explainTxt = panel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        CickObj();
    }

    private void CickObj()
    {
        if (!Input.GetMouseButtonDown(0) || panel.isTwin) return;

        panel.CloseTutorial();

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
                    print(layer.value);
                    isClick = true;

                    stageNum = i;
                    explainTxt.text = gimmick.Explain[stageNum];

                    Vector3 hitPos = Camera.main.WorldToScreenPoint(hit.transform.position);
                    transform.position = hitPos;

                    panel.ShowTutorial();
                    break;
                }
            }
        }
    }

    public void RoadStage()
    {
        PlayerPrefs.SetInt(GameManager.Instance.stage, stageNum);
        SceneManager.LoadScene("TutorialPlay");
    }
}
