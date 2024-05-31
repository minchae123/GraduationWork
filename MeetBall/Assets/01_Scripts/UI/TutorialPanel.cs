using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private float _panelSize;


    private RectTransform _panel;
    private Sequence _seq;

    private CanvasGroup cg;
    private TextMeshProUGUI explainText;
    private Image highlightImage;

    public bool isWait { get; set; }
    public bool isTwin { get; private set; }


    private void Awake()
    {
        _seq = DOTween.Sequence();
        _panel = GetComponent<RectTransform>();
    }

    public void InitializeFirstTut()
    {
        gameObject.SetActive(true);
        cg = GetComponent<CanvasGroup>();
        highlightImage = transform.Find("Highlight").GetComponent<Image>();
        explainText = transform.Find("TextPanel/TutorialText").GetComponent<TextMeshProUGUI>();

        cg.alpha = 0;
        highlightImage.raycastTarget = true;
    }

    public void ResetFirstTut()
    {
        gameObject.SetActive(false);
        highlightImage.raycastTarget = false;
    }

    public void SetFirstTutorial(Transform parent, string text)
    {
        cg.alpha = 1;
        isWait = true;

        _panel.SetParent(parent);
        _panel.localPosition = Vector3.zero;
        _panel.sizeDelta = Vector2.zero;

        explainText.text = text;

        transform.localScale = Vector3.one * 10;

        transform.DOScale(1f, 0.6f).SetEase(Ease.InOutExpo).OnComplete(()=>
       {
           highlightImage.DOFade(0.2f, 0.3f).OnComplete(() => highlightImage.DOFade(0.0f, 0.2f));
       });
    }

    public void NextTut()
    {
        cg.DOFade(0f, 1.0f);
    }

    public void ShowTutorial(Action action = null)
    {
        isWait = true;
        isTwin = true;

        _seq.Append(_panel.DOScaleX(_panelSize + 0.05f, 0.8f).SetEase(Ease.InOutQuint))
                .Append(_panel.GetComponent<Image>().DOFade(1, .8f)
                .OnComplete(() =>
                {
                    _seq.Append(_panel.DOScaleX(_panelSize, .2f));
                    isTwin = false;
                    action();
                }));
    }

    public void CloseTutorial()
    {
        isTwin = true;

        _seq.Append(_panel.DOScaleX(0, .8f).SetEase(Ease.InOutQuint))
           .Join(_panel.GetComponent<Image>().DOFade(0, .8f).SetEase(Ease.InOutQuint)
           .OnComplete(() =>
           {
               isTwin = false;
               isWait = false;
           }));
    }
}
