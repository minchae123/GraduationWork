using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private float _panelSize;

    private RectTransform _panel;

    public bool isWait { get; set; }
    public bool isTwin { get; private set; }

    private void Awake()
    {
        _panel = GetComponent<RectTransform>();
    }

    public void ShowTutorial(Action action = null)
    {
        isWait = true;
        isTwin = true;

        _panel.DOScaleX(_panelSize, 1f).SetEase(Ease.InOutQuint)
            .OnComplete(() =>
            {
                isTwin = false;
                action();
            });
    }

    public void CloseTutorial()
    {
        isTwin = true;

        _panel.DOScaleX(0, 1f).SetEase(Ease.InOutQuint)
            .OnComplete(() =>
            {
                isTwin= false;
                isWait = false;
            });
    }
}
