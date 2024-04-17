using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class StageUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
        StageManager.Instance.LoadStage(stageNum);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1.0f, 0.3f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOShakeScale(1, 1, 10, 1, false);
    }
}
