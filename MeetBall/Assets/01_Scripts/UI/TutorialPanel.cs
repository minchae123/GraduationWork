using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private float _panelSize;

    private RectTransform _panel;

    private void Awake()
    {
        _panel = GetComponent<RectTransform>();
    }

    public void ShowTutorial()
    {
        _panel.DOScaleY(_panelSize, 1f);
    }

    public void CloseTutorial()
    {
        _panel?.DOScaleY(0, 1f);
    }
}
