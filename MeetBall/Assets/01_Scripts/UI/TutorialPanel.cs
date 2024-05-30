using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private float _panelSize;

    private RectTransform _panel;
    private Sequence _seq;

    public bool isWait { get; set; }
    public bool isTwin { get; private set; }

    private void Awake()
    {
        _seq = DOTween.Sequence();
        _panel = GetComponent<RectTransform>();
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

        //foreach (RectTransform obj in _panel)    //자식오브젝트까지 해보려했는데 필요할까요구르ㅡ=트
        //{
        //    if (obj.TryGetComponent(out Image img))
        //    {
        //        _seq.Join(obj.DOScaleX(_panelSize, 1f).SetEase(Ease.OutBounce))
        //        .Join(img.DOFade(1, 1).SetEase(Ease.InOutQuint)
        //        .OnComplete(() =>
        //        {
        //            isTwin = false;
        //            action();
        //        }));
        //    }
        //}
    }

    public void CloseTutorial(Action action = null)
    {
        action();
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
