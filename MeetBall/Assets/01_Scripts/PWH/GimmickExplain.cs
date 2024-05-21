using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GimmickExplain : MonoBehaviour
{
    [SerializeField] private string[] explain;
    [SerializeField] private int objLayer;
    [SerializeField] private TextMeshProUGUI explainTxt;

    private void Update()
    {
        CickObj();
    }

    private void CickObj()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            LayerMask layer = hit.transform.gameObject.layer;

            for(int i = objLayer; i < objLayer + 5; i++)//나중에 기믹 수에 맞게 포문 돌아가게 바꾸기
            {
                if(layer.value == i)
                {
                    explainTxt.text = explain[i - objLayer];
                    break;
                }
            }

            Vector3 hitPos = Camera.main.WorldToScreenPoint(hit.transform.position);
            transform.position = hitPos;
;
            if (TryGetComponent(out TutorialPanel panel))
                panel.ShowTutorial();
        }
        else
        {

        }
    }
}
