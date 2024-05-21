using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private float _panelSize;

    private RectTransform _panel;

    public bool isWait { get; set; }

    private void Awake()
    {
        _panel = GetComponent<RectTransform>();
    }

    public void ShowTutorial(Action action = null)
    {
        isWait = true;

        _panel.DOScaleX(_panelSize, 1f)
            .OnComplete(() => { action(); });
    }

    public void CloseTutorial()
    {
        _panel?.DOScaleX(0, 1f)
            .OnComplete(()=> isWait = false);
    }
}
