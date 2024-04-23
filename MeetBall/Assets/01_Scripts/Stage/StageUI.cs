using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class StageUI : MonoBehaviour
{
    public StageSO stage;
    public bool IsSelected = false;
    [SerializeField]private TextMeshProUGUI stageNumText;

    private void Start()
    {
        IsSelected = false;
    }

    public void Selected()
    {
        IsSelected = true;
        transform.DOScale(1.3f, 0.6f);
    }
    public void UnSelected()
    {
        IsSelected = false;
        transform.DOScale(1f, 0.6f);
    }

    internal void SetUI(StageSO stage, int stageNum)
    {
        if (stageNum < 10) stageNumText.text = $"0{stageNum}";
        else stageNumText.text = $"{stageNum}";

        this.stage = stage;
    }
}
