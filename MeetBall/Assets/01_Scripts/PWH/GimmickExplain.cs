using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GimmickExplain : MonoBehaviour
{
    [SerializeField] private string[] explain;
    [SerializeField] private int objLayer;
    [SerializeField] private TextMeshProUGUI explainTxt;

    private TutorialPanel panel;
    private bool isClick = false;

    private int stageNum;

    private void Awake()
    {
        panel = GetComponent<TutorialPanel>();
    }

    private void Update()
    {
        CickObj();
    }

    private void CickObj()
    {
        if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject() || panel.isTwin) return;

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

        if (Physics.Raycast(ray, out hit))
        {
            isClick = true;
            LayerMask layer = hit.transform.gameObject.layer;

            for (int i = objLayer; i < objLayer + 5; i++)//나중에 기믹 수에 맞게 포문 돌아가게 바꾸기
            {
                if (layer.value == i)
                {
                    stageNum = i - objLayer;
                    explainTxt.text = explain[stageNum];
                    break;
                }
            }

            Vector3 hitPos = Camera.main.WorldToScreenPoint(hit.transform.position);
            transform.position = hitPos;

            panel.ShowTutorial();
        }
    }

    public void RoadStage()
    {
        //GameManager.Instance.curStage = stageNum;
        SceneManager.LoadScene("TutorialPlay");
    }
}
