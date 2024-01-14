using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public void ScaleRectTransform(RectTransform obj, Vector3 endValue, float duraion, Ease ease = Ease.Linear, params Action[] action)
    {
        obj.DOScale(endValue, duraion).SetEase(ease).OnComplete(() =>
        {
            for (int i = 0; i < action.Length; i++)
            {
                action[i]?.Invoke();
            }
        });
    }
}
