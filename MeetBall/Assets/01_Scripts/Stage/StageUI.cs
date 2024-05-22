using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class StageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageText;

    private bool isSelect = false;
    public bool IsSelect => isSelect;

    public void SetUI(int stage)
    {
        stageText.text = $"{stage}";
    }

    public void Selected()
    {
        transform.DOScale(1.3f, 0.6f);
        isSelect = true;
    }
    public void UnSelected()
    {
        transform.DOScale(1f, 0.6f);
        isSelect = false;
    }
}
