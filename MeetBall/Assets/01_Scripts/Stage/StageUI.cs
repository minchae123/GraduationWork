using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageUI : MonoBehaviour, IPointerClickHandler
{
    private int stageNum = 0;
    [SerializeField] private TextMeshProUGUI stageText;

    public void SetNumber(int num)
    {
        stageNum = num;
        stageText.text = $"{stageNum}";

        if (stageNum < 10) stageText.text = $"0{stageNum}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print(stageNum);
        Stage.Instance.LoadStage(stageNum);
    }
}
