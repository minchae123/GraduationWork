using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private Image lockImage;

    private bool canPlay = false;
    public bool CanPlay => canPlay;

    private bool isSelect = false;
    public bool IsSelect => isSelect;

    public void SetUI(int stage, bool clear)
    {
        stageText.text = $"{stage}";
        lockImage.gameObject.SetActive(!clear);

        canPlay = clear;
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
